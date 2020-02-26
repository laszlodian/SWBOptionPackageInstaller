using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class CommandControler
    {
        public static CommandControler Instance = null;
        public string sunriseWorkbenchPath = @"C:\_SWB\OptionPackageInstallerTest\SWB";
        /// <summary>
        /// The first argument must look like this: jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/"
        /// The second argument is the name of the feature and it looks like this: com.kuka.kmpOmniMove.swb.feature.feature.group -This could be listed out with 'featuresListCommand'
        /// </summary>
        public string installCommandFormat = @"{2}\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} -installIUs {1}";
        /// <summary>
        /// First argument should look like this: "file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Sunrise/SunriseWorkbench-1.16.2.16-win32-x86.zip!/"
        /// </summary>
        public string featuresListCommandFormat = @"{1}\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash{0} -list";
        private List<string> packages = new List<string>();

        /// <summary>
        /// Complete example:
        /// List command:
        /// SunriseWorkbench\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/" -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345991;01;KUKA Sunrise.Mobility 1.11 KMP 1500.zip!/" -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345992;01;KUKA Sunrise.Mobility 1.11 KMP 200.zip!/" -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Sunrise/SunriseWorkbench-1.16.2.16-win32-x86.zip!/" -list
        /// 
        /// Install command:
        /// SunriseWorkbench\eclipsec.exe -clean -purgeHistory -application org.eclipse.equinox.p2.director -noSplash 
        /// -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345989;01;KUKA Sunrise.Mobility 1.11 KMP_KMR oM.zip!/" 
        /// -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345991;01;KUKA Sunrise.Mobility 1.11 KMP 1500.zip!/" 
        /// -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Mobility/0000345992;01;KUKA Sunrise.Mobility 1.11 KMP 200.zip!/" 
        /// -repository jar:"file:///C:/_SWB/OptionPackageInstallerTest/SWB/repos/Sunrise/SunriseWorkbench-1.16.2.16-win32-x86.zip!/" 
        /// -installIUs com.kuka.kmpOmniMove.swb.feature.feature.group -installIUs com.kuka.kmpOmniMove1500.swb.feature.feature.group 
        /// -installIUs com.kuka.kmpOmniMove200.swb.feature.feature.group 
        /// </summary>
        private DirectoryInfo tempDir;

        #region Properties

        private string optionPackageList;
        private string featuresList;
        private List<string> listOfFeatures=new List<string>();

        public string OptionPackageList
        {
            get { return optionPackageList; }
            set { optionPackageList = value; }
        } 

        #endregion

        public CommandControler()
        {
            Instance = this;
        }

        public void InstallOptionPackages()
        {
            /*List the features and store them for the install command
                * Get the names of the option packages and store them for the install command with list features command
                * Build the install command
                * Run the install command 
                */
            string commandToRun = string.Empty;
            optionPackageList =     ArtifactHandler.Instance.CollectOptionPackages(MainForm.Instance.PathOfOptionPackages);
            MainForm.Instance.ConfigureDataGridView();
            if (OptionPackageList == string.Empty || OptionPackageList == null)
            {
                return;
            }
            else
            {
                OptionPackageList = OptionPackageList.Replace('\\', '/');
                MessageBox.Show(string.Format("Option packages: {0}", OptionPackageList));
                Trace.TraceInformation("Option packages: {0}", OptionPackageList);
            }



            commandToRun = string.Format(featuresListCommandFormat, OptionPackageList, sunriseWorkbenchPath).Replace('\\','/');
            Trace.TraceInformation("Command to run for list features:{1} {0}", commandToRun,Environment.NewLine);
            MessageBox.Show(string.Format("Command to list features: {0}", commandToRun));
                        
         
            string featuresListResult = RunCommand(commandToRun);
            string consoleOut=Console.ReadLine();
            featuresList = FormatFeaturesList(featuresListResult);

            Trace.TraceInformation("Result of the list features command: {0}", featuresList);


            commandToRun = string.Format(installCommandFormat, OptionPackageList, featuresList, sunriseWorkbenchPath);
            Trace.TraceInformation("Command to run for install packages: {0}", commandToRun);
            string installResult = RunCommand(commandToRun);

            MessageBox.Show(installResult);
            Trace.TraceInformation("Result of the install command: {0}", installResult);

        }

        private string FormatFeaturesList(string featuresList)
        {
            Trace.TraceInformation("Features List:{1}{0}{1}",featuresList,Environment.NewLine);
            featuresList=featuresList.Split('\n')[1];
           foreach (string feature in featuresList.Split(' '))
            {
                Trace.TraceInformation("Formated console: Feature: {0}",feature);

                listOfFeatures.Add(feature);

            }
            return "";
        }

        private string RunCommand(string commandToRun)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(string.Format(commandToRun));
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            return cmd.StandardOutput.ReadToEnd();
        }


       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWBOptionPackageInstaller
{
    public partial class Form1 : Form
    {

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
        private List<string> packages=new List<string>();

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



        private string optionPackageList;

        public string OptionPackageList
        {
            get { return optionPackageList; }
            set { optionPackageList = value; }
        }


        public Form1()
        {
            InitializeComponent();

            this.tbPackagesPath.Text = @"C:\_SWB\OptionPackageInstallerTest\optionpackages";
        }



        public void InstallOptionPackages()
        {
            /*List the features and store them for the install command
                * Get the names of the option packages and store them for the install command with list features command
                * Build the install command
                * Run the install command 
                */
            string commandToRun = string.Empty;
            if (OptionPackageList == string.Empty || OptionPackageList == null)
            {
                return;
            }
            else
            {
                OptionPackageList = OptionPackageList.Replace('\\', '/');
                MessageBox.Show(string.Format("Option packages: {0}", OptionPackageList));
            }

            

            commandToRun = string.Format(featuresListCommandFormat, OptionPackageList, sunriseWorkbenchPath);
            Trace.TraceInformation("Command to run for list features: {0}",commandToRun);
            MessageBox.Show(string.Format("Command to list features: {0}",commandToRun));

            string featuresList =  RunCommand(commandToRun);
            featuresList = FormatFeaturesList(featuresList);
            
            MessageBox.Show(featuresList);
            Trace.TraceInformation("Result of the list features command: {0}", featuresList);


            commandToRun = string.Format(installCommandFormat, OptionPackageList, featuresList, sunriseWorkbenchPath);
            Trace.TraceInformation("Command to run for install packages: {0}", commandToRun);
            string installResult= RunCommand(commandToRun);

            MessageBox.Show(installResult);
            Trace.TraceInformation("Result of the install command: {0}",installResult);

        }

        private string FormatFeaturesList(string featuresList)
        {
            throw new NotImplementedException();
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

        private void tbPackagesPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              optionPackageList=  CollectOptionPackages(tbPackagesPath.Text);
            }
            
            InstallOptionPackages();
        }

        private string CollectOptionPackages(string pathIn)
        {
            if (!CheckPath(pathIn))
                return string.Empty;

            DirectoryInfo optionPackagesPath=new DirectoryInfo(pathIn);

            foreach (FileInfo package in optionPackagesPath.GetFiles("*.zip"))
            {
                packages.Add(package.FullName);
            }

            string optionPackageList=  FormatPackageNames(packages);

            return optionPackageList;
        }

        private string FormatPackageNames(List<string> packages)
        {
            string stringToInsertCommand = string.Empty;

            foreach (string item in packages)
            {
                stringToInsertCommand += string.Format(" -repository jar:\"file:///{0}!/\"", item);
            }

            return stringToInsertCommand;
        }

        private bool CheckPath(string pathIn)
        {
            if (!Directory.Exists(pathIn))
            {
                MessageBox.Show(string.Format("The given directory doesn't exists: {0}",pathIn));
                return false;
            }
            else
                return true;

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
   public class ArtifactHandler
    {
        public static ArtifactHandler Instance=null;

        public List<string> packages = new List<string>();


        public ArtifactHandler()
        {
            Instance = this;
        }

        public string CollectOptionPackages(string pathIn)
        {
            if (!CheckStatesHandler.Instance.CheckPath(pathIn))
                return string.Empty;

            DirectoryInfo optionPackagesPath = new DirectoryInfo(pathIn);
            DataGridView dgv = MainForm.Instance.DataGridViewOfArtifacts;
            foreach (FileInfo package in optionPackagesPath.GetFiles("*.zip"))
            {
                packages.Add(package.FullName);

                
            }

            string optionPackageList = FormatPackageNames(packages);

            return optionPackageList;
        }

        private static void ExpandDataGridWithRows(DataGridView dgv, FileInfo package)
        {
            DataGridViewCheckBoxCell packageInstall = new DataGridViewCheckBoxCell();
            dgv.Rows.Add();
            dgv.Rows.Add(new object[] { package.Name, "version", packageInstall });
            dgv.Refresh();
            dgv.Update();
        }

        public string FormatPackageNames(List<string> packages)
        {
            string stringToInsertCommand = string.Empty;

            foreach (string item in packages)
            {
                stringToInsertCommand += string.Format(" -repository jar:\"file:///{0}!/\"", item);
            }

            return stringToInsertCommand;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
   public class PackageGridModel
    {

        #region Properties
        private DataGridViewCheckBoxCell _installCell;

        public DataGridViewCheckBoxCell InstallCell
        {
            get { return _installCell; }
            set { _installCell = value; }
        }

        private static string _optionPackageName = string.Empty;
        public static string OptionPackageName
        {
            get
            {
                return _optionPackageName;
            }
            set
            {
                _optionPackageName = value;
            }
        }

        private static string _optionPackageVersion = string.Empty;
        public static string Version
        {
            get
            {
                return _optionPackageVersion;
            }
            set
            {
                _optionPackageVersion = value;
            }
        } 
        #endregion



        public PackageGridModel(string optionPackageName,string optionPackageVersion,DataGridViewCheckBoxCell installCell)
        {
            _optionPackageName = optionPackageName;
            _optionPackageVersion = optionPackageVersion;
            _installCell = installCell;
        }
    }
}

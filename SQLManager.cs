using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWB_OptionPackageInstaller
{
    public class SQLManager
    {
        public static SQLManager Instance = null;

        public SQLManager()
        {
            Instance = this;
        }
    }
}

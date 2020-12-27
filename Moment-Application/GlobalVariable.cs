using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moment_Application
{
    public class GlobalVariable
    {
        public static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/DataArchive/";
    }
}

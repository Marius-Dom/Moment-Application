using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Moment_Application
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/DataArchive/";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Directory.Exists(dataPath))
                Application.Run(new InitForm());

            if (!File.Exists(dataPath + "Information.json"))
                Application.Run(new InitForm());

            Application.Run(new MainForm());
        }
    }
}

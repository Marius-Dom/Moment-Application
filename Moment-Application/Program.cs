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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Directory.Exists(GlobalVariable.dataPath))
                Application.Run(new InitForm());

            if (!File.Exists(GlobalVariable.dataPath + "Information.json"))
                Application.Run(new InitForm());

            Application.Run(new MainForm());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KL
{
    internal static class Program
    {
        internal static string MenuItemDir;
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            checkfolder();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using(new Form1())
                Application.Run();
          //  Application.Run(new Form1());
        }

        private static void checkfolder()
        {
            string dir = Path.GetDirectoryName(Application.ExecutablePath);
            MenuItemDir = Path.Combine(dir, "MenuItems");
            if (!Directory.Exists(MenuItemDir))
                Directory.CreateDirectory(MenuItemDir);
        }
    }
}

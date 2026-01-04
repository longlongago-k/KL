using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KL
{
    public class Shortcut
    {
        static WshShell shell = new WshShell();
        public static string GetShortcutLink(string shortcutName)
        {
            try
            {
                // ショートカットオブジェクトの取得
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutName);
                // ショートカットのリンク先の取得
                return shortcut.TargetPath.ToString();
            }catch
            {
                return shortcutName;
            }

        }
    }
}

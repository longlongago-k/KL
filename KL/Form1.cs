using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ToolStripMenuItemFolder.CreateTree(contextMenuStrip1, $@"{Application.StartupPath}\MenuItems");
            
            
        }

        //private void readRootItems()
        //{
        //    string itemDir = $@"{Application.StartupPath}\MenuItems";
        //    Directory.GetDirectories(itemDir)
        //        .Select(d => new ToolStripMenuItemFolder(d));
        //    Directory.GetFiles(itemDir)
        //        .Select(d =>
        //        {
        //            string name = d;
        //            if (d.EndsWith(".lnk"))
        //                name = Shortcut.GetShortcutLink(d);
        //            return new ToolStripMenuItemFile(name);
        //        });

        //}
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
            {
                SetForegroundWindow(new HandleRef(contextMenuStrip1, contextMenuStrip1.Handle));
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void toolStripMenuItemOpenMenuItemDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", Program.MenuItemDir);
        }
    }
}

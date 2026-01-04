using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KL
{
    internal class ToolStripMenuItemFile : ToolStripMenuItem
    {
        public string FullPath { get; set; }
        private static ShellContextMenu shellContextmenu = new ShellContextMenu();
        private MouseButtons mouseButtons;
        public ToolStripMenuItemFile()
        {
            InitializeComponent();
        }

        public ToolStripMenuItemFile(string fullPath) : base()
        {
            InitializeComponent();
            FullPath = fullPath;
            Text = Path.GetFileName(fullPath);
            //Image = Icon.ExtractAssociatedIcon(fullPath).ToBitmap();
            Image = ShellIcon.GetSmallIcon(fullPath)?.ToBitmap();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Image?.Dispose();
        }

        private void InitializeComponent()
        {
            // 
            // ToolStripMenuItemFile
            // 
            this.Click += new System.EventHandler(this.ToolStripMenuItemFile_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolStripMenuItemFile_MouseDown);

        }

        private void ToolStripMenuItemFile_Click(object sender, EventArgs e)
        {
            if (mouseButtons == MouseButtons.Left)
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = FullPath,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                };
                System.Diagnostics.Process.Start(startInfo);
            }
            else
            {
                shellContextmenu.ShowContextMenu(new FileInfo[] { new FileInfo(FullPath) }, Cursor.Position);
            }
        }

        private void ToolStripMenuItemFile_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtons = e.Button;
        }
    }
}

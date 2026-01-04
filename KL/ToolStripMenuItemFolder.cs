using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KL
{
    public class ToolStripMenuItemFolder : ToolStripMenuItem
    {
        public string FullPath { get; set; } = "";
        private static Bitmap folderIcon = ShellIcon.GetSmallFolderIcon().ToBitmap();

        public ToolStripMenuItemFolder()
        {
            InitializeComponent();
            DropDownOpening += ToolStripMenuItemFolder_DropDownOpening;
            DropDownItems.Add(new ToolStripMenuItem("dummy"));
            Image = folderIcon;
        }
        public ToolStripMenuItemFolder(string fullPath) : base()
        {
            InitializeComponent();
            FullPath = fullPath;
            Text = Path.GetFileName(fullPath);
            Image = folderIcon;
            DropDownOpening += ToolStripMenuItemFolder_DropDownOpening;
            DropDownItems.Add(new ToolStripMenuItem("dummy"));
        }


        public static void CreateTree(ToolStrip parent, string searchPath)
        {
            var subFolders = Directory.GetDirectories(searchPath)
            .Select(d => new ToolStripMenuItemFolder(d)).ToArray();
            var files = getChildFiiles(searchPath);
            if (subFolders.Length > 0 || files.Length > 0)
            {
                parent.Items.AddRange(subFolders);
                parent.Items.AddRange(files);
            }

        }
        private void ToolStripMenuItemFolder_DropDownOpening(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItemFolder;
            var subFolders = Directory.GetDirectories(menu.FullPath)
                .Select(d => new ToolStripMenuItemFolder(d)).ToArray();
            var files = getChildFiiles(menu.FullPath);
            if (subFolders.Length > 0 || files.Length > 0)
            {
                disposeItems();
                menu.DropDownItems.Clear();
                menu.DropDownItems.AddRange(subFolders);
                menu.DropDownItems.AddRange(files);
            }
            else
            {
                menu.DropDownItems.Clear();
                menu.DropDownItems.Add(new ToolStripMenuItem("( 空 )"));
            }

        }
        private static ToolStripMenuItem[] getChildFiiles(string fullPath)
        {
            return Directory.GetFiles(fullPath)
                    .Where(f => !f.StartsWith(".gitkeep"))
                    .Select(d => {
                        if (d.EndsWith(".lnk"))
                        {
                            string targetPath = Shortcut.GetShortcutLink(d);
                            if(Directory.Exists(targetPath))
                            {
                                return (ToolStripMenuItem)(new ToolStripMenuItemFolder(targetPath));
                            }
                            else
                                return (ToolStripMenuItem)(new ToolStripMenuItemFile(targetPath));

                        }
                        else
                            return (ToolStripMenuItem)(new ToolStripMenuItemFile(d));
                        }).ToArray();
        }

        private void disposeItems()
        {
            for (int i = DropDownItems.Count - 1; i >= 0; i--)
            {
                this.DropDownItems[i].Dispose();
            }
        }

        private void InitializeComponent()
        {
            // 
            // ToolStripMenuItemFolder
            // 
            this.DoubleClickEnabled = true;
            this.DoubleClick += new System.EventHandler(this.ToolStripMenuItemFolder_DoubleClick);

        }

        private void ToolStripMenuItemFolder_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(FullPath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMAD.NET;
using GMAD.NET.Addon;

namespace GMAD.NET.Frontend
{
    public partial class MainForm : Form
    {
        private Reader activeReader = new Reader();

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {DefaultExt = "*.gma", Filter = "Garry's Mod Packages (*.gma)|*.gma"};
            var selected = dialog.ShowDialog();

            if (selected != DialogResult.Cancel)
            {
                activeReader.Parse(dialog.FileName);
                propertyGmad.SelectedObject = activeReader;

                menuFiles.Enabled = true;

                foreach (var file in activeReader.Files)
                {
                    var seperatorPos = file.StrName.LastIndexOf("/", System.StringComparison.Ordinal);
                    var vdir = file.StrName.Substring(0, seperatorPos);
                    var group = listFiles.Groups[vdir];

                    if(group == null)
                    {
                        group = new ListViewGroup(vdir, vdir);
                        listFiles.Groups.Add(group);
                    }

                    var item = new ListViewItem(file.StrName.Substring(seperatorPos + 1)) {Tag = file, Group = group, ToolTipText = file.StrName};
                    group.Items.Add(item);
                    listFiles.Items.Add(item);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = sender as ListView;
            if (list == null) return;

            var items = list.SelectedItems;

            foreach (var file in from ListViewItem item in items select (FileFormat.FileEntry) item.Tag)
            {
                var save = new SaveFileDialog {FileName = file.StrName.Substring(file.StrName.LastIndexOf("/", System.StringComparison.Ordinal) + 1)};
                var selected = save.ShowDialog();
                
                if (selected != DialogResult.Cancel)
                {
                    var path = save.FileName;

                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        var contents = activeReader.GetFile(file);
                        fs.Write(contents, 0, contents.Length);
                    }
                }
            }
        }

        private void extractAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractToFolder(activeReader.Files);
        }

        private void extractSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractToFolder(from ListViewItem item in listFiles.SelectedItems select (FileFormat.FileEntry)item.Tag);
        }

        private void ExtractToFolder(IEnumerable<FileFormat.FileEntry> files)
        {
            var browser = new FolderBrowserDialog();
            var result = browser.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                var folder = browser.SelectedPath;

                foreach (var file in files)
                {
                    var fileFolder = folder + "/" + file.StrName.Substring(0, file.StrName.LastIndexOf("/", System.StringComparison.Ordinal));
                    var path = folder + "/" + file.StrName;

                    if (!Directory.Exists(fileFolder))
                        Directory.CreateDirectory(fileFolder + "/");

                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        var contents = activeReader.GetFile(file);
                        fs.Write(contents, 0, contents.Length);
                    }
                }
            }           
        }
    }
}

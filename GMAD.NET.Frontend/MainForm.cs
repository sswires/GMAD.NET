/*
 *
 * Copyright (C) 2013 Stephen Swires
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject
 * to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            toolStripProgress.Visible = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog {DefaultExt = "*.gma", Filter = "Garry's Mod Packages (*.gma)|*.gma"};
            var selected = dialog.ShowDialog();

            if (selected != DialogResult.Cancel)
            {
                activeReader.Parse(dialog.FileName);
                propertyGmad.SelectedObject = activeReader.Header;

                menuFiles.Enabled = true;

                BindFiles();
            }
        }

        protected void BindFiles()
        {
            listFiles.Items.Clear();
            listFiles.Groups.Clear();

            foreach (var file in activeReader.Files)
            {
                var seperatorPos = file.StrName.LastIndexOf("/", System.StringComparison.Ordinal);
                if (seperatorPos <= 0) continue;

                var vdir = file.StrName.Substring(0, seperatorPos);
                var group = listFiles.Groups[vdir];

                if (@group == null)
                {
                    @group = new ListViewGroup(vdir, vdir);
                    listFiles.Groups.Add(@group);
                }

                var item = new ListViewItem(file.StrName.Substring(seperatorPos + 1)) { Tag = file, Group = @group };
                @group.Items.Add(item);
                listFiles.Items.Add(item);
            }          

            listFiles.Sort();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

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

            toolStripProgress.Visible = true;

            if (result != DialogResult.Cancel)
            {
                var folder = browser.SelectedPath;

                toolStripProgress.ProgressBar.Maximum = files.Count();
                toolStripProgress.ProgressBar.Value = 0;

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

                        toolStripProgress.ProgressBar.Value++;
                        toolStripStatusLabel.Text = "Writing file [" + file.StrName + "] " + toolStripProgress.ProgressBar.Value + " of " +
                                                    toolStripProgress.ProgressBar.Maximum + ".";
                    }
                }
            }

            toolStripStatusLabel.Text = "Finished extracting package.";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog() { DefaultExt = "*.gma", Filter = "Garry's Mod Packages (*.gma)|*.gma" };
            var selected = dialog.ShowDialog();

            if (selected != DialogResult.Cancel)
            {
                var writer = new Writer() { Reader = activeReader, Header = (FileFormat.Header)propertyGmad.SelectedObject, Files = (from ListViewItem item in listFiles.Items select (FileFormat.FileEntry)item.Tag).ToList() };
                writer.Write(dialog.FileName);
            }
        }

        protected void AddDirectory(string basedir, string path)
        {
            foreach (var d in Directory.GetDirectories(path + "/"))
            {
                if(basedir != d)
                    AddDirectory(basedir, d); // recursive directory search
            }

            foreach(var f in Directory.GetFiles(path))
            {
                if (Whitelist.Check(f))
                {
                    // do stuff with files
                    var file = new FileFormat.FileEntry();
                    file.LocalFile = true;
                    file.PhysicalPath = f;
                    file.StrName = file.PhysicalPath.Substring(basedir.Length + 1).Replace("\\", "/");
                    file.Size = 0;

                    activeReader.Files.Add(file);
                }
            }
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog();
            var result = browser.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                AddDirectory(browser.SelectedPath, browser.SelectedPath);
                BindFiles();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyGmad.SelectedObject = new FileFormat.Header();
            menuFiles.Enabled = true;

            activeReader.Files.Clear();

            listFiles.Items.Clear();
            listFiles.Groups.Clear();
        }
    }
}

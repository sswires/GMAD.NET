namespace GMAD.NET.Frontend
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.propertyGmad = new System.Windows.Forms.PropertyGrid();
            this.listFiles = new System.Windows.Forms.ListView();
            this.menuFiles = new System.Windows.Forms.MenuStrip();
            this.extractSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.extractAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.menuFiles.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(742, 24);
            this.menuMain.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 24);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.propertyGmad);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.Controls.Add(this.listFiles);
            this.mainContainer.Panel2.Controls.Add(this.menuFiles);
            this.mainContainer.Size = new System.Drawing.Size(742, 520);
            this.mainContainer.SplitterDistance = 247;
            this.mainContainer.TabIndex = 1;
            // 
            // propertyGmad
            // 
            this.propertyGmad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGmad.Location = new System.Drawing.Point(0, 0);
            this.propertyGmad.Name = "propertyGmad";
            this.propertyGmad.Size = new System.Drawing.Size(247, 520);
            this.propertyGmad.TabIndex = 0;
            // 
            // listFiles
            // 
            this.listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFiles.Location = new System.Drawing.Point(0, 24);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(491, 496);
            this.listFiles.TabIndex = 0;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Tile;
            this.listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDoubleClick);
            // 
            // menuFiles
            // 
            this.menuFiles.Enabled = false;
            this.menuFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAllToolStripMenuItem,
            this.extractSelectedToolStripMenuItem});
            this.menuFiles.Location = new System.Drawing.Point(0, 0);
            this.menuFiles.Name = "menuFiles";
            this.menuFiles.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuFiles.Size = new System.Drawing.Size(491, 24);
            this.menuFiles.TabIndex = 1;
            this.menuFiles.Text = "menuStrip1";
            // 
            // extractSelectedToolStripMenuItem
            // 
            this.extractSelectedToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.folder_blue;
            this.extractSelectedToolStripMenuItem.Name = "extractSelectedToolStripMenuItem";
            this.extractSelectedToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.extractSelectedToolStripMenuItem.Text = "Extract Selected";
            this.extractSelectedToolStripMenuItem.Click += new System.EventHandler(this.extractSelectedToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(742, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(625, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "Ready.";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripProgress
            // 
            this.toolStripProgress.Name = "toolStripProgress";
            this.toolStripProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // extractAllToolStripMenuItem
            // 
            this.extractAllToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.folder_green;
            this.extractAllToolStripMenuItem.Name = "extractAllToolStripMenuItem";
            this.extractAllToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.extractAllToolStripMenuItem.Text = "Extract All";
            this.extractAllToolStripMenuItem.Click += new System.EventHandler(this.extractAllToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.document_green;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.folder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.disk;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::GMAD.NET.Frontend.Properties.Resources.door_out;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 566);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "GMAD.NET";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            this.mainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.menuFiles.ResumeLayout(false);
            this.menuFiles.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.PropertyGrid propertyGmad;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.MenuStrip menuFiles;
        private System.Windows.Forms.ToolStripMenuItem extractAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgress;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}


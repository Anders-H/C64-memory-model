namespace MemoryVisualizer
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.setDisassemblyStartAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disassemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousPageToolStripMenuItem,
            this.nextPageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.setDisassemblyStartAddressToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // previousPageToolStripMenuItem
            // 
            this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
            this.previousPageToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.previousPageToolStripMenuItem.Text = "Previous page (Page Up)";
            this.previousPageToolStripMenuItem.Click += new System.EventHandler(this.previousPageToolStripMenuItem_Click);
            // 
            // nextPageToolStripMenuItem
            // 
            this.nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
            this.nextPageToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.nextPageToolStripMenuItem.Text = "Next page (Page Down)";
            this.nextPageToolStripMenuItem.Click += new System.EventHandler(this.nextPageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // setDisassemblyStartAddressToolStripMenuItem
            // 
            this.setDisassemblyStartAddressToolStripMenuItem.Name = "setDisassemblyStartAddressToolStripMenuItem";
            this.setDisassemblyStartAddressToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.setDisassemblyStartAddressToolStripMenuItem.Text = "Set disassembly start address...";
            this.setDisassemblyStartAddressToolStripMenuItem.Click += new System.EventHandler(this.setDisassemblyStartAddressToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawHexToolStripMenuItem,
            this.rawDecToolStripMenuItem,
            this.disassemblyToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // rawHexToolStripMenuItem
            // 
            this.rawHexToolStripMenuItem.Checked = true;
            this.rawHexToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rawHexToolStripMenuItem.Name = "rawHexToolStripMenuItem";
            this.rawHexToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rawHexToolStripMenuItem.Text = "Raw (Hex)";
            this.rawHexToolStripMenuItem.Click += new System.EventHandler(this.rawHexToolStripMenuItem_Click);
            // 
            // rawDecToolStripMenuItem
            // 
            this.rawDecToolStripMenuItem.Name = "rawDecToolStripMenuItem";
            this.rawDecToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rawDecToolStripMenuItem.Text = "Raw (Dec)";
            this.rawDecToolStripMenuItem.Click += new System.EventHandler(this.rawDecToolStripMenuItem_Click);
            // 
            // disassemblyToolStripMenuItem
            // 
            this.disassemblyToolStripMenuItem.Name = "disassemblyToolStripMenuItem";
            this.disassemblyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.disassemblyToolStripMenuItem.Text = "Disassembly";
            this.disassemblyToolStripMenuItem.Click += new System.EventHandler(this.disassemblyToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "MainWindow";
            this.Text = "C64 Memory Visualizer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragOver);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawDecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDisassemblyStartAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disassemblyToolStripMenuItem;
    }
}


namespace Sprdef.Tools.MemoryVisualizer
{
    partial class MemoryVisualizerMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryVisualizerMainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.setDisassemblyStartAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawDecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disassemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.previousByteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextByteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousPageToolStripMenuItem,
            this.nextPageToolStripMenuItem,
            this.toolStripMenuItem2,
            this.previousByteToolStripMenuItem,
            this.nextByteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.homeToolStripMenuItem,
            this.endToolStripMenuItem,
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
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(232, 6);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // endToolStripMenuItem
            // 
            this.endToolStripMenuItem.Name = "endToolStripMenuItem";
            this.endToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.endToolStripMenuItem.Text = "End";
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
            this.disassemblyToolStripMenuItem,
            this.spriteToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // rawHexToolStripMenuItem
            // 
            this.rawHexToolStripMenuItem.Checked = true;
            this.rawHexToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rawHexToolStripMenuItem.Name = "rawHexToolStripMenuItem";
            this.rawHexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rawHexToolStripMenuItem.Text = "Raw (Hex)";
            this.rawHexToolStripMenuItem.Click += new System.EventHandler(this.rawHexToolStripMenuItem_Click);
            // 
            // rawDecToolStripMenuItem
            // 
            this.rawDecToolStripMenuItem.Name = "rawDecToolStripMenuItem";
            this.rawDecToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rawDecToolStripMenuItem.Text = "Raw (Dec)";
            this.rawDecToolStripMenuItem.Click += new System.EventHandler(this.rawDecToolStripMenuItem_Click);
            // 
            // disassemblyToolStripMenuItem
            // 
            this.disassemblyToolStripMenuItem.Name = "disassemblyToolStripMenuItem";
            this.disassemblyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disassemblyToolStripMenuItem.Text = "Disassembly";
            this.disassemblyToolStripMenuItem.Click += new System.EventHandler(this.disassemblyToolStripMenuItem_Click);
            // 
            // spriteToolStripMenuItem
            // 
            this.spriteToolStripMenuItem.Name = "spriteToolStripMenuItem";
            this.spriteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.spriteToolStripMenuItem.Text = "Sprite";
            this.spriteToolStripMenuItem.Click += new System.EventHandler(this.spriteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(232, 6);
            // 
            // previousByteToolStripMenuItem
            // 
            this.previousByteToolStripMenuItem.Name = "previousByteToolStripMenuItem";
            this.previousByteToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.previousByteToolStripMenuItem.Text = "Previous byte (-)";
            this.previousByteToolStripMenuItem.Click += new System.EventHandler(this.previousByteToolStripMenuItem_Click);
            // 
            // nextByteToolStripMenuItem
            // 
            this.nextByteToolStripMenuItem.Name = "nextByteToolStripMenuItem";
            this.nextByteToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.nextByteToolStripMenuItem.Text = "Next byte (+)";
            this.nextByteToolStripMenuItem.Click += new System.EventHandler(this.nextByteToolStripMenuItem_Click);
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
            this.Load += new System.EventHandler(this.MainWindow_Load);
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousByteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextByteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}


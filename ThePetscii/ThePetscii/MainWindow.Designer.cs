namespace ThePetscii
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setQuartercharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panelColors = new System.Windows.Forms.Panel();
            this.panelScreenContainer = new System.Windows.Forms.Panel();
            this.canvas1 = new ThePetscii.CanvasModel.Canvas();
            this.menuStrip1.SuspendLayout();
            this.panelScreenContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.viewToolStripMenuItem, this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(804, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.gridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.ShortcutKeys =
                ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.setQuartercharToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // setQuartercharToolStripMenuItem
            // 
            this.setQuartercharToolStripMenuItem.Checked = true;
            this.setQuartercharToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.setQuartercharToolStripMenuItem.Name = "setQuartercharToolStripMenuItem";
            this.setQuartercharToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.setQuartercharToolStripMenuItem.Text = "Set quarter-char";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panelColors
            // 
            this.panelColors.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (30)))), ((int) (((byte) (30)))),
                ((int) (((byte) (30)))));
            this.panelColors.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelColors.Location = new System.Drawing.Point(0, 49);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(42, 484);
            this.panelColors.TabIndex = 4;
            this.panelColors.Paint += new System.Windows.Forms.PaintEventHandler(this.panelColors_Paint);
            this.panelColors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelColors_MouseClick);
            this.panelColors.Resize += new System.EventHandler(this.panelColors_Resize);
            // 
            // panelScreenContainer
            // 
            this.panelScreenContainer.AutoScroll = true;
            this.panelScreenContainer.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (50)))),
                ((int) (((byte) (50)))), ((int) (((byte) (50)))));
            this.panelScreenContainer.Controls.Add(this.canvas1);
            this.panelScreenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScreenContainer.Location = new System.Drawing.Point(42, 49);
            this.panelScreenContainer.Name = "panelScreenContainer";
            this.panelScreenContainer.Size = new System.Drawing.Size(762, 484);
            this.panelScreenContainer.TabIndex = 5;
            this.panelScreenContainer.Resize += new System.EventHandler(this.PanelScreenContainer_Resize);
            // 
            // canvas1
            // 
            this.canvas1.ExtraLarge = false;
            this.canvas1.GridVisible = true;
            this.canvas1.Location = new System.Drawing.Point(5, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.PetsciiImage = null;
            this.canvas1.Size = new System.Drawing.Size(640, 400);
            this.canvas1.TabIndex = 0;
            this.canvas1.CanvasClick += new ThePetscii.CanvasModel.CanvasClickDelegate(this.Canvas1_CanvasClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 555);
            this.Controls.Add(this.panelScreenContainer);
            this.Controls.Add(this.panelColors);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(820, 594);
            this.Name = "MainWindow";
            this.Text = "The Petscii";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelScreenContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelColors;
        private System.Windows.Forms.Panel panelScreenContainer;
        private ThePetscii.CanvasModel.Canvas canvas1;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setQuartercharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    }
}


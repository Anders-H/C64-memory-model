﻿namespace Sprdef
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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToBASICToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCBMPrgStudioDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprite8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multicolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.pickBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickForegroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickExtraColor1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickExtraColor2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.spritesToolStripMenuItem,
            this.paletteToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(717, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToBASICToolStripMenuItem,
            this.exportToCBMPrgStudioDataToolStripMenuItem,
            this.exportPNGToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveAsToolStripMenuItem.Text = "Save as..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportToBASICToolStripMenuItem
            // 
            this.exportToBASICToolStripMenuItem.Name = "exportToBASICToolStripMenuItem";
            this.exportToBASICToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportToBASICToolStripMenuItem.Text = "Export to BASIC...";
            this.exportToBASICToolStripMenuItem.Click += new System.EventHandler(this.exportToBASICToolStripMenuItem_Click);
            // 
            // exportToCBMPrgStudioDataToolStripMenuItem
            // 
            this.exportToCBMPrgStudioDataToolStripMenuItem.Name = "exportToCBMPrgStudioDataToolStripMenuItem";
            this.exportToCBMPrgStudioDataToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportToCBMPrgStudioDataToolStripMenuItem.Text = "Export to CBM prg Studio data...";
            this.exportToCBMPrgStudioDataToolStripMenuItem.Click += new System.EventHandler(this.exportToCBMPrgStudioDataToolStripMenuItem_Click);
            // 
            // exportPNGToolStripMenuItem
            // 
            this.exportPNGToolStripMenuItem.Name = "exportPNGToolStripMenuItem";
            this.exportPNGToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportPNGToolStripMenuItem.Text = "Export PNG...";
            this.exportPNGToolStripMenuItem.Click += new System.EventHandler(this.exportPNGToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::Sprdef.Properties.Resources._112_ArrowReturnLeft_Blue_16x16_72;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::Sprdef.Properties.Resources._112_ArrowReturnRight_Blue_16x16_72;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // spritesToolStripMenuItem
            // 
            this.spritesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sprite1ToolStripMenuItem,
            this.sprite2ToolStripMenuItem,
            this.sprite3ToolStripMenuItem,
            this.sprite4ToolStripMenuItem,
            this.sprite5ToolStripMenuItem,
            this.sprite6ToolStripMenuItem,
            this.sprite7ToolStripMenuItem,
            this.sprite8ToolStripMenuItem});
            this.spritesToolStripMenuItem.Name = "spritesToolStripMenuItem";
            this.spritesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.spritesToolStripMenuItem.Text = "Sprite 1/8";
            // 
            // sprite1ToolStripMenuItem
            // 
            this.sprite1ToolStripMenuItem.Checked = true;
            this.sprite1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sprite1ToolStripMenuItem.Name = "sprite1ToolStripMenuItem";
            this.sprite1ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.sprite1ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite1ToolStripMenuItem.Text = "Sprite 1";
            // 
            // sprite2ToolStripMenuItem
            // 
            this.sprite2ToolStripMenuItem.Name = "sprite2ToolStripMenuItem";
            this.sprite2ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.sprite2ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite2ToolStripMenuItem.Text = "Sprite 2";
            // 
            // sprite3ToolStripMenuItem
            // 
            this.sprite3ToolStripMenuItem.Name = "sprite3ToolStripMenuItem";
            this.sprite3ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.sprite3ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite3ToolStripMenuItem.Text = "Sprite 3";
            // 
            // sprite4ToolStripMenuItem
            // 
            this.sprite4ToolStripMenuItem.Name = "sprite4ToolStripMenuItem";
            this.sprite4ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.sprite4ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite4ToolStripMenuItem.Text = "Sprite 4";
            // 
            // sprite5ToolStripMenuItem
            // 
            this.sprite5ToolStripMenuItem.Name = "sprite5ToolStripMenuItem";
            this.sprite5ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.sprite5ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite5ToolStripMenuItem.Text = "Sprite 5";
            // 
            // sprite6ToolStripMenuItem
            // 
            this.sprite6ToolStripMenuItem.Name = "sprite6ToolStripMenuItem";
            this.sprite6ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.sprite6ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite6ToolStripMenuItem.Text = "Sprite 6";
            // 
            // sprite7ToolStripMenuItem
            // 
            this.sprite7ToolStripMenuItem.Name = "sprite7ToolStripMenuItem";
            this.sprite7ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
            this.sprite7ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite7ToolStripMenuItem.Text = "Sprite 7";
            // 
            // sprite8ToolStripMenuItem
            // 
            this.sprite8ToolStripMenuItem.Name = "sprite8ToolStripMenuItem";
            this.sprite8ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
            this.sprite8ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sprite8ToolStripMenuItem.Text = "Sprite 8";
            // 
            // paletteToolStripMenuItem
            // 
            this.paletteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multicolorToolStripMenuItem,
            this.toolStripMenuItem1,
            this.pickBackgroundColorToolStripMenuItem,
            this.pickForegroundColorToolStripMenuItem,
            this.pickExtraColor1ToolStripMenuItem,
            this.pickExtraColor2ToolStripMenuItem});
            this.paletteToolStripMenuItem.Name = "paletteToolStripMenuItem";
            this.paletteToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.paletteToolStripMenuItem.Text = "Palette";
            // 
            // multicolorToolStripMenuItem
            // 
            this.multicolorToolStripMenuItem.Name = "multicolorToolStripMenuItem";
            this.multicolorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.multicolorToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.multicolorToolStripMenuItem.Text = "Multicolor";
            this.multicolorToolStripMenuItem.Click += new System.EventHandler(this.multicolorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // pickBackgroundColorToolStripMenuItem
            // 
            this.pickBackgroundColorToolStripMenuItem.Name = "pickBackgroundColorToolStripMenuItem";
            this.pickBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pickBackgroundColorToolStripMenuItem.Text = "Pick background color...";
            this.pickBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.pickBackgroundColorToolStripMenuItem_Click);
            // 
            // pickForegroundColorToolStripMenuItem
            // 
            this.pickForegroundColorToolStripMenuItem.Name = "pickForegroundColorToolStripMenuItem";
            this.pickForegroundColorToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pickForegroundColorToolStripMenuItem.Text = "Pick foreground color...";
            this.pickForegroundColorToolStripMenuItem.Click += new System.EventHandler(this.pickForegroundColorToolStripMenuItem_Click);
            // 
            // pickExtraColor1ToolStripMenuItem
            // 
            this.pickExtraColor1ToolStripMenuItem.Name = "pickExtraColor1ToolStripMenuItem";
            this.pickExtraColor1ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pickExtraColor1ToolStripMenuItem.Text = "Pick extra color 1...";
            this.pickExtraColor1ToolStripMenuItem.Click += new System.EventHandler(this.pickExtraColor1ToolStripMenuItem_Click);
            // 
            // pickExtraColor2ToolStripMenuItem
            // 
            this.pickExtraColor2ToolStripMenuItem.Name = "pickExtraColor2ToolStripMenuItem";
            this.pickExtraColor2ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pickExtraColor2ToolStripMenuItem.Text = "Pick extra color 2...";
            this.pickExtraColor2ToolStripMenuItem.Click += new System.EventHandler(this.pickExtraColor2ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.helpToolStripMenuItem1.Text = "Help...";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 543);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(717, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 17);
            this.lblStatus.Text = "   ";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(717, 565);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "SPRDEF";
            this.Activated += new System.EventHandler(this.MainWindow_Activated);
            this.Deactivate += new System.EventHandler(this.MainWindow_Deactivate);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem spritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprite8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToBASICToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCBMPrgStudioDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem paletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickBackgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickForegroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickExtraColor1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickExtraColor2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multicolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    }
}


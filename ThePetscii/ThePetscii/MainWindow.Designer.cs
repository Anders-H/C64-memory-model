﻿namespace ThePetscii
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panelColors = new System.Windows.Forms.Panel();
            this.panelScreenContainer = new System.Windows.Forms.Panel();
            this.canvas1 = new ThePetscii.Canvas();
            this.panelScreenContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(689, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(689, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 459);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(689, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panelColors
            // 
            this.panelColors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelColors.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelColors.Location = new System.Drawing.Point(0, 49);
            this.panelColors.Name = "panelColors";
            this.panelColors.Size = new System.Drawing.Size(36, 410);
            this.panelColors.TabIndex = 4;
            // 
            // panelScreenContainer
            // 
            this.panelScreenContainer.AutoScroll = true;
            this.panelScreenContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panelScreenContainer.Controls.Add(this.canvas1);
            this.panelScreenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScreenContainer.Location = new System.Drawing.Point(36, 49);
            this.panelScreenContainer.Name = "panelScreenContainer";
            this.panelScreenContainer.Size = new System.Drawing.Size(653, 410);
            this.panelScreenContainer.TabIndex = 5;
            this.panelScreenContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelScreenContainer_Paint);
            this.panelScreenContainer.Resize += new System.EventHandler(this.PanelScreenContainer_Resize);
            // 
            // canvas1
            // 
            this.canvas1.Location = new System.Drawing.Point(4, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.Size = new System.Drawing.Size(640, 400);
            this.canvas1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 481);
            this.Controls.Add(this.panelScreenContainer);
            this.Controls.Add(this.panelColors);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(705, 520);
            this.Name = "MainWindow";
            this.Text = "The Petscii";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.panelScreenContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Canvas canvas1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelColors;
        private System.Windows.Forms.Panel panelScreenContainer;
    }
}


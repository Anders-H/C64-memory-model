namespace Sprdef
{
    partial class ExportPngDialog
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
            this.chkDoubleWidth = new System.Windows.Forms.CheckBox();
            this.chkTransparent = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkDoubleWidth
            // 
            this.chkDoubleWidth.AutoSize = true;
            this.chkDoubleWidth.Checked = true;
            this.chkDoubleWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoubleWidth.Location = new System.Drawing.Point(12, 12);
            this.chkDoubleWidth.Name = "chkDoubleWidth";
            this.chkDoubleWidth.Size = new System.Drawing.Size(88, 17);
            this.chkDoubleWidth.TabIndex = 0;
            this.chkDoubleWidth.Text = "Double width";
            this.chkDoubleWidth.UseVisualStyleBackColor = true;
            // 
            // chkTransparent
            // 
            this.chkTransparent.AutoSize = true;
            this.chkTransparent.Checked = true;
            this.chkTransparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTransparent.Location = new System.Drawing.Point(12, 32);
            this.chkTransparent.Name = "chkTransparent";
            this.chkTransparent.Size = new System.Drawing.Size(143, 17);
            this.chkTransparent.TabIndex = 1;
            this.chkTransparent.Text = "Transparent background";
            this.chkTransparent.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(36, 68);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(116, 68);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ExportPngDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(202, 103);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkTransparent);
            this.Controls.Add(this.chkDoubleWidth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportPngDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export PNG";
            this.Load += new System.EventHandler(this.ExportPngDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDoubleWidth;
        private System.Windows.Forms.CheckBox chkTransparent;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
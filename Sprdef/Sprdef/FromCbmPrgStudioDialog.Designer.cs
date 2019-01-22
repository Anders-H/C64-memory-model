namespace Sprdef
{
    partial class FromCbmPrgStudioDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtCleanDataOutput = new System.Windows.Forms.TextBox();
            this.lblCleanDataOutput = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // txtInput
            // 
            this.txtInput.AcceptsReturn = true;
            this.txtInput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(8, 24);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(424, 156);
            this.txtInput.TabIndex = 7;
            this.txtInput.WordWrap = false;
            // 
            // txtCleanDataOutput
            // 
            this.txtCleanDataOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCleanDataOutput.Location = new System.Drawing.Point(8, 200);
            this.txtCleanDataOutput.Multiline = true;
            this.txtCleanDataOutput.Name = "txtCleanDataOutput";
            this.txtCleanDataOutput.ReadOnly = true;
            this.txtCleanDataOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCleanDataOutput.Size = new System.Drawing.Size(320, 156);
            this.txtCleanDataOutput.TabIndex = 9;
            // 
            // lblCleanDataOutput
            // 
            this.lblCleanDataOutput.AutoSize = true;
            this.lblCleanDataOutput.Location = new System.Drawing.Point(8, 184);
            this.lblCleanDataOutput.Name = "lblCleanDataOutput";
            this.lblCleanDataOutput.Size = new System.Drawing.Size(61, 13);
            this.lblCleanDataOutput.TabIndex = 8;
            this.lblCleanDataOutput.Text = "Clean data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Preview:";
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(332, 200);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(100, 156);
            this.picPreview.TabIndex = 11;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(196, 364);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 12;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(276, 364);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(356, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FromCbmPrgStudioDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(440, 394);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCleanDataOutput);
            this.Controls.Add(this.lblCleanDataOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FromCbmPrgStudioDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copy från  CBM prg Studio data";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtCleanDataOutput;
        private System.Windows.Forms.Label lblCleanDataOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
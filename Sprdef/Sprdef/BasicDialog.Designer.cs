namespace Sprdef
{
    partial class BasicDialog
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numLineNum = new System.Windows.Forms.NumericUpDown();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.chkCompact = new System.Windows.Forms.CheckBox();
            this.chkSeparators = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numLineNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(4, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(384, 340);
            this.textBox1.TabIndex = 6;
            this.textBox1.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line number:";
            // 
            // numLineNum
            // 
            this.numLineNum.Location = new System.Drawing.Point(72, 6);
            this.numLineNum.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.numLineNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLineNum.Name = "numLineNum";
            this.numLineNum.Size = new System.Drawing.Size(60, 20);
            this.numLineNum.TabIndex = 1;
            this.numLineNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLineNum.ValueChanged += new System.EventHandler(this.numLineNum_ValueChanged);
            // 
            // numStep
            // 
            this.numStep.Location = new System.Drawing.Point(172, 6);
            this.numStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(60, 20);
            this.numStep.TabIndex = 3;
            this.numStep.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numStep.ValueChanged += new System.EventHandler(this.numStep_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Step:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(312, 372);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(232, 372);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // chkCompact
            // 
            this.chkCompact.AutoSize = true;
            this.chkCompact.Location = new System.Drawing.Point(240, 8);
            this.chkCompact.Name = "chkCompact";
            this.chkCompact.Size = new System.Drawing.Size(68, 17);
            this.chkCompact.TabIndex = 4;
            this.chkCompact.Text = "Compact";
            this.chkCompact.UseVisualStyleBackColor = true;
            this.chkCompact.CheckedChanged += new System.EventHandler(this.chkCompact_CheckedChanged);
            // 
            // chkSeparators
            // 
            this.chkSeparators.AutoSize = true;
            this.chkSeparators.Checked = true;
            this.chkSeparators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeparators.Location = new System.Drawing.Point(312, 8);
            this.chkSeparators.Name = "chkSeparators";
            this.chkSeparators.Size = new System.Drawing.Size(77, 17);
            this.chkSeparators.TabIndex = 5;
            this.chkSeparators.Text = "Separators";
            this.chkSeparators.UseVisualStyleBackColor = true;
            this.chkSeparators.CheckedChanged += new System.EventHandler(this.chkSeparators_CheckedChanged);
            // 
            // BasicDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 400);
            this.Controls.Add(this.chkSeparators);
            this.Controls.Add(this.chkCompact);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.numStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numLineNum);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export to BASIC";
            this.Load += new System.EventHandler(this.BasicDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numLineNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLineNum;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox chkCompact;
        private System.Windows.Forms.CheckBox chkSeparators;
    }
}
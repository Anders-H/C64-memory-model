namespace Sprdef.Dialogs
{
    partial class OpenMemoryVisualizerDialog
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
            this.radioInitialized = new System.Windows.Forms.RadioButton();
            this.radioEmpty = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioInitialized
            // 
            this.radioInitialized.AutoSize = true;
            this.radioInitialized.Checked = true;
            this.radioInitialized.Location = new System.Drawing.Point(16, 16);
            this.radioInitialized.Name = "radioInitialized";
            this.radioInitialized.Size = new System.Drawing.Size(199, 17);
            this.radioInitialized.TabIndex = 0;
            this.radioInitialized.TabStop = true;
            this.radioInitialized.Text = "Initialize visualizer with current sprites";
            this.radioInitialized.UseVisualStyleBackColor = true;
            // 
            // radioEmpty
            // 
            this.radioEmpty.AutoSize = true;
            this.radioEmpty.Location = new System.Drawing.Point(16, 40);
            this.radioEmpty.Name = "radioEmpty";
            this.radioEmpty.Size = new System.Drawing.Size(143, 17);
            this.radioEmpty.TabIndex = 1;
            this.radioEmpty.Text = "Open an empty visualizer";
            this.radioEmpty.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(132, 76);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OpenMemoryVisualizerDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(310, 111);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.radioEmpty);
            this.Controls.Add(this.radioInitialized);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenMemoryVisualizerDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open memory visualizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioInitialized;
        private System.Windows.Forms.RadioButton radioEmpty;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
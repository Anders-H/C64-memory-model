using System;
using System.Windows.Forms;

namespace ThePetscii
{
    public partial class ExportToBasicDialog : Form
    {
        public string Code { get; set; }
        
        public ExportToBasicDialog()
        {
            InitializeComponent();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Code);
            MessageBox.Show(@"The code is copied to clipboard.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportToBasicDialog_Load(object sender, EventArgs e)
        {
            txtSource.Text = Code;
            txtSource.SelectionStart = 0;
            txtSource.SelectionLength = 0;
        }
    }
}
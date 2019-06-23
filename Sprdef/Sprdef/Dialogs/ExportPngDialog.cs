using System;
using System.Windows.Forms;

namespace Sprdef.Dialogs
{
    public partial class ExportPngDialog : Form
    {
        private static bool SuggestDoubleWidth { get; set; } = true;
        private static bool SuggestTransparentBackground { get; set; } = true;
        public bool Multicolor { get; set; }

        public bool DoubleWidth =>
            chkDoubleWidth.Checked;

        public bool TransparentBackground =>
            chkTransparent.Checked;

        public ExportPngDialog()
        {
            InitializeComponent();
        }

        private void ExportPngDialog_Load(object sender, EventArgs e)
        {
            chkDoubleWidth.Enabled = Multicolor;
            chkDoubleWidth.Checked = SuggestDoubleWidth;
            chkTransparent.Checked = SuggestTransparentBackground;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SuggestDoubleWidth = chkDoubleWidth.Checked;
            SuggestTransparentBackground = chkTransparent.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}
using System;
using System.Windows.Forms;

namespace Sprdef
{
    public partial class ExportPngDialog : Form
    {
        public bool Multicolor { get; set; }
        public ExportPngDialog()
        {
            InitializeComponent();
        }
        private void ExportPngDialog_Load(object sender, EventArgs e)
        {
            chkDoubleWidth.Enabled = Multicolor;
        }
    }
}

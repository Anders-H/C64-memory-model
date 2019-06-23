using System;
using System.Windows.Forms;

namespace Sprdef.Dialogs
{
    public partial class OpenMemoryVisualizerDialog : Form
    {
        public OpenMemoryVisualizerDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e) =>
            DialogResult = DialogResult.OK;

        public bool OpenEmpty =>
            radioEmpty.Checked;

        public bool OpenInitialized =>
            radioInitialized.Checked;
    }
}
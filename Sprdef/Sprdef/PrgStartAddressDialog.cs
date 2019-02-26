using System;
using System.Globalization;
using System.Windows.Forms;

namespace Sprdef
{
    public partial class PrgStartAddressDialog : Form
    {
        public int StartAddress { get; set; }

        public PrgStartAddressDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            void InvalidAddress() =>
                MessageBox.Show($@"Invalid start address. Must be integer (0 to {ushort.MaxValue}).", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!int.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var startAddress))
            {
                InvalidAddress();
                return;
            }
            if (startAddress < 0 || startAddress > ushort.MaxValue)
            {
                InvalidAddress();
                return;
            }
            StartAddress = startAddress;
            DialogResult = DialogResult.OK;
        }

        private void DialogDisassemblyStartAddress_Load(object sender, EventArgs e) =>
            textBox1.Text = StartAddress.ToString(CultureInfo.InvariantCulture);
    }
}
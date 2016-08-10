using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryVisualizer
{
    public partial class DialogDisassemblyStartAddress : Form
    {
        public int StartAddress { get; set; }
        public DialogDisassemblyStartAddress()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Action invalidAddress = () => MessageBox.Show($@"Invalid start address. Must be integer (0 to {ushort.MaxValue}).",Text,MessageBoxButtons.OK, MessageBoxIcon.Information);
            int startAddress;
            if (!int.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out startAddress))
            {
                invalidAddress();
                return;
            }
            if (startAddress < 0 || startAddress > ushort.MaxValue)
            {
                invalidAddress();
                return;
            }
            StartAddress = startAddress;
            DialogResult = DialogResult.OK;
        }
        private void DialogDisassemblyStartAddress_Load(object sender, EventArgs e) => textBox1.Text = StartAddress.ToString(CultureInfo.InvariantCulture);
    }
}

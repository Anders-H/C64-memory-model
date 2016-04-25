using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sprdef
{
    public partial class CbmPrgStudioDialog : Form
    {
        public CbmPrgStudioDialog()
        {
            InitializeComponent();
        }

        public C64Sprite[] Sprites { get; set; }

        private void CbmPrgStudioDialog_Load(object sender, EventArgs e)
        {
            cboDecHex.Items.Add("Hex");
            cboDecHex.Items.Add("Dec");
            cboDecHex.SelectedIndex = 0;
        }

        private void cboDecHex_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = new StringBuilder();
            var hex = cboDecHex.SelectedIndex == 0;
            for (var i = 0; i < 8; i++)
            {
                if (i > 0)
                    s.AppendLine();
                s.AppendLine($"; Sprite {i + 1}");
                var bytes = Sprites[i].GetBytes();
                for (var b = 0; b < 63; b++)
                {
                    switch (b % 3)
                    {
                        case 0:
                            s.AppendLine(hex
                                ? $" BYTE ${bytes[b]:X2}, ${bytes[b + 1]:X2}, ${bytes[b + 2]:X2}"
                                : $" BYTE {bytes[b]}, {bytes[b + 1]}, {bytes[b + 2]}");
                            break;
                    }
                }
            }
            textBox1.Text = s.ToString();
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
        }

        private void btnCopy_Click(object sender, EventArgs e) => Clipboard.SetText(textBox1.Text);
        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}

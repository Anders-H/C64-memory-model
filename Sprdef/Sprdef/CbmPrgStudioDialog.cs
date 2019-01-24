using System;
using System.Text;
using System.Windows.Forms;

namespace Sprdef
{
    public partial class CbmPrgStudioDialog : Form
    {
        public SpriteArray Sprites { get; set; }

        public CbmPrgStudioDialog()
        {
            InitializeComponent();
        }

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
                s.Append(" BYTE");
                for (var b = 0; b < 40; b++)
                {
                    if (hex)
                        s.Append($" ${bytes[b]:X2}{(b < 39 ? "," : "")}");
                    else
                        s.Append($" {bytes[b]}{(b < 39 ? "," : "")}");
                }
                s.AppendLine();
                s.Append(" BYTE");
                for (var b = 40; b < 63; b++)
                    s.Append(hex ? $" ${bytes[b]:X2}{(b < 62 ? "," : "")}" : $" {bytes[b]}{(b < 62 ? "," : "")}");
            }
            textBox1.Text = s.ToString();
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
        }

        private void btnCopy_Click(object sender, EventArgs e) =>
            Clipboard.SetText(textBox1.Text);

        private void btnClose_Click(object sender, EventArgs e) =>
            Close();
    }
}
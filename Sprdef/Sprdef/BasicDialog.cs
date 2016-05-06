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
    public partial class BasicDialog : Form
    {
        public BasicDialog()
        {
            InitializeComponent();
        }

        public C64Sprite[] Sprites { get; set; }

        private void BasicDialog_Load(object sender, EventArgs e) => RefreshList();

        private void numLineNum_ValueChanged(object sender, EventArgs e) => RefreshList();

        private void numStep_ValueChanged(object sender, EventArgs e) => RefreshList();

        private void RefreshList()
        {
            var lineNumber = (int)numLineNum.Value;
            var step = (int)numStep.Value;
            var s = new StringBuilder();
            for (var i = 0; i < 8; i++)
            {
                if (i > 0)
                    s.AppendLine();
                s.AppendLine($"{lineNumber} REM SPRITE {i + 1}");
                lineNumber += step;
                var p = 0;
                var data = Sprites[i].GetBytes();
                for (var x = 0; x < 10; x++)
                {
                    s.AppendLine($"{lineNumber} DATA {data[p]}, {data[p + 1]}, {data[p + 2]}, {data[p + 3]}, {data[p + 4]}, {data[p + 5]}");
                    lineNumber += step;
                    p += 6;
                }
                s.AppendLine($"{lineNumber} DATA {data[p]}, {data[p + 1]}, {data[p + 2]}");
                lineNumber += step;
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

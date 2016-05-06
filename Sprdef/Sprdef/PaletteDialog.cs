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
    public partial class PaletteDialog : Form
    {
        public PaletteDialog()
        {
            InitializeComponent();
        }

        public string Prompt { private get; set; }
        public int ColorIndex { get; set; }
        private Label[] Labels { get; set; }
        private void PaletteDialog_Load(object sender, EventArgs e)
        {
            lblPrompt.Text = Prompt ?? "";
            Labels = new Label[16];
            Labels[0] = lblCol0;
            Labels[1] = lblCol1;
            Labels[2] = lblCol2;
            Labels[3] = lblCol3;
            Labels[4] = lblCol4;
            Labels[5] = lblCol5;
            Labels[6] = lblCol6;
            Labels[7] = lblCol7;
            Labels[8] = lblCol8;
            Labels[9] = lblCol9;
            Labels[10] = lblCol10;
            Labels[11] = lblCol11;
            Labels[12] = lblCol12;
            Labels[13] = lblCol13;
            Labels[14] = lblCol14;
            Labels[15] = lblCol15;
            for (var i = 0; i < 16; i++)
            {
                Labels[i].Click += lblCol_Click;
                Labels[i].BackColor = C64Sprite.Palette.GetColor(i);
            }
        }

        private void PaletteDialog_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.DrawRectangle(Pens.Black, Labels[ColorIndex].Left - 2, Labels[ColorIndex].Top - 2, Labels[ColorIndex].Width + 3, Labels[ColorIndex].Height + 3);
        }

        private void lblCol_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 16; i++)
                if (sender == Labels[i])
                {
                    ColorIndex = i;
                    break;
                }
            Invalidate();
        }

        private void btnOK_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;
    }
}

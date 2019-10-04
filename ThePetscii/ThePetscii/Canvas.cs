using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThePetscii
{
    public partial class Canvas : UserControl
    {
        public Canvas()
        {
            InitializeComponent();
        }

        private void Canvas_Resize(object sender, EventArgs e)
        {
            Width = 640;
            Height = 400;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            using (var darkPen = new Pen(Color.FromArgb(30, 30, 30)))
            {
                var xpos = 0;
                var ypos = 0;
                for (var y = 0; y < 400; y += 16)
                {
                    for (var x = 0; x < 640; x += 16)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, x, y, 16, 16);
                        xpos += 1;
                    }

                    xpos = 0;
                    ypos += 1;
                }
            }
        }
    }
}

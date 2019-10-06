using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThePetscii
{
    public partial class Canvas : UserControl
    {
        private bool _gridVisible = true;
        public PetsciiImage PetsciiImage { get; set; }
        
        public Canvas()
        {
            InitializeComponent();
        }

        public bool GridVisible
        {
            get => _gridVisible;
            set
            {
                _gridVisible = value;
                Invalidate();
            }
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
            var totalWidth = _gridVisible ? 15 : 16;
            var xpos = 0;
            var ypos = 0;
            for (var y = 0; y < 25; y++)
            {
                for (var x = 0; x < 40; x++)
                {
                    e.Graphics.FillRectangle(
                        PetsciiImage.Background.GetBrush(
                            PetsciiImage.Background.Colors[x, y]
                            ), xpos, ypos, totalWidth, totalWidth);
                    xpos += 16;
                }
                xpos = 0;
                ypos += 16;
            }
        }
    }
}

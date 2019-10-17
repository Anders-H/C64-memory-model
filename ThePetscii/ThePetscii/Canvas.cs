using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThePetscii
{
    public partial class Canvas : UserControl
    {
        private bool _gridVisible = true;
        private bool _extraLarge;
        private int _totalWidth;
        private int _totalHeight;
        private int _characterSize;
        
        public PetsciiImage PetsciiImage { get; set; }
        
        public Canvas()
        {
            InitializeComponent();
            ExtraLarge = false;
        }

        public bool ExtraLarge
        {
            get => _extraLarge;
            set
            {
                _extraLarge = value;
                if (_extraLarge)
                {
                    _totalWidth = 960;
                    _totalHeight = 600;
                    _characterSize = 24;
                }
                else
                {
                    _totalWidth = 640;
                    _totalHeight = 400;
                    _characterSize = 16;
                }
                Canvas_Resize(null, null);
                Invalidate();
            }
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
            Width = _totalWidth;
            Height = _totalHeight;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
                return;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            var totalWidth = _gridVisible ? _characterSize - 1 : _characterSize;
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
                    xpos += _characterSize;
                }
                xpos = 0;
                ypos += _characterSize;
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}

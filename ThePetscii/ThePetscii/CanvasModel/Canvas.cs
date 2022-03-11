#nullable enable

using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using C64MemoryModel.Graphics;

namespace ThePetscii.CanvasModel
{
    public partial class Canvas : UserControl
    {
        private bool _gridVisible = true;
        private bool _extraLarge;
        private int _totalWidth;
        private int _totalHeight;
        private int _characterSize;
        public event CanvasClickDelegate? CanvasClick;
        
        public PetsciiImage? PetsciiImage { get; set; }
        
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
                Canvas_Resize(this, EventArgs.Empty);
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
            var halfWidth = totalWidth / 2;
            var xPos = 0;
            var yPos = 0;

            for (var y = 0; y < 25; y++)
            {
                for (var x = 0; x < 40; x++)
                {
                    if (PetsciiImage!.IsAllSet(x, y))
                    {
                        e.Graphics.FillRectangle(
                            PetsciiImage.Foreground.GetBrush(
                                PetsciiImage.Foreground.Colors[x, y]
                            ), xPos, yPos, totalWidth, totalWidth);
                    }
                    else if (PetsciiImage.IsNoneSet(x, y))
                    {
                        e.Graphics.FillRectangle( //Background...
                            PetsciiImage.Foreground.GetBrush(
                                C64Color.Blue
                            ), xPos, yPos, totalWidth, totalWidth);
                    }
                    else
                    {
                        e.Graphics.FillRectangle( //Background...
                            PetsciiImage.Foreground.GetBrush(
                                C64Color.Blue
                            ), xPos, yPos, totalWidth, totalWidth);

                        var c = PetsciiImage.GetChar(x, y);
                        
                        if (c.IsSetAt(0, 0))
                            e.Graphics.FillRectangle(
                                PetsciiImage.Foreground.GetBrush(
                                    PetsciiImage.Foreground.Colors[x, y]
                                ), xPos, yPos, halfWidth + 1, halfWidth + 1);
                        
                        if (c.IsSetAt(1, 0))
                            e.Graphics.FillRectangle(
                                PetsciiImage.Foreground.GetBrush(
                                    PetsciiImage.Foreground.Colors[x, y]
                                ), xPos + halfWidth + 1, yPos, halfWidth, halfWidth + 1);
                        
                        if (c.IsSetAt(0, 1))
                            e.Graphics.FillRectangle(
                                PetsciiImage.Foreground.GetBrush(
                                    PetsciiImage.Foreground.Colors[x, y]
                                ), xPos, yPos + halfWidth + 1, halfWidth + 1, halfWidth);
                        
                        if (c.IsSetAt(1, 1))
                            e.Graphics.FillRectangle(
                                PetsciiImage.Foreground.GetBrush(
                                    PetsciiImage.Foreground.Colors[x, y]
                                ), xPos + halfWidth + 1, yPos + halfWidth + 1, halfWidth, halfWidth);
                    }

                    xPos += _characterSize;
                }

                xPos = 0;
                yPos += _characterSize;
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) =>
            CanvasClick!.Invoke(this, new CanvasClickEventArgs(ExtraLarge ? 24 : 16, e.X, e.Y));
    }
}
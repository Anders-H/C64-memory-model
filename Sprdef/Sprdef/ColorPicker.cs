using System;
using System.Drawing;

namespace Sprdef
{
    public class ColorPicker
    {
        public class ColorCell : IScreenThing
        {
            public static int Size { get; set; }
            public Color Color { get; set; }
            public Rectangle Bounds { get; private set; }
            public ColorCell(Color color) { Bounds = new Rectangle(); Size = 20; Color = color; }
            public void Draw(Graphics g, int x, int y, bool enabled, bool selected)
            {
                Bounds = new Rectangle(x, y, Size, Size);
                using (var b = new SolidBrush(Color))
                    g.FillRectangle(b, Bounds);
                if (enabled && selected)
                {
                    g.DrawRectangle(Pens.Blue, Bounds);
                    g.DrawRectangle(Pens.Blue, x + 1, y + 1, Size - 2, Size - 2);
                    g.DrawRectangle(Pens.Black, x + 2, y + 2, Size - 4, Size - 4);
                    g.DrawRectangle(Pens.White, x + 3, y + 3, Size - 6, Size - 6);
                }
                else if (enabled)
                {
                    g.DrawRectangle(Pens.Black, Bounds);
                    g.DrawRectangle(Pens.White, x + 1, y + 1, Size - 2, Size - 2);
                }
                else
                {
                    g.DrawRectangle(Pens.DimGray, Bounds);
                    g.DrawRectangle(Pens.DimGray, x + 1, y + 1, Size - 2, Size - 2);
                    g.DrawLine(Pens.DimGray, x, y, x + Size, y + Size);
                    g.DrawLine(Pens.DimGray, x, y + Size, x + Size, y);
                }
            }

            public bool HitTest(int x, int y) => Bounds.IntersectsWith(new Rectangle(x, y, 1, 1));
        }

        private readonly ColorCell[] _colorCells = new ColorCell[4];
        public int SelectedColor { get; set; } = 1;

        public Rectangle Bounds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ColorPicker(Color col1, Color col2, Color col3, Color col4)
        {
            _colorCells[0] = new ColorCell(col1);
            _colorCells[1] = new ColorCell(col2);
            _colorCells[2] = new ColorCell(col3);
            _colorCells[3] = new ColorCell(col4);
        }

        public ColorCell GetColorCell(int index) => _colorCells[index];

        public void Draw(Graphics g, int x, int y, bool multiColor)
        {
            for (int i = 0; i < 4; i++)
                GetColorCell(i).Draw(g, x + (i * (ColorCell.Size + 8)), y, multiColor | (i < 2), i == SelectedColor);
        }

        public ColorCell HitTest(int x, int y)
        {
            if (_colorCells[0].HitTest(x, y))
                return _colorCells[0];
            if (_colorCells[1].HitTest(x, y))
                return _colorCells[1];
            if (_colorCells[2].HitTest(x, y))
                return _colorCells[2];
            if (_colorCells[3].HitTest(x, y))
                return _colorCells[3];
            return null;
        }
    }
}

using System;
using System.Drawing;

namespace Sprdef
{
    public class ColorPicker
    {
        public class ColorCell : IScreenThing
        {
            public static int Size { get; set; }
            public Rectangle Bounds { get; private set; }
            public ColorCell() { Bounds = new Rectangle(); Size = 20; }
            public void Draw(Graphics g, int x, int y, int colorIndex, bool enabled, bool selected)
            {
                Bounds = new Rectangle(x, y, Size, Size);
                var paletteIndex = C64Sprite.BackgroundColorIndex;
                switch (colorIndex)
                {
                    case 1: paletteIndex = C64Sprite.ForegroundColorIndex; break;
                    case 2: paletteIndex = C64Sprite.ExtraColor1Index; break;
                    case 3: paletteIndex = C64Sprite.ExtraColor2Index; break;
                }
                var color = C64Sprite.Palette.GetColor(paletteIndex);
                using (var b = new SolidBrush(color))
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
        public ColorPicker()
        {
            _colorCells[0] = new ColorCell();
            _colorCells[1] = new ColorCell();
            _colorCells[2] = new ColorCell();
            _colorCells[3] = new ColorCell();
        }
        public ColorCell GetColorCell(int index) => _colorCells[index];
        public void Draw(Graphics g, int x, int y, bool multiColor)
        {
            for (var i = 0; i < 4; i++)
                GetColorCell(i).Draw(g, x + (i * (ColorCell.Size + 8)), y, i, multiColor | (i < 2), i == SelectedColor);
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

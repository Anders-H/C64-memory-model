using System.Drawing;
using Sprdef.Model;

namespace Sprdef.Rendering
{
    public class ColorPickerCell : IScreenThing
    {
        public static int Size { get; set; }
        public Rectangle Bounds { get; private set; }

        public ColorPickerCell()
        {
            Bounds = new Rectangle();
            Size = 20;
        }

        public void Draw(Graphics g, C64Sprite sprite, int x, int y, int colorIndex, bool enabled, bool selected)
        {
            Bounds = new Rectangle(x, y, Size, Size);
            var paletteIndex = sprite.BackgroundColorIndex;
            switch (colorIndex)
            {
                case 1: paletteIndex = sprite.ForegroundColorIndex; break;
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

        public bool HitTest(int x, int y) =>
            Bounds.IntersectsWith(new Rectangle(x, y, 1, 1));
    }
}
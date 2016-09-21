using System.Drawing;

namespace Sprdef
{
    public class SpriteEditor
    {
        private int CursorX { get; set; }
        private int CursorY { get; set; }
        public static bool Multicolor { get; set; } = false;

        public int PixelSize { get; set; } = 10;

        public C64Sprite Sprite { get; set; }

        public int InnerWidth => 24*PixelSize;
        public int InnerHeight => 21*PixelSize;
        public void MoveCursor(int x, int y)
        {
            CursorX += x;
            if (CursorX <= 0)
                CursorX = 0;
            if (Multicolor)
            {
                if (CursorX >= 22)
                    CursorX = 22;
            }
            else
            {
                if (CursorX >= 23)
                    CursorX = 23;
            }
            CursorY += y;
            if (CursorY <= 0)
                CursorY = 0;
            if (CursorY >= 20)
                CursorY = 20;
        }
        public void SetCursorX(int x) => CursorX = x;
        public void SetCursorY(int y) => CursorY = y;
        public int GetCursorX() => CursorX;
        public int GetCursorY() => CursorY;
        public void SetPixelAtCursor(int index) => Sprite.SetPixel(CursorX, CursorY, index);
        public void Draw(Graphics g, int x, int y)
        {
            if (Sprite == null)
                return;
            var physicalX = x;
            var physicalY = y;
            var ps = PixelSize - 1;
            var brushes = new SolidBrush[4];
            brushes[0] = new SolidBrush(C64Sprite.Palette.GetColor(C64Sprite.BackgroundColorIndex));
            brushes[1] = new SolidBrush(C64Sprite.Palette.GetColor(C64Sprite.ForegroundColorIndex));
            brushes[2] = new SolidBrush(C64Sprite.Palette.GetColor(C64Sprite.ExtraColor1Index));
            brushes[3] = new SolidBrush(C64Sprite.Palette.GetColor(C64Sprite.ExtraColor2Index));
            if (Multicolor)
            {
                var pwidth = PixelSize * 2;
                for (var sy = 0; sy < 21; sy++)
                {
                    for (var sx = 0; sx < 24; sx++)
                    {
                        if (sx % 2 == 0)
                        {
                            g.FillRectangle(brushes[Sprite.GetColorIndex(sx, sy)], physicalX, physicalY, pwidth - 1, ps);
                            physicalX += pwidth;
                        }
                    }
                    physicalY += PixelSize;
                    physicalX = x;
                }
                for (var i = 0; i < 4; i++)
                    brushes[i].Dispose();
                g.DrawRectangle(Pens.White, x + (CursorX * PixelSize), y + (CursorY * PixelSize), pwidth - 2, PixelSize - 2);
                g.DrawRectangle(Pens.Black, x + (CursorX * PixelSize) + 1, y + (CursorY * PixelSize) + 1, pwidth - 4, PixelSize - 4);
            }
            else
            {
                for (var sy = 0; sy < 21; sy++)
                {
                    for (var sx = 0; sx < 24; sx++)
                    {
                        g.FillRectangle(brushes[Sprite.GetPixel(sx, sy)], physicalX, physicalY, ps, ps);
                        physicalX += PixelSize;
                    }
                    physicalY += PixelSize;
                    physicalX = x;
                }
                for (var i = 0; i < 4; i++)
                    brushes[i].Dispose();
                g.DrawRectangle(Pens.White, x + (CursorX * PixelSize), y + (CursorY * PixelSize), PixelSize - 2, PixelSize - 2);
                g.DrawRectangle(Pens.Black, x + (CursorX * PixelSize) + 1, y + (CursorY * PixelSize) + 1, PixelSize - 4, PixelSize - 4);
            }
            brushes[0].Dispose();
            brushes[1].Dispose();
            brushes[2].Dispose();
            brushes[3].Dispose();
        }
    }
}

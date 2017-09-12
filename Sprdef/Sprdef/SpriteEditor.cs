using System.Drawing;
using System.Drawing.Imaging;

namespace Sprdef
{
    public class SpriteEditor
    {
        private int CursorX { get; set; }
        private int CursorY { get; set; }
        public static bool Multicolor { get; set; } = false;

        public int PixelSize { get; set; } = 10;

        public C64Sprite Sprite { get; set; }

        public int InnerWidth => C64Sprite.Width*PixelSize;
        public int InnerHeight => C64Sprite.Height*PixelSize;
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
        public void SavePngMultiColorDoubleWidth(string filename, C64Sprite[] sprites, bool transparentBackground)
        {
            using (var b = new Bitmap(sprites.Length*C64Sprite.Width, C64Sprite.Height))
            {
                var x = 0;
                foreach (var sprite in sprites)
                {
                    sprite.ExportMultiColorDoubleWidth(b, x, 0, transparentBackground);
                    x += C64Sprite.Width;
                }
                b.Save(filename, ImageFormat.Png);
            }
        }
        public void SavePngMultiColor(string filename, C64Sprite[] sprites, bool transparentBackground)
        {
            const int w = C64Sprite.Width/2;
            using (var b = new Bitmap(sprites.Length*w, C64Sprite.Height))
            {
                var x = 0;
                foreach (var sprite in sprites)
                {
                    sprite.ExportMultiColor(b, x, 0, transparentBackground);
                    x += w;
                }
                b.Save(filename, ImageFormat.Png);
            }
        }
        public void SavePng(string filename, C64Sprite[] sprites, bool transparentBackground)
        {
            using (var b = new Bitmap(sprites.Length*C64Sprite.Width, C64Sprite.Height))
            {
                var x = 0;
                foreach (var sprite in sprites)
                {
                    sprite.Export(b, x, 0, transparentBackground);
                    x += C64Sprite.Width;
                }
                b.Save(filename, ImageFormat.Png);
            }
        }
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
                for (var sy = 0; sy < C64Sprite.Height; sy++)
                {
                    for (var sx = 0; sx < C64Sprite.Width; sx++)
                    {
                        if (sx % 2 != 0)
                            continue;
                        g.FillRectangle(brushes[Sprite.GetColorIndex(sx, sy)], physicalX, physicalY, pwidth - 1, ps);
                        physicalX += pwidth;
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
                for (var sy = 0; sy < C64Sprite.Height; sy++)
                {
                    for (var sx = 0; sx < C64Sprite.Width; sx++)
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

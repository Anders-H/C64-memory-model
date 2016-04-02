using System;
using System.Drawing;

namespace Sprdef
{
    public class C64Sprite : IScreenThing
    {
        private byte[,] SpriteData { get; }
        private Bitmap SpritePreviewData { get; }
        public Color[] SpritePalette { get; } = new Color[4];
        public  static Color[] C64Palette { get; } = new Color[16];
        private Rectangle _bounds;
        public Rectangle Bounds { get; }
        static C64Sprite()
        {
            C64Palette[0] = Color.FromArgb(0, 0, 0);
            C64Palette[1] = Color.FromArgb(255, 255, 255);
            C64Palette[2] = Color.FromArgb(136, 0, 0);
            C64Palette[3] = Color.FromArgb(170, 255, 238);
            C64Palette[4] = Color.FromArgb(204, 68, 204);
            C64Palette[5] = Color.FromArgb(0, 204, 85);
            C64Palette[6] = Color.FromArgb(0, 0, 170);
            C64Palette[7] = Color.FromArgb(238, 238, 119);
            C64Palette[8] = Color.FromArgb(221, 136, 85);
            C64Palette[9] = Color.FromArgb(102, 68, 0);
            C64Palette[10] = Color.FromArgb(255, 119, 119);
            C64Palette[11] = Color.FromArgb(51, 51, 51);
            C64Palette[12] = Color.FromArgb(119, 119, 119);
            C64Palette[13] = Color.FromArgb(170, 255, 102);
            C64Palette[14] = Color.FromArgb(0, 136, 255);
            C64Palette[15] = Color.FromArgb(187, 187, 187);
        }
        public C64Sprite()
        {
            SpriteData = new byte[24, 21];
            SpritePalette[0] = C64Palette[0];
            SpritePalette[1] = C64Palette[1];
            SpritePalette[2] = C64Palette[2];
            SpritePalette[3] = C64Palette[3];
            SpritePreviewData = new Bitmap(24, 21);
            using (var g = Graphics.FromImage(SpritePreviewData))
                g.FillRectangle(Brushes.Black, 0, 0, 24, 21);
        }
        public void SetPixel(int x, int y, int colorIndex)
        {
            SpriteData[x, y] = (byte)colorIndex;
            SpritePreviewData.SetPixel(x, y, C64Palette[colorIndex]);
        }
        public byte GetPixel(int x, int y) => SpriteData[x, y];
        public void Draw(Graphics g, int x, int y, bool doubleSize)
        {
            if (doubleSize)
            {
                g.DrawImage(SpritePreviewData, x, y, 48, 42);
                _bounds = new Rectangle(x, y, 48, 42);
            }
            else
            {
                g.DrawImage(SpritePreviewData, x, y);
                _bounds = new Rectangle(x, y, 24, 21);
            }
        }
        public bool HitTest(int x, int y) => Bounds.IntersectsWith(new Rectangle(x, y, 1, 1));
    }
}

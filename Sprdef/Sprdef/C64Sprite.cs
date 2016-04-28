using System.Drawing;
using System.IO;

namespace Sprdef
{
    public class C64Sprite : IScreenThing
    {
        private byte[,] SpriteData { get; }
        private Bitmap SpritePreviewData { get; }
        public static C64MemoryModel.C64Palette Palette = new C64MemoryModel.C64Palette();
        public static int BackgroundColorIndex { get; set; }
        public static int ForegroundColorIndex { get; set; }
        public static int ExtraColor1Index { get; set; }
        public static int ExtraColor2Index { get; set; }
        public Rectangle Bounds { get; private set; }
        static C64Sprite()
        {
            BackgroundColorIndex = 0;
            ForegroundColorIndex = 1;
            ExtraColor1Index = 2;
            ExtraColor2Index = 3;
        }
        public C64Sprite()
        {
            SpriteData = new byte[24, 21];
            SpritePreviewData = new Bitmap(24, 21);
            using (var g = Graphics.FromImage(SpritePreviewData))
                g.FillRectangle(Brushes.Black, 0, 0, 24, 21);
        }
        public void ResetPixels()
        {
            for (var y = 0; y < 21; y++)
                for (var x = 0; x < 24; x++)
                    SetPixel(x, y, GetPixel(x, y));
        }
        public void SetPixel(int x, int y, int colorIndex)
        {
            SpriteData[x, y] = (byte)colorIndex;
            var paletteIndex = BackgroundColorIndex;
            switch (colorIndex)
            {
                case 1: paletteIndex = ForegroundColorIndex; break;
                case 2: paletteIndex = ExtraColor1Index; break;
                case 3: paletteIndex = ExtraColor2Index; break;
            }       
            SpritePreviewData.SetPixel(x, y, Palette.GetColor(paletteIndex));
        }
        public byte GetPixel(int x, int y) => SpriteData[x, y];
        public void Draw(Graphics g, int x, int y, bool doubleSize)
        {
            if (doubleSize)
            {
                g.DrawImage(SpritePreviewData, x, y, 48, 43);
                Bounds = new Rectangle(x, y, 48, 42);
            }
            else
            {
                g.DrawImage(SpritePreviewData, x, y);
                Bounds = new Rectangle(x, y, 24, 21);
            }
        }
        public bool HitTest(int x, int y) => Bounds.IntersectsWith(new Rectangle(x, y, 1, 1));
        public byte[] GetBytes()
        {
            var ret = new byte[63];
            var i = 0;
            for (var y = 0; y < 21; y++)
                for (var x = 0; x < 3; x++)
                {
                    var physicalX = 8*x;
                    var b = new C64MemoryModel.Types.Byte(IsSet(physicalX, y), IsSet(physicalX + 1, y), IsSet(physicalX + 2, y), IsSet(physicalX + 3, y), IsSet(physicalX + 4, y), IsSet(physicalX + 5, y), IsSet(physicalX + 6, y), IsSet(physicalX + 7, y));
                    ret[i] = b.ToByte();
                    i++;
                }
            return ret;
        }
        private bool IsSet(int x, int y) => SpriteData[x, y] > 0;
        public  bool Load(BinaryReader sr)
        {
            int x = 0, y = 0;
            for (var i = 0; i < 63; i++)
            {
                var b = new C64MemoryModel.Types.Byte(sr.ReadByte());
                var physicalX = x*8;
                SetPixel(physicalX, y, b.Bit0 ? 1 : 0);
                SetPixel(physicalX + 1, y, b.Bit1 ? 1 : 0);
                SetPixel(physicalX + 2, y, b.Bit2 ? 1 : 0);
                SetPixel(physicalX + 3, y, b.Bit3 ? 1 : 0);
                SetPixel(physicalX + 4, y, b.Bit4 ? 1 : 0);
                SetPixel(physicalX + 5, y, b.Bit5 ? 1 : 0);
                SetPixel(physicalX + 6, y, b.Bit6 ? 1 : 0);
                SetPixel(physicalX + 7, y, b.Bit7 ? 1 : 0);
                x++;
                if (x > 2)
                {
                    x = 0;
                    y++;
                }
            }
            return true;
        }
    }
}

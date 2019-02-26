using System.Drawing;
using System.Drawing.Drawing2D;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace Sprdef.Tools.MemoryVisualizer.Renderer
{
    public class SpriteScreenRenderer : ScreenRenderer
    {
        public SpriteScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
        {

        }

        public override int RenderText(ref int displayPointer, Memory memory)
        {
            var steps = 0;
            for (var row = 0; row < RowCount; row++)
            {
                if (displayPointer > ushort.MaxValue)
                    break;
                if (row >= 8)
                    break;
                Characters.SetCharacters(0, row, (row + 1).ToString("0"));
                Characters.SetCharacters(1, row, ":");
                Characters.SetCharacters(3, row, displayPointer.ToString("00000"));
                Characters.SetCharacters(9, row, "$" + displayPointer.ToString("X0000"));
                steps += 63;
                displayPointer += 63;
            }
            return steps;
        }

        public override void DrawGraphics(Graphics g, Size size, Memory memory, int start)
        {
            var x = size.Width - 300;
            var y = 100;
            var ystep = size.Height / 10;
            if (ystep < 42)
                ystep = 42;
            for (var s = 0; s < 4; s++)
            {
                DrawSprite(g, x, y, memory, start);
                start += 63;
                DrawSprite(g, x + 100, y, memory, start);
                start += 63;
                y += ystep;
            }
        }

        private void DrawSprite(Graphics g, int x, int y, Memory memory, int start)
        {
            using (var b = new Bitmap(24, 21))
            {
                for (var by = 0; by < 21; by++)
                    for (var bx = 0; bx < 3; bx++)
                    {
                        if (start <= ushort.MaxValue)
                            PlotByte(b, memory.GetBits((Address)start), bx * 8, by);
                        start++;
                    }
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(b, x, y, 48, 42);
            }
        }

        private void PlotByte(Bitmap bitmap, Byte b, int x, int y)
        {
            bitmap.SetPixel(x, y, b.Bit7 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 1, y, b.Bit6 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 2, y, b.Bit5 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 3, y, b.Bit4 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 4, y, b.Bit3 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 5, y, b.Bit2 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 6, y, b.Bit1 ? Color.Black : Color.White);
            bitmap.SetPixel(x + 7, y, b.Bit0 ? Color.Black : Color.White);
        }
    }
}
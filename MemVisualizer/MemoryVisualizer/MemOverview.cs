using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using C64MemoryModel.Mem;

namespace MemoryVisualizer
{
    public class MemOverview
    {
        private int[] Data { get; set; }
        private Bitmap Bitmap { get; set; }
        private static int _actualHeight;
        private static int Height => ushort.MaxValue/_bytesPerPixel;
        private static int _bytesPerPixel = 128;
        private static int _width = 16;
        private static int _disassembly;

        public static MemOverview Create(Memory memory, int actualHeight, int disassembly)
        {
            _actualHeight = actualHeight;
            _disassembly = disassembly;
            _bytesPerPixel = GetDivFactorFromHeight(actualHeight);
            var x = new MemOverview {
                Data = new int[Height]
            };
            var memIndex = 0;
            for (var yIndex = 0; yIndex < Height; yIndex++)
            {
                var byteSum = 0;
                for (var y = 0; y < _bytesPerPixel; y++)
                {
                    byteSum += memory.GetByte((ushort)memIndex);
                    memIndex++;
                }
                x.Data[yIndex] = byteSum;
            }
            x.NormalizeData();
            x.CreateBitmap();
            return x;
        }

        public bool NeedsRecreating(int actualHeight)
        {
            var f1 = GetDivFactorFromHeight(actualHeight);
            var f2 = GetDivFactorFromHeight(_actualHeight);
            return f1 != f2;
        }

        private void NormalizeData()
        {
            var highest = (double)Data.Concat(new[] {0}).Max();
            if (highest <= 0)
                return;
            for(var i = 0; i < Data.Length; i++)
            {
                if (Data[i] <= 0)
                    continue;
                var percent = Data[i] / highest;
                var newValue = Math.Round(255 * percent);
                Data[i] = (int)newValue;
                if (Data[i] > 255)
                    Data[i] = 255;
                else if (Data[i] < 0)
                    Data[i] = 0;
            }
        }

        private void CreateBitmap()
        {
            try
            {
                Bitmap?.Dispose();
            }
            catch
            {
                // Ignored.
            }
            Bitmap = new Bitmap(_width, Height);
            using (var g = Graphics.FromImage(Bitmap))
            {
                g.SmoothingMode = SmoothingMode.None;
                for (var i = 0; i < Data.Length; i++)
                {
                    using (var p = new Pen(Color.FromArgb(Data[i], Data[i], Data[i])))
                        g.DrawLine(p, 0, i, _width, i);
                    if (_disassembly <= 0)
                        continue;
                    {
                        var mem = i * _bytesPerPixel;
                        if (mem > _disassembly)
                            continue;
                        using (var p = new Pen(Color.FromArgb(140, 255, 0, 0)))
                            g.DrawLine(p, 0, i, _width, i);
                    }
                }
            }
        }

        public void Draw(Graphics g, int displayPointer, int x, int y)
        {
            g.DrawImage(Bitmap, x, y);
            var ypos = y + (displayPointer / _bytesPerPixel);
            g.DrawLine(Pens.Red, x + 7, ypos, x + 20, ypos);
        }

        private static int GetDivFactorFromHeight(int height)
        {
            if (height >= 1200)
                return 64;
            return height >= 600 ? 128 : 256;
        }
    }
}
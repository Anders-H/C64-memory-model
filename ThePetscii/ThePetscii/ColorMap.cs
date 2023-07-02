using System;
using System.Drawing;
using C64Color;

namespace ThePetscii
{
    public class ColorMap : IDisposable
    {
        private readonly Resources _resources;
        public const int Width = 40;
        public const int Height = 25;
        public readonly ColorName[,] Colors = new ColorName[Width, Height];
        
        public ColorMap(ColorName initialColor)
        {
            _resources = new Resources();
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    Colors[x, y] = initialColor;
        }

        public Brush GetBrush(ColorName color) =>
            _resources.GetColorBrush(color);

        public void SetColor(int x, int y, ColorName color) =>
            Colors[x, y] = color;

        public byte GetColorByte(int x, int y) =>
            (byte)Colors[x, y];
        
        public void Dispose() =>
            _resources.Dispose();
    }
}
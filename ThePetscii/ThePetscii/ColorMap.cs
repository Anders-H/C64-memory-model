using System;
using System.Drawing;
using C64MemoryModel.Graphics;

namespace ThePetscii
{
    public class ColorMap : IDisposable
    {
        private readonly Brush[] _brushes = new Brush[16];
        private readonly C64Palette _palette;
        public const int Width = 40;
        public const int Height = 25;
        public C64Color[,] Colors = new C64Color[Width, Height];
        
        public ColorMap(C64Palette palette, C64Color initialColor)
        {
            _palette = palette;
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    Colors[x, y] = initialColor;
        }

        public Brush GetBrush(C64Color color)
        {
            var index = (int)color;
            if (_brushes[index] == null)
                _brushes[index] = new SolidBrush(_palette.GetColor(color));
            return _brushes[index];
        }

        public void Dispose()
        {
            _palette.Dispose();
            for (var i = 0; i < 16; i++)
                _brushes[i]?.Dispose();
        }
    }
}
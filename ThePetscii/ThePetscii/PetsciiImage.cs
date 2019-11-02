using System;
using C64MemoryModel.Graphics;

namespace ThePetscii
{
    public class PetsciiImage : IDisposable
    {
        public ColorMap Background { get; }
        public ColorMap Foreground { get; }
        public PetsciiMap Content { get; }

        public PetsciiImage(C64Palette palette)
        {
            Background = new ColorMap(palette, C64Color.Blue);
            Foreground = new ColorMap(palette, C64Color.LightBlue);
            Content = new PetsciiMap();
        }
        
        public PetsciiImage(ColorMap background, ColorMap foreground, PetsciiMap content)
        {
            Background = background;
            Foreground = foreground;
            Content = content;
        }

        public bool IsAllSet(int x, int y) =>
            Content.IsAllSet(x, y);

        public bool IsNoneSet(int x, int y) =>
            Content.IsNoneSet(x, y);

        public PetsciiChar GetChar(int x, int y) =>
            Content.GetChar(x, y);
        
        public void Dispose()
        {
            Background.Dispose();
            Foreground.Dispose();
        }
    }
}
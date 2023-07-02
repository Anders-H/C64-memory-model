using System;

namespace ThePetscii
{
    public class PetsciiImage : IDisposable
    {
        public ColorMap Foreground { get; }
        public PetsciiMap Content { get; }

        public PetsciiImage(ColorMap foreground)
        {
            Foreground = foreground;
            Content = new PetsciiMap();
        }
        
        public PetsciiImage(ColorMap foreground, PetsciiMap content)
        {
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
            Foreground.Dispose();
        }
    }
}
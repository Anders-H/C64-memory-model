using System.Drawing;

namespace Sprdef.Rendering
{
    public interface IScreenThing
    {
        Rectangle Bounds { get; }
        bool HitTest(int x, int y);
    }
}
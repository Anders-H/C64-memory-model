using System.Drawing;

namespace Sprdef
{
    public interface IScreenThing
    {
        Rectangle Bounds { get; }
        bool HitTest(int x, int y);
    }
}

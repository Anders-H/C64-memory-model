using System.Drawing;
using C64MemoryModel.Mem;

namespace Sprdef.Tools.MemoryVisualizer.Renderer
{
    public interface IScreenRenderer
    {
        int RenderText(ref int displayPointer, Memory memory);
        void DrawGraphics(Graphics g, Size size, Memory memory, int start);
    }
}
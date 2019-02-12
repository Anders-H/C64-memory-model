using C64MemoryModel.Mem;

namespace MemoryVisualizer.Renderer
{
    public interface IScreenRenderer
    {
        int Render(ref int displayPointer, Memory memory);
    }
}
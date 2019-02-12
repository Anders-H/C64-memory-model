using C64MemoryModel.Mem;

namespace MemoryVisualizer.Renderer
{
    public abstract class ScreenRenderer : IScreenRenderer
    {
        public int RowCount { get; }
        public ScreenCharacterMap Characters { get; }

        protected ScreenRenderer(int rowCount, ScreenCharacterMap characters)
        {
            RowCount = rowCount;
            Characters = characters;
        }

        public abstract int Render(ref int displayPointer, Memory memory);
    }
}
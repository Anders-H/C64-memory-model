using C64MemoryModel.Mem;

namespace MemoryVisualizer.Renderer
{
    public class SpriteScreenRenderer : ScreenRenderer
    {
        public SpriteScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
        {

        }

        public override int Render(ref int displayPointer, Memory memory)
        {
            var steps = 0;
            for (var row = 0; row < RowCount; row++)
            {
                if (displayPointer > ushort.MaxValue)
                    break;
                if (row >= 8)
                    break;
                Characters.SetCharacters(0, row, (row + 1).ToString("0"));
                Characters.SetCharacters(1, row, ":");
                Characters.SetCharacters(3, row, displayPointer.ToString("00000"));
                Characters.SetCharacters(9, row, "$" + displayPointer.ToString("X0000"));
                steps += 63;
                displayPointer += 63;
            }
            return steps;
        }
    }
}
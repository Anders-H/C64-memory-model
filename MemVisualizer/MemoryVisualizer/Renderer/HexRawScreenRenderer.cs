using C64MemoryModel.Mem;

namespace MemoryVisualizer.Renderer
{
    public class HexRawScreenRenderer : ScreenRenderer
    {
        public HexRawScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
        {
        }

        public override int Render(ref int displayPointer, Memory memory)
        {
            for (var row = 0; row < RowCount; row++)
            {
                if (displayPointer > ushort.MaxValue)
                    break;
                Characters.SetCharacters(0, row, displayPointer.ToString("00000"));
                Characters.SetCharacters(6, row, "$" + displayPointer.ToString("X0000"));
                var x = 12;
                for (var col = 0; col < 8; col++)
                {
                    if (displayPointer > ushort.MaxValue)
                        break;
                    Characters.SetCharacters(x, row, memory.GetByte((ushort)displayPointer).ToString("X2"));
                    x += 3;
                    displayPointer++;
                }
            }
            return RowCount * 8;
        }
    }
}
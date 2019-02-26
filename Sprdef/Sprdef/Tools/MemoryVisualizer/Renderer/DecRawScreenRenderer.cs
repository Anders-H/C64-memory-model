using System.Drawing;
using C64MemoryModel.Mem;

namespace Sprdef.Tools.MemoryVisualizer.Renderer
{
    public class DecRawScreenRenderer : ScreenRenderer
    {
        public DecRawScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
        {
        }

        public override int RenderText(ref int displayPointer, Memory memory)
        {
            for (var row = 0; row < RowCount; row++)
            {
                if (displayPointer > ushort.MaxValue)
                    break;
                Characters.SetCharacters(0, row, displayPointer.ToString("00000"));
                Characters.SetCharacters(6, row, "$" + displayPointer.ToString("X0000"));
                var x = 12;
                for (var col = 0; col < 4; col++)
                {
                    if (displayPointer > ushort.MaxValue)
                        break;
                    Characters.SetCharacters(x, row, memory.GetByte((ushort)displayPointer).ToString("000"));
                    x += 4;
                    displayPointer++;
                }
            }
            return RowCount * 4;
        }

        public override void DrawGraphics(Graphics g, Size size, Memory memory, int start)
        {
        }
    }
}
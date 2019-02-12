using C64MemoryModel.Mem;

namespace MemoryVisualizer.Renderer
{
    public class DisassemblyScreenRenderer : ScreenRenderer
    {
        public DisassemblyScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
        {
        }

        public override int Render(ref int displayPointer, Memory memory)
        {
            memory.SetBytePointer((ushort)displayPointer);
            var disassembly = memory.GetDisassembly(25);
            var disassemblyRows = System.Text.RegularExpressions.Regex.Split(disassembly, @"\n");
            for (var row = 0; row < disassemblyRows.Length; row++)
                Characters.SetCharacters(0, row, disassemblyRows[row]);
            return memory.GetBytePointer().Value - displayPointer;
        }
    }
}
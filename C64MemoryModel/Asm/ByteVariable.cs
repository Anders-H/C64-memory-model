using System.Runtime.CompilerServices;

namespace C64MemoryModel.Asm
{
    public class ByteVariable : VariableBase<byte>
    {
        public ByteVariable(Assembler assembler, string name, ushort address) : base(assembler, name, address) { } 
        public override void WriteAssign(byte value)
        {
            Value = value;
        }
    }
}

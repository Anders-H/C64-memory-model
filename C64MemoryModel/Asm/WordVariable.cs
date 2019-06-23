using System;
using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public class WordVariable : VariableBase<ushort>
    {
        public WordVariable(Assembler assembler, string name, Address address) : base(assembler, name, address)
        {
        }

        public override void WriteAssign(Address address, ushort value)
        {
            Value = value;
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            Assembler.Memory.SetBytePointer(address); //From argument - wher the assembler sould be written.
            Assembler.Lda(low);
            Assembler.Sta(Address);
            Assembler.Lda(high);
            Assembler.Sta(Address + 1);
        }

        public override void WriteDirect(ushort value)
        {
            Value = value;
            Assembler.Memory.SetBytePointer(Address); //From property - where the variable is stored.
            Assembler.Memory.SetWord(value);
        }
    }
}
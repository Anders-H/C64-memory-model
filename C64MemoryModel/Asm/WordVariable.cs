using System;

namespace C64MemoryModel.Asm
{
    public class WordVariable : VariableBase<Word>
    {
        public WordVariable(Assembler assembler, string name, Word address) : base(assembler, name, address) { }
        public override void WriteAssign(Word address, Word value)
        {
            Value = value;
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            Assembler.Memory.SetBytePointer(address); //From argument - wher the assembler sould be written.
            Assembler.Lda(low);
            Assembler.Sta(Address);
            Assembler.Lda(high);
            Assembler.Sta((Word)(Address + 1));
        }
        public override void WriteDirect(Word value)
        {
            Value = value;
            Assembler.Memory.SetBytePointer(Address); //From property - where the variable is stored.
            Assembler.Memory.SetWord(value);
        }
    }
}

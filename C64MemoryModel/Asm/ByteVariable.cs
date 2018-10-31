namespace C64MemoryModel.Asm
{
    public class ByteVariable : VariableBase<byte>
    {
        public ByteVariable(Assembler assembler, string name, ushort address) : base(assembler, name, address)
        {
        }

        public override void WriteAssign(ushort address, byte value)
        {
            Value = value;
            Assembler.Memory.SetBytePointer(address); //From argument - where the assembler sould be written.
            Assembler.Lda(Value);
            Assembler.Sta(Address);
        }

        public override void WriteDirect(byte value)
        {
            Value = value;
            Assembler.Memory.SetBytePointer(Address); //From property - where the variable is stored.
            Assembler.Memory.SetByte(value);
        }
    }
}
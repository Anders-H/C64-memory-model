using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public class ExtendedAssembler
    {
        private Assembler Assembler { get; }
        public VariableList Variables { get; }
        internal ExtendedAssembler(Assembler assembler) { Assembler = assembler; Variables = new VariableList(Assembler); }
        public void PokeByte(Word address, byte value)
        {
            Assembler.Lda(value);
            Assembler.Sta(address);
        }
        public void PokeByte(WordVariable address, byte value)
        {
            Assembler.Lda(value);
            Assembler.Sta(address.Address);
        }
        public void PokeByte(Word address, ByteVariable value)
        {
            Assembler.Lda(value.Address);
            Assembler.Sta(address);
        }
        public void PokeByte(WordVariable address, ByteVariable value)
        {
            Assembler.Lda(value.Address);
            Assembler.Sta(address.Address);
        }
        public ByteVariable CreateByteVariable(string name, Word address) => Variables.CreateByteVariable(name, address);
        public ByteVariable CreateByteVariable(string name, IMemoryLocation address) => Variables.CreateByteVariable(name, address.StartAddress);
        public ByteVariable CreateByteVariable(MemoryModelLocation address) => Variables.CreateByteVariable(address.Name.ToString(), address.StartAddress);
        public WordVariable CreateWordVariable(string name, Word address) => Variables.CreateWordVariable(name, address);
        public WordVariable CreateWordVariable(string name, IMemoryLocation address) => Variables.CreateWordVariable(name, address.StartAddress);
        public WordVariable CreateWordVariable(MemoryModelLocation address) => Variables.CreateWordVariable(address.Name.ToString(), address.StartAddress);
    }
}

namespace C64MemoryModel.Asm
{
    public class WordVariable : VariableBase<ushort>
    {
        public WordVariable(Assembler assembler, string name, ushort address) : base(assembler, name, address) { }
        public override void WriteAssign(ushort value)
        {
            Value = value;
        }
    }
}

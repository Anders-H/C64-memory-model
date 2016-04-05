namespace C64MemoryModel.Asm
{
    public class WordVariable : VariableBase
    {
        public ushort Value { get; set; }
        internal WordVariable(Memory memory, string name, ushort address, ushort initialValue) : base(memory, name, address)
        {
            Value = initialValue;
        }
    }
}

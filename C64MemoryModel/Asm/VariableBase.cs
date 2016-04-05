namespace C64MemoryModel.Asm
{
    public abstract class VariableBase
    {
        protected Memory Memory { get; }
        public string Name { get; }
        public ushort Address { get; }
        protected VariableBase(Memory memory, string name, ushort address)
        {
            Memory = memory;
            Name = name;
            Address = address;
        }
    }
}

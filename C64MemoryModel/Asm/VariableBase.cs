namespace C64MemoryModel.Asm
{
    public interface IVariable
    {
    }
    public abstract class VariableBase<T> : IVariable
    {
        protected Assembler Assembler { get; }
        public string Name { get; }
        public ushort Address { get; }
        public T Value { get; protected set; }
        protected VariableBase(Assembler assembler, string name, ushort address)
        {
            Assembler = assembler;
            Name = name;
            Address = address;
        }
        public abstract void WriteAssign(T value);
    }
}

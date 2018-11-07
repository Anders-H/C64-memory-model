using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public abstract class VariableBase<T> : IVariable
    {
        protected Assembler Assembler { get; }
        public string Name { get; }
        public Address Address { get; }
        public T Value { get; protected set; }

        protected VariableBase(Assembler assembler, string name, Address address)
        {
            Assembler = assembler;
            Name = name;
            Address = address;
        }

        public abstract void WriteAssign(Address address, T value);

        public abstract void WriteDirect(T value);
    }
}
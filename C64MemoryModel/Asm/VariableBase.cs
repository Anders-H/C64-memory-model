using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public interface IVariable
    {
        string Name { get; }
        Word Address { get; }
    }
    public abstract class VariableBase<T> : IVariable
    {
        protected Assembler Assembler { get; }
        public string Name { get; }
        public Word Address { get; }
        public T Value { get; protected set; }
        protected VariableBase(Assembler assembler, string name, Word address)
        {
            Assembler = assembler;
            Name = name;
            Address = address;
        }
        public abstract void WriteAssign(Word address, T value);
        public abstract void WriteDirect(T value);
    }
}

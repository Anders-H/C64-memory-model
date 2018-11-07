using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public interface IVariable
    {
        string Name { get; }
        Address Address { get; }
    }
}
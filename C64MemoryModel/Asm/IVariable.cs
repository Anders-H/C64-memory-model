namespace C64MemoryModel.Asm
{
    public interface IVariable
    {
        string Name { get; }
        ushort Address { get; }
    }
}
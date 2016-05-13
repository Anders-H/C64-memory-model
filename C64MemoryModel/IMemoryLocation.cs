namespace C64MemoryModel
{
    public interface IMemoryLocation
    {
        Word StartAddress { get; }
        Word EndAddress { get; }
        Word Length { get; }
        bool IsMemoyModel { get; }
        bool IsCustomBookmark { get; }
        bool HitTest(Word address);
    }
}

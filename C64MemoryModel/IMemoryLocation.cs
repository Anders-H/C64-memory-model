namespace C64MemoryModel
{
    public interface IMemoryLocation
    {
        ushort StartAddress { get; }
        ushort EndAddress { get; }
        ushort Length { get; }
        bool IsMemoyModel { get; }
        bool IsCustomBookmark { get; }
        bool HitTest(ushort address);
    }
}

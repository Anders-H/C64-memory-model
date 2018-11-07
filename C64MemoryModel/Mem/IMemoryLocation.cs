using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public interface IMemoryLocation
    {
        Address StartAddress { get; }
        Address EndAddress { get; }
        ushort Length { get; }
        bool IsMemoyModel { get; }
        bool IsCustomBookmark { get; }
        bool HitTest(Address address);
    }
}
using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace Sprdef
{
    public class SimpleMemoryLocation : IMemoryLocation
    {
        public SimpleMemoryLocation(Address startAddress)
        {
            StartAddress = startAddress;
            EndAddress = startAddress;
            Length = 0;
            IsMemoyModel = false;
            IsCustomBookmark = false;
        }

        public bool HitTest(Address address) =>
            StartAddress.Value == address.Value;

        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length { get; }
        public bool IsMemoyModel { get; }
        public bool IsCustomBookmark { get; }
    }
}
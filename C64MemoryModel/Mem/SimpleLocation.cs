using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class SimpleLocation : IMemoryLocation
    {
        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length { get; }
        public bool IsMemoyModel { get; }
        public bool IsCustomBookmark { get; }

        public SimpleLocation(Address startAddress)
        {
            StartAddress = startAddress;
            EndAddress = startAddress;
            Length = 0;
            IsMemoyModel = false;
            IsCustomBookmark = false;
        }

        public bool HitTest(Address address) =>
            StartAddress.Value == address.Value;
    }
}
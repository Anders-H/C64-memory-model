using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class RangeLocation : IMemoryLocation
    {
        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length { get; }
        public bool IsMemoyModel { get; }
        public bool IsCustomBookmark { get; }

        public RangeLocation(Address startAddress, ushort length)
        {
            StartAddress = startAddress;
            EndAddress = new Address((ushort)(startAddress.Value + length));
            Length = length;
            IsMemoyModel = false;
            IsCustomBookmark = false;
        }

        public bool HitTest(Address address) =>
            StartAddress.Value >= address.Value && EndAddress.Value <= address.Value;
    }
}
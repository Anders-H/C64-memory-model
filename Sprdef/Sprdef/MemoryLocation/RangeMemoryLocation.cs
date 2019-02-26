using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace Sprdef.MemoryLocation
{
    public class RangeMemoryLocation : IMemoryLocation
    {
        public RangeMemoryLocation(Address startAddress, ushort length)
        {
            StartAddress = startAddress;
            EndAddress = new Address((ushort)(startAddress.Value + length));
            Length = length;
            IsMemoyModel = false;
            IsCustomBookmark = false;
        }

        public bool HitTest(Address address) =>
            StartAddress.Value >= address.Value && EndAddress.Value <= address.Value;

        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length { get; }
        public bool IsMemoyModel { get; }
        public bool IsCustomBookmark { get; }
    }
}
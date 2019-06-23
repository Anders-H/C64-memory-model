using System;
using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class MemoryModelLocation : IMemoryLocation
    {
        internal static MemoryModelLocationList List { private get; set; }
        public MemoryModelLocationName Name { get; }
        public string DisplayName { get; }
        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length => (ushort)(EndAddress - StartAddress + 1);
        public bool IsMemoyModel => true;
        public bool IsCustomBookmark => false;

        internal MemoryModelLocation(MemoryModelLocationName name, Address address)
        {
            Name = name;
            DisplayName = name.ToString();
            StartAddress = address;
            EndAddress = address;
        }

        internal MemoryModelLocation(MemoryModelLocationName name, Address startAddress, Address endAddress)
        {
            if (startAddress > endAddress)
                throw new SystemException($"Negative range: {startAddress} - {endAddress}");
            Name = name;
            DisplayName = name.ToString();
            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public override string ToString() =>
            Length > 1 ? $"{StartAddress:00000}-{EndAddress:00000} ({Length:0000}) : {DisplayName}" : $"{StartAddress:00000}       ({Length:0000}) : {DisplayName}";

        public bool HitTest(Address address) =>
            address >= StartAddress && address <= EndAddress;

        public static Address operator +(MemoryModelLocation a, MemoryModelLocation b) =>
            (Address)(a.StartAddress + b.StartAddress);

        public static Address operator +(MemoryModelLocation a, int b) =>
            (Address)(a.StartAddress + b);

        public static implicit operator Address (MemoryModelLocation x) =>
            x.StartAddress;

        public static implicit operator MemoryModelLocation(MemoryModelLocationName x) =>
            List.GetLocation(x);
    }
}
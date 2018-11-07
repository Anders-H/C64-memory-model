using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class MemoryBookmark : IMemoryLocation
    {
        public string Name { get; set; }
        public Address StartAddress { get; }
        public Address EndAddress { get; }
        public ushort Length => (ushort)(EndAddress - StartAddress + 1);
        public bool IsMemoyModel => false;
        public bool IsCustomBookmark => true;

        public MemoryBookmark(Address address)
        {
            Name = "";
            StartAddress = address;
            EndAddress = address;
        }

        public MemoryBookmark(string name, Address address)
        {
            Name = name;
            StartAddress = address;
            EndAddress = address;
        }

        public MemoryBookmark(string name, Address startAddress, Address endAddress)
        {
            if (startAddress > endAddress)
                throw new SystemException($"Negative range: {startAddress} - {endAddress}");
            Name = name;
            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public override string ToString() =>
            Length > 1 ? $"{StartAddress:00000}-{EndAddress:00000} ({Length:0000}) : {Name}" : $"{StartAddress:00000}       ({Length:0000}) : {Name}";

        public bool HitTest(Address address) =>
            address >= StartAddress && address <= EndAddress;
    }

    public class MemoryBookmarkList : List<MemoryBookmark>
    {
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine("START-END   (LEN ) : NAME");
            s.AppendLine(new string('=', 78));
            ForEach(x => s.AppendLine(x.ToString()));
            return s.ToString();
        }
        public MemoryBookmark GetLocation(string name)
            => this.FirstOrDefault(x => string.Compare(name, x.Name, StringComparison.CurrentCultureIgnoreCase) == 0);

        public MemoryBookmark GetLocation(Address address)
            => this.FirstOrDefault(x => x.HitTest(address));

        public void Add(string name, Address address) =>
            Add(new MemoryBookmark(name, address));

        public void Add(string name, Address startAddress, Address endAddress) =>
            Add(new MemoryBookmark(name, startAddress, endAddress));

        public List<IMemoryLocation> GetAll(Address address) =>
            this.Where(x => x.HitTest(address)).Cast<IMemoryLocation>().ToList();

        public List<IMemoryLocation> GetAll(string name) =>
            this.Where(x => string.Compare(name, x.Name, StringComparison.CurrentCultureIgnoreCase) == 0).Cast<IMemoryLocation>().ToList();
    }
}
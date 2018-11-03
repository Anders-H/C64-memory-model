using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C64MemoryModel.Mem
{
    public class MemoryBookmark : IMemoryLocation
    {
        public string Name { get; set; }
        public ushort StartAddress { get; }
        public ushort EndAddress { get; }
        public ushort Length => (ushort)(EndAddress - StartAddress + 1);
        public bool IsMemoyModel => false;
        public bool IsCustomBookmark => true;

        public MemoryBookmark(ushort address)
        {
            Name = "";
            StartAddress = address;
            EndAddress = address;
        }

        public MemoryBookmark(string name, ushort address)
        {
            Name = name;
            StartAddress = address;
            EndAddress = address;
        }

        public MemoryBookmark(string name, ushort startAddress, ushort endAddress)
        {
            if (startAddress > endAddress)
                throw new SystemException($"Negative range: {startAddress} - {endAddress}");
            Name = name;
            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public override string ToString() =>
            Length > 1 ? $"{StartAddress:00000}-{EndAddress:00000} ({Length:0000}) : {Name}" : $"{StartAddress:00000}       ({Length:0000}) : {Name}";

        public bool HitTest(ushort address) =>
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

        public MemoryBookmark GetLocation(ushort address)
            => this.FirstOrDefault(x => x.HitTest(address));

        public void Add(string name, ushort address) =>
            Add(new MemoryBookmark(name, address));

        public void Add(string name, ushort startAddress, ushort endAddress) =>
            Add(new MemoryBookmark(name, startAddress, endAddress));

        public List<IMemoryLocation> GetAll(ushort address) =>
            this.Where(x => x.HitTest(address)).Cast<IMemoryLocation>().ToList();

        public List<IMemoryLocation> GetAll(string name) =>
            this.Where(x => string.Compare(name, x.Name, StringComparison.CurrentCultureIgnoreCase) == 0).Cast<IMemoryLocation>().ToList();
    }
}
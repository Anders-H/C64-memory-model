using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C64MemoryModel.Types;

namespace C64MemoryModel
{
    public class MemoryModelLocation : IMemoryLocation
    {
        internal static MemoryModelLocationList List { private get; set; }
        public MemoryModelLocationName Name { get; }
        public string DisplayName { get; }
        public Word StartAddress { get; }
        public Word EndAddress { get; }
        public Word Length => (Word)(EndAddress - StartAddress + 1);
        public bool IsMemoyModel => true;
        public bool IsCustomBookmark => false;
        internal MemoryModelLocation(MemoryModelLocationName name, Word address)
        {
            Name = name;
            DisplayName = name.ToString();
            StartAddress = address;
            EndAddress = address;
        }
        internal MemoryModelLocation(MemoryModelLocationName name, Word startAddress, Word endAddress)
        {
            if (startAddress > endAddress)
                throw new SystemException($"Negative range: {startAddress} - {endAddress}");
            Name = name;
            DisplayName = name.ToString();
            StartAddress = startAddress;
            EndAddress = endAddress;
        }
        public override string ToString() => Length > 1 ? $"{StartAddress:00000}-{EndAddress:00000} ({Length:0000}) : {DisplayName}" : $"{StartAddress:00000}       ({Length:0000}) : {DisplayName}";
        public bool HitTest(Word address) => address >= StartAddress && address <= EndAddress;
        public static Word operator +(MemoryModelLocation a, MemoryModelLocation b) => (Word)(a.StartAddress + b.StartAddress);
        public static Word operator +(MemoryModelLocation a, int b) => (Word)(a.StartAddress + b);
        public static implicit operator Word (MemoryModelLocation x) => x.StartAddress;
        public static implicit operator MemoryModelLocation(MemoryModelLocationName x) => List.GetLocation(x);
    }

    public class MemoryModelLocationList : List<MemoryModelLocation>
    {
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine("START-END   (LEN ) : NAME");
            s.AppendLine(new string('=', 78));
            ForEach(x => s.AppendLine(x.ToString()));
            return s.ToString();
        }
        public MemoryModelLocation GetLocation(string name)
            => this.FirstOrDefault(x => string.Compare(name, x.DisplayName, StringComparison.CurrentCultureIgnoreCase) == 0);
        public MemoryModelLocation GetLocation(MemoryModelLocationName name)
            => this.FirstOrDefault(x => name == x.Name);
        public MemoryModelLocation GetLocation(Word address)
            => this.FirstOrDefault(x => x.HitTest(address));
        public List<IMemoryLocation> GetAll(Word address) => this.Where(x => x.HitTest(address)).Cast<IMemoryLocation>().ToList();
        public List<IMemoryLocation> GetAll(string name) => this.Where(x => string.Compare(name, x.DisplayName, StringComparison.CurrentCultureIgnoreCase) == 0).Cast<IMemoryLocation>().ToList();
    }
}

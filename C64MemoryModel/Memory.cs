using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C64MemoryModel
{
    public class Memory
    {
        private Disassembler Disassembler { get; set; }
        public int BytePointer { get; set; }
        private byte[] Bytes { get; } = new byte[ushort.MaxValue];
        public MemoryModelLocationList Locations { get; } = new MemoryModelLocationList();
        public MemoryBookmarkList Bookmarks { get; } = new MemoryBookmarkList();
        public Memory()
        {
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ZeroPage, 0));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ProcessorPort, 1));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicAreaPointer, 43,44));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.KeyboardBuffer, 631, 640));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.DefaultBasicArea, 2049, 40959));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicRom, 40960, 49151));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BorderColor, 53280));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BackgroundColor, 53281));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ColdRestExecutionAddress, 65532, 65533));

            //Just for the fun of it. Shall remove.
            var rnd = new Random();
            for (int i = 0; i < 2000; i++)
                SetByte((byte)rnd.Next(256));
            SetBytePointer(0);
        }
        public void Load(string filename, out int startAddress, out int length)
        {
            var newBytes = File.ReadAllBytes(filename);
            var low = newBytes[0];
            var high = newBytes[1];
            startAddress = BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
            length = newBytes.Length - 2;
            SetBytePointer(startAddress);
            for (var i = 2; i < newBytes.Length; i++)
                SetByte(newBytes[i]);
        }
        public void Save(string filename, out int startAddress, out int length)
        {
            startAddress = 0;
            var end = ushort.MaxValue - 1;
            while (Bytes[startAddress] == 0)
                startAddress += 1;
            while (Bytes[end] == 0)
                end -= 1;
            length = (end - startAddress) + 1;
            var startBytes = BitConverter.GetBytes(startAddress);
            using (var sw = new FileStream(filename, FileMode.Create))
            {
                sw.Write(startBytes, 0, 2);
                sw.Write(Bytes, startAddress, length);
                sw.Flush();
                sw.Close();
            }
        }
        public void Clear()
        {
            for (var i = 0; i < Bytes.Length; i++)
                Bytes[i] = 0;
        }
        public string Visualize(ushort address) { SetBytePointer(address); return Visualize(); }
        public string Visualize()
        {
            var s = new StringBuilder();
            s.Append($". {BytePointer:00000} ${BytePointer:X4}");
            for (var i = 0; i < 16; i++)
                s.Append($" {GetByte():X2}");
            return s.ToString();
        }
        public void SetBytePointer(int address) { address = address < 0 ? ushort.MaxValue : address; address = address > ushort.MaxValue ? 0 : address; BytePointer = address; }
        public void SetBytePointer(IMemoryLocation l, ushort offset) => SetBytePointer(l.StartAddress + offset);
        private void IncreaseBytePointer() => IncreaseBytePointer(1);
        private void IncreaseBytePointer(int count) => SetBytePointer(BytePointer + count);
        public string GetDisassembly(bool withDescription = false)
        {
            if (Disassembler == null)
                Disassembler = new Disassembler();
            return Disassembler.GetDisassembly(this, withDescription);
        }
        public string GetDisassembly(int count, bool withDescription = false)
        {
            var s = new StringBuilder();
            if (count > 0)
                for (int i = 0; i < count; i++)
                    s.Append(GetDisassembly(withDescription));
            return s.ToString();
        }
        public byte PeekByte() => Bytes[BytePointer];
        public byte PeekByte(ushort address) => Bytes[address];
        public byte GetByte()
        {
            var ret = Bytes[BytePointer];
            IncreaseBytePointer();
            return ret;
        }
        public byte GetByte(ushort address)
        {
            SetBytePointer(address);
            var ret = Bytes[BytePointer];
            IncreaseBytePointer();
            return ret;
        }
        public byte GetByte(IMemoryLocation location, ushort offset)
        {
            SetBytePointer(location.StartAddress + offset);
            var ret = Bytes[BytePointer];
            IncreaseBytePointer();
            return ret;
        }
        public void SetByte(byte value)
        {
            Bytes[BytePointer] = value;
            IncreaseBytePointer();
        }
        public void SetByte(ushort address, byte value)
        {
            SetBytePointer(address);
            Bytes[BytePointer] = value;
            IncreaseBytePointer();
        }
        public void SetByte(IMemoryLocation location, ushort offset, byte value)
        {
            SetBytePointer(location.StartAddress + offset);
            Bytes[BytePointer] = value;
            IncreaseBytePointer();
        }
        public void SetBytes(IMemoryLocation location, params byte[] bytes)
        {
            if (bytes == null)
                return;
            if (bytes.Length <= 0)
                return;
            SetBytePointer(location.StartAddress);
            foreach (var t in bytes)
                SetByte(t);
        }
        public byte[] GetBytes(IMemoryLocation location)
        {
            var ret = new byte[location.Length];
            SetBytePointer(location.StartAddress);
            for (var i = 0; i < ret.Length; i++)
                ret[i] = GetByte();
            return ret;
        }
        public ushort GetWord()
        {
            var low = GetByte();
            var high = GetByte();
            return BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
        }
        public ushort GetWord(ushort address)
        {
            var low = GetByte(address);
            var high = GetByte();
            return BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
        }
        public void SetWord(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            SetByte(low);
            SetByte(high);
        }
        public void SetWord(ushort address, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            SetByte(address, low);
            SetByte(high);
        }
        public void AddBookmark(string name, ushort address) => Bookmarks.Add(new MemoryBookmark(name, address));
        public void AddBookmark(string name, ushort startAddress, ushort endAddress) => Bookmarks.Add(new MemoryBookmark(name, startAddress, endAddress));
        public MemoryModelLocation GetModelLocation(string name) => Locations.GetLocation(name);
        public MemoryModelLocation GetModelLocation(MemoryModelLocationName name) => Locations.GetLocation(name);
        public MemoryModelLocation GetModelLocation(ushort address) => Locations.GetLocation(address);
        public MemoryBookmark GetBookmark(string name) => Bookmarks.GetLocation(name);
        public MemoryBookmark GetBookmark(ushort address) => Bookmarks.GetLocation(address);
        public List<IMemoryLocation> GetLocations(ushort address)
        {
            var ret = new List<IMemoryLocation>();
            ret.AddRange(Bookmarks.GetAll(address));
            ret.AddRange(Locations.GetAll(address));
            return ret;
        }
        public List<IMemoryLocation> GetLocations(string name)
        {
            var ret = new List<IMemoryLocation>();
            ret.AddRange(Bookmarks.GetAll(name));
            ret.AddRange(Locations.GetAll(name));
            return ret;
        }
    }
}

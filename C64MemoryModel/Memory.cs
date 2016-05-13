using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using C64MemoryModel.Asm;
using C64MemoryModel.Chr;

namespace C64MemoryModel
{
    public class Memory
    {
        public Assembler Assembler { get; }
        private Disassembler Disassembler { get; set; }
        internal int BytePointer { get; set; }
        private byte[] Bytes { get; } = new byte[ushort.MaxValue];
        public static MemoryModelLocationList Locations { get; }
        public MemoryBookmarkList Bookmarks { get; } = new MemoryBookmarkList();
        public CharacterSetList CharacterSets { get; } = new CharacterSetList();
        static Memory()
        {
            Locations = new MemoryModelLocationList();
            MemoryModelLocation.List = Locations;
        }
        public Memory()
        {
            Assembler = new Assembler(this);
            CharacterSets.Add(new SimpleUppercaseCharacterSet());
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ZeroPage, 0));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ProcessorPort, 1));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicAreaPointer, 43,44));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.KeyboardBuffer, 631, 640));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.DefaultSpritePointerArea, 2040, 2047));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.DefaultBasicArea, 2049, 40959));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicRom, 40960, 49151));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.UpperRam, 49152, 53247));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.SpriteLocations, 53248, 53264));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.SpriteEnableRegister, 53269));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BorderColor, 53280));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BackgroundColor, 53281));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ColdRestExecutionAddress, 65532, 65533));
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
        public void Load(string filename)
        {
            int startAddress, length;
            Load(filename, out startAddress, out length);
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
        public void Save(string filename)
        {
            int startAddress, length;
            Save(filename, out startAddress, out length);
        }
        public void Clear()
        {
            for (var i = 0; i < Bytes.Length; i++)
                Bytes[i] = 0;
        }
        public string Visualize(Word address) { SetBytePointer(address); return Visualize(); }
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
        public Word GetBytePointer() => (Word)BytePointer;
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
        public byte PeekByte(Word address) => Bytes[address];
        public byte GetByte()
        {
            var ret = Bytes[BytePointer];
            IncreaseBytePointer();
            return ret;
        }
        public byte GetByte(Word address)
        {
            SetBytePointer(address);
            var ret = Bytes[BytePointer];
            IncreaseBytePointer();
            return ret;
        }
        public byte GetByte(IMemoryLocation location, Word offset)
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
        public void SetByte(Word address, byte value)
        {
            SetBytePointer(address);
            Bytes[BytePointer] = value;
            IncreaseBytePointer();
        }
        public void SetByte(IMemoryLocation location, Word offset, byte value)
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
        public Word GetWord()
        {
            var low = GetByte();
            var high = GetByte();
            return BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
        }
        public Word GetWord(Word address)
        {
            var low = GetByte(address);
            var high = GetByte();
            return BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
        }
        public void SetWord(Word value)
        {
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            SetByte(low);
            SetByte(high);
        }
        public void SetWord(Word address, Word value)
        {
            var bytes = BitConverter.GetBytes(value);
            var low = bytes[0];
            var high = bytes[1];
            SetByte(address, low);
            SetByte(high);
        }
        public void SetBits(Word address, BitValue b7, BitValue b6, BitValue b5, BitValue b4, BitValue b3, BitValue b2, BitValue b1, BitValue b0)
        {
            SetBytePointer(address);
            SetBits(b7, b6, b5, b4, b3, b2, b1, b0);
        }
        public void SetBits(IMemoryLocation location, BitValue b7, BitValue b6, BitValue b5, BitValue b4, BitValue b3, BitValue b2, BitValue b1, BitValue b0)
        {
            SetBytePointer(location.StartAddress);
            SetBits(b7, b6, b5, b4, b3, b2, b1, b0);
        }
        public void SetBits(BitValue b7, BitValue b6, BitValue b5, BitValue b4, BitValue b3, BitValue b2, BitValue b1, BitValue b0)
        {
            var adr = GetBytePointer();
            var b = new Types.Byte(GetByte(adr));
            b.Modify(b7, b6, b5, b4, b3, b2, b1, b0);
            SetByte(adr, b.ToByte());
        }
        public Types.Byte GetBits(Word address) => new Types.Byte(GetByte(address));
        public Types.Byte GetBits(IMemoryLocation location) => new Types.Byte(GetByte(location.StartAddress));
        public Types.Byte GetBits() => new Types.Byte(GetByte());
        public void SetString(CharacterSetBase characterSet, string text)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (text == null || text.Length <= 0)
                return;
            var bytes = characterSet.TranslateString(text);
            foreach (var b in bytes)
                SetByte(b);
        }
        public void SetString(string characterSet, string text)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            SetString(set, text);
        }
        public void SetString(Word address, CharacterSetBase characterSet, string text)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (text == null || text.Length <= 0)
                return;
            SetBytePointer(address);
            var bytes = characterSet.TranslateString(text);
            foreach (var b in bytes)
                SetByte(b);
        }
        public void SetString(Word address, string characterSet, string text)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            SetString(address, set, text);
        }
        public string GetString(CharacterSetBase characterSet, int length)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (length <= 0)
                throw new SystemException("Length must be > 0.");
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
                bytes[i] = GetByte();
            return characterSet.TranslateString(bytes);
        }
        public string GetString(string characterSet, int length)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            return GetString(set, length);
        }
        public string GetString(Word address, CharacterSetBase characterSet, int length)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (length <= 0)
                throw new SystemException("Length must be > 0.");
            SetBytePointer(address);
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
                bytes[i] = GetByte();
            return characterSet.TranslateString(bytes);
        }
        public string GetString(Word address, string characterSet, int length)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            return GetString(address, set, length);
        }
        public void AddBookmark(string name, Word address) => Bookmarks.Add(new MemoryBookmark(name, address));
        public void AddBookmark(string name, Word startAddress, Word endAddress) => Bookmarks.Add(new MemoryBookmark(name, startAddress, endAddress));
        public MemoryModelLocation GetModelLocation(string name) => Locations.GetLocation(name);
        public MemoryModelLocation GetModelLocation(MemoryModelLocationName name) => Locations.GetLocation(name);
        public MemoryModelLocation GetModelLocation(Word address) => Locations.GetLocation(address);
        public MemoryBookmark GetBookmark(string name) => Bookmarks.GetLocation(name);
        public MemoryBookmark GetBookmark(Word address) => Bookmarks.GetLocation(address);
        public List<IMemoryLocation> GetLocations(Word address)
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

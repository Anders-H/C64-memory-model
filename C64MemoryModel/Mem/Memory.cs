﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using C64MemoryModel.Asm;
using C64MemoryModel.Chr;
using C64MemoryModel.Disasm;
using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class Memory
    {
        private Disassembler Disassembler { get; set; }

        private byte[] Bytes { get; } = new byte[ushort.MaxValue + 1];

        internal Address BytePointer { get; } = new Address(0);

        public Assembler Assembler { get; }

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
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ZeroPage, (Address)0));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ProcessorPort, (Address)1));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.Unused, (Address)2, (Address)6));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.CurrentExpressionType, (Address)13));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.CurrentNumericalExpressionType, (Address)14));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicAreaPointer, (Address)43, (Address)44));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.KeyboardBuffer, (Address)631, (Address)640));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.DefaultSpritePointerArea, (Address)2040, (Address)2047));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.DefaultBasicArea, (Address)2049, (Address)40959));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BasicRom, (Address)40960, (Address)49151));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.UpperRam, (Address)49152, (Address)53247));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.SpriteLocations, (Address)53248, (Address)53264));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.SpriteEnableRegister, (Address)53269));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BorderColor, (Address)53280));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.BackgroundColor, (Address)53281));
            Locations.Add(new MemoryModelLocation(MemoryModelLocationName.ColdRestExecutionAddress, (Address)65532, (Address)65533));
        }

        public void Load(string filename, out Address startAddress, out ushort length)
        {
            var newBytes = File.ReadAllBytes(filename);
            var low = newBytes[0];
            var high = newBytes[1];
            startAddress = (Address)BitConverter.ToUInt16(new byte[] { low, high, 0, 0 }, 0);
            length = (ushort)(newBytes.Length - 2);
            SetBytePointer(startAddress);
            for (var i = 2; i < newBytes.Length; i++)
                SetByte(newBytes[i]);
        }

        public void Load(string filename) =>
            Load(filename, out _, out _);

        public void Save(string filename, out Address startAddress, out Word length)
        {
            startAddress = (Address)0;
            var end = ushort.MaxValue - 1;
            while (Bytes[startAddress.Value] == 0)
                startAddress += 1;
            while (Bytes[end] == 0)
                end -= 1;
            length = (Word)(end - startAddress.Value + 1);
            var startBytes = BitConverter.GetBytes(startAddress.Value);
            using (var sw = new FileStream(filename, FileMode.Create))
            {
                sw.Write(startBytes, 0, 2);
                sw.Write(Bytes, startAddress.Value, length.Value);
                sw.Flush();
                sw.Close();
            }
        }

        public void Save(string filename) =>
            Save(filename, out _, out _);

        public void SaveRange(string filename, IMemoryLocation addressRange)
        {
            var startBytes = BitConverter.GetBytes(addressRange.StartAddress.Value);
            using (var sw = new FileStream(filename, FileMode.Create))
            {
                sw.Write(startBytes, 0, 2);
                sw.Write(Bytes, addressRange.StartAddress.Value, addressRange.Length);
                sw.Flush();
                sw.Close();
            }
        }

        public void Clear()
        {
            for (var i = 0; i < Bytes.Length; i++)
                Bytes[i] = 0;
        }

        public string Visualize(Address address)
        {
            SetBytePointer(address); return Visualize();
        }

        public string Visualize()
        {
            var s = new StringBuilder();
            s.Append($". {BytePointer:00000} ${BytePointer:X4}");
            for (var i = 0; i < 16; i++)
                s.Append($" {GetByte():X2}");
            return s.ToString();
        }

        public void SetBytePointer(Address address) =>
            BytePointer.FromInt(address.Value);

        public void SetBytePointer(ushort address) =>
            BytePointer.FromInt(address);

        public void SetBytePointer(IMemoryLocation l, ushort offset) =>
            SetBytePointer(l.StartAddress + offset);

        public Address GetBytePointer() =>
            BytePointer;

        private void IncreaseBytePointer() =>
            IncreaseBytePointer(1);

        private void IncreaseBytePointer(int count) =>
            SetBytePointer(BytePointer + count);

        public string GetDisassembly(bool withDescription = false)
        {
            if (Disassembler == null)
                Disassembler = new Disassembler();
            return Disassembler.GetDisassembly(this, withDescription);
        }

        public string GetDisassembly(int count, bool withDescription = false)
        {
            var s = new StringBuilder();
            if (count <= 0)
                return s.ToString();
            for (var i = 0; i < count; i++)
                s.Append(GetDisassembly(withDescription));
            return s.ToString();
        }

        public byte PeekByte() =>
            Bytes[BytePointer.Value];

        public byte PeekByte(Address address) =>
            Bytes[address.Value];

        public byte GetByte()
        {
            var ret = Bytes[BytePointer.Value];
            IncreaseBytePointer();
            return ret;
        }

        public byte GetByte(ushort address)
        {
            SetBytePointer(address);
            var ret = Bytes[BytePointer.Value];
            IncreaseBytePointer();
            return ret;
        }

        public byte GetByte(Address address)
        {
            SetBytePointer(address);
            var ret = Bytes[BytePointer.Value];
            IncreaseBytePointer();
            return ret;
        }

        public byte GetByte(IMemoryLocation location, ushort offset)
        {
            SetBytePointer(location.StartAddress + offset);
            var ret = Bytes[BytePointer.Value];
            IncreaseBytePointer();
            return ret;
        }

        public void SetByte(byte value)
        {
            Bytes[BytePointer.Value] = value;
            IncreaseBytePointer();
        }

        public void SetByte(Address address, byte value)
        {
            SetBytePointer(address);
            Bytes[BytePointer.Value] = value;
            IncreaseBytePointer();
        }

        public void SetByte(IMemoryLocation location, ushort offset, byte value)
        {
            SetBytePointer(location.StartAddress + offset);
            Bytes[BytePointer.Value] = value;
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

        public ushort GetWord(Address address)
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

        public void SetWord(Address address, Word value)
        {
            var bytes = BitConverter.GetBytes(value.Value);
            var low = bytes[0];
            var high = bytes[1];
            SetByte(address, low);
            SetByte(high);
        }

        public void SetBits(Address address, BitValue b7, BitValue b6, BitValue b5, BitValue b4, BitValue b3, BitValue b2, BitValue b1, BitValue b0)
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
            SetByte(adr, b.Value);
        }

        public Types.Byte GetBits(Address address) =>
            new Types.Byte(GetByte(address));

        public Types.Byte GetBits(IMemoryLocation location) =>
            new Types.Byte(GetByte(location.StartAddress));

        public Types.Byte GetBits() =>
            new Types.Byte(GetByte());

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

        public void SetString(Address address, CharacterSetBase characterSet, string text)
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

        public void SetString(Address address, string characterSet, string text)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            SetString(address, set, text);
        }

        public string GetString(CharacterSetBase characterSet, Word length)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (length <= 0)
                throw new SystemException("Length must be > 0.");
            var bytes = new byte[length.Value];
            for (var i = 0; i < length.Value; i++)
                bytes[i] = GetByte();
            return characterSet.TranslateString(bytes);
        }

        public string GetString(string characterSet, Word length)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            return GetString(set, length);
        }

        public string GetString(Address address, CharacterSetBase characterSet, Word length)
        {
            if (characterSet == null)
                throw new SystemException("Character set must not be null.");
            if (length <= 0)
                throw new SystemException("Length must be > 0.");
            SetBytePointer(address);
            var bytes = new byte[length.Value];
            for (var i = 0; i < length.Value; i++)
                bytes[i] = GetByte();
            return characterSet.TranslateString(bytes);
        }

        public string GetString(Address address, string characterSet, Word length)
        {
            var set = CharacterSets.GetCharacterSet(characterSet);
            if (set == null)
                throw new SystemException($"Character set {characterSet} not found.");
            return GetString(address, set, length);
        }

        public void AddBookmark(string name, Address address) =>
            Bookmarks.Add(new MemoryBookmark(name, address));

        public void AddBookmark(string name, Address startAddress, Address endAddress) =>
            Bookmarks.Add(new MemoryBookmark(name, startAddress, endAddress));

        public MemoryModelLocation GetModelLocation(string name) =>
            Locations.GetLocation(name);

        public MemoryModelLocation GetModelLocation(MemoryModelLocationName name) =>
            Locations.GetLocation(name);

        public MemoryModelLocation GetModelLocation(Address address) =>
            Locations.GetLocation(address);

        public MemoryBookmark GetBookmark(string name) =>
            Bookmarks.GetLocation(name);

        public MemoryBookmark GetBookmark(Address address) =>
            Bookmarks.GetLocation(address);

        public List<IMemoryLocation> GetLocations(Address address)
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
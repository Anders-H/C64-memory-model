using System;

namespace C64MemoryModel.Types
{
    public class Word
    {
        public Byte HighByte { get; set; }
        public Byte LowByte { get; set; }
        public Word(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            HighByte = new Byte(bytes[1]);
            LowByte = new Byte(bytes[0]);
        }
        public Word(byte highByte, byte lowByte)
        {
            HighByte = new Byte(highByte);
            LowByte = new Byte(lowByte);
        }
        public Word(Byte highByte, Byte lowByte)
        {
            HighByte = new Byte(highByte.ToByte());
            LowByte = new Byte(lowByte.ToByte());
        }
        public Word(bool b15, bool b14, bool b13, bool b12, bool b11, bool b10, bool b9, bool b8, bool b7, bool b6, bool b5, bool b4, bool b3, bool b2, bool b1, bool b0)
        {
            HighByte = new Byte(b15, b14, b13, b12, b11, b10, b9, b8);
            LowByte = new Byte(b7, b6, b5, b4, b3, b2, b1, b0);
        }
        public override string ToString() => $"{HighByte}{LowByte}";
        public ushort ToUshort() => BitConverter.ToUInt16(new [] {LowByte.ToByte(), HighByte.ToByte() }, 0);
        public static implicit operator Word(ushort x) => new Word(x);
        public static bool operator ==(Word a, Word b) => a.ToUshort() == b.ToUshort();
        public static bool operator !=(Word a, Word b) => a.ToUshort() != b.ToUshort();
        public static bool operator >(Word a, Word b) => a.ToUshort() > b.ToUshort();
        public static bool operator <(Word a, Word b) => a.ToUshort() < b.ToUshort();
    }
}

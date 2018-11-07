using System;

namespace C64MemoryModel.Types
{
    public class Word
    {
        private string _toString;
        private string _toHexString;

        public ushort Value { get; }
        public Byte HighByte { get; }
        public Byte LowByte { get; }

        public Word(ushort value)
        {
            Value = value;
            var bytes = BitConverter.GetBytes(value);
            HighByte = new Byte(bytes[1]);
            LowByte = new Byte(bytes[0]);
        }

        public Word(byte highByte, byte lowByte)
        {
            HighByte = new Byte(highByte);
            LowByte = new Byte(lowByte);
            Value = ToUshort();
        }

        public Word(Byte highByte, Byte lowByte)
        {
            HighByte = new Byte(highByte.ToByte());
            LowByte = new Byte(lowByte.ToByte());
            Value = ToUshort();
        }

        public Word(bool b15, bool b14, bool b13, bool b12, bool b11, bool b10, bool b9, bool b8, bool b7, bool b6, bool b5, bool b4, bool b3, bool b2, bool b1, bool b0)
        {
            HighByte = new Byte(b15, b14, b13, b12, b11, b10, b9, b8);
            LowByte = new Byte(b7, b6, b5, b4, b3, b2, b1, b0);
        }

        public ushort AsUshort() =>
            Value;

        public string ToBinString() =>
            $"{HighByte}{LowByte}";

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(_toString))
                return _toString;
            _toString = Value.ToString("00000");
            return _toString;
        }

        public virtual string ToHexString()
        {
            if (!string.IsNullOrWhiteSpace(_toHexString))
                return _toHexString;
            var x = Value.ToString("X");
            while (x.Length < 4)
                x = $"0{x}";
            _toHexString = $"${x}".ToUpper();
            return _toHexString;
        }

        private ushort ToUshort() =>
            BitConverter.ToUInt16(new [] {LowByte.ToByte(), HighByte.ToByte() }, 0);

        public static explicit operator Word(int x) =>
            new Word((ushort)x);

        public static Word operator +(Word x, int y) =>
            new Word((ushort)(x.Value + y));

        public static bool operator >(Word x, Word y) =>
            x.Value > y.Value;

        public static bool operator <(Word x, Word y) =>
            x.Value < y.Value;

        public static bool operator >=(Word x, Word y) =>
            x.Value >= y.Value;

        public static bool operator <=(Word x, Word y) =>
            x.Value <= y.Value;

        public static bool operator >=(Word x, int y) =>
            x.Value >= y;

        public static bool operator <=(Word x, int y) =>
            x.Value <= y;
    }
}
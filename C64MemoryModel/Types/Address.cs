namespace C64MemoryModel.Types
{
    public class Address : Word
    {
        public Address(ushort value) : base(value)
        {
        }

        public Address(byte highByte, byte lowByte) : base(highByte, lowByte)
        {
        }

        public Address(Byte highByte, Byte lowByte) : base(highByte, lowByte)
        {
        }

        public Address(bool b15, bool b14, bool b13, bool b12, bool b11, bool b10, bool b9, bool b8, bool b7, bool b6, bool b5, bool b4, bool b3, bool b2, bool b1, bool b0) : base(b15, b14, b13, b12, b11, b10, b9, b8, b7, b6, b5, b4, b3, b2, b1, b0)
        {
        }

        public static Address MinValue => new Address(0);

        public static Address MaxValue => new Address(ushort.MaxValue);

        public static explicit operator Address(int x) =>
            new Address((ushort)x);

        public static explicit operator int(Address x) =>
            x.Value;

        public static bool operator >(Address x, Address y) =>
            x.Value > y.Value;

        public static bool operator <(Address x, Address y) =>
            x.Value < y.Value;

        public static bool operator >=(Address x, Address y) =>
            x.Value >= y.Value;

        public static bool operator <=(Address x, Address y) =>
            x.Value <= y.Value;

        public static Address operator -(Address x, Address y) =>
            (Address)(x.Value - y.Value);

        public static Address operator +(Address x, Address y) =>
            (Address)(x.Value + y.Value);

        public static Address operator +(Address x, int y) =>
            (Address)(x.Value + y);
    }
}
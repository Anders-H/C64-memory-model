namespace C64MemoryModel.Types
{
    public class Byte
    {
        public bool Bit7 { get; set; }
        public bool Bit6 { get; set; }
        public bool Bit5 { get; set; }
        public bool Bit4 { get; set; }
        public bool Bit3 { get; set; }
        public bool Bit2 { get; set; }
        public bool Bit1 { get; set; }
        public bool Bit0 { get; set; }
        public Byte(byte b)
        {
            Bit7 = (b & 128) == 128;
            Bit6 = (b & 64) == 64;
            Bit5 = (b & 32) == 32;
            Bit4 = (b & 16) == 16;
            Bit3 = (b & 8) == 8;
            Bit2 = (b & 4) == 4;
            Bit1 = (b & 2) == 2;
            Bit0 = (b & 1) == 1;
        }
        public Byte(bool b7, bool b6, bool b5, bool b4, bool b3, bool b2, bool b1, bool b0)
        {
            Bit7 = b7;
            Bit6 = b6;
            Bit5 = b5;
            Bit4 = b4;
            Bit3 = b3;
            Bit2 = b2;
            Bit1 = b1;
            Bit0 = b0;
        }
        public void Modify(BitValue b7, BitValue b6, BitValue b5, BitValue b4, BitValue b3, BitValue b2, BitValue b1, BitValue b0)
        {
            Bit7 = (b7 == BitValue.Set || b7 != BitValue.NotSet && Bit7);
            Bit6 = (b6 == BitValue.Set || b6 != BitValue.NotSet && Bit6);
            Bit5 = (b5 == BitValue.Set || b5 != BitValue.NotSet && Bit5);
            Bit4 = (b4 == BitValue.Set || b4 != BitValue.NotSet && Bit4);
            Bit3 = (b3 == BitValue.Set || b3 != BitValue.NotSet && Bit3);
            Bit2 = (b2 == BitValue.Set || b2 != BitValue.NotSet && Bit2);
            Bit1 = (b1 == BitValue.Set || b1 != BitValue.NotSet && Bit1);
            Bit0 = (b0 == BitValue.Set || b0 != BitValue.NotSet && Bit0);
        }
        public override string ToString() => $"{HighNibbleString()}{LowNibbleString()}";
        public byte ToByte()
        {
            var b = Bit7 ? 128 : 0;
            b += Bit6 ? 64 : 0;
            b += Bit5 ? 32 : 0;
            b += Bit4 ? 16 : 0;
            b += Bit3 ? 8 : 0;
            b += Bit2 ? 4 : 0;
            b += Bit1 ? 2 : 0;
            b += Bit0 ? 1 : 0;
            return (byte)b;
        }
        private static char To01(bool b) => b ? '1' : '0';
        public string HighNibbleString() => $"{To01(Bit7)}{To01(Bit6)}{To01(Bit5)}{To01(Bit4)}";
        public string LowNibbleString() => $"{To01(Bit3)}{To01(Bit2)}{To01(Bit1)}{To01(Bit0)}";
    }
}

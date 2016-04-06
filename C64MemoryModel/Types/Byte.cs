using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Bit7 = (b7 == BitValue.Set ? true : (b7 == BitValue.NotSet ? false : Bit7));
            Bit6 = (b6 == BitValue.Set ? true : (b6 == BitValue.NotSet ? false : Bit6));
            Bit5 = (b5 == BitValue.Set ? true : (b5 == BitValue.NotSet ? false : Bit5));
            Bit4 = (b4 == BitValue.Set ? true : (b4 == BitValue.NotSet ? false : Bit4));
            Bit3 = (b3 == BitValue.Set ? true : (b3 == BitValue.NotSet ? false : Bit3));
            Bit2 = (b2 == BitValue.Set ? true : (b2 == BitValue.NotSet ? false : Bit2));
            Bit1 = (b1 == BitValue.Set ? true : (b1 == BitValue.NotSet ? false : Bit1));
            Bit0 = (b0 == BitValue.Set ? true : (b0 == BitValue.NotSet ? false : Bit0));
        }
        public override string ToString() => $"{(Bit7 ? "1" : "0")}{(Bit6 ? "1" : "0")}{(Bit5 ? "1" : "0")}{(Bit4 ? "1" : "0")}{(Bit3 ? "1" : "0")}{(Bit2 ? "1" : "0")}{(Bit1 ? "1" : "0")}{(Bit0 ? "1" : "0")}";
        public byte ToByte()
        {
            var b = (Bit7 ? 128 : 0);
            b += (Bit6 ? 64 : 0);
            b += (Bit5 ? 32 : 0);
            b += (Bit4 ? 16 : 0);
            b += (Bit3 ? 8 : 0);
            b += (Bit2 ? 4 : 0);
            b += (Bit1 ? 2 : 0);
            b += (Bit0 ? 1 : 0);
            return (byte)b;
        }
    }
}

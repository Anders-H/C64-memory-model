using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace C64MemoryModel.Types
{
    public class Byte : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _bit7;
        private bool _bit6;
        private bool _bit5;
        private bool _bit4;
        private bool _bit3;
        private bool _bit2;
        private bool _bit1;
        private bool _bit0;

        public string Name { get; set; }

        public bool Bit7
        {
            get => _bit7;
            set
            {
                if (_bit7 == value)
                    return;
                _bit7 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit7" : Name);
            }
        }

        public bool Bit6
        {
            get => _bit6;
            set
            {
                if (_bit6 == value)
                    return;
                _bit6 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit6" : Name);
            }
        }

        public bool Bit5
        {
            get => _bit5;
            set
            {
                if (_bit5 == value)
                    return;
                _bit5 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit5" : Name);
            }
        }

        public bool Bit4
        {
            get => _bit4;
            set
            {
                if (_bit4 == value)
                    return;
                _bit4 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit4" : Name);
            }
        }

        public bool Bit3
        {
            get => _bit3;
            set
            {
                if (_bit3 == value)
                    return;
                _bit3 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit3" : Name);
            }
        }


        public bool Bit2
        {
            get => _bit2;
            set
            {
                if (_bit2 == value)
                    return;
                _bit2 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit2" : Name);
            }
        }

        public bool Bit1
        {
            get => _bit1;
            set
            {
                if (_bit1 == value)
                    return;
                _bit1 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit1" : Name);
            }
        }

        public bool Bit0
        {
            get => _bit0;
            set
            {
                if (_bit0 == value)
                    return;
                _bit0 = value;
                OnPropertyChanged(string.IsNullOrWhiteSpace(Name) ? "Bit0" : Name);
            }
        }

        public Byte(string bytestring)
        {
            if (bytestring.Length != 8)
                throw new ArgumentException("Expected a 8 character string, like \"00010011\".");
            Bit7 = bytestring[7] == '1' ? true : (bytestring[7] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit6 = bytestring[6] == '1' ? true : (bytestring[6] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit5 = bytestring[5] == '1' ? true : (bytestring[5] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit4 = bytestring[4] == '1' ? true : (bytestring[4] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit3 = bytestring[3] == '1' ? true : (bytestring[3] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit2 = bytestring[2] == '1' ? true : (bytestring[2] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit1 = bytestring[1] == '1' ? true : (bytestring[1] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit0 = bytestring[0] == '1' ? true : (bytestring[0] == '0' ? false : throw new Exception("Expected 0 or 1."));
        }

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

        public override string ToString() =>
            $"{HighNibbleString()}{LowNibbleString()}";

        public string ToDecString() =>
            Value.ToString("000");

        public byte Value
        {
            get
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
        }

        private static char To01(bool b) =>
            b ? '1' : '0';

        public string HighNibbleString() =>
            $"{To01(Bit7)}{To01(Bit6)}{To01(Bit5)}{To01(Bit4)}";

        public string LowNibbleString() =>
            $"{To01(Bit3)}{To01(Bit2)}{To01(Bit1)}{To01(Bit0)}";

        protected virtual void OnPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Nibble GetLowNibble() =>
            new Nibble(Bit3, Bit2, Bit1, Bit0);
        
        public Nibble GetHighNibble() =>
            new Nibble(Bit7, Bit6, Bit5, Bit4);

    }
}
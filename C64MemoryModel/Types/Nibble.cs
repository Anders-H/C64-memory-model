using System;
using System.ComponentModel;

namespace C64MemoryModel.Types
{
    public class Nibble
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private bool _bit3;
        private bool _bit2;
        private bool _bit1;
        private bool _bit0;
        
        public string Name { get; set; }
        
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
        
        public Nibble(string bytestring)
        {
            if (bytestring.Length != 4)
                throw new ArgumentException("Expected a 4 character string, like \"0011\".");
            Bit3 = bytestring[3] == '1' ? true : (bytestring[3] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit2 = bytestring[2] == '1' ? true : (bytestring[2] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit1 = bytestring[1] == '1' ? true : (bytestring[1] == '0' ? false : throw new Exception("Expected 0 or 1."));
            Bit0 = bytestring[0] == '1' ? true : (bytestring[0] == '0' ? false : throw new Exception("Expected 0 or 1."));
        }
        
        public Nibble(byte b, bool high)
        {
            if (high)
            {
                Bit3 = (b & 128) == 128;
                Bit2 = (b & 64) == 64;
                Bit1 = (b & 32) == 32;
                Bit0 = (b & 16) == 16;
            }
            else
            {
                Bit3 = (b & 8) == 8;
                Bit2 = (b & 4) == 4;
                Bit1 = (b & 2) == 2;
                Bit0 = (b & 1) == 1;
            }
        }

        public Nibble(bool b3, bool b2, bool b1, bool b0)
        {
            Bit3 = b3;
            Bit2 = b2;
            Bit1 = b1;
            Bit0 = b0;
        }

        public void Modify(BitValue b3, BitValue b2, BitValue b1, BitValue b0)
        {
            Bit3 = (b3 == BitValue.Set || b3 != BitValue.NotSet && Bit3);
            Bit2 = (b2 == BitValue.Set || b2 != BitValue.NotSet && Bit2);
            Bit1 = (b1 == BitValue.Set || b1 != BitValue.NotSet && Bit1);
            Bit0 = (b0 == BitValue.Set || b0 != BitValue.NotSet && Bit0);
        }

        public string ToDecString() =>
            Value.ToString("000");

        public byte Value
        {
            get
            {
                var b = Bit3 ? 8 : 0;
                b += Bit2 ? 4 : 0;
                b += Bit1 ? 2 : 0;
                b += Bit0 ? 1 : 0;
                return (byte)b;
            }
        }

        public byte HighValue
        {
            get
            {
                var b = Bit3 ? 128 : 0;
                b += Bit2 ? 64 : 0;
                b += Bit1 ? 32 : 0;
                b += Bit0 ? 16 : 0;
                return (byte)b;
            }
        }

        public bool IsSet(bool b3, bool b2, bool b1, bool b0)
        {
            if (b3 && !Bit3)
                return false;
            if (b2 && !Bit2)
                return false;
            if (b1 && !Bit1)
                return false;
            if (b0 && !Bit0)
                return false;
            return true;
        }

        public bool IsNotSet(bool b3, bool b2, bool b1, bool b0)
        {
            if (b3 && Bit3)
                return false;
            if (b2 && Bit2)
                return false;
            if (b1 && Bit1)
                return false;
            if (b0 && Bit0)
                return false;
            return true;
        }

        private static char To01(bool b) =>
            b ? '1' : '0';
        
        public override string ToString() =>
            $"{To01(Bit3)}{To01(Bit2)}{To01(Bit1)}{To01(Bit0)}";

        protected virtual void OnPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
using System;

namespace C64MemoryModel.Types
{
	public class Word : IDisposable
	{
		private string _toString;
		private string _toHexString;

		public ushort Value { get; private set; }
		public Byte HighByte { get; }
		public Byte LowByte { get; }

		public Word(ushort value)
		{
			Value = value;
			var bytes = BitConverter.GetBytes(value);
			HighByte = new Byte(bytes[1]);
			LowByte = new Byte(bytes[0]);
			BindEvents();
		}

		public Word(byte highByte, byte lowByte)
		{
			HighByte = new Byte(highByte);
			LowByte = new Byte(lowByte);
			Value = ToUshort();
			BindEvents();
		}

		public Word(Byte highByte, Byte lowByte)
		{
			HighByte = new Byte(highByte.Value);
			LowByte = new Byte(lowByte.Value);
			Value = ToUshort();
			BindEvents();
		}

		public Word(bool b15, bool b14, bool b13, bool b12, bool b11, bool b10, bool b9, bool b8, bool b7, bool b6, bool b5, bool b4, bool b3, bool b2, bool b1, bool b0)
		{
			HighByte = new Byte(b15, b14, b13, b12, b11, b10, b9, b8);
			LowByte = new Byte(b7, b6, b5, b4, b3, b2, b1, b0);
			BindEvents();
		}

		private void BindEvents()
		{
			HighByte.Name = nameof(HighByte);
			LowByte.Name = nameof(LowByte);
			HighByte.PropertyChanged += ByteChanged;
			LowByte.PropertyChanged += ByteChanged;
		}

		public void FromInt(int i)
		{
			var value = (ushort)i;
			Value = value;
			var bytes = BitConverter.GetBytes(value);
			var b = bytes[1];
			HighByte.Bit7 = (b & 128) == 128;
			HighByte.Bit6 = (b & 64) == 64;
			HighByte.Bit5 = (b & 32) == 32;
			HighByte.Bit4 = (b & 16) == 16;
			HighByte.Bit3 = (b & 8) == 8;
			HighByte.Bit2 = (b & 4) == 4;
			HighByte.Bit1 = (b & 2) == 2;
			HighByte.Bit0 = (b & 1) == 1;
			b = bytes[0];
			LowByte.Bit7 = (b & 128) == 128;
			LowByte.Bit6 = (b & 64) == 64;
			LowByte.Bit5 = (b & 32) == 32;
			LowByte.Bit4 = (b & 16) == 16;
			LowByte.Bit3 = (b & 8) == 8;
			LowByte.Bit2 = (b & 4) == 4;
			LowByte.Bit1 = (b & 2) == 2;
			LowByte.Bit0 = (b & 1) == 1;
		}

		public bool CanInc() =>
			CanInc(1);

		public bool CanInc(int amt)
		{
			var current = (int)Value;
			return current + amt <= ushort.MaxValue;
		}

		public void Inc() =>
			FromInt(Value + 1);

		public void Inc(int amt) =>
			FromInt(Value + amt);

		public bool CanDec() =>
			CanDec(1);

		public bool CanDec(int amt)
		{
			var current = (int)Value;
			return current - amt >= ushort.MinValue;
		}

		public void Dec() =>
			FromInt(Value - 1);

		public void Dec(int amt) =>
			FromInt(Value - amt);

		private void ByteChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
			Value = ToUshort();

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
			BitConverter.ToUInt16(new[] { LowByte.Value, HighByte.Value }, 0);

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

		public void Dispose()
		{
			HighByte.PropertyChanged -= ByteChanged;
			LowByte.PropertyChanged -= ByteChanged;
		}

	}
}
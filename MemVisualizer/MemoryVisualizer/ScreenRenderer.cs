using C64MemoryModel.Mem;

namespace MemoryVisualizer
{
	public interface IScreenRenderer
	{
		int Render(ref int displayPointer, Memory memory);
	}

	public abstract class ScreenRenderer : IScreenRenderer
	{
		public int RowCount { get; }
		public ScreenCharacterMap Characters { get; }

		protected ScreenRenderer(int rowCount, ScreenCharacterMap characters)
		{
			RowCount = rowCount;
			Characters = characters;
		}

		public abstract int Render(ref int displayPointer, Memory memory);
	}

	public class HexRawScreenRenderer : ScreenRenderer
	{
		public HexRawScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
		{
		}

		public override int Render(ref int displayPointer, Memory memory)
		{
			for (var row = 0; row < RowCount; row++)
			{
				if (displayPointer > ushort.MaxValue)
					break;
				Characters.SetCharacters(0, row, displayPointer.ToString("00000"));
				Characters.SetCharacters(6, row, "$" + displayPointer.ToString("X0000"));
				var x = 12;
				for (var col = 0; col < 8; col++)
				{
					if (displayPointer > ushort.MaxValue)
						break;
					Characters.SetCharacters(x, row, memory.GetByte((ushort)displayPointer).ToString("X2"));
					x += 3;
					displayPointer++;
				}
			}
			return RowCount * 8;
		}
	}

	public class DecRawScreenRenderer : ScreenRenderer
	{
		public DecRawScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
		{
		}

		public override int Render(ref int displayPointer, Memory memory)
		{
			for (var row = 0; row < RowCount; row++)
			{
				if (displayPointer > ushort.MaxValue)
					break;
				Characters.SetCharacters(0, row, displayPointer.ToString("00000"));
				Characters.SetCharacters(6, row, "$" + displayPointer.ToString("X0000"));
				var x = 12;
				for (var col = 0; col < 4; col++)
				{
					if (displayPointer > ushort.MaxValue)
						break;
					Characters.SetCharacters(x, row, memory.GetByte((ushort)displayPointer).ToString("000"));
					x += 4;
					displayPointer++;
				}
			}
			return RowCount * 4;
		}
	}

	public class DisassemblyScreenRenderer : ScreenRenderer
	{
		public DisassemblyScreenRenderer(int rowCount, ScreenCharacterMap characters) : base(rowCount, characters)
		{
		}

		public override int Render(ref int displayPointer, Memory memory)
		{
			memory.SetBytePointer((ushort)displayPointer);
			var disassembly = memory.GetDisassembly(25);
			var disassemblyRows = System.Text.RegularExpressions.Regex.Split(disassembly, @"\n");
			for (var row = 0; row < disassemblyRows.Length; row++)
				Characters.SetCharacters(0, row, disassemblyRows[row]);
			return memory.GetBytePointer().Value - displayPointer;
		}
	}
}
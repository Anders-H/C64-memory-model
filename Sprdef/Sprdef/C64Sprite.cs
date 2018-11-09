using System.Drawing;
using System.IO;
using C64MemoryModel.Graphics;

namespace Sprdef
{
	public class C64Sprite : IScreenThing
	{
		private bool[,] SpriteData { get; }
		private Bitmap SpritePreviewData { get; }
		public static C64Palette Palette = new C64Palette();
		public static int BackgroundColorIndex { get; set; }
		public static int ForegroundColorIndex { get; set; }
		public static int ExtraColor1Index { get; set; }
		public static int ExtraColor2Index { get; set; }
		public Rectangle Bounds { get; private set; }
		public const int Width = 24;
		public const int Height = 21;

		static C64Sprite()
		{
			BackgroundColorIndex = 0;
			ForegroundColorIndex = 1;
			ExtraColor1Index = 2;
			ExtraColor2Index = 3;
		}

		public C64Sprite()
		{
			SpriteData = new bool[Width, Height];
			SpritePreviewData = new Bitmap(Width, Height);
			using (var g = Graphics.FromImage(SpritePreviewData))
				g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
		}

		public void ResetPixels()
		{
			if (SpriteEditor.Multicolor)
				for (var y = 0; y < Height; y++)
					for (var x = 0; x < Width - 1; x += 2)
						SetPixel(x, y, GetColorIndex(x, y));
			else
				for (var y = 0; y < Height; y++)
					for (var x = 0; x < Width; x++)
						SetPixel(x, y, GetPixel(x, y));
		}

		public int GetColorIndex(int x, int y)
		{
			if (!SpriteEditor.Multicolor)
				return SpriteData[x, y] ? 1 : 0;
			if (!SpriteData[x, y] && SpriteData[x + 1, y])
				return 1;
			if (SpriteData[x, y] && !SpriteData[x + 1, y])
				return 2;
			if (SpriteData[x, y] && SpriteData[x + 1, y])
				return 3;
			return 0;
		}

		public void SetPixel(int x, int y, int colorIndex)
		{
			if (SpriteEditor.Multicolor)
			{
				int paletteIndex;
				switch (colorIndex)
				{
					case 1:
						paletteIndex = ForegroundColorIndex;
						SpriteData[x, y] = false;
						SpriteData[x + 1, y] = true;
						break;
					case 2:
						paletteIndex = ExtraColor1Index;
						SpriteData[x, y] = true;
						SpriteData[x + 1, y] = false;
						break;
					case 3:
						paletteIndex = ExtraColor2Index;
						SpriteData[x, y] = true;
						SpriteData[x + 1, y] = true;
						break;
					default:
						paletteIndex = BackgroundColorIndex;
						SpriteData[x, y] = false;
						SpriteData[x + 1, y] = false;
						break;
				}
				SpritePreviewData.SetPixel(x, y, Palette.GetColor(paletteIndex));
				SpritePreviewData.SetPixel(x + 1, y, Palette.GetColor(paletteIndex));
			}
			else
			{
				SpriteData[x, y] = colorIndex > 0;
				SpritePreviewData.SetPixel(x, y, Palette.GetColor(colorIndex == 0 ? BackgroundColorIndex : ForegroundColorIndex));
			}
		}

		public int GetPixel(int x, int y)
		{
			if (!SpriteEditor.Multicolor)
				return SpriteData[x, y] ? 1 : 0;
			if (x % 2 != 0)
				x -= 1;
			if (!SpriteData[x, y] && SpriteData[x + 1, y])
				return ForegroundColorIndex;
			if (SpriteData[x, y] && !SpriteData[x + 1, y])
				return ExtraColor1Index;
			if (SpriteData[x, y] && SpriteData[x + 1, y])
				return ExtraColor2Index;
			return BackgroundColorIndex;
		}

		public void Draw(Graphics g, int x, int y, bool doubleSize)
		{
			if (doubleSize)
			{
				g.DrawImage(SpritePreviewData, x, y, Width * 2, Height * 2 + 1);
				Bounds = new Rectangle(x, y, Width * 2, Height * 2);
				return;
			}
			g.DrawImage(SpritePreviewData, x, y);
			Bounds = new Rectangle(x, y, Width, Height);
		}
		public void Export(Bitmap b, int offsetX, int offsetY, bool transparentBackground)
		{
			for (var y = 0; y < Height; y++)
				for (var x = 0; x < Width; x++)
				{
					if (!SpriteData[x, y] && !transparentBackground)
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(BackgroundColorIndex));
					else if (SpriteData[x, y])
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(ForegroundColorIndex));
				}
		}

		public void ExportMultiColor(Bitmap b, int offsetX, int offsetY, bool transparentBackground)
		{
			const int w = Width/2;
			for (var y = 0; y < Height; y++)
				for (var x = 0; x < w; x++)
				{
					var colorIndex = GetPixel(x*2, y);
					if (colorIndex == BackgroundColorIndex && !transparentBackground)
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(BackgroundColorIndex));
					else if (colorIndex != BackgroundColorIndex)
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(colorIndex));
				}
		}

		public void ExportMultiColorDoubleWidth(Bitmap b, int offsetX, int offsetY, bool transparentBackground)
		{
			for (var y = 0; y < Height; y++)
				for (var x = 0; x < Width; x += 2)
				{
					var colorIndex = GetPixel(x, y);
					if (colorIndex == BackgroundColorIndex && !transparentBackground)
					{
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(BackgroundColorIndex));
						b.SetPixel(x + offsetX + 1, y + offsetY, Palette.GetColor(BackgroundColorIndex));
					}
					else if (colorIndex != BackgroundColorIndex)
					{
						b.SetPixel(x + offsetX, y + offsetY, Palette.GetColor(colorIndex));
						b.SetPixel(x + offsetX + 1, y + offsetY, Palette.GetColor(colorIndex));
					}
				}
		}

		public bool HitTest(int x, int y) =>
			Bounds.IntersectsWith(new Rectangle(x, y, 1, 1));

		public byte[] GetBytes()
		{
			var ret = new byte[63];
			var i = 0;
			for (var y = 0; y < Height; y++)
				for (var x = 0; x < 3; x++)
				{
					var physicalX = 8*x;
					var b = new C64MemoryModel.Types.Byte(IsSet(physicalX, y), IsSet(physicalX + 1, y), IsSet(physicalX + 2, y), IsSet(physicalX + 3, y), IsSet(physicalX + 4, y), IsSet(physicalX + 5, y), IsSet(physicalX + 6, y), IsSet(physicalX + 7, y));
					ret[i] = b.Value;
					i++;
				}
			return ret;
		}

		private bool IsSet(int x, int y) => SpriteData[x, y];

		public bool Load(BinaryReader sr)
		{
			int x = 0, y = 0;
			for (var i = 0; i < 63; i++)
			{
				var b = new C64MemoryModel.Types.Byte(sr.ReadByte());
				var physicalX = x*8;
				SetPixel(physicalX, y, b.Bit7 ? 1 : 0);
				SetPixel(physicalX + 1, y, b.Bit6 ? 1 : 0);
				SetPixel(physicalX + 2, y, b.Bit5 ? 1 : 0);
				SetPixel(physicalX + 3, y, b.Bit4 ? 1 : 0);
				SetPixel(physicalX + 4, y, b.Bit3 ? 1 : 0);
				SetPixel(physicalX + 5, y, b.Bit2 ? 1 : 0);
				SetPixel(physicalX + 6, y, b.Bit1 ? 1 : 0);
				SetPixel(physicalX + 7, y, b.Bit0 ? 1 : 0);
				x++;
				if (x <= 2)
					continue;
				x = 0;
				y++;
			}
			return true;
		}
	}
}
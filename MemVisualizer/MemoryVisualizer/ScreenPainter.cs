using System.Drawing;
using System.Drawing.Drawing2D;
using C64MemoryModel.Graphics;
using C64MemoryModel.Mem;

namespace MemoryVisualizer
{
    public class ScreenPainter
    {
        private static C64Palette Palette { get; } = new C64Palette();
        private Font GridFont { get; set; } = new Font("Courier New", 10);
        public bool RecalcGridFontSize { get; set; } = true;

        public void Paint(Graphics g, Memory memory, Size clientSize, int yOffset, int rowCount, int columnCount, ScreenCharacterMap characters)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            var borderHeight = (int)(clientSize.Height * 0.10);
            var borderWidth = (int)(clientSize.Width * 0.10);
            var outerClient = new Rectangle(0, yOffset, clientSize.Width, clientSize.Height - yOffset);
            var innerClient = new Rectangle(outerClient.Left + borderWidth, outerClient.Top + borderHeight, outerClient.Width - borderWidth - borderWidth, outerClient.Height - borderHeight - borderHeight);
            using (var lightBlue = new SolidBrush(Palette.GetColor(C64Color.LightBlue)))
            {
                using (var blue = new SolidBrush(Palette.GetColor(C64Color.Blue)))
                {
                    var fontXOffset = 0.0f;
                    var fontYOffset = 0.0f;
                    //Draw border.
                    g.FillRectangle(lightBlue, outerClient.Left, outerClient.Top, outerClient.Width, borderHeight);
                    g.FillRectangle(lightBlue, outerClient.Left, outerClient.Top, borderWidth, outerClient.Height);
                    g.FillRectangle(lightBlue, outerClient.Left, outerClient.Top + outerClient.Height - borderHeight, outerClient.Width, borderHeight);
                    g.FillRectangle(lightBlue, outerClient.Left + outerClient.Width - borderWidth, 0, borderWidth, outerClient.Height);
                    g.FillRectangle(blue, innerClient);
                    if (memory == null)
                    {
                        using (var f = new Font("Arial", 20, FontStyle.Regular))
                            g.DrawString("Drop .prg file here.", f, Brushes.White, innerClient.Left, innerClient.Top);
                    }
                    else
                    {
                        var characterWidth = (float)(innerClient.Width / (double)columnCount);
                        var characterHeight = (float)(innerClient.Height / (double)rowCount);
                        if (RecalcGridFontSize)
                        {
                            RecalcGridFontSize = false;
                            GridFont.Dispose();
                            var fontSize = characterHeight > characterWidth ? characterWidth : characterHeight;
                            GridFont = new Font("Courier New", fontSize * 0.9f);
                            var charSize = g.MeasureString("W", GridFont);
                            fontXOffset = (characterWidth / 2) - (charSize.Width / 2);
                            fontYOffset = ((characterHeight / 2) - (charSize.Height / 2)) * 1.1f;
                        }
                        var xPos = (float)innerClient.Left;
                        var yPos = (float)innerClient.Top;
                        for (var y = 0; y < rowCount; y++)
                        {
                            for (var x = 0; x < columnCount; x++)
                            {
                                g.DrawString(characters.GetCharacter(x, y).ToString(), GridFont, lightBlue, xPos + fontXOffset, yPos + fontYOffset);
                                xPos += characterWidth;
                            }
                            xPos = innerClient.Left;
                            yPos += characterHeight;
                        }
                    }
                }
            }
        }
    }
}
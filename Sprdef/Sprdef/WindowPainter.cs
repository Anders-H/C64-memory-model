using System;
using System.Drawing;

namespace Sprdef
{
    public static class WindowPainter
    {
        public static void Paint(Graphics g, bool redrawBackground, Color backColor, int windowWidth, int windowHeight, int extraOffsetY, SpriteEditor spriteEditor, SpriteArray sprites, ColorPicker colorPicker)
        {
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            if (redrawBackground)
                g.Clear(backColor);

            spriteEditor.X = 10;
            spriteEditor.Y = 10 + ColorPickerCell.Size + extraOffsetY;
            var pw = (windowWidth - 200) / C64Sprite.Width;
            var ph = (windowHeight - 200) / C64Sprite.Height;
            spriteEditor.PixelSize = Math.Min(pw, ph);
            spriteEditor.PixelSize = spriteEditor.PixelSize < 6 ? 6 : spriteEditor.PixelSize;

            var doubleSize = windowWidth * 1.5 > windowHeight;
            var spritesStartX = doubleSize ? windowWidth - 68 : windowWidth - 44;
            var spritesStartY = spriteEditor.Y;
            for (var i = 0; i < SpriteArray.Length; i++)
            {
                sprites[i].Draw(g, spritesStartX, spritesStartY, doubleSize);
                spritesStartY += doubleSize ? 44 : 22;
            }

            spriteEditor.Draw(g);
            ColorPickerCell.Size = spriteEditor.PixelSize * 2;
            colorPicker.Draw(g, spriteEditor.X, spriteEditor.Y - (ColorPickerCell.Size + 8), SpriteEditor.Multicolor);
        }
    }
}
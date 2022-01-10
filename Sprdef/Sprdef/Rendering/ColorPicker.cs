using Sprdef.Model;
using System.Drawing;

namespace Sprdef.Rendering
{
    public class ColorPicker
    {
        private readonly ColorPickerCell[] _colorCells = new ColorPickerCell[4];
        public int SelectedColor { get; set; } = 1;

        public ColorPicker()
        {
            _colorCells[0] = new ColorPickerCell();
            _colorCells[1] = new ColorPickerCell();
            _colorCells[2] = new ColorPickerCell();
            _colorCells[3] = new ColorPickerCell();
        }

        public ColorPickerCell GetColorCell(int index) =>
            _colorCells[index];

        public void Draw(Graphics g, C64Sprite sprite, int x, int y)
        {
            for (var i = 0; i < 4; i++)
                GetColorCell(i).Draw(
                    g, sprite,
                    x + (i * (ColorPickerCell.Size + 8)),
                    y,
                    i,
                    sprite.Multicolor | (i < 2),
                    i == SelectedColor
                );
        }

        public ColorPickerCell HitTest(int x, int y)
        {
            if (_colorCells[0].HitTest(x, y))
                return _colorCells[0];
            if (_colorCells[1].HitTest(x, y))
                return _colorCells[1];
            if (_colorCells[2].HitTest(x, y))
                return _colorCells[2];
            if (_colorCells[3].HitTest(x, y))
                return _colorCells[3];
            return null;
        }
    }
}
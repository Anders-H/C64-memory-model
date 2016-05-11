using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryVisualizer
{
    public class ScreenCharacterMap
    {
        private const int Columns = 40;
        private const int Rows = 25;
        private char[,] Characters = new char[Columns, Rows];
        public ScreenCharacterMap()
        {
            Clear();
        }
        public void Clear()
        {
            for (var y = 0; y < Rows; y++)
                for (var x = 0; x < Columns; x++)
                    Characters[x, y] = ' ';
        }
    }
}

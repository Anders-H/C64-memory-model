namespace MemoryVisualizer
{
    public class ScreenCharacterMap
    {
        public const int Columns = 40;
        public const int Rows = 25;
        private char[,] Characters { get; } = new char[Columns, Rows];
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
        public char GetCharacter(int x, int y) => Characters[x, y];
        public void SetCharacters(int x, int y, string characters)
        {
            if (string.IsNullOrEmpty(characters))
                return;
            for (var i = 0; i < characters.Length; i++)
            {
                var xPos = i + x;
                if (xPos < Columns)
                    Characters[xPos, y] = characters[i];
                else
                    return;
            }
        }
    }
}

namespace ThePetscii
{
    public class PetsciiMap
    {
        private readonly PetsciiChar[,] _chars;

        public PetsciiMap()
        {
            _chars = new PetsciiChar[40, 25];
        }

        public void SetSubpixel(int charX, int charY, int subX, int subY, bool value) =>
            _chars.Get(charX, charY).SetAt(subX, subY, true);

        public bool GetSubpixel(int charX, int charY, int subX, int subY)
        {
            var c = _chars[charX, charY];
            return c != null && c.IsSetAt(subX, subY);
        }

        public bool IsAllSet(int charX, int charY)
        {
            var c = _chars[charX, charY];
            return c != null && c.IsAllSet();
        }

        public bool IsNoneSet(int charX, int charY)
        {
            var c = _chars[charX, charY];
            return c == null || c.IsNoneSet();
        }
    }
}
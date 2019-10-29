namespace ThePetscii
{
    public static class PetsciiCharExtensions
    {
        public static PetsciiChar Get(this PetsciiChar[,] me, int x, int y)
        {
            var c = me[x, y];
            if (c != null)
                return c;
            c = new PetsciiChar(0, false);
            me[x, y] = c;
            return c;
        }
    }
}
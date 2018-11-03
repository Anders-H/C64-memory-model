namespace C64MemoryModel.Chr
{
    public class Character
    {
        public byte Petscii { get; }
        public char Unicode { get; }

        public Character(byte petscii, char unicode)
        {
            Petscii = petscii;
            Unicode = unicode;
        }
    }
}
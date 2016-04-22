namespace C64MemoryModel.Chr
{
    public class Character
    {
        public byte PETSCII { get; }
        public char Unicode { get; }
        public Character(byte petscii, char unicode)
        {
            PETSCII = petscii;
            Unicode = unicode;
        }
    }
}

namespace C64MemoryModel.Chr
{
    public class Character
    {
        // ReSharper disable once InconsistentNaming
        public byte PETSCII { get; }
        public char Unicode { get; }
        public Character(byte petscii, char unicode)
        {
            PETSCII = petscii;
            Unicode = unicode;
        }
    }
}

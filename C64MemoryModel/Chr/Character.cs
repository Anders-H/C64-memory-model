namespace C64MemoryModel.Chr
{
    public class Character
    {
        public byte PetsciiByte { get; }
        public char Unicode { get; }
        public PetsciiCode PetsciiCode { get; }
        
        public Character(byte petsciiByte, char unicode)
        {
            PetsciiByte = petsciiByte;
            Unicode = unicode;
            PetsciiCode = (PetsciiCode)petsciiByte;
        }
        
        public Character(PetsciiCode petsciiCode, char unicode)
        {
            PetsciiByte = (byte)petsciiCode;
            Unicode = unicode;
            PetsciiCode = petsciiCode;
        }
    }
}
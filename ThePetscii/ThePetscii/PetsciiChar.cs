using System;
using C64MemoryModel.Chr;
using Byte = C64MemoryModel.Types.Byte;

namespace ThePetscii
{
    public class PetsciiChar
    {
        public byte Identity { get; private set; }
        public bool ReversedFlag { get; private set; }

        public PetsciiChar(byte identity, bool reversedFlag)
        {
            Identity = identity;
            ReversedFlag = reversedFlag;
        }

        public (PetsciiCode code, bool reversed) GetPetscii()
        {
            switch (Identity)
            {
                case 0b00000000:                                 // OO
                    return (PetsciiCode.Petscii032Space, false); // OO
                case 0b00000001:                                      // XO
                    return (PetsciiCode.Petscii126, false);           // OO
                case 0b00000010:                                 // OX
                    return (PetsciiCode.Petscii124, false);      // OO
                case 0b00000011:                                      // XX
                    return (PetsciiCode.Petscii098, true);            // OO
                case 0b00000100:                                 // OO
                    return (PetsciiCode.Petscii123, false);      // XO
                case 0b00000101:                                      // XO
                    return (PetsciiCode.Petscii097, false);           // XO
                case 0b00000110:                                 // OX
                    return (PetsciiCode.Petscii127, true);       // XO
                case 0b00000111:                                      // XX
                    return (PetsciiCode.Petscii108L, true);            // XO
                case 0b00001000:                                 // OO
                    return (PetsciiCode.Petscii108L, false);      // OX
                case 0b00001001:                                      // XO
                    return (PetsciiCode.Petscii127, false);           // OX
                case 0b00001010:                                 // OX
                    return (PetsciiCode.Petscii097, true);       // OX
                case 0b00001011:                                      // XX
                    return (PetsciiCode.Petscii123, true);            // OX
                case 0b00001100:                                 // OO
                    return (PetsciiCode.Petscii098, false);      // XX
                case 0b00001101:                                      // XO
                    return (PetsciiCode.Petscii124, true);            // XX
                case 0b00001110:                                 // OX
                    return (PetsciiCode.Petscii196, true);       // XX
                case 0b00001111:                                      // XX
                    return (PetsciiCode.Petscii032Space, true);       // XX
            }
            throw new ArgumentOutOfRangeException();
        }

        public (byte code, bool reversed) IdentityToByte()
        {
            switch (Identity)
            {
                case 0b00000000:                                 // OO
                    return (32, false);                          // OO
                case 0b00000001:                                      // XO
                    return (126, false);                              // OO
                case 0b00000010:                                 // OX
                    return (124, false);                         // OO
                case 0b00000011:                                      // XX
                    return (98, true);                                // OO
                case 0b00000100:                                 // OO
                    return (123, false);                         // XO
                case 0b00000101:                                      // XO
                    return (97, false);                               // XO
                case 0b00000110:                                 // OX
                    return (127, true);                          // XO
                case 0b00000111:                                      // XX
                    return (108, true);                               // XO
                case 0b00001000:                                 // OO
                    return (108, false);                         // OX
                case 0b00001001:                                      // XO
                    return (127, false);                              // OX
                case 0b00001010:                                 // OX
                    return (97, true);                           // OX
                case 0b00001011:                                      // XX
                    return (123, true);                               // OX
                case 0b00001100:                                 // OO
                    return (98, false);                          // XX
                case 0b00001101:                                      // XO
                    return (124, true);                               // XX
                case 0b00001110:                                 // OX
                    return (126, true);                          // XX
                case 0b00001111:                                      // XX
                    return (32, true);                                // XX
            }
            throw new ArgumentOutOfRangeException();
        }
        public byte GetByte()
        {
            var (code, reversed) = IdentityToByte();
            return reversed ? (byte)(code + 128) : code;
        }
        
        public bool IsAllSet() =>
            new Byte(Identity)
                .GetLowNibble()
                .IsSet(true, true, true, true);
        
        public bool IsNoneSet() =>
            new Byte(Identity)
                .GetLowNibble()
                .IsNotSet(true, true, true, true);

        public void SetAt(int subX, int subY, bool set)
        {
            var b = new Byte(Identity);
            if (subY == 0)
            {
                if (subX == 0)
                    b.Bit0 = set;
                else if (subX == 1)
                    b.Bit1 = set;
                else
                    throw new ArgumentOutOfRangeException();
                Identity = b.Value;
                return;
            }
            if (subY == 1)
            {
                if (subX == 0)
                    b.Bit2 = set;
                else if (subX == 1)
                    b.Bit3 = set;
                else
                    throw new ArgumentOutOfRangeException();
                Identity = b.Value;
                return;
            }
            throw new ArgumentOutOfRangeException();
        }
        
        public bool IsSetAt(int subX, int subY)
        {
            if (subY == 0)
            {
                if (subX == 0)
                    return new Byte(Identity)
                        .GetLowNibble()
                        .IsSet(false, false, false, true);
                if (subX == 1)
                    return new Byte(Identity)
                        .GetLowNibble()
                        .IsSet(false, false, true, false);
                throw new ArgumentOutOfRangeException();
            }
            if (subY == 1)
            {
                if (subX == 0)
                    return new Byte(Identity)
                        .GetLowNibble()
                        .IsSet(false, true, false, false);
                if (subX == 1)
                    return new Byte(Identity)
                        .GetLowNibble()
                        .IsSet(true, false, false, false);
                throw new ArgumentOutOfRangeException();
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using C64MemoryModel.Chr;
using Byte = C64MemoryModel.Types.Byte;

namespace ThePetscii
{
    public class PetsciiChar
    {
        public byte Identity { get; }
        public PetsciiCode PetsciiCode { get; }
        public bool ReversedFlag { get; }

        public PetsciiChar(byte identity)
        {
            switch (identity)
            {
                case 0b00000000:
                case 0b00000001:
                case 0b00000010:
                case 0b00000011:
                case 0b00000100:
                case 0b00000101:
                case 0b00000110:
                case 0b00000111:
                case 0b00001000:
                case 0b00001001:
                case 0b00001010:
                case 0b00001011:
                case 0b00001100:
                case 0b00001101:
                case 0b00001110:
                case 0b00001111:
                    break; // TODO Send back Petscii codes
            }
        }

        public bool IsAllSet() =>
            new Byte(Identity)
                .GetLowNibble()
                .IsSet(true, true, true, true);
        
        public bool IsNoneSet() =>
            new Byte(Identity)
                .GetLowNibble()
                .IsNotSet(true, true, true, true);

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
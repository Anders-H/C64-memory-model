using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel
{
    public class ExtendedAssembler
    {
        private Assembler Assembler { get; }
        internal ExtendedAssembler(Assembler assembler) { Assembler = assembler; }
        public void PokeByte(ushort address, byte value)
        {
            Assembler.Lda(value);
            Assembler.Sta(address);
        }
    }
}

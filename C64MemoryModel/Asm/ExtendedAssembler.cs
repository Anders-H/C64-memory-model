using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class ExtendedAssembler
    {
        private Assembler Assembler { get; }
        public VariableList Variables { get; }
        internal ExtendedAssembler(Assembler assembler) { Assembler = assembler; Variables = new VariableList(Assembler); }
        public void PokeByte(ushort address, byte value)
        {
            Assembler.Lda(value);
            Assembler.Sta(address);
        }
        public void PokeByte(WordVariable address, byte value)
        {
            Assembler.Lda(value);
            Assembler.Sta(address.Address);
        }
        public void PokeByte(ushort address, ByteVariable value)
        {
            Assembler.Lda(value.Address);
            Assembler.Sta(address);
        }
        public void PokeByte(WordVariable address, ByteVariable value)
        {
            Assembler.Lda(value.Address);
            Assembler.Sta(address.Address);
        }
    }
}

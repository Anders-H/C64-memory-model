using System;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public class Assembler
    {
        internal Memory Memory { get; }
        public ExtendedAssembler Extended { get; }

        internal Assembler(Memory memory)
        {
            Memory = memory;
            Extended = new ExtendedAssembler(this);
        }

        //000 00 BRK
        public void Brk() => Memory.SetByte(0);
        //001 01 ORA
        public void Ora(byte value) => Immediate(1, value);
        //032 20 JSR
        public void Jsr(Address address) => Absolute(32, address);
        //096 60 RTS
        public void Rts() => Memory.SetByte(96);
        //141 8D STA Absolute
        public void Sta(Address address) => Absolute(141, address);
        //162 A2 LDX Immediate
        public void Ldx(byte value) => Immediate(162, value);
        //169 A9 LDA Immediate
        public void Lda(byte value) => Immediate(169, value);
        //173 AD LDA Absolute
        public void Lda(Address address) => Absolute(173, address);
        //189 BD LDA Absolute,X
        public void LdaAbsX(Address address) => Absolute(189, address);
        //208 D0 BNE
        public void Bne(Address address) => Relative(208, address);
        //232 E8 INX
        public void Inx() => Memory.SetByte(232);
        //240 F0 BEQ
        public void Beq(Address address) => Relative(240, address);

        //--------------------------------------------------------------------------------------------------//

        private void Immediate(byte opcode, byte value)
        {
            Memory.SetByte(opcode);
            Memory.SetByte(value);
        }

        private void Absolute(byte opcode, Address address)
        {
            var bytes = BitConverter.GetBytes(address.Value);
            var low = bytes[0];
            var high = bytes[1];
            Memory.SetByte(opcode);
            Memory.SetByte(low);
            Memory.SetByte(high);
        }

        private void Relative(byte opcode, Address address)
        {
            var currentAddress = Memory.GetBytePointer() + 2;
            var diff = (int)(address - currentAddress);
            if (diff > 127 || diff < -128)
                throw new SystemException($"Jump from {currentAddress} to {address} is too long.");
            diff = diff < 0 ? diff + 256 : diff;
            Memory.SetByte(opcode);
            Memory.SetByte((byte)diff);
        }
    }
}
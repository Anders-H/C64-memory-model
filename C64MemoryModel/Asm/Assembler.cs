using System;

namespace C64MemoryModel.Asm
{
    public class Assembler
    {
        internal Memory Memory { get; }
        public ExtendedAssembler Extended { get; }
        internal Assembler(Memory memory) { Memory = memory; Extended = new ExtendedAssembler(this); }
        //032 20 JSR
        public void Jsr(ushort address) => Absolute(23, address);
        //096 60 RTS
        public void Rts() => Memory.SetByte(96);
        //141 8D STA Absolute
        public void Sta(ushort address) => Absolute(141, address);
        //162 A2 LDX Immediate
        public void Ldx(byte value) => Immediate(162, value);
        //169 A9 LDA Immediate
        public void Lda(byte value) => Immediate(169, value);
        //173 AD LDA Absolute
        public void Lda(ushort address) => Absolute(173, address);
        //189 BD LDA Absolute,X
        public void LdaAbsX(ushort address) => Absolute(189, address);
        //208 D0 BNE
        public void Bne(ushort address) => Relative(208, address);
        //232 E8 INX
        public void Inx() => Memory.SetByte(232);
        //240 F0 BEQ
        public void Beq(ushort address) => Relative(240, address);
        //--------------------------------------------------------------------------------------------------
        private void Immediate(byte opcode, byte value)
        {
            Memory.SetByte(opcode);
            Memory.SetByte(value);
        }
        private void Absolute(byte opcode, ushort address)
        {
            var bytes = BitConverter.GetBytes(address);
            var low = bytes[0];
            var high = bytes[1];
            Memory.SetByte(opcode);
            Memory.SetByte(low);
            Memory.SetByte(high);
        }
        private void Relative(byte opcode, ushort address)
        {
            var currentAddress = Memory.GetBytePointer() + 2;
            var diff = address - currentAddress;
            if (diff > 127 || diff < -128)
                throw new SystemException($"Jump from {currentAddress} to {address} is too long.");
            diff = diff < 0 ? diff + 256 : diff;
            Memory.SetByte(opcode);
            Memory.SetByte((byte)diff);
        }
    }
}

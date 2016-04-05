using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class Assembler
    {
        private Memory Memory { get; }
        public ExtendedAssembler Extended { get; }
        internal Assembler(Memory memory) { Memory = memory; Extended = new ExtendedAssembler(this); }
        //032 20 JSR
        public void Jsr(ushort address)
        {
            var bytes = BitConverter.GetBytes(address);
            var low = bytes[0];
            var high = bytes[1];
            Memory.SetByte(32);
            Memory.SetByte(low);
            Memory.SetByte(high);
        }
        //096 60 RTS
        public void Rts() => Memory.SetByte(96);
        //141 8D STA Absolute
        public void Sta(ushort address)
        {
            var bytes = BitConverter.GetBytes(address);
            var low = bytes[0];
            var high = bytes[1];
            Memory.SetByte(141);
            Memory.SetByte(low);
            Memory.SetByte(high);
        }
        //162 A2 LDX Immediate
        public void Ldx(byte value)
        {
            Memory.SetByte(162);
            Memory.SetByte(value);
        }
        //169 A9 LDA Immediate
        public void Lda(byte value)
        {
            Memory.SetByte(169);
            Memory.SetByte(value);
        }
        //189 BD LDA Absolute,X
        public void LdaAbsX(ushort address)
        {
            var bytes = BitConverter.GetBytes(address);
            var low = bytes[0];
            var high = bytes[1];
            Memory.SetByte(189);
            Memory.SetByte(low);
            Memory.SetByte(high);
        }
        //208 D0 BNE
        public void Bne(ushort address)
        {
            var currentAddress = Memory.GetBytePointer() + 2;
            var diff = address - currentAddress;
            if (diff > 127 || diff < -128)
                throw new SystemException($"Jump from {currentAddress} to {address} is too long.");
            diff = diff < 0 ? diff + 256 : diff;
            Memory.SetByte(208);
            Memory.SetByte((byte)diff);
        }
        //232 E8 INX
        public void Inx() => Memory.SetByte(232);
        //240 F0 BEQ
        public void Beq(ushort address)
        {
            var currentAddress = Memory.GetBytePointer() + 2;
            var diff = address - currentAddress;
            if (diff > 127 || diff < -128)
                throw new SystemException($"Jump from {currentAddress} to {address} is too long.");
            diff = diff < 0 ? diff + 256 : diff;
            Memory.SetByte(240);
            Memory.SetByte((byte)diff);
        }
    }
}

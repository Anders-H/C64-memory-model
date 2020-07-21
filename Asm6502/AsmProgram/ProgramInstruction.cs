using System.Collections.Generic;
using Asm6502.AsmInstructionPalette;

namespace Asm6502.AsmProgram
{
    public class ProgramInstruction
    {
        public ushort Address { get; set; }

        public OperationCode OperationCode { get; }

        public OperationMode OperationMode { get; }

        public byte ByteArgument { get; set; }

        public ushort WordArgument { get; set; }

        public string Comment { get; set; }

        public ProgramInstruction() : this(0, OperationCode.Lda, OperationMode.Absolute)
        {
        }

        public ProgramInstruction(ushort address, OperationCode opCode, OperationMode opMode)
        {
            Address = address;
            OperationCode = opCode;
            OperationMode = opMode;
        }

        public int GetLength()
        {
            return 3;
        }

        public List<byte> GetMachineCode()
        {
            return new List<byte>
            {
                1, 2, 3
            };
        }

        public void InitializeFromMachineCode(List<byte> bytes)
        {

        }
    }
}
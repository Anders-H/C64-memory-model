using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class ByteVariable : VariableBase
    {
        public byte Value { get; set; }
        public ByteVariable(ushort address, byte initialValue) : base(address)
        {
            Value = initialValue;
        }
    }
}

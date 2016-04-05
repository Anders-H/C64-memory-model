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
        internal ByteVariable(Memory memory, string name, ushort address, byte initialValue) : base(memory, name, address)
        {
            Value = initialValue;
        }
    }
}

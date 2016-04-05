using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class VariableList : List<VariableBase>
    {
        internal Memory Memory { get; }
        public VariableList(Memory memory)
        {
            Memory = memory;
        }
        public ByteVariable CreateByteVariable(string name, ushort address, byte initialValue)
        {
            var x = new ByteVariable(Memory, name, address, initialValue);
            Memory.SetBytePointer(address);
            Memory.SetByte(initialValue);
            Add(x);
            return x;
        }
        public WordVariable CreateWordVariable(string name, ushort address, ushort initialValue)
        {
            var x = new WordVariable(Memory, name, address, initialValue);
            Memory.SetBytePointer(address);
            Memory.SetWord(initialValue);
            Add(x);
            return x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class VariableList : List<IVariable>
    {
        internal Assembler Assembler { get; }
        public VariableList(Assembler assembler)
        {
            Assembler = assembler;
        }
        public ByteVariable CreateByteVariable(string name, ushort address)
        {
            var x = new ByteVariable(Assembler, name, address);
            Add(x);
            return x;
        }
        public WordVariable CreateWordVariable(string name, ushort address)
        {
            var x = new WordVariable(Assembler, name, address);
            Add(x);
            return x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public class WordVariable : VariableBase
    {
        public ushort Value { get; set; }
        public WordVariable(ushort address, ushort initialValue) : base(address)
        {
            Value = initialValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel.Asm
{
    public abstract class VariableBase
    {
        public ushort Address { get; }
        protected VariableBase(ushort address) { Address = address; }
    }
}

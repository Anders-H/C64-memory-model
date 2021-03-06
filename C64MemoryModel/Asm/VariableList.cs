﻿using System.Collections.Generic;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace C64MemoryModel.Asm
{
    public class VariableList : List<IVariable>
    {
        internal Assembler Assembler { get; }

        public VariableList(Assembler assembler)
        {
            Assembler = assembler;
        }

        public ByteVariable CreateByteVariable(string name, Address address)
        {
            var x = new ByteVariable(Assembler, name, address);
            Add(x);
            return x;
        }

        public ByteVariable CreateByteVariable(string name, IMemoryLocation address) =>
            CreateByteVariable(name, address.StartAddress);

        public ByteVariable CreateByteVariable(MemoryModelLocation address) =>
            CreateByteVariable(address.Name.ToString(), address.StartAddress);

        public WordVariable CreateWordVariable(string name, Address address)
        {
            var x = new WordVariable(Assembler, name, address);
            Add(x);
            return x;
        }

        public WordVariable CreateWordVariable(string name, IMemoryLocation address) =>
            CreateWordVariable(name, address.StartAddress);

        public WordVariable CreateWordVariable(MemoryModelLocation address) =>
            CreateWordVariable(address.Name.ToString(), address.StartAddress);
    }
}
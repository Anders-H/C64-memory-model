using System;
using System.Diagnostics;
using C64MemoryModel;

namespace C64Mem
{
    internal class Program
    {
        private static void Main()
        {
            var m = new Memory();
            //A program that sets the backgroud color to red.
            var v = m.Assembler.Extended.CreateByteVariable(MemoryModelLocationName.BackgroundColor);
            v.WriteAssign(4096, 2);
            m.Assembler.Rts();
            m.SetBytePointer(4096);
            Console.Write(m.GetDisassembly(10, true));


            var t = new TextAdapter(new Memory());
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{t.BytePointer:00000} ${t.BytePointer:X4}> ");
                Console.ForegroundColor = ConsoleColor.Gray;
                var i = Console.ReadLine();
                Debug.Assert(i != null, "i != null");
                if (string.Compare(i.Trim(), "x", StringComparison.CurrentCulture) == 0)
                    break;
                bool success;
                var resp = t.Ask(i, out success).Trim();
                Console.ForegroundColor = success ? ConsoleColor.DarkGray : ConsoleColor.Red;
                if (resp.Length > 0)
                    Console.WriteLine(resp);
            } while (true);
        }
    }
}

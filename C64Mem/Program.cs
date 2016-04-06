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
            //Turn on first and second sprite.
            var b = new C64MemoryModel.Types.Byte(false, false, false, false, false, false, true, true);
            m.SetBytePointer(4096);
            m.Assembler.Lda(b.ToByte());
            m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteEnableRegister));
            //Position the first sprite at 128, 128.
            m.Assembler.Lda(128);
            m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteLocations));
            m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteLocations) + 1);
            m.Assembler.Rts();
            m.Save(@"C:\Temp\hej.prg");

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

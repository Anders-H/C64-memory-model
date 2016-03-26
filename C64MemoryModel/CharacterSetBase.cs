using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel
{
    public abstract class CharacterSetBase
    {
        protected List<Character> Characters { get; } = new List<Character>();
        public string Name { get; }
        public CharacterSetBase(string name) { Name = name; }
        protected byte TranslateCharacterFromUnicodeToPETSCII(char input)
        {
            var ret = Characters.FirstOrDefault(x => x.Unicode == input);
            if (ret == null)
                throw new SystemException($"Unsupported character: ${(int)input:X4}");
            return ret.PETSCII;
        }
        protected char TranslateCharacterFromPETSCIIToUnicode(byte input)
        {
            var ret = Characters.FirstOrDefault(x => x.PETSCII == input);
            if (ret == null)
                throw new SystemException($"Unsupported character: ${input:X2}");
            return ret.Unicode;
        }
        public string TranslateString(byte[] input)
        {
            if (input == null || input.Length <= 0)
                return "";
            var ret = new StringBuilder();
            foreach (var x in input)
                ret.Append(TranslateCharacterFromPETSCIIToUnicode(x));
            return ret.ToString();

        }
        public byte[] TranslateString(string input)
        {
            var ret = new List<byte>();
            if (input == null || input.Length <= 0)
                return ret.ToArray();
            foreach (var x in input)
                ret.Add(TranslateCharacterFromUnicodeToPETSCII(x));
            return ret.ToArray();
        }
    }
}

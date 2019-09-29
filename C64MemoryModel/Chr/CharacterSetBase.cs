using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C64MemoryModel.Chr
{
    public abstract class CharacterSetBase
    {
        protected List<Character> Characters { get; } = new List<Character>();
        public string Name { get; }

        protected CharacterSetBase(string name)
        {
            Name = name;
        }

        protected byte TranslateCharacterFromUnicodeToPetscii(char input)
        {
            var ret = Characters.FirstOrDefault(x => x.Unicode == input);
            if (ret == null)
                throw new SystemException($"Unsupported character: ${(int)input:X4}");
            return ret.PetsciiByte;
        }
        
        protected char TranslateCharacterFromPetsciiToUnicode(byte input)
        {
            var ret = Characters.FirstOrDefault(x => x.PetsciiByte == input);
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
                ret.Append(TranslateCharacterFromPetsciiToUnicode(x));
            return ret.ToString();

        }

        public byte[] TranslateString(string input)
        {
            var ret = new List<byte>();
            if (input == null || input.Length <= 0)
                return ret.ToArray();
            ret.AddRange(input.Select(TranslateCharacterFromUnicodeToPetscii));
            return ret.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Byte = C64MemoryModel.Types.Byte;

namespace Sprdef
{
    public class TextDataParser
    {
        private string _source;

        public TextDataParser(string source)
        {
            _source = source;
        }

        public Byte[] CleanDataOutput()
        {
            var tempParts = Regex.Split(_source, @"\n");
            var parts = new List<string>();
            foreach (var part in tempParts)
            {
                if (part.ToLower().IndexOf("data", StringComparison.Ordinal) > -1)
                    parts.Add(part.Substring(part.IndexOf("data", StringComparison.Ordinal) + 4).Trim());
                else if (!string.IsNullOrWhiteSpace(part))
                    parts.Add(part.Trim());
            }
            var bytes = new List<Byte>();
            foreach (var part in parts)
            {
                var subparts = Regex.Split(part, @"[,\s,;]");
                foreach (var subpart in subparts)
                {
                    if (!Parse(subpart, out var result))
                        continue;
                    if (result >= 0 && result < 256)
                        bytes.Add(new Byte((byte)result));
                }
            }
            var returnvalue = new List<Byte>();
            for (var i = 0; i < 63; i++)
                returnvalue.Add(i < bytes.Count ? bytes[i] : new Byte(0));
            return returnvalue.ToArray();
        }

        private bool Parse(string part, out int result)
        {
            result = 0;
            part = part ?? "";
            if (string.IsNullOrWhiteSpace(part))
                return false;
            var s = new StringBuilder();
            //TODO
        }
    }
}
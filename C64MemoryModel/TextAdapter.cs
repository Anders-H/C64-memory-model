using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace C64MemoryModel
{
    public class TextAdapter
    {
        private Memory Memory { get; }
        public int BytePointer => Memory.BytePointer;
        public TextAdapter(Memory memory) { Memory = memory; }
        public string Ask(string input, out bool success)
        {
            success = false;

            //Remove comment
            var x = Regex.Match(input, @"\/\/(?=[^""]*(?: ""[^""]*""[^""]*)*$)");
            if (x.Success)
                input = input.Substring(0, x.Index).Trim();
            if (input == "")
                return "";

            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return "";
                input = Regex.Replace(input, @"\s+", " ").Trim();
                Match match;

                //GetByte
                match = Regex.Match(input, @"^getbyte$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adr = Memory.BytePointer;
                    var theByte = Memory.GetByte();
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {theByte:000} ${theByte:X2}";
                }

                //GetByte 4096
                match = Regex.Match(input, @"^getbyte\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetWordHex(adrValue, out addressSuccess) : GetWordDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var theByte = Memory.GetByte(adr);
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {theByte:000} ${theByte:X2}";
                }

                //GetWord
                match = Regex.Match(input, @"^getword$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adr = Memory.BytePointer;
                    var theWord = Memory.GetWord();
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {theWord:00000} ${theWord:X4}";
                }

                //GetWord 4096
                match = Regex.Match(input, @"^getword\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetWordHex(adrValue, out addressSuccess) : GetWordDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var theWord = Memory.GetWord(adr);
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {theWord:00000} ${theWord:X4}";
                }

                //SetByte 1
                match = Regex.Match(input, @"^setbyte\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool byteSuccess;
                    var value = match.Groups[1].Value;
                    var adr = (ushort)Memory.BytePointer;
                    var oldByte = Memory.GetByte(adr);
                    var theByte = value.StartsWith("$") ? GetByteHex(value, out byteSuccess) : GetByteDec(value, out byteSuccess);
                    if (!byteSuccess)
                        return "Invalid byte.";
                    Memory.SetByte(adr, theByte);
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {oldByte:000} ${oldByte:X2} --> {theByte:000} ${theByte:X2}";
                }

                //SetByte 4096, 1
                match = Regex.Match(input, @"^setbyte\s?(\$?[0-9A-F]+)\s?,\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetWordHex(adrValue, out addressSuccess) : GetWordDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    bool byteSuccess;
                    var value = match.Groups[2].Value;
                    var oldByte = Memory.GetByte(adr);
                    var theByte = value.StartsWith("$") ? GetByteHex(value, out byteSuccess) : GetByteDec(value, out byteSuccess);
                    if (!byteSuccess)
                        return "Invalid byte.";
                    Memory.SetByte(adr, theByte);
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {oldByte:000} ${oldByte:X2} --> {theByte:000} ${theByte:X2}";
                }

                //SetWord 400
                match = Regex.Match(input, @"^setword\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool wordSuccess;
                    var value = match.Groups[1].Value;
                    var adr = (ushort)Memory.BytePointer;
                    var oldByte1 = Memory.GetByte(adr);
                    var oldByte2 = Memory.GetByte();
                    var theWord = value.StartsWith("$") ? GetWordHex(value, out wordSuccess) : GetWordDec(value, out wordSuccess);
                    if (!wordSuccess)
                        return "Invalid word.";
                    Memory.SetWord(adr, theWord);
                    success = true;
                    var s = new StringBuilder();
                    var newByte1 = Memory.GetByte(adr);
                    var adr2 = Memory.BytePointer;
                    s.AppendLine($"{adr:00000} ${adr:X4}: {oldByte1:000} ${oldByte1:X2} --> {newByte1:000} ${newByte1:X2}");
                    var newByte2 = Memory.GetByte();
                    s.Append($"{adr2:00000} ${adr2:X4}: {oldByte2:000} ${oldByte2:X2} --> {newByte2:000} ${newByte2:X2}");
                    return s.ToString();
                }

                //SetWord 4096, 400
                match = Regex.Match(input, @"^setword\s?(\$?[0-9A-F]+)\s?,\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetWordHex(adrValue, out addressSuccess) : GetWordDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    bool wordSuccess;
                    var value = match.Groups[2].Value;
                    var oldByte1 = Memory.GetByte(adr);
                    var oldByte2 = Memory.GetByte();
                    var theWord = value.StartsWith("$") ? GetWordHex(value, out wordSuccess) : GetWordDec(value, out wordSuccess);
                    if (!wordSuccess)
                        return "Invalid word.";
                    Memory.SetWord(adr, theWord);
                    success = true;
                    var s = new StringBuilder();
                    var newByte1 = Memory.GetByte(adr);
                    var adr2 = Memory.BytePointer;
                    s.AppendLine($"{adr:00000} ${adr:X4}: {oldByte1:000} ${oldByte1:X2} --> {newByte1:000} ${newByte1:X2}");
                    var newByte2 = Memory.GetByte();
                    s.Append($"{adr2:00000} ${adr2:X4}: {oldByte2:000} ${oldByte2:X2} --> {newByte2:000} ${newByte2:X2}");
                    return s.ToString();
                }

                //M
                match = Regex.Match(input, @"^m$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var s = new StringBuilder();
                    for (var i = 0; i < 16; i++)
                        s.AppendLine(Memory.Visualize());
                    success = true;
                    return s.ToString();
                }

                //M 4096
                match = Regex.Match(input, @"^m\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetWordHex(match.Groups[1].Value, out addressSuccess) : GetWordDec(match.Groups[1].Value, out addressSuccess);
                    if (addressSuccess)
                    {
                        var s = new StringBuilder();
                        s.AppendLine(Memory.Visualize(address));
                        for (var i = 0; i < 15; i++)
                            s.AppendLine(Memory.Visualize());
                        success = true;
                        return s.ToString();
                    }
                    return "Invalid address.";
                }

                //D
                match = Regex.Match(input, @"^d$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var ret = Memory.GetDisassembly(16, true);
                    success = true;
                    return ret;
                }

                //D 4096
                match = Regex.Match(input, @"^d\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetWordHex(value, out addressSuccess) : GetWordDec(value, out addressSuccess);
                    if (addressSuccess)
                    {
                        Memory.SetBytePointer(address);
                        var ret = Memory.GetDisassembly(16, true);
                        success = true;
                        return ret;
                    }
                    return "Invalid address.";
                }

                //GoTo 4096
                match = Regex.Match(input, @"^goto\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    bool addressSuccess;
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetWordHex(value, out addressSuccess) : GetWordDec(value, out addressSuccess);
                    if (addressSuccess)
                    {
                        Memory.SetBytePointer(address);
                        return "";
                    }
                    return "Invalid address.";
                }

                //Load "Filename"
                match = Regex.Match(input, @"^Load ""(.+)""$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var filename = match.Groups[1].Value;
                    try
                    {
                        int startAddress, length;
                        Memory.Load(filename, out startAddress, out length);
                        success = true;
                        return $"{length} bytes loaded to {startAddress} (${startAddress:X}).";
                    }
                    catch (Exception)
                    {
                        return "Load failed.";
                    }
                }

                //Save "Filename"
                match = Regex.Match(input, @"^Save ""(.+)""$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var filename = match.Groups[1].Value;
                    try
                    {
                        int startAddress, length;
                        Memory.Save(filename, out startAddress, out length);
                        success = true;
                        return $"{length} bytes saved from {startAddress} (${startAddress:X}).";
                    }
                    catch (Exception)
                    {
                        return "Save failed.";
                    }
                }

                match = Regex.Match(input, @"^Clear$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Memory.Clear();
                    success = true;
                    return "Memory cleared.";
                }
                return "Unknown command.";
            }
            catch (Exception)
            {
                success = false;
                return "Invalid argument.";
            }
        }
        private ushort GetWordHex(string s, out bool success)
        {
            s = s.StartsWith("$", StringComparison.Ordinal) ? s.Substring(1) : s;
            success = true;
            int result;
            if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (ushort) result;
        }
        private ushort GetWordDec(string s, out bool success)
        {
            success = true;
            int result;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (ushort)result;
        }
        private byte GetByteHex(string s, out bool success)
        {
            s = s.StartsWith("$", StringComparison.Ordinal) ? s.Substring(1) : s;
            success = true;
            int result;
            if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result))
                result = result % (byte.MaxValue + 1);
            else
                success = false;
            return (byte)result;
        }
        private byte GetByteDec(string s, out bool success)
        {
            success = true;
            int result;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out result))
                result = result % (byte.MaxValue + 1);
            else
                success = false;
            return (byte)result;
        }
    }
}

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;

namespace C64MemoryModel
{
    public class TextAdapter
    {
        private Memory Memory { get; }
        public Address BytePointer => Memory.BytePointer;

        public TextAdapter(Memory memory)
        {
            Memory = memory;
        }

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

                //GetByte
                var match = Regex.Match(input, @"^getbyte$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adr = Memory.BytePointer;
                    var theByte = Memory.GetByte();
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {theByte:000} ${theByte:X2}";
                }

                //GetByte 4096
                match = Regex.Match(input, @"^getbyte\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetAddressHex(adrValue, out var addressSuccess) : GetAddressDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var theByte = Memory.GetByte(adr);
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {theByte:000} ${theByte:X2}";
                }

                //GetWord
                match = Regex.Match(input, @"^getword$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adr = Memory.BytePointer;
                    var theWord = Memory.GetWord();
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {theWord:00000} ${theWord:X4}";
                }

                //GetWord 4096
                match = Regex.Match(input, @"^getword\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetWordHex(adrValue, out var addressSuccess) : GetWordDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var theWord = Memory.GetWord((Address)adr);
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {theWord:00000} ${theWord:X4}";
                }

                //SetByte 1
                match = Regex.Match(input, @"^setbyte\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var value = match.Groups[1].Value;
                    var adr = Memory.BytePointer;
                    var oldByte = Memory.GetByte(adr);
                    var theByte = value.StartsWith("$") ? GetByteHex(value, out var byteSuccess) : GetByteDec(value, out byteSuccess);
                    if (!byteSuccess)
                        return "Invalid byte.";
                    Memory.SetByte(adr, theByte);
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {oldByte:000} ${oldByte:X2} --> {theByte:000} ${theByte:X2}";
                }

                //SetByte 4096, 1
                match = Regex.Match(input, @"^setbyte\s?(\$?[0-9A-F]+)\s?,\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetAddressHex(adrValue, out var addressSuccess) : GetAddressDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var value = match.Groups[2].Value;
                    var oldByte = Memory.GetByte(adr);
                    var theByte = value.StartsWith("$") ? GetByteHex(value, out var byteSuccess) : GetByteDec(value, out byteSuccess);
                    if (!byteSuccess)
                        return "Invalid byte.";
                    Memory.SetByte(adr, theByte);
                    success = true;
                    return $"{adr} {adr.ToHexString()}: {oldByte:000} ${oldByte:X2} --> {theByte:000} ${theByte:X2}";
                }

                //SetWord 400
                match = Regex.Match(input, @"^setword\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var value = match.Groups[1].Value;
                    var adr = Memory.BytePointer;
                    var oldByte1 = Memory.GetByte(adr);
                    var oldByte2 = Memory.GetByte();
                    var theWord = value.StartsWith("$") ? GetAddressHex(value, out var wordSuccess) : GetAddressDec(value, out wordSuccess);
                    if (!wordSuccess)
                        return "Invalid word.";
                    Memory.SetWord(adr, theWord);
                    success = true;
                    var s = new StringBuilder();
                    var newByte1 = Memory.GetByte(adr);
                    var adr2 = Memory.BytePointer;
                    s.AppendLine($"{adr} {adr.ToHexString()}: {oldByte1:000} ${oldByte1:X2} --> {newByte1:000} ${newByte1:X2}");
                    var newByte2 = Memory.GetByte();
                    s.Append($"{adr2} {adr2.ToHexString()}: {oldByte2:000} ${oldByte2:X2} --> {newByte2:000} ${newByte2:X2}");
                    return s.ToString();
                }

                //SetWord 4096, 400
                match = Regex.Match(input, @"^setword\s?(\$?[0-9A-F]+)\s?,\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var adrValue = match.Groups[1].Value;
                    var adr = adrValue.StartsWith("$") ? GetAddressHex(adrValue, out var addressSuccess) : GetAddressDec(adrValue, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var value = match.Groups[2].Value;
                    var oldByte1 = Memory.GetByte(adr);
                    var oldByte2 = Memory.GetByte();
                    var theWord = value.StartsWith("$") ? GetWordHex(value, out var wordSuccess) : GetWordDec(value, out wordSuccess);
                    if (!wordSuccess)
                        return "Invalid word.";
                    Memory.SetWord(adr, theWord);
                    success = true;
                    var s = new StringBuilder();
                    var newByte1 = Memory.GetByte(adr);
                    var adr2 = Memory.BytePointer;
                    s.AppendLine($"{adr} {adr.ToHexString()}: {oldByte1:000} ${oldByte1:X2} --> {newByte1:000} ${newByte1:X2}");
                    var newByte2 = Memory.GetByte();
                    s.Append($"{adr2} {adr2.ToHexString()}: {oldByte2:000} ${oldByte2:X2} --> {newByte2:000} ${newByte2:X2}");
                    return s.ToString();
                }

                //SetString $1000, "Hello!"
                match = Regex.Match(input, @"^setstring\s?(\$?[0-9A-F]+)\s?,\s?\""(.*)\""$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        var adrValue = match.Groups[1].Value;
                        var adr = adrValue.StartsWith("$") ? GetAddressHex(adrValue, out var addressSuccess) : GetAddressDec(adrValue, out addressSuccess);
                        if (!addressSuccess)
                            return "Invalid address.";
                        var value = match.Groups[2].Value;
                        if (string.IsNullOrEmpty(value))
                            return "Empty string.";
                        Memory.SetString(adr, Memory.CharacterSets[0], value);
                        success = true;
                        return "Ok.";
                    }
                    catch (SystemException ex)
                    {
                        success = false;
                        return ex.Message;
                    }
                }

                //SetString "Hello!"
                match = Regex.Match(input, @"^setstring\s?\""(.*)\""$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        var value = match.Groups[1].Value;
                        if (string.IsNullOrEmpty(value))
                            return "Empty string.";
                        Memory.SetString(Memory.CharacterSets[0], value);
                        success = true;
                        return "Ok.";
                    }
                    catch (SystemException ex)
                    {
                        success = false;
                        return ex.Message;
                    }
                }

                //GetString $1000, 5
                match = Regex.Match(input, @"^getstring\s?(\$?[0-9A-F]+)\s?,\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        var adrValue = match.Groups[1].Value;
                        var adr = adrValue.StartsWith("$") ? GetAddressHex(adrValue, out var addressSuccess) : GetAddressDec(adrValue, out addressSuccess);
                        if (!addressSuccess)
                            return "Invalid address.";
                        var lenValue = match.Groups[2].Value;
                        var len = lenValue.StartsWith("$") ? GetWordHex(lenValue, out var lengthSuccess) : GetWordDec(lenValue, out lengthSuccess);
                        if (!lengthSuccess)
                            return "Invalid length.";
                        var ret = Memory.GetString(adr, Memory.CharacterSets[0], len);
                        success = true;
                        return ret;
                    }
                    catch (SystemException ex)
                    {
                        success = false;
                        return ex.Message;
                    }
                }

                //GetString 5
                match = Regex.Match(input, @"^getstring\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        var lenValue = match.Groups[1].Value;
                        var len = lenValue.StartsWith("$") ? GetWordHex(lenValue, out var lengthSuccess) : GetWordDec(lenValue, out lengthSuccess);
                        if (!lengthSuccess)
                            return "Invalid length.";
                        var ret = Memory.GetString(Memory.CharacterSets[0], len);
                        success = true;
                        return ret;
                    }
                    catch (SystemException ex)
                    {
                        success = false;
                        return ex.Message;
                    }
                }

                //SetBits 110*01*1
                match = Regex.Match(input, @"^setbits ([01\*][01\*][01\*][01\*][01\*][01\*][01\*][01\*])$");
                if (match.Success)
                {
                    var adr = Memory.GetBytePointer();
                    var oldByte = Memory.GetByte(adr);
                    var change = match.Groups[1].Value;
                    var args = new BitValue[8];
                    for (var i = 0; i < 8; i++)
                        args[i] = change.Substring(i, 1) == "0" ? BitValue.NotSet : change.Substring(i, 1) == "1" ? BitValue.Set : BitValue.Unchanged;
                    Memory.SetBits(adr, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                    var theByte = Memory.GetByte(adr);
                    success = true;
                    return $@"{adr:00000} ${adr:X4}: {new Types.Byte(oldByte)} --> {new Types.Byte(theByte)}
{adr:00000} ${adr:X4}:  {oldByte:000} ${oldByte:X2} -->  {theByte:000} ${theByte:X2}";
                }

                //GetBits
                match = Regex.Match(input, @"^getbits$");
                if (match.Success)
                {
                    var adr = Memory.GetBytePointer();
                    var b = new Types.Byte(Memory.GetByte());
                    success = true;
                    return $"{adr:00000} ${adr:X4}: {b}";
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
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetAddressHex(match.Groups[1].Value, out var addressSuccess) : GetAddressDec(match.Groups[1].Value, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    var s = new StringBuilder();
                    s.AppendLine(Memory.Visualize(address));
                    for (var i = 0; i < 15; i++)
                        s.AppendLine(Memory.Visualize());
                    success = true;
                    return s.ToString();
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
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetAddressHex(value, out var addressSuccess) : GetAddressDec(value, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    Memory.SetBytePointer(address);
                    var ret = Memory.GetDisassembly(16, true);
                    success = true;
                    return ret;
                }

                //GoTo 4096
                match = Regex.Match(input, @"^goto\s?(\$?[0-9A-F]+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var value = match.Groups[1].Value;
                    var address = value.StartsWith("$") ? GetAddressHex(value, out var addressSuccess) : GetAddressDec(value, out addressSuccess);
                    if (!addressSuccess)
                        return "Invalid address.";
                    Memory.SetBytePointer(address);
                    return "";
                }

                //Load "Filename"
                match = Regex.Match(input, @"^Load ""(.+)""$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var filename = match.Groups[1].Value;
                    try
                    {
                        Memory.Load(filename, out var startAddress, out var length);
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
                        Memory.Save(filename, out var startAddress, out var length);
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

        private static Word GetWordHex(string s, out bool success)
        {
            s = s.StartsWith("$", StringComparison.Ordinal) ? s.Substring(1) : s;
            success = true;
            if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (Word)result;
        }

        private static Word GetWordDec(string s, out bool success)
        {
            success = true;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (Word)result;
        }

        private static Address GetAddressHex(string s, out bool success)
        {
            s = s.StartsWith("$", StringComparison.Ordinal) ? s.Substring(1) : s;
            success = true;
            if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (Address)result;
        }

        private static Address GetAddressDec(string s, out bool success)
        {
            success = true;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result))
                result = result % (ushort.MaxValue + 1);
            else
                success = false;
            return (Address)result;
        }

        private static byte GetByteHex(string s, out bool success)
        {
            s = s.StartsWith("$", StringComparison.Ordinal) ? s.Substring(1) : s;
            success = true;
            if (int.TryParse(s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var result))
                result = result % (byte.MaxValue + 1);
            else
                success = false;
            return (byte)result;
        }

        private static byte GetByteDec(string s, out bool success)
        {
            success = true;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result))
                result = result % (byte.MaxValue + 1);
            else
                success = false;
            return (byte)result;
        }
    }
}
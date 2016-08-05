using System;
using System.Text;

namespace C64MemoryModel
{
    internal class Disassembler
    {
        public string GetDisassembly(Memory m, bool withDescription = false)
        {
            var s = new StringBuilder();
            s.Append($". {m.BytePointer:00000} ${m.BytePointer:X4} ");
            var b = m.GetByte();
            s.Append($"{b:X2} ");
            byte b2 = 0, b3 = 0;
            int signedByte;
            Action write0 = () => s.Append($"      ");
            // ReSharper disable once AccessToModifiedClosure
            Action write1 = () => s.Append($"{b2:X2}    ");
            // ReSharper disable once AccessToModifiedClosure
            byte low, high;
            byte[] bytes;
            Action write2 = () => s.Append($"{b2:X2} {b3:X2} ");
            Action<string, string> writeNoArg = (operation, description) => { write0(); s.Append($"{operation}{(withDescription ? $"          ; {description}" : "")}"); };
            Action<string, string> writeAbsolute = (operation, description) => { b2 = m.GetByte(); b3 = m.GetByte(); write2(); s.Append($"{operation} ${b3:X2}{b2:X2}{(withDescription ? $"    ; {description} (Absolute)" : "")}"); };
            Action<string, string, bool> writeJump = (operation, description, indirect) => { b2 = m.GetByte(); b3 = m.GetByte(); write2(); s.Append($"{operation} {(indirect ? "(" : "")}${b3:X2}{b2:X2}{(indirect ? ")" : "")}{(withDescription ? $"{(indirect ? "" : "  ")}  ; {description}" : "")}"); };
            Action<string, string> writeAbsoluteX = (operation, description) => { b2 = m.GetByte(); b3 = m.GetByte(); write2(); s.Append($"{operation} ${b3:X2}{b2:X2},X{(withDescription ? $"  ; {description} (Absolute,X)" : "")}"); };
            Action<string, string> writeAbsoluteY = (operation, description) => { b2 = m.GetByte(); b3 = m.GetByte(); write2(); s.Append($"{operation} ${b3:X2}{b2:X2},Y{(withDescription ? $"  ; {description} (Absolute,Y)" : "")}"); };
            Action<string, string> writeZeroPage = (operation, description) => { b2 = m.GetByte(); write1(); s.Append($"{operation} ${b2:X2}{(withDescription ? $"      ; {description} (Zero Page)" : "")}"); };
            Action<string, string> writeZeroPageX = (operation, description) => { b2 = m.GetByte(); write1(); s.Append($"{operation} ${b2:X2},X{(withDescription ? $"    ; {description} (Zero Page,X)" : "")}"); };
            Action<string, string> writeImmediate = (operation, description) => { b2 = m.GetByte(); write1(); s.Append($"{operation} #${b2:X2}{(withDescription ? $"     ; {description} (Immediate)" : "")}"); };
            Action<string, string> writeIndirektX = (operation, description) => { b2 = m.GetByte(); write1(); s.Append($"ORA (${b2:X2},X){(withDescription ? $"  ; {description} (Indirect,X)" : "")}"); };
            Action<string, string> writeIndirektY = (operation, description) => { b2 = m.GetByte(); write1(); s.Append($"ORA (${b2:X2}),Y{(withDescription ? $"  ; {description} (Indirect),Y" : "")}"); };
            Action<string, string> writeRel = (operation, description) => { b2 = m.GetByte(); signedByte = b2 >= 128 ? b2 - 256 : b2; bytes = BitConverter.GetBytes(m.BytePointer + signedByte); low = bytes[0]; high = bytes[1]; write1(); s.Append($"{operation} ${high:X2}{low:X2}{(withDescription ? $"    ; {description}" : "")}"); };
            switch (b)
            {
                case 0: //00
                    writeNoArg("BRK", "Break"); break;
                case 1: //01
                    writeIndirektX("ORA", "Bitwise OR with Accumulator"); break;
                case 2: //02
                case 3: //03
                case 4: //04
                    writeNoArg("???", "Unknown"); break;
                case 5: //05
                    writeZeroPage("ORA", "Bitwise OR with Accumulator"); break;
                case 6: //06
                    writeZeroPage("ASL", "Arithmetic Shift Left"); break;
                case 7: //07
                    writeNoArg("???", "Unknown"); break;
                case 8: //08
                    writeNoArg("PHP", "Push Processor Status"); break;
                case 9: //09
                    writeImmediate("ORA", "Bitwise OR with Accumulator"); break;
                case 10: //0A
                    writeNoArg("ASL", "Arithmetic Shift Left (Accumulator)"); break;
                case 11: //0B
                case 12: //0C
                    writeNoArg("???", "Unknown"); break;
                case 13: //0D
                    writeAbsolute("ORA", "Bitwise OR with Accumulator"); break;
                case 14: //0E
                    writeAbsolute("ASL", "Arithmetic Shift Left"); break;
                case 15: //0F
                    writeNoArg("???", "Unknown"); break;
                case 16: //10
                    writeRel("BPL", "Branch on Plus"); break;
                case 17: //11
                    writeIndirektY("ORA", "Bitwise OR with Accumulator"); break;
                case 18: //12
                case 19: //13
                case 20: //14
                    writeNoArg("???", "Unknown"); break;
                case 21: //15
                    writeZeroPageX("ORA", "Bitwise OR with Accumulator"); break;
                case 22: //16
                    writeZeroPageX("ASL", "Arithmetic Shift Left"); break;
                case 23: //17
                    writeNoArg("???", "Unknown"); break;
                case 24: //18
                    writeNoArg("CLC", "Clear Carry"); break;
                case 25: //19
                    writeAbsoluteY("ORA", "Bitwise OR with Accumulator"); break;
                case 26: //1A
                case 27: //1B
                case 28: //1C
                    writeNoArg("???", "Unknown"); break;
                case 29: //1D
                    writeAbsoluteX("ORA", "Bitwise OR with Accumulator"); break;
                case 30: //1E
                    writeAbsoluteX("ASL", "Arithmetic Shift Left"); break;
                case 31: //1F
                    writeNoArg("???", "Unknown"); break;
                case 32: //20
                    writeAbsolute("JSR", "Jump to Subroutine"); break;
                case 33: //21
                    writeIndirektX("AND", "Bitwise AND with Accumulator"); break;
                case 34: //22
                case 35: //23
                    writeNoArg("???", "Unknown"); break;
                case 36: //24
                    writeZeroPage("BIT", "Test bits"); break;
                case 37: //25
                    writeZeroPage("AND", "Bitwise AND with Accumulator"); break;
                case 38: //26
                    writeZeroPage("ROL", "Rotate Left"); break;
                case 39: //27
                    writeNoArg("???", "Unknown"); break;
                case 40: //28
                    writeNoArg("PLP", "Pull Processor Status"); break;
                case 41: //29
                    writeImmediate("AND", "Bitwise AND with Accumulator"); break;
                case 42: //2A
                    writeNoArg("ROL", "Rotate Left"); break;
                case 43: //2B
                    writeNoArg("???", "Unknown"); break;
                case 44: //2C
                    writeZeroPage("BIT", "Test bits"); break;
                case 45: //2D
                    writeAbsolute("AND", "Bitwise AND with Accumulator"); break;
                case 46: //2E
                    writeAbsolute("ROL", "Rotate Left"); break;
                case 47: //2F
                    writeNoArg("???", "Unknown"); break;
                case 48: //30
                    writeRel("BMI", "Branch on Minus"); break;
                case 49: //31
                    writeIndirektY("AND", "Bitwise AND with Accumulator"); break;
                case 50: //32
                case 51: //33
                case 52: //34
                    writeNoArg("???", "Unknown"); break;
                case 53: //35
                    writeZeroPageX("AND", "Bitwise AND with Accumulator"); break;
                case 54: //36
                    writeZeroPageX("ROL", "Rotate Left"); break;
                case 55: //37
                    writeNoArg("???", "Unknown"); break;
                case 56: //38
                    writeNoArg("SEC", "Set Carry"); break;
                case 57: //39
                    writeAbsoluteY("AND", "Bitwise AND with Accumulator"); break;
                case 58: //3A
                case 59: //3B
                case 60: //3C
                    writeNoArg("???", "Unknown"); break;
                case 61: //3D
                    writeAbsoluteX("AND", "Bitwise AND with Accumulator"); break;
                case 62: //3E
                    writeAbsoluteX("ROL", "Rotate Left"); break;
                case 63: //3F
                    writeNoArg("???", "Unknown"); break;
                case 64: //40
                    writeNoArg("RTI", "Return from Interrupt"); break;
                case 65: //41
                    writeIndirektX("EOR", "Bitwise Exclusive OR"); break;
                case 66: //42
                case 67: //43
                case 68: //44
                    writeNoArg("???", "Unknown"); break;
                case 69: //45
                    writeZeroPage("EOR", "Bitwise Exclusive OR"); break;
                case 70: //46
                    writeZeroPage("LSR", "Logical Shift Right"); break;
                case 71: //47
                    writeNoArg("???", "Unknown"); break;
                case 72: //48
                    writeNoArg("PHA", "Push Accumulator"); break;
                case 73: //49
                    writeImmediate("EOR", "Bitwise Exclusive OR"); break;
                case 74: //4A
                    writeNoArg("LSR", "Logical Shift Right (Accumulator)"); break;
                case 75: //4B
                    writeNoArg("???", "Unknown"); break;
                case 76: //4C
                    writeJump("JMP", "Absolute Jump", false); break;
                case 77: //4D
                    writeAbsolute("EOR", "Bitwise Exclusive OR"); break;
                case 78: //4E
                    writeAbsolute("LSR", "Logical Shift Right"); break;
                case 79: //4F
                    writeNoArg("???", "Unknown"); break;
                case 80: //50
                    writeRel("BVC", "Branch on Overflow Clear"); break;
                case 81: //51
                    writeIndirektY("EOR", "Bitwise Exclusive OR"); break;
                case 82: //52
                case 83: //53
                case 84: //54
                    writeNoArg("???", "Unknown"); break;
                case 85: //55
                    writeZeroPageX("EOR", "Bitwise Exclusive OR"); break;
                case 86: //56
                    writeZeroPageX("LSR", "Logical Shift Right"); break;
                case 87: //57
                    writeNoArg("???", "Unknown"); break;
                case 88: //58
                    writeNoArg("CLI", "Clear Interrupt"); break;
                case 89: //59
                    writeAbsoluteY("EOR", "Bitwise Exclusive OR"); break;
                case 90: //5A
                case 91: //5B
                case 92: //5C
                    writeNoArg("???", "Unknown"); break;
                case 93: //5D
                    writeAbsoluteX("EOR", "Bitwise Exclusive OR"); break;
                case 94: //5E
                    writeAbsoluteX("LSR", "Logical Shift Right"); break;
                case 95: //5F
                    writeNoArg("???", "Unknown"); break;
                case 96: //60
                    writeNoArg("RTS", "Return from Subroutine"); break;
                case 97: //61
                    writeIndirektX("ADC", "Add with Carry"); break;
                case 98: //62
                case 99: //63
                case 100: //64
                    writeNoArg("???", "Unknown"); break;
                case 101: //65
                    writeZeroPage("ADC", "Add with Carry"); break;
                case 102: //66
                    writeZeroPage("ROR", "Rotate Right"); break;
                case 103: //67
                    writeNoArg("???", "Unknown"); break;
                case 104: //68
                    writeNoArg("PLA", "Pull Accumulator"); break;
                case 105: //69
                    writeImmediate("ADC", "Add with Carry"); break;
                case 106: //6A
                    writeNoArg("ROR", "Rotate Right"); break;
                case 107: //6B
                    writeNoArg("???", "Unknown"); break;
                case 108: //6C
                    writeJump("JMP", "Indirect Jump", true); break;
                case 109: //6D
                    writeAbsolute("ADC", "Add with Carry"); break;
                case 110: //6E
                    writeAbsolute("ROR", "Rotate Right"); break;
                case 111: //6F
                    writeNoArg("???", "Unknown"); break;
                case 112: //70
                    writeRel("BVS", "Branch on Overflow Set"); break;
                case 113: //71
                    writeIndirektY("ADC", "Add with Carry"); break;
                case 114: //72
                case 115: //73
                case 116: //74
                    writeNoArg("???", "Unknown"); break;
                case 117: //75
                    writeZeroPageX("ADC", "Add with Carry"); break;
                case 118: //76
                    writeZeroPageX("ROR", "Rotate Right"); break;
                case 119: //77
                    writeNoArg("???", "Unknown"); break;
                case 120: //78
                    writeNoArg("SEI", "Set Interrupt"); break;
                case 121: //79
                    writeAbsoluteY("ADC", "Add with Carry"); break;
                case 122: //7A
                case 123: //7B
                case 124: //7C
                    writeNoArg("???", "Unknown"); break;
                case 125: //7D
                    writeAbsoluteX("ADC", "Add with Carry"); break;
                case 126: //7E
                    writeAbsoluteX("ROR", "Rotate Right"); break;
                case 127: //7F
                case 128: //80
                    writeNoArg("???", "Unknown"); break;
                case 129: //81
                    writeIndirektX("STA", "Store Accumulator"); break;
                case 130: //82
                case 131: //83
                    writeNoArg("???", "Unknown"); break;
                case 132: //84
                    writeZeroPage("STY", "Store Y Register"); break;
                case 133: //85
                    writeZeroPage("STA", "Store Accumulator"); break;
                case 134: //86
                    writeZeroPage("STX", "Store X Register"); break;
                case 135: //87
                    writeNoArg("???", "Unknown"); break;
                case 136: //88
                    writeNoArg("DEY", "Decrement Y"); break;
                case 137: //89
                    writeNoArg("???", "Unknown"); break;
                case 138: //8A
                    writeNoArg("TXA", "Transfer X to A"); break;
                case 139: //8B
                    writeNoArg("???", "Unknown"); break;
                case 140: //8C
                    writeAbsolute("STY", "Store Y Register"); break;
                case 141: //8D
                    writeAbsolute("STA", "Store Accumulator"); break;
                case 142: //8E
                    writeAbsolute("STX", "Store X Register"); break;
                case 143: //8F
                    writeNoArg("???", "Unknown"); break;
                case 144: //90
                    writeRel("BCC", "Branch on Carry Clear"); break;
                case 145: //91
                    writeIndirektY("STA", "Store Accumulator"); break;
                case 146: //92
                case 147: //93
                    writeNoArg("???", "Unknown"); break;
                case 148: //94
                    writeZeroPageX("STY", "Store Y Register"); break;
                case 149: //95
                    writeZeroPageX("STA", "Store Accumulator"); break;
                case 150: //96
                    writeZeroPageX("STX", "Store X Register"); break;
                case 151: //97
                    writeNoArg("???", "Unknown"); break;
                case 152: //98
                    writeNoArg("TYA", "Transfer Y to A"); break;
                case 153: //99
                    writeAbsoluteY("STA", "Store Accumulator"); break;
                case 154: //9A
                    writeNoArg("TXS", "Transfer X to Stack Pointer"); break;
                case 155: //9B
                case 156: //9C
                    writeNoArg("???", "Unknown"); break;
                case 157: //9D
                    writeAbsoluteX("STA", "Store Accumulator"); break;
                case 158: //9E
                case 159: //9F
                    writeNoArg("???", "Unknown"); break;
                case 160: //A0
                    writeImmediate("LDY", "Load Y Register"); break;
                case 161: //A1
                    writeIndirektX("LDA", "Load Accumulator"); break;
                case 162: //A2
                    writeImmediate("LDX", "Load X Register"); break;
                case 163: //A3
                    writeNoArg("???", "Unknown"); break;
                case 164: //A4
                    writeZeroPage("LDY", "Load Y Register"); break;
                case 165: //A5
                    writeZeroPage("LDA", "Load Accumulator"); break;
                case 166: //A6
                    writeZeroPage("LDX", "Load X Register"); break;
                case 167: //A7
                    writeNoArg("???", "Unknown"); break;
                case 168: //A8
                    writeNoArg("TXA", "Transfer X to A"); break;
                case 169: //A9
                    writeImmediate("LDA", "Load Accumulator"); break;
                case 170: //AA
                    writeNoArg("TAX", "Transfer A to X"); break;
                case 171: //AB
                    writeNoArg("???", "Unknown"); break;
                case 172: //AC
                    writeAbsolute("LDY", "Load Y Register"); break;
                case 173: //AD
                    writeAbsolute("LDA", "Load Accumulator"); break;
                case 174: //AE
                    writeAbsolute("LDX", "Load X Register"); break;
                case 175: //AF
                    writeNoArg("???", "Unknown"); break;
                case 176: //B0
                    writeRel("BNE", "Branch on Carry Set"); break;
                case 177: //B1
                    writeIndirektY("LDA", "Load Accumulator"); break;
                case 178: //B2
                case 179: //B3
                    writeNoArg("???", "Unknown"); break;
                case 180: //B4
                    writeZeroPageX("LDY", "Load Y Register"); break;
                case 181: //B5
                    writeZeroPageX("LDA", "Load Accumulator"); break;
                case 182: //B6
                    writeZeroPageX("LDX", "Load X Register"); break;
                case 183: //B7
                    writeNoArg("???", "Unknown"); break;
                case 184: //B8
                    writeNoArg("CLV", "Clear Overflow"); break;
                case 185: //B9
                    writeAbsoluteY("LDA", "Load Accumulator"); break;
                case 186: //BA
                    writeNoArg("TSX", "Transfer Stack Pointer to X"); break;
                case 187: //BB
                    writeNoArg("???", "Unknown"); break;
                case 188: //BC
                    writeAbsoluteX("LDY", "Load Y Register"); break;
                case 189: //BD
                    writeAbsoluteX("LDA", "Load Accumulator"); break;
                case 190: //BE
                    writeAbsoluteY("LDX", "Load X Register"); break;
                case 191: //BF
                    writeNoArg("???", "Unknown"); break;
                case 192: //C0
                    writeImmediate("CPY", "Compare Y Register"); break;
                case 193: //C1
                    writeIndirektX("CMP", "Compare Accumulator"); break;
                case 194: //C2
                case 195: //C3
                    writeNoArg("???", "Unknown"); break;
                case 196: //C4
                    writeZeroPage("CPY", "Compare Y Register"); break;
                case 197: //C5
                    writeZeroPage("CMP", "Compare Accumulator"); break;
                case 198: //C6
                    writeZeroPage("DEC", "Decrement Memory"); break;
                case 199: //C7
                    writeNoArg("???", "Unknown"); break;
                case 200: //C8
                    writeNoArg("INY", "Increment Y"); break;
                case 201: //C9
                    writeImmediate("CMP", "Compare Accumulator"); break;
                case 202: //CA
                    writeNoArg("DEX", "Decrement X"); break;
                case 203: //CB
                    writeNoArg("???", "Unknown"); break;
                case 204: //CC
                    writeAbsolute("CPY", "Compare Y Register"); break;
                case 205: //CD
                    writeAbsolute("CMP", "Compare Accumulator"); break;
                case 206: //CE
                    writeAbsolute("DEC", "Decrement Memory"); break;
                case 207: //CF
                    writeNoArg("???", "Unknown"); break;
                case 208: //D0
                    writeRel("BNE", "Branch on Not Equal"); break;
                case 209: //D1
                    writeIndirektY("CMP", "Compare Accumulator"); break;
                case 210: //D2
                case 211: //D3
                case 212: //D4
                    writeNoArg("???", "Unknown"); break;
                case 213: //D5
                    writeZeroPageX("CMP", "Compare Accumulator"); break;
                case 214: //D6
                    writeZeroPageX("DEC", "Decrement Memory"); break;
                case 215: //D7
                    writeNoArg("???", "Unknown"); break;
                case 216: //D8
                    writeNoArg("CLD", "Clear Decimal"); break;
                case 217: //D9
                    writeAbsoluteY("CMP", "Compare Accumulator"); break;
                case 218: //DA
                case 219: //DB
                case 220: //DC
                    writeNoArg("???", "Unknown"); break;
                case 221: //DD
                    writeAbsoluteX("CMP", "Compare Accumulator"); break;
                case 222: //DE
                    writeAbsoluteX("DEC", "Decrement Memory"); break;
                case 223: //DF
                    writeNoArg("???", "Unknown"); break;
                case 224: //E0
                    writeImmediate("CPX", "Compare X Register"); break;
                case 225: //E1
                    writeIndirektX("SBC", "Subtract with Carry"); break;
                case 226: //E2
                case 227: //E3
                    writeNoArg("???", "Unknown"); break;
                case 228: //E4
                    writeZeroPage("CPX", "Compare X Register"); break;
                case 229: //E5
                    writeZeroPage("SBC", "Subtract with Carry"); break;
                case 230: //E6
                    writeZeroPage("INC", "Increment Memory"); break;
                case 231: //E7
                    writeNoArg("???", "Unknown"); break;
                case 232: //E8
                    writeNoArg("INX", "Increment X"); break;
                case 233: //E9
                    writeImmediate("SBC", "Subtract with Carry"); break;
                case 234: //EA
                    writeNoArg("NOP", "No Operation"); break;
                case 235: //EB
                    writeNoArg("???", "Unknown"); break;
                case 236: //EC
                    writeAbsolute("CPX", "Compare X Register"); break;
                case 237: //ED
                    writeAbsolute("SBC", "Subtract with Carry"); break;
                case 238: //EE
                    writeAbsolute("INC", "Increment Memory"); break;
                case 239: //EF
                    writeNoArg("???", "Unknown"); break;
                case 240: //F0
                    writeRel("BEQ", "Branch on Equal"); break;
                case 241: //F1
                    writeIndirektY("SBC", "Subtract with Carry"); break;
                case 242: //F2
                case 243: //F3
                case 244: //F4
                    writeNoArg("???", "Unknown"); break;
                case 245: //F5
                    writeZeroPageX("SBC", "Subtract with Carry"); break;
                case 246: //F6
                    writeZeroPageX("INC", "Increment Memory"); break;
                case 247: //F7
                    writeNoArg("???", "Unknown"); break;
                case 248: //F8
                    writeNoArg("SED", "Set Decimal"); break;
                case 249: //F9
                    writeAbsoluteY("SBC", "Subtract with Carry"); break;
                case 250: //FA
                case 251: //FB
                case 252: //FC
                    writeNoArg("???", "Unknown"); break;
                case 253: //FD
                    writeAbsoluteX("SBC", "Subtract with Carry"); break;
                case 254: //FE
                    writeAbsoluteX("INC", "Increment Memory"); break;
                case 255: //FF
                    writeNoArg("???", "Unknown"); break;
                default:
                    throw new SystemException($" --- Unexpected fallthrough: {b:X2} ---");
            }
            s.AppendLine();
            return s.ToString();
        }
    }
}

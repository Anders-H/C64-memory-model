using System;
using System.Text;
using C64MemoryModel.Mem;

namespace C64MemoryModel.Disasm
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
            void Write0() =>
                s.Append("      ");
            void Write1() =>
                s.Append($"{b2:X2}    ");
            byte[] bytes;
            void Write2() =>
                s.Append($"{b2:X2} {b3:X2} ");

            void WriteNoArg(string operation, string description)
            {
                Write0();
                s.Append($"{operation}{(withDescription ? $"          ; {description}" : "")}");
            }

            void WriteAbsolute(string operation, string description)
            {
                b2 = m.GetByte();
                b3 = m.GetByte();
                Write2();
                s.Append($"{operation} ${b3:X2}{b2:X2}{(withDescription ? $"    ; {description} (Absolute)" : "")}");
            }

            void WriteJump(string operation, string description, bool indirect)
            {
                b2 = m.GetByte();
                b3 = m.GetByte();
                Write2();
                s.Append($"{operation} {(indirect ? "(" : "")}${b3:X2}{b2:X2}{(indirect ? ")" : "")}{(withDescription ? $"{(indirect ? "" : "  ")}  ; {description}" : "")}");
            }

            void WriteAbsoluteX(string operation, string description)
            {
                b2 = m.GetByte();
                b3 = m.GetByte();
                Write2();
                s.Append($"{operation} ${b3:X2}{b2:X2},X{(withDescription ? $"  ; {description} (Absolute,X)" : "")}");
            }

            void WriteAbsoluteY(string operation, string description)
            {
                b2 = m.GetByte();
                b3 = m.GetByte();
                Write2();
                s.Append($"{operation} ${b3:X2}{b2:X2},Y{(withDescription ? $"  ; {description} (Absolute,Y)" : "")}");
            }

            void WriteZeroPage(string operation, string description)
            {
                b2 = m.GetByte();
                Write1();
                s.Append($"{operation} ${b2:X2}{(withDescription ? $"      ; {description} (Zero Page)" : "")}");
            }

            void WriteZeroPageX(string operation, string description)
            {
                b2 = m.GetByte();
                Write1();
                s.Append($"{operation} ${b2:X2},X{(withDescription ? $"    ; {description} (Zero Page,X)" : "")}");
            }

            void WriteImmediate(string operation, string description)
            {
                b2 = m.GetByte();
                Write1();
                s.Append($"{operation} #${b2:X2}{(withDescription ? $"     ; {description} (Immediate)" : "")}");
            }

            void WriteIndirektX(string operation, string description)
            {
                b2 = m.GetByte();
                Write1();
                s.Append($"{operation} (${b2:X2},X){(withDescription ? $"  ; {description} (Indirect,X)" : "")}");
            }

            void WriteIndirektY(string operation, string description)
            {
                b2 = m.GetByte();
                Write1();
                s.Append($"{operation} (${b2:X2}),Y{(withDescription ? $"  ; {description} (Indirect),Y" : "")}");
            }

            void WriteRel(string operation, string description)
            {
                b2 = m.GetByte();
                var signedByte = b2 >= 128 ? b2 - 256 : b2;
                bytes = BitConverter.GetBytes((ushort)m.BytePointer + signedByte);
                var low = bytes[0];
                var high = bytes[1];
                Write1();
                s.Append($"{operation} ${high:X2}{low:X2}{(withDescription ? $"    ; {description}" : "")}");
            }

            switch (b)
            {
                case 0: //00
                    WriteNoArg("BRK", "Break"); break;
                case 1: //01
                    WriteIndirektX("ORA", "Bitwise OR with Accumulator"); break;
                case 2: //02
                case 3: //03
                case 4: //04
                    WriteNoArg("???", "Unknown"); break;
                case 5: //05
                    WriteZeroPage("ORA", "Bitwise OR with Accumulator"); break;
                case 6: //06
                    WriteZeroPage("ASL", "Arithmetic Shift Left"); break;
                case 7: //07
                    WriteNoArg("???", "Unknown"); break;
                case 8: //08
                    WriteNoArg("PHP", "Push Processor Status"); break;
                case 9: //09
                    WriteImmediate("ORA", "Bitwise OR with Accumulator"); break;
                case 10: //0A
                    WriteNoArg("ASL", "Arithmetic Shift Left (Accumulator)"); break;
                case 11: //0B
                case 12: //0C
                    WriteNoArg("???", "Unknown"); break;
                case 13: //0D
                    WriteAbsolute("ORA", "Bitwise OR with Accumulator"); break;
                case 14: //0E
                    WriteAbsolute("ASL", "Arithmetic Shift Left"); break;
                case 15: //0F
                    WriteNoArg("???", "Unknown"); break;
                case 16: //10
                    WriteRel("BPL", "Branch on Plus"); break;
                case 17: //11
                    WriteIndirektY("ORA", "Bitwise OR with Accumulator"); break;
                case 18: //12
                case 19: //13
                case 20: //14
                    WriteNoArg("???", "Unknown"); break;
                case 21: //15
                    WriteZeroPageX("ORA", "Bitwise OR with Accumulator"); break;
                case 22: //16
                    WriteZeroPageX("ASL", "Arithmetic Shift Left"); break;
                case 23: //17
                    WriteNoArg("???", "Unknown"); break;
                case 24: //18
                    WriteNoArg("CLC", "Clear Carry"); break;
                case 25: //19
                    WriteAbsoluteY("ORA", "Bitwise OR with Accumulator"); break;
                case 26: //1A
                case 27: //1B
                case 28: //1C
                    WriteNoArg("???", "Unknown"); break;
                case 29: //1D
                    WriteAbsoluteX("ORA", "Bitwise OR with Accumulator"); break;
                case 30: //1E
                    WriteAbsoluteX("ASL", "Arithmetic Shift Left"); break;
                case 31: //1F
                    WriteNoArg("???", "Unknown"); break;
                case 32: //20
                    WriteAbsolute("JSR", "Jump to Subroutine"); break;
                case 33: //21
                    WriteIndirektX("AND", "Bitwise AND with Accumulator"); break;
                case 34: //22
                case 35: //23
                    WriteNoArg("???", "Unknown"); break;
                case 36: //24
                    WriteZeroPage("BIT", "Test bits"); break;
                case 37: //25
                    WriteZeroPage("AND", "Bitwise AND with Accumulator"); break;
                case 38: //26
                    WriteZeroPage("ROL", "Rotate Left"); break;
                case 39: //27
                    WriteNoArg("???", "Unknown"); break;
                case 40: //28
                    WriteNoArg("PLP", "Pull Processor Status"); break;
                case 41: //29
                    WriteImmediate("AND", "Bitwise AND with Accumulator"); break;
                case 42: //2A
                    WriteNoArg("ROL", "Rotate Left"); break;
                case 43: //2B
                    WriteNoArg("???", "Unknown"); break;
                case 44: //2C
                    WriteZeroPage("BIT", "Test bits"); break;
                case 45: //2D
                    WriteAbsolute("AND", "Bitwise AND with Accumulator"); break;
                case 46: //2E
                    WriteAbsolute("ROL", "Rotate Left"); break;
                case 47: //2F
                    WriteNoArg("???", "Unknown"); break;
                case 48: //30
                    WriteRel("BMI", "Branch on Minus"); break;
                case 49: //31
                    WriteIndirektY("AND", "Bitwise AND with Accumulator"); break;
                case 50: //32
                case 51: //33
                case 52: //34
                    WriteNoArg("???", "Unknown"); break;
                case 53: //35
                    WriteZeroPageX("AND", "Bitwise AND with Accumulator"); break;
                case 54: //36
                    WriteZeroPageX("ROL", "Rotate Left"); break;
                case 55: //37
                    WriteNoArg("???", "Unknown"); break;
                case 56: //38
                    WriteNoArg("SEC", "Set Carry"); break;
                case 57: //39
                    WriteAbsoluteY("AND", "Bitwise AND with Accumulator"); break;
                case 58: //3A
                case 59: //3B
                case 60: //3C
                    WriteNoArg("???", "Unknown"); break;
                case 61: //3D
                    WriteAbsoluteX("AND", "Bitwise AND with Accumulator"); break;
                case 62: //3E
                    WriteAbsoluteX("ROL", "Rotate Left"); break;
                case 63: //3F
                    WriteNoArg("???", "Unknown"); break;
                case 64: //40
                    WriteNoArg("RTI", "Return from Interrupt"); break;
                case 65: //41
                    WriteIndirektX("EOR", "Bitwise Exclusive OR"); break;
                case 66: //42
                case 67: //43
                case 68: //44
                    WriteNoArg("???", "Unknown"); break;
                case 69: //45
                    WriteZeroPage("EOR", "Bitwise Exclusive OR"); break;
                case 70: //46
                    WriteZeroPage("LSR", "Logical Shift Right"); break;
                case 71: //47
                    WriteNoArg("???", "Unknown"); break;
                case 72: //48
                    WriteNoArg("PHA", "Push Accumulator"); break;
                case 73: //49
                    WriteImmediate("EOR", "Bitwise Exclusive OR"); break;
                case 74: //4A
                    WriteNoArg("LSR", "Logical Shift Right (Accumulator)"); break;
                case 75: //4B
                    WriteNoArg("???", "Unknown"); break;
                case 76: //4C
                    WriteJump("JMP", "Absolute Jump", false); break;
                case 77: //4D
                    WriteAbsolute("EOR", "Bitwise Exclusive OR"); break;
                case 78: //4E
                    WriteAbsolute("LSR", "Logical Shift Right"); break;
                case 79: //4F
                    WriteNoArg("???", "Unknown"); break;
                case 80: //50
                    WriteRel("BVC", "Branch on Overflow Clear"); break;
                case 81: //51
                    WriteIndirektY("EOR", "Bitwise Exclusive OR"); break;
                case 82: //52
                case 83: //53
                case 84: //54
                    WriteNoArg("???", "Unknown"); break;
                case 85: //55
                    WriteZeroPageX("EOR", "Bitwise Exclusive OR"); break;
                case 86: //56
                    WriteZeroPageX("LSR", "Logical Shift Right"); break;
                case 87: //57
                    WriteNoArg("???", "Unknown"); break;
                case 88: //58
                    WriteNoArg("CLI", "Clear Interrupt"); break;
                case 89: //59
                    WriteAbsoluteY("EOR", "Bitwise Exclusive OR"); break;
                case 90: //5A
                case 91: //5B
                case 92: //5C
                    WriteNoArg("???", "Unknown"); break;
                case 93: //5D
                    WriteAbsoluteX("EOR", "Bitwise Exclusive OR"); break;
                case 94: //5E
                    WriteAbsoluteX("LSR", "Logical Shift Right"); break;
                case 95: //5F
                    WriteNoArg("???", "Unknown"); break;
                case 96: //60
                    WriteNoArg("RTS", "Return from Subroutine"); break;
                case 97: //61
                    WriteIndirektX("ADC", "Add with Carry"); break;
                case 98: //62
                case 99: //63
                case 100: //64
                    WriteNoArg("???", "Unknown"); break;
                case 101: //65
                    WriteZeroPage("ADC", "Add with Carry"); break;
                case 102: //66
                    WriteZeroPage("ROR", "Rotate Right"); break;
                case 103: //67
                    WriteNoArg("???", "Unknown"); break;
                case 104: //68
                    WriteNoArg("PLA", "Pull Accumulator"); break;
                case 105: //69
                    WriteImmediate("ADC", "Add with Carry"); break;
                case 106: //6A
                    WriteNoArg("ROR", "Rotate Right"); break;
                case 107: //6B
                    WriteNoArg("???", "Unknown"); break;
                case 108: //6C
                    WriteJump("JMP", "Indirect Jump", true); break;
                case 109: //6D
                    WriteAbsolute("ADC", "Add with Carry"); break;
                case 110: //6E
                    WriteAbsolute("ROR", "Rotate Right"); break;
                case 111: //6F
                    WriteNoArg("???", "Unknown"); break;
                case 112: //70
                    WriteRel("BVS", "Branch on Overflow Set"); break;
                case 113: //71
                    WriteIndirektY("ADC", "Add with Carry"); break;
                case 114: //72
                case 115: //73
                case 116: //74
                    WriteNoArg("???", "Unknown"); break;
                case 117: //75
                    WriteZeroPageX("ADC", "Add with Carry"); break;
                case 118: //76
                    WriteZeroPageX("ROR", "Rotate Right"); break;
                case 119: //77
                    WriteNoArg("???", "Unknown"); break;
                case 120: //78
                    WriteNoArg("SEI", "Set Interrupt"); break;
                case 121: //79
                    WriteAbsoluteY("ADC", "Add with Carry"); break;
                case 122: //7A
                case 123: //7B
                case 124: //7C
                    WriteNoArg("???", "Unknown"); break;
                case 125: //7D
                    WriteAbsoluteX("ADC", "Add with Carry"); break;
                case 126: //7E
                    WriteAbsoluteX("ROR", "Rotate Right"); break;
                case 127: //7F
                case 128: //80
                    WriteNoArg("???", "Unknown"); break;
                case 129: //81
                    WriteIndirektX("STA", "Store Accumulator"); break;
                case 130: //82
                case 131: //83
                    WriteNoArg("???", "Unknown"); break;
                case 132: //84
                    WriteZeroPage("STY", "Store Y Register"); break;
                case 133: //85
                    WriteZeroPage("STA", "Store Accumulator"); break;
                case 134: //86
                    WriteZeroPage("STX", "Store X Register"); break;
                case 135: //87
                    WriteNoArg("???", "Unknown"); break;
                case 136: //88
                    WriteNoArg("DEY", "Decrement Y"); break;
                case 137: //89
                    WriteNoArg("???", "Unknown"); break;
                case 138: //8A
                    WriteNoArg("TXA", "Transfer X to A"); break;
                case 139: //8B
                    WriteNoArg("???", "Unknown"); break;
                case 140: //8C
                    WriteAbsolute("STY", "Store Y Register"); break;
                case 141: //8D
                    WriteAbsolute("STA", "Store Accumulator"); break;
                case 142: //8E
                    WriteAbsolute("STX", "Store X Register"); break;
                case 143: //8F
                    WriteNoArg("???", "Unknown"); break;
                case 144: //90
                    WriteRel("BCC", "Branch on Carry Clear"); break;
                case 145: //91
                    WriteIndirektY("STA", "Store Accumulator"); break;
                case 146: //92
                case 147: //93
                    WriteNoArg("???", "Unknown"); break;
                case 148: //94
                    WriteZeroPageX("STY", "Store Y Register"); break;
                case 149: //95
                    WriteZeroPageX("STA", "Store Accumulator"); break;
                case 150: //96
                    WriteZeroPageX("STX", "Store X Register"); break;
                case 151: //97
                    WriteNoArg("???", "Unknown"); break;
                case 152: //98
                    WriteNoArg("TYA", "Transfer Y to A"); break;
                case 153: //99
                    WriteAbsoluteY("STA", "Store Accumulator"); break;
                case 154: //9A
                    WriteNoArg("TXS", "Transfer X to Stack Pointer"); break;
                case 155: //9B
                case 156: //9C
                    WriteNoArg("???", "Unknown"); break;
                case 157: //9D
                    WriteAbsoluteX("STA", "Store Accumulator"); break;
                case 158: //9E
                case 159: //9F
                    WriteNoArg("???", "Unknown"); break;
                case 160: //A0
                    WriteImmediate("LDY", "Load Y Register"); break;
                case 161: //A1
                    WriteIndirektX("LDA", "Load Accumulator"); break;
                case 162: //A2
                    WriteImmediate("LDX", "Load X Register"); break;
                case 163: //A3
                    WriteNoArg("???", "Unknown"); break;
                case 164: //A4
                    WriteZeroPage("LDY", "Load Y Register"); break;
                case 165: //A5
                    WriteZeroPage("LDA", "Load Accumulator"); break;
                case 166: //A6
                    WriteZeroPage("LDX", "Load X Register"); break;
                case 167: //A7
                    WriteNoArg("???", "Unknown"); break;
                case 168: //A8
                    WriteNoArg("TXA", "Transfer X to A"); break;
                case 169: //A9
                    WriteImmediate("LDA", "Load Accumulator"); break;
                case 170: //AA
                    WriteNoArg("TAX", "Transfer A to X"); break;
                case 171: //AB
                    WriteNoArg("???", "Unknown"); break;
                case 172: //AC
                    WriteAbsolute("LDY", "Load Y Register"); break;
                case 173: //AD
                    WriteAbsolute("LDA", "Load Accumulator"); break;
                case 174: //AE
                    WriteAbsolute("LDX", "Load X Register"); break;
                case 175: //AF
                    WriteNoArg("???", "Unknown"); break;
                case 176: //B0
                    WriteRel("BNE", "Branch on Carry Set"); break;
                case 177: //B1
                    WriteIndirektY("LDA", "Load Accumulator"); break;
                case 178: //B2
                case 179: //B3
                    WriteNoArg("???", "Unknown"); break;
                case 180: //B4
                    WriteZeroPageX("LDY", "Load Y Register"); break;
                case 181: //B5
                    WriteZeroPageX("LDA", "Load Accumulator"); break;
                case 182: //B6
                    WriteZeroPageX("LDX", "Load X Register"); break;
                case 183: //B7
                    WriteNoArg("???", "Unknown"); break;
                case 184: //B8
                    WriteNoArg("CLV", "Clear Overflow"); break;
                case 185: //B9
                    WriteAbsoluteY("LDA", "Load Accumulator"); break;
                case 186: //BA
                    WriteNoArg("TSX", "Transfer Stack Pointer to X"); break;
                case 187: //BB
                    WriteNoArg("???", "Unknown"); break;
                case 188: //BC
                    WriteAbsoluteX("LDY", "Load Y Register"); break;
                case 189: //BD
                    WriteAbsoluteX("LDA", "Load Accumulator"); break;
                case 190: //BE
                    WriteAbsoluteY("LDX", "Load X Register"); break;
                case 191: //BF
                    WriteNoArg("???", "Unknown"); break;
                case 192: //C0
                    WriteImmediate("CPY", "Compare Y Register"); break;
                case 193: //C1
                    WriteIndirektX("CMP", "Compare Accumulator"); break;
                case 194: //C2
                case 195: //C3
                    WriteNoArg("???", "Unknown"); break;
                case 196: //C4
                    WriteZeroPage("CPY", "Compare Y Register"); break;
                case 197: //C5
                    WriteZeroPage("CMP", "Compare Accumulator"); break;
                case 198: //C6
                    WriteZeroPage("DEC", "Decrement Memory"); break;
                case 199: //C7
                    WriteNoArg("???", "Unknown"); break;
                case 200: //C8
                    WriteNoArg("INY", "Increment Y"); break;
                case 201: //C9
                    WriteImmediate("CMP", "Compare Accumulator"); break;
                case 202: //CA
                    WriteNoArg("DEX", "Decrement X"); break;
                case 203: //CB
                    WriteNoArg("???", "Unknown"); break;
                case 204: //CC
                    WriteAbsolute("CPY", "Compare Y Register"); break;
                case 205: //CD
                    WriteAbsolute("CMP", "Compare Accumulator"); break;
                case 206: //CE
                    WriteAbsolute("DEC", "Decrement Memory"); break;
                case 207: //CF
                    WriteNoArg("???", "Unknown"); break;
                case 208: //D0
                    WriteRel("BNE", "Branch on Not Equal"); break;
                case 209: //D1
                    WriteIndirektY("CMP", "Compare Accumulator"); break;
                case 210: //D2
                case 211: //D3
                case 212: //D4
                    WriteNoArg("???", "Unknown"); break;
                case 213: //D5
                    WriteZeroPageX("CMP", "Compare Accumulator"); break;
                case 214: //D6
                    WriteZeroPageX("DEC", "Decrement Memory"); break;
                case 215: //D7
                    WriteNoArg("???", "Unknown"); break;
                case 216: //D8
                    WriteNoArg("CLD", "Clear Decimal"); break;
                case 217: //D9
                    WriteAbsoluteY("CMP", "Compare Accumulator"); break;
                case 218: //DA
                case 219: //DB
                case 220: //DC
                    WriteNoArg("???", "Unknown"); break;
                case 221: //DD
                    WriteAbsoluteX("CMP", "Compare Accumulator"); break;
                case 222: //DE
                    WriteAbsoluteX("DEC", "Decrement Memory"); break;
                case 223: //DF
                    WriteNoArg("???", "Unknown"); break;
                case 224: //E0
                    WriteImmediate("CPX", "Compare X Register"); break;
                case 225: //E1
                    WriteIndirektX("SBC", "Subtract with Carry"); break;
                case 226: //E2
                case 227: //E3
                    WriteNoArg("???", "Unknown"); break;
                case 228: //E4
                    WriteZeroPage("CPX", "Compare X Register"); break;
                case 229: //E5
                    WriteZeroPage("SBC", "Subtract with Carry"); break;
                case 230: //E6
                    WriteZeroPage("INC", "Increment Memory"); break;
                case 231: //E7
                    WriteNoArg("???", "Unknown"); break;
                case 232: //E8
                    WriteNoArg("INX", "Increment X"); break;
                case 233: //E9
                    WriteImmediate("SBC", "Subtract with Carry"); break;
                case 234: //EA
                    WriteNoArg("NOP", "No Operation"); break;
                case 235: //EB
                    WriteNoArg("???", "Unknown"); break;
                case 236: //EC
                    WriteAbsolute("CPX", "Compare X Register"); break;
                case 237: //ED
                    WriteAbsolute("SBC", "Subtract with Carry"); break;
                case 238: //EE
                    WriteAbsolute("INC", "Increment Memory"); break;
                case 239: //EF
                    WriteNoArg("???", "Unknown"); break;
                case 240: //F0
                    WriteRel("BEQ", "Branch on Equal"); break;
                case 241: //F1
                    WriteIndirektY("SBC", "Subtract with Carry"); break;
                case 242: //F2
                case 243: //F3
                case 244: //F4
                    WriteNoArg("???", "Unknown"); break;
                case 245: //F5
                    WriteZeroPageX("SBC", "Subtract with Carry"); break;
                case 246: //F6
                    WriteZeroPageX("INC", "Increment Memory"); break;
                case 247: //F7
                    WriteNoArg("???", "Unknown"); break;
                case 248: //F8
                    WriteNoArg("SED", "Set Decimal"); break;
                case 249: //F9
                    WriteAbsoluteY("SBC", "Subtract with Carry"); break;
                case 250: //FA
                case 251: //FB
                case 252: //FC
                    WriteNoArg("???", "Unknown"); break;
                case 253: //FD
                    WriteAbsoluteX("SBC", "Subtract with Carry"); break;
                case 254: //FE
                    WriteAbsoluteX("INC", "Increment Memory"); break;
                case 255: //FF
                    WriteNoArg("???", "Unknown"); break;
                default:
                    throw new SystemException($" --- Unexpected fallthrough: {b:X2} ---");
            }
            s.AppendLine();
            return s.ToString();
        }
    }
}
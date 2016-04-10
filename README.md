#Commodore 64 memory model
Create and edit .prg files.

##Load .prg files
```C#
var m = new Memory();
int startAddress, length;
m.Load(@"C:\Temp\MyProgram.prg", out startAddress, out length);
Console.WriteLine($"{length} bytes loaded to {startAddress}.");
```

##Create machine code programs
A C64 program that makes the border purple.
```C#
m.Clear();
m.SetBytePointer(4096); //Start address
m.SetByte(169); //LDA
m.SetByte(4); //Purple
m.SetByte(141); //STA
m.SetWord(53280); //Border color
m.SetByte(96); //RTS
```

##Save .prg files
```C#
m.Save(@"C:\Temp\ChangeCol.prg", out startAddress, out length);
Console.WriteLine($"{length} bytes saved from {startAddress}.");
```

##Disassemble programs
```C#
m.SetBytePointer(4096);
Console.WriteLine(m.GetDisassembly(3, true));
```

Output:
```
. 04096 $1000 A9 04    LDA #$04     ; Load Accumulator (Immediate)
. 04098 $1002 8D 20 D0 STA $D020    ; Store Accumulator (Absolute)
. 04101 $1005 60       RTS          ; Return from Subroutine
```

##Bookmarks and locations
A C64 program that makes the border purple.
```C#
m.Clear();
m.SetBytePointer(4096); //Start address
m.SetBytes(new MemoryBookmark(4096), 169, 4, 141); //LDA Purple STA
m.SetWord(m.GetModelLocation(MemoryModelLocationName.BorderColor));
m.SetByte(96); //RTS
```

##String support
```C#
m.SetBytePointer(8192);
m.SetString("simpleUppercase", "Hello, Computer!");
m.SetBytePointer(8192);
Console.WriteLine(m.GetString("simpleUppercase", 16));
```

Output:
```
HELLO, COMPUTER!
```

##Hello, World!
The Hello World program for the C64.
```C#
//The program.
m.SetBytes(new MemoryBookmark(4096), 162, 0, 189, 14, 16, 240, 6, 32, 210, 255, 232, 208, 245, 96);
//The data at 4110.
m.SetString(m.CharacterSets[0], "Hello, world!");
m.SetByte(0); //Terminate string at 4123.
m.SetByte(1); //Last byte saved can not be 0, it will be trimmed away.
//Save the program.     
int startAddress, length;
m.Save(@"C:\Temp\helloworld.prg", out startAddress, out length);
//Show the disassembly.
m.SetBytePointer(4096);
Console.WriteLine(m.GetDisassembly(7));
```

Output:
```
. 04096 $1000 A2 00    LDX #$00
. 04098 $1002 BD 0E 10 LDA $100E,X
. 04101 $1005 F0 06    BEQ $100D
. 04103 $1007 20 D2 FF JSR $FFD2
. 04106 $100A E8       INX
. 04107 $100B D0 F5    BNE $1002
. 04109 $100D 60       RTS
```

Result:

![Hello, World!](http://imghost.winsoft.se/upload/270571459008119c64helloworld.jpg)

Same as above, with the assembler:
```C#
//The program.
m.SetBytePointer(4096);
m.Assembler.Ldx(0);
m.Assembler.LdaAbsX(4110);
m.Assembler.Beq(4109);
m.Assembler.Jsr(65490);
m.Assembler.Inx();
m.Assembler.Bne(4098);
m.Assembler.Rts();
//The data at 4110.
m.SetString(m.CharacterSets[0], "Hello, world!");
m.SetByte(0); //Terminate string at 4123.
m.SetByte(1); //Last byte saved can not be 0, it will be trimmed away.
//Save the program.     
m.Save(@"C:\Temp\helloworld.prg");
//Show the disassembly.
m.SetBytePointer(4096);
Console.WriteLine(m.GetDisassembly(7));
```

##Sprites
Turn on two sprites and position the first:
```C#
//Turn on first and second sprite.
var b = new C64MemoryModel.Byte(false, false, false, false, false, false, true, true);
m.SetBytes(new MemoryBookmark(4096), 169, b.ToByte()); //LDA #$03
m.SetByte(141); //STA
m.SetWord(m.GetModelLocation(MemoryModelLocationName.SpriteEnableRegister));
//Position the first sprite at 128, 128.
m.SetByte(169); //LDA
m.SetByte(128); //#$80
m.SetByte(141); //STA
m.SetWord(m.GetModelLocation(MemoryModelLocationName.SpriteLocations));
m.SetByte(141); //STA
m.SetWord(m.GetModelLocation(MemoryModelLocationName.SpriteLocations) + 1);
m.SetByte(96); //RTS
```

Same as above, with the assembler:
```C#
//Turn on first and second sprite.
var b = new C64MemoryModel.Byte(false, false, false, false, false, false, true, true);
m.SetBytePointer(4096);
m.Assembler.Lda(b.ToByte());
m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteEnableRegister));
//Position the first sprite at 128, 128.
m.Assembler.Lda(128);
m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteLocations));
m.Assembler.Sta(m.GetModelLocation(MemoryModelLocationName.SpriteLocations) + 1);
m.Assembler.Rts();
```

##Extended assembler
Using byte variables:
```C#
//A program that sets the backgroud color to red.
var v = m.Assembler.Extended.CreateByteVariable(MemoryModelLocationName.BackgroundColor);
v.WriteAssign(4096, 2);
m.Assembler.Rts();
```
Disassembly:
```
. 04096 $1000 A9 02    LDA #$02     ; Load Accumulator (Immediate)
. 04098 $1002 8D 21 D0 STA $D021    ; Store Accumulator (Absolute)
. 04101 $1005 60       RTS          ; Return from Subroutine
```

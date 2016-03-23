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
```C#
m.Clear();
m.SetBytePointer(4096); //Start address
m.SetByte(169); //LDA
m.SetByte(4); //Purple
m.SetByte(141); //STA
m.SetWord(53280); //Border color
m.SetByte(96); //RTS
```

#Save .prg files
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

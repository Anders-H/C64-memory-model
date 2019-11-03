using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ThePetscii
{
    public class CodeGenerator
    {
        private PetsciiImage _image;

        public CodeGenerator(PetsciiImage image)
        {
            _image = image;
        }

        public string GetBasic()
        {
            var s = new StringBuilder();
            var lineNumber = 10;
            void Add(string c)
            {
                s.AppendLine($"{lineNumber} {c}");
                lineNumber += 10;
            }
            //Read characters.
            Add("REM CHARACTERS");
            Add("FOR I=1024 TO 2023");
            Add("READ A:POKE I,A");
            Add("NEXT");
            //Read fore color.
            Add("REM FORE COLOR");
            Add("FOR I=55296 TO 56295");
            Add("READ A:POKE I,A");
            Add("NEXT");
            //Character data
            for (var y = 0; y < 25; y++)
            {
                Add($"REM CHARACTER DATA ROW {y}({y+1}/25)");
                for (var x = 0; x < 40; x++)
                {
                    if (x == 0 || x == 10 || x == 20 || x == 30)
                    {
                        s.Append($"{lineNumber} DATA ");
                        lineNumber += 10;
                    }
                    if (x == 9 || x == 19 || x == 29 || x == 39)
                    {
                        s.AppendLine(_image.GetChar(x, y).GetByte().ToString());
                    }
                    else
                    {
                        s.Append(_image.GetChar(x, y).GetByte().ToString());
                        s.Append(",");
                    }
                }
            }
            //Fore color data
            for (var y = 0; y < 25; y++)
            {
                Add($"REM FORE COLOR DATA ROW {y}({y+1}/25)");
                for (var x = 0; x < 40; x++)
                {
                    if (x == 0 || x == 10 || x == 20 || x == 30)
                    {
                        s.Append($"{lineNumber} DATA ");
                        lineNumber += 10;
                    }
                    if (x == 9 || x == 19 || x == 29 || x == 39)
                    {
                        s.AppendLine(_image.Foreground.GetColorByte(x, y).ToString());
                    }
                    else
                    {
                        s.Append(_image.Foreground.GetColorByte(x, y).ToString());
                        s.Append(",");
                    }
                }
            }
            Add("REM PRESS ANY KEY TO EXIT");
            Add("POKE 198,0: WAIT 198,1");
            return s.ToString();
        }
    }
}
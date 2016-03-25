using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel
{
    public class CharacterSetList : List<CharacterSetBase>
    {
        public CharacterSetBase GetCharacterSet(string name) => this.FirstOrDefault(x => string.Compare(name, x.Name, true, System.Globalization.CultureInfo.CurrentCulture) == 0);
    }
}

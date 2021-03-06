﻿using System.Collections.Generic;
using System.Linq;

namespace C64MemoryModel.Chr
{
    public class CharacterSetList : List<CharacterSetBase>
    {
        public CharacterSetBase GetCharacterSet(string name) =>
            this.FirstOrDefault(x =>
                string.Compare(name, x.Name, true, System.Globalization.CultureInfo.CurrentCulture) == 0);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C64MemoryModel.Types;

namespace C64MemoryModel.Mem
{
    public class MemoryModelLocationList : List<MemoryModelLocation>
    {
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine("START-END   (LEN ) : NAME");
            s.AppendLine(new string('=', 78));
            ForEach(x => s.AppendLine(x.ToString()));
            return s.ToString();
        }

        public MemoryModelLocation GetLocation(string name)
            => this.FirstOrDefault(x =>
                string.Compare(name, x.DisplayName, StringComparison.CurrentCultureIgnoreCase) == 0);

        public MemoryModelLocation GetLocation(MemoryModelLocationName name)
            => this.FirstOrDefault(x => name == x.Name);

        public MemoryModelLocation GetLocation(Address address)
            => this.FirstOrDefault(x => x.HitTest(address));

        public List<IMemoryLocation> GetAll(Address address) =>
            this.Where(x => x.HitTest(address)).Cast<IMemoryLocation>().ToList();

        public List<IMemoryLocation> GetAll(string name) =>
            this.Where(x => string.Compare(name, x.DisplayName, StringComparison.CurrentCultureIgnoreCase) == 0)
                .Cast<IMemoryLocation>().ToList();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C64MemoryModel;

namespace Sprdef
{
    public class UndoBuffer
    {
        private List<C64Sprite[]> Buffer { get; } = new List<C64Sprite[]>();
        private int UndoPointer { get; set; } = -1;
        public bool CanUndo { get; set; }
        public bool CanRedo { get; set; }
        public void PushState(C64Sprite[] sprites)
        {
            
        }
        public C64Sprite[] Undo()
        {
            return null;
        }
        public C64Sprite[] Redo()
        {
            return null;
        }
    }
}

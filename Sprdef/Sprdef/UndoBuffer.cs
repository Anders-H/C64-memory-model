using System.Collections.Generic;

namespace Sprdef
{
    public class UndoBuffer
    {
        private List<SpriteArray> Buffer { get; } = new List<SpriteArray>();
        private int UndoPointer { get; set; } = -1;
        public bool CanUndo { get; set; }
        public bool CanRedo { get; set; }

        public void PushState(SpriteArray sprites)
        {
            while (Buffer.Count > 0 && UndoPointer < Buffer.Count - 1)
                Buffer.RemoveAt(Buffer.Count - 1);
            Buffer.Add(sprites);
            UndoPointer = Buffer.Count - 1;
        }

        public SpriteArray Undo()
        {
            return null;
        }

        public SpriteArray Redo()
        {
            return null;
        }
    }
}
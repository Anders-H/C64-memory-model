using System;
using System.Collections.Generic;

namespace Sprdef
{
    public class UndoBuffer
    {
        private List<C64Sprite> Buffer { get; } = new List<C64Sprite>();
        private int UndoPointer { get; set; } = -1;

        public bool CanUndo =>
            Buffer.Count > 0 && UndoPointer > -1;

        public bool CanRedo =>
            Buffer.Count > 0 && UndoPointer < Buffer.Count - 1;

        public void PushState(C64Sprite sprite)
        {
            while (UndoPointer < Buffer.Count - 1)
                Buffer.RemoveAt(Buffer.Count - 1);
            Buffer.Add(sprite.Clone());
            while (Buffer.Count > 100)
                Buffer.RemoveAt(0);
            UndoPointer = Buffer.Count - 1;
        }

        public C64Sprite Undo()
        {
            if (!CanUndo)
                throw new SystemException();
            var result = Buffer[UndoPointer];
            UndoPointer--;
            return result;
        }

        public C64Sprite Redo()
        {
            if (!CanRedo)
                throw new SystemException();
            UndoPointer++;
            return Buffer[UndoPointer];
        }
    }
}
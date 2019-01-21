using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprdef
{
    public class UndoBuffer
    {
        private List<C64Sprite> Buffer { get; } = new List<C64Sprite>();
        private int UndoPointer { get; set; } = -1;

        private bool NeedsCurrentState(C64Sprite currentState) =>
            UndoPointer == Buffer.Count - 1 && !HasState(currentState);

        private bool HasState(C64Sprite currentState) =>
            Buffer.Count > 0 && Buffer.Last().CompareTo(currentState);

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
            System.Diagnostics.Debug.WriteLine(Buffer.Count + " - " + UndoPointer);
        }

        public C64Sprite Undo(C64Sprite currentState)
        {
            if (!CanUndo)
                throw new SystemException();
            if (NeedsCurrentState(currentState))
                Buffer.Add(currentState.Clone());
            var result = Buffer[UndoPointer];
            UndoPointer--;
            if (result.CompareTo(currentState))
            {
                result = Buffer[UndoPointer];
                UndoPointer--;
            }
            System.Diagnostics.Debug.WriteLine(Buffer.Count + " - " + UndoPointer);
            return result;
        }

        public C64Sprite Redo()
        {
            if (!CanRedo)
                throw new SystemException();
            if (UndoPointer < 0)
                UndoPointer++;
            UndoPointer++;
            System.Diagnostics.Debug.WriteLine(Buffer.Count + " - " + UndoPointer);
            return Buffer[UndoPointer];
        }
    }
}
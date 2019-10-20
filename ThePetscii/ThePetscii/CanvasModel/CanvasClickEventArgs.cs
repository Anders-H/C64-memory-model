using System;

namespace ThePetscii.CanvasModel
{
    public class CanvasClickEventArgs : EventArgs
    {
        public int PhysicalX { get; private set; }
        public int PhysicalY { get; private set; }
        public int CharacterX { get; private set; }
        public int CharacterY { get; private set; }
        public int SubCharacterX { get; private set; }        
        public int SubCharacterY { get; private set; }

        public CanvasClickEventArgs(int scale, int physicalX, int physicalY)
        {
            PhysicalX = physicalX;
            PhysicalY = physicalY;
            CharacterX = PhysicalX / scale;
            CharacterY = PhysicalY / scale;
            var halfScale = scale / 2;
            var mx = PhysicalX % scale;
            var my = PhysicalY % scale;
            SubCharacterX = mx >= halfScale ? 1 : 0;
            SubCharacterY = my >= halfScale ? 1 : 0;
        }
    }
}
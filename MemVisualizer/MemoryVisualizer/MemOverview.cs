using System;
using System.Drawing;
using C64MemoryModel.Mem;

namespace MemoryVisualizer
{
    public class MemOverview
    {
        private int[] Data { get; set; }
        public static MemOverview Create(Memory memory, int height)
        {
            //Create the overview.
            var x = new MemOverview {
                Data = new int[height]
            };
            //Initialize
            var stepFactor = 1;
            if (height < ushort.MaxValue)
                stepFactor = (int)Math.Ceiling((double)ushort.MaxValue/height);
            var maxValue = 255*stepFactor;
            //TODO!
            return x;
        }
        public void Draw(Graphics g, int displayPointer)
        {

        }
    }
}
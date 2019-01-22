using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Sprdef
{
    public class SpriteArray : IEnumerable<C64Sprite>
    {
        private readonly List<C64Sprite> _sprites = new List<C64Sprite>();

        public SpriteArray()
        {
            for(var i = 0; i < Length; i++)
                _sprites.Add(null);
        }

        public C64Sprite this[int i]
        {
            get => _sprites[i];
            set => _sprites[i] = value;
        }

        public static int Length { get; } = 8;

        public int Count => Length;

        public IEnumerator<C64Sprite> GetEnumerator() =>
            _sprites.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _sprites.GetEnumerator();

        public int SpriteWidth => 24;

        public int SpriteHeight => 21;

        public int TotalWidth => 24*Length;

        public int TotalMultiColorWidth => TotalWidth/2;

        public void WriteBytes(BinaryWriter w)
        {
            foreach (var sprite in this)
                w.Write(sprite.GetBytes());
        }
    }
}
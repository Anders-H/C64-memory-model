using System.Collections.Generic;

namespace Sprdef
{
	public class UndoBuffer
	{
		private List<C64Sprite> Buffer { get; } = new List<C64Sprite>();
		public bool CanUndo { get; set; }
		public bool CanRedo { get; set; }

		public void PushState(C64Sprite sprite)
		{
			Buffer.Add(sprite);
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
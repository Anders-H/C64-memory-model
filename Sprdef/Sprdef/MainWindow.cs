using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
namespace Sprdef
{
    public partial class MainWindow : Form, ISpriteEditorWindow
    {
        private C64Sprite[] Sprites { get; } = new C64Sprite[8];
        private Rectangle[] SpriteThumb
        private int CurrentSpriteIndex { get; set; } = 0;
        private C64Sprite CurrentSprite => Sprites[CurrentSpriteIndex];
        private SpriteEditor SpriteEditor { get; }
        private int EditorX { get; set; }
        private int EditorY { get; set; }
        private bool RedrawBackgroundFlag { get; set; } = true;
        private ColorPicker ColorPicker { get; }

        public MainWindow()
        {
            SpriteEditor = new SpriteEditor(this);
            ColorPicker = new ColorPicker(C64Sprite.C64Palette[0], C64Sprite.C64Palette[1], C64Sprite.C64Palette[2], C64Sprite.C64Palette[3]);
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Sprites.Length; i++)
                Sprites[i] = new C64Sprite();
            sprite1ToolStripMenuItem.Click += PickSpriteClick;
            sprite2ToolStripMenuItem.Click += PickSpriteClick;
            sprite3ToolStripMenuItem.Click += PickSpriteClick;
            sprite4ToolStripMenuItem.Click += PickSpriteClick;
            sprite5ToolStripMenuItem.Click += PickSpriteClick;
            sprite6ToolStripMenuItem.Click += PickSpriteClick;
            sprite7ToolStripMenuItem.Click += PickSpriteClick;
            sprite8ToolStripMenuItem.Click += PickSpriteClick;
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            if (RedrawBackgroundFlag)
            {
                e.Graphics.Clear(BackColor);
                RedrawBackgroundFlag = false;
            }
            EditorX = 10;
            EditorY = ((Height/2) - (SpriteEditor.InnerHeight/2)) - menuStrip1.Height;
            var pw = ((Width - 200)/24);
            var ph = ((Height - 200)/21);
            SpriteEditor.PixelSize = Math.Min(pw, ph);
            SpriteEditor.PixelSize = SpriteEditor.PixelSize < 6 ? 6 : SpriteEditor.PixelSize;

            var doubleSize = (Width*1.5) > Height;
            var spritesHeight = doubleSize ? 336 : 168;
            var startX = doubleSize ? Width - 68 : Width - 44;
            var startY = ((Height / 2) - (spritesHeight / 2)) - menuStrip1.Height; 
            for (var i = 0; i < 8; i++)
            {
                Sprites[i].Draw(e.Graphics, startX, startY, doubleSize);
                startY += doubleSize ? 42 : 21;
            }

            SpriteEditor.Draw(e.Graphics, EditorX, EditorY);
            ColorPicker.ColorCell.Size = SpriteEditor.PixelSize*2;
            ColorPicker.Draw(e.Graphics, EditorX, EditorY - (ColorPicker.ColorCell.Size + 8), false);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            SpriteEditor.Sprite = CurrentSprite;
            Action x = DelayedRedraw;
            x.BeginInvoke(null, null);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            RedrawBackgroundFlag = true;
            Invalidate();
        }


        protected override void WndProc(ref Message m)
        {
            // Redraw on window state change.
            var org = WindowState;
            base.WndProc(ref m);
            if (WindowState != org)
            {
                Action x = DelayedRedraw;
                x.BeginInvoke(null, null);
            }
        }

        private void DelayedRedraw()
        {
            System.Threading.Thread.Sleep(20);
            Invalidate();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // ReSharper disable once ImplicitlyCapturedClosure
            Action handled = () => { e.Handled = true; e.SuppressKeyPress = true; Invalidate(); };
            Action<int> setCol = i => { SpriteEditor.SetPixelAtCursor(i); ColorPicker.SelectedColor = i; e.Handled = true; e.SuppressKeyPress = true; Invalidate(); };
            switch (e.KeyCode)
            {
                case Keys.Up:
                    SpriteEditor.MoveCursor(0, -1); handled();
                    break;
                case Keys.Down:
                    SpriteEditor.MoveCursor(0, 1); handled();
                    break;
                case Keys.Left:
                    SpriteEditor.MoveCursor(-1, 0); handled();
                    break;
                case Keys.Right:
                    SpriteEditor.MoveCursor(1, 0); handled();
                    break;
                case Keys.D1:
                    setCol(0);
                    break;
                case Keys.D2:
                    setCol(1);
                    break;
                case Keys.D3:
                    if (!SpriteEditor.Multicolor)
                        return;
                    setCol(2);
                    break;
                case Keys.D4:
                    if (!SpriteEditor.Multicolor)
                        return;
                    setCol(3);
                    break;
            }
        }

        private void PickSpriteClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            sprite1ToolStripMenuItem.Checked = false;
            sprite2ToolStripMenuItem.Checked = false;
            sprite3ToolStripMenuItem.Checked = false;
            sprite4ToolStripMenuItem.Checked = false;
            sprite5ToolStripMenuItem.Checked = false;
            sprite6ToolStripMenuItem.Checked = false;
            sprite7ToolStripMenuItem.Checked = false;
            sprite8ToolStripMenuItem.Checked = false;
            Debug.Assert(item != null, "item != null");
            item.Checked = true;
            if (item == sprite1ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[0]; spritesToolStripMenuItem.Text = @"Sprite 1/8"; }
            else if (item == sprite2ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[1]; spritesToolStripMenuItem.Text = @"Sprite 2/8"; }
            else if (item == sprite3ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[2]; spritesToolStripMenuItem.Text = @"Sprite 3/8"; }
            else if (item == sprite4ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[3]; spritesToolStripMenuItem.Text = @"Sprite 4/8"; }
            else if (item == sprite5ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[4]; spritesToolStripMenuItem.Text = @"Sprite 5/8"; }
            else if (item == sprite6ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[5]; spritesToolStripMenuItem.Text = @"Sprite 6/8"; }
            else if (item == sprite7ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[6]; spritesToolStripMenuItem.Text = @"Sprite 7/8"; }
            else if (item == sprite8ToolStripMenuItem) { SpriteEditor.Sprite = Sprites[7]; spritesToolStripMenuItem.Text = @"Sprite 8/8"; }
            Invalidate();
        }

        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            var screenThing = GetScreenThing(e.X, e.Y);
            if (screenThing is C64Sprite)
            {
                var s = screenThing as C64Sprite;
                if ((e.Button & MouseButtons.Left) > 0)
                {
                    //Select color.
                }
                else if ((e.Button & MouseButtons.Right) > 0)
                {
                    //Modify palette.
                }
            }
        }

        private IScreenThing GetScreenThing(int x, int y)
        {
            for (var i = 0; i < 8; i++)
                if (Sprites[i].HitTest(x, y))
                    return Sprites[i];
            return null;
        }
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text;

namespace Sprdef
{
    public partial class MainWindow : Form, ISpriteEditorWindow
    {
        private UndoBuffer[] UndoBuffers { get; set; }
        private SpriteArray Sprites { get; } = new SpriteArray();
        private int CurrentSpriteIndex { get; } = 0;
        private SpriteEditor SpriteEditor { get; }
        private bool RedrawBackgroundFlag { get; set; } = true;
        private ColorPicker ColorPicker { get; }
        private bool Active { get; set; }
        private string Filename { get; set; }

        public MainWindow()
        {
            SpriteEditor = new SpriteEditor();
            ColorPicker = new ColorPicker();
            Filename = "";
            InitializeComponent();
            CreateUndoBuffers();
        }

        private void CreateUndoBuffers()
        {
            UndoBuffers = new UndoBuffer[8];
            for (var i = 0; i < Sprites.Count; i++)
                UndoBuffers[i] = new UndoBuffer();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < Sprites.Count; i++)
                Sprites[i] = new C64Sprite();
            sprite1ToolStripMenuItem.Click += PickSpriteClick;
            sprite2ToolStripMenuItem.Click += PickSpriteClick;
            sprite3ToolStripMenuItem.Click += PickSpriteClick;
            sprite4ToolStripMenuItem.Click += PickSpriteClick;
            sprite5ToolStripMenuItem.Click += PickSpriteClick;
            sprite6ToolStripMenuItem.Click += PickSpriteClick;
            sprite7ToolStripMenuItem.Click += PickSpriteClick;
            sprite8ToolStripMenuItem.Click += PickSpriteClick;

            switch (Configuration.InputMethod)
            {
                case InputMethod.MouseInputMethod:
                    mouseToolStripMenuItem.Checked = true;
                    btnInputMouse.Checked = true;
                    keyboardToolStripMenuItem.Checked = false;
                    btnInputKeyboard.Checked = false;
                    break;
                case InputMethod.KeyboardInputMethod:
                    mouseToolStripMenuItem.Checked = false;
                    btnInputMouse.Checked = false;
                    keyboardToolStripMenuItem.Checked = true;
                    btnInputKeyboard.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            SpriteEditor.X = 10;
            SpriteEditor.Y = 10 + ColorPicker.ColorCell.Size + menuStrip1.Height + toolStrip1.Height;
            var pw = (Width - 200) / C64Sprite.Width;
            var ph = (Height - 200) / C64Sprite.Height;
            SpriteEditor.PixelSize = Math.Min(pw, ph);
            SpriteEditor.PixelSize = SpriteEditor.PixelSize < 6 ? 6 : SpriteEditor.PixelSize;

            var doubleSize = Width * 1.5 > Height;
            var spritesStartX = doubleSize ? Width - 68 : Width - 44;
            var spritesStartY = SpriteEditor.Y;
            for (var i = 0; i < 8; i++)
            {
                Sprites[i].Draw(e.Graphics, spritesStartX, spritesStartY, doubleSize);
                spritesStartY += doubleSize ? 44 : 22;
            }

            SpriteEditor.Draw(e.Graphics);
            ColorPicker.ColorCell.Size = SpriteEditor.PixelSize * 2;
            ColorPicker.Draw(e.Graphics, SpriteEditor.X, SpriteEditor.Y - (ColorPicker.ColorCell.Size + 8), SpriteEditor.Multicolor);
            if (Active || Width < 4)
                return;
            using (var shadow = new SolidBrush(Color.FromArgb(190, 0, 0, 0)))
                e.Graphics.FillRectangle(shadow, 0, 0, Width, Height);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            Action x = DelayedRedraw;
            x.BeginInvoke(null, null);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            RedrawBackgroundFlag = true;
            Refresh();
        }

        protected override void WndProc(ref Message m)
        {
            // Redraw on window state change.
            var org = WindowState;
            base.WndProc(ref m);
            if (WindowState == org)
                return;
            Action x = DelayedRedraw;
            x.BeginInvoke(null, null);
        }

        private void DelayedRedraw()
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(20);
            Application.DoEvents();
            Invoke(new Action(Refresh));
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            void Handled()
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                Invalidate();
            }

            void SetCol(int i)
            {
                SpriteEditor.SetPixelAtCursor(i);
                ColorPicker.SelectedColor = i;
                e.Handled = true;
                e.SuppressKeyPress = true;
                Invalidate();
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    SpriteEditor.MoveCursor(0, -1); Handled();
                    break;
                case Keys.Down:
                    SpriteEditor.MoveCursor(0, 1); Handled();
                    break;
                case Keys.Left:
                    SpriteEditor.MoveCursor(SpriteEditor.Multicolor ? -2 : -1, 0); Handled();
                    break;
                case Keys.Right:
                    SpriteEditor.MoveCursor(SpriteEditor.Multicolor ? 2 : 1, 0); Handled();
                    break;
                case Keys.Enter:
                    SpriteEditor.MoveCursor(0, 1); SpriteEditor.SetCursorX(0); Handled();
                    break;
                case Keys.Home:
                    SpriteEditor.SetCursorX(0); Handled();
                    break;
                case Keys.End:
                    SpriteEditor.SetCursorX(SpriteEditor.Multicolor ? 22 : 23); Handled();
                    break;
                case Keys.PageUp:
                    SpriteEditor.SetCursorY(0); Handled();
                    break;
                case Keys.PageDown:
                    SpriteEditor.SetCursorY(20); Handled();
                    break;
                case Keys.D1:
                    UndoBuffers[CurrentSpriteIndex].PushState(Sprites[CurrentSpriteIndex]);
                    SetCol(0);
                    break;
                case Keys.D2:
                    UndoBuffers[CurrentSpriteIndex].PushState(Sprites[CurrentSpriteIndex]);
                    SetCol(1);
                    break;
                case Keys.D3:
                    if (!SpriteEditor.Multicolor)
                        return;
                    UndoBuffers[CurrentSpriteIndex].PushState(Sprites[CurrentSpriteIndex]);
                    SetCol(2);
                    break;
                case Keys.D4:
                    if (!SpriteEditor.Multicolor)
                        return;
                    UndoBuffers[CurrentSpriteIndex].PushState(Sprites[CurrentSpriteIndex]);
                    SetCol(3);
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
            if (item == null)
                return;
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
            if (screenThing is C64Sprite thing)
            {
                var s = thing;
                if ((e.Button & MouseButtons.Left) > 0)
                {
                    if (s == Sprites[0]) { PickSpriteClick(sprite1ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[1]) { PickSpriteClick(sprite2ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[2]) { PickSpriteClick(sprite3ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[3]) { PickSpriteClick(sprite4ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[4]) { PickSpriteClick(sprite5ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[5]) { PickSpriteClick(sprite6ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[6]) { PickSpriteClick(sprite7ToolStripMenuItem, new EventArgs()); return; }
                    if (s == Sprites[7]) { PickSpriteClick(sprite8ToolStripMenuItem, new EventArgs()); return; }
                    return;
                }
            }
            screenThing = ColorPicker.HitTest(e.X, e.Y);
            if (screenThing != null)
            {
                var c = (ColorPicker.ColorCell)screenThing;
                if ((e.Button & MouseButtons.Left) > 0)
                {
                    void SetCol(int i)
                    {
                        if (Configuration.InputMethod == InputMethod.KeyboardInputMethod)
                            SpriteEditor.SetPixelAtCursor(i);
                        ColorPicker.SelectedColor = i;
                        Invalidate();
                    }

                    if (c == ColorPicker.GetColorCell(0))
                        SetCol(0);
                    if (c == ColorPicker.GetColorCell(1))
                        SetCol(1);
                    if (SpriteEditor.Multicolor && c == ColorPicker.GetColorCell(2))
                        SetCol(2);
                    if (SpriteEditor.Multicolor && c == ColorPicker.GetColorCell(3))
                        SetCol(3);
                }
                else if ((e.Button & MouseButtons.Right) > 0)
                {
                    if (c == ColorPicker.GetColorCell(0))
                    {
                        if (Configuration.InputMethod == InputMethod.MouseInputMethod)
                            ColorPicker.SelectedColor = 0;
                        pickBackgroundColorToolStripMenuItem_Click(null, new EventArgs());
                    }

                    if (c == ColorPicker.GetColorCell(1))
                    {
                        if (Configuration.InputMethod == InputMethod.MouseInputMethod)
                            ColorPicker.SelectedColor = 1;
                        pickForegroundColorToolStripMenuItem_Click(null, new EventArgs());
                    }

                    if (c == ColorPicker.GetColorCell(2))
                    {
                        if (Configuration.InputMethod == InputMethod.MouseInputMethod && SpriteEditor.Multicolor)
                            ColorPicker.SelectedColor = 2;
                        pickExtraColor1ToolStripMenuItem_Click(null, new EventArgs());
                    }

                    if (c == ColorPicker.GetColorCell(3))
                    {
                        if (Configuration.InputMethod == InputMethod.MouseInputMethod && SpriteEditor.Multicolor)
                            ColorPicker.SelectedColor = 3;
                        pickExtraColor2ToolStripMenuItem_Click(null, new EventArgs());
                    }
                }
            }
        }

        private IScreenThing GetScreenThing(int x, int y)
        {
            if (SpriteEditor.HitTest(x, y))
                return SpriteEditor;
            for (var i = 0; i < 8; i++)
                if (Sprites[i].HitTest(x, y))
                    return Sprites[i];
            return null;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e) =>
            MessageBox.Show(@"Use cursor keys to move cursor. Use keys 1 or 2 (1 to 4 in multi color mode) to set pixels. Happy spriting!",
                @"Help", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            switch (Configuration.InputMethod)
            {
                case InputMethod.MouseInputMethod:
                    Active = true;
                    Invalidate();
                    lblStatus.Text = @"Sprite editor activated.";
                    break;
                case InputMethod.KeyboardInputMethod:
                    Active = true;
                    Invalidate();
                    lblStatus.Text = @"Activated. Use keyboard to draw sprites.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            switch (Configuration.InputMethod)
            {
                case InputMethod.MouseInputMethod:
                    lblStatus.Text = @"Waiting for focus...";
                    break;
                case InputMethod.KeyboardInputMethod:
                    Active = false;
                    Invalidate();
                    lblStatus.Text = @"Paused. Activate window to enable program.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Clear all sprites?", @"New", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            for (var i = 0; i < Sprites.Count; i++)
                Sprites[i] = new C64Sprite();
            PickSpriteClick(sprite1ToolStripMenuItem, new EventArgs());
            Filename = "";
            Text = @"SPRDEF";
            Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Filename == "")
                saveAsToolStripMenuItem_Click(sender, new EventArgs());
            else
            {
                if (SaveSprites(Filename))
                    return;
                MessageBox.Show($@"Failed to save ""{Filename}"".", @"Save sprites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new SaveFileDialog())
            {
                x.Title = @"Save as";
                x.Filter = @"Sprite files (*.spr)|*.spr|All files (*.*)|*.*";
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                if (SaveSprites(x.FileName))
                    return;
                MessageDisplayer.Fail($@"Failed to save ""{Filename}"".", @"Save sprites");
            }
        }

        private bool SaveSprites(string filename)
        {
            var multicolor = SpriteEditor.Multicolor;
            if (multicolor)
                multicolorToolStripMenuItem_Click(null, new EventArgs());
            try
            {
                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    using (var w = new BinaryWriter(fs))
                    {
                        w.Write(Encoding.UTF8.GetBytes("SPRDEF"));
                        Sprites.WriteBytes(w);
                        w.Flush();
                        w.Close();
                    }
                    fs.Close();
                }
                Filename = filename;
                Text = $@"SPRDEF - {Filename}";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (multicolor)
                    multicolorToolStripMenuItem_Click(null, new EventArgs());
            }
        }

        private bool LoadSprites(string filename)
        {
            var multicolor = SpriteEditor.Multicolor;
            if (multicolor)
                multicolorToolStripMenuItem_Click(null, new EventArgs());
            try
            {
                var fi = new FileInfo(filename);
                if (!fi.Exists)
                    return false;
                if (fi.Length > 1200 || fi.Length < 10)
                    return false;
                using (var fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new BinaryReader(fs))
                    {
                        var header = sr.ReadBytes(6);
                        if (Encoding.UTF8.GetString(header) == "SPRDEF")
                        {
                            for (var i = 0; i < 8; i++)
                                Sprites[i].Load(sr);
                        }
                        sr.Close();
                    }
                    fs.Close();
                }
                Filename = filename;
                Text = $@"SPRDEF - {Filename}";
                Invalidate();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CreateUndoBuffers();
                if (multicolor)
                    multicolorToolStripMenuItem_Click(null, new EventArgs());
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new OpenFileDialog())
            {
                x.Title = @"Open sprites";
                x.Filter = @"Sprite files (*.spr)|*.spr|All files (*.*)|*.*";
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                if (LoadSprites(x.FileName))
                    return;
                MessageDisplayer.Fail($@"Failed to load ""{Filename}"".", @"Load sprites");
            }
        }

        private void exportToCBMPrgStudioDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new CbmPrgStudioDialog())
            {
                x.Sprites = Sprites;
                x.ShowDialog(this);
            }
        }

        private void exportToBASICToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new BasicDialog())
            {
                x.Sprites = Sprites;
                x.ShowDialog(this);
            }
        }

        private void pickBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new PaletteDialog())
            {
                x.Prompt = "Select background color:";
                x.ColorIndex = C64Sprite.BackgroundColorIndex;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                C64Sprite.BackgroundColorIndex = x.ColorIndex;
                for (var i = 0; i < 8; i++)
                    Sprites[i].ResetPixels();
                Invalidate();
            }
        }

        private void pickForegroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new PaletteDialog())
            {
                x.Prompt = "Select foreground color:";
                x.ColorIndex = C64Sprite.ForegroundColorIndex;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                C64Sprite.ForegroundColorIndex = x.ColorIndex;
                for (var i = 0; i < 8; i++)
                    Sprites[i].ResetPixels();
                Invalidate();
            }
        }

        private void pickExtraColor1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new PaletteDialog())
            {
                x.Prompt = "Select first extra color:";
                x.ColorIndex = C64Sprite.ExtraColor1Index;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                C64Sprite.ExtraColor1Index = x.ColorIndex;
                for (var i = 0; i < 8; i++)
                    Sprites[i].ResetPixels();
                Invalidate();
            }
        }

        private void pickExtraColor2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new PaletteDialog())
            {
                x.Prompt = "Select second extra color:";
                x.ColorIndex = C64Sprite.ExtraColor2Index;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                C64Sprite.ExtraColor2Index = x.ColorIndex;
                for (var i = 0; i < 8; i++)
                    Sprites[i].ResetPixels();
                Invalidate();
            }
        }

        private void multicolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cursorX = SpriteEditor.GetCursorX();
            multicolorToolStripMenuItem.Checked = !multicolorToolStripMenuItem.Checked;
            SpriteEditor.Multicolor = multicolorToolStripMenuItem.Checked;
            if (!multicolorToolStripMenuItem.Checked && ColorPicker.SelectedColor > 1)
                ColorPicker.SelectedColor = 1;
            if (SpriteEditor.Multicolor && cursorX % 2 != 0)
            {
                cursorX -= 1;
                SpriteEditor.SetCursorX(cursorX);
            }
            SpriteEditor.SetCursorX(cursorX);
            for (var i = 0; i < 8; i++)
                Sprites[i].ResetPixels();
            Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show($@"Sprdef version {System.Reflection.Assembly.GetEntryAssembly().GetName().Version}", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void exportPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new ExportPngDialog())
            {
                x.Multicolor = SpriteEditor.Multicolor;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                using (var y = new SaveFileDialog())
                {
                    y.Title = @"Export PNG";
                    y.Filter = @"PNG files (*.png)|*.png|All files (*.*)|*.*";
                    if (y.ShowDialog(this) != DialogResult.OK)
                        return;
                    try
                    {
                        if (SpriteEditor.Multicolor)
                        {
                            if (x.DoubleWidth)
                                SpriteEditor.SavePngMultiColorDoubleWidth(y.FileName, Sprites, x.TransparentBackground);
                            else
                                SpriteEditor.SavePngMultiColor(y.FileName, Sprites, x.TransparentBackground);
                        }
                        else
                            SpriteEditor.SavePng(y.FileName, Sprites, x.TransparentBackground);
                    }
                    catch (Exception ex)
                    {
                        MessageDisplayer.Fail(ex.Message, @"Export failed");
                    }
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UndoBuffers[CurrentSpriteIndex].CanUndo)
                return;
            var undoResult = UndoBuffers[CurrentSpriteIndex].Undo(SpriteEditor.Sprite);
            if (undoResult == null)
                return;
            Sprites[CurrentSpriteIndex] = undoResult;
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            RedrawBackgroundFlag = true;
            Invalidate();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UndoBuffers[CurrentSpriteIndex].CanRedo)
                return;
            Sprites[CurrentSpriteIndex] = UndoBuffers[CurrentSpriteIndex].Redo();
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            RedrawBackgroundFlag = true;
            Invalidate();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            redoToolStripMenuItem.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
        }

        private void keyboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mouseToolStripMenuItem.Checked = false;
            btnInputMouse.Checked = false;
            keyboardToolStripMenuItem.Checked = true;
            btnInputKeyboard.Checked = true;
            Configuration.InputMethod = InputMethod.KeyboardInputMethod;
            lblStatus.Text = @"Input method is set to keyboard.";
        }

        private void mouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keyboardToolStripMenuItem.Checked = false;
            btnInputKeyboard.Checked = false;
            mouseToolStripMenuItem.Checked = true;
            btnInputMouse.Checked = true;
            Configuration.InputMethod = InputMethod.MouseInputMethod;
            lblStatus.Text = @"Input method is set to mouse.";
        }

        private void btnInputKeyboard_Click(object sender, EventArgs e) =>
            keyboardToolStripMenuItem_Click(sender, e);

        private void btnInputMouse_Click(object sender, EventArgs e) =>
            mouseToolStripMenuItem_Click(sender, e);

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            var screenThing = GetScreenThing(e.X, e.Y);
            if (Configuration.InputMethod != InputMethod.MouseInputMethod || !(screenThing is SpriteEditor ed))
                return;
            var position = ed.GetPixelPositionFromPhysicalPosition(SpriteEditor.Multicolor, e.X, e.Y);
            if (position == null)
                return;
            ed.SetCursorX(position.Value.X);
            ed.SetCursorY(position.Value.Y);
            if ((e.Button & MouseButtons.Left) > 0)
                ed.SetPixelAtCursor(ColorPicker.SelectedColor);
            else if ((e.Button & MouseButtons.Right) > 0)
                ed.SetPixelAtCursor(0);
            Invalidate();
        }
    }
}
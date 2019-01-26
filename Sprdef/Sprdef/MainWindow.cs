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
        private int CurrentSpriteIndex { set; get; }
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
            UndoBuffers = new UndoBuffer[SpriteArray.Length];
            for (var i = 0; i < Sprites.Count; i++)
                UndoBuffers[i] = new UndoBuffer();
        }

        private void PushUndoState() =>
            UndoBuffers[CurrentSpriteIndex].PushState(Sprites[CurrentSpriteIndex]);

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

            sprite1ToolStripMenuItem1.Click += CopyFromSprite;
            sprite2ToolStripMenuItem1.Click += CopyFromSprite;
            sprite3ToolStripMenuItem1.Click += CopyFromSprite;
            sprite4ToolStripMenuItem1.Click += CopyFromSprite;
            sprite5ToolStripMenuItem1.Click += CopyFromSprite;
            sprite6ToolStripMenuItem1.Click += CopyFromSprite;
            sprite7ToolStripMenuItem1.Click += CopyFromSprite;
            sprite8ToolStripMenuItem1.Click += CopyFromSprite;

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
            WindowPainter.Paint(
                e.Graphics,
                RedrawBackgroundFlag,
                BackColor,
                Width,
                Height,
                menuStrip1.Height + toolStrip1.Height,
                SpriteEditor,
                Sprites,
                ColorPicker);
            if (!Active)
                using (var shadow = new SolidBrush(Color.FromArgb(190, 0, 0, 0)))
                    e.Graphics.FillRectangle(shadow, 0, 0, Width, Height);
            RedrawBackgroundFlag = false;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            Action x = DelayedRedraw;
            x.BeginInvoke(null, null);
            timer1.Enabled = true;
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
                    SpriteEditor.MoveCursor(0, -1);
                    Handled();
                    break;
                case Keys.Down:
                    SpriteEditor.MoveCursor(0, 1);
                    Handled();
                    break;
                case Keys.Left:
                    SpriteEditor.MoveCursor(SpriteEditor.Multicolor ? -2 : -1, 0);
                    Handled();
                    break;
                case Keys.Right:
                    SpriteEditor.MoveCursor(SpriteEditor.Multicolor ? 2 : 1, 0);
                    Handled();
                    break;
                case Keys.Enter:
                    SpriteEditor.MoveCursor(0, 1);
                    SpriteEditor.SetCursorX(0);
                    Handled();
                    break;
                case Keys.Home:
                    SpriteEditor.SetCursorX(0);
                    Handled();
                    break;
                case Keys.End:
                    SpriteEditor.SetCursorX(SpriteEditor.Multicolor ? 22 : 23);
                    Handled();
                    break;
                case Keys.PageUp:
                    SpriteEditor.SetCursorY(0);
                    Handled();
                    break;
                case Keys.PageDown:
                    SpriteEditor.SetCursorY(20);
                    Handled();
                    break;
                case Keys.D1:
                    switch (Configuration.InputMethod)
                    {
                        case InputMethod.KeyboardInputMethod:
                            PushUndoState();
                            SetCol(0);
                            break;
                        case InputMethod.MouseInputMethod:
                            ColorPicker.SelectedColor = 0;
                            Invalidate();
                            break;
                    }
                    break;
                case Keys.D2:
                    switch (Configuration.InputMethod)
                    {
                        case InputMethod.KeyboardInputMethod:
                            PushUndoState();
                            SetCol(1);
                            break;
                        case InputMethod.MouseInputMethod:
                            ColorPicker.SelectedColor = 1;
                            Invalidate();
                            break;
                    }
                    break;
                case Keys.D3:
                    if (!SpriteEditor.Multicolor)
                        return;
                    switch (Configuration.InputMethod)
                    {
                        case InputMethod.KeyboardInputMethod:
                            PushUndoState();
                            SetCol(2);
                            break;
                        case InputMethod.MouseInputMethod:
                            ColorPicker.SelectedColor = 2;
                            Invalidate();
                            break;
                    }
                    break;
                case Keys.D4:
                    if (!SpriteEditor.Multicolor)
                        return;
                    switch (Configuration.InputMethod)
                    {
                        case InputMethod.KeyboardInputMethod:
                            PushUndoState();
                            SetCol(3);
                            break;
                        case InputMethod.MouseInputMethod:
                            ColorPicker.SelectedColor = 3;
                            Invalidate();
                            break;
                    }
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
            if (item == sprite1ToolStripMenuItem)
            {
                CurrentSpriteIndex = 0;
                SpriteEditor.Sprite = Sprites[0];
                spritesToolStripMenuItem.Text = @"Sprite 1/8";
            }
            else if (item == sprite2ToolStripMenuItem)
            {
                CurrentSpriteIndex = 1;
                SpriteEditor.Sprite = Sprites[1];
                spritesToolStripMenuItem.Text = @"Sprite 2/8";
            }
            else if (item == sprite3ToolStripMenuItem)
            {
                CurrentSpriteIndex = 2;
                SpriteEditor.Sprite = Sprites[2];
                spritesToolStripMenuItem.Text = @"Sprite 3/8";
            }
            else if (item == sprite4ToolStripMenuItem)
            {
                CurrentSpriteIndex = 3;
                SpriteEditor.Sprite = Sprites[3];
                spritesToolStripMenuItem.Text = @"Sprite 4/8";
            }
            else if (item == sprite5ToolStripMenuItem)
            {
                CurrentSpriteIndex = 4;
                SpriteEditor.Sprite = Sprites[4];
                spritesToolStripMenuItem.Text = @"Sprite 5/8";
            }
            else if (item == sprite6ToolStripMenuItem)
            {
                CurrentSpriteIndex = 5;
                SpriteEditor.Sprite = Sprites[5];
                spritesToolStripMenuItem.Text = @"Sprite 6/8";
            }
            else if (item == sprite7ToolStripMenuItem)
            {
                CurrentSpriteIndex = 6;
                SpriteEditor.Sprite = Sprites[6];
                spritesToolStripMenuItem.Text = @"Sprite 7/8";
            }
            else if (item == sprite8ToolStripMenuItem)
            {
                CurrentSpriteIndex = 7;
                SpriteEditor.Sprite = Sprites[7];
                spritesToolStripMenuItem.Text = @"Sprite 8/8";
            }
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
                    if (s == Sprites[0])
                    {
                        PickSpriteClick(sprite1ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[1])
                    {
                        PickSpriteClick(sprite2ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[2])
                    {
                        PickSpriteClick(sprite3ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[3])
                    {
                        PickSpriteClick(sprite4ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[4])
                    {
                        PickSpriteClick(sprite5ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[5])
                    {
                        PickSpriteClick(sprite6ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[6])
                    {
                        PickSpriteClick(sprite7ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    if (s == Sprites[7])
                    {
                        PickSpriteClick(sprite8ToolStripMenuItem, new EventArgs());
                        return;
                    }
                    return;
                }
            }
            screenThing = ColorPicker.HitTest(e.X, e.Y);
            if (screenThing != null)
            {
                var c = (ColorPickerCell)screenThing;
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
            for (var i = 0; i < SpriteArray.Length; i++)
                if (Sprites[i].HitTest(x, y))
                    return Sprites[i];
            return null;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var s = new StringBuilder();
            s.AppendLine(Configuration.InputMethod == InputMethod.MouseInputMethod
                ? "In mouse control mode (current):"
                : "In mouse control mode:");
            s.AppendLine(@"Use keys 1 or 2 (1 to 4 in multi color mode) to select color, left click to set pixel and right click to clear pixel.");
            s.AppendLine();
            s.AppendLine(Configuration.InputMethod == InputMethod.KeyboardInputMethod
                ? "In keyboard control mode (current):"
                : "In keyboard control mode:");
            s.AppendLine(@"Use cursor keys to move cursor. Use keys 1 or 2 (1 to 4 in multi color mode) to set pixels.");
            s.AppendLine();
            s.Append("Happy spriting!");
            MessageBox.Show(s.ToString(), @"Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
                    Active = false;
                    Invalidate();
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
                            for (var i = 0; i < SpriteArray.Length; i++)
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
                for (var i = 0; i < SpriteArray.Length; i++)
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
                for (var i = 0; i < SpriteArray.Length; i++)
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
                for (var i = 0; i < SpriteArray.Length; i++)
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
                for (var i = 0; i < SpriteArray.Length; i++)
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
            for (var i = 0; i < SpriteArray.Length; i++)
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
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
            if (!UndoBuffers[CurrentSpriteIndex].CanUndo)
                return;
            var undoResult = UndoBuffers[CurrentSpriteIndex].Undo(SpriteEditor.Sprite);
            if (undoResult == null)
                return;
            Sprites[CurrentSpriteIndex] = undoResult;
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            RedrawBackgroundFlag = true;
            Invalidate();
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
            if (!UndoBuffers[CurrentSpriteIndex].CanRedo)
                return;
            Sprites[CurrentSpriteIndex] = UndoBuffers[CurrentSpriteIndex].Redo();
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            RedrawBackgroundFlag = true;
            Invalidate();
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
        }

        private void btnUndo_Click(object sender, EventArgs e) =>
            undoToolStripMenuItem_Click(sender, e);

        private void btnRedo_Click(object sender, EventArgs e) =>
            redoToolStripMenuItem_Click(sender, e);


        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            redoToolStripMenuItem.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
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
            {
                PushUndoState();
                ed.SetPixelAtCursor(ColorPicker.SelectedColor);
            }
            else if ((e.Button & MouseButtons.Right) > 0)
            {
                PushUndoState();
                ed.SetPixelAtCursor(0);
            }
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnUndo.Enabled = UndoBuffers[CurrentSpriteIndex].CanUndo;
            btnRedo.Enabled = UndoBuffers[CurrentSpriteIndex].CanRedo;
        }

        private void cBMPrgStudioDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new FromCbmPrgStudioDialog())
            {
                if (x.ShowDialog(this) != DialogResult.OK)
                    return;
                PushUndoState();
                Sprites[CurrentSpriteIndex].SetBytes(x.Sprite.GetBytes());
                Invalidate();
            }
        }

        private void copyFromToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            sprite1ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 0;
            sprite2ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 1;
            sprite3ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 2;
            sprite4ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 3;
            sprite5ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 4;
            sprite6ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 5;
            sprite7ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 6;
            sprite8ToolStripMenuItem1.Enabled = CurrentSpriteIndex != 7;
        }

        private void CopyFromSprite(object sender, EventArgs e)
        {
            PushUndoState();
            if (sender == sprite1ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[0].Clone();
            else if (sender == sprite2ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[1].Clone();
            else if (sender == sprite3ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[2].Clone();
            else if (sender == sprite4ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[3].Clone();
            else if (sender == sprite5ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[4].Clone();
            else if (sender == sprite6ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[5].Clone();
            else if (sender == sprite7ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[6].Clone();
            else if (sender == sprite8ToolStripMenuItem1)
                Sprites[CurrentSpriteIndex] = Sprites[7].Clone();
            SpriteEditor.Sprite = Sprites[CurrentSpriteIndex];
            Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MessageDisplayer.Ask("Quit?", Text))
                e.Cancel = true;
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnScrollUp_Click(object sender, EventArgs e) =>
            upToolStripMenuItem_Click(sender, e);

        private void btnScrollRight_Click(object sender, EventArgs e) =>
            rightToolStripMenuItem_Click(sender, e);

        private void btnScrollDown_Click(object sender, EventArgs e) =>
            downToolStripMenuItem_Click(sender, e);

        private void btnScrollLeft_Click(object sender, EventArgs e) =>
            leftToolStripMenuItem_Click(sender, e);
    }
}
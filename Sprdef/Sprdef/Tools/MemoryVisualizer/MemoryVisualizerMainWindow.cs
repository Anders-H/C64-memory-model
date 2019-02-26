using System;
using System.Collections.Generic;
using System.Windows.Forms;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;
using Sprdef.Tools.MemoryVisualizer.Renderer;

namespace Sprdef.Tools.MemoryVisualizer
{
    public partial class MemoryVisualizerMainWindow : Form
    {
        private readonly ScreenPainter _screenPainter = new ScreenPainter();
        private IScreenRenderer _screenRenderer;
        private Memory Memory { get; set; }
        private Address DisplayPointer { get; } = new Address(0);
        private int _lastRenderedDisplayPointer;
        private DisplayMode DisplayMode { get; set; }
        private ScreenCharacterMap Characters { get; } = new ScreenCharacterMap();
        private int StepSize { get; set; }
        private MemOverview MemOverview { get; set; }
        private Address DisassemblyStartAddress { get; } = new Address(0);
        private int LastPerformedDisassemblyStepSize { get; set; }
        private Stack<int> DisassemblyStepSize { get; } = new Stack<int>();

        public MemoryVisualizerMainWindow()
        {
            InitializeComponent();
            _screenRenderer = new HexRawScreenRenderer(ScreenCharacterMap.Rows, Characters);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            _screenPainter.RecalcGridFontSize = true;
            if (Memory != null && (MemOverview?.NeedsRecreating(Height) ?? false))
                MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            Invalidate();
        }
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            _screenPainter.Paint(e.Graphics, Memory, ClientSize, menuStrip1.Height, ScreenCharacterMap.Rows, ScreenCharacterMap.Columns, Characters);
            _screenRenderer.DrawGraphics(e.Graphics, Size, Memory, _lastRenderedDisplayPointer);
            MemOverview?.Draw(e.Graphics, DisplayPointer.Value, 10, menuStrip1.Height + 10);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new OpenFileDialog())
            {
                x.Title = @"Select a C64 program to open";
                x.Filter = @"Programs (*.prg)|*.prg|All files (*.*)|*.*";
                if (x.ShowDialog(this) == DialogResult.OK)
                    LoadFile(x.FileName);
            }
        }

        public void InitializeFromSprites(ushort location, SpriteArray sprites)
        {
            Memory = new Memory();
            foreach (var sprite in sprites)
            {
                Memory.SetBytes(new SimpleMemoryLocation((Address)location), sprite.GetBytes());
                location += 63;
            }
            spriteToolStripMenuItem_Click(null, new EventArgs());
        }

        private void LoadFile(string filename)
        {
            try
            {
                var temp = new Memory();
                temp.Load(filename, out var start, out _);
                Text = $@"C64 Memory Visualizer - {filename}";
                Memory = temp;
                DisplayPointer.FromInt(start.Value);
                DisassemblyStartAddress.FromInt(start.Value);
                DisplayMode = DisplayMode.HexRaw;
                if (Memory != null)
                    MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
                RenderScreen();
            }
            catch (Exception)
            {
                MessageBox.Show($@"The file ""{filename}"" could not be loaded.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
                LoadFile(files[0]);
        }

        private void MainWindow_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void RenderScreen()
        {
            if (Memory == null)
                return;
            Characters.Clear();
            _lastRenderedDisplayPointer = DisplayPointer.Value;
            var p = _lastRenderedDisplayPointer;
            switch (DisplayMode)
            {
                case DisplayMode.HexRaw:
                case DisplayMode.DecRaw:
                case DisplayMode.Sprite:
                    StepSize = _screenRenderer.RenderText(ref p, Memory);
                    break;
                case DisplayMode.Disassembly:
                    LastPerformedDisassemblyStepSize = StepSize = _screenRenderer.RenderText(ref p, Memory);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Invalidate();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    previousPageToolStripMenuItem_Click(sender, new EventArgs());
                    break;
                case Keys.PageDown:
                    nextPageToolStripMenuItem_Click(sender, new EventArgs());
                    break;
                case Keys.OemMinus:
                    previousByteToolStripMenuItem_Click(sender, new EventArgs());
                    break;
                case Keys.Oemplus:
                    nextByteToolStripMenuItem_Click(sender, new EventArgs());
                    break;
            }
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (DisplayMode)
            {
                case DisplayMode.Disassembly:
                    if (DisassemblyStepSize.Count > 0)
                    {
                        var step = DisassemblyStepSize.Pop();
                        if (DisplayPointer.CanDec(step))
                            DisplayPointer.Dec(step);
                    }
                    break;
                default:
                    if (DisplayPointer.CanDec(StepSize))
                        DisplayPointer.Dec(StepSize);
                    else
                        DisplayPointer.FromInt(0);
                    break;
            }
            RenderScreen();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (DisplayMode)
            {
                case DisplayMode.Disassembly:
                    if (LastPerformedDisassemblyStepSize > 0)
                    {
                        DisassemblyStepSize.Push(LastPerformedDisassemblyStepSize);
                        DisplayPointer.Inc(LastPerformedDisassemblyStepSize);
                    }
                    break;
                default:
                    if (DisplayPointer.CanInc(StepSize))
                        DisplayPointer.Inc(StepSize);
                    break;
            }
            RenderScreen();
        }

        private void rawHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            rawHexToolStripMenuItem.Checked = true;
            previousByteToolStripMenuItem.Enabled = true;
            nextByteToolStripMenuItem.Enabled = true;
            DisplayMode = DisplayMode.HexRaw;
            _screenRenderer = new HexRawScreenRenderer(ScreenCharacterMap.Rows, Characters);
            MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            RenderScreen();
        }

        private void rawDecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            rawDecToolStripMenuItem.Checked = true;
            previousByteToolStripMenuItem.Enabled = true;
            nextByteToolStripMenuItem.Enabled = true;
            DisplayMode = DisplayMode.DecRaw;
            _screenRenderer = new DecRawScreenRenderer(ScreenCharacterMap.Rows, Characters);
            MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            RenderScreen();
        }

        private void disassemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SetDisassemblyStartAddress())
                return;
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            disassemblyToolStripMenuItem.Checked = true;
            previousByteToolStripMenuItem.Enabled = false;
            nextByteToolStripMenuItem.Enabled = false;
            DisplayMode = DisplayMode.Disassembly;
            _screenRenderer = new DisassemblyScreenRenderer(ScreenCharacterMap.Rows, Characters);
            MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            RenderScreen();
        }

        private void spriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            spriteToolStripMenuItem.Checked = true;
            previousByteToolStripMenuItem.Enabled = true;
            nextByteToolStripMenuItem.Enabled = true;
            DisplayMode = DisplayMode.Sprite;
            _screenRenderer = new SpriteScreenRenderer(ScreenCharacterMap.Rows, Characters);
            MemOverview = MemOverview.Create(Memory, Height, DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            RenderScreen();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e) =>
            setDisassemblyStartAddressToolStripMenuItem.Enabled = Memory != null && DisplayMode == DisplayMode.Disassembly;

        private void setDisassemblyStartAddressToolStripMenuItem_Click(object sender, EventArgs e) =>
            SetDisassemblyStartAddress();

        private bool SetDisassemblyStartAddress()
        {
            using (var x = new DialogDisassemblyStartAddress())
            {
                x.StartAddress = DisassemblyStartAddress.Value;
                if (x.ShowDialog(this) != DialogResult.OK)
                    return false;
                DisassemblyStartAddress.FromInt(x.StartAddress);
                DisassemblyStepSize.Clear();
                DisplayPointer.FromInt(x.StartAddress);
                RenderScreen();
                return true;
            }
        }
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayPointer.FromInt(DisplayMode == DisplayMode.Disassembly ? DisassemblyStartAddress.Value : 0);
            RenderScreen();
        }

        private void MainWindow_Load(object sender, EventArgs e) =>
            System.Diagnostics.Debug.WriteLine($"Width: {Width}, Height: {Height}");

        private void previousByteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DisplayMode == DisplayMode.Disassembly)
                return;
            if (DisplayPointer.CanDec(1))
                DisplayPointer.Dec(1);
            else
                DisplayPointer.FromInt(0);
            RenderScreen();
        }

        private void nextByteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DisplayMode == DisplayMode.Disassembly)
                return;
            if (DisplayPointer.CanInc(1))
                DisplayPointer.Inc(1);
            RenderScreen();
        }
    }
}
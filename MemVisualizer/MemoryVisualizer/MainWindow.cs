using System;
using System.Windows.Forms;
using System.Collections.Generic;
using C64MemoryModel.Mem;
using C64MemoryModel.Types;
using MemoryVisualizer.Renderer;

namespace MemoryVisualizer
{
    public partial class MainWindow : Form
    {
        private readonly ScreenPainter _screenPainter = new ScreenPainter();
        private IScreenRenderer _screenRenderer;
        private Memory Memory { get; set; }
        private Address DisplayPointer { get; } = new Address(0);
        private DisplayMode DisplayMode { get; set; }
        private ScreenCharacterMap Characters { get; } = new ScreenCharacterMap();
        private int StepSize { get; set; }
        private MemOverview MemOverview { get; set; }
        private Address DisassemblyStartAddress { get; } = new Address(0);
        private int LastPerformedDisassemblyStepSize { get; set; }
        private Stack<int> DisassemblyStepSize { get; } = new Stack<int>();

        public MainWindow()
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
            System.Diagnostics.Debug.WriteLine($"Width: {Width}, Height: {Height}");
        }
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            _screenPainter.Paint(e.Graphics, Memory, ClientSize, menuStrip1.Height, ScreenCharacterMap.Rows, ScreenCharacterMap.Columns, Characters);
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
            var displayPointer = (int)DisplayPointer.Value;
            switch (DisplayMode)
            {
                case DisplayMode.HexRaw:
                case DisplayMode.DecRaw:
                    StepSize = _screenRenderer.Render(ref displayPointer, Memory);
                    break;
                case DisplayMode.Disassembly:
                    LastPerformedDisassemblyStepSize = StepSize = _screenRenderer.Render(ref displayPointer, Memory);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Invalidate();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
                previousPageToolStripMenuItem_Click(sender, new EventArgs());
            else if (e.KeyCode == Keys.PageDown)
                nextPageToolStripMenuItem_Click(sender, new EventArgs());
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
            DisplayMode = DisplayMode.Disassembly;
            _screenRenderer = new DisassemblyScreenRenderer(ScreenCharacterMap.Rows, Characters);
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
    }
}
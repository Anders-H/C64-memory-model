using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using C64MemoryModel;

namespace MemoryVisualizer
{
    public partial class MainWindow : Form
    {
        private static C64Palette Palette { get; }
        private Rectangle OuterClient { get; set; }
        private Rectangle InnerClient { get; set; }
        private Memory Memory { get; set; }
        private int DisplayPointer { get; set; }
        private DisplayMode DisplayMode { get; set; }
        private ScreenCharacterMap Characters { get; } = new ScreenCharacterMap();
        private bool RecalcGridFontSize { get; set; } = true;
        private Font GridFont { get; set; } = new Font("Courier New", 10);
        private float FontXOffset { get; set; } = 0f;
        private float FontYOffset { get; set; } = 0f;
        private int StepSize { get; set; }
        private List<int> DisassemblyStepSize { get; } = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }
        static MainWindow()
        {
            Palette = new C64Palette();
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            RecalcGridFontSize = true;
            Invalidate();
        }
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            var borderHeight = (int)(ClientSize.Height*0.10);
            var borderWidth = (int)(ClientSize.Width*0.10);
            OuterClient = new Rectangle(0, menuStrip1.Height, ClientSize.Width, ClientSize.Height - menuStrip1.Height);
            InnerClient = new Rectangle(OuterClient.Left + borderWidth, OuterClient.Top + borderHeight, OuterClient.Width - borderWidth - borderWidth, OuterClient.Height - borderHeight - borderHeight);
            using (var lightBlue = new SolidBrush(Palette.GetColor(C64Color.LightBlue)))
            {
                using (var blue = new SolidBrush(Palette.GetColor(C64Color.Blue)))
                {
                    //Draw border.
                    e.Graphics.FillRectangle(lightBlue, OuterClient.Left, OuterClient.Top, OuterClient.Width, borderHeight);
                    e.Graphics.FillRectangle(lightBlue, OuterClient.Left, OuterClient.Top, borderWidth, OuterClient.Height);
                    e.Graphics.FillRectangle(lightBlue, OuterClient.Left, OuterClient.Top + OuterClient.Height - borderHeight, OuterClient.Width, borderHeight);
                    e.Graphics.FillRectangle(lightBlue, OuterClient.Left + OuterClient.Width - borderWidth, 0, borderWidth, OuterClient.Height);
                    e.Graphics.FillRectangle(blue, InnerClient);
                    if (Memory == null)
                    {
                        using (var f = new Font("Arial", 20, FontStyle.Regular))
                            e.Graphics.DrawString("Drop .prg file here.", f, Brushes.White, InnerClient.Left, InnerClient.Top);
                    }
                    else
                    {
                        var characterWidth = (float)((double)InnerClient.Width/(double)ScreenCharacterMap.Columns);
                        var characterHeight = (float)((double)InnerClient.Height / (double)ScreenCharacterMap.Rows);
                        if (RecalcGridFontSize)
                        {
                            RecalcGridFontSize = false;
                            GridFont.Dispose();
                            var fontSize = characterHeight > characterWidth ? characterWidth : characterHeight;
                            GridFont = new Font("Courier New", fontSize * 0.9f);
                            var charSize = e.Graphics.MeasureString("W", GridFont);
                            FontXOffset = (characterWidth / 2) - (charSize.Width / 2);
                            FontYOffset = ((characterHeight / 2) - (charSize.Height / 2)) * 1.1f;
                        }
                        var xPos = (float)InnerClient.Left;
                        var yPos = (float)InnerClient.Top;
                        for (var y = 0; y < ScreenCharacterMap.Rows; y++)
                        {
                            for (var x = 0; x < ScreenCharacterMap.Columns; x++)
                            {
                                e.Graphics.DrawString(Characters.GetCharacter(x, y).ToString(), GridFont, lightBlue, xPos + FontXOffset, yPos + FontYOffset);
                                xPos += characterWidth;
                            }
                            xPos = (float)InnerClient.Left;
                            yPos += characterHeight;
                        }
                    }
                }
            }
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) => Close();
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
                int start, length;
                temp.Load(filename, out start, out length);
                Text = $"C64 Memory Visualizer - {filename}";
                Memory = temp;
                DisplayPointer = start;
                DisplayMode = DisplayMode.HexRaw;
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
            Characters.Clear();
            var displayPointer = DisplayPointer;
            switch (DisplayMode)
            {
                case DisplayMode.HexRaw:
                    for (var row = 0; row < ScreenCharacterMap.Rows; row++)
                    {
                        if (displayPointer > ushort.MaxValue)
                            break;
                        Characters.SetCharacters(0, row, displayPointer.ToString("00000"));
                        Characters.SetCharacters(6, row, displayPointer.ToString("X4"));
                        var x = 11;
                        for (int col = 0; col < 8; col++)
                        {
                            if (displayPointer > ushort.MaxValue)
                                break;
                            Characters.SetCharacters(x, row, Memory.GetByte((ushort)displayPointer).ToString("X2"));
                            x += 3;
                            displayPointer++;
                        }
                    }
                    StepSize = ScreenCharacterMap.Rows * 8;
                    break;
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
            if (DisplayPointer > 0)
                DisplayPointer -= StepSize;
            if (DisplayPointer < 0)
                DisplayPointer = 0;
            RenderScreen();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DisplayPointer + StepSize <= ushort.MaxValue)
                DisplayPointer += StepSize;
            if (DisplayPointer > ushort.MaxValue)
                DisplayPointer = ushort.MaxValue;
            RenderScreen();
        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Windows.Forms;
using C64MemoryModel.Graphics;

namespace ThePetscii
{
    public partial class MainWindow : Form
    {
        private Tool _currentTool = Tool.SetQuarterChar;
        private float _colorHeight;
        private C64Color _currentColor = C64Color.LightBlue;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            panelScreenContainer.Invalidate();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            canvas1.PetsciiImage = new PetsciiImage(new C64Palette());
            MainWindow_Resize(sender, e);
        }

        private void PanelScreenContainer_Resize(object sender, EventArgs e)
        {
            canvas1.ExtraLarge = panelScreenContainer.Width > 960 && panelScreenContainer.Height > 600;
            canvas1.Left = panelScreenContainer.Width / 2 - canvas1.Width / 2;
            canvas1.Top = panelScreenContainer.Height / 2 - canvas1.Height / 2;
        }

        private void panelColors_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
                return;
            var y = 0f;
            var palette = new C64Palette();
            var index = 0;
            foreach (var color in palette)
            {
                using (var b = new SolidBrush(color))
                    e.Graphics.FillRectangle(b, 0, y, panelColors.Width, _colorHeight);
                if (index == (int)_currentColor)
                {
                    e.Graphics.DrawRectangle(Pens.Black, 0, y + 0, panelColors.Width - 1, _colorHeight);
                    e.Graphics.DrawRectangle(Pens.White, 1, y + 1, panelColors.Width - 3, _colorHeight - 2);
                    e.Graphics.DrawRectangle(Pens.Black, 2, y + 2, panelColors.Width - 5, _colorHeight - 4);
                }
                y += _colorHeight;
                index++;
            }
        }

        private void panelColors_Resize(object sender, EventArgs e)
        {
            _colorHeight = (float)(panelColors.Height / 16.0);
            panelColors.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas1.GridVisible = !canvas1.GridVisible;
            panelScreenContainer.Invalidate();
        }

        private void panelColors_MouseClick(object sender, MouseEventArgs e)
        {
            var newColor = (int)(e.Y / _colorHeight);
            newColor = newColor < 0
                ? 0 : newColor > 15
                    ? 15 : newColor;
            _currentColor = (C64Color)newColor;
            panelColors.Invalidate();
        }
        
        private void Canvas1_CanvasClick(object sender, CanvasModel.CanvasClickEventArgs e)
        {
            switch (_currentTool)
            {
                case Tool.SetQuarterChar:
                    canvas1.PetsciiImage.Content.SetSubpixel(e.CharacterX, e.CharacterY, e.SubCharacterX, e.SubCharacterY, true);
                    canvas1.Invalidate();
                    break;
                case Tool.UnsetQuarterChar:
                    canvas1.PetsciiImage.Content.SetSubpixel(e.CharacterX, e.CharacterY, e.SubCharacterX, e.SubCharacterY, false);
                    canvas1.Invalidate();
                    break;
                case Tool.ToggleQuarterChar:
                    canvas1.PetsciiImage.Content.SetSubpixel(
                        e.CharacterX,
                        e.CharacterY,
                        e.SubCharacterX,
                        e.SubCharacterY,
                        !canvas1.PetsciiImage.Content.GetSubpixel(
                            e.CharacterX,
                            e.CharacterY,
                            e.SubCharacterX,
                            e.SubCharacterY)
                    );
                    canvas1.Invalidate();
                    break;
                case Tool.SetForecolor:
                    canvas1.PetsciiImage.Foreground.SetColor(e.CharacterX, e.CharacterY, _currentColor);
                    canvas1.Invalidate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e) =>
            Refresh();

        private void UncheckTools()
        {
            setQuartercharToolStripMenuItem.Checked = false;
            unsetQuartercharToolStripMenuItem.Checked = false;
            toggleQuartercharToolStripMenuItem.Checked = false;
            setForecolorToolStripMenuItem.Checked = false;
        }
        
        private void setQuartercharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckTools();
            setQuartercharToolStripMenuItem.Checked = true;
            _currentTool = Tool.SetQuarterChar;
        }
        
        private void unsetQuartercharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckTools();
            unsetQuartercharToolStripMenuItem.Checked = true;
            _currentTool = Tool.UnsetQuarterChar;
        }

        private void toggleQuartercharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckTools();
            toggleQuartercharToolStripMenuItem.Checked = true;
            _currentTool = Tool.ToggleQuarterChar;
        }

        private void setForecolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckTools();
            setForecolorToolStripMenuItem.Checked = true;
            _currentTool = Tool.SetForecolor;
        }
        
        private void exportToBASICToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var codeGenerator = new CodeGenerator(canvas1.PetsciiImage);
            using var x = new ExportToBasicDialog();
            x.Code = codeGenerator.GetBasic();
            x.ShowDialog(this);
        }
    }
}

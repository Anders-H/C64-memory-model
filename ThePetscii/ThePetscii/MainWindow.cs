using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using C64MemoryModel.Graphics;

namespace ThePetscii
{
    public partial class MainWindow : Form
    {
        private float _colorHeight;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            canvas1.Left = panelScreenContainer.Width / 2 - canvas1.Width / 2;
            canvas1.Top = panelScreenContainer.Height / 2 - canvas1.Height / 2;
            panelScreenContainer.Invalidate();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            MainWindow_Resize(sender, e);
        }

        private void PanelScreenContainer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            e.Graphics.Clear(panelScreenContainer.BackColor);
            if (canvas1.GridVisible)
            {
                using (var darkPen = new Pen(Color.FromArgb(30, 30, 30)))
                    e.Graphics.DrawRectangle(darkPen, canvas1.Left, canvas1.Top, canvas1.Width, canvas1.Height);
            }
            else
            {
                using (var darkPen = new Pen(Color.FromArgb(30, 30, 30)))
                    e.Graphics.DrawRectangle(darkPen, canvas1.Left - 1, canvas1.Top - 1, canvas1.Width + 1, canvas1.Height + 1);
            }
        }

        private void PanelScreenContainer_Resize(object sender, EventArgs e)
        {
            panelScreenContainer.Invalidate();
        }

        private void panelColors_Paint(object sender, PaintEventArgs e)
        {
            var y = 0f;
            var palette = new C64Palette();
            foreach (var color in palette)
            {
                using (var b = new SolidBrush(color))
                    e.Graphics.FillRectangle(b, 0, y, panelColors.Width, _colorHeight);
                y += _colorHeight;
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
    }
}

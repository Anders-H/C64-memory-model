using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThePetscii
{
    public partial class MainWindow : Form
    {
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
            using (var darkPen = new Pen(Color.FromArgb(30, 30, 30)))
                e.Graphics.DrawRectangle(Pens.Black, canvas1.Left, canvas1.Top, canvas1.Width, canvas1.Height);
        }

        private void PanelScreenContainer_Resize(object sender, EventArgs e)
        {
            panelScreenContainer.Invalidate();
        }
    }
}

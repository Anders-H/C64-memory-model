using System;
using System.Drawing;
using System.Windows.Forms;
using C64MemoryModel;

namespace MemoryVisualizer
{
    public partial class MainWindow : Form
    {
        private static C64Palette Palette { get; }
        private Rectangle OuterClient { get; set; }
        private Rectangle InnerClient { get; set; }
        private Memory Memory { get; set; }
        private ScreenCharacterMap Characters = new ScreenCharacterMap();
        public MainWindow()
        {
            InitializeComponent();
        }
        static MainWindow()
        {
            Palette = new C64Palette();
        }
        private void MainWindow_Resize(object sender, EventArgs e) => Invalidate();

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
                }
            }
            if (Memory == null)
            {
                using (var f = new Font("Arial", 20, FontStyle.Regular))
                    e.Graphics.DrawString("Drop .prg file here.", f, Brushes.White, InnerClient.Left, InnerClient.Top);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

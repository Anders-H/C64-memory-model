using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sprdef
{
    public partial class FromCbmPrgStudioDialog : Form
    {
        private C64Sprite Sprite { get; } = new C64Sprite();

        public FromCbmPrgStudioDialog()
        {
            InitializeComponent();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var x = new TextDataParser(txtInput.Text);
            txtCleanDataOutput.Text = x.CleanDataOutput();
            Sprite.SetBytes(x.GetBytes());
            picPreview.Invalidate();
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.ButtonFace);
            var x = picPreview.Width / 2 - C64Sprite.Width;
            var y = picPreview.Height / 2 - C64Sprite.Height;
            Sprite.Draw(e.Graphics, x, y, true);
        }
    }
}
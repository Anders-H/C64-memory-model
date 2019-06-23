using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sprdef.Model;

namespace Sprdef.Dialogs
{
    public partial class FromCbmPrgStudioDialog : Form
    {
        public C64Sprite Sprite { get; } = new C64Sprite();

        public FromCbmPrgStudioDialog()
        {
            InitializeComponent();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var x = new TextDataParser(txtInput.Text);
            var cleanOutput = x.CleanDataOutput();
            var s = new StringBuilder();
            foreach (var b in cleanOutput)
            {
                s.Append(b.Value.ToString());
                if (b != cleanOutput.Last())
                    s.Append(", ");
            }
            txtCleanDataOutput.Text = s.ToString();
            Sprite.SetBytes(cleanOutput);
            picPreview.Invalidate();
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.ButtonFace);
            var x = picPreview.Width / 2 - C64Sprite.Width;
            var y = picPreview.Height / 2 - C64Sprite.Height;
            Sprite.Draw(e.Graphics, x, y, true);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var x = new TextDataParser(txtInput.Text);
            var cleanOutput = x.CleanDataOutput();
            Sprite.SetBytes(cleanOutput);
            DialogResult = DialogResult.OK;
        }
    }
}
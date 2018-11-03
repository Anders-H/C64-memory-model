using System.Windows.Forms;

namespace Sprdef
{
    public static class MessageDisplayer
    {
        public static void Fail(string message, string text)
            => MessageBox.Show(message, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

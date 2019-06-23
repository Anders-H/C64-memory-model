using System.Text;
using System.Windows.Forms;

namespace Sprdef
{
    public static class MessageDisplayer
    {
        public static void Fail(string message, string text) =>
            MessageBox.Show(message, text, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Fail(StringBuilder message, string text) =>
            Fail(message.ToString(), text);

        public static bool Ask(string message, string text) =>
            MessageBox.Show(message, text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

        public static bool Ask(StringBuilder message, string text) =>
            Ask(message.ToString(), text);
    }
}
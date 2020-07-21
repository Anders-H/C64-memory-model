using System.Windows.Forms;
using Asm6502.AsmProgram;

namespace Monitor
{
    public class ProgramListController
    {
        private ListView ListView { get; }

        public ProgramListController(ListView listView)
        {
            ListView = listView;
        }

        public void RefreshList(CurrentAsmProgram program)
        {
            ListView.BeginUpdate();
            ListView.Items.Clear();
            foreach (var p in program)
            {
                var li = ListView.Items.Add("");
                li.Tag = p;
                UpdateListItem(li);
            }
            ListView.EndUpdate();
        }

        public void UpdateCurrentListItems()
        {
            ListView.BeginUpdate();

            foreach (ListViewItem item in ListView.Items)
                UpdateListItem(item);

            ListView.EndUpdate();
        }

        private void UpdateListItem(ListViewItem l)
        {
            var p = (ProgramInstruction)l.Tag;
            l.Text = $@"${p.Address:X4} - {p.Address:n0}";

            while (l.SubItems.Count < ListView.Columns.Count)
                l.SubItems.Add("");

            l.SubItems[1].Text = p.OperationCode.ToString().ToUpper();
        }
    }
}
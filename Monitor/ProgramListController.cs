using System.Windows.Forms;

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

            ListView.EndUpdate();
        }

        public void UpdateCurrentListItems()
        {
            ListView.BeginUpdate();

            ListView.EndUpdate();
        }
    }
}
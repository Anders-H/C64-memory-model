using System;
using System.Windows.Forms;
using Asm6502.AsmInstructionPalette;
using Asm6502.AsmProgram;

namespace Monitor
{
    public partial class MainWindow : Form
    {
        public ViewMode ViewMode { get; set; }
        public ProgramListController ProgramListController { get; }
        public CurrentAsmProgram Program { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewMode = ViewMode.Hex;
            Program = new CurrentAsmProgram();
            ProgramListController = new ProgramListController(lv);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //TEST CODE!!!
            Program.Add(new ProgramInstruction(2048, OperationCode.Lda, OperationMode.Absolute) { ByteArgument = 45 });
            ProgramListController.RefreshList(Program);

        }

        private void hexaDecimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewMode = ViewMode.Hex;
            hexaDecimalToolStripMenuItem.Checked = true;
            decimalToolStripMenuItem.Checked = false;
            ProgramListController.UpdateCurrentListItems();
        }

        private void decimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewMode = ViewMode.Dec;
            hexaDecimalToolStripMenuItem.Checked = false;
            decimalToolStripMenuItem.Checked = true;
            ProgramListController.UpdateCurrentListItems();
        }
    }
}

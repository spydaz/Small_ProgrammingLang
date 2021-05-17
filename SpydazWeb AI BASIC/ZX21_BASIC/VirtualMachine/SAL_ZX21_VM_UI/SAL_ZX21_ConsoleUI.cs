using System;
using SAL_VM.STACK_VM;

namespace SAL_VM
{
    public partial class SAL_ZX21_ConsoleUI
    {
        public SAL_ZX21_ConsoleUI()
        {
            InitializeComponent();
            _ButtonRunCode.Name = "ButtonRunCode";
            _ButtonClrScrn.Name = "ButtonClrScrn";
            _ButtonNewScrn.Name = "ButtonNewScrn";
        }

        public string ExecuteCode(ref string Code)
        {
            return X86API.RunMachineCode(ref Code);
        }

        private void ButtonRunCode_Click(object sender, EventArgs e)
        {
            string CodeAnalysis = RichTextBoxCodeEntry.Text;
            RichTextBoxInfo.Text += ExecuteCode(ref CodeAnalysis);
        }

        private void ButtonNewScrn_Click(object sender, EventArgs e)
        {
            var frm = new SAL_ZX21_ConsoleUI();
            frm.Show();
        }

        private void ButtonClrScrn_Click(object sender, EventArgs e)
        {
            RichTextBoxInfo.Clear();
        }
    }
}
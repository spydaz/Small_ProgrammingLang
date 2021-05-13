using System;
using SDK.SAL;

namespace SDK
{
    public partial class VM_MachineUI
    {
        public VM_MachineUI()
        {
            InitializeComponent();
            _ButtonRunCode.Name = "ButtonRunCode";
            _ButtonClrScrn.Name = "ButtonClrScrn";
            _ButtonNewScrn.Name = "ButtonNewScrn";
            _ButtonRef.Name = "ButtonRef";
            _ButtonEMU.Name = "ButtonEMU";
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
            var frm = new VM_MachineUI();
            frm.Show();
        }

        private void ButtonClrScrn_Click(object sender, EventArgs e)
        {
            RichTextBoxInfo.Clear();
        }

        public void DISPLAY_TEXT(ref string Str)
        {
            RichTextBoxInfo.Clear();
            RichTextBoxInfo.Text = Str;
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            var frm = new VM_MachineUI();
            string argStr = My.Resources.Resources.QuickRef_SAL;
            frm.DISPLAY_TEXT(ref argStr);
            My.Resources.Resources.QuickRef_SAL = argStr;
            frm.Show();
        }

        private void ButtonEMU_Click(object sender, EventArgs e)
        {
            var frmU = new EMU_MachineUI();
            frmU.Show();
        }
    }
}
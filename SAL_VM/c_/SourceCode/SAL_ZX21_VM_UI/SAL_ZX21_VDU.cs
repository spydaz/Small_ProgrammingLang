using Microsoft.VisualBasic;

namespace SAL_VM
{
    public partial class SAL_ZX21_VDU
    {
        public SAL_ZX21_VDU()
        {
            InitializeComponent();
        }

        public void Print(ref string Userinput)
        {
            Zx81_DisplayScreen.Text += Userinput;
        }

        public void CLS()
        {
            Zx81_DisplayScreen.Text = "" + Constants.vbCr;
        }

        public string Input(ref string Message)
        {
            // Default = "1"    ' Set default.
            // Display message, title, and default value.
            return Interaction.InputBox(Message, "INPUT");
        }
    }
}
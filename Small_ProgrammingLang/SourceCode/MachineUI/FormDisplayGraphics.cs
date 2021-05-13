
namespace SDK
{
    public partial class FormDisplayGraphics
    {
        public FormDisplayGraphics()
        {
            InitializeComponent();
        }

        public void Decode(ref SAL_VM.STACK_VM.ZX81_GPU.LogoCmd Cmd)
        {
            GDU_DISPLAY.Decode(ref Cmd);
        }
    }
}
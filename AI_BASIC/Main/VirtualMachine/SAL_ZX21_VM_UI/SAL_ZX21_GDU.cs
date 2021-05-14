
namespace SAL_VM
{
    public partial class SAL_ZX21_GDU
    {
        public SAL_ZX21_GDU()
        {
            InitializeComponent();
        }

        /// <summary>
    /// fd – forward
    /// bk – backward
    /// rt – right
    /// lt – left
    /// cs – clearscreen
    /// pu − penup
    /// pd − pendown
    /// ht − hideturtle
    /// st − showturtle
    /// </summary>
    /// <param name="Cmd"></param>
        public void Decode(ref STACK_VM.ZX81_GPU.LogoCmd Cmd)
        {
            GDU_DISPLAY.Decode(ref Cmd);
        }
    }
}
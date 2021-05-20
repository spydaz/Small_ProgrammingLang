
namespace SAL_VM.STACK_VM
{
    public class ZX81_GPU
    {
        private SAL_ZX21_VDU iMonitorConsole;
        private SAL_ZX21_GDU IMonitorLogoGraphics;

        public ZX81_GPU()
        {
            iMonitorConsole = new SAL_ZX21_VDU();
            IMonitorLogoGraphics = new SAL_ZX21_GDU();
        }

        public void ConsolePrint(ref string Str)
        {
            if (iMonitorConsole.Visible == false)
            {
                iMonitorConsole.Show();
            }
            else
            {
            }

            iMonitorConsole.Print(ref Str);
        }

        public void Console_CLS()
        {
            if (iMonitorConsole.Visible)
            {
            }
            else
            {
                iMonitorConsole.Show();
            }

            iMonitorConsole.CLS();
        }

        public void DecodeGDU_Cmd(ref LogoCmd Cmd)
        {
            if (IMonitorLogoGraphics.Visible)
            {
            }
            else
            {
                IMonitorLogoGraphics.Show();
            }

            IMonitorLogoGraphics.Decode(ref Cmd);
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
        public struct LogoCmd
        {
            public string Cmd;
            public int Value;
        }
    }
}
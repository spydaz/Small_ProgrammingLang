using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SAL_VM.STACK_VM
{
    public class X86API
    {
        public static string RunMachineCode(ref string Code)
        {
            Code = Strings.UCase(Code);
            var PROG = Strings.Split(Code.Replace(Constants.vbCrLf, " "), " ");
            var InstructionLst = new List<string>();
            var ROOT = new TreeNode();
            ROOT.Text = "ASSEMBLY_CODE";
            int Count = 0;
            foreach (var item in PROG)
            {
                Count += 1;
                if (!string.IsNullOrEmpty(item))
                {
                    var NDE = new TreeNode();
                    NDE.Text = Count + ": " + item;
                    ROOT.Nodes.Add(NDE);
                    InstructionLst.Add(item);
                }
            }
            // cpu - START



            string argThreadName = "Test";
            var CPU = new ZX81_CPU(ref argThreadName, ref InstructionLst);
            CPU.RUN();
            Tree = ROOT;
            return "CURRENT POINTER = " + CPU.Get_Instruction_Pointer_Position + Constants.vbCr + "CONTAINED DATA = " + CPU.Peek();
        }

        public static TreeNode Tree = new TreeNode();
    }
}
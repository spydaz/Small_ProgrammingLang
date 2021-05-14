using System.Collections.Generic;

namespace SAL_VM.STACK_VM
{
    /// <summary>
    /// Memory frame for Variables 
    /// </summary>
    public class StackMemoryFrame
    {
        public struct Var
        {
            public int Value;
            public string VarNumber;
        }

        public int ReturnAddress;
        public List<Var> Variables;

        public StackMemoryFrame(ref int _ReturnAddress)
        {
            ReturnAddress = _ReturnAddress;
            Variables = new List<Var>();
        }

        public int GetReturnAddress()
        {
            return ReturnAddress;
        }

        public int GetVar(ref string VarNumber)
        {
            foreach (var item in Variables)
            {
                if ((item.VarNumber ?? "") == (VarNumber ?? ""))
                {
                    return item.Value;
                }
            }

            return 0;
        }

        public void SetVar(ref string VarNumber, ref int value)
        {
            var item = new Var();
            item.VarNumber = VarNumber;
            item.Value = value;
            Variables.Add(item);
        }
    }
}
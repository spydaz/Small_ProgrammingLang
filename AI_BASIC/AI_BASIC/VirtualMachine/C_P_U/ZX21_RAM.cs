using System.Collections.Generic;

namespace SAL_VM.STACK_VM
{
    public class ZX81_RAM
    {
        public struct Variable
        {
            public string iName;
            public string iValue;
            public string iType;
        }
        /// <summary>
        /// Currently only Variables can be stored
        /// </summary>
        public List<Variable> CurrentVars;

        public ZX81_RAM()
        {
            CurrentVars = new List<Variable>();
        }

        // Variables

        public void UpdateVar(ref string VarName, ref string iVALUE)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? ""))
                {
                    var NiTEM = item;
                    NiTEM.iValue = iVALUE;
                    CurrentVars.Remove(item);
                    CurrentVars.Add(NiTEM);
                    break;
                }
                else
                {
                }
            }
        }

        public object RemoveVar(ref Variable Var)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (Var.iName ?? ""))
                {
                    CurrentVars.Remove(item);
                }
            }

            return Var;
        }

        public void AddVar(ref Variable Var)
        {
            if (CheckVar(ref Var.iName) == false)
            {
                CurrentVars.Add(Var);
            }
        }

        public bool CheckVar(ref string VarName)
        {
            bool CheckVarRet = default;
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? ""))
                {
                    return true;
                }
            }

            CheckVarRet = false;
            return CheckVarRet;
        }

        public string GetVar(ref string VarName)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? "") == true)
                {
                    if (item.iType == "BOOLEAN")
                    {
                        switch (item.iValue ?? "")
                        {
                            case 0:
                                {
                                    return "False";
                                }

                            case 1:
                                {
                                    return "True";
                                }

                            default:
                                {
                                    return item.iValue;
                                }
                        }
                    }
                    else
                    {
                        return item.iValue;
                    }
                }
                else
                {
                }
            }

            return VarName;
        }
    }
}
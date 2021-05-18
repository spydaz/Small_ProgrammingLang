using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SAL_VM.STACK_VM
{
    /// <summary>
    /// SpydazWeb X86 Assembly language Virtual X86 Processor
    /// </summary>
    public class ZX81_CPU
    {
        public ZX81_GPU GPU = new ZX81_GPU();

        #region CPU


        /// <summary>
        /// Used to monitor the Program status ; 
        /// If the program is being executed then the cpu must be running
        /// the Property value can only be changed within the program
        /// </summary>
        public bool RunningState
        {
            get
            {
                if (mRunningState == State.RUN)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                return Conversions.ToBoolean(mRunningState);
            }
        }

        private State mRunningState = State.HALT;
        /// <summary>
        /// This is the cpu stack memory space; 
        /// Items being interrogated will be placed in this memeory frame
        /// calling functions will access this frame ; 
        /// the cpu stack can be considered to be a bus; Functions are devices / 
        /// or gate logic which is connected to the bus via the cpu; 
        /// </summary>
        private Stack CPU_CACHE = new Stack();
        /// <summary>
        /// Returns the Current position of the instruction Pointer 
        /// in the Program being executed The instruction Pionet can be manipulated 
        /// Jumping backwards and forwards in the program code.
        /// </summary>
        /// <returns></returns>
        private int GetInstructionAddress
        {
            get
            {
                return InstructionAdrress;
            }
        }

        public int Get_Instruction_Pointer_Position
        {
            get
            {
                return GetInstructionAddress;
            }
        }
        /// <summary>
        /// Returns the current data in the stack
        /// </summary>
        /// <returns></returns>
        public List<string> Get_Current_Stack_Data
        {
            get
            {
                List<string> Get_Current_Stack_DataRet = default;
                Get_Current_Stack_DataRet = new List<string>();
                foreach (var item in CPU_CACHE)
                    Get_Current_Stack_DataRet.Add(item.ToString());
                return Get_Current_Stack_DataRet;
            }
        }
        /// <summary>
        /// Returns the Current Cache (the stack)
        /// </summary>
        /// <returns></returns>
        public Stack View_C_P_U
        {
            get
            {
                return CPU_CACHE;
            }
        }
        /// <summary>
        /// Returns the current object on top of the stack
        /// </summary>
        /// <returns></returns>
        public object Get_Current_Stack_Item
        {
            get
            {
                return Peek();
            }
        }
        /// <summary>
        /// Used to pass the intensive error messaging required; 
        /// </summary>
        private VM_ERR CPU_ERR;
        /// <summary>
        /// Used to hold the Program being sent to the CPU
        /// A list of objects has been chosen to allow for a Richer CPU
        /// enabling for objects to be passed instead of strings; 
        /// due to this being a compiler as well as a morenized CPU
        /// converting strings to string or integers or booleans etc 
        /// makes it much harder to create quick easy code;
        /// the sender is expeected to understand the logic of the items in the program
        /// the decoder only decodes bassed on what is expected; 
        /// </summary>
        private List<object> ProgramData = new List<object>();
        /// <summary>
        /// the InstructionAdrress is the current position in the program; 
        /// it could be considered to be the line numbe
        /// </summary>
        private int InstructionAdrress;
        /// <summary>
        /// Name of current program or process running in CPU thread
        /// </summary>
        public string PROCESS_NAME = "";
        /// <summary>
        /// Used for local memory frame
        /// </summary>
        private StackMemoryFrame CurrentCache;
        /// <summary>
        /// Used to Store memory frames (The Heap)
        /// </summary>
        private Stack R_A_M = new Stack();
        /// <summary>
        /// Returns the Ram as a Stack of Stack Memeory frames;
        /// </summary>
        /// <returns></returns>
        public Stack View_R_A_M
        {
            get
            {
                return R_A_M;
            }
        }

        private int WaitTime = 0;


        /// <summary>
        /// Each Program can be considered to be a task or thread; 
        /// A name should be assigned to the Process; 
        /// Processes themselves can be stacked in a higher level processor,
        /// allowing for paralel processing of code
        /// This process allows for the initialization of the CPU; THe Prgram will still need to be loaded
        /// </summary>
        /// <param name="ThreadName"></param>
        public ZX81_CPU(ref string ThreadName)
        {
            PROCESS_NAME = ThreadName;
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
        }
        /// <summary>
        /// Load Program and Executes Code on CPU
        /// </summary>
        /// <param name="ThreadName">A name is required to Identify the Process</param>
        /// <param name="Program"></param>
        public ZX81_CPU(ref string ThreadName, ref List<string> Program)
        {
            PROCESS_NAME = ThreadName;
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
            LoadProgram(ref Program);
            mRunningState = State.RUN;
            RUN();
        }
        /// <summary>
        /// Loads items in to the program cache; 
        /// this has been added to allow for continuious running of the VM
        /// the run/wait Command will be added to the assembler 
        /// enabling for the pausing of the program and restarting of the program stack
        /// </summary>
        /// <param name="Prog"></param>
        public void LoadProgram(ref List<string> Prog)
        {
            ProgramData.AddRange(Prog);
        }
        /// <summary>
        /// Loads items in to the program cache; 
        /// this has been added to allow for continuious running of the VM
        /// the run/wait Command will be added to the assembler 
        /// enabling for the pausing of the program and restarting of the program stack
        /// </summary>
        /// <param name="Prog"></param>
        public void LoadProgram(ref List<object> Prog)
        {
            ProgramData.AddRange(Prog);
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
        }
        /// <summary>
        /// Begins eexecution of the instructions held in program data
        /// </summary>
        public void RUN()
        {
            while (IsHalted == false)
            {
                if (IsWait == true)
                {
                    for (int I = 0, loopTo = WaitTime; I <= loopTo; I++)
                    {
                    }

                    EXECUTE();
                    mRunningState = State.RUN;
                }
                else
                {
                    EXECUTE();
                }
            }
        }
        /// <summary>
        /// Checks the status of the cpu
        /// </summary>
        /// <returns></returns>
        public bool IsHalted
        {
            get
            {
                if (mRunningState == State.HALT == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsWait
        {
            get
            {
                if (Conversions.ToInteger(RunningState) == (int)State.PAUSE)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Executes the next instruction in the Program
        /// Each Instruction is fed individually to the decoder : 
        /// The Execute cycle Checks the Current State to determine 
        /// if to fetch the next instruction to be decoded;(or EXECUTED) -
        /// The decoder contains the Chip logic
        /// </summary>
        public void EXECUTE()
        {
            // The HALT command is used to STOP THE CPU
            if (IsHalted == false)
            {
                DECODE(ref Fetch());
            }
            else
            {
                // CPU_ERR = New VM_ERR("CPU HALTED", Me)
                // CPU_ERR.RaiseErr()
                // ERR - state stopped
            }
        }
        /// <summary>
        /// Program Instructions can be boolean/String or integer 
        /// so an object is assumed enabling for later classification
        /// of the instructions at the decoder level : 
        /// The Fetch Cycle Fetches the next Instruction in the Program to be executed:
        /// It is fed to the decoder to be decoded and executed
        /// </summary>
        /// <returns></returns>
        private object Fetch()
        {
            // Check that it is not the end of the program
            if (InstructionAdrress >= ProgramData.Count)
            {
                string argErr = "End of instruction list reached! No more instructions in Program data Error Invalid Address -FETCH(Missing HALT command)";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                // End of instruction list reached no more instructions in program data
                // HALT CPU!!
                mRunningState = State.HALT;
            }
            // 
            else
            {
                // Each Instruction is considered to be a string 
                // or even a integer or boolean the string is the most universal
                var CurrentInstruction = ProgramData[InstructionAdrress];
                // Move to next instruction
                InstructionAdrress += 1;
                return CurrentInstruction;
            }

            return null;
        }
        /// <summary>
        /// Contains MainInstruction Set: Decode Cycle Decodes program instructions from the program 
        /// the Insruction pointer points to the current Instruction being feed into the decoder: 
        /// Important Note : the stack will always point to the data at top of the CPU CACHE (Which is Working Memory);
        ///                 THe memory frames being used are Extensions of this memeory and can be seen as registers, 
        ///                 itself being a memory stack (stack of memory frames)
        /// </summary>
        /// <param name="ProgramInstruction"></param>
        public void DECODE(ref object ProgramInstruction)
        {
            switch (ProgramInstruction)
            {

                #region Basic Assembly
                case "WAIT":
                    {
                        WaitTime = int.Parse(Fetch().ToString());
                        mRunningState = State.PAUSE;
                        break;
                    }

                case "HALT":
                    {
                        // Stop the CPU
                        mRunningState = State.HALT;
                        break;
                    }

                case "PAUSE":
                    {
                        WaitTime = int.Parse(Fetch().ToString());
                        mRunningState = State.PAUSE;
                        break;
                    }

                case "RESUME":
                    {
                        if (mRunningState == State.PAUSE)
                        {
                            mRunningState = State.RUN;
                            RUN();
                        }

                        break;
                    }
                // A Wait time Maybe Neccasary
                case "DUP":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 1)
                            {
                                // Get Current item on the stack
                                string n = Peek();
                                // Push another copy onto the stack
                                object argValue = n;
                                Push(ref argValue);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr = "STACK ERROR : Stack Not intialized - DUP";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - DUP";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "POP":
                    {
                        CheckStackHasAtLeastOneItem();
                        Pop();
                        break;
                    }

                case "PUSH":
                    {
                        // Push
                        Push(ref Fetch());
                        break;
                    }

                case "JMP":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JMP instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JUMP(ref address);
                        break;
                    }

                case "JIF_T":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JIF instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JumpIf_TRUE(ref address);
                        break;
                    }

                case "JIF_F":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JIF instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JumpIf_False(ref address);
                        break;
                    }

                case "LOAD":
                    {
                        CheckStackHasAtLeastOneItem();
                        // lOADS A VARIABLE
                        int varNumber = int.Parse(Fetch().ToString());
                        string localGetVar() { string argVarNumber = varNumber.ToString(); var ret = GetCurrentFrame().GetVar(ref argVarNumber); return ret; }

                        CPU_CACHE.Push(localGetVar());
                        break;
                    }

                case "REMOVE":
                    {
                        // lOADS A VARIABLE
                        int varNumber = int.Parse(Fetch().ToString());
                        string argVarNumber = varNumber.ToString();
                        GetCurrentFrame().RemoveVar(ref argVarNumber);
                        break;
                    }

                case "STORE":
                    {
                        // "Should have the variable number after the STORE instruction"
                        int varNumber = int.Parse(Fetch().ToString());
                        CheckStackHasAtLeastOneItem();
                        string argVarNumber1 = varNumber.ToString();
                        string argvalue = Conversions.ToString(CPU_CACHE.Peek());
                        CurrentCache.SetVar(ref argVarNumber1, ref argvalue);
                        R_A_M.Push(CurrentCache);
                        break;
                    }

                case "CALL":
                    {
                        // The word after the instruction will contain the function address
                        int address = int.Parse(Fetch().ToString());
                        CheckJumpAddress(ref address);
                        R_A_M.Push(new StackMemoryFrame(ref InstructionAdrress));  // // Push a New stack frame on to the memory heap
                        InstructionAdrress = address; // // And jump!
                        break;
                    }

                case "RET":
                    {
                        // Pop the stack frame And return to the previous address from the memory heap
                        CheckThereIsAReturnAddress();
                        int returnAddress = GetCurrentFrame().GetReturnAddress();
                        InstructionAdrress = returnAddress;
                        R_A_M.Pop();
                        break;
                    }
                #endregion
                #region PRINT
                // PRINT TO MONITOR
                case "PRINT_M":
                    {
                        string argStr = Conversions.ToString(Pop());
                        GPU.ConsolePrint(ref argStr);
                        break;
                    }

                case "CLS":
                    {
                        GPU.Console_CLS();
                        break;
                    }
                // PRINT TO CONSOLE
                case "PRINT_C":
                    {
                        // System.Console.WriteLine("------------ ZX 81 ----------" & vbNewLine & Peek())
                        Console.ReadKey();
                        break;
                    }
                #endregion
                #region Operations
                case "ADD":
                    {
                        // ADD
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue1 = localBINARYOP();
                                Push(ref argValue1);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr1 = "Error Decoding Invalid Instruction - ADD";
                                CPU_ERR = new VM_ERR(ref argErr1, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - ADD";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "SUB":
                    {
                        // SUB
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP1() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue2 = localBINARYOP1();
                                Push(ref argValue2);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr2 = "Error Decoding Invalid Instruction - SUB";
                                CPU_ERR = new VM_ERR(ref argErr2, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - SUB";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "MUL":
                    {
                        // MUL
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP2() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue3 = localBINARYOP2();
                                Push(ref argValue3);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr3 = "Error Decoding Invalid Instruction - MUL";
                                CPU_ERR = new VM_ERR(ref argErr3, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - MUL";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "DIV":
                    {
                        // DIV
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP3() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue4 = localBINARYOP3();
                                Push(ref argValue4);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr4 = "Error Decoding Invalid Instruction - DIV";
                                CPU_ERR = new VM_ERR(ref argErr4, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - DIV";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "NOT":
                    {
                        CheckStackHasAtLeastOneItem();
                        int localNOT_ToBool() { int argVal = Conversions.ToInteger(Pop()); var ret = NOT_ToBool(ref argVal); return ret; }

                        string localToInt() { bool argBool = Conversions.ToBoolean(hsbbfb022751bd4a15816118ed6efa7986()); var ret = ToInt(ref argBool); return ret; }

                        object argValue5 = localToInt();
                        Push(ref argValue5);
                        break;
                    }

                case "AND":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP4() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue6 = localBINARYOP4();
                                Push(ref argValue6);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr5 = "Error Decoding Invalid Instruction - AND";
                                CPU_ERR = new VM_ERR(ref argErr5, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - AND";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "OR":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP5() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue7 = localBINARYOP5();
                                Push(ref argValue7);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr6 = "Error Decoding Invalid Instruction - OR";
                                CPU_ERR = new VM_ERR(ref argErr6, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - OR";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_EQ":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP6() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue8 = localBINARYOP6();
                                Push(ref argValue8);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr7 = "Error Decoding Invalid Instruction - ISEQ";
                                CPU_ERR = new VM_ERR(ref argErr7, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - OR";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_GTE":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP7() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue9 = localBINARYOP7();
                                Push(ref argValue9);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr8 = "Error Decoding Invalid Instruction - IS_GTE";
                                CPU_ERR = new VM_ERR(ref argErr8, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_GTE";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_GT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP8() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue10 = localBINARYOP8();
                                Push(ref argValue10);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr9 = "Error Decoding Invalid Instruction - IS_GT";
                                CPU_ERR = new VM_ERR(ref argErr9, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_GT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_LT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP9() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue11 = localBINARYOP9();
                                Push(ref argValue11);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr10 = "Error Decoding Invalid Instruction - IS_LT";
                                CPU_ERR = new VM_ERR(ref argErr10, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_LT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_LTE":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP10() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue12 = localBINARYOP10();
                                Push(ref argValue12);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr11 = "Error Decoding Invalid Instruction - IS_LTE";
                                CPU_ERR = new VM_ERR(ref argErr11, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_LTE";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region POSITIVE VS NEGATIVE
                case "TO_POS":
                    {
                        if (CPU_CACHE.Count >= 1)
                        {
                            var argValue13 = ToPositive(int.Parse(Conversions.ToString(Pop())));
                            Push(ref argValue13);
                        }
                        else
                        {
                            mRunningState = State.HALT;
                            string argErr12 = "Error Decoding Invalid arguments - POS";
                            CPU_ERR = new VM_ERR(ref argErr12, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "TO_NEG":
                    {
                        if (CPU_CACHE.Count >= 1)
                        {
                            var argValue14 = ToNegative(int.Parse(Conversions.ToString(Pop())));
                            Push(ref argValue14);
                        }
                        else
                        {
                            mRunningState = State.HALT;
                            string argErr13 = "Error Decoding Invalid arguments - NEG";
                            CPU_ERR = new VM_ERR(ref argErr13, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region Extended JmpCmds
                case "JIF_GT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION = "IS_GT";
                                object argValue15 = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue15);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr14 = "Error Decoding Invalid arguments - JIF_GT";
                                CPU_ERR = new VM_ERR(ref argErr14, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_GT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "JIF_LT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION1 = "IS_LT";
                                object argValue16 = BINARYOP(ref argINSTRUCTION1, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue16);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr15 = "Error Decoding Invalid arguments - JIF_LT";
                                CPU_ERR = new VM_ERR(ref argErr15, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_LT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "JIF_EQ":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION2 = "IS_EQ";
                                object argValue17 = BINARYOP(ref argINSTRUCTION2, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue17);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr16 = "Error Decoding Invalid arguments - JIF_EQ";
                                CPU_ERR = new VM_ERR(ref argErr16, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_EQ";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region INCREMENT/DECREMENT
                case "INCR":
                    {
                        CheckStackHasAtLeastOneItem();
                        object argValue18 = int.Parse(Conversions.ToString(Pop())) + 1;
                        Push(ref argValue18);
                        break;
                    }

                case "DECR":
                    {
                        CheckStackHasAtLeastOneItem();
                        #endregion

                        object argValue19 = int.Parse(Conversions.ToString(Pop())) - 1;
                        Push(ref argValue19);
                        break;
                    }

                default:
                    {
                        mRunningState = State.HALT;
                        break;
                    }
                    // CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction", Me)
                    // CPU_ERR.RaiseErr()

            }
        }
        #endregion
        #region functions required by cpu and assembly language
        #region Handle Boolean
        private string ToInt(ref bool Bool)
        {
            if (Bool == false)
            {
                return 0.ToString();
            }
            else
            {
                return 1.ToString();
            }
        }

        private bool ToBool(ref int Val)
        {
            if (Val == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int NOT_ToBool(ref int Val)
        {
            if (Val == 1)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        #endregion
        #region Functional Parts
        /// <summary>
        /// Checks if there is a jump address available
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool CheckJumpAddress(ref int address)
        {
            try
            {
                // Check if it is in range
                if (address < 0 | address >= ProgramData.Count)
                {
                    // Not in range
                    mRunningState = State.HALT;
                    string argErr = string.Format("Invalid jump address %d at %d", address, GetInstructionAddress);
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = string.Format("Invalid jump address %d at %d", address, GetInstructionAddress);
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return false;
            }
        }
        /// <summary>
        /// Function used by the internal functions to check if there is a return address
        /// </summary>
        /// <returns></returns>
        private bool CheckThereIsAReturnAddress()
        {
            try
            {
                if (R_A_M.Count >= 1)
                {
                    return true;
                }
                else
                {
                    mRunningState = State.HALT;
                    string argErr = string.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress);
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    return false;
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = string.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress);
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return false;
            }
        }
        /// <summary>
        /// RAM is a STACK MEMORY - Here we can take a look at the stack item
        /// </summary>
        /// <returns></returns>
        public StackMemoryFrame GetCurrentFrame()
        {
            if (R_A_M.Count > 0)
            {
                return (StackMemoryFrame)R_A_M.Pop();
            }
            else
            {
                return null;
                mRunningState = State.HALT;
                string argErr = "Error Decoding STACK MEMORY FRAME - GetCurrentFrame";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
            }
        }
        /// <summary>
        /// Outputs stack data for verbose output
        /// </summary>
        /// <returns></returns>
        public string GetStackData()
        {
            string Str = "";
            object argOBJ = this;
            Str = ToJson(ref argOBJ);
            return Str;
        }

        private string ToJson(ref object OBJ)
        {
            var Converter = new JavaScriptSerializer();
            return Converter.Serialize(OBJ);
        }
        #endregion
        #region Operational Functions
        /// <summary>
        /// REQUIRED TO SEE IN-SIDE CURRENT POINTER LOCATION
        /// ----------Public For Testing Purposes-----------
        /// </summary>
        /// <returns></returns>
        public string Peek()
        {
            try
            {
                if (CPU_CACHE.Count > 0)
                {
                    return CPU_CACHE.Peek().ToString();
                }
                else
                {
                    mRunningState = State.HALT;
                    return "NULL";
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = "NULL POINTER CPU HALTED";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return "NULL";
            }
        }

        private string BINARYOP(ref string INSTRUCTION, int LEFT, int RIGHT)
        {
            if (INSTRUCTION is object)
            {
                switch (INSTRUCTION ?? "")
                {
                    case "IS_EQ":
                        {
                            try
                            {
                                string localToInt() { bool argBool = ToBool(ref LEFT) == ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal = Conversions.ToInteger(localToInt());
                                return Conversions.ToString(ToBool(ref argVal));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation - isEQ";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_GT":
                        {
                            try
                            {
                                string localToInt1() { bool argBool = ToBool(ref LEFT) < ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal1 = Conversions.ToInteger(localToInt1());
                                return Conversions.ToString(ToBool(ref argVal1));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation - isGT";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_GTE":
                        {
                            try
                            {
                                string localToInt2() { bool argBool = ToBool(ref LEFT) <= ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal2 = Conversions.ToInteger(localToInt2());
                                return Conversions.ToString(ToBool(ref argVal2));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isGTE";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_LT":
                        {
                            try
                            {
                                string localToInt3() { bool argBool = ToBool(ref LEFT) > ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal3 = Conversions.ToInteger(localToInt3());
                                return Conversions.ToString(ToBool(ref argVal3));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isLT";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_LE":
                        {
                            try
                            {
                                string localToInt4() { bool argBool = ToBool(ref LEFT) >= ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal4 = Conversions.ToInteger(localToInt4());
                                return Conversions.ToString(ToBool(ref argVal4));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isLTE";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "ADD":
                        {
                            try
                            {
                                return (LEFT + RIGHT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -add";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "SUB":
                        {
                            try
                            {
                                return (RIGHT - LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -sub";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "MUL":
                        {
                            try
                            {
                                return (RIGHT * LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -mul";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "DIV":
                        {
                            try
                            {
                                return (RIGHT / (double)LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -div";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "OR":
                        {
                            try
                            {
                                bool argBool = ToBool(ref LEFT) | ToBool(ref RIGHT);
                                return ToInt(ref argBool);
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -or";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "AND":
                        {
                            try
                            {
                                string localToInt5() { bool argBool = ToBool(ref LEFT) & ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal5 = Conversions.ToInteger(localToInt5());
                                return Conversions.ToString(ToBool(ref argVal5));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -and";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "NOT":
                        {
                            CheckStackHasAtLeastOneItem();
                            int localNOT_ToBool() { int argVal = Conversions.ToInteger(Pop()); var ret = NOT_ToBool(ref argVal); return ret; }

                            string localToInt6() { bool argBool = Conversions.ToBoolean(hs42d0a06c3e9f4e8c9a0a64a4a46ff390()); var ret = ToInt(ref argBool); return ret; }

                            object argValue = localToInt6();
                            Push(ref argValue);
                            break;
                        }

                    default:
                        {
                            mRunningState = State.HALT;
                            string argErr = "Invalid Operation -not";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                            break;
                        }
                }
            }
            else
            {
                mRunningState = State.HALT;
            }

            return "NULL";
        }

        private bool CheckStackHasAtLeastOneItem(ref Stack Current)
        {
            if (Current.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckStackHasAtLeastOneItem()
        {
            if (CPU_CACHE.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRamHasAtLeastOneItem()
        {
            if (CPU_CACHE.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void JumpIf_TRUE(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true & CheckStackHasAtLeastOneItem() == true)
            {
                int argVal = Conversions.ToInteger(Pop());
                if (ToBool(ref argVal))
                {
                    InstructionAdrress = Address;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void JUMP(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true)
            {
                InstructionAdrress = Address;
            }
        }

        private void JumpIf_False(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true & CheckStackHasAtLeastOneItem() == true)
            {
                int argVal = Conversions.ToInteger(Pop());
                if (Conversions.ToBoolean(NOT_ToBool(ref argVal)))
                {
                    InstructionAdrress = Address;
                }
                else
                {
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// Puts a value on the cpu stack to be available to funcitons
        /// </summary>
        /// <param name="Value"></param>
        private void Push(ref object Value)
        {
            try
            {
                CPU_CACHE.Push(Value);
            }
            catch (Exception ex)
            {
                string argErr = "STACK ERROR - CPU HALTED -push";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                mRunningState = State.HALT;
            }
        }
        /// <summary>
        /// Pops a value of the cpu_Stack (current workspace)
        /// </summary>
        /// <returns></returns>
        private object Pop()
        {
            try
            {
                if (CPU_CACHE.Count >= 1)
                {
                    return CPU_CACHE.Pop();
                }
                else
                {
                    string argErr = "STACK ERROR - NULL POINTER CPU HALTED -pop";
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    mRunningState = State.HALT;
                    return "NULL";
                }
            }
            catch (Exception ex)
            {
                string argErr = "STACK ERROR - NULL POINTER CPU HALTED -pop";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                mRunningState = State.HALT;
                return "NULL";
            }

            return "NULL";
        }
        #endregion
        private object ToPositive(int Number)
        {
            return Math.Abs(Number);
        }

        private object ToNegative(int Number)
        {
            return Math.Abs(Number) * -1;
        }
        #endregion
        #region CPU _ INTERNAL _ Components
        private enum State
        {
            RUN,
            HALT,
            PAUSE
        }
        /// <summary>
        /// Memory frame for Variables
        /// </summary>
        public class StackMemoryFrame
        {
            public struct Var
            {
                public string Value;
                public string VarNumber;
            }

            public int ReturnAddress;
            public List<Var> Variables;

            public StackMemoryFrame(ref int ReturnAddress)
            {
                ReturnAddress = ReturnAddress;
                Variables = new List<Var>();
            }

            public int GetReturnAddress()
            {
                return ReturnAddress;
            }

            public string GetVar(ref string VarNumber)
            {
                foreach (var item in Variables)
                {
                    if ((item.VarNumber ?? "") == (VarNumber ?? ""))
                    {
                        return item.Value;
                    }
                }

                return 0.ToString();
            }

            public void SetVar(ref string VarNumber, ref string value)
            {
                var item = new Var();
                bool added = false;
                item.VarNumber = VarNumber;
                item.Value = value;
                foreach (var ITM in Variables)
                {
                    if ((ITM.VarNumber ?? "") == (VarNumber ?? ""))
                    {
                        ITM.Value = value;
                    }
                }

                Variables.Add(item);
            }

            public void RemoveVar(ref string VarNumber)
            {
                foreach (var item in Variables)
                {
                    if ((item.VarNumber ?? "") == (VarNumber ?? "") == true)
                    {
                        Variables.Remove(item);
                        break;
                    }
                    else
                    {
                    }
                }
            }
        }

        public class VM_ERR
        {
            private string ErrorStr = "";
            private SAL_ZX21_VDU frm = new SAL_ZX21_VDU();
            private ZX81_CPU CpuCurrentState;

            public VM_ERR(ref string Err, ZX81_CPU CPUSTATE)
            {
                ErrorStr = Err;
                CpuCurrentState = CPUSTATE;
            }

            public void RaiseErr()
            {
                if (frm is null)
                {
                    frm = new SAL_ZX21_VDU();
                    frm.Show();
                    string argUserinput = ErrorStr + Constants.vbNewLine + CpuCurrentState.GetStackData();
                    frm.Print(ref argUserinput);
                }
                else
                {
                    frm.Show();
                    string argUserinput1 = ErrorStr + Constants.vbNewLine + CpuCurrentState.GetStackData();
                    frm.Print(ref argUserinput1);
                }
            }
        }
        #endregion
        /// <summary>
        /// COMMANDS FOR ASSEMBLY LANGUAGE FOR THIS CPU
        /// SPYDAZWEB_VM_X86
        /// </summary>
        public enum VM_x86_Cmds
        {
            _NULL,
            _REMOVE,
            _RESUME,
            _PUSH,
            _PULL,
            _PEEK,
            _WAIT,
            _PAUSE,
            _HALT,
            _DUP,
            _JMP,
            _JIF_T,
            _JIF_F,
            _JIF_EQ,
            _JIF_GT,
            _JIF_LT,
            _LOAD,
            _STORE,
            _CALL,
            _RET,
            _PRINT_M,
            _PRINT_C,
            _ADD,
            _SUB,
            _MUL,
            _DIV,
            _AND,
            _OR,
            _NOT,
            _IS_EQ,
            _IS_GT,
            _IS_GTE,
            _IS_LT,
            _IS_LTE,
            _TO_POS,
            _TO_NEG,
            _INCR,
            _DECR
        }
    }
}
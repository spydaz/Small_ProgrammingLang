using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SAL_VM.SAL;
using SAL_VM.SmallProgLang;
using SAL_VM.SmallProgLang.Ast_ExpressionFactory;
using SAL_VM.SmallProgLang.Compiler;

namespace SAL_VM
{
    public partial class SAL_ZX21_SPL_BASIC_REPL
    {
        public SAL_ZX21_SPL_BASIC_REPL()
        {
            InitializeComponent();
            _Small_PL_NewToolStripButton.Name = "Small_PL_NewToolStripButton";
            _Small_PL_OpenToolStripButton.Name = "Small_PL_OpenToolStripButton";
            _Small_PL_SaveToolStripButton.Name = "Small_PL_SaveToolStripButton";
            _Small_PL_ToolStripButtonCompileCode.Name = "Small_PL_ToolStripButtonCompileCode";
            _Small_PL_ButtonOpenVM.Name = "Small_PL_ButtonOpenVM";
            _Small_PL_HelpToolStripButton.Name = "Small_PL_HelpToolStripButton";
            _Small_PL_AstTreeView.Name = "Small_PL_AstTreeView";
            _SAL_NewToolStripButton.Name = "SAL_NewToolStripButton";
            _SAL_OpenToolStripButton.Name = "SAL_OpenToolStripButton";
            _SAL_SaveToolStripButton.Name = "SAL_SaveToolStripButton";
            _SAL_ToolStripButtonCompileCode.Name = "SAL_ToolStripButtonCompileCode";
            _SAL_ButtonOpenVM.Name = "SAL_ButtonOpenVM";
            _ToolStripButton_RUN_LOGO.Name = "ToolStripButton_RUN_LOGO";
        }
        #region SMALL_PL
        private ParserFactory PSER = new ParserFactory();

        private void ToolStripButtonCompile_Click(object sender, EventArgs e)
        {
            string InputCode = Small_PL_TextBoxCodeInput.Text;
            PSER = new ParserFactory();
            var outputStr = PSER.ParseFactory(ref InputCode);
            Small_PL_AstTreeView.Nodes.Clear();
            loadTree(ref outputStr);
            Small_PL_TextBoxREPL_OUTPUT.Text = EXT.FormatJsonOutput(outputStr.ToJson());
            Small_PL_TextboxErrors.Text = "";
            if (PSER.ParserErrors is object)
            {
                if (PSER.ParserErrors.Count > 0)
                {
                    Small_PL_TextboxErrors.Text = "Error in Syntax :" + Constants.vbNewLine;
                    foreach (var item in PSER.ParserErrors)
                        Small_PL_TextboxErrors.Text += Constants.vbNewLine + item + Constants.vbNewLine;
                    if (outputStr.Body is object)
                    {
                        foreach (var item in outputStr.Body)
                        {
                            Small_PL_TextboxErrors.ForeColor = Color.Red;
                            Small_PL_TextboxErrors.Text += Constants.vbNewLine + item.ToJson() + Constants.vbNewLine;
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    Small_PL_TextboxErrors.ForeColor = Color.Green;
                    Small_PL_TextboxErrors.Text = "all Passed sucessfully" + Constants.vbNewLine;
                }
            }
        }

        public void loadTree(ref AstProgram Prog)
        {
            Small_PL_AstTreeView.Nodes.Clear();
            var root = new TreeNode();
            if (PSER.ParserErrors.Count > 0)
            {
                root.ForeColor = Color.Red;
            }
            else
            {
                root.ForeColor = Color.GreenYellow;
            }

            root.Text = Prog._TypeStr + Constants.vbNewLine;
            root.Tag = EXT.FormatJsonOutput(Prog.ToJson());
            var Body = new TreeNode();
            Body.Text = "Body";
            Body.Tag = EXT.FormatJsonOutput(Prog.ToJson());
            foreach (var item in Prog.Body)
            {
                var MainNode = new TreeNode();
                MainNode.Text = EXT.FormatJsonOutput(item.ToJson());
                MainNode.Tag = EXT.FormatJsonOutput(item.ToJson());
                var RawNode = new TreeNode();
                if (PSER.ParserErrors.Count > 0)
                {
                    RawNode.ForeColor = Color.Red;
                }
                else
                {
                    RawNode.ForeColor = Color.GreenYellow;
                }

                RawNode.Text = "_Raw :" + item._Raw;
                RawNode.Tag = "_raw";
                MainNode.Nodes.Add(RawNode);
                var _StartNode = new TreeNode();
                _StartNode.Text = "_Start :" + item._Start;
                _StartNode.Tag = "_Start";
                MainNode.Nodes.Add(_StartNode);
                var _EndNode = new TreeNode();
                _EndNode.Text = "_End :" + item._End;
                _EndNode.Tag = "_End";
                MainNode.Nodes.Add(_EndNode);
                var _TypeNode = new TreeNode();
                if (PSER.ParserErrors.Count > 0)
                {
                    _TypeNode.ForeColor = Color.Red;
                }
                else
                {
                    _TypeNode.ForeColor = Color.GreenYellow;
                }

                _TypeNode.Text = "_Type :" + item._TypeStr;
                _TypeNode.Tag = "_Type";
                MainNode.Nodes.Add(_TypeNode);
                Body.Nodes.Add(MainNode);
            }

            root.Nodes.Add(Body);
            Small_PL_AstTreeView.Nodes.Add(root);
            Small_PL_AstTreeView.ExpandAll();
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            StreamReader sr;

            // Supposing you haven't already set these properties...
            {
                var withBlock = OpenTextFileDialog;
                withBlock.FileName = "";
                withBlock.Title = "Open a Program file...";
                withBlock.InitialDirectory = @"C:\";
                withBlock.Filter = " Program Files|*.*";
            }

            if (OpenTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sr = new StreamReader(OpenTextFileDialog.FileName);
                    Small_PL_TextBoxCodeInput.Text = OpenTextFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    Small_PL_TextboxErrors.Text = "The file specified could not be opened." + Constants.vbNewLine + "Error message:" + Constants.vbNewLine + Constants.vbNewLine + ex.Message + Constants.vbNewLine + " File Could Not Be Opened!";
                }
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            StreamWriter sr;

            // Supposing you haven't already set these properties...
            {
                var withBlock = SaveTextFileDialog;
                withBlock.FileName = "";
                withBlock.Title = "Save a Program file...";
                withBlock.InitialDirectory = @"C:\";
                withBlock.Filter = " Program Files|*.*";
            }

            if (SaveTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sr = new StreamWriter(SaveTextFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    Small_PL_TextboxErrors.Text = "The file specified could not be opened." + Constants.vbNewLine + "Error message:" + Constants.vbNewLine + ex.Message + "File Could Not Be Opened!";
                }
            }
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            Small_PL_TextBoxCodeInput.Text = "";
            Small_PL_TextBoxREPL_OUTPUT.Clear();
            Small_PL_TextBoxCodeInput.Clear();
            Small_PL_AstTreeView.Nodes.Clear();
        }

        public SAL_ZX21_ConsoleUI VM;
        #endregion
        #region SAL REPL
        private void ToolStripButtonCompileCode_Click(object sender, EventArgs e)
        {
            var PROG = Strings.Split(SAL_TextBoxCodeInput.Text.Replace(Constants.vbCrLf, " "), " ");
            SAL_RichTextBoxProgram.Text = PROG.ToJson();
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
                    InstructionLst.Add(Strings.UCase(item));
                }
            }
            // cpu - START

            // Dim CPU As ZX81_CPU = New ZX81_CPU(InstructionLst)
            // CPU.Run()
            string argThreadName = "Test";
            var CPU = new ZX81_CPU(ref argThreadName, ref InstructionLst);
            string argOutputStr = "CURRENT POINTER = " + CPU.Get_Instruction_Pointer_Position + Constants.vbNewLine + "CONTAINED DATA = " + CPU.Peek();
            DisplayOutput(ref argOutputStr);
            SAL_AST.Nodes.Add(ROOT);
        }

        public void DisplayOutput(ref string OutputStr)
        {
            SAL_RichTextBoxDisplayOutput.Text = OutputStr;
        }

        public void DisplayError(ref string ErrorStr)
        {
            SAL_TextBoxErrorOutput.Text += ErrorStr;
        }

        private void SAL_NewToolStripButton_Click(object sender, EventArgs e)
        {
            SAL_RichTextBoxDisplayOutput.Text = "";
            SAL_AST.Nodes.Clear();
            SAL_TextBoxCodeInput.Text = "";
        }

        private void SAL_OpenToolStripButton_Click(object sender, EventArgs e)
        {
            StreamReader sr;

            // Supposing you haven't already set these properties...
            {
                var withBlock = OpenTextFileDialog;
                withBlock.FileName = "";
                withBlock.Title = "Open a Program file...";
                withBlock.InitialDirectory = @"C:\";
                withBlock.Filter = " Program Files|*.*";
            }

            if (OpenTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sr = new StreamReader(OpenTextFileDialog.FileName);
                    SAL_TextBoxCodeInput.Text = OpenTextFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    SAL_TextBoxErrorOutput.Text = "The file specified could not be opened." + Constants.vbNewLine + "Error message:" + Constants.vbNewLine + Constants.vbNewLine + ex.Message + Constants.vbNewLine + " File Could Not Be Opened!";
                }
            }
        }

        private void SAL_SaveToolStripButton_Click(object sender, EventArgs e)
        {
            StreamWriter sr;

            // Supposing you haven't already set these properties...
            {
                var withBlock = SaveTextFileDialog;
                withBlock.FileName = "";
                withBlock.Title = "Save a Program file...";
                withBlock.InitialDirectory = @"C:\";
                withBlock.Filter = " Program Files|*.*";
            }

            if (SaveTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sr = new StreamWriter(SaveTextFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    SAL_TextBoxErrorOutput.Text = "The file specified could not be opened." + Constants.vbNewLine + "Error message:" + Constants.vbNewLine + ex.Message + "File Could Not Be Opened!";
                }
            }
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            var help = new Process();
            help.StartInfo.UseShellExecute = true;
            help.StartInfo.FileName = @"c:\windows\hh.exe";
            help.StartInfo.Arguments = Application.StartupPath + @"\help\SpydazWeb AI _Emulators.chm";
            help.Start();
        }

        private void ButtonOpenVM_Click(object sender, EventArgs e)
        {
            VM = new SAL_ZX21_ConsoleUI();
            VM.Show();
        }

        private void Small_PL_AstTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Small_PL_TextBoxREPL_OUTPUT.Text = Conversions.ToString(Small_PL_AstTreeView.SelectedNode.Tag);
        }

        public TURTLE Iturtle;


        #endregion
        private void Multi_REPL_Load(object sender, EventArgs e)
        {
            // 'LOGO PANEL TEST
            // Iturtle = New TURTLE(logo_display_panel, My.Resources.Icon_UpVote)

            // Iturtle.SetPenWidth(2)
            // Iturtle._PenStatus = TURTLE.PenStatus.Down
            // Iturtle._Reset()

            // Iturtle._forward(45)
            // Iturtle._Right(45)
            // Iturtle._forward(45)
            // Iturtle._Reset()
        }

        public void loadLogoTree(ref AstProgram Prog)
        {
            LOGO_TreeView.Nodes.Clear();
            var root = new TreeNode();
            if (PSER.ParserErrors.Count > 0)
            {
                root.ForeColor = Color.Red;
            }
            else
            {
                root.ForeColor = Color.GreenYellow;
            }

            root.Text = Prog._TypeStr + Constants.vbNewLine;
            root.Tag = EXT.FormatJsonOutput(Prog.ToJson());
            var Body = new TreeNode();
            Body.Text = "Body";
            Body.Tag = EXT.FormatJsonOutput(Prog.ToJson());
            foreach (var item in Prog.Body)
            {
                var MainNode = new TreeNode();
                MainNode.Text = EXT.FormatJsonOutput(item.ToJson());
                MainNode.Tag = EXT.FormatJsonOutput(item.ToJson());
                var RawNode = new TreeNode();
                if (PSER.ParserErrors.Count > 0)
                {
                    RawNode.ForeColor = Color.Red;
                }
                else
                {
                    RawNode.ForeColor = Color.GreenYellow;
                }

                RawNode.Text = "_Raw :" + item._Raw;
                RawNode.Tag = "_raw";
                MainNode.Nodes.Add(RawNode);
                var _StartNode = new TreeNode();
                _StartNode.Text = "_Start :" + item._Start;
                _StartNode.Tag = "_Start";
                MainNode.Nodes.Add(_StartNode);
                var _EndNode = new TreeNode();
                _EndNode.Text = "_End :" + item._End;
                _EndNode.Tag = "_End";
                MainNode.Nodes.Add(_EndNode);
                var _TypeNode = new TreeNode();
                if (PSER.ParserErrors.Count > 0)
                {
                    _TypeNode.ForeColor = Color.Red;
                }
                else
                {
                    _TypeNode.ForeColor = Color.GreenYellow;
                }

                _TypeNode.Text = "_Type :" + item._TypeStr;
                _TypeNode.Tag = "_Type";
                MainNode.Nodes.Add(_TypeNode);
                Body.Nodes.Add(MainNode);
            }

            root.Nodes.Add(Body);
            LOGO_TreeView.Nodes.Add(root);
            LOGO_TreeView.ExpandAll();
        }

        private void ToolStripButton_RUN_LOGO_Click(object sender, EventArgs e)
        {
            string InputCode = PROGRAM_TEXTBOX.Text;
            var logo_PSER = new LogoParser();
            var outputStr = logo_PSER._Parse(ref InputCode);
            LOGO_TreeView.Nodes.Clear();
            loadLogoTree(ref outputStr);
            LogoTextOut.Text = EXT.FormatJsonOutput(outputStr.ToJson());
            LOGO_ERRORS.Text = "";
            if (logo_PSER.ParserErrors is object)
            {
                if (logo_PSER.ParserErrors.Count > 0)
                {
                    LOGO_ERRORS.Text = "Error in Syntax :" + Constants.vbNewLine;
                    foreach (var item in logo_PSER.ParserErrors)
                        LOGO_ERRORS.Text += Constants.vbNewLine + item + Constants.vbNewLine;
                    if (outputStr.Body is object)
                    {
                        foreach (var item in outputStr.Body)
                        {
                            Small_PL_TextboxErrors.ForeColor = Color.Red;
                            LOGO_ERRORS.Text += Constants.vbNewLine + item.ToJson() + Constants.vbNewLine;
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    LOGO_ERRORS.ForeColor = Color.Green;
                    LOGO_ERRORS.Text = "all Passed sucessfully" + Constants.vbNewLine;
                }
            }
        }
    }
    // REPL_ERROR SYSTEM
    // 
    namespace Repl
    {
        public class PL_ReplErrorSystem
        {


            /// <summary>
        /// Creates an Error Message to be displayed
        /// </summary>
        /// <param name="ErrorStr"></param>
        /// <param name="Errtok"></param>
        /// <returns></returns>
            public static string DisplayError(ref string ErrorStr, ref Ast_Literal Errtok)
            {
                string str = ErrorStr + Constants.vbNewLine + Errtok.ToJson().FormatJsonOutput();
                return str;
            }
        }

        public class SAL_ReplErrorSystem
        {


            /// <summary>
        /// Creates an Error Message to be displayed
        /// </summary>
        /// <param name="ErrorStr"></param>
        /// <param name="Errtok"></param>
        /// <returns></returns>
            public static string DisplayError(ref string ErrorStr, ref string Errtok)
            {
                string str = ErrorStr + Constants.vbNewLine + Errtok.ToJson().FormatJsonOutput();
                return str;
            }
        }
    }
}
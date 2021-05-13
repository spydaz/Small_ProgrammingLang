using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SDK
{
    [DesignerGenerated()]
    public partial class SAL_ZX21_SPL_BASIC_REPL : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SAL_ZX21_SPL_BASIC_REPL));
            TabControl_ProgrammingLang_Repl = new TabControl();
            TabPageSpydaz_PL = new TabPage();
            TabControlRepl_Inner = new TabControl();
            TabPageSmall_PL_repl = new TabPage();
            SplitContainer3 = new SplitContainer();
            SplitContainer4 = new SplitContainer();
            GroupBox7 = new GroupBox();
            TabControl3 = new TabControl();
            TabPage3 = new TabPage();
            Small_PL_TextBoxCodeInput = new TextBox();
            Small_PL_ToolStrip = new ToolStrip();
            ToolStripSeparator15 = new ToolStripSeparator();
            _Small_PL_NewToolStripButton = new ToolStripButton();
            _Small_PL_NewToolStripButton.Click += new EventHandler(NewToolStripButton_Click);
            ToolStripSeparator10 = new ToolStripSeparator();
            _Small_PL_OpenToolStripButton = new ToolStripButton();
            _Small_PL_OpenToolStripButton.Click += new EventHandler(OpenToolStripButton_Click);
            ToolStripSeparator11 = new ToolStripSeparator();
            _Small_PL_SaveToolStripButton = new ToolStripButton();
            _Small_PL_SaveToolStripButton.Click += new EventHandler(SaveToolStripButton_Click);
            ToolStripSeparator12 = new ToolStripSeparator();
            _Small_PL_ToolStripButtonCompileCode = new ToolStripButton();
            _Small_PL_ToolStripButtonCompileCode.Click += new EventHandler(ToolStripButtonCompile_Click);
            ToolStripSeparator14 = new ToolStripSeparator();
            Small_PL_ToolStripButtonRunCode = new ToolStripButton();
            ToolStripSeparator16 = new ToolStripSeparator();
            ToolStripSeparator13 = new ToolStripSeparator();
            Small_PL_ToolStripButtonCompilesTox86 = new ToolStripButton();
            ToolStripSeparator17 = new ToolStripSeparator();
            _Small_PL_ButtonOpenVM = new ToolStripButton();
            _Small_PL_ButtonOpenVM.Click += new EventHandler(ButtonOpenVM_Click);
            ToolStripSeparator18 = new ToolStripSeparator();
            _Small_PL_HelpToolStripButton = new ToolStripButton();
            _Small_PL_HelpToolStripButton.Click += new EventHandler(HelpToolStripButton_Click);
            ToolStripSeparator19 = new ToolStripSeparator();
            GroupBox8 = new GroupBox();
            Small_PL_TextBoxREPL_OUTPUT = new TextBox();
            SplitContainer5 = new SplitContainer();
            GroupBox9 = new GroupBox();
            Small_PL_TabControl_Repl_ErrorOutput = new TabControl();
            TabPageReplErrors = new TabPage();
            Small_PL_TextboxErrors = new RichTextBox();
            TabPagePl_Help = new TabPage();
            RichTextBoxPL_Help = new RichTextBox();
            GroupBox10 = new GroupBox();
            _Small_PL_AstTreeView = new TreeView();
            _Small_PL_AstTreeView.AfterSelect += new TreeViewEventHandler(Small_PL_AstTreeView_AfterSelect);
            TabPageSAL_REPL = new TabPage();
            SplitContainer1 = new SplitContainer();
            GroupBox1 = new GroupBox();
            GroupBox6 = new GroupBox();
            SAL_RichTextBoxDisplayOutput = new RichTextBox();
            GroupBox5 = new GroupBox();
            SAL_AST = new TreeView();
            GroupBox2 = new GroupBox();
            SAL_RichTextBoxProgram = new TextBox();
            SplitContainer2 = new SplitContainer();
            GroupBox3 = new GroupBox();
            TabControl2 = new TabControl();
            TabPageSalReplHelp = new TabPage();
            SAL_RichTextBoxHelp = new RichTextBox();
            TabPageSalReplErrors = new TabPage();
            SAL_TextBoxErrorOutput = new TextBox();
            GroupBox4 = new GroupBox();
            TabControl_REPL_INPUT = new TabControl();
            TabPage2 = new TabPage();
            SAL_TextBoxCodeInput = new TextBox();
            SAL_ToolStripRepl = new ToolStrip();
            _SAL_NewToolStripButton = new ToolStripButton();
            _SAL_NewToolStripButton.Click += new EventHandler(SAL_NewToolStripButton_Click);
            ToolStripSeparator7 = new ToolStripSeparator();
            _SAL_OpenToolStripButton = new ToolStripButton();
            _SAL_OpenToolStripButton.Click += new EventHandler(SAL_OpenToolStripButton_Click);
            ToolStripSeparator6 = new ToolStripSeparator();
            _SAL_SaveToolStripButton = new ToolStripButton();
            _SAL_SaveToolStripButton.Click += new EventHandler(SAL_SaveToolStripButton_Click);
            toolStripSeparator = new ToolStripSeparator();
            ToolStripSeparator8 = new ToolStripSeparator();
            toolStripSeparator1 = new ToolStripSeparator();
            _SAL_ToolStripButtonCompileCode = new ToolStripButton();
            _SAL_ToolStripButtonCompileCode.Click += new EventHandler(ToolStripButtonCompileCode_Click);
            ToolStripSeparator9 = new ToolStripSeparator();
            SAL_ToolStripButtonRunCode = new ToolStripButton();
            ToolStripSeparator3 = new ToolStripSeparator();
            ToolStripSeparator2 = new ToolStripSeparator();
            _SAL_ButtonOpenVM = new ToolStripButton();
            _SAL_ButtonOpenVM.Click += new EventHandler(ButtonOpenVM_Click);
            ToolStripSeparator4 = new ToolStripSeparator();
            ToolStripSeparator5 = new ToolStripSeparator();
            TabPageLOGO = new TabPage();
            SplitContainer6 = new SplitContainer();
            SplitContainer7 = new SplitContainer();
            GroupBox12 = new GroupBox();
            TabControl4 = new TabControl();
            TabPage5 = new TabPage();
            PROGRAM_TEXTBOX = new TextBox();
            LOGO_ToolStrip = new ToolStrip();
            ToolStripSeparator27 = new ToolStripSeparator();
            ToolStripButtonLOGO_NEW = new ToolStripButton();
            ToolStripSeparator20 = new ToolStripSeparator();
            ToolStripSeparator29 = new ToolStripSeparator();
            ToolStripButtonLOGO_OPEN = new ToolStripButton();
            ToolStripSeparator21 = new ToolStripSeparator();
            ToolStripSeparator25 = new ToolStripSeparator();
            ToolStripButtonLOGO_SAVE = new ToolStripButton();
            ToolStripSeparator22 = new ToolStripSeparator();
            _ToolStripButton_RUN_LOGO = new ToolStripButton();
            _ToolStripButton_RUN_LOGO.Click += new EventHandler(ToolStripButton_RUN_LOGO_Click);
            ToolStripSeparator24 = new ToolStripSeparator();
            ToolStripButtonEXECUTE_LOGO = new ToolStripButton();
            ToolStripSeparator23 = new ToolStripSeparator();
            ToolStripSeparator26 = new ToolStripSeparator();
            ToolStripSeparator28 = new ToolStripSeparator();
            GroupBox11 = new GroupBox();
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            LOGO_ERRORS = new RichTextBox();
            TabPage4 = new TabPage();
            LOGO_HELP = new RichTextBox();
            SplitContainer8 = new SplitContainer();
            GroupBox15 = new GroupBox();
            logo_display_panel = new Panel();
            SplitContainer9 = new SplitContainer();
            GroupBox13 = new GroupBox();
            LOGO_TreeView = new TreeView();
            GroupBox14 = new GroupBox();
            LogoTextOut = new RichTextBox();
            OpenTextFileDialog = new OpenFileDialog();
            SaveTextFileDialog = new SaveFileDialog();
            ToolTip1 = new ToolTip(components);
            TabControl_ProgrammingLang_Repl.SuspendLayout();
            TabPageSpydaz_PL.SuspendLayout();
            TabControlRepl_Inner.SuspendLayout();
            TabPageSmall_PL_repl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer3).BeginInit();
            SplitContainer3.Panel1.SuspendLayout();
            SplitContainer3.Panel2.SuspendLayout();
            SplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer4).BeginInit();
            SplitContainer4.Panel1.SuspendLayout();
            SplitContainer4.Panel2.SuspendLayout();
            SplitContainer4.SuspendLayout();
            GroupBox7.SuspendLayout();
            TabControl3.SuspendLayout();
            TabPage3.SuspendLayout();
            Small_PL_ToolStrip.SuspendLayout();
            GroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer5).BeginInit();
            SplitContainer5.Panel1.SuspendLayout();
            SplitContainer5.Panel2.SuspendLayout();
            SplitContainer5.SuspendLayout();
            GroupBox9.SuspendLayout();
            Small_PL_TabControl_Repl_ErrorOutput.SuspendLayout();
            TabPageReplErrors.SuspendLayout();
            TabPagePl_Help.SuspendLayout();
            GroupBox10.SuspendLayout();
            TabPageSAL_REPL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
            SplitContainer1.Panel1.SuspendLayout();
            SplitContainer1.Panel2.SuspendLayout();
            SplitContainer1.SuspendLayout();
            GroupBox1.SuspendLayout();
            GroupBox6.SuspendLayout();
            GroupBox5.SuspendLayout();
            GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer2).BeginInit();
            SplitContainer2.Panel1.SuspendLayout();
            SplitContainer2.Panel2.SuspendLayout();
            SplitContainer2.SuspendLayout();
            GroupBox3.SuspendLayout();
            TabControl2.SuspendLayout();
            TabPageSalReplHelp.SuspendLayout();
            TabPageSalReplErrors.SuspendLayout();
            GroupBox4.SuspendLayout();
            TabControl_REPL_INPUT.SuspendLayout();
            TabPage2.SuspendLayout();
            SAL_ToolStripRepl.SuspendLayout();
            TabPageLOGO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer6).BeginInit();
            SplitContainer6.Panel1.SuspendLayout();
            SplitContainer6.Panel2.SuspendLayout();
            SplitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer7).BeginInit();
            SplitContainer7.Panel1.SuspendLayout();
            SplitContainer7.Panel2.SuspendLayout();
            SplitContainer7.SuspendLayout();
            GroupBox12.SuspendLayout();
            TabControl4.SuspendLayout();
            TabPage5.SuspendLayout();
            LOGO_ToolStrip.SuspendLayout();
            GroupBox11.SuspendLayout();
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer8).BeginInit();
            SplitContainer8.Panel1.SuspendLayout();
            SplitContainer8.Panel2.SuspendLayout();
            SplitContainer8.SuspendLayout();
            GroupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer9).BeginInit();
            SplitContainer9.Panel1.SuspendLayout();
            SplitContainer9.Panel2.SuspendLayout();
            SplitContainer9.SuspendLayout();
            GroupBox13.SuspendLayout();
            GroupBox14.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl_ProgrammingLang_Repl
            // 
            TabControl_ProgrammingLang_Repl.Controls.Add(TabPageSpydaz_PL);
            TabControl_ProgrammingLang_Repl.Dock = DockStyle.Fill;
            TabControl_ProgrammingLang_Repl.Location = new Point(0, 0);
            TabControl_ProgrammingLang_Repl.Margin = new Padding(5, 6, 5, 6);
            TabControl_ProgrammingLang_Repl.Name = "TabControl_ProgrammingLang_Repl";
            TabControl_ProgrammingLang_Repl.SelectedIndex = 0;
            TabControl_ProgrammingLang_Repl.Size = new Size(1577, 746);
            TabControl_ProgrammingLang_Repl.TabIndex = 0;
            ToolTip1.SetToolTip(TabControl_ProgrammingLang_Repl, "Welcome to the SpydazWeb Compiler/AstVisualizer");
            // 
            // TabPageSpydaz_PL
            // 
            TabPageSpydaz_PL.Controls.Add(TabControlRepl_Inner);
            TabPageSpydaz_PL.Location = new Point(4, 31);
            TabPageSpydaz_PL.Margin = new Padding(5, 6, 5, 6);
            TabPageSpydaz_PL.Name = "TabPageSpydaz_PL";
            TabPageSpydaz_PL.Padding = new Padding(5, 6, 5, 6);
            TabPageSpydaz_PL.Size = new Size(1569, 711);
            TabPageSpydaz_PL.TabIndex = 0;
            TabPageSpydaz_PL.Text = "Spydaz_PL";
            TabPageSpydaz_PL.UseVisualStyleBackColor = true;
            // 
            // TabControlRepl_Inner
            // 
            TabControlRepl_Inner.Controls.Add(TabPageSmall_PL_repl);
            TabControlRepl_Inner.Controls.Add(TabPageSAL_REPL);
            TabControlRepl_Inner.Controls.Add(TabPageLOGO);
            TabControlRepl_Inner.Dock = DockStyle.Fill;
            TabControlRepl_Inner.Location = new Point(5, 6);
            TabControlRepl_Inner.Margin = new Padding(5, 6, 5, 6);
            TabControlRepl_Inner.Name = "TabControlRepl_Inner";
            TabControlRepl_Inner.SelectedIndex = 0;
            TabControlRepl_Inner.Size = new Size(1559, 699);
            TabControlRepl_Inner.TabIndex = 1;
            // 
            // TabPageSmall_PL_repl
            // 
            TabPageSmall_PL_repl.Controls.Add(SplitContainer3);
            TabPageSmall_PL_repl.Location = new Point(4, 31);
            TabPageSmall_PL_repl.Margin = new Padding(5, 6, 5, 6);
            TabPageSmall_PL_repl.Name = "TabPageSmall_PL_repl";
            TabPageSmall_PL_repl.Padding = new Padding(5, 6, 5, 6);
            TabPageSmall_PL_repl.Size = new Size(1551, 664);
            TabPageSmall_PL_repl.TabIndex = 2;
            TabPageSmall_PL_repl.Text = "Small_PL";
            TabPageSmall_PL_repl.UseVisualStyleBackColor = true;
            // 
            // SplitContainer3
            // 
            SplitContainer3.Dock = DockStyle.Fill;
            SplitContainer3.Location = new Point(5, 6);
            SplitContainer3.Margin = new Padding(8);
            SplitContainer3.Name = "SplitContainer3";
            SplitContainer3.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer3.Panel1
            // 
            SplitContainer3.Panel1.Controls.Add(SplitContainer4);
            // 
            // SplitContainer3.Panel2
            // 
            SplitContainer3.Panel2.Controls.Add(SplitContainer5);
            SplitContainer3.Size = new Size(1541, 652);
            SplitContainer3.SplitterDistance = 388;
            SplitContainer3.SplitterWidth = 10;
            SplitContainer3.TabIndex = 1;
            // 
            // SplitContainer4
            // 
            SplitContainer4.Dock = DockStyle.Fill;
            SplitContainer4.Location = new Point(0, 0);
            SplitContainer4.Margin = new Padding(8);
            SplitContainer4.Name = "SplitContainer4";
            // 
            // SplitContainer4.Panel1
            // 
            SplitContainer4.Panel1.Controls.Add(GroupBox7);
            // 
            // SplitContainer4.Panel2
            // 
            SplitContainer4.Panel2.Controls.Add(GroupBox8);
            SplitContainer4.Size = new Size(1541, 388);
            SplitContainer4.SplitterDistance = 556;
            SplitContainer4.SplitterWidth = 12;
            SplitContainer4.TabIndex = 0;
            // 
            // GroupBox7
            // 
            GroupBox7.BackColor = SystemColors.ActiveCaptionText;
            GroupBox7.Controls.Add(TabControl3);
            GroupBox7.Controls.Add(Small_PL_ToolStrip);
            GroupBox7.Dock = DockStyle.Fill;
            GroupBox7.ForeColor = Color.Cyan;
            GroupBox7.Location = new Point(0, 0);
            GroupBox7.Margin = new Padding(8);
            GroupBox7.Name = "GroupBox7";
            GroupBox7.Padding = new Padding(8);
            GroupBox7.Size = new Size(556, 388);
            GroupBox7.TabIndex = 1;
            GroupBox7.TabStop = false;
            GroupBox7.Text = "REPL";
            // 
            // TabControl3
            // 
            TabControl3.Controls.Add(TabPage3);
            TabControl3.Dock = DockStyle.Fill;
            TabControl3.Location = new Point(8, 88);
            TabControl3.Margin = new Padding(8);
            TabControl3.Name = "TabControl3";
            TabControl3.SelectedIndex = 0;
            TabControl3.Size = new Size(540, 292);
            TabControl3.TabIndex = 1;
            // 
            // TabPage3
            // 
            TabPage3.Controls.Add(Small_PL_TextBoxCodeInput);
            TabPage3.Location = new Point(4, 31);
            TabPage3.Margin = new Padding(8);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(8);
            TabPage3.Size = new Size(532, 257);
            TabPage3.TabIndex = 1;
            TabPage3.Text = "Program";
            TabPage3.UseVisualStyleBackColor = true;
            // 
            // Small_PL_TextBoxCodeInput
            // 
            Small_PL_TextBoxCodeInput.BackColor = SystemColors.InactiveBorder;
            Small_PL_TextBoxCodeInput.Dock = DockStyle.Fill;
            Small_PL_TextBoxCodeInput.Font = new Font("Consolas", 20.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            Small_PL_TextBoxCodeInput.Location = new Point(8, 8);
            Small_PL_TextBoxCodeInput.Margin = new Padding(5, 6, 5, 6);
            Small_PL_TextBoxCodeInput.Multiline = true;
            Small_PL_TextBoxCodeInput.Name = "Small_PL_TextBoxCodeInput";
            Small_PL_TextBoxCodeInput.ScrollBars = ScrollBars.Vertical;
            Small_PL_TextBoxCodeInput.Size = new Size(516, 241);
            Small_PL_TextBoxCodeInput.TabIndex = 0;
            // 
            // Small_PL_ToolStrip
            // 
            Small_PL_ToolStrip.BackColor = Color.Black;
            Small_PL_ToolStrip.ImageScalingSize = new Size(50, 50);
            Small_PL_ToolStrip.Items.AddRange(new ToolStripItem[] { ToolStripSeparator15, _Small_PL_NewToolStripButton, ToolStripSeparator10, _Small_PL_OpenToolStripButton, ToolStripSeparator11, _Small_PL_SaveToolStripButton, ToolStripSeparator12, _Small_PL_ToolStripButtonCompileCode, ToolStripSeparator14, Small_PL_ToolStripButtonRunCode, ToolStripSeparator16, ToolStripSeparator13, Small_PL_ToolStripButtonCompilesTox86, ToolStripSeparator17, _Small_PL_ButtonOpenVM, ToolStripSeparator18, _Small_PL_HelpToolStripButton, ToolStripSeparator19 });
            Small_PL_ToolStrip.Location = new Point(8, 31);
            Small_PL_ToolStrip.Name = "Small_PL_ToolStrip";
            Small_PL_ToolStrip.Padding = new Padding(0, 0, 2, 0);
            Small_PL_ToolStrip.Size = new Size(540, 57);
            Small_PL_ToolStrip.TabIndex = 0;
            Small_PL_ToolStrip.Text = "ToolStrip1";
            // 
            // ToolStripSeparator15
            // 
            ToolStripSeparator15.Name = "ToolStripSeparator15";
            ToolStripSeparator15.Size = new Size(6, 57);
            // 
            // Small_PL_NewToolStripButton
            // 
            _Small_PL_NewToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _Small_PL_NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_NewToolStripButton.Image = (Image)resources.GetObject("Small_PL_NewToolStripButton.Image");
            _Small_PL_NewToolStripButton.ImageTransparentColor = Color.Magenta;
            _Small_PL_NewToolStripButton.Name = "_Small_PL_NewToolStripButton";
            _Small_PL_NewToolStripButton.Size = new Size(54, 54);
            _Small_PL_NewToolStripButton.Tag = "New Program";
            _Small_PL_NewToolStripButton.Text = "&New";
            // 
            // ToolStripSeparator10
            // 
            ToolStripSeparator10.Name = "ToolStripSeparator10";
            ToolStripSeparator10.Size = new Size(6, 57);
            // 
            // Small_PL_OpenToolStripButton
            // 
            _Small_PL_OpenToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _Small_PL_OpenToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_OpenToolStripButton.Image = (Image)resources.GetObject("Small_PL_OpenToolStripButton.Image");
            _Small_PL_OpenToolStripButton.ImageTransparentColor = Color.Magenta;
            _Small_PL_OpenToolStripButton.Name = "_Small_PL_OpenToolStripButton";
            _Small_PL_OpenToolStripButton.Size = new Size(54, 54);
            _Small_PL_OpenToolStripButton.Tag = "Open Program";
            _Small_PL_OpenToolStripButton.Text = "&Open";
            // 
            // ToolStripSeparator11
            // 
            ToolStripSeparator11.Name = "ToolStripSeparator11";
            ToolStripSeparator11.Size = new Size(6, 57);
            // 
            // Small_PL_SaveToolStripButton
            // 
            _Small_PL_SaveToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _Small_PL_SaveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_SaveToolStripButton.Image = (Image)resources.GetObject("Small_PL_SaveToolStripButton.Image");
            _Small_PL_SaveToolStripButton.ImageTransparentColor = Color.Magenta;
            _Small_PL_SaveToolStripButton.Name = "_Small_PL_SaveToolStripButton";
            _Small_PL_SaveToolStripButton.Size = new Size(54, 54);
            _Small_PL_SaveToolStripButton.Tag = "Save Program";
            _Small_PL_SaveToolStripButton.Text = "&Save ";
            // 
            // ToolStripSeparator12
            // 
            ToolStripSeparator12.Name = "ToolStripSeparator12";
            ToolStripSeparator12.Size = new Size(6, 57);
            // 
            // Small_PL_ToolStripButtonCompileCode
            // 
            _Small_PL_ToolStripButtonCompileCode.BackColor = Color.Black;
            _Small_PL_ToolStripButtonCompileCode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_ToolStripButtonCompileCode.Image = My.Resources.Resources.Complier_RUN;
            _Small_PL_ToolStripButtonCompileCode.ImageTransparentColor = Color.Magenta;
            _Small_PL_ToolStripButtonCompileCode.Name = "_Small_PL_ToolStripButtonCompileCode";
            _Small_PL_ToolStripButtonCompileCode.Size = new Size(54, 54);
            _Small_PL_ToolStripButtonCompileCode.Tag = "Compiles to AST - If it compiles to AST then it will Evaluate or Generate SAL";
            _Small_PL_ToolStripButtonCompileCode.Text = "Compiles Code to AST";
            _Small_PL_ToolStripButtonCompileCode.ToolTipText = "Compile Code to AST";
            // 
            // ToolStripSeparator14
            // 
            ToolStripSeparator14.Name = "ToolStripSeparator14";
            ToolStripSeparator14.Size = new Size(6, 57);
            // 
            // Small_PL_ToolStripButtonRunCode
            // 
            Small_PL_ToolStripButtonRunCode.BackColor = Color.Black;
            Small_PL_ToolStripButtonRunCode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Small_PL_ToolStripButtonRunCode.Image = My.Resources.Resources.Arrow_Right;
            Small_PL_ToolStripButtonRunCode.ImageTransparentColor = Color.Magenta;
            Small_PL_ToolStripButtonRunCode.Name = "Small_PL_ToolStripButtonRunCode";
            Small_PL_ToolStripButtonRunCode.Size = new Size(54, 54);
            Small_PL_ToolStripButtonRunCode.Tag = "Evaluate Code (Uses S-Expression)";
            Small_PL_ToolStripButtonRunCode.Text = "Run";
            Small_PL_ToolStripButtonRunCode.ToolTipText = "Runs Code on VM";
            // 
            // ToolStripSeparator16
            // 
            ToolStripSeparator16.Name = "ToolStripSeparator16";
            ToolStripSeparator16.Size = new Size(6, 57);
            // 
            // ToolStripSeparator13
            // 
            ToolStripSeparator13.Name = "ToolStripSeparator13";
            ToolStripSeparator13.Size = new Size(6, 57);
            // 
            // Small_PL_ToolStripButtonCompilesTox86
            // 
            Small_PL_ToolStripButtonCompilesTox86.BackColor = Color.Silver;
            Small_PL_ToolStripButtonCompilesTox86.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Small_PL_ToolStripButtonCompilesTox86.Image = My.Resources.Resources.Script;
            Small_PL_ToolStripButtonCompilesTox86.ImageTransparentColor = Color.Magenta;
            Small_PL_ToolStripButtonCompilesTox86.Name = "Small_PL_ToolStripButtonCompilesTox86";
            Small_PL_ToolStripButtonCompilesTox86.Size = new Size(54, 54);
            Small_PL_ToolStripButtonCompilesTox86.Text = "Transpile to X86 Code";
            // 
            // ToolStripSeparator17
            // 
            ToolStripSeparator17.Name = "ToolStripSeparator17";
            ToolStripSeparator17.Size = new Size(6, 57);
            // 
            // Small_PL_ButtonOpenVM
            // 
            _Small_PL_ButtonOpenVM.BackColor = Color.Black;
            _Small_PL_ButtonOpenVM.BackgroundImage = My.Resources.Resources.EYE_BLUE_;
            _Small_PL_ButtonOpenVM.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_ButtonOpenVM.Image = My.Resources.Resources.EYE_BLUE_;
            _Small_PL_ButtonOpenVM.ImageTransparentColor = Color.Magenta;
            _Small_PL_ButtonOpenVM.Name = "_Small_PL_ButtonOpenVM";
            _Small_PL_ButtonOpenVM.Size = new Size(54, 54);
            _Small_PL_ButtonOpenVM.Text = "Load SAL";
            // 
            // ToolStripSeparator18
            // 
            ToolStripSeparator18.Name = "ToolStripSeparator18";
            ToolStripSeparator18.Size = new Size(6, 57);
            // 
            // Small_PL_HelpToolStripButton
            // 
            _Small_PL_HelpToolStripButton.BackColor = Color.Black;
            _Small_PL_HelpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _Small_PL_HelpToolStripButton.Image = (Image)resources.GetObject("Small_PL_HelpToolStripButton.Image");
            _Small_PL_HelpToolStripButton.ImageTransparentColor = Color.Magenta;
            _Small_PL_HelpToolStripButton.Name = "_Small_PL_HelpToolStripButton";
            _Small_PL_HelpToolStripButton.Size = new Size(54, 54);
            _Small_PL_HelpToolStripButton.Text = "He&lp Refference";
            // 
            // ToolStripSeparator19
            // 
            ToolStripSeparator19.Name = "ToolStripSeparator19";
            ToolStripSeparator19.Size = new Size(6, 57);
            // 
            // GroupBox8
            // 
            GroupBox8.BackColor = Color.DarkGray;
            GroupBox8.BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            GroupBox8.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox8.Controls.Add(Small_PL_TextBoxREPL_OUTPUT);
            GroupBox8.Dock = DockStyle.Fill;
            GroupBox8.ForeColor = Color.Cyan;
            GroupBox8.Location = new Point(0, 0);
            GroupBox8.Margin = new Padding(8);
            GroupBox8.Name = "GroupBox8";
            GroupBox8.Padding = new Padding(8);
            GroupBox8.Size = new Size(973, 388);
            GroupBox8.TabIndex = 0;
            GroupBox8.TabStop = false;
            // 
            // Small_PL_TextBoxREPL_OUTPUT
            // 
            Small_PL_TextBoxREPL_OUTPUT.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Small_PL_TextBoxREPL_OUTPUT.BackColor = Color.Black;
            Small_PL_TextBoxREPL_OUTPUT.Font = new Font("Consolas", 18.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            Small_PL_TextBoxREPL_OUTPUT.ForeColor = Color.Lime;
            Small_PL_TextBoxREPL_OUTPUT.Location = new Point(0, 0);
            Small_PL_TextBoxREPL_OUTPUT.Margin = new Padding(8);
            Small_PL_TextBoxREPL_OUTPUT.Multiline = true;
            Small_PL_TextBoxREPL_OUTPUT.Name = "Small_PL_TextBoxREPL_OUTPUT";
            Small_PL_TextBoxREPL_OUTPUT.ScrollBars = ScrollBars.Vertical;
            Small_PL_TextBoxREPL_OUTPUT.Size = new Size(979, 287);
            Small_PL_TextBoxREPL_OUTPUT.TabIndex = 1;
            Small_PL_TextBoxREPL_OUTPUT.Text = ">";
            // 
            // SplitContainer5
            // 
            SplitContainer5.Dock = DockStyle.Fill;
            SplitContainer5.Location = new Point(0, 0);
            SplitContainer5.Margin = new Padding(5, 6, 5, 6);
            SplitContainer5.Name = "SplitContainer5";
            // 
            // SplitContainer5.Panel1
            // 
            SplitContainer5.Panel1.Controls.Add(GroupBox9);
            // 
            // SplitContainer5.Panel2
            // 
            SplitContainer5.Panel2.Controls.Add(GroupBox10);
            SplitContainer5.Size = new Size(1541, 254);
            SplitContainer5.SplitterDistance = 553;
            SplitContainer5.SplitterWidth = 7;
            SplitContainer5.TabIndex = 0;
            // 
            // GroupBox9
            // 
            GroupBox9.BackColor = Color.Black;
            GroupBox9.Controls.Add(Small_PL_TabControl_Repl_ErrorOutput);
            GroupBox9.Dock = DockStyle.Fill;
            GroupBox9.ForeColor = Color.Lime;
            GroupBox9.Location = new Point(0, 0);
            GroupBox9.Margin = new Padding(8);
            GroupBox9.Name = "GroupBox9";
            GroupBox9.Padding = new Padding(8);
            GroupBox9.Size = new Size(553, 254);
            GroupBox9.TabIndex = 1;
            GroupBox9.TabStop = false;
            GroupBox9.Text = "Errors";
            // 
            // Small_PL_TabControl_Repl_ErrorOutput
            // 
            Small_PL_TabControl_Repl_ErrorOutput.Controls.Add(TabPageReplErrors);
            Small_PL_TabControl_Repl_ErrorOutput.Controls.Add(TabPagePl_Help);
            Small_PL_TabControl_Repl_ErrorOutput.Dock = DockStyle.Fill;
            Small_PL_TabControl_Repl_ErrorOutput.Location = new Point(8, 31);
            Small_PL_TabControl_Repl_ErrorOutput.Margin = new Padding(8);
            Small_PL_TabControl_Repl_ErrorOutput.Name = "Small_PL_TabControl_Repl_ErrorOutput";
            Small_PL_TabControl_Repl_ErrorOutput.SelectedIndex = 0;
            Small_PL_TabControl_Repl_ErrorOutput.Size = new Size(537, 215);
            Small_PL_TabControl_Repl_ErrorOutput.TabIndex = 0;
            // 
            // TabPageReplErrors
            // 
            TabPageReplErrors.Controls.Add(Small_PL_TextboxErrors);
            TabPageReplErrors.Location = new Point(4, 31);
            TabPageReplErrors.Margin = new Padding(5, 6, 5, 6);
            TabPageReplErrors.Name = "TabPageReplErrors";
            TabPageReplErrors.Padding = new Padding(5, 6, 5, 6);
            TabPageReplErrors.Size = new Size(529, 180);
            TabPageReplErrors.TabIndex = 0;
            TabPageReplErrors.Text = "Errors";
            TabPageReplErrors.UseVisualStyleBackColor = true;
            // 
            // Small_PL_TextboxErrors
            // 
            Small_PL_TextboxErrors.Dock = DockStyle.Fill;
            Small_PL_TextboxErrors.Font = new Font("Microsoft Tai Le", 14.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            Small_PL_TextboxErrors.Location = new Point(5, 6);
            Small_PL_TextboxErrors.Margin = new Padding(5, 6, 5, 6);
            Small_PL_TextboxErrors.Name = "Small_PL_TextboxErrors";
            Small_PL_TextboxErrors.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            Small_PL_TextboxErrors.Size = new Size(519, 168);
            Small_PL_TextboxErrors.TabIndex = 0;
            Small_PL_TextboxErrors.Text = "";
            // 
            // TabPagePl_Help
            // 
            TabPagePl_Help.Controls.Add(RichTextBoxPL_Help);
            TabPagePl_Help.Location = new Point(4, 22);
            TabPagePl_Help.Name = "TabPagePl_Help";
            TabPagePl_Help.Padding = new Padding(3);
            TabPagePl_Help.Size = new Size(529, 203);
            TabPagePl_Help.TabIndex = 1;
            TabPagePl_Help.Text = "Syntax";
            TabPagePl_Help.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxPL_Help
            // 
            RichTextBoxPL_Help.Dock = DockStyle.Fill;
            RichTextBoxPL_Help.Location = new Point(3, 3);
            RichTextBoxPL_Help.Name = "RichTextBoxPL_Help";
            RichTextBoxPL_Help.Size = new Size(523, 197);
            RichTextBoxPL_Help.TabIndex = 0;
            RichTextBoxPL_Help.Text = resources.GetString("RichTextBoxPL_Help.Text");
            // 
            // GroupBox10
            // 
            GroupBox10.BackgroundImage = My.Resources.Resources.Console_A;
            GroupBox10.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox10.Controls.Add(_Small_PL_AstTreeView);
            GroupBox10.Dock = DockStyle.Fill;
            GroupBox10.Location = new Point(0, 0);
            GroupBox10.Margin = new Padding(5, 6, 5, 6);
            GroupBox10.Name = "GroupBox10";
            GroupBox10.Padding = new Padding(5, 6, 5, 6);
            GroupBox10.Size = new Size(981, 254);
            GroupBox10.TabIndex = 0;
            GroupBox10.TabStop = false;
            // 
            // Small_PL_AstTreeView
            // 
            _Small_PL_AstTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _Small_PL_AstTreeView.BackColor = SystemColors.InfoText;
            _Small_PL_AstTreeView.BorderStyle = BorderStyle.FixedSingle;
            _Small_PL_AstTreeView.Font = new Font("Courier New", 15.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            _Small_PL_AstTreeView.ForeColor = Color.MintCream;
            _Small_PL_AstTreeView.HotTracking = true;
            _Small_PL_AstTreeView.Location = new Point(162, 26);
            _Small_PL_AstTreeView.Margin = new Padding(5, 6, 5, 6);
            _Small_PL_AstTreeView.Name = "_Small_PL_AstTreeView";
            _Small_PL_AstTreeView.ShowNodeToolTips = true;
            _Small_PL_AstTreeView.Size = new Size(655, 216);
            _Small_PL_AstTreeView.TabIndex = 0;
            // 
            // TabPageSAL_REPL
            // 
            TabPageSAL_REPL.Controls.Add(SplitContainer1);
            TabPageSAL_REPL.Location = new Point(4, 31);
            TabPageSAL_REPL.Margin = new Padding(5, 6, 5, 6);
            TabPageSAL_REPL.Name = "TabPageSAL_REPL";
            TabPageSAL_REPL.Padding = new Padding(5, 6, 5, 6);
            TabPageSAL_REPL.Size = new Size(1551, 664);
            TabPageSAL_REPL.TabIndex = 1;
            TabPageSAL_REPL.Text = "SAL";
            TabPageSAL_REPL.UseVisualStyleBackColor = true;
            // 
            // SplitContainer1
            // 
            SplitContainer1.Dock = DockStyle.Fill;
            SplitContainer1.Location = new Point(5, 6);
            SplitContainer1.Margin = new Padding(5, 6, 5, 6);
            SplitContainer1.Name = "SplitContainer1";
            SplitContainer1.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            SplitContainer1.Panel1.Controls.Add(GroupBox1);
            // 
            // SplitContainer1.Panel2
            // 
            SplitContainer1.Panel2.Controls.Add(SplitContainer2);
            SplitContainer1.Size = new Size(1541, 652);
            SplitContainer1.SplitterDistance = 321;
            SplitContainer1.SplitterWidth = 6;
            SplitContainer1.TabIndex = 1;
            // 
            // GroupBox1
            // 
            GroupBox1.BackColor = Color.Black;
            GroupBox1.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox1.Controls.Add(GroupBox6);
            GroupBox1.Controls.Add(GroupBox5);
            GroupBox1.Controls.Add(GroupBox2);
            GroupBox1.Dock = DockStyle.Fill;
            GroupBox1.Font = new Font("Comic Sans MS", 7.8f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GroupBox1.ForeColor = Color.White;
            GroupBox1.Location = new Point(0, 0);
            GroupBox1.Margin = new Padding(3, 4, 3, 4);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(3, 4, 3, 4);
            GroupBox1.Size = new Size(1541, 321);
            GroupBox1.TabIndex = 1;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "SpydazWeb AI Programming Language Editor";
            // 
            // GroupBox6
            // 
            GroupBox6.BackColor = Color.Black;
            GroupBox6.BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            GroupBox6.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox6.Controls.Add(SAL_RichTextBoxDisplayOutput);
            GroupBox6.Dock = DockStyle.Fill;
            GroupBox6.ForeColor = Color.Lime;
            GroupBox6.Location = new Point(502, 19);
            GroupBox6.Margin = new Padding(3, 4, 3, 4);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Padding = new Padding(3, 4, 3, 4);
            GroupBox6.Size = new Size(704, 298);
            GroupBox6.TabIndex = 2;
            GroupBox6.TabStop = false;
            GroupBox6.Text = "Program Output";
            // 
            // SAL_RichTextBoxDisplayOutput
            // 
            SAL_RichTextBoxDisplayOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            SAL_RichTextBoxDisplayOutput.BackColor = Color.Gainsboro;
            SAL_RichTextBoxDisplayOutput.Font = new Font("Consolas", 18.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_RichTextBoxDisplayOutput.Location = new Point(8, 30);
            SAL_RichTextBoxDisplayOutput.Margin = new Padding(3, 4, 3, 4);
            SAL_RichTextBoxDisplayOutput.Name = "SAL_RichTextBoxDisplayOutput";
            SAL_RichTextBoxDisplayOutput.Size = new Size(684, 189);
            SAL_RichTextBoxDisplayOutput.TabIndex = 0;
            SAL_RichTextBoxDisplayOutput.Text = "";
            // 
            // GroupBox5
            // 
            GroupBox5.BackColor = Color.Black;
            GroupBox5.Controls.Add(SAL_AST);
            GroupBox5.Dock = DockStyle.Right;
            GroupBox5.Font = new Font("Comic Sans MS", 7.8f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GroupBox5.ForeColor = Color.Aqua;
            GroupBox5.Location = new Point(1206, 19);
            GroupBox5.Margin = new Padding(3, 4, 3, 4);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Padding = new Padding(3, 4, 3, 4);
            GroupBox5.Size = new Size(332, 298);
            GroupBox5.TabIndex = 1;
            GroupBox5.TabStop = false;
            GroupBox5.Text = "Abstract Syntax Tree";
            // 
            // SAL_AST
            // 
            SAL_AST.BackColor = SystemColors.Info;
            SAL_AST.Dock = DockStyle.Fill;
            SAL_AST.Font = new Font("Comic Sans MS", 15.75f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_AST.Location = new Point(3, 19);
            SAL_AST.Margin = new Padding(3, 4, 3, 4);
            SAL_AST.Name = "SAL_AST";
            SAL_AST.Size = new Size(326, 275);
            SAL_AST.TabIndex = 0;
            // 
            // GroupBox2
            // 
            GroupBox2.BackColor = Color.Black;
            GroupBox2.Controls.Add(SAL_RichTextBoxProgram);
            GroupBox2.Dock = DockStyle.Left;
            GroupBox2.Font = new Font("Comic Sans MS", 7.8f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GroupBox2.ForeColor = Color.Yellow;
            GroupBox2.Location = new Point(3, 19);
            GroupBox2.Margin = new Padding(3, 4, 3, 4);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(3, 4, 3, 4);
            GroupBox2.Size = new Size(499, 298);
            GroupBox2.TabIndex = 0;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "AST";
            // 
            // SAL_RichTextBoxProgram
            // 
            SAL_RichTextBoxProgram.Dock = DockStyle.Fill;
            SAL_RichTextBoxProgram.Font = new Font("Consolas", 18.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_RichTextBoxProgram.Location = new Point(3, 19);
            SAL_RichTextBoxProgram.Margin = new Padding(3, 4, 3, 4);
            SAL_RichTextBoxProgram.Multiline = true;
            SAL_RichTextBoxProgram.Name = "SAL_RichTextBoxProgram";
            SAL_RichTextBoxProgram.Size = new Size(493, 275);
            SAL_RichTextBoxProgram.TabIndex = 0;
            // 
            // SplitContainer2
            // 
            SplitContainer2.Dock = DockStyle.Fill;
            SplitContainer2.Location = new Point(0, 0);
            SplitContainer2.Margin = new Padding(5, 6, 5, 6);
            SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            SplitContainer2.Panel1.Controls.Add(GroupBox3);
            // 
            // SplitContainer2.Panel2
            // 
            SplitContainer2.Panel2.Controls.Add(GroupBox4);
            SplitContainer2.Size = new Size(1541, 325);
            SplitContainer2.SplitterDistance = 642;
            SplitContainer2.SplitterWidth = 7;
            SplitContainer2.TabIndex = 0;
            // 
            // GroupBox3
            // 
            GroupBox3.BackColor = Color.Black;
            GroupBox3.Controls.Add(TabControl2);
            GroupBox3.Dock = DockStyle.Fill;
            GroupBox3.Font = new Font("Comic Sans MS", 7.8f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            GroupBox3.ForeColor = Color.Lime;
            GroupBox3.Location = new Point(0, 0);
            GroupBox3.Margin = new Padding(3, 4, 3, 4);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Padding = new Padding(3, 4, 3, 4);
            GroupBox3.Size = new Size(642, 325);
            GroupBox3.TabIndex = 2;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "Error Output";
            // 
            // TabControl2
            // 
            TabControl2.Controls.Add(TabPageSalReplHelp);
            TabControl2.Controls.Add(TabPageSalReplErrors);
            TabControl2.Dock = DockStyle.Fill;
            TabControl2.Location = new Point(3, 19);
            TabControl2.Margin = new Padding(5, 6, 5, 6);
            TabControl2.Name = "TabControl2";
            TabControl2.SelectedIndex = 0;
            TabControl2.Size = new Size(636, 302);
            TabControl2.TabIndex = 0;
            // 
            // TabPageSalReplHelp
            // 
            TabPageSalReplHelp.Controls.Add(SAL_RichTextBoxHelp);
            TabPageSalReplHelp.Location = new Point(4, 23);
            TabPageSalReplHelp.Margin = new Padding(5, 6, 5, 6);
            TabPageSalReplHelp.Name = "TabPageSalReplHelp";
            TabPageSalReplHelp.Padding = new Padding(5, 6, 5, 6);
            TabPageSalReplHelp.Size = new Size(628, 275);
            TabPageSalReplHelp.TabIndex = 1;
            TabPageSalReplHelp.Text = "Syntax";
            TabPageSalReplHelp.UseVisualStyleBackColor = true;
            // 
            // SAL_RichTextBoxHelp
            // 
            SAL_RichTextBoxHelp.Dock = DockStyle.Fill;
            SAL_RichTextBoxHelp.Font = new Font("Comic Sans MS", 9.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_RichTextBoxHelp.Location = new Point(5, 6);
            SAL_RichTextBoxHelp.Margin = new Padding(5, 6, 5, 6);
            SAL_RichTextBoxHelp.Name = "SAL_RichTextBoxHelp";
            SAL_RichTextBoxHelp.Size = new Size(618, 263);
            SAL_RichTextBoxHelp.TabIndex = 0;
            SAL_RichTextBoxHelp.Text = resources.GetString("SAL_RichTextBoxHelp.Text");
            // 
            // TabPageSalReplErrors
            // 
            TabPageSalReplErrors.Controls.Add(SAL_TextBoxErrorOutput);
            TabPageSalReplErrors.Location = new Point(4, 23);
            TabPageSalReplErrors.Margin = new Padding(5, 6, 5, 6);
            TabPageSalReplErrors.Name = "TabPageSalReplErrors";
            TabPageSalReplErrors.Padding = new Padding(5, 6, 5, 6);
            TabPageSalReplErrors.Size = new Size(628, 280);
            TabPageSalReplErrors.TabIndex = 0;
            TabPageSalReplErrors.Text = "Errors";
            TabPageSalReplErrors.UseVisualStyleBackColor = true;
            // 
            // SAL_TextBoxErrorOutput
            // 
            SAL_TextBoxErrorOutput.Dock = DockStyle.Fill;
            SAL_TextBoxErrorOutput.Font = new Font("Consolas", 10.2f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_TextBoxErrorOutput.Location = new Point(5, 6);
            SAL_TextBoxErrorOutput.Margin = new Padding(3, 4, 3, 4);
            SAL_TextBoxErrorOutput.Multiline = true;
            SAL_TextBoxErrorOutput.Name = "SAL_TextBoxErrorOutput";
            SAL_TextBoxErrorOutput.ScrollBars = ScrollBars.Vertical;
            SAL_TextBoxErrorOutput.Size = new Size(618, 268);
            SAL_TextBoxErrorOutput.TabIndex = 1;
            // 
            // GroupBox4
            // 
            GroupBox4.BackColor = SystemColors.ActiveCaptionText;
            GroupBox4.Controls.Add(TabControl_REPL_INPUT);
            GroupBox4.Controls.Add(SAL_ToolStripRepl);
            GroupBox4.Dock = DockStyle.Fill;
            GroupBox4.ForeColor = Color.Cyan;
            GroupBox4.Location = new Point(0, 0);
            GroupBox4.Margin = new Padding(8);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(8);
            GroupBox4.Size = new Size(892, 325);
            GroupBox4.TabIndex = 2;
            GroupBox4.TabStop = false;
            GroupBox4.Text = "REPL";
            // 
            // TabControl_REPL_INPUT
            // 
            TabControl_REPL_INPUT.Controls.Add(TabPage2);
            TabControl_REPL_INPUT.Dock = DockStyle.Fill;
            TabControl_REPL_INPUT.Location = new Point(8, 63);
            TabControl_REPL_INPUT.Margin = new Padding(8);
            TabControl_REPL_INPUT.Name = "TabControl_REPL_INPUT";
            TabControl_REPL_INPUT.SelectedIndex = 0;
            TabControl_REPL_INPUT.Size = new Size(876, 254);
            TabControl_REPL_INPUT.TabIndex = 1;
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(SAL_TextBoxCodeInput);
            TabPage2.Location = new Point(4, 31);
            TabPage2.Margin = new Padding(8);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(8);
            TabPage2.Size = new Size(868, 219);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Program";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // SAL_TextBoxCodeInput
            // 
            SAL_TextBoxCodeInput.BackColor = SystemColors.InactiveBorder;
            SAL_TextBoxCodeInput.Dock = DockStyle.Fill;
            SAL_TextBoxCodeInput.Font = new Font("Consolas", 20.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            SAL_TextBoxCodeInput.Location = new Point(8, 8);
            SAL_TextBoxCodeInput.Margin = new Padding(5, 6, 5, 6);
            SAL_TextBoxCodeInput.Multiline = true;
            SAL_TextBoxCodeInput.Name = "SAL_TextBoxCodeInput";
            SAL_TextBoxCodeInput.ScrollBars = ScrollBars.Vertical;
            SAL_TextBoxCodeInput.Size = new Size(852, 203);
            SAL_TextBoxCodeInput.TabIndex = 0;
            // 
            // SAL_ToolStripRepl
            // 
            SAL_ToolStripRepl.BackColor = Color.Black;
            SAL_ToolStripRepl.ImageScalingSize = new Size(25, 25);
            SAL_ToolStripRepl.Items.AddRange(new ToolStripItem[] { _SAL_NewToolStripButton, ToolStripSeparator7, _SAL_OpenToolStripButton, ToolStripSeparator6, _SAL_SaveToolStripButton, toolStripSeparator, ToolStripSeparator8, toolStripSeparator1, _SAL_ToolStripButtonCompileCode, ToolStripSeparator9, SAL_ToolStripButtonRunCode, ToolStripSeparator3, ToolStripSeparator2, _SAL_ButtonOpenVM, ToolStripSeparator4, ToolStripSeparator5 });
            SAL_ToolStripRepl.Location = new Point(8, 31);
            SAL_ToolStripRepl.Name = "SAL_ToolStripRepl";
            SAL_ToolStripRepl.Padding = new Padding(0, 0, 2, 0);
            SAL_ToolStripRepl.Size = new Size(876, 32);
            SAL_ToolStripRepl.TabIndex = 0;
            SAL_ToolStripRepl.Text = "ToolStrip1";
            // 
            // SAL_NewToolStripButton
            // 
            _SAL_NewToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _SAL_NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _SAL_NewToolStripButton.Image = (Image)resources.GetObject("SAL_NewToolStripButton.Image");
            _SAL_NewToolStripButton.ImageTransparentColor = Color.Magenta;
            _SAL_NewToolStripButton.Name = "_SAL_NewToolStripButton";
            _SAL_NewToolStripButton.Size = new Size(29, 29);
            _SAL_NewToolStripButton.Tag = "New Program";
            _SAL_NewToolStripButton.Text = "&New";
            // 
            // ToolStripSeparator7
            // 
            ToolStripSeparator7.Name = "ToolStripSeparator7";
            ToolStripSeparator7.Size = new Size(6, 32);
            // 
            // SAL_OpenToolStripButton
            // 
            _SAL_OpenToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _SAL_OpenToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _SAL_OpenToolStripButton.Image = (Image)resources.GetObject("SAL_OpenToolStripButton.Image");
            _SAL_OpenToolStripButton.ImageTransparentColor = Color.Magenta;
            _SAL_OpenToolStripButton.Name = "_SAL_OpenToolStripButton";
            _SAL_OpenToolStripButton.Size = new Size(29, 29);
            _SAL_OpenToolStripButton.Tag = "Open Program";
            _SAL_OpenToolStripButton.Text = "&Open";
            // 
            // ToolStripSeparator6
            // 
            ToolStripSeparator6.Name = "ToolStripSeparator6";
            ToolStripSeparator6.Size = new Size(6, 32);
            // 
            // SAL_SaveToolStripButton
            // 
            _SAL_SaveToolStripButton.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            _SAL_SaveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _SAL_SaveToolStripButton.Image = (Image)resources.GetObject("SAL_SaveToolStripButton.Image");
            _SAL_SaveToolStripButton.ImageTransparentColor = Color.Magenta;
            _SAL_SaveToolStripButton.Name = "_SAL_SaveToolStripButton";
            _SAL_SaveToolStripButton.Size = new Size(29, 29);
            _SAL_SaveToolStripButton.Tag = "Save Program";
            _SAL_SaveToolStripButton.Text = "&Save ";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 32);
            // 
            // ToolStripSeparator8
            // 
            ToolStripSeparator8.Name = "ToolStripSeparator8";
            ToolStripSeparator8.Size = new Size(6, 32);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 32);
            // 
            // SAL_ToolStripButtonCompileCode
            // 
            _SAL_ToolStripButtonCompileCode.BackColor = Color.Black;
            _SAL_ToolStripButtonCompileCode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _SAL_ToolStripButtonCompileCode.Image = My.Resources.Resources.Complier_RUN;
            _SAL_ToolStripButtonCompileCode.ImageTransparentColor = Color.Magenta;
            _SAL_ToolStripButtonCompileCode.Name = "_SAL_ToolStripButtonCompileCode";
            _SAL_ToolStripButtonCompileCode.Size = new Size(29, 29);
            _SAL_ToolStripButtonCompileCode.Tag = "Compiles to AST - If it compiles to AST then it will Evaluate or Generate SAL";
            _SAL_ToolStripButtonCompileCode.Text = "Compiles Code to AST";
            _SAL_ToolStripButtonCompileCode.ToolTipText = "Compile Code to AST";
            // 
            // ToolStripSeparator9
            // 
            ToolStripSeparator9.Name = "ToolStripSeparator9";
            ToolStripSeparator9.Size = new Size(6, 32);
            // 
            // SAL_ToolStripButtonRunCode
            // 
            SAL_ToolStripButtonRunCode.BackColor = Color.Black;
            SAL_ToolStripButtonRunCode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SAL_ToolStripButtonRunCode.Image = My.Resources.Resources.Arrow_Right;
            SAL_ToolStripButtonRunCode.ImageTransparentColor = Color.Magenta;
            SAL_ToolStripButtonRunCode.Name = "SAL_ToolStripButtonRunCode";
            SAL_ToolStripButtonRunCode.Size = new Size(29, 29);
            SAL_ToolStripButtonRunCode.Tag = "Evaluate Code (Uses S-Expression)";
            SAL_ToolStripButtonRunCode.Text = "Run";
            SAL_ToolStripButtonRunCode.ToolTipText = "Runs Code on VM";
            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(6, 32);
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(6, 32);
            // 
            // SAL_ButtonOpenVM
            // 
            _SAL_ButtonOpenVM.BackColor = Color.Black;
            _SAL_ButtonOpenVM.BackgroundImage = My.Resources.Resources.EYE_BLUE_;
            _SAL_ButtonOpenVM.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _SAL_ButtonOpenVM.Image = My.Resources.Resources.EYE_BLUE_;
            _SAL_ButtonOpenVM.ImageTransparentColor = Color.Magenta;
            _SAL_ButtonOpenVM.Name = "_SAL_ButtonOpenVM";
            _SAL_ButtonOpenVM.Size = new Size(29, 29);
            _SAL_ButtonOpenVM.Text = "Load SAL";
            // 
            // ToolStripSeparator4
            // 
            ToolStripSeparator4.Name = "ToolStripSeparator4";
            ToolStripSeparator4.Size = new Size(6, 32);
            // 
            // ToolStripSeparator5
            // 
            ToolStripSeparator5.Name = "ToolStripSeparator5";
            ToolStripSeparator5.Size = new Size(6, 32);
            // 
            // TabPageLOGO
            // 
            TabPageLOGO.Controls.Add(SplitContainer6);
            TabPageLOGO.Location = new Point(4, 31);
            TabPageLOGO.Name = "TabPageLOGO";
            TabPageLOGO.Padding = new Padding(3);
            TabPageLOGO.Size = new Size(1551, 664);
            TabPageLOGO.TabIndex = 3;
            TabPageLOGO.Text = "LOGO";
            TabPageLOGO.UseVisualStyleBackColor = true;
            // 
            // SplitContainer6
            // 
            SplitContainer6.Dock = DockStyle.Fill;
            SplitContainer6.Location = new Point(3, 3);
            SplitContainer6.Name = "SplitContainer6";
            // 
            // SplitContainer6.Panel1
            // 
            SplitContainer6.Panel1.Controls.Add(SplitContainer7);
            // 
            // SplitContainer6.Panel2
            // 
            SplitContainer6.Panel2.Controls.Add(SplitContainer8);
            SplitContainer6.Size = new Size(1545, 658);
            SplitContainer6.SplitterDistance = 658;
            SplitContainer6.TabIndex = 0;
            // 
            // SplitContainer7
            // 
            SplitContainer7.Dock = DockStyle.Fill;
            SplitContainer7.Location = new Point(0, 0);
            SplitContainer7.Name = "SplitContainer7";
            SplitContainer7.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer7.Panel1
            // 
            SplitContainer7.Panel1.Controls.Add(GroupBox12);
            // 
            // SplitContainer7.Panel2
            // 
            SplitContainer7.Panel2.Controls.Add(GroupBox11);
            SplitContainer7.Size = new Size(658, 658);
            SplitContainer7.SplitterDistance = 332;
            SplitContainer7.TabIndex = 0;
            // 
            // GroupBox12
            // 
            GroupBox12.BackColor = SystemColors.ActiveCaptionText;
            GroupBox12.Controls.Add(TabControl4);
            GroupBox12.Controls.Add(LOGO_ToolStrip);
            GroupBox12.Dock = DockStyle.Fill;
            GroupBox12.ForeColor = Color.Cyan;
            GroupBox12.Location = new Point(0, 0);
            GroupBox12.Margin = new Padding(8);
            GroupBox12.Name = "GroupBox12";
            GroupBox12.Padding = new Padding(8);
            GroupBox12.Size = new Size(658, 332);
            GroupBox12.TabIndex = 2;
            GroupBox12.TabStop = false;
            GroupBox12.Text = "REPL";
            // 
            // TabControl4
            // 
            TabControl4.Controls.Add(TabPage5);
            TabControl4.Dock = DockStyle.Fill;
            TabControl4.Location = new Point(8, 88);
            TabControl4.Margin = new Padding(8);
            TabControl4.Name = "TabControl4";
            TabControl4.SelectedIndex = 0;
            TabControl4.Size = new Size(642, 236);
            TabControl4.TabIndex = 1;
            // 
            // TabPage5
            // 
            TabPage5.Controls.Add(PROGRAM_TEXTBOX);
            TabPage5.Location = new Point(4, 31);
            TabPage5.Margin = new Padding(8);
            TabPage5.Name = "TabPage5";
            TabPage5.Padding = new Padding(8);
            TabPage5.Size = new Size(634, 201);
            TabPage5.TabIndex = 1;
            TabPage5.Text = "Program";
            TabPage5.UseVisualStyleBackColor = true;
            // 
            // PROGRAM_TEXTBOX
            // 
            PROGRAM_TEXTBOX.BackColor = SystemColors.InactiveBorder;
            PROGRAM_TEXTBOX.Dock = DockStyle.Fill;
            PROGRAM_TEXTBOX.Font = new Font("Consolas", 20.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            PROGRAM_TEXTBOX.Location = new Point(8, 8);
            PROGRAM_TEXTBOX.Margin = new Padding(5, 6, 5, 6);
            PROGRAM_TEXTBOX.Multiline = true;
            PROGRAM_TEXTBOX.Name = "PROGRAM_TEXTBOX";
            PROGRAM_TEXTBOX.ScrollBars = ScrollBars.Vertical;
            PROGRAM_TEXTBOX.Size = new Size(618, 185);
            PROGRAM_TEXTBOX.TabIndex = 0;
            // 
            // LOGO_ToolStrip
            // 
            LOGO_ToolStrip.BackColor = Color.Black;
            LOGO_ToolStrip.ImageScalingSize = new Size(50, 50);
            LOGO_ToolStrip.Items.AddRange(new ToolStripItem[] { ToolStripSeparator27, ToolStripButtonLOGO_NEW, ToolStripSeparator20, ToolStripSeparator29, ToolStripButtonLOGO_OPEN, ToolStripSeparator21, ToolStripSeparator25, ToolStripButtonLOGO_SAVE, ToolStripSeparator22, _ToolStripButton_RUN_LOGO, ToolStripSeparator24, ToolStripButtonEXECUTE_LOGO, ToolStripSeparator23, ToolStripSeparator26, ToolStripSeparator28 });
            LOGO_ToolStrip.Location = new Point(8, 31);
            LOGO_ToolStrip.Name = "LOGO_ToolStrip";
            LOGO_ToolStrip.Padding = new Padding(0, 0, 2, 0);
            LOGO_ToolStrip.Size = new Size(642, 57);
            LOGO_ToolStrip.TabIndex = 0;
            LOGO_ToolStrip.Text = "ToolStrip1";
            // 
            // ToolStripSeparator27
            // 
            ToolStripSeparator27.Name = "ToolStripSeparator27";
            ToolStripSeparator27.Size = new Size(6, 57);
            // 
            // ToolStripButtonLOGO_NEW
            // 
            ToolStripButtonLOGO_NEW.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            ToolStripButtonLOGO_NEW.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripButtonLOGO_NEW.Image = (Image)resources.GetObject("ToolStripButtonLOGO_NEW.Image");
            ToolStripButtonLOGO_NEW.ImageTransparentColor = Color.Magenta;
            ToolStripButtonLOGO_NEW.Name = "ToolStripButtonLOGO_NEW";
            ToolStripButtonLOGO_NEW.Size = new Size(54, 54);
            ToolStripButtonLOGO_NEW.Tag = "New Program";
            ToolStripButtonLOGO_NEW.Text = "&New";
            // 
            // ToolStripSeparator20
            // 
            ToolStripSeparator20.Name = "ToolStripSeparator20";
            ToolStripSeparator20.Size = new Size(6, 57);
            // 
            // ToolStripSeparator29
            // 
            ToolStripSeparator29.Name = "ToolStripSeparator29";
            ToolStripSeparator29.Size = new Size(6, 57);
            // 
            // ToolStripButtonLOGO_OPEN
            // 
            ToolStripButtonLOGO_OPEN.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            ToolStripButtonLOGO_OPEN.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripButtonLOGO_OPEN.Image = (Image)resources.GetObject("ToolStripButtonLOGO_OPEN.Image");
            ToolStripButtonLOGO_OPEN.ImageTransparentColor = Color.Magenta;
            ToolStripButtonLOGO_OPEN.Name = "ToolStripButtonLOGO_OPEN";
            ToolStripButtonLOGO_OPEN.Size = new Size(54, 54);
            ToolStripButtonLOGO_OPEN.Tag = "Open Program";
            ToolStripButtonLOGO_OPEN.Text = "&Open";
            // 
            // ToolStripSeparator21
            // 
            ToolStripSeparator21.Name = "ToolStripSeparator21";
            ToolStripSeparator21.Size = new Size(6, 57);
            // 
            // ToolStripSeparator25
            // 
            ToolStripSeparator25.Name = "ToolStripSeparator25";
            ToolStripSeparator25.Size = new Size(6, 57);
            // 
            // ToolStripButtonLOGO_SAVE
            // 
            ToolStripButtonLOGO_SAVE.BackColor = Color.FromArgb(Conversions.ToInteger(Conversions.ToByte(192)), Conversions.ToInteger(Conversions.ToByte(255)), Conversions.ToInteger(Conversions.ToByte(192)));
            ToolStripButtonLOGO_SAVE.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripButtonLOGO_SAVE.Image = (Image)resources.GetObject("ToolStripButtonLOGO_SAVE.Image");
            ToolStripButtonLOGO_SAVE.ImageTransparentColor = Color.Magenta;
            ToolStripButtonLOGO_SAVE.Name = "ToolStripButtonLOGO_SAVE";
            ToolStripButtonLOGO_SAVE.Size = new Size(54, 54);
            ToolStripButtonLOGO_SAVE.Tag = "Save Program";
            ToolStripButtonLOGO_SAVE.Text = "&Save ";
            // 
            // ToolStripSeparator22
            // 
            ToolStripSeparator22.Name = "ToolStripSeparator22";
            ToolStripSeparator22.Size = new Size(6, 57);
            // 
            // ToolStripButton_RUN_LOGO
            // 
            _ToolStripButton_RUN_LOGO.BackColor = Color.Black;
            _ToolStripButton_RUN_LOGO.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _ToolStripButton_RUN_LOGO.Image = My.Resources.Resources.Complier_RUN;
            _ToolStripButton_RUN_LOGO.ImageTransparentColor = Color.Magenta;
            _ToolStripButton_RUN_LOGO.Name = "_ToolStripButton_RUN_LOGO";
            _ToolStripButton_RUN_LOGO.Size = new Size(54, 54);
            _ToolStripButton_RUN_LOGO.Tag = "Compiles to AST - If it compiles to AST then it will Evaluate or Generate SAL";
            _ToolStripButton_RUN_LOGO.Text = "Compiles Code to AST";
            _ToolStripButton_RUN_LOGO.ToolTipText = "Compile Code to AST";
            // 
            // ToolStripSeparator24
            // 
            ToolStripSeparator24.Name = "ToolStripSeparator24";
            ToolStripSeparator24.Size = new Size(6, 57);
            // 
            // ToolStripButtonEXECUTE_LOGO
            // 
            ToolStripButtonEXECUTE_LOGO.BackColor = Color.Black;
            ToolStripButtonEXECUTE_LOGO.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripButtonEXECUTE_LOGO.Image = My.Resources.Resources.EYE_BLUE_;
            ToolStripButtonEXECUTE_LOGO.ImageTransparentColor = Color.Magenta;
            ToolStripButtonEXECUTE_LOGO.Name = "ToolStripButtonEXECUTE_LOGO";
            ToolStripButtonEXECUTE_LOGO.Size = new Size(54, 54);
            ToolStripButtonEXECUTE_LOGO.Tag = "Execute on SAL";
            ToolStripButtonEXECUTE_LOGO.Text = "Run";
            ToolStripButtonEXECUTE_LOGO.ToolTipText = "Runs Code on VM";
            // 
            // ToolStripSeparator23
            // 
            ToolStripSeparator23.Name = "ToolStripSeparator23";
            ToolStripSeparator23.Size = new Size(6, 57);
            // 
            // ToolStripSeparator26
            // 
            ToolStripSeparator26.Name = "ToolStripSeparator26";
            ToolStripSeparator26.Size = new Size(6, 57);
            // 
            // ToolStripSeparator28
            // 
            ToolStripSeparator28.Name = "ToolStripSeparator28";
            ToolStripSeparator28.Size = new Size(6, 57);
            // 
            // GroupBox11
            // 
            GroupBox11.BackColor = Color.Black;
            GroupBox11.Controls.Add(TabControl1);
            GroupBox11.Dock = DockStyle.Fill;
            GroupBox11.ForeColor = Color.Lime;
            GroupBox11.Location = new Point(0, 0);
            GroupBox11.Margin = new Padding(8);
            GroupBox11.Name = "GroupBox11";
            GroupBox11.Padding = new Padding(8);
            GroupBox11.Size = new Size(658, 322);
            GroupBox11.TabIndex = 2;
            GroupBox11.TabStop = false;
            GroupBox11.Text = "Errors";
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage4);
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.Location = new Point(8, 31);
            TabControl1.Margin = new Padding(8);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(642, 283);
            TabControl1.TabIndex = 0;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(LOGO_ERRORS);
            TabPage1.Location = new Point(4, 31);
            TabPage1.Margin = new Padding(5, 6, 5, 6);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(5, 6, 5, 6);
            TabPage1.Size = new Size(634, 248);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Errors";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // LOGO_ERRORS
            // 
            LOGO_ERRORS.Dock = DockStyle.Fill;
            LOGO_ERRORS.Font = new Font("Microsoft Tai Le", 14.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            LOGO_ERRORS.Location = new Point(5, 6);
            LOGO_ERRORS.Margin = new Padding(5, 6, 5, 6);
            LOGO_ERRORS.Name = "LOGO_ERRORS";
            LOGO_ERRORS.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            LOGO_ERRORS.Size = new Size(624, 236);
            LOGO_ERRORS.TabIndex = 0;
            LOGO_ERRORS.Text = "";
            // 
            // TabPage4
            // 
            TabPage4.Controls.Add(LOGO_HELP);
            TabPage4.Location = new Point(4, 22);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(634, 272);
            TabPage4.TabIndex = 1;
            TabPage4.Text = "Syntax";
            TabPage4.UseVisualStyleBackColor = true;
            // 
            // LOGO_HELP
            // 
            LOGO_HELP.Dock = DockStyle.Fill;
            LOGO_HELP.Location = new Point(3, 3);
            LOGO_HELP.Name = "LOGO_HELP";
            LOGO_HELP.Size = new Size(628, 266);
            LOGO_HELP.TabIndex = 0;
            LOGO_HELP.Text = resources.GetString("LOGO_HELP.Text");
            // 
            // SplitContainer8
            // 
            SplitContainer8.Dock = DockStyle.Fill;
            SplitContainer8.Location = new Point(0, 0);
            SplitContainer8.Name = "SplitContainer8";
            SplitContainer8.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer8.Panel1
            // 
            SplitContainer8.Panel1.Controls.Add(GroupBox15);
            // 
            // SplitContainer8.Panel2
            // 
            SplitContainer8.Panel2.Controls.Add(SplitContainer9);
            SplitContainer8.Size = new Size(883, 658);
            SplitContainer8.SplitterDistance = 294;
            SplitContainer8.TabIndex = 0;
            // 
            // GroupBox15
            // 
            GroupBox15.BackColor = Color.Black;
            GroupBox15.Controls.Add(logo_display_panel);
            GroupBox15.Dock = DockStyle.Fill;
            GroupBox15.ForeColor = Color.White;
            GroupBox15.Location = new Point(0, 0);
            GroupBox15.Name = "GroupBox15";
            GroupBox15.Size = new Size(883, 294);
            GroupBox15.TabIndex = 0;
            GroupBox15.TabStop = false;
            GroupBox15.Text = "Image Output";
            // 
            // logo_display_panel
            // 
            logo_display_panel.BackColor = Color.White;
            logo_display_panel.Dock = DockStyle.Fill;
            logo_display_panel.Location = new Point(3, 26);
            logo_display_panel.Name = "logo_display_panel";
            logo_display_panel.Size = new Size(877, 265);
            logo_display_panel.TabIndex = 0;
            // 
            // SplitContainer9
            // 
            SplitContainer9.Dock = DockStyle.Fill;
            SplitContainer9.Location = new Point(0, 0);
            SplitContainer9.Name = "SplitContainer9";
            // 
            // SplitContainer9.Panel1
            // 
            SplitContainer9.Panel1.Controls.Add(GroupBox13);
            // 
            // SplitContainer9.Panel2
            // 
            SplitContainer9.Panel2.Controls.Add(GroupBox14);
            SplitContainer9.Size = new Size(883, 360);
            SplitContainer9.SplitterDistance = 387;
            SplitContainer9.TabIndex = 0;
            // 
            // GroupBox13
            // 
            GroupBox13.BackgroundImage = My.Resources.Resources.Console_A;
            GroupBox13.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox13.Controls.Add(LOGO_TreeView);
            GroupBox13.Dock = DockStyle.Fill;
            GroupBox13.Location = new Point(0, 0);
            GroupBox13.Margin = new Padding(5, 6, 5, 6);
            GroupBox13.Name = "GroupBox13";
            GroupBox13.Padding = new Padding(5, 6, 5, 6);
            GroupBox13.Size = new Size(387, 360);
            GroupBox13.TabIndex = 1;
            GroupBox13.TabStop = false;
            // 
            // LOGO_TreeView
            // 
            LOGO_TreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            LOGO_TreeView.BackColor = SystemColors.InfoText;
            LOGO_TreeView.BorderStyle = BorderStyle.FixedSingle;
            LOGO_TreeView.Font = new Font("Courier New", 15.75f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            LOGO_TreeView.ForeColor = Color.MintCream;
            LOGO_TreeView.HotTracking = true;
            LOGO_TreeView.Location = new Point(45, 20);
            LOGO_TreeView.Margin = new Padding(5, 6, 5, 6);
            LOGO_TreeView.Name = "LOGO_TreeView";
            LOGO_TreeView.ShowNodeToolTips = true;
            LOGO_TreeView.Size = new Size(294, 328);
            LOGO_TreeView.TabIndex = 0;
            // 
            // GroupBox14
            // 
            GroupBox14.BackColor = Color.Black;
            GroupBox14.BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            GroupBox14.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox14.Controls.Add(LogoTextOut);
            GroupBox14.Dock = DockStyle.Fill;
            GroupBox14.ForeColor = Color.Lime;
            GroupBox14.Location = new Point(0, 0);
            GroupBox14.Margin = new Padding(3, 4, 3, 4);
            GroupBox14.Name = "GroupBox14";
            GroupBox14.Padding = new Padding(3, 4, 3, 4);
            GroupBox14.Size = new Size(492, 360);
            GroupBox14.TabIndex = 3;
            GroupBox14.TabStop = false;
            GroupBox14.Text = "Program Output";
            // 
            // LogoTextOut
            // 
            LogoTextOut.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            LogoTextOut.BackColor = Color.Gainsboro;
            LogoTextOut.Font = new Font("Consolas", 18.0f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            LogoTextOut.Location = new Point(8, 30);
            LogoTextOut.Margin = new Padding(3, 4, 3, 4);
            LogoTextOut.Name = "LogoTextOut";
            LogoTextOut.Size = new Size(472, 251);
            LogoTextOut.TabIndex = 0;
            LogoTextOut.Text = "";
            // 
            // OpenTextFileDialog
            // 
            OpenTextFileDialog.Filter = "All Files|*.*";
            OpenTextFileDialog.Title = "Open Program";
            // 
            // SaveTextFileDialog
            // 
            SaveTextFileDialog.Filter = "All Files|*.*";
            // 
            // ToolTip1
            // 
            ToolTip1.IsBalloon = true;
            // 
            // Multi_REPL
            // 
            AutoScaleDimensions = new SizeF(10.0f, 22.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1577, 746);
            Controls.Add(TabControl_ProgrammingLang_Repl);
            Font = new Font("Consolas", 14.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 6, 5, 6);
            Name = "Multi_REPL";
            Text = "SpydazWeb Programming Language REPL_ ";
            TabControl_ProgrammingLang_Repl.ResumeLayout(false);
            TabPageSpydaz_PL.ResumeLayout(false);
            TabControlRepl_Inner.ResumeLayout(false);
            TabPageSmall_PL_repl.ResumeLayout(false);
            SplitContainer3.Panel1.ResumeLayout(false);
            SplitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer3).EndInit();
            SplitContainer3.ResumeLayout(false);
            SplitContainer4.Panel1.ResumeLayout(false);
            SplitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer4).EndInit();
            SplitContainer4.ResumeLayout(false);
            GroupBox7.ResumeLayout(false);
            GroupBox7.PerformLayout();
            TabControl3.ResumeLayout(false);
            TabPage3.ResumeLayout(false);
            TabPage3.PerformLayout();
            Small_PL_ToolStrip.ResumeLayout(false);
            Small_PL_ToolStrip.PerformLayout();
            GroupBox8.ResumeLayout(false);
            GroupBox8.PerformLayout();
            SplitContainer5.Panel1.ResumeLayout(false);
            SplitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer5).EndInit();
            SplitContainer5.ResumeLayout(false);
            GroupBox9.ResumeLayout(false);
            Small_PL_TabControl_Repl_ErrorOutput.ResumeLayout(false);
            TabPageReplErrors.ResumeLayout(false);
            TabPagePl_Help.ResumeLayout(false);
            GroupBox10.ResumeLayout(false);
            TabPageSAL_REPL.ResumeLayout(false);
            SplitContainer1.Panel1.ResumeLayout(false);
            SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
            SplitContainer1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            GroupBox6.ResumeLayout(false);
            GroupBox5.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            SplitContainer2.Panel1.ResumeLayout(false);
            SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer2).EndInit();
            SplitContainer2.ResumeLayout(false);
            GroupBox3.ResumeLayout(false);
            TabControl2.ResumeLayout(false);
            TabPageSalReplHelp.ResumeLayout(false);
            TabPageSalReplErrors.ResumeLayout(false);
            TabPageSalReplErrors.PerformLayout();
            GroupBox4.ResumeLayout(false);
            GroupBox4.PerformLayout();
            TabControl_REPL_INPUT.ResumeLayout(false);
            TabPage2.ResumeLayout(false);
            TabPage2.PerformLayout();
            SAL_ToolStripRepl.ResumeLayout(false);
            SAL_ToolStripRepl.PerformLayout();
            TabPageLOGO.ResumeLayout(false);
            SplitContainer6.Panel1.ResumeLayout(false);
            SplitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer6).EndInit();
            SplitContainer6.ResumeLayout(false);
            SplitContainer7.Panel1.ResumeLayout(false);
            SplitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer7).EndInit();
            SplitContainer7.ResumeLayout(false);
            GroupBox12.ResumeLayout(false);
            GroupBox12.PerformLayout();
            TabControl4.ResumeLayout(false);
            TabPage5.ResumeLayout(false);
            TabPage5.PerformLayout();
            LOGO_ToolStrip.ResumeLayout(false);
            LOGO_ToolStrip.PerformLayout();
            GroupBox11.ResumeLayout(false);
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage4.ResumeLayout(false);
            SplitContainer8.Panel1.ResumeLayout(false);
            SplitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer8).EndInit();
            SplitContainer8.ResumeLayout(false);
            GroupBox15.ResumeLayout(false);
            SplitContainer9.Panel1.ResumeLayout(false);
            SplitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer9).EndInit();
            SplitContainer9.ResumeLayout(false);
            GroupBox13.ResumeLayout(false);
            GroupBox14.ResumeLayout(false);
            Load += new EventHandler(Multi_REPL_Load);
            ResumeLayout(false);
        }

        internal TabControl TabControl_ProgrammingLang_Repl;
        internal TabPage TabPageSpydaz_PL;
        internal TabControl TabControlRepl_Inner;
        internal TabPage TabPageSAL_REPL;
        internal SplitContainer SplitContainer1;
        internal GroupBox GroupBox1;
        internal GroupBox GroupBox6;
        internal RichTextBox SAL_RichTextBoxDisplayOutput;
        internal GroupBox GroupBox5;
        internal TreeView SAL_AST;
        internal GroupBox GroupBox2;
        internal TextBox SAL_RichTextBoxProgram;
        internal SplitContainer SplitContainer2;
        internal GroupBox GroupBox3;
        internal TabControl TabControl2;
        internal TabPage TabPageSalReplHelp;
        internal RichTextBox SAL_RichTextBoxHelp;
        internal TabPage TabPageSalReplErrors;
        internal TextBox SAL_TextBoxErrorOutput;
        internal GroupBox GroupBox4;
        internal TabControl TabControl_REPL_INPUT;
        internal TabPage TabPage2;
        internal TextBox SAL_TextBoxCodeInput;
        internal TabPage TabPageSmall_PL_repl;
        internal SplitContainer SplitContainer3;
        internal SplitContainer SplitContainer4;
        internal GroupBox GroupBox7;
        internal TabControl TabControl3;
        internal TabPage TabPage3;
        internal TextBox Small_PL_TextBoxCodeInput;
        internal ToolStrip Small_PL_ToolStrip;
        private ToolStripButton _Small_PL_NewToolStripButton;

        internal ToolStripButton Small_PL_NewToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_NewToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_NewToolStripButton != null)
                {
                    _Small_PL_NewToolStripButton.Click -= NewToolStripButton_Click;
                }

                _Small_PL_NewToolStripButton = value;
                if (_Small_PL_NewToolStripButton != null)
                {
                    _Small_PL_NewToolStripButton.Click += NewToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator10;
        private ToolStripButton _Small_PL_OpenToolStripButton;

        internal ToolStripButton Small_PL_OpenToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_OpenToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_OpenToolStripButton != null)
                {
                    _Small_PL_OpenToolStripButton.Click -= OpenToolStripButton_Click;
                }

                _Small_PL_OpenToolStripButton = value;
                if (_Small_PL_OpenToolStripButton != null)
                {
                    _Small_PL_OpenToolStripButton.Click += OpenToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator11;
        private ToolStripButton _Small_PL_SaveToolStripButton;

        internal ToolStripButton Small_PL_SaveToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_SaveToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_SaveToolStripButton != null)
                {
                    _Small_PL_SaveToolStripButton.Click -= SaveToolStripButton_Click;
                }

                _Small_PL_SaveToolStripButton = value;
                if (_Small_PL_SaveToolStripButton != null)
                {
                    _Small_PL_SaveToolStripButton.Click += SaveToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator12;
        internal ToolStripSeparator ToolStripSeparator13;
        internal ToolStripSeparator ToolStripSeparator14;
        internal ToolStripSeparator ToolStripSeparator15;
        private ToolStripButton _Small_PL_ToolStripButtonCompileCode;

        internal ToolStripButton Small_PL_ToolStripButtonCompileCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_ToolStripButtonCompileCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_ToolStripButtonCompileCode != null)
                {
                    _Small_PL_ToolStripButtonCompileCode.Click -= ToolStripButtonCompile_Click;
                }

                _Small_PL_ToolStripButtonCompileCode = value;
                if (_Small_PL_ToolStripButtonCompileCode != null)
                {
                    _Small_PL_ToolStripButtonCompileCode.Click += ToolStripButtonCompile_Click;
                }
            }
        }

        internal ToolStripButton Small_PL_ToolStripButtonRunCode;
        internal ToolStripSeparator ToolStripSeparator16;
        internal ToolStripButton Small_PL_ToolStripButtonCompilesTox86;
        internal ToolStripSeparator ToolStripSeparator17;
        private ToolStripButton _Small_PL_ButtonOpenVM;

        internal ToolStripButton Small_PL_ButtonOpenVM
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_ButtonOpenVM;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_ButtonOpenVM != null)
                {
                    _Small_PL_ButtonOpenVM.Click -= ButtonOpenVM_Click;
                }

                _Small_PL_ButtonOpenVM = value;
                if (_Small_PL_ButtonOpenVM != null)
                {
                    _Small_PL_ButtonOpenVM.Click += ButtonOpenVM_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator18;
        private ToolStripButton _Small_PL_HelpToolStripButton;

        internal ToolStripButton Small_PL_HelpToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_HelpToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_HelpToolStripButton != null)
                {
                    _Small_PL_HelpToolStripButton.Click -= HelpToolStripButton_Click;
                }

                _Small_PL_HelpToolStripButton = value;
                if (_Small_PL_HelpToolStripButton != null)
                {
                    _Small_PL_HelpToolStripButton.Click += HelpToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator19;
        internal GroupBox GroupBox8;
        internal TextBox Small_PL_TextBoxREPL_OUTPUT;
        internal SplitContainer SplitContainer5;
        internal GroupBox GroupBox9;
        internal TabControl Small_PL_TabControl_Repl_ErrorOutput;
        internal TabPage TabPageReplErrors;
        internal RichTextBox Small_PL_TextboxErrors;
        internal GroupBox GroupBox10;
        private TreeView _Small_PL_AstTreeView;

        internal TreeView Small_PL_AstTreeView
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Small_PL_AstTreeView;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Small_PL_AstTreeView != null)
                {
                    _Small_PL_AstTreeView.AfterSelect -= Small_PL_AstTreeView_AfterSelect;
                }

                _Small_PL_AstTreeView = value;
                if (_Small_PL_AstTreeView != null)
                {
                    _Small_PL_AstTreeView.AfterSelect += Small_PL_AstTreeView_AfterSelect;
                }
            }
        }

        internal OpenFileDialog OpenTextFileDialog;
        internal SaveFileDialog SaveTextFileDialog;
        internal ToolTip ToolTip1;
        internal TabPage TabPagePl_Help;
        internal RichTextBox RichTextBoxPL_Help;
        internal TabPage TabPageLOGO;
        internal SplitContainer SplitContainer6;
        internal SplitContainer SplitContainer7;
        internal GroupBox GroupBox12;
        internal TabControl TabControl4;
        internal TabPage TabPage5;
        internal TextBox PROGRAM_TEXTBOX;
        internal GroupBox GroupBox11;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal RichTextBox LOGO_ERRORS;
        internal TabPage TabPage4;
        internal RichTextBox LOGO_HELP;
        internal SplitContainer SplitContainer8;
        internal GroupBox GroupBox15;
        internal SplitContainer SplitContainer9;
        internal GroupBox GroupBox13;
        internal TreeView LOGO_TreeView;
        internal GroupBox GroupBox14;
        internal RichTextBox LogoTextOut;
        internal ToolStrip SAL_ToolStripRepl;
        private ToolStripButton _SAL_NewToolStripButton;

        internal ToolStripButton SAL_NewToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SAL_NewToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SAL_NewToolStripButton != null)
                {
                    _SAL_NewToolStripButton.Click -= SAL_NewToolStripButton_Click;
                }

                _SAL_NewToolStripButton = value;
                if (_SAL_NewToolStripButton != null)
                {
                    _SAL_NewToolStripButton.Click += SAL_NewToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator7;
        private ToolStripButton _SAL_OpenToolStripButton;

        internal ToolStripButton SAL_OpenToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SAL_OpenToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SAL_OpenToolStripButton != null)
                {
                    _SAL_OpenToolStripButton.Click -= SAL_OpenToolStripButton_Click;
                }

                _SAL_OpenToolStripButton = value;
                if (_SAL_OpenToolStripButton != null)
                {
                    _SAL_OpenToolStripButton.Click += SAL_OpenToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator6;
        private ToolStripButton _SAL_SaveToolStripButton;

        internal ToolStripButton SAL_SaveToolStripButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SAL_SaveToolStripButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SAL_SaveToolStripButton != null)
                {
                    _SAL_SaveToolStripButton.Click -= SAL_SaveToolStripButton_Click;
                }

                _SAL_SaveToolStripButton = value;
                if (_SAL_SaveToolStripButton != null)
                {
                    _SAL_SaveToolStripButton.Click += SAL_SaveToolStripButton_Click;
                }
            }
        }

        internal ToolStripSeparator toolStripSeparator;
        internal ToolStripSeparator ToolStripSeparator8;
        internal ToolStripSeparator toolStripSeparator1;
        private ToolStripButton _SAL_ToolStripButtonCompileCode;

        internal ToolStripButton SAL_ToolStripButtonCompileCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SAL_ToolStripButtonCompileCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SAL_ToolStripButtonCompileCode != null)
                {
                    _SAL_ToolStripButtonCompileCode.Click -= ToolStripButtonCompileCode_Click;
                }

                _SAL_ToolStripButtonCompileCode = value;
                if (_SAL_ToolStripButtonCompileCode != null)
                {
                    _SAL_ToolStripButtonCompileCode.Click += ToolStripButtonCompileCode_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator9;
        internal ToolStripButton SAL_ToolStripButtonRunCode;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripSeparator ToolStripSeparator2;
        private ToolStripButton _SAL_ButtonOpenVM;

        internal ToolStripButton SAL_ButtonOpenVM
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SAL_ButtonOpenVM;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SAL_ButtonOpenVM != null)
                {
                    _SAL_ButtonOpenVM.Click -= ButtonOpenVM_Click;
                }

                _SAL_ButtonOpenVM = value;
                if (_SAL_ButtonOpenVM != null)
                {
                    _SAL_ButtonOpenVM.Click += ButtonOpenVM_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripSeparator ToolStripSeparator5;
        internal ToolStrip LOGO_ToolStrip;
        internal ToolStripSeparator ToolStripSeparator27;
        internal ToolStripButton ToolStripButtonLOGO_NEW;
        internal ToolStripSeparator ToolStripSeparator20;
        internal ToolStripSeparator ToolStripSeparator29;
        internal ToolStripButton ToolStripButtonLOGO_OPEN;
        internal ToolStripSeparator ToolStripSeparator21;
        internal ToolStripSeparator ToolStripSeparator25;
        internal ToolStripButton ToolStripButtonLOGO_SAVE;
        internal ToolStripSeparator ToolStripSeparator22;
        private ToolStripButton _ToolStripButton_RUN_LOGO;

        internal ToolStripButton ToolStripButton_RUN_LOGO
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ToolStripButton_RUN_LOGO;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ToolStripButton_RUN_LOGO != null)
                {
                    _ToolStripButton_RUN_LOGO.Click -= ToolStripButton_RUN_LOGO_Click;
                }

                _ToolStripButton_RUN_LOGO = value;
                if (_ToolStripButton_RUN_LOGO != null)
                {
                    _ToolStripButton_RUN_LOGO.Click += ToolStripButton_RUN_LOGO_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator24;
        internal ToolStripButton ToolStripButtonEXECUTE_LOGO;
        internal ToolStripSeparator ToolStripSeparator23;
        internal ToolStripSeparator ToolStripSeparator26;
        internal ToolStripSeparator ToolStripSeparator28;
        internal Panel logo_display_panel;
    }
}
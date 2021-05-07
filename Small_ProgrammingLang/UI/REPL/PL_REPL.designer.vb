<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PL_REPL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PL_REPL))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageSmall_PL_repl = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabControl_REPL_INPUT = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Small_PL_TextBoxCodeInput = New System.Windows.Forms.TextBox()
        Me.ToolStripRepl = New System.Windows.Forms.ToolStrip()
        Me.Small_PL_NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_ToolStripButtonCompileCode = New System.Windows.Forms.ToolStripButton()
        Me.Small_PL_ToolStripButtonRunCode = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_ToolStripButtonCompilesTox86 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_ButtonOpenVM = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.Small_PL_HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Small_PL_TextBoxREPL_OUTPUT = New System.Windows.Forms.TextBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Small_PL_TabControl_Repl_ErrorOutput = New System.Windows.Forms.TabControl()
        Me.TabPageReplErrors = New System.Windows.Forms.TabPage()
        Me.Small_PL_TextboxErrors = New System.Windows.Forms.RichTextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Small_PL_AstTreeView = New System.Windows.Forms.TreeView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenTextFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveTextFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPageSmall_PL_repl.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl_REPL_INPUT.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ToolStripRepl.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Small_PL_TabControl_Repl_ErrorOutput.SuspendLayout()
        Me.TabPageReplErrors.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageSmall_PL_repl)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1660, 684)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageSmall_PL_repl
        '
        Me.TabPageSmall_PL_repl.Controls.Add(Me.SplitContainer1)
        Me.TabPageSmall_PL_repl.Location = New System.Drawing.Point(4, 30)
        Me.TabPageSmall_PL_repl.Name = "TabPageSmall_PL_repl"
        Me.TabPageSmall_PL_repl.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSmall_PL_repl.Size = New System.Drawing.Size(1652, 650)
        Me.TabPageSmall_PL_repl.TabIndex = 0
        Me.TabPageSmall_PL_repl.Text = "Small_PL"
        Me.TabPageSmall_PL_repl.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1646, 644)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(5)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox3)
        Me.SplitContainer2.Size = New System.Drawing.Size(1646, 492)
        Me.SplitContainer2.SplitterDistance = 529
        Me.SplitContainer2.SplitterWidth = 7
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Controls.Add(Me.TabControl_REPL_INPUT)
        Me.GroupBox1.Controls.Add(Me.ToolStripRepl)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.ForeColor = System.Drawing.Color.Cyan
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5)
        Me.GroupBox1.Size = New System.Drawing.Size(529, 492)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "REPL"
        '
        'TabControl_REPL_INPUT
        '
        Me.TabControl_REPL_INPUT.Controls.Add(Me.TabPage2)
        Me.TabControl_REPL_INPUT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_REPL_INPUT.Location = New System.Drawing.Point(5, 83)
        Me.TabControl_REPL_INPUT.Margin = New System.Windows.Forms.Padding(5)
        Me.TabControl_REPL_INPUT.Name = "TabControl_REPL_INPUT"
        Me.TabControl_REPL_INPUT.SelectedIndex = 0
        Me.TabControl_REPL_INPUT.Size = New System.Drawing.Size(519, 404)
        Me.TabControl_REPL_INPUT.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Small_PL_TextBoxCodeInput)
        Me.TabPage2.Location = New System.Drawing.Point(4, 30)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(5)
        Me.TabPage2.Size = New System.Drawing.Size(511, 370)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Program"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Small_PL_TextBoxCodeInput
        '
        Me.Small_PL_TextBoxCodeInput.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Small_PL_TextBoxCodeInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Small_PL_TextBoxCodeInput.Font = New System.Drawing.Font("Consolas", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Small_PL_TextBoxCodeInput.Location = New System.Drawing.Point(5, 5)
        Me.Small_PL_TextBoxCodeInput.Multiline = True
        Me.Small_PL_TextBoxCodeInput.Name = "Small_PL_TextBoxCodeInput"
        Me.Small_PL_TextBoxCodeInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Small_PL_TextBoxCodeInput.Size = New System.Drawing.Size(501, 360)
        Me.Small_PL_TextBoxCodeInput.TabIndex = 0
        '
        'ToolStripRepl
        '
        Me.ToolStripRepl.BackColor = System.Drawing.Color.Black
        Me.ToolStripRepl.ImageScalingSize = New System.Drawing.Size(50, 50)
        Me.ToolStripRepl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Small_PL_NewToolStripButton, Me.ToolStripSeparator7, Me.Small_PL_OpenToolStripButton, Me.ToolStripSeparator6, Me.Small_PL_SaveToolStripButton, Me.toolStripSeparator, Me.ToolStripSeparator8, Me.ToolStripSeparator9, Me.toolStripSeparator1, Me.Small_PL_ToolStripButtonCompileCode, Me.Small_PL_ToolStripButtonRunCode, Me.ToolStripSeparator3, Me.Small_PL_ToolStripButtonCompilesTox86, Me.ToolStripSeparator2, Me.Small_PL_ButtonOpenVM, Me.ToolStripSeparator4, Me.Small_PL_HelpToolStripButton, Me.ToolStripSeparator5})
        Me.ToolStripRepl.Location = New System.Drawing.Point(5, 26)
        Me.ToolStripRepl.Name = "ToolStripRepl"
        Me.ToolStripRepl.Size = New System.Drawing.Size(519, 57)
        Me.ToolStripRepl.TabIndex = 0
        Me.ToolStripRepl.Text = "ToolStrip1"
        '
        'Small_PL_NewToolStripButton
        '
        Me.Small_PL_NewToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Small_PL_NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_NewToolStripButton.Image = CType(resources.GetObject("Small_PL_NewToolStripButton.Image"), System.Drawing.Image)
        Me.Small_PL_NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_NewToolStripButton.Name = "Small_PL_NewToolStripButton"
        Me.Small_PL_NewToolStripButton.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_NewToolStripButton.Tag = "New Program"
        Me.Small_PL_NewToolStripButton.Text = "&New"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_OpenToolStripButton
        '
        Me.Small_PL_OpenToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Small_PL_OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_OpenToolStripButton.Image = CType(resources.GetObject("Small_PL_OpenToolStripButton.Image"), System.Drawing.Image)
        Me.Small_PL_OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_OpenToolStripButton.Name = "Small_PL_OpenToolStripButton"
        Me.Small_PL_OpenToolStripButton.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_OpenToolStripButton.Tag = "Open Program"
        Me.Small_PL_OpenToolStripButton.Text = "&Open"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_SaveToolStripButton
        '
        Me.Small_PL_SaveToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Small_PL_SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_SaveToolStripButton.Image = CType(resources.GetObject("Small_PL_SaveToolStripButton.Image"), System.Drawing.Image)
        Me.Small_PL_SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_SaveToolStripButton.Name = "Small_PL_SaveToolStripButton"
        Me.Small_PL_SaveToolStripButton.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_SaveToolStripButton.Tag = "Save Program"
        Me.Small_PL_SaveToolStripButton.Text = "&Save "
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 57)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 57)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 57)
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_ToolStripButtonCompileCode
        '
        Me.Small_PL_ToolStripButtonCompileCode.BackColor = System.Drawing.Color.Black
        Me.Small_PL_ToolStripButtonCompileCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_ToolStripButtonCompileCode.Image = Global.SDK.My.Resources.Resources.Complier_RUN
        Me.Small_PL_ToolStripButtonCompileCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_ToolStripButtonCompileCode.Name = "Small_PL_ToolStripButtonCompileCode"
        Me.Small_PL_ToolStripButtonCompileCode.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_ToolStripButtonCompileCode.Tag = "Compiles to AST - If it compiles to AST then it will Evaluate or Generate SAL"
        Me.Small_PL_ToolStripButtonCompileCode.Text = "Compiles Code to AST"
        Me.Small_PL_ToolStripButtonCompileCode.ToolTipText = "Compile Code to AST"
        '
        'Small_PL_ToolStripButtonRunCode
        '
        Me.Small_PL_ToolStripButtonRunCode.BackColor = System.Drawing.Color.Black
        Me.Small_PL_ToolStripButtonRunCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_ToolStripButtonRunCode.Image = Global.SDK.My.Resources.Resources.Arrow_Right
        Me.Small_PL_ToolStripButtonRunCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_ToolStripButtonRunCode.Name = "Small_PL_ToolStripButtonRunCode"
        Me.Small_PL_ToolStripButtonRunCode.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_ToolStripButtonRunCode.Tag = "Evaluate Code (Uses S-Expression)"
        Me.Small_PL_ToolStripButtonRunCode.Text = "Run"
        Me.Small_PL_ToolStripButtonRunCode.ToolTipText = "Runs Code on VM"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_ToolStripButtonCompilesTox86
        '
        Me.Small_PL_ToolStripButtonCompilesTox86.BackColor = System.Drawing.Color.Silver
        Me.Small_PL_ToolStripButtonCompilesTox86.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_ToolStripButtonCompilesTox86.Image = Global.SDK.My.Resources.Resources.Script
        Me.Small_PL_ToolStripButtonCompilesTox86.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_ToolStripButtonCompilesTox86.Name = "Small_PL_ToolStripButtonCompilesTox86"
        Me.Small_PL_ToolStripButtonCompilesTox86.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_ToolStripButtonCompilesTox86.Text = "Transpile to X86 Code"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_ButtonOpenVM
        '
        Me.Small_PL_ButtonOpenVM.BackColor = System.Drawing.Color.Black
        Me.Small_PL_ButtonOpenVM.BackgroundImage = Global.SDK.My.Resources.Resources.EYE_BLUE_
        Me.Small_PL_ButtonOpenVM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_ButtonOpenVM.Image = Global.SDK.My.Resources.Resources.EYE_BLUE_
        Me.Small_PL_ButtonOpenVM.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_ButtonOpenVM.Name = "Small_PL_ButtonOpenVM"
        Me.Small_PL_ButtonOpenVM.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_ButtonOpenVM.Text = "Load SAL"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 57)
        '
        'Small_PL_HelpToolStripButton
        '
        Me.Small_PL_HelpToolStripButton.BackColor = System.Drawing.Color.Black
        Me.Small_PL_HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Small_PL_HelpToolStripButton.Image = CType(resources.GetObject("Small_PL_HelpToolStripButton.Image"), System.Drawing.Image)
        Me.Small_PL_HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Small_PL_HelpToolStripButton.Name = "Small_PL_HelpToolStripButton"
        Me.Small_PL_HelpToolStripButton.Size = New System.Drawing.Size(54, 54)
        Me.Small_PL_HelpToolStripButton.Text = "He&lp Refference"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 57)
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.DarkGray
        Me.GroupBox3.BackgroundImage = Global.SDK.My.Resources.Resources.Dell_UltraSharp_27
        Me.GroupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox3.Controls.Add(Me.Small_PL_TextBoxREPL_OUTPUT)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.ForeColor = System.Drawing.Color.Cyan
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(5)
        Me.GroupBox3.Size = New System.Drawing.Size(1110, 492)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'Small_PL_TextBoxREPL_OUTPUT
        '
        Me.Small_PL_TextBoxREPL_OUTPUT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Small_PL_TextBoxREPL_OUTPUT.BackColor = System.Drawing.Color.Black
        Me.Small_PL_TextBoxREPL_OUTPUT.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Small_PL_TextBoxREPL_OUTPUT.ForeColor = System.Drawing.Color.Lime
        Me.Small_PL_TextBoxREPL_OUTPUT.Location = New System.Drawing.Point(0, 0)
        Me.Small_PL_TextBoxREPL_OUTPUT.Margin = New System.Windows.Forms.Padding(5)
        Me.Small_PL_TextBoxREPL_OUTPUT.Multiline = True
        Me.Small_PL_TextBoxREPL_OUTPUT.Name = "Small_PL_TextBoxREPL_OUTPUT"
        Me.Small_PL_TextBoxREPL_OUTPUT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Small_PL_TextBoxREPL_OUTPUT.Size = New System.Drawing.Size(1110, 362)
        Me.Small_PL_TextBoxREPL_OUTPUT.TabIndex = 1
        Me.Small_PL_TextBoxREPL_OUTPUT.Text = ">"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer3.Size = New System.Drawing.Size(1646, 146)
        Me.SplitContainer3.SplitterDistance = 532
        Me.SplitContainer3.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Black
        Me.GroupBox2.Controls.Add(Me.Small_PL_TabControl_Repl_ErrorOutput)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(5)
        Me.GroupBox2.Size = New System.Drawing.Size(532, 146)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Errors"
        '
        'Small_PL_TabControl_Repl_ErrorOutput
        '
        Me.Small_PL_TabControl_Repl_ErrorOutput.Controls.Add(Me.TabPageReplErrors)
        Me.Small_PL_TabControl_Repl_ErrorOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Small_PL_TabControl_Repl_ErrorOutput.Location = New System.Drawing.Point(5, 26)
        Me.Small_PL_TabControl_Repl_ErrorOutput.Margin = New System.Windows.Forms.Padding(5)
        Me.Small_PL_TabControl_Repl_ErrorOutput.Name = "Small_PL_TabControl_Repl_ErrorOutput"
        Me.Small_PL_TabControl_Repl_ErrorOutput.SelectedIndex = 0
        Me.Small_PL_TabControl_Repl_ErrorOutput.Size = New System.Drawing.Size(522, 115)
        Me.Small_PL_TabControl_Repl_ErrorOutput.TabIndex = 0
        '
        'TabPageReplErrors
        '
        Me.TabPageReplErrors.Controls.Add(Me.Small_PL_TextboxErrors)
        Me.TabPageReplErrors.Location = New System.Drawing.Point(4, 30)
        Me.TabPageReplErrors.Name = "TabPageReplErrors"
        Me.TabPageReplErrors.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReplErrors.Size = New System.Drawing.Size(514, 81)
        Me.TabPageReplErrors.TabIndex = 0
        Me.TabPageReplErrors.Text = "Errors"
        Me.TabPageReplErrors.UseVisualStyleBackColor = True
        '
        'Small_PL_TextboxErrors
        '
        Me.Small_PL_TextboxErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Small_PL_TextboxErrors.Font = New System.Drawing.Font("Microsoft Tai Le", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Small_PL_TextboxErrors.Location = New System.Drawing.Point(3, 3)
        Me.Small_PL_TextboxErrors.Name = "Small_PL_TextboxErrors"
        Me.Small_PL_TextboxErrors.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.Small_PL_TextboxErrors.Size = New System.Drawing.Size(508, 75)
        Me.Small_PL_TextboxErrors.TabIndex = 0
        Me.Small_PL_TextboxErrors.Text = ""
        '
        'GroupBox4
        '
        Me.GroupBox4.BackgroundImage = Global.SDK.My.Resources.Resources.Console_A
        Me.GroupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox4.Controls.Add(Me.Small_PL_AstTreeView)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1110, 146)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'Small_PL_AstTreeView
        '
        Me.Small_PL_AstTreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Small_PL_AstTreeView.BackColor = System.Drawing.SystemColors.InfoText
        Me.Small_PL_AstTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Small_PL_AstTreeView.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Small_PL_AstTreeView.ForeColor = System.Drawing.Color.MintCream
        Me.Small_PL_AstTreeView.HotTracking = True
        Me.Small_PL_AstTreeView.Location = New System.Drawing.Point(97, 15)
        Me.Small_PL_AstTreeView.Name = "Small_PL_AstTreeView"
        Me.Small_PL_AstTreeView.ShowNodeToolTips = True
        Me.Small_PL_AstTreeView.Size = New System.Drawing.Size(915, 124)
        Me.Small_PL_AstTreeView.TabIndex = 0
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        '
        'OpenTextFileDialog
        '
        Me.OpenTextFileDialog.Filter = "All Files|*.*"
        Me.OpenTextFileDialog.Title = "Open Program"
        '
        'SaveTextFileDialog
        '
        Me.SaveTextFileDialog.Filter = "All Files|*.*"
        '
        'FrmPL_REPL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1660, 684)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Tai Le", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "FrmPL_REPL"
        Me.Text = "SpydazWeb Basic Programming language REPL"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageSmall_PL_repl.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl_REPL_INPUT.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ToolStripRepl.ResumeLayout(False)
        Me.ToolStripRepl.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Small_PL_TabControl_Repl_ErrorOutput.ResumeLayout(False)
        Me.TabPageReplErrors.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageSmall_PL_repl As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TabControl_REPL_INPUT As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Small_PL_TextBoxCodeInput As TextBox
    Friend WithEvents ToolStripRepl As ToolStrip
    Friend WithEvents Small_PL_NewToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents Small_PL_OpenToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents Small_PL_SaveToolStripButton As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Small_PL_ToolStripButtonCompileCode As ToolStripButton
    Friend WithEvents Small_PL_ToolStripButtonRunCode As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Small_PL_ToolStripButtonCompilesTox86 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents Small_PL_ButtonOpenVM As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents Small_PL_HelpToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Small_PL_TextBoxREPL_OUTPUT As TextBox
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Small_PL_TabControl_Repl_ErrorOutput As TabControl
    Friend WithEvents TabPageReplErrors As TabPage
    Friend WithEvents Small_PL_TextboxErrors As RichTextBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Small_PL_AstTreeView As TreeView
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents OpenTextFileDialog As OpenFileDialog
    Friend WithEvents SaveTextFileDialog As SaveFileDialog
End Class

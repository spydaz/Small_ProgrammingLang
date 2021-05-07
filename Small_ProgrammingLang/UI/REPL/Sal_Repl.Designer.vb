<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Sal_Repl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sal_Repl))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageSAL_REPL = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.SAL_RichTextBoxDisplayOutput = New System.Windows.Forms.RichTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.SAL_AST = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SAL_RichTextBoxProgram = New System.Windows.Forms.TextBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPageSalReplHelp = New System.Windows.Forms.TabPage()
        Me.SAL_RichTextBoxHelp = New System.Windows.Forms.RichTextBox()
        Me.TabPageSalReplErrors = New System.Windows.Forms.TabPage()
        Me.SAL_TextBoxErrorOutput = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TabControl_REPL_INPUT = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SAL_TextBoxCodeInput = New System.Windows.Forms.TextBox()
        Me.ToolStripRepl = New System.Windows.Forms.ToolStrip()
        Me.SAL_NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.SAL_OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.SAL_SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SAL_ToolStripButtonCompileCode = New System.Windows.Forms.ToolStripButton()
        Me.SAL_ToolStripButtonRunCode = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonCompilesTox86 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SAL_ButtonOpenVM = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SAL_HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenTextFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveTextFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPageSAL_REPL.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPageSalReplHelp.SuspendLayout()
        Me.TabPageSalReplErrors.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl_REPL_INPUT.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ToolStripRepl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageSAL_REPL)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1451, 760)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageSAL_REPL
        '
        Me.TabPageSAL_REPL.Controls.Add(Me.SplitContainer1)
        Me.TabPageSAL_REPL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSAL_REPL.Name = "TabPageSAL_REPL"
        Me.TabPageSAL_REPL.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSAL_REPL.Size = New System.Drawing.Size(1443, 734)
        Me.TabPageSAL_REPL.TabIndex = 1
        Me.TabPageSAL_REPL.Text = "SAL"
        Me.TabPageSAL_REPL.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1437, 728)
        Me.SplitContainer1.SplitterDistance = 309
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Black
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(1437, 309)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SpydazWeb AI Programming Language Editor"
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Black
        Me.GroupBox6.BackgroundImage = Global.SDK.My.Resources.Resources.Dell_UltraSharp_27
        Me.GroupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox6.Controls.Add(Me.SAL_RichTextBoxDisplayOutput)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox6.Location = New System.Drawing.Point(457, 17)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox6.Size = New System.Drawing.Size(580, 290)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Program Output"
        '
        'SAL_RichTextBoxDisplayOutput
        '
        Me.SAL_RichTextBoxDisplayOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SAL_RichTextBoxDisplayOutput.BackColor = System.Drawing.Color.Gainsboro
        Me.SAL_RichTextBoxDisplayOutput.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_RichTextBoxDisplayOutput.Location = New System.Drawing.Point(5, 18)
        Me.SAL_RichTextBoxDisplayOutput.Margin = New System.Windows.Forms.Padding(2)
        Me.SAL_RichTextBoxDisplayOutput.Name = "SAL_RichTextBoxDisplayOutput"
        Me.SAL_RichTextBoxDisplayOutput.Size = New System.Drawing.Size(570, 186)
        Me.SAL_RichTextBoxDisplayOutput.TabIndex = 0
        Me.SAL_RichTextBoxDisplayOutput.Text = ""
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Black
        Me.GroupBox5.Controls.Add(Me.SAL_AST)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox5.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Aqua
        Me.GroupBox5.Location = New System.Drawing.Point(1037, 17)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox5.Size = New System.Drawing.Size(398, 290)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Abstract Syntax Tree"
        '
        'SAL_AST
        '
        Me.SAL_AST.BackColor = System.Drawing.SystemColors.Info
        Me.SAL_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SAL_AST.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_AST.Location = New System.Drawing.Point(2, 17)
        Me.SAL_AST.Margin = New System.Windows.Forms.Padding(2)
        Me.SAL_AST.Name = "SAL_AST"
        Me.SAL_AST.Size = New System.Drawing.Size(394, 271)
        Me.SAL_AST.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Black
        Me.GroupBox2.Controls.Add(Me.SAL_RichTextBoxProgram)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox2.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Yellow
        Me.GroupBox2.Location = New System.Drawing.Point(2, 17)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(455, 290)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "AST"
        '
        'SAL_RichTextBoxProgram
        '
        Me.SAL_RichTextBoxProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SAL_RichTextBoxProgram.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_RichTextBoxProgram.Location = New System.Drawing.Point(2, 17)
        Me.SAL_RichTextBoxProgram.Margin = New System.Windows.Forms.Padding(2)
        Me.SAL_RichTextBoxProgram.Multiline = True
        Me.SAL_RichTextBoxProgram.Name = "SAL_RichTextBoxProgram"
        Me.SAL_RichTextBoxProgram.Size = New System.Drawing.Size(451, 271)
        Me.SAL_RichTextBoxProgram.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer2.Size = New System.Drawing.Size(1437, 415)
        Me.SplitContainer2.SplitterDistance = 600
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Black
        Me.GroupBox3.Controls.Add(Me.TabControl2)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(600, 415)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Error Output"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPageSalReplHelp)
        Me.TabControl2.Controls.Add(Me.TabPageSalReplErrors)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(2, 17)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(596, 396)
        Me.TabControl2.TabIndex = 0
        '
        'TabPageSalReplHelp
        '
        Me.TabPageSalReplHelp.Controls.Add(Me.SAL_RichTextBoxHelp)
        Me.TabPageSalReplHelp.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSalReplHelp.Name = "TabPageSalReplHelp"
        Me.TabPageSalReplHelp.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSalReplHelp.Size = New System.Drawing.Size(588, 369)
        Me.TabPageSalReplHelp.TabIndex = 1
        Me.TabPageSalReplHelp.Text = "Syntax"
        Me.TabPageSalReplHelp.UseVisualStyleBackColor = True
        '
        'SAL_RichTextBoxHelp
        '
        Me.SAL_RichTextBoxHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SAL_RichTextBoxHelp.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_RichTextBoxHelp.Location = New System.Drawing.Point(3, 3)
        Me.SAL_RichTextBoxHelp.Name = "SAL_RichTextBoxHelp"
        Me.SAL_RichTextBoxHelp.Size = New System.Drawing.Size(582, 363)
        Me.SAL_RichTextBoxHelp.TabIndex = 0
        Me.SAL_RichTextBoxHelp.Text = resources.GetString("SAL_RichTextBoxHelp.Text")
        '
        'TabPageSalReplErrors
        '
        Me.TabPageSalReplErrors.Controls.Add(Me.SAL_TextBoxErrorOutput)
        Me.TabPageSalReplErrors.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSalReplErrors.Name = "TabPageSalReplErrors"
        Me.TabPageSalReplErrors.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSalReplErrors.Size = New System.Drawing.Size(588, 369)
        Me.TabPageSalReplErrors.TabIndex = 0
        Me.TabPageSalReplErrors.Text = "Errors"
        Me.TabPageSalReplErrors.UseVisualStyleBackColor = True
        '
        'SAL_TextBoxErrorOutput
        '
        Me.SAL_TextBoxErrorOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SAL_TextBoxErrorOutput.Font = New System.Drawing.Font("Consolas", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_TextBoxErrorOutput.Location = New System.Drawing.Point(3, 3)
        Me.SAL_TextBoxErrorOutput.Margin = New System.Windows.Forms.Padding(2)
        Me.SAL_TextBoxErrorOutput.Multiline = True
        Me.SAL_TextBoxErrorOutput.Name = "SAL_TextBoxErrorOutput"
        Me.SAL_TextBoxErrorOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.SAL_TextBoxErrorOutput.Size = New System.Drawing.Size(582, 363)
        Me.SAL_TextBoxErrorOutput.TabIndex = 1
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox4.Controls.Add(Me.TabControl_REPL_INPUT)
        Me.GroupBox4.Controls.Add(Me.ToolStripRepl)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.ForeColor = System.Drawing.Color.Cyan
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(5)
        Me.GroupBox4.Size = New System.Drawing.Size(833, 415)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "REPL"
        '
        'TabControl_REPL_INPUT
        '
        Me.TabControl_REPL_INPUT.Controls.Add(Me.TabPage1)
        Me.TabControl_REPL_INPUT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_REPL_INPUT.Location = New System.Drawing.Point(5, 50)
        Me.TabControl_REPL_INPUT.Margin = New System.Windows.Forms.Padding(5)
        Me.TabControl_REPL_INPUT.Name = "TabControl_REPL_INPUT"
        Me.TabControl_REPL_INPUT.SelectedIndex = 0
        Me.TabControl_REPL_INPUT.Size = New System.Drawing.Size(823, 360)
        Me.TabControl_REPL_INPUT.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SAL_TextBoxCodeInput)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(5)
        Me.TabPage1.Size = New System.Drawing.Size(815, 334)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Program"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SAL_TextBoxCodeInput
        '
        Me.SAL_TextBoxCodeInput.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.SAL_TextBoxCodeInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SAL_TextBoxCodeInput.Font = New System.Drawing.Font("Consolas", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SAL_TextBoxCodeInput.Location = New System.Drawing.Point(5, 5)
        Me.SAL_TextBoxCodeInput.Multiline = True
        Me.SAL_TextBoxCodeInput.Name = "SAL_TextBoxCodeInput"
        Me.SAL_TextBoxCodeInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.SAL_TextBoxCodeInput.Size = New System.Drawing.Size(805, 324)
        Me.SAL_TextBoxCodeInput.TabIndex = 0
        '
        'ToolStripRepl
        '
        Me.ToolStripRepl.BackColor = System.Drawing.Color.Black
        Me.ToolStripRepl.ImageScalingSize = New System.Drawing.Size(25, 25)
        Me.ToolStripRepl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SAL_NewToolStripButton, Me.ToolStripSeparator7, Me.SAL_OpenToolStripButton, Me.ToolStripSeparator6, Me.SAL_SaveToolStripButton, Me.toolStripSeparator, Me.ToolStripSeparator8, Me.ToolStripSeparator9, Me.toolStripSeparator1, Me.SAL_ToolStripButtonCompileCode, Me.SAL_ToolStripButtonRunCode, Me.ToolStripSeparator3, Me.ToolStripButtonCompilesTox86, Me.ToolStripSeparator2, Me.SAL_ButtonOpenVM, Me.ToolStripSeparator4, Me.SAL_HelpToolStripButton, Me.ToolStripSeparator5})
        Me.ToolStripRepl.Location = New System.Drawing.Point(5, 18)
        Me.ToolStripRepl.Name = "ToolStripRepl"
        Me.ToolStripRepl.Size = New System.Drawing.Size(823, 32)
        Me.ToolStripRepl.TabIndex = 0
        Me.ToolStripRepl.Text = "ToolStrip1"
        '
        'SAL_NewToolStripButton
        '
        Me.SAL_NewToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SAL_NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_NewToolStripButton.Image = CType(resources.GetObject("SAL_NewToolStripButton.Image"), System.Drawing.Image)
        Me.SAL_NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_NewToolStripButton.Name = "SAL_NewToolStripButton"
        Me.SAL_NewToolStripButton.Size = New System.Drawing.Size(29, 29)
        Me.SAL_NewToolStripButton.Tag = "New Program"
        Me.SAL_NewToolStripButton.Text = "&New"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 32)
        '
        'SAL_OpenToolStripButton
        '
        Me.SAL_OpenToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SAL_OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_OpenToolStripButton.Image = CType(resources.GetObject("SAL_OpenToolStripButton.Image"), System.Drawing.Image)
        Me.SAL_OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_OpenToolStripButton.Name = "SAL_OpenToolStripButton"
        Me.SAL_OpenToolStripButton.Size = New System.Drawing.Size(29, 29)
        Me.SAL_OpenToolStripButton.Tag = "Open Program"
        Me.SAL_OpenToolStripButton.Text = "&Open"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 32)
        '
        'SAL_SaveToolStripButton
        '
        Me.SAL_SaveToolStripButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SAL_SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_SaveToolStripButton.Image = CType(resources.GetObject("SAL_SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SAL_SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_SaveToolStripButton.Name = "SAL_SaveToolStripButton"
        Me.SAL_SaveToolStripButton.Size = New System.Drawing.Size(29, 29)
        Me.SAL_SaveToolStripButton.Tag = "Save Program"
        Me.SAL_SaveToolStripButton.Text = "&Save "
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 32)
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'SAL_ToolStripButtonCompileCode
        '
        Me.SAL_ToolStripButtonCompileCode.BackColor = System.Drawing.Color.Black
        Me.SAL_ToolStripButtonCompileCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_ToolStripButtonCompileCode.Image = Global.SDK.My.Resources.Resources.Complier_RUN
        Me.SAL_ToolStripButtonCompileCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_ToolStripButtonCompileCode.Name = "SAL_ToolStripButtonCompileCode"
        Me.SAL_ToolStripButtonCompileCode.Size = New System.Drawing.Size(29, 29)
        Me.SAL_ToolStripButtonCompileCode.Tag = "Compiles to AST - If it compiles to AST then it will Evaluate or Generate SAL"
        Me.SAL_ToolStripButtonCompileCode.Text = "Compiles Code to AST"
        Me.SAL_ToolStripButtonCompileCode.ToolTipText = "Compile Code to AST"
        '
        'SAL_ToolStripButtonRunCode
        '
        Me.SAL_ToolStripButtonRunCode.BackColor = System.Drawing.Color.Black
        Me.SAL_ToolStripButtonRunCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_ToolStripButtonRunCode.Image = Global.SDK.My.Resources.Resources.Arrow_Right
        Me.SAL_ToolStripButtonRunCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_ToolStripButtonRunCode.Name = "SAL_ToolStripButtonRunCode"
        Me.SAL_ToolStripButtonRunCode.Size = New System.Drawing.Size(29, 29)
        Me.SAL_ToolStripButtonRunCode.Tag = "Evaluate Code (Uses S-Expression)"
        Me.SAL_ToolStripButtonRunCode.Text = "Run"
        Me.SAL_ToolStripButtonRunCode.ToolTipText = "Runs Code on VM"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripButtonCompilesTox86
        '
        Me.ToolStripButtonCompilesTox86.BackColor = System.Drawing.Color.Silver
        Me.ToolStripButtonCompilesTox86.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonCompilesTox86.Image = Global.SDK.My.Resources.Resources.Script
        Me.ToolStripButtonCompilesTox86.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCompilesTox86.Name = "ToolStripButtonCompilesTox86"
        Me.ToolStripButtonCompilesTox86.Size = New System.Drawing.Size(29, 29)
        Me.ToolStripButtonCompilesTox86.Text = "Transpile to X86 Code"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'SAL_ButtonOpenVM
        '
        Me.SAL_ButtonOpenVM.BackColor = System.Drawing.Color.Black
        Me.SAL_ButtonOpenVM.BackgroundImage = Global.SDK.My.Resources.Resources.EYE_BLUE_
        Me.SAL_ButtonOpenVM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_ButtonOpenVM.Image = Global.SDK.My.Resources.Resources.EYE_BLUE_
        Me.SAL_ButtonOpenVM.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_ButtonOpenVM.Name = "SAL_ButtonOpenVM"
        Me.SAL_ButtonOpenVM.Size = New System.Drawing.Size(29, 29)
        Me.SAL_ButtonOpenVM.Text = "Load SAL"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 32)
        '
        'SAL_HelpToolStripButton
        '
        Me.SAL_HelpToolStripButton.BackColor = System.Drawing.Color.Black
        Me.SAL_HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SAL_HelpToolStripButton.Image = CType(resources.GetObject("SAL_HelpToolStripButton.Image"), System.Drawing.Image)
        Me.SAL_HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SAL_HelpToolStripButton.Name = "SAL_HelpToolStripButton"
        Me.SAL_HelpToolStripButton.Size = New System.Drawing.Size(29, 29)
        Me.SAL_HelpToolStripButton.Text = "He&lp Refference"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 32)
        '
        'OpenTextFileDialog
        '
        Me.OpenTextFileDialog.FileName = "OpenFileDialog1"
        '
        'FrmSal_Repl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1451, 760)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmSal_Repl"
        Me.Text = "Spydaz Assembly Language REPL"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageSAL_REPL.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPageSalReplHelp.ResumeLayout(False)
        Me.TabPageSalReplErrors.ResumeLayout(False)
        Me.TabPageSalReplErrors.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabControl_REPL_INPUT.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ToolStripRepl.ResumeLayout(False)
        Me.ToolStripRepl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageSAL_REPL As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents SAL_RichTextBoxDisplayOutput As RichTextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents SAL_AST As TreeView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents SAL_RichTextBoxProgram As TextBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPageSalReplHelp As TabPage
    Friend WithEvents SAL_RichTextBoxHelp As RichTextBox
    Friend WithEvents TabPageSalReplErrors As TabPage
    Friend WithEvents SAL_TextBoxErrorOutput As TextBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents TabControl_REPL_INPUT As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents SAL_TextBoxCodeInput As TextBox
    Friend WithEvents ToolStripRepl As ToolStrip
    Friend WithEvents SAL_NewToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents SAL_OpenToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents SAL_SaveToolStripButton As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents SAL_ToolStripButtonCompileCode As ToolStripButton
    Friend WithEvents SAL_ToolStripButtonRunCode As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButtonCompilesTox86 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents SAL_ButtonOpenVM As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents SAL_HelpToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents OpenTextFileDialog As OpenFileDialog
    Friend WithEvents SaveTextFileDialog As SaveFileDialog
End Class

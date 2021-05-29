Imports System.Windows.Forms
Imports AI_BASIC

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IDE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IDE))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControlInputCode = New System.Windows.Forms.TabControl()
        Me.Basic = New System.Windows.Forms.TabPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.CodeTextBox = New AI_BASIC.SyntaxTextBox()
        Me.LineNumberor = New AI_BASIC.LineNumbers.LineNumbering()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.TabControl_AST_TEXT = New System.Windows.Forms.TabControl()
        Me.TabPageAstSyntax = New System.Windows.Forms.TabPage()
        Me.AstSyntaxJson = New System.Windows.Forms.RichTextBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PL_AstTreeView = New System.Windows.Forms.TreeView()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.EmbeddedFilePaths = New System.Windows.Forms.ListBox()
        Me.ToolsEmbeddedfiles = New System.Windows.Forms.ToolStrip()
        Me.AddEmbedded = New System.Windows.Forms.ToolStripButton()
        Me.RemoveEmbedded = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RefferenceFilePaths = New System.Windows.Forms.ListBox()
        Me.ToolsRefferences = New System.Windows.Forms.ToolStrip()
        Me.AddRefferences = New System.Windows.Forms.ToolStripButton()
        Me.RemoveRefference = New System.Windows.Forms.ToolStripButton()
        Me.IDE_MainMenu = New System.Windows.Forms.ToolStrip()
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.But_Compile = New System.Windows.Forms.ToolStripButton()
        Me.But_Run = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CompileVBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DebugSal = New System.Windows.Forms.ToolStripButton()
        Me.But_RunOnSal = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CreateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PluginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AvatarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SALSnippetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VBSnippetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControlOutput = New System.Windows.Forms.TabControl()
        Me.TabPage_IDE_RESULTS = New System.Windows.Forms.TabPage()
        Me.DISPLAY_OUT = New System.Windows.Forms.RichTextBox()
        Me.TabPage_IDE_COMPILER_ERRORS = New System.Windows.Forms.TabPage()
        Me.CompilerErrors = New System.Windows.Forms.RichTextBox()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ToolStripButtonRepl = New System.Windows.Forms.ToolStripButton()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControlInputCode.SuspendLayout()
        Me.Basic.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.TabControl_AST_TEXT.SuspendLayout()
        Me.TabPageAstSyntax.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolsEmbeddedfiles.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ToolsRefferences.SuspendLayout()
        Me.IDE_MainMenu.SuspendLayout()
        Me.TabControlOutput.SuspendLayout()
        Me.TabPage_IDE_RESULTS.SuspendLayout()
        Me.TabPage_IDE_COMPILER_ERRORS.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.Black
        Me.SplitContainer1.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.AppWorkspace
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.IDE_MainMenu)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControlOutput)
        Me.SplitContainer1.Size = New System.Drawing.Size(1559, 935)
        Me.SplitContainer1.SplitterDistance = 615
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 52)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TabControlInputCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer2.Size = New System.Drawing.Size(1559, 563)
        Me.SplitContainer2.SplitterDistance = 1240
        Me.SplitContainer2.TabIndex = 3
        '
        'TabControlInputCode
        '
        Me.TabControlInputCode.Controls.Add(Me.Basic)
        Me.TabControlInputCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlInputCode.Font = New System.Drawing.Font("Comic Sans MS", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlInputCode.ImageList = Me.ImageList1
        Me.TabControlInputCode.Location = New System.Drawing.Point(0, 0)
        Me.TabControlInputCode.Name = "TabControlInputCode"
        Me.TabControlInputCode.SelectedIndex = 0
        Me.TabControlInputCode.Size = New System.Drawing.Size(1240, 563)
        Me.TabControlInputCode.TabIndex = 4
        '
        'Basic
        '
        Me.Basic.BackColor = System.Drawing.Color.Black
        Me.Basic.Controls.Add(Me.SplitContainer3)
        Me.Basic.ImageIndex = 33
        Me.Basic.Location = New System.Drawing.Point(4, 38)
        Me.Basic.Name = "Basic"
        Me.Basic.Padding = New System.Windows.Forms.Padding(3)
        Me.Basic.Size = New System.Drawing.Size(1232, 521)
        Me.Basic.TabIndex = 1
        Me.Basic.Text = "Editor"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.CodeTextBox)
        Me.SplitContainer3.Panel1.Controls.Add(Me.LineNumberor)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(1226, 515)
        Me.SplitContainer3.SplitterDistance = 580
        Me.SplitContainer3.TabIndex = 0
        '
        'CodeTextBox
        '
        Me.CodeTextBox.AcceptsTab = True
        Me.CodeTextBox.AutoWordSelection = True
        Me.CodeTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodeTextBox.Font = New System.Drawing.Font("Consolas", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeTextBox.Location = New System.Drawing.Point(36, 0)
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.CodeTextBox.Size = New System.Drawing.Size(544, 515)
        Me.CodeTextBox.Syntax = CType(resources.GetObject("CodeTextBox.Syntax"), System.Collections.Generic.List(Of String))
        Me.CodeTextBox.TabIndex = 0
        Me.CodeTextBox.Text = ""
        '
        'LineNumberor
        '
        Me.LineNumberor._SeeThroughMode_ = False
        Me.LineNumberor.AutoSizing = True
        Me.LineNumberor.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LineNumberor.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue
        Me.LineNumberor.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.LineNumberor.BorderLines_Color = System.Drawing.Color.SlateGray
        Me.LineNumberor.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot
        Me.LineNumberor.BorderLines_Thickness = 1.0!
        Me.LineNumberor.Dock = System.Windows.Forms.DockStyle.Left
        Me.LineNumberor.DockSide = AI_BASIC.LineNumbers.LineNumbering.LineNumberDockSide.Left
        Me.LineNumberor.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LineNumberor.GridLines_Color = System.Drawing.Color.SlateGray
        Me.LineNumberor.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot
        Me.LineNumberor.GridLines_Thickness = 1.0!
        Me.LineNumberor.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight
        Me.LineNumberor.LineNrs_AntiAlias = True
        Me.LineNumberor.LineNrs_AsHexadecimal = False
        Me.LineNumberor.LineNrs_ClippedByItemRectangle = True
        Me.LineNumberor.LineNrs_LeadingZeroes = True
        Me.LineNumberor.LineNrs_Offset = New System.Drawing.Size(0, 0)
        Me.LineNumberor.Location = New System.Drawing.Point(0, 0)
        Me.LineNumberor.Margin = New System.Windows.Forms.Padding(0)
        Me.LineNumberor.MarginLines_Color = System.Drawing.Color.SlateGray
        Me.LineNumberor.MarginLines_Side = AI_BASIC.LineNumbers.LineNumbering.LineNumberDockSide.Right
        Me.LineNumberor.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid
        Me.LineNumberor.MarginLines_Thickness = 1.0!
        Me.LineNumberor.Name = "LineNumberor"
        Me.LineNumberor.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.LineNumberor.ParentRichTextBox = Me.CodeTextBox
        Me.LineNumberor.Show_BackgroundGradient = True
        Me.LineNumberor.Show_BorderLines = True
        Me.LineNumberor.Show_GridLines = True
        Me.LineNumberor.Show_LineNrs = True
        Me.LineNumberor.Show_MarginLines = True
        Me.LineNumberor.Size = New System.Drawing.Size(36, 515)
        Me.LineNumberor.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BackColor = System.Drawing.Color.Black
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.TabControl_AST_TEXT)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.PL_AstTreeView)
        Me.SplitContainer4.Size = New System.Drawing.Size(642, 515)
        Me.SplitContainer4.SplitterDistance = 321
        Me.SplitContainer4.TabIndex = 0
        '
        'TabControl_AST_TEXT
        '
        Me.TabControl_AST_TEXT.Controls.Add(Me.TabPageAstSyntax)
        Me.TabControl_AST_TEXT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_AST_TEXT.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_AST_TEXT.ImageList = Me.ImageList1
        Me.TabControl_AST_TEXT.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_AST_TEXT.Name = "TabControl_AST_TEXT"
        Me.TabControl_AST_TEXT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabControl_AST_TEXT.RightToLeftLayout = True
        Me.TabControl_AST_TEXT.SelectedIndex = 0
        Me.TabControl_AST_TEXT.ShowToolTips = True
        Me.TabControl_AST_TEXT.Size = New System.Drawing.Size(642, 321)
        Me.TabControl_AST_TEXT.TabIndex = 0
        '
        'TabPageAstSyntax
        '
        Me.TabPageAstSyntax.Controls.Add(Me.AstSyntaxJson)
        Me.TabPageAstSyntax.ImageIndex = 21
        Me.TabPageAstSyntax.Location = New System.Drawing.Point(4, 36)
        Me.TabPageAstSyntax.Name = "TabPageAstSyntax"
        Me.TabPageAstSyntax.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAstSyntax.Size = New System.Drawing.Size(634, 281)
        Me.TabPageAstSyntax.TabIndex = 3
        Me.TabPageAstSyntax.Text = "AstSyntax"
        Me.TabPageAstSyntax.UseVisualStyleBackColor = True
        '
        'AstSyntaxJson
        '
        Me.AstSyntaxJson.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.AstSyntaxJson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AstSyntaxJson.Font = New System.Drawing.Font("Comic Sans MS", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AstSyntaxJson.Location = New System.Drawing.Point(3, 3)
        Me.AstSyntaxJson.Name = "AstSyntaxJson"
        Me.AstSyntaxJson.Size = New System.Drawing.Size(628, 275)
        Me.AstSyntaxJson.TabIndex = 0
        Me.AstSyntaxJson.Text = ""
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "AppWorkspace.png")
        Me.ImageList1.Images.SetKeyName(1, "Arrow_Right.png")
        Me.ImageList1.Images.SetKeyName(2, "Bar2.PNG")
        Me.ImageList1.Images.SetKeyName(3, "Complier_RUN.png")
        Me.ImageList1.Images.SetKeyName(4, "Console A.jpg")
        Me.ImageList1.Images.SetKeyName(5, "console-icon.png")
        Me.ImageList1.Images.SetKeyName(6, "Dell UltraSharp 27.png")
        Me.ImageList1.Images.SetKeyName(7, "editcopy.png")
        Me.ImageList1.Images.SetKeyName(8, "editcut.png")
        Me.ImageList1.Images.SetKeyName(9, "editpaste.png")
        Me.ImageList1.Images.SetKeyName(10, "editredo.png")
        Me.ImageList1.Images.SetKeyName(11, "editundo.png")
        Me.ImageList1.Images.SetKeyName(12, "error.png")
        Me.ImageList1.Images.SetKeyName(13, "EYE BLUE .gif")
        Me.ImageList1.Images.SetKeyName(14, "filenew.png")
        Me.ImageList1.Images.SetKeyName(15, "fileopen.png")
        Me.ImageList1.Images.SetKeyName(16, "filesave.png")
        Me.ImageList1.Images.SetKeyName(17, "filesaveas.png")
        Me.ImageList1.Images.SetKeyName(18, "glass.png")
        Me.ImageList1.Images.SetKeyName(19, "Icon_UpVote.png")
        Me.ImageList1.Images.SetKeyName(20, "information.png")
        Me.ImageList1.Images.SetKeyName(21, "intellisenseevent.png")
        Me.ImageList1.Images.SetKeyName(22, "intellisenseitem.png")
        Me.ImageList1.Images.SetKeyName(23, "intellisensekeyword.png")
        Me.ImageList1.Images.SetKeyName(24, "intellisenselabel.png")
        Me.ImageList1.Images.SetKeyName(25, "intellisensemethod.png")
        Me.ImageList1.Images.SetKeyName(26, "intellisenseproperty.png")
        Me.ImageList1.Images.SetKeyName(27, "intellisensesubroutine.png")
        Me.ImageList1.Images.SetKeyName(28, "intellisensevariable.png")
        Me.ImageList1.Images.SetKeyName(29, "programbreakpoint.png")
        Me.ImageList1.Images.SetKeyName(30, "programbuild.png")
        Me.ImageList1.Images.SetKeyName(31, "programcheck.png")
        Me.ImageList1.Images.SetKeyName(32, "programrun.png")
        Me.ImageList1.Images.SetKeyName(33, "Script_Code_Html_.png")
        Me.ImageList1.Images.SetKeyName(34, "search.png")
        Me.ImageList1.Images.SetKeyName(35, "SERIES 1 sal9000.bmp")
        Me.ImageList1.Images.SetKeyName(36, "shield.png")
        Me.ImageList1.Images.SetKeyName(37, "splash.png")
        Me.ImageList1.Images.SetKeyName(38, "Turtle.png")
        Me.ImageList1.Images.SetKeyName(39, "vbexport.png")
        Me.ImageList1.Images.SetKeyName(40, "warning.png")
        Me.ImageList1.Images.SetKeyName(41, "webopen.png")
        Me.ImageList1.Images.SetKeyName(42, "websave.png")
        '
        'PL_AstTreeView
        '
        Me.PL_AstTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PL_AstTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PL_AstTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PL_AstTreeView.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PL_AstTreeView.ForeColor = System.Drawing.Color.MintCream
        Me.PL_AstTreeView.HotTracking = True
        Me.PL_AstTreeView.ImageIndex = 0
        Me.PL_AstTreeView.ImageList = Me.ImageList1
        Me.PL_AstTreeView.Location = New System.Drawing.Point(0, 0)
        Me.PL_AstTreeView.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PL_AstTreeView.Name = "PL_AstTreeView"
        Me.PL_AstTreeView.RightToLeftLayout = True
        Me.PL_AstTreeView.SelectedImageIndex = 0
        Me.PL_AstTreeView.ShowNodeToolTips = True
        Me.PL_AstTreeView.Size = New System.Drawing.Size(642, 190)
        Me.PL_AstTreeView.TabIndex = 1
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.ToolsEmbeddedfiles)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer5.Panel2.Controls.Add(Me.ToolsRefferences)
        Me.SplitContainer5.Size = New System.Drawing.Size(315, 563)
        Me.SplitContainer5.SplitterDistance = 230
        Me.SplitContainer5.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = CType(resources.GetObject("GroupBox1.BackgroundImage"), System.Drawing.Image)
        Me.GroupBox1.Controls.Add(Me.EmbeddedFilePaths)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Linen
        Me.GroupBox1.Location = New System.Drawing.Point(0, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(315, 175)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Embedded Files"
        '
        'EmbeddedFilePaths
        '
        Me.EmbeddedFilePaths.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.EmbeddedFilePaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EmbeddedFilePaths.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmbeddedFilePaths.FormattingEnabled = True
        Me.EmbeddedFilePaths.ItemHeight = 22
        Me.EmbeddedFilePaths.Location = New System.Drawing.Point(3, 24)
        Me.EmbeddedFilePaths.Name = "EmbeddedFilePaths"
        Me.EmbeddedFilePaths.Size = New System.Drawing.Size(309, 148)
        Me.EmbeddedFilePaths.TabIndex = 0
        '
        'ToolsEmbeddedfiles
        '
        Me.ToolsEmbeddedfiles.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ToolsEmbeddedfiles.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolsEmbeddedfiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddEmbedded, Me.RemoveEmbedded})
        Me.ToolsEmbeddedfiles.Location = New System.Drawing.Point(0, 0)
        Me.ToolsEmbeddedfiles.Name = "ToolsEmbeddedfiles"
        Me.ToolsEmbeddedfiles.Size = New System.Drawing.Size(315, 55)
        Me.ToolsEmbeddedfiles.TabIndex = 1
        Me.ToolsEmbeddedfiles.Text = "ToolStrip1"
        '
        'AddEmbedded
        '
        Me.AddEmbedded.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddEmbedded.Image = Global.AI_BASIC.My.Resources.Resources.Arrow_Right
        Me.AddEmbedded.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddEmbedded.Name = "AddEmbedded"
        Me.AddEmbedded.Size = New System.Drawing.Size(52, 52)
        Me.AddEmbedded.Text = "ToolStripButton3"
        '
        'RemoveEmbedded
        '
        Me.RemoveEmbedded.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RemoveEmbedded.Image = Global.AI_BASIC.My.Resources.Resources._error
        Me.RemoveEmbedded.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RemoveEmbedded.Name = "RemoveEmbedded"
        Me.RemoveEmbedded.Size = New System.Drawing.Size(52, 52)
        Me.RemoveEmbedded.Text = "ToolStripButton4"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackgroundImage = CType(resources.GetObject("GroupBox2.BackgroundImage"), System.Drawing.Image)
        Me.GroupBox2.Controls.Add(Me.RefferenceFilePaths)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Linen
        Me.GroupBox2.Location = New System.Drawing.Point(0, 55)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(315, 274)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Refference Libs"
        '
        'RefferenceFilePaths
        '
        Me.RefferenceFilePaths.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.RefferenceFilePaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RefferenceFilePaths.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefferenceFilePaths.FormattingEnabled = True
        Me.RefferenceFilePaths.ItemHeight = 22
        Me.RefferenceFilePaths.Location = New System.Drawing.Point(3, 24)
        Me.RefferenceFilePaths.Name = "RefferenceFilePaths"
        Me.RefferenceFilePaths.Size = New System.Drawing.Size(309, 247)
        Me.RefferenceFilePaths.TabIndex = 0
        '
        'ToolsRefferences
        '
        Me.ToolsRefferences.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ToolsRefferences.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolsRefferences.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddRefferences, Me.RemoveRefference})
        Me.ToolsRefferences.Location = New System.Drawing.Point(0, 0)
        Me.ToolsRefferences.Name = "ToolsRefferences"
        Me.ToolsRefferences.Size = New System.Drawing.Size(315, 55)
        Me.ToolsRefferences.TabIndex = 1
        Me.ToolsRefferences.Text = "ToolStrip2"
        '
        'AddRefferences
        '
        Me.AddRefferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddRefferences.Image = Global.AI_BASIC.My.Resources.Resources.Arrow_Right
        Me.AddRefferences.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddRefferences.Name = "AddRefferences"
        Me.AddRefferences.Size = New System.Drawing.Size(52, 52)
        Me.AddRefferences.Text = "ToolStripButton1"
        '
        'RemoveRefference
        '
        Me.RemoveRefference.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RemoveRefference.Image = Global.AI_BASIC.My.Resources.Resources._error
        Me.RemoveRefference.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RemoveRefference.Name = "RemoveRefference"
        Me.RemoveRefference.Size = New System.Drawing.Size(52, 52)
        Me.RemoveRefference.Text = "ToolStripButton2"
        '
        'IDE_MainMenu
        '
        Me.IDE_MainMenu.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Bar2
        Me.IDE_MainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IDE_MainMenu.ImageScalingSize = New System.Drawing.Size(45, 45)
        Me.IDE_MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.ToolStripSeparator2, Me.But_Compile, Me.But_Run, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator1, Me.ToolStripButton1, Me.ToolStripSeparator3, Me.DebugSal, Me.But_RunOnSal, Me.ToolStripSeparator4, Me.ToolStripButton2, Me.ToolStripButtonRepl})
        Me.IDE_MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.IDE_MainMenu.Name = "IDE_MainMenu"
        Me.IDE_MainMenu.Size = New System.Drawing.Size(1559, 52)
        Me.IDE_MainMenu.TabIndex = 2
        Me.IDE_MainMenu.Text = "ToolStrip1"
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.filenew
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.NewToolStripButton.Text = "&New"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.fileopen
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.filesave
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 52)
        '
        'But_Compile
        '
        Me.But_Compile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.But_Compile.Image = Global.AI_BASIC.My.Resources.Resources.Complier_RUN
        Me.But_Compile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.But_Compile.Name = "But_Compile"
        Me.But_Compile.Size = New System.Drawing.Size(49, 49)
        Me.But_Compile.Text = "Debug"
        '
        'But_Run
        '
        Me.But_Run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.But_Run.Image = Global.AI_BASIC.My.Resources.Resources.programrun
        Me.But_Run.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.But_Run.Name = "But_Run"
        Me.But_Run.Size = New System.Drawing.Size(49, 49)
        Me.But_Run.Text = "Run"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 52)
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CutToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.editcut
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.CutToolStripButton.Text = "C&ut"
        '
        'CopyToolStripButton
        '
        Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.editcopy
        Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        Me.CopyToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.CopyToolStripButton.Text = "&Copy"
        '
        'PasteToolStripButton
        '
        Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PasteToolStripButton.Image = Global.AI_BASIC.My.Resources.Resources.editpaste
        Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        Me.PasteToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.PasteToolStripButton.Text = "&Paste"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 52)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompileVBToolStripMenuItem})
        Me.ToolStripButton1.Image = Global.AI_BASIC.My.Resources.Resources.dotnet
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(58, 49)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'CompileVBToolStripMenuItem
        '
        Me.CompileVBToolStripMenuItem.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompileVBToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.Script_Code_Html
        Me.CompileVBToolStripMenuItem.Name = "CompileVBToolStripMenuItem"
        Me.CompileVBToolStripMenuItem.Size = New System.Drawing.Size(190, 32)
        Me.CompileVBToolStripMenuItem.Text = "Compile VB"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 52)
        '
        'DebugSal
        '
        Me.DebugSal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DebugSal.Image = Global.AI_BASIC.My.Resources.Resources.Arrow_Right
        Me.DebugSal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DebugSal.Name = "DebugSal"
        Me.DebugSal.Size = New System.Drawing.Size(49, 49)
        Me.DebugSal.Tag = "Debug Sal Code"
        Me.DebugSal.Text = "Debug Sal Code"
        '
        'But_RunOnSal
        '
        Me.But_RunOnSal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.But_RunOnSal.Image = Global.AI_BASIC.My.Resources.Resources.EYE_BLUE
        Me.But_RunOnSal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.But_RunOnSal.Name = "But_RunOnSal"
        Me.But_RunOnSal.Size = New System.Drawing.Size(49, 49)
        Me.But_RunOnSal.Text = "Run On Sal"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 52)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateToolStripMenuItem, Me.InsertToolStripMenuItem})
        Me.ToolStripButton2.Image = Global.AI_BASIC.My.Resources.Resources.Script_Code_Html
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(58, 49)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'CreateToolStripMenuItem
        '
        Me.CreateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PluginToolStripMenuItem, Me.DeviceToolStripMenuItem, Me.UpdateToolStripMenuItem, Me.AvatarToolStripMenuItem})
        Me.CreateToolStripMenuItem.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CreateToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.intellisensemethod
        Me.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem"
        Me.CreateToolStripMenuItem.Size = New System.Drawing.Size(152, 32)
        Me.CreateToolStripMenuItem.Text = "Create"
        '
        'PluginToolStripMenuItem
        '
        Me.PluginToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PluginToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.SpydazLookingGlass
        Me.PluginToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PluginToolStripMenuItem.Name = "PluginToolStripMenuItem"
        Me.PluginToolStripMenuItem.Size = New System.Drawing.Size(156, 34)
        Me.PluginToolStripMenuItem.Text = "Plugin"
        '
        'DeviceToolStripMenuItem
        '
        Me.DeviceToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeviceToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.SpydazLookingGlass
        Me.DeviceToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeviceToolStripMenuItem.Name = "DeviceToolStripMenuItem"
        Me.DeviceToolStripMenuItem.Size = New System.Drawing.Size(156, 34)
        Me.DeviceToolStripMenuItem.Text = "Device"
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.SpydazLookingGlass
        Me.UpdateToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(156, 34)
        Me.UpdateToolStripMenuItem.Text = "Update"
        '
        'AvatarToolStripMenuItem
        '
        Me.AvatarToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AvatarToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.SpydazLookingGlass
        Me.AvatarToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AvatarToolStripMenuItem.Name = "AvatarToolStripMenuItem"
        Me.AvatarToolStripMenuItem.Size = New System.Drawing.Size(156, 34)
        Me.AvatarToolStripMenuItem.Text = "Avatar"
        '
        'InsertToolStripMenuItem
        '
        Me.InsertToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SALSnippetToolStripMenuItem, Me.VBSnippetToolStripMenuItem})
        Me.InsertToolStripMenuItem.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InsertToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.Script_Code_Html
        Me.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem"
        Me.InsertToolStripMenuItem.Size = New System.Drawing.Size(152, 32)
        Me.InsertToolStripMenuItem.Text = "Insert"
        '
        'SALSnippetToolStripMenuItem
        '
        Me.SALSnippetToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.SERIES_1_sal9000
        Me.SALSnippetToolStripMenuItem.Name = "SALSnippetToolStripMenuItem"
        Me.SALSnippetToolStripMenuItem.Size = New System.Drawing.Size(197, 32)
        Me.SALSnippetToolStripMenuItem.Text = "SAL Snippet"
        '
        'VBSnippetToolStripMenuItem
        '
        Me.VBSnippetToolStripMenuItem.Image = Global.AI_BASIC.My.Resources.Resources.dotnet
        Me.VBSnippetToolStripMenuItem.Name = "VBSnippetToolStripMenuItem"
        Me.VBSnippetToolStripMenuItem.Size = New System.Drawing.Size(197, 32)
        Me.VBSnippetToolStripMenuItem.Text = "VB Snippet"
        '
        'TabControlOutput
        '
        Me.TabControlOutput.Controls.Add(Me.TabPage_IDE_RESULTS)
        Me.TabControlOutput.Controls.Add(Me.TabPage_IDE_COMPILER_ERRORS)
        Me.TabControlOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlOutput.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlOutput.ImageList = Me.ImageList1
        Me.TabControlOutput.Location = New System.Drawing.Point(0, 0)
        Me.TabControlOutput.Name = "TabControlOutput"
        Me.TabControlOutput.SelectedIndex = 0
        Me.TabControlOutput.Size = New System.Drawing.Size(1559, 316)
        Me.TabControlOutput.TabIndex = 1
        '
        'TabPage_IDE_RESULTS
        '
        Me.TabPage_IDE_RESULTS.AutoScroll = True
        Me.TabPage_IDE_RESULTS.BackColor = System.Drawing.Color.Black
        Me.TabPage_IDE_RESULTS.Controls.Add(Me.DISPLAY_OUT)
        Me.TabPage_IDE_RESULTS.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage_IDE_RESULTS.ImageIndex = 9
        Me.TabPage_IDE_RESULTS.Location = New System.Drawing.Point(4, 36)
        Me.TabPage_IDE_RESULTS.Name = "TabPage_IDE_RESULTS"
        Me.TabPage_IDE_RESULTS.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_IDE_RESULTS.Size = New System.Drawing.Size(1551, 276)
        Me.TabPage_IDE_RESULTS.TabIndex = 0
        Me.TabPage_IDE_RESULTS.Text = "Results"
        Me.TabPage_IDE_RESULTS.UseVisualStyleBackColor = True
        '
        'DISPLAY_OUT
        '
        Me.DISPLAY_OUT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DISPLAY_OUT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DISPLAY_OUT.Font = New System.Drawing.Font("Comic Sans MS", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DISPLAY_OUT.Location = New System.Drawing.Point(3, 3)
        Me.DISPLAY_OUT.Name = "DISPLAY_OUT"
        Me.DISPLAY_OUT.Size = New System.Drawing.Size(1545, 270)
        Me.DISPLAY_OUT.TabIndex = 9999
        Me.DISPLAY_OUT.TabStop = False
        Me.DISPLAY_OUT.Text = ""
        '
        'TabPage_IDE_COMPILER_ERRORS
        '
        Me.TabPage_IDE_COMPILER_ERRORS.AutoScroll = True
        Me.TabPage_IDE_COMPILER_ERRORS.BackColor = System.Drawing.Color.Black
        Me.TabPage_IDE_COMPILER_ERRORS.Controls.Add(Me.CompilerErrors)
        Me.TabPage_IDE_COMPILER_ERRORS.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage_IDE_COMPILER_ERRORS.ImageIndex = 12
        Me.TabPage_IDE_COMPILER_ERRORS.Location = New System.Drawing.Point(4, 36)
        Me.TabPage_IDE_COMPILER_ERRORS.Name = "TabPage_IDE_COMPILER_ERRORS"
        Me.TabPage_IDE_COMPILER_ERRORS.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_IDE_COMPILER_ERRORS.Size = New System.Drawing.Size(1551, 276)
        Me.TabPage_IDE_COMPILER_ERRORS.TabIndex = 1
        Me.TabPage_IDE_COMPILER_ERRORS.Text = "Errors"
        Me.TabPage_IDE_COMPILER_ERRORS.UseVisualStyleBackColor = True
        '
        'CompilerErrors
        '
        Me.CompilerErrors.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CompilerErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompilerErrors.Font = New System.Drawing.Font("Courier New", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompilerErrors.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CompilerErrors.Location = New System.Drawing.Point(3, 3)
        Me.CompilerErrors.Name = "CompilerErrors"
        Me.CompilerErrors.Size = New System.Drawing.Size(1545, 270)
        Me.CompilerErrors.TabIndex = 99999
        Me.CompilerErrors.TabStop = False
        Me.CompilerErrors.Text = ""
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.BalloonTipText = "Running in background"
        Me.NotifyIcon1.BalloonTipTitle = "SpydazWeb AI"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "SpydazWeb AI"
        Me.NotifyIcon1.Visible = True
        '
        'ToolStripButtonRepl
        '
        Me.ToolStripButtonRepl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRepl.Image = Global.AI_BASIC.My.Resources.Resources.console_icon
        Me.ToolStripButtonRepl.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRepl.Name = "ToolStripButtonRepl"
        Me.ToolStripButtonRepl.Size = New System.Drawing.Size(49, 49)
        Me.ToolStripButtonRepl.Text = "Repl"
        '
        'IDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Bar2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1559, 935)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "IDE"
        Me.Text = "SpydazWeb Basic ZX21"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControlInputCode.ResumeLayout(False)
        Me.Basic.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.TabControl_AST_TEXT.ResumeLayout(False)
        Me.TabPageAstSyntax.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.Panel2.PerformLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ToolsEmbeddedfiles.ResumeLayout(False)
        Me.ToolsEmbeddedfiles.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ToolsRefferences.ResumeLayout(False)
        Me.ToolsRefferences.PerformLayout()
        Me.IDE_MainMenu.ResumeLayout(False)
        Me.IDE_MainMenu.PerformLayout()
        Me.TabControlOutput.ResumeLayout(False)
        Me.TabPage_IDE_RESULTS.ResumeLayout(False)
        Me.TabPage_IDE_COMPILER_ERRORS.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents IDE_MainMenu As ToolStrip
    Friend WithEvents NewToolStripButton As ToolStripButton
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents But_Compile As ToolStripButton
    Friend WithEvents But_Run As ToolStripButton
    Friend WithEvents But_RunOnSal As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents CutToolStripButton As ToolStripButton
    Friend WithEvents CopyToolStripButton As ToolStripButton
    Friend WithEvents PasteToolStripButton As ToolStripButton
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TabControlOutput As TabControl
    Friend WithEvents TabPage_IDE_RESULTS As TabPage
    Friend WithEvents DISPLAY_OUT As RichTextBox
    Friend WithEvents TabPage_IDE_COMPILER_ERRORS As TabPage
    Friend WithEvents CompilerErrors As RichTextBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolStripButton1 As ToolStripDropDownButton
    Friend WithEvents CompileVBToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents DebugSal As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripDropDownButton
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TabControlInputCode As TabControl
    Friend WithEvents Basic As TabPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents CodeTextBox As SyntaxTextBox
    Friend WithEvents LineNumberor As LineNumbers.LineNumbering
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents TabControl_AST_TEXT As TabControl
    Friend WithEvents TabPageAstSyntax As TabPage
    Friend WithEvents AstSyntaxJson As RichTextBox
    Friend WithEvents PL_AstTreeView As TreeView
    Friend WithEvents CreateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PluginToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeviceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AvatarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents ToolsEmbeddedfiles As ToolStrip
    Friend WithEvents AddEmbedded As ToolStripButton
    Friend WithEvents RemoveEmbedded As ToolStripButton
    Friend WithEvents ToolsRefferences As ToolStrip
    Friend WithEvents AddRefferences As ToolStripButton
    Friend WithEvents RemoveRefference As ToolStripButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents EmbeddedFilePaths As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RefferenceFilePaths As ListBox
    Friend WithEvents InsertToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SALSnippetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VBSnippetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ToolStripButtonRepl As ToolStripButton
End Class

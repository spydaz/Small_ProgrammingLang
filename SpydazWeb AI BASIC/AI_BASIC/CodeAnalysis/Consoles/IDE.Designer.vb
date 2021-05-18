<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IDE))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.Basic = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CodeTextBox = New AI_BASIC.SyntaxTextBox()
        Me.LineNumberor = New AI_BASIC.LineNumbers.LineNumbering()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControlOutput = New System.Windows.Forms.TabControl()
        Me.TabPage_IDE_RESULTS = New System.Windows.Forms.TabPage()
        Me.DISPLAY_OUT = New System.Windows.Forms.RichTextBox()
        Me.TabPage_IDE_COMPILER_ERRORS = New System.Windows.Forms.TabPage()
        Me.CompilerErrors = New System.Windows.Forms.RichTextBox()
        Me.TabPageAstSyntax = New System.Windows.Forms.TabPage()
        Me.AstSyntaxJson = New System.Windows.Forms.RichTextBox()
        Me.Small_PL_AstTreeView = New System.Windows.Forms.TreeView()
        Me.TabControlInputCode = New System.Windows.Forms.TabControl()
        Me.ToolStrip1.SuspendLayout()
        Me.Basic.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControlOutput.SuspendLayout()
        Me.TabPage_IDE_RESULTS.SuspendLayout()
        Me.TabPage_IDE_COMPILER_ERRORS.SuspendLayout()
        Me.TabPageAstSyntax.SuspendLayout()
        Me.TabControlInputCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Bar2
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(45, 45)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.ToolStripSeparator2, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1247, 52)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.AI_BASIC.My.Resources.Resources.Complier_RUN
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(49, 49)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.AI_BASIC.My.Resources.Resources.Arrow_Right
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(49, 49)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.AI_BASIC.My.Resources.Resources.EYE_BLUE
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(49, 49)
        Me.ToolStripButton3.Text = "ToolStripButton3"
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
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(49, 49)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'Basic
        '
        Me.Basic.Controls.Add(Me.SplitContainer1)
        Me.Basic.Location = New System.Drawing.Point(4, 22)
        Me.Basic.Name = "Basic"
        Me.Basic.Padding = New System.Windows.Forms.Padding(3)
        Me.Basic.Size = New System.Drawing.Size(1239, 372)
        Me.Basic.TabIndex = 1
        Me.Basic.Text = "Editor"
        Me.Basic.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CodeTextBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LineNumberor)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1233, 366)
        Me.SplitContainer1.SplitterDistance = 713
        Me.SplitContainer1.TabIndex = 0
        '
        'CodeTextBox
        '
        Me.CodeTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.CodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodeTextBox.Font = New System.Drawing.Font("Courier New", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeTextBox.Location = New System.Drawing.Point(36, 0)
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.CodeTextBox.Size = New System.Drawing.Size(677, 366)
        Me.CodeTextBox.Syntax = CType(resources.GetObject("CodeTextBox.Syntax"), System.Collections.Generic.List(Of String))
        Me.CodeTextBox.TabIndex = 1
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
        Me.LineNumberor.Size = New System.Drawing.Size(36, 366)
        Me.LineNumberor.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TabControlOutput)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Small_PL_AstTreeView)
        Me.SplitContainer2.Size = New System.Drawing.Size(516, 366)
        Me.SplitContainer2.SplitterDistance = 172
        Me.SplitContainer2.TabIndex = 0
        '
        'TabControlOutput
        '
        Me.TabControlOutput.Controls.Add(Me.TabPage_IDE_RESULTS)
        Me.TabControlOutput.Controls.Add(Me.TabPage_IDE_COMPILER_ERRORS)
        Me.TabControlOutput.Controls.Add(Me.TabPageAstSyntax)
        Me.TabControlOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlOutput.Location = New System.Drawing.Point(0, 0)
        Me.TabControlOutput.Name = "TabControlOutput"
        Me.TabControlOutput.SelectedIndex = 0
        Me.TabControlOutput.Size = New System.Drawing.Size(516, 172)
        Me.TabControlOutput.TabIndex = 0
        '
        'TabPage_IDE_RESULTS
        '
        Me.TabPage_IDE_RESULTS.Controls.Add(Me.DISPLAY_OUT)
        Me.TabPage_IDE_RESULTS.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_IDE_RESULTS.Name = "TabPage_IDE_RESULTS"
        Me.TabPage_IDE_RESULTS.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_IDE_RESULTS.Size = New System.Drawing.Size(508, 146)
        Me.TabPage_IDE_RESULTS.TabIndex = 0
        Me.TabPage_IDE_RESULTS.Text = "Results"
        Me.TabPage_IDE_RESULTS.UseVisualStyleBackColor = True
        '
        'DISPLAY_OUT
        '
        Me.DISPLAY_OUT.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DISPLAY_OUT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DISPLAY_OUT.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DISPLAY_OUT.Location = New System.Drawing.Point(3, 3)
        Me.DISPLAY_OUT.Name = "DISPLAY_OUT"
        Me.DISPLAY_OUT.Size = New System.Drawing.Size(502, 140)
        Me.DISPLAY_OUT.TabIndex = 1
        Me.DISPLAY_OUT.Text = ""
        '
        'TabPage_IDE_COMPILER_ERRORS
        '
        Me.TabPage_IDE_COMPILER_ERRORS.Controls.Add(Me.CompilerErrors)
        Me.TabPage_IDE_COMPILER_ERRORS.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_IDE_COMPILER_ERRORS.Name = "TabPage_IDE_COMPILER_ERRORS"
        Me.TabPage_IDE_COMPILER_ERRORS.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_IDE_COMPILER_ERRORS.Size = New System.Drawing.Size(508, 146)
        Me.TabPage_IDE_COMPILER_ERRORS.TabIndex = 1
        Me.TabPage_IDE_COMPILER_ERRORS.Text = "Errors"
        Me.TabPage_IDE_COMPILER_ERRORS.UseVisualStyleBackColor = True
        '
        'CompilerErrors
        '
        Me.CompilerErrors.BackColor = System.Drawing.SystemColors.ControlLight
        Me.CompilerErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompilerErrors.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompilerErrors.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CompilerErrors.Location = New System.Drawing.Point(3, 3)
        Me.CompilerErrors.Name = "CompilerErrors"
        Me.CompilerErrors.Size = New System.Drawing.Size(502, 140)
        Me.CompilerErrors.TabIndex = 0
        Me.CompilerErrors.Text = ""
        '
        'TabPageAstSyntax
        '
        Me.TabPageAstSyntax.Controls.Add(Me.AstSyntaxJson)
        Me.TabPageAstSyntax.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAstSyntax.Name = "TabPageAstSyntax"
        Me.TabPageAstSyntax.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAstSyntax.Size = New System.Drawing.Size(508, 146)
        Me.TabPageAstSyntax.TabIndex = 2
        Me.TabPageAstSyntax.Text = "AstSyntax"
        Me.TabPageAstSyntax.UseVisualStyleBackColor = True
        '
        'AstSyntaxJson
        '
        Me.AstSyntaxJson.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.AstSyntaxJson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AstSyntaxJson.Font = New System.Drawing.Font("Microsoft Tai Le", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AstSyntaxJson.Location = New System.Drawing.Point(3, 3)
        Me.AstSyntaxJson.Name = "AstSyntaxJson"
        Me.AstSyntaxJson.Size = New System.Drawing.Size(502, 140)
        Me.AstSyntaxJson.TabIndex = 0
        Me.AstSyntaxJson.Text = ""
        '
        'Small_PL_AstTreeView
        '
        Me.Small_PL_AstTreeView.BackColor = System.Drawing.SystemColors.InfoText
        Me.Small_PL_AstTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Small_PL_AstTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Small_PL_AstTreeView.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Small_PL_AstTreeView.ForeColor = System.Drawing.Color.MintCream
        Me.Small_PL_AstTreeView.HotTracking = True
        Me.Small_PL_AstTreeView.Location = New System.Drawing.Point(0, 0)
        Me.Small_PL_AstTreeView.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Small_PL_AstTreeView.Name = "Small_PL_AstTreeView"
        Me.Small_PL_AstTreeView.ShowNodeToolTips = True
        Me.Small_PL_AstTreeView.Size = New System.Drawing.Size(516, 190)
        Me.Small_PL_AstTreeView.TabIndex = 1
        '
        'TabControlInputCode
        '
        Me.TabControlInputCode.Controls.Add(Me.Basic)
        Me.TabControlInputCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlInputCode.Location = New System.Drawing.Point(0, 52)
        Me.TabControlInputCode.Name = "TabControlInputCode"
        Me.TabControlInputCode.SelectedIndex = 0
        Me.TabControlInputCode.Size = New System.Drawing.Size(1247, 398)
        Me.TabControlInputCode.TabIndex = 1
        '
        'IDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Bar2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1247, 450)
        Me.Controls.Add(Me.TabControlInputCode)
        Me.Controls.Add(Me.ToolStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "IDE"
        Me.Text = "SpydazWeb Basic ZX21"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Basic.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControlOutput.ResumeLayout(False)
        Me.TabPage_IDE_RESULTS.ResumeLayout(False)
        Me.TabPage_IDE_COMPILER_ERRORS.ResumeLayout(False)
        Me.TabPageAstSyntax.ResumeLayout(False)
        Me.TabControlInputCode.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents Basic As Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents LineNumberor As LineNumbers.LineNumbering
    Friend WithEvents TabControlInputCode As Windows.Forms.TabControl
    Friend WithEvents CodeTextBox As SyntaxTextBox
    Friend WithEvents NewToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer2 As Windows.Forms.SplitContainer
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
    Friend WithEvents Small_PL_AstTreeView As Windows.Forms.TreeView
    Friend WithEvents TabControlOutput As Windows.Forms.TabControl
    Friend WithEvents TabPage_IDE_RESULTS As Windows.Forms.TabPage
    Friend WithEvents DISPLAY_OUT As Windows.Forms.RichTextBox
    Friend WithEvents TabPage_IDE_COMPILER_ERRORS As Windows.Forms.TabPage
    Friend WithEvents CompilerErrors As Windows.Forms.RichTextBox
    Friend WithEvents TabPageAstSyntax As Windows.Forms.TabPage
    Friend WithEvents AstSyntaxJson As Windows.Forms.RichTextBox
End Class

Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CompilerPropertys
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompilerPropertys))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ButtonCompileEXE = New System.Windows.Forms.Button()
        Me.ButtonCompileDLL = New System.Windows.Forms.Button()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxStartFunctionName = New System.Windows.Forms.TextBox()
        Me.TextBoxMainClassName = New System.Windows.Forms.TextBox()
        Me.TextBoxFolderName = New System.Windows.Forms.TextBox()
        Me.TextBoxAssemblyName = New System.Windows.Forms.TextBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
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
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolsEmbeddedfiles.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ToolsRefferences.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.Black
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonCompileEXE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonCompileDLL)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ButtonBrowse)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxStartFunctionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxMainClassName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxFolderName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxAssemblyName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1527, 645)
        Me.SplitContainer1.SplitterDistance = 824
        Me.SplitContainer1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Black
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MintCream
        Me.Label5.Location = New System.Drawing.Point(231, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(593, 130)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'ButtonCompileEXE
        '
        Me.ButtonCompileEXE.BackColor = System.Drawing.Color.Navy
        Me.ButtonCompileEXE.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCompileEXE.ForeColor = System.Drawing.Color.Snow
        Me.ButtonCompileEXE.Location = New System.Drawing.Point(637, 178)
        Me.ButtonCompileEXE.Name = "ButtonCompileEXE"
        Me.ButtonCompileEXE.Size = New System.Drawing.Size(171, 38)
        Me.ButtonCompileEXE.TabIndex = 16
        Me.ButtonCompileEXE.Text = "Compile ExE"
        Me.ButtonCompileEXE.UseVisualStyleBackColor = False
        '
        'ButtonCompileDLL
        '
        Me.ButtonCompileDLL.BackColor = System.Drawing.Color.Navy
        Me.ButtonCompileDLL.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCompileDLL.ForeColor = System.Drawing.Color.Snow
        Me.ButtonCompileDLL.Location = New System.Drawing.Point(460, 178)
        Me.ButtonCompileDLL.Name = "ButtonCompileDLL"
        Me.ButtonCompileDLL.Size = New System.Drawing.Size(171, 38)
        Me.ButtonCompileDLL.TabIndex = 17
        Me.ButtonCompileDLL.Text = "Compile DLL"
        Me.ButtonCompileDLL.UseVisualStyleBackColor = False
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonBrowse.Location = New System.Drawing.Point(700, 355)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(107, 41)
        Me.ButtonBrowse.TabIndex = 15
        Me.ButtonBrowse.Text = "Browse"
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Comic Sans MS", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Cyan
        Me.Label4.Location = New System.Drawing.Point(19, 513)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(260, 34)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Start Function Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Comic Sans MS", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Cyan
        Me.Label3.Location = New System.Drawing.Point(19, 411)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(213, 34)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Main Class Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Comic Sans MS", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Cyan
        Me.Label2.Location = New System.Drawing.Point(19, 309)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(164, 34)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Folder Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Comic Sans MS", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Cyan
        Me.Label1.Location = New System.Drawing.Point(19, 207)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 34)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Assembly Name"
        '
        'TextBoxStartFunctionName
        '
        Me.TextBoxStartFunctionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStartFunctionName.Font = New System.Drawing.Font("Microsoft Tai Le", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxStartFunctionName.Location = New System.Drawing.Point(25, 550)
        Me.TextBoxStartFunctionName.Name = "TextBoxStartFunctionName"
        Me.TextBoxStartFunctionName.Size = New System.Drawing.Size(782, 34)
        Me.TextBoxStartFunctionName.TabIndex = 7
        Me.TextBoxStartFunctionName.Text = "Main"
        '
        'TextBoxMainClassName
        '
        Me.TextBoxMainClassName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMainClassName.Font = New System.Drawing.Font("Microsoft Tai Le", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMainClassName.Location = New System.Drawing.Point(25, 448)
        Me.TextBoxMainClassName.Name = "TextBoxMainClassName"
        Me.TextBoxMainClassName.Size = New System.Drawing.Size(782, 34)
        Me.TextBoxMainClassName.TabIndex = 8
        Me.TextBoxMainClassName.Text = "Program"
        '
        'TextBoxFolderName
        '
        Me.TextBoxFolderName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFolderName.Font = New System.Drawing.Font("Microsoft Tai Le", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxFolderName.Location = New System.Drawing.Point(25, 355)
        Me.TextBoxFolderName.Name = "TextBoxFolderName"
        Me.TextBoxFolderName.Size = New System.Drawing.Size(642, 34)
        Me.TextBoxFolderName.TabIndex = 9
        '
        'TextBoxAssemblyName
        '
        Me.TextBoxAssemblyName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAssemblyName.Font = New System.Drawing.Font("Microsoft Tai Le", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxAssemblyName.Location = New System.Drawing.Point(25, 244)
        Me.TextBoxAssemblyName.Name = "TextBoxAssemblyName"
        Me.TextBoxAssemblyName.Size = New System.Drawing.Size(782, 34)
        Me.TextBoxAssemblyName.TabIndex = 10
        Me.TextBoxAssemblyName.Text = "Program"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ToolsEmbeddedfiles)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ToolsRefferences)
        Me.SplitContainer2.Size = New System.Drawing.Size(699, 645)
        Me.SplitContainer2.SplitterDistance = 340
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = CType(resources.GetObject("GroupBox1.BackgroundImage"), System.Drawing.Image)
        Me.GroupBox1.Controls.Add(Me.EmbeddedFilePaths)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 590)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'EmbeddedFilePaths
        '
        Me.EmbeddedFilePaths.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.EmbeddedFilePaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EmbeddedFilePaths.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmbeddedFilePaths.FormattingEnabled = True
        Me.EmbeddedFilePaths.ItemHeight = 22
        Me.EmbeddedFilePaths.Location = New System.Drawing.Point(3, 16)
        Me.EmbeddedFilePaths.Name = "EmbeddedFilePaths"
        Me.EmbeddedFilePaths.Size = New System.Drawing.Size(334, 571)
        Me.EmbeddedFilePaths.TabIndex = 0
        '
        'ToolsEmbeddedfiles
        '
        Me.ToolsEmbeddedfiles.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ToolsEmbeddedfiles.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolsEmbeddedfiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddEmbedded, Me.RemoveEmbedded})
        Me.ToolsEmbeddedfiles.Location = New System.Drawing.Point(0, 0)
        Me.ToolsEmbeddedfiles.Name = "ToolsEmbeddedfiles"
        Me.ToolsEmbeddedfiles.Size = New System.Drawing.Size(340, 55)
        Me.ToolsEmbeddedfiles.TabIndex = 0
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
        Me.GroupBox2.Location = New System.Drawing.Point(0, 55)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(355, 590)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " "
        '
        'RefferenceFilePaths
        '
        Me.RefferenceFilePaths.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.RefferenceFilePaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RefferenceFilePaths.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefferenceFilePaths.FormattingEnabled = True
        Me.RefferenceFilePaths.ItemHeight = 22
        Me.RefferenceFilePaths.Location = New System.Drawing.Point(3, 16)
        Me.RefferenceFilePaths.Name = "RefferenceFilePaths"
        Me.RefferenceFilePaths.Size = New System.Drawing.Size(349, 571)
        Me.RefferenceFilePaths.TabIndex = 0
        '
        'ToolsRefferences
        '
        Me.ToolsRefferences.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ToolsRefferences.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolsRefferences.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddRefferences, Me.RemoveRefference})
        Me.ToolsRefferences.Location = New System.Drawing.Point(0, 0)
        Me.ToolsRefferences.Name = "ToolsRefferences"
        Me.ToolsRefferences.Size = New System.Drawing.Size(355, 55)
        Me.ToolsRefferences.TabIndex = 0
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
        'CompilerPropertys
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1527, 645)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CompilerPropertys"
        Me.Text = "CompilerPropertys"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ToolsEmbeddedfiles.ResumeLayout(False)
        Me.ToolsEmbeddedfiles.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ToolsRefferences.ResumeLayout(False)
        Me.ToolsRefferences.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ButtonCompileEXE As Button
    Friend WithEvents ButtonCompileDLL As Button
    Friend WithEvents ButtonBrowse As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxStartFunctionName As TextBox
    Friend WithEvents TextBoxMainClassName As TextBox
    Friend WithEvents TextBoxFolderName As TextBox
    Friend WithEvents TextBoxAssemblyName As TextBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents ToolsEmbeddedfiles As ToolStrip
    Friend WithEvents AddEmbedded As ToolStripButton
    Friend WithEvents RemoveEmbedded As ToolStripButton
    Friend WithEvents ToolsRefferences As ToolStrip
    Friend WithEvents AddRefferences As ToolStripButton
    Friend WithEvents RemoveRefference As ToolStripButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents EmbeddedFilePaths As ListBox
    Friend WithEvents RefferenceFilePaths As ListBox
    Friend WithEvents Label5 As Label
End Class

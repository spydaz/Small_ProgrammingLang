<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SyntaxViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SyntaxViewer))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DISPLAY_TREE = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.CodeDisplay = New System.Windows.Forms.RichTextBox()
        Me.ButtonClearTree = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DISPLAY_TREE)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(625, 774)
        Me.SplitContainer1.SplitterDistance = 336
        Me.SplitContainer1.TabIndex = 0
        '
        'DISPLAY_TREE
        '
        Me.DISPLAY_TREE.BackColor = System.Drawing.Color.Linen
        Me.DISPLAY_TREE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DISPLAY_TREE.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DISPLAY_TREE.ForeColor = System.Drawing.Color.Maroon
        Me.DISPLAY_TREE.ImageIndex = 0
        Me.DISPLAY_TREE.ImageList = Me.ImageList1
        Me.DISPLAY_TREE.Location = New System.Drawing.Point(0, 0)
        Me.DISPLAY_TREE.Name = "DISPLAY_TREE"
        Me.DISPLAY_TREE.SelectedImageIndex = 0
        Me.DISPLAY_TREE.Size = New System.Drawing.Size(625, 336)
        Me.DISPLAY_TREE.TabIndex = 0
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
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.CodeDisplay)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.ButtonClearTree)
        Me.SplitContainer2.Size = New System.Drawing.Size(625, 434)
        Me.SplitContainer2.SplitterDistance = 370
        Me.SplitContainer2.TabIndex = 0
        '
        'CodeDisplay
        '
        Me.CodeDisplay.BackColor = System.Drawing.SystemColors.Info
        Me.CodeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CodeDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodeDisplay.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeDisplay.Location = New System.Drawing.Point(0, 0)
        Me.CodeDisplay.Name = "CodeDisplay"
        Me.CodeDisplay.Size = New System.Drawing.Size(625, 370)
        Me.CodeDisplay.TabIndex = 1
        Me.CodeDisplay.Text = ""
        '
        'ButtonClearTree
        '
        Me.ButtonClearTree.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClearTree.Location = New System.Drawing.Point(12, 3)
        Me.ButtonClearTree.Name = "ButtonClearTree"
        Me.ButtonClearTree.Size = New System.Drawing.Size(122, 45)
        Me.ButtonClearTree.TabIndex = 0
        Me.ButtonClearTree.Text = "Clear"
        Me.ButtonClearTree.UseVisualStyleBackColor = True
        '
        'SyntaxViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Bar2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(625, 774)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SyntaxViewer"
        Me.Text = "SyntaxViewer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As Windows.Forms.SplitContainer
    Friend WithEvents DISPLAY_TREE As Windows.Forms.TreeView
    Friend WithEvents SplitContainer2 As Windows.Forms.SplitContainer
    Friend WithEvents ImageList1 As Windows.Forms.ImageList
    Friend WithEvents CodeDisplay As Windows.Forms.RichTextBox
    Friend WithEvents ButtonClearTree As Windows.Forms.Button
End Class

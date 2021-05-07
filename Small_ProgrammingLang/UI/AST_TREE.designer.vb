<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AST_TREE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AST_TREE))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonbOTH = New System.Windows.Forms.Button()
        Me.ButtonParseAST = New System.Windows.Forms.Button()
        Me.ButtonParseTokens = New System.Windows.Forms.Button()
        Me.InputText = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImage = Global.SDK.My.Resources.Resources.Console_A
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.ButtonbOTH)
        Me.GroupBox1.Controls.Add(Me.ButtonParseAST)
        Me.GroupBox1.Controls.Add(Me.ButtonParseTokens)
        Me.GroupBox1.Controls.Add(Me.InputText)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox1.Size = New System.Drawing.Size(1276, 530)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "AST Development"
        '
        'ButtonbOTH
        '
        Me.ButtonbOTH.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonbOTH.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButtonbOTH.Location = New System.Drawing.Point(505, 476)
        Me.ButtonbOTH.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonbOTH.Name = "ButtonbOTH"
        Me.ButtonbOTH.Size = New System.Drawing.Size(130, 33)
        Me.ButtonbOTH.TabIndex = 4
        Me.ButtonbOTH.Text = "PARSE BOTH"
        Me.ButtonbOTH.UseVisualStyleBackColor = False
        '
        'ButtonParseAST
        '
        Me.ButtonParseAST.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonParseAST.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButtonParseAST.Location = New System.Drawing.Point(372, 476)
        Me.ButtonParseAST.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonParseAST.Name = "ButtonParseAST"
        Me.ButtonParseAST.Size = New System.Drawing.Size(130, 33)
        Me.ButtonParseAST.TabIndex = 3
        Me.ButtonParseAST.Text = "PARSE AST"
        Me.ButtonParseAST.UseVisualStyleBackColor = False
        '
        'ButtonParseTokens
        '
        Me.ButtonParseTokens.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ButtonParseTokens.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButtonParseTokens.Location = New System.Drawing.Point(238, 476)
        Me.ButtonParseTokens.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonParseTokens.Name = "ButtonParseTokens"
        Me.ButtonParseTokens.Size = New System.Drawing.Size(130, 33)
        Me.ButtonParseTokens.TabIndex = 2
        Me.ButtonParseTokens.Text = "PARSE Tokens"
        Me.ButtonParseTokens.UseVisualStyleBackColor = False
        '
        'InputText
        '
        Me.InputText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.InputText.Location = New System.Drawing.Point(160, 48)
        Me.InputText.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.InputText.Name = "InputText"
        Me.InputText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.InputText.Size = New System.Drawing.Size(949, 376)
        Me.InputText.TabIndex = 1
        Me.InputText.Tag = "Syntax Tree (Json)"
        Me.InputText.Text = ""
        '
        'AST_TREE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1276, 530)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "AST_TREE"
        Me.Text = "SpydazWeb ZX81 BASIC to Abstract Syntax Tree"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents InputText As RichTextBox
    Friend WithEvents ButtonbOTH As Button
    Friend WithEvents ButtonParseAST As Button
    Friend WithEvents ButtonParseTokens As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VM_MachineUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VM_MachineUI))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RichTextBoxCodeEntry = New System.Windows.Forms.RichTextBox()
        Me.ButtonRunCode = New System.Windows.Forms.Button()
        Me.RichTextBoxInfo = New System.Windows.Forms.RichTextBox()
        Me.ButtonClrScrn = New System.Windows.Forms.Button()
        Me.ButtonNewScrn = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RichTextBoxCodeEntry)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 562)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(932, 168)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Enter Machine Code "
        '
        'RichTextBoxCodeEntry
        '
        Me.RichTextBoxCodeEntry.BackColor = System.Drawing.SystemColors.Info
        Me.RichTextBoxCodeEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxCodeEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.1!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxCodeEntry.Location = New System.Drawing.Point(4, 26)
        Me.RichTextBoxCodeEntry.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RichTextBoxCodeEntry.Name = "RichTextBoxCodeEntry"
        Me.RichTextBoxCodeEntry.Size = New System.Drawing.Size(924, 137)
        Me.RichTextBoxCodeEntry.TabIndex = 0
        Me.RichTextBoxCodeEntry.Text = ""
        '
        'ButtonRunCode
        '
        Me.ButtonRunCode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonRunCode.BackColor = System.Drawing.Color.DimGray
        Me.ButtonRunCode.Location = New System.Drawing.Point(828, 522)
        Me.ButtonRunCode.Name = "ButtonRunCode"
        Me.ButtonRunCode.Size = New System.Drawing.Size(80, 32)
        Me.ButtonRunCode.TabIndex = 1
        Me.ButtonRunCode.Text = "Run Code"
        Me.ButtonRunCode.UseVisualStyleBackColor = False
        '
        'RichTextBoxInfo
        '
        Me.RichTextBoxInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxInfo.Location = New System.Drawing.Point(12, 27)
        Me.RichTextBoxInfo.Name = "RichTextBoxInfo"
        Me.RichTextBoxInfo.Size = New System.Drawing.Size(908, 445)
        Me.RichTextBoxInfo.TabIndex = 2
        Me.RichTextBoxInfo.Text = ""
        '
        'ButtonClrScrn
        '
        Me.ButtonClrScrn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonClrScrn.BackColor = System.Drawing.Color.DimGray
        Me.ButtonClrScrn.Location = New System.Drawing.Point(630, 522)
        Me.ButtonClrScrn.Name = "ButtonClrScrn"
        Me.ButtonClrScrn.Size = New System.Drawing.Size(89, 32)
        Me.ButtonClrScrn.TabIndex = 1
        Me.ButtonClrScrn.Text = "Clear Scrn"
        Me.ButtonClrScrn.UseVisualStyleBackColor = False
        '
        'ButtonNewScrn
        '
        Me.ButtonNewScrn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonNewScrn.BackColor = System.Drawing.Color.DimGray
        Me.ButtonNewScrn.Location = New System.Drawing.Point(725, 522)
        Me.ButtonNewScrn.Name = "ButtonNewScrn"
        Me.ButtonNewScrn.Size = New System.Drawing.Size(97, 32)
        Me.ButtonNewScrn.TabIndex = 1
        Me.ButtonNewScrn.Text = "New Scrn"
        Me.ButtonNewScrn.UseVisualStyleBackColor = False
        '
        'X86_MACHINE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.SAL_VM.My.Resources.Resources.Dell_UltraSharp_27
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(932, 730)
        Me.Controls.Add(Me.RichTextBoxInfo)
        Me.Controls.Add(Me.ButtonNewScrn)
        Me.Controls.Add(Me.ButtonClrScrn)
        Me.Controls.Add(Me.ButtonRunCode)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Comic Sans MS", 11.1!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "X86_MACHINE"
        Me.Text = "X86_MACHINE_CODE"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RichTextBoxCodeEntry As RichTextBox
    Friend WithEvents ButtonRunCode As Button
    Friend WithEvents RichTextBoxInfo As RichTextBox
    Friend WithEvents ButtonClrScrn As Button
    Friend WithEvents ButtonNewScrn As Button
End Class

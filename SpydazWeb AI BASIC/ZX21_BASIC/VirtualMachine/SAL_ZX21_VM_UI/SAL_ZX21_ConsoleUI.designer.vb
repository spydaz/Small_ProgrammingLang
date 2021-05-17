Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SAL_ZX21_ConsoleUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SAL_ZX21_ConsoleUI))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RichTextBoxCodeEntry = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonNewScrn = New System.Windows.Forms.Button()
        Me.ButtonClrScrn = New System.Windows.Forms.Button()
        Me.ButtonRunCode = New System.Windows.Forms.Button()
        Me.RichTextBoxInfo = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RichTextBoxCodeEntry)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(0, 593)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(857, 168)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Enter Machine Code "
        '
        'RichTextBoxCodeEntry
        '
        Me.RichTextBoxCodeEntry.BackColor = System.Drawing.Color.White
        Me.RichTextBoxCodeEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxCodeEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.1!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxCodeEntry.Location = New System.Drawing.Point(4, 26)
        Me.RichTextBoxCodeEntry.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RichTextBoxCodeEntry.Name = "RichTextBoxCodeEntry"
        Me.RichTextBoxCodeEntry.Size = New System.Drawing.Size(849, 137)
        Me.RichTextBoxCodeEntry.TabIndex = 0
        Me.RichTextBoxCodeEntry.Text = ""
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Dell_UltraSharp_27
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.ButtonNewScrn)
        Me.Panel1.Controls.Add(Me.ButtonClrScrn)
        Me.Panel1.Controls.Add(Me.ButtonRunCode)
        Me.Panel1.Controls.Add(Me.RichTextBoxInfo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(857, 593)
        Me.Panel1.TabIndex = 2
        '
        'ButtonNewScrn
        '
        Me.ButtonNewScrn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonNewScrn.BackColor = System.Drawing.Color.DimGray
        Me.ButtonNewScrn.Location = New System.Drawing.Point(662, 453)
        Me.ButtonNewScrn.Name = "ButtonNewScrn"
        Me.ButtonNewScrn.Size = New System.Drawing.Size(97, 32)
        Me.ButtonNewScrn.TabIndex = 4
        Me.ButtonNewScrn.Text = "New Scrn"
        Me.ButtonNewScrn.UseVisualStyleBackColor = False
        '
        'ButtonClrScrn
        '
        Me.ButtonClrScrn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonClrScrn.BackColor = System.Drawing.Color.DimGray
        Me.ButtonClrScrn.Location = New System.Drawing.Point(567, 453)
        Me.ButtonClrScrn.Name = "ButtonClrScrn"
        Me.ButtonClrScrn.Size = New System.Drawing.Size(89, 32)
        Me.ButtonClrScrn.TabIndex = 5
        Me.ButtonClrScrn.Text = "Clear Scrn"
        Me.ButtonClrScrn.UseVisualStyleBackColor = False
        '
        'ButtonRunCode
        '
        Me.ButtonRunCode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonRunCode.BackColor = System.Drawing.Color.DimGray
        Me.ButtonRunCode.Location = New System.Drawing.Point(765, 453)
        Me.ButtonRunCode.Name = "ButtonRunCode"
        Me.ButtonRunCode.Size = New System.Drawing.Size(80, 32)
        Me.ButtonRunCode.TabIndex = 6
        Me.ButtonRunCode.Text = "Run Code"
        Me.ButtonRunCode.UseVisualStyleBackColor = False
        '
        'RichTextBoxInfo
        '
        Me.RichTextBoxInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxInfo.Location = New System.Drawing.Point(12, 12)
        Me.RichTextBoxInfo.Name = "RichTextBoxInfo"
        Me.RichTextBoxInfo.Size = New System.Drawing.Size(833, 412)
        Me.RichTextBoxInfo.TabIndex = 3
        Me.RichTextBoxInfo.Text = "SpydazWeb AI ZX21 Copyright © 2021" & Global.Microsoft.VisualBasic.ChrW(10) & "Ready." & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'SAL_ZX21_ConsoleUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(857, 761)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Comic Sans MS", 11.1!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "SAL_ZX21_ConsoleUI"
        Me.Text = "SpydazWeb ZX21 Assembler"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RichTextBoxCodeEntry As RichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonNewScrn As Button
    Friend WithEvents ButtonClrScrn As Button
    Friend WithEvents ButtonRunCode As Button
    Friend WithEvents RichTextBoxInfo As RichTextBox
End Class

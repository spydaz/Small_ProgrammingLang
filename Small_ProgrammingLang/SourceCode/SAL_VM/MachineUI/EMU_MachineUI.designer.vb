<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMU_MachineUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EMU_MachineUI))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonRunCode = New System.Windows.Forms.Button()
        Me.ButtonNewScrn = New System.Windows.Forms.Button()
        Me.ComboBoxURL = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BrowserMain = New System.Windows.Forms.WebBrowser()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonRunCode)
        Me.GroupBox1.Controls.Add(Me.ButtonNewScrn)
        Me.GroupBox1.Controls.Add(Me.ComboBoxURL)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 508)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(731, 67)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Online System"
        '
        'ButtonRunCode
        '
        Me.ButtonRunCode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonRunCode.BackColor = System.Drawing.Color.DimGray
        Me.ButtonRunCode.Location = New System.Drawing.Point(402, 22)
        Me.ButtonRunCode.Name = "ButtonRunCode"
        Me.ButtonRunCode.Size = New System.Drawing.Size(105, 34)
        Me.ButtonRunCode.TabIndex = 3
        Me.ButtonRunCode.Tag = "Opens in Current Window"
        Me.ButtonRunCode.Text = "Run System"
        Me.ButtonRunCode.UseVisualStyleBackColor = False
        '
        'ButtonNewScrn
        '
        Me.ButtonNewScrn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ButtonNewScrn.BackColor = System.Drawing.Color.DimGray
        Me.ButtonNewScrn.Location = New System.Drawing.Point(292, 21)
        Me.ButtonNewScrn.Name = "ButtonNewScrn"
        Me.ButtonNewScrn.Size = New System.Drawing.Size(104, 35)
        Me.ButtonNewScrn.TabIndex = 2
        Me.ButtonNewScrn.Tag = "Opens in New Window"
        Me.ButtonNewScrn.Text = "New Scrn"
        Me.ButtonNewScrn.UseVisualStyleBackColor = False
        '
        'ComboBoxURL
        '
        Me.ComboBoxURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxURL.FormattingEnabled = True
        Me.ComboBoxURL.Items.AddRange(New Object() {"Amiga Workbench Simulator", "Windows 3.1", "Windows 3.1 with CD-ROM", "Macintosh System 7", "Windows 95", "OS/2", "Windows 93", "Atari ST", "Windows 1.0", "EmuOS", "Mac Oxs Lion 10", "PCDOS 5", "Mac os7", "Mermaid diagrams", "AST Explorer", "Visual basic", "Nearly Parser", "Code Script Prettier", ".NET"})
        Me.ComboBoxURL.Location = New System.Drawing.Point(7, 28)
        Me.ComboBoxURL.Name = "ComboBoxURL"
        Me.ComboBoxURL.Size = New System.Drawing.Size(261, 28)
        Me.ComboBoxURL.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.BrowserMain)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(712, 400)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'BrowserMain
        '
        Me.BrowserMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BrowserMain.Location = New System.Drawing.Point(3, 24)
        Me.BrowserMain.MinimumSize = New System.Drawing.Size(20, 20)
        Me.BrowserMain.Name = "BrowserMain"
        Me.BrowserMain.ScriptErrorsSuppressed = True
        Me.BrowserMain.Size = New System.Drawing.Size(706, 373)
        Me.BrowserMain.TabIndex = 0
        Me.BrowserMain.Url = New System.Uri("HTTP://www.spydazweb.co.uk", System.UriKind.Absolute)
        '
        'EMU_MachineUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.SDK.My.Resources.Resources.Dell_UltraSharp_27
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(731, 575)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Comic Sans MS", 11.1!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Transparent
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "EMU_MachineUI"
        Me.Text = "Online System"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ButtonRunCode As Button
    Friend WithEvents ButtonNewScrn As Button
    Friend WithEvents ComboBoxURL As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BrowserMain As WebBrowser
End Class

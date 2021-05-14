Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SAL_ZX21_GDU
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GDU_DISPLAY = New AI_BASIC.GDU()
        Me.GroupBox6.SuspendLayout()
        CType(Me.GDU_DISPLAY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.BackgroundImage = Global.AI_BASIC.My.Resources.Resources.Dell_UltraSharp_27
        Me.GroupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox6.Controls.Add(Me.GDU_DISPLAY)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox6.Size = New System.Drawing.Size(998, 698)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        '
        'GDU_DISPLAY
        '
        Me.GDU_DISPLAY.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GDU_DISPLAY.Current_X_Position = 0
        Me.GDU_DISPLAY.Current_Y_Position = 0
        Me.GDU_DISPLAY.Direction = 0
        Me.GDU_DISPLAY.Location = New System.Drawing.Point(12, 20)
        Me.GDU_DISPLAY.Name = "GDU_DISPLAY"
        Me.GDU_DISPLAY.Size = New System.Drawing.Size(980, 485)
        Me.GDU_DISPLAY.TabIndex = 0
        Me.GDU_DISPLAY.TabStop = False
        Me.GDU_DISPLAY.TurtleColor = System.Drawing.Color.Black
        Me.GDU_DISPLAY.TurtlePenSize = 3
        Me.GDU_DISPLAY.TurtlePenUP_DWN = False
        Me.GDU_DISPLAY.TurtlePosition = New System.Drawing.Point(0, 0)
        Me.GDU_DISPLAY.TurtleShow = False
        '
        'SAL_ZX21_GDU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 698)
        Me.Controls.Add(Me.GroupBox6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "SAL_ZX21_GDU"
        Me.Text = "S.A.L 2021© LogoGraphics"
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.GDU_DISPLAY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GDU_DISPLAY As GDU
End Class

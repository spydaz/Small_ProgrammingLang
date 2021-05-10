<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplayGraphics
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
        Me.GDU_DISPLAY = New SDK.GDU()
        CType(Me.GDU_DISPLAY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GDU_DISPLAY
        '
        Me.GDU_DISPLAY.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GDU_DISPLAY.Current_X_Position = 0
        Me.GDU_DISPLAY.Current_Y_Position = 0
        Me.GDU_DISPLAY.Direction = 0
        Me.GDU_DISPLAY.Location = New System.Drawing.Point(12, 12)
        Me.GDU_DISPLAY.Name = "GDU_DISPLAY"
        Me.GDU_DISPLAY.Size = New System.Drawing.Size(776, 306)
        Me.GDU_DISPLAY.TabIndex = 0
        Me.GDU_DISPLAY.TabStop = False
        Me.GDU_DISPLAY.TurtleColor = System.Drawing.Color.Black
        Me.GDU_DISPLAY.TurtlePenSize = 3
        Me.GDU_DISPLAY.TurtlePenUP_DWN = False
        Me.GDU_DISPLAY.TurtlePosition = New System.Drawing.Point(0, 0)
        Me.GDU_DISPLAY.TurtleShow = False
        '
        'FormDisplayGraphics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SDK.My.Resources.Resources.Dell_UltraSharp_27
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GDU_DISPLAY)
        Me.DoubleBuffered = True
        Me.Name = "FormDisplayGraphics"
        Me.Text = "FormDisplayGraphics"
        CType(Me.GDU_DISPLAY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GDU_DISPLAY As GDU
End Class

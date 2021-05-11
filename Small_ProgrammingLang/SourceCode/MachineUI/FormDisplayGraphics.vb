Imports SAL_VM.STACK_VM.ZX81_GPU

Public Class FormDisplayGraphics
    Public Sub Decode(ByRef Cmd As LogoCmd)
        Me.GDU_DISPLAY.Decode(Cmd)
    End Sub
End Class
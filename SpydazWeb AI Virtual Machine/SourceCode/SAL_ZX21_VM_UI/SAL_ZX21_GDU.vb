Imports SAL_VM.STACK_VM.ZX81_GPU

Public Class SAL_ZX21_GDU

    ''' <summary>
    '''fd – forward
    '''bk – backward
    '''rt – right
    '''lt – left
    '''cs – clearscreen
    '''pu − penup
    '''pd − pendown
    '''ht − hideturtle
    '''st − showturtle
    ''' </summary>
    ''' <param name="Cmd"></param>
    Public Sub Decode(ByRef Cmd As LogoCmd)
        Me.GDU_DISPLAY.Decode(Cmd)
    End Sub




End Class
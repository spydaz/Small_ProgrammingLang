Namespace VirtualMachine
    Public Class ZX81_GPU
        Private iMonitorConsole As SAL_ZX21_VDU
        Private IMonitorLogoGraphics As SAL_ZX21_GDU

        Public Sub New()
            iMonitorConsole = New SAL_ZX21_VDU
            IMonitorLogoGraphics = New SAL_ZX21_GDU
        End Sub

        Public Sub ConsolePrint(ByRef Str As String)
            If iMonitorConsole.Visible = False Then
                iMonitorConsole.Show()
            Else
            End If
            iMonitorConsole.Print(Str)
        End Sub
        Public Sub Console_CLS()
            If iMonitorConsole.Visible Then
            Else
                iMonitorConsole.Show()
            End If
            iMonitorConsole.CLS()
        End Sub


        Public Sub DecodeGDU_Cmd(ByRef Cmd As LogoCmd)
            If IMonitorLogoGraphics.Visible Then
            Else
                IMonitorLogoGraphics.Show()
            End If
            IMonitorLogoGraphics.Decode(Cmd)
        End Sub


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
        Public Structure LogoCmd
            Dim Cmd As String
            Dim Value As Integer
        End Structure
    End Class
End Namespace

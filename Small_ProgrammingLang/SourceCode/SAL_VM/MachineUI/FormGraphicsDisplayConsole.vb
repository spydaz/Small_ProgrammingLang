Imports SDK.SmallProgLang

Public Class FormGraphicsDisplayConsole
    Public Iturtle As TURTLE
    Private Sub FormGraphicsDisplayConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Iturtle = New TURTLE(DISPLAY_PANEL, My.Resources.Arrow_Right)
        Iturtle.SetPenWidth(2)
        Iturtle._Reset()
    End Sub
End Class
Imports SAL_VM.STACK_VM

Public Class SAL_ZX21_ConsoleUI

    Public Function ExecuteCode(ByRef Code As String) As String
        Return X86API.RunMachineCode(Code)
    End Function

    Private Sub ButtonRunCode_Click(sender As Object, e As EventArgs) Handles ButtonRunCode.Click
        Dim CodeAnalysis As String = RichTextBoxCodeEntry.Text
        RichTextBoxInfo.Text &= ExecuteCode(CodeAnalysis)
    End Sub

    Private Sub ButtonNewScrn_Click(sender As Object, e As EventArgs) Handles ButtonNewScrn.Click
        Dim frm As New SAL_ZX21_ConsoleUI
        frm.Show()
    End Sub

    Private Sub ButtonClrScrn_Click(sender As Object, e As EventArgs) Handles ButtonClrScrn.Click
        RichTextBoxInfo.Clear()
    End Sub
End Class
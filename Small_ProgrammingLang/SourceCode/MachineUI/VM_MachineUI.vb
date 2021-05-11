Imports SDK.SAL

Public Class VM_MachineUI

    Public Function ExecuteCode(ByRef Code As String) As String
        Return X86API.RunMachineCode(Code)
    End Function

    Private Sub ButtonRunCode_Click(sender As Object, e As EventArgs) Handles ButtonRunCode.Click
        Dim CodeAnalysis As String = RichTextBoxCodeEntry.Text
        RichTextBoxInfo.Text &= ExecuteCode(CodeAnalysis)
    End Sub

    Private Sub ButtonNewScrn_Click(sender As Object, e As EventArgs) Handles ButtonNewScrn.Click
        Dim frm As New VM_MachineUI
        frm.Show()
    End Sub

    Private Sub ButtonClrScrn_Click(sender As Object, e As EventArgs) Handles ButtonClrScrn.Click
        RichTextBoxInfo.Clear()
    End Sub
    Public Sub DISPLAY_TEXT(ByRef Str As String)
        RichTextBoxInfo.Clear()
        RichTextBoxInfo.Text = Str
    End Sub
    Private Sub ButtonRef_Click(sender As Object, e As EventArgs) Handles ButtonRef.Click
        Dim frm As New VM_MachineUI
        frm.DISPLAY_TEXT(My.Resources.QuickRef_SAL)
        frm.Show()
    End Sub

    Private Sub ButtonEMU_Click(sender As Object, e As EventArgs) Handles ButtonEMU.Click
        Dim frmU As New EMU_MachineUI
        frmU.Show()
    End Sub
End Class
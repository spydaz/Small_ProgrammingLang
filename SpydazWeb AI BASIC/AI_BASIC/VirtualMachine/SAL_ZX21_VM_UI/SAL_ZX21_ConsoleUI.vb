Imports System.Windows.Forms
Imports AI_BASIC.VirtualMachine

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




    Private Sub SAL_ZX21_ConsoleUI_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown


        Select Case e.KeyCode

            Case Keys.Enter

                Select Case UCase(RichTextBoxCodeEntry.Text)


                    Case "CLS"
                        RichTextBoxInfo.Clear()
                        RichTextBoxCodeEntry.Text = ""


                End Select
        End Select

    End Sub

End Class
Imports System.IO
Imports SDK.SAL
Imports SDK.SmallProgLang

Public Class Sal_Repl

#Region "SAL REPL"
    Private Sub ToolStripButtonCompileCode_Click(sender As Object, e As EventArgs) Handles SAL_ToolStripButtonCompileCode.Click
        Dim PROG() As String = Split(SAL_TextBoxCodeInput.Text.Replace(vbCrLf, " "), " ")
        SAL_RichTextBoxProgram.Text = PROG.ToJson
        Dim InstructionLst As New List(Of String)
        Dim ROOT As New TreeNode
        ROOT.Text = "ASSEMBLY_CODE"
        Dim Count As Integer = 0
        For Each item In PROG
            Count += 1
            If item <> "" Then
                Dim NDE As New TreeNode
                NDE.Text = Count & ": " & item
                ROOT.Nodes.Add(NDE)
                InstructionLst.Add(UCase(item))
            End If
        Next
        'cpu - START

        'Dim CPU As ZX81_CPU = New ZX81_CPU(InstructionLst)
        'CPU.Run()
        Dim CPU As ZX81_CPU = New ZX81_CPU("Test", InstructionLst)
        DisplayOutput("CURRENT POINTER = " & CPU.Get_Instruction_Pointer_Position & vbNewLine & "CONTAINED DATA = " & CPU.Peek)
        SAL_AST.Nodes.Add(ROOT)
    End Sub
    Public Sub DisplayOutput(ByRef OutputStr As String)
        SAL_RichTextBoxDisplayOutput.Text = OutputStr
    End Sub
    Public Sub DisplayError(ByRef ErrorStr As String)
        SAL_TextBoxErrorOutput.Text &= ErrorStr
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles SAL_NewToolStripButton.Click
        SAL_RichTextBoxDisplayOutput.Text = ""
        SAL_AST.Nodes.Clear()
        SAL_TextBoxCodeInput.Text = ""
    End Sub
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles SAL_OpenToolStripButton.Click
        Dim sr As StreamReader

        'Supposing you haven't already set these properties...
        With OpenTextFileDialog
            .FileName = ""
            .Title = "Open a Program file..."
            .InitialDirectory = "C:\"
            .Filter = " Program Files|*.*"
        End With

        If OpenTextFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                sr = New StreamReader(OpenTextFileDialog.FileName)
                SAL_TextBoxCodeInput.Text = OpenTextFileDialog.FileName
            Catch ex As Exception
                SAL_TextBoxErrorOutput.Text = "The file specified could not be opened." & vbNewLine & "Error message:" & vbNewLine & vbNewLine & ex.Message & vbNewLine & " File Could Not Be Opened!"
            End Try
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SAL_SaveToolStripButton.Click
        Dim sr As StreamWriter

        'Supposing you haven't already set these properties...
        With SaveTextFileDialog
            .FileName = ""
            .Title = "Save a Program file..."
            .InitialDirectory = "C:\"
            .Filter = " Program Files|*.*"
        End With

        If SaveTextFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                sr = New StreamWriter(SaveTextFileDialog.FileName)

            Catch ex As Exception
                SAL_TextBoxErrorOutput.Text = "The file specified could not be opened." & vbNewLine & "Error message:" & vbNewLine & ex.Message & "File Could Not Be Opened!"
            End Try
        End If
    End Sub

    Private Sub HelpToolStripButton_Click(sender As Object, e As EventArgs) Handles SAL_HelpToolStripButton.Click
        Dim help As New Process
        help.StartInfo.UseShellExecute = True
        help.StartInfo.FileName = "c:\windows\hh.exe"
        help.StartInfo.Arguments = Application.StartupPath & "\help\LexParseEval.chm"
        help.Start()
    End Sub
    Public VM As VM_MachineUI

    Private Sub ButtonOpenVM_Click(sender As Object, e As EventArgs) Handles SAL_ButtonOpenVM.Click
        VM = New VM_MachineUI
        VM.Show()
    End Sub

#End Region

End Class
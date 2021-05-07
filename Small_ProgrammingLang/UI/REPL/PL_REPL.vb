Imports System.IO
Imports System.Security.Cryptography
Imports SDK
Imports SDK.SmallProgLang
Imports SDK.SmallProgLang.Ast_ExpressionFactory
Imports SDK.SmallProgLang.Compiler

Public Class PL_REPL
#Region "SMALL_PL"
    Dim PSER As New Parser
    Private Sub ToolStripButtonCompile_Click(sender As Object, e As EventArgs) Handles Small_PL_ToolStripButtonCompileCode.Click
        Dim InputCode As String = Small_PL_TextBoxCodeInput.Text


        Dim outputStr = PSER.ParseFactory(InputCode)
        Small_PL_AstTreeView.Nodes.Clear()
        loadTree(outputStr)

        Small_PL_TextBoxREPL_OUTPUT.Text = FormatJsonOutput(outputStr.ToJson)
        Small_PL_TextboxErrors.Text = ""
        If PSER.ParserErrors IsNot Nothing Then
            If PSER.ParserErrors.Count > 0 Then
                Small_PL_TextboxErrors.Text = "Error in Syntax :" & vbNewLine
                For Each item In PSER.ParserErrors

                    Small_PL_TextboxErrors.Text &= vbNewLine & item & vbNewLine
                Next
                If outputStr.Body IsNot Nothing Then
                    For Each item In outputStr.Body
                        Small_PL_TextboxErrors.ForeColor = Color.Red

                        Small_PL_TextboxErrors.Text &= vbNewLine & item.ToJson & vbNewLine
                    Next
                Else
                End If
            Else
                Small_PL_TextboxErrors.ForeColor = Color.Green
                Small_PL_TextboxErrors.Text = "all Passed sucessfully" & vbNewLine
            End If
        End If

    End Sub

    Public Sub loadTree(ByRef Prog As AstProgram)
        Small_PL_AstTreeView.Nodes.Clear()
        Dim root As New TreeNode
        If PSER.ParserErrors.Count > 0 Then
            root.ForeColor = Color.Red
        Else
            root.ForeColor = Color.GreenYellow
        End If

        root.Text = Prog._TypeStr & vbNewLine
        root.Tag = FormatJsonOutput(Prog.ToJson)
        Dim Body As New TreeNode
        Body.Text = "Body"
        Body.Tag = FormatJsonOutput(Prog.ToJson)
        For Each item In Prog.Body
            Dim MainNode As New TreeNode
            MainNode.Text = FormatJsonOutput(item.ToJson)
            MainNode.Tag = FormatJsonOutput(item.ToJson)
            Dim RawNode As New TreeNode
            If PSER.ParserErrors.Count > 0 Then
                RawNode.ForeColor = Color.Red
            Else
                RawNode.ForeColor = Color.GreenYellow
            End If
            RawNode.Text = "_Raw :" & item._Raw
            RawNode.Tag = "_raw"
            MainNode.Nodes.Add(RawNode)
            Dim _StartNode As New TreeNode
            _StartNode.Text = "_Start :" & item._Start
            _StartNode.Tag = "_Start"
            MainNode.Nodes.Add(_StartNode)
            Dim _EndNode As New TreeNode
            _EndNode.Text = "_End :" & item._End
            _EndNode.Tag = "_End"
            MainNode.Nodes.Add(_EndNode)
            Dim _TypeNode As New TreeNode
            If PSER.ParserErrors.Count > 0 Then
                _TypeNode.ForeColor = Color.Red
            Else
                _TypeNode.ForeColor = Color.GreenYellow
            End If
            _TypeNode.Text = "_Type :" & item._TypeStr
            _TypeNode.Tag = "_Type"
            MainNode.Nodes.Add(_TypeNode)





            Body.Nodes.Add(MainNode)
        Next
        root.Nodes.Add(Body)
        Small_PL_AstTreeView.Nodes.Add(root)
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles Small_PL_OpenToolStripButton.Click
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
                Small_PL_TextBoxCodeInput.Text = OpenTextFileDialog.FileName
            Catch ex As Exception
                Small_PL_TextboxErrors.Text = "The file specified could not be opened." & vbNewLine & "Error message:" & vbNewLine & vbNewLine & ex.Message & vbNewLine & " File Could Not Be Opened!"
            End Try
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles Small_PL_SaveToolStripButton.Click
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
                Small_PL_TextboxErrors.Text = "The file specified could not be opened." & vbNewLine & "Error message:" & vbNewLine & ex.Message & "File Could Not Be Opened!"
            End Try
        End If
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles Small_PL_NewToolStripButton.Click
        Small_PL_TextBoxCodeInput.Text = ""
        Small_PL_TextBoxREPL_OUTPUT.Clear()
        Small_PL_TextBoxCodeInput.Clear()
        Small_PL_AstTreeView.Nodes.Clear()
    End Sub

    Private Sub HelpToolStripButton_Click(sender As Object, e As EventArgs) Handles Small_PL_HelpToolStripButton.Click
        Dim help As New Process
        help.StartInfo.UseShellExecute = True
        help.StartInfo.FileName = "c:\windows\hh.exe"
        help.StartInfo.Arguments = Application.StartupPath & "\help\LexParseEval.chm"
        help.Start()
    End Sub
    Public VM As VM_MachineUI

    Private Sub ButtonOpenVM_Click(sender As Object, e As EventArgs) Handles Small_PL_ButtonOpenVM.Click
        VM = New VM_MachineUI
        VM.Show()
    End Sub
#End Region

End Class
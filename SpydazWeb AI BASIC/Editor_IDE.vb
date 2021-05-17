Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Consoles.Interpretor
Imports AI_BASIC.Syntax

Public Class Editor_IDE


    Private Sub Small_PL_ToolStripButtonRunCode_Click(sender As Object, e As EventArgs) Handles Small_PL_ToolStripButtonRunCode.Click
        Dim ExpressionTree As SyntaxTree
        For Each line In Small_PL_TextBoxCodeInput.Lines
            ExpressionTree = SyntaxTree.Parse(Small_PL_TextBoxCodeInput.Text)
            AddCompiledTree(ExpressionTree)
            Small_PL_TextBoxREPL_OUTPUT.Text = ExpressionTree.ToJson

            Dim Eval As New Evaluator(ExpressionTree)
            Dim Result = Eval._Evaluate
            Small_PL_TextboxErrors.Text = vbNewLine & vbNewLine & vbNewLine &
                vbNewLine & "RESULTS RETUNED : " & Result & vbNewLine

        Next

    End Sub



    Private Sub CompileSpydazWebAI_BASIC_Click(sender As Object, e As EventArgs) Handles CompileSpydazWebAI_BASIC.Click
        Small_PL_TextBoxREPL_OUTPUT.Text = ""
        Dim Prog As String = Small_PL_TextBoxCodeInput.Text
        Dim MyCompiler As New Compiler(Prog)
        ClearTree()

        If MyCompiler.CompileProgram() = True Then
            If MyCompiler.GetCompilerDiagnostics.Length > 0 Then
                '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
                Small_PL_TextboxErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics
            Else
                For Each item In MyCompiler.ExpressionTrees
                    AddCompiledTree(item)

                Next
                For Each tree In MyCompiler.TokenTrees
                    For Each item In tree
                        Small_PL_TextBoxREPL_OUTPUT.Text &= vbNewLine & item.ToJson
                    Next
                Next
                Small_PL_TextboxErrors.Text = "Compiled Successfully"




                CompilerResutltsText.Text = MyCompiler.ExecuteProgram()
            End If

        Else
            '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
            Small_PL_TextboxErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics

        End If

    End Sub
    Public Sub ClearTree()
        Small_PL_AstTreeView.Nodes.Clear()
    End Sub
    Public Sub AddCompiledTree(ByRef Prog As SyntaxTree)

        '_:::ROOT:::_(0)
        Dim root As New TreeNode
        root.Text = Prog._ProgramScript
        root.Tag = Prog.ToJson
        For Each item In Prog.Body
            If item IsNot Nothing Then
                '_:::MainNodes:::_(0)
                Dim MainNode As New TreeNode
                MainNode.Text = item._SyntaxTypeStr
                MainNode.Tag = item.ToJson
                Dim SubNode As New TreeNode
                SubNode.Text = item.ToString
                SubNode.Tag = item.ToString
                MainNode.Nodes.Add(SubNode)
                root.Nodes.Add(MainNode)
            End If
        Next

        Small_PL_AstTreeView.Nodes.Add(root)
        Small_PL_AstTreeView.ExpandAll()
    End Sub

End Class
'REPL_ERROR SYSTEM
'
Namespace REPL
    Public Class PL_ReplErrorSystem
        ''' <summary>
        ''' Creates an Error Message to be displayed
        ''' </summary>
        ''' <param name="ErrorStr"></param>
        ''' <param name="Errtok"></param>
        ''' <returns></returns>
        Public Shared Function DisplayError(ByRef ErrorStr As String, ByRef Errtok As SyntaxToken) As String
            Dim str As String = ErrorStr & vbNewLine & Errtok.ToJson
            Return str
        End Function
    End Class
    Public Class SAL_ReplErrorSystem
        ''' <summary>
        ''' Creates an Error Message to be displayed
        ''' </summary>
        ''' <param name="ErrorStr"></param>
        ''' <param name="Errtok"></param>
        ''' <returns></returns>
        Public Shared Function DisplayError(ByRef ErrorStr As String, ByRef Errtok As SyntaxToken) As String
            Dim str As String = ErrorStr & vbNewLine & Errtok.ToJson
            Return str
        End Function
    End Class
End Namespace
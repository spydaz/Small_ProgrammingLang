Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Web
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Consoles.Compiler
Imports AI_BASIC.Consoles.Interpretor
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

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

        run()
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
    Public Function GetCurrentLine() As String
        '' Get current line index using char index of any selected text for that line

        Dim CurrentLineIndex = Small_PL_TextBoxCodeInput.GetLineFromCharIndex(Small_PL_TextBoxCodeInput.SelectionStart)

        '' Get current line string from array of lines using the index

        Dim CurrentLine = Small_PL_TextBoxCodeInput.Lines(CurrentLineIndex)
        Return CurrentLine
    End Function
    Public Function QuickCompile(ByRef Line As String) As String

        Dim Prog As String = Line
        Dim MyCompiler As New Compiler(Prog)

        If MyCompiler.CompileProgram() = True Then
            If MyCompiler.GetCompilerDiagnostics.Length > 0 Then

                Return "Did not Compiled Successfully"
            Else

                Return "Compiled Successfully"

            End If

        Else

            Return "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics

        End If
    End Function
    Public Sub Compile()
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
            End If

        Else
            '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
            Small_PL_TextboxErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics

        End If
    End Sub
    Public Sub run()
        Dim Prog As String = Small_PL_TextBoxCodeInput.Text

        CompilerResutltsText.Text = ""
        Small_PL_TextBoxREPL_OUTPUT.Text = ""
        ClearTree()

        For Each item In Small_PL_TextBoxCodeInput.Lines
            Dim MyCompiler As New Compiler(item)
            Dim Eval = MyCompiler.ExecuteProgram()
            CompilerResutltsText.Text &= Eval & vbNewLine
            Small_PL_TextboxErrors.Text &= MyCompiler.GetCompilerDiagnostics & vbNewLine
            AddCompiledTree(MyCompiler.CurrentSyntaxTree)
            For Each tok In MyCompiler.CurrentTokenTree
                Small_PL_TextBoxREPL_OUTPUT.Text &= tok.ToJson & vbNewLine
            Next

        Next

    End Sub
    Public Sub RunGeneral()
        Small_PL_TextBoxREPL_OUTPUT.Text = ""
        Dim Prog As String = Small_PL_TextBoxCodeInput.Text
        Prog = Prog.Replace(vbNewLine, " ")
        Prog = RTrim(Prog)
        Prog = LTrim(Prog)
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


                Dim result = MyCompiler.ExecuteProgram()

                Small_PL_TextBoxREPL_OUTPUT.Text = result
                CompilerResutltsText.Text = result
            End If

        Else
            '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
            Small_PL_TextboxErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics

        End If
    End Sub

    Private Sub Small_PL_TextBoxCodeInput_KeyDown(sender As Object, e As KeyEventArgs) Handles Small_PL_TextBoxCodeInput.KeyDown
        Dim Prog As String = Small_PL_TextBoxCodeInput.Text

        Select Case e.KeyCode
            Case Keys.Enter
                CompilerResutltsText.Text = ""
                Dim result = QuickCompile(GetCurrentLine())
                If result = "Compiled Successfully" Then
                    Small_PL_TextboxErrors.Text = "Compiled Successfully"
                    Compile()
                    For Each item In Small_PL_TextBoxCodeInput.Lines
                        Dim MyCompiler As New Compiler(item)
                        Dim Eval = MyCompiler.ExecuteProgram()
                        CompilerResutltsText.Text &= Eval & vbNewLine
                    Next



                Else

                    Small_PL_TextboxErrors.Text = ""
                    Small_PL_TextboxErrors.Text = "Did not Compiled Successfully" & vbNewLine
                    Dim Templine As Integer = 0
                    For Each item In Small_PL_TextBoxCodeInput.Lines
                        Templine += 1
                        Dim MyCompiler As New Compiler(item)
                        Dim Eval = MyCompiler.CompileProgram()
                        Small_PL_TextboxErrors.Text &= "Repl Line :" & Templine & vbNewLine & " " & MyCompiler.GetCompilerDiagnostics & vbNewLine
                    Next

                End If


        End Select

    End Sub

    Private Sub Small_PL_AstTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Small_PL_AstTreeView.AfterSelect
        Small_PL_TextBoxREPL_OUTPUT.Text = Small_PL_AstTreeView.SelectedNode.Tag
    End Sub

    Private Sub ButtonSpydazWebBasicREPL_Click(sender As Object, e As EventArgs) Handles ButtonSpydazWebBasicREPL.Click
        Dim frm As New IDE
        frm.Show()

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
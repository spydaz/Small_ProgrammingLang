Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Syntax.SyntaxNodes

Public Class IDE
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim ExpressionTree As SyntaxTree
        For Each line In CodeTextBox.Lines
            ExpressionTree = SyntaxTree.Parse(CodeTextBox.Text)
            AddCompiledTree(ExpressionTree)
            ' CodeTextBox.Text = ExpressionTree.ToJson

            Dim Eval As New Evaluator(ExpressionTree)
            Dim Result = Eval._Evaluate
            DISPLAY_OUT.Text = vbNewLine & vbNewLine & vbNewLine &
                vbNewLine & "RESULTS RETUNED : " & Result & vbNewLine

        Next
    End Sub
    Private Sub Small_PL_AstTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Small_PL_AstTreeView.AfterSelect
        AstSyntaxJson.Text = Small_PL_AstTreeView.SelectedNode.Tag
        TabControlOutput.SelectTab(TabPageAstSyntax)
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
    Public Sub ClearTree()
        Small_PL_AstTreeView.Nodes.Clear()
    End Sub
    Public Sub Compile()
        DISPLAY_OUT.Text = ""
        Dim Prog As String = CodeTextBox.Text
        Dim MyCompiler As New Compiler(Prog)
        ClearTree()

        If MyCompiler.CompileProgram() = True Then
            If MyCompiler.GetCompilerDiagnostics.Length > 0 Then
                '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
                CompilerErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics
            Else
                For Each item In MyCompiler.ExpressionTrees
                    AddCompiledTree(item)

                Next
                For Each tree In MyCompiler.TokenTrees
                    For Each item In tree
                        AstSyntaxJson.Text &= vbNewLine & item.ToJson
                    Next
                Next
                TabControlOutput.SelectTab(TabPageAstSyntax)
                CompilerErrors.Text = "Compiled Successfully"
                TabControlOutput.SelectTab(TabPage_IDE_COMPILER_ERRORS)
            End If

        Else
            '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
            CompilerErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics
            TabControlOutput.SelectTab(TabPage_IDE_COMPILER_ERRORS)
        End If
    End Sub
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
    Private Sub CodeTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CodeTextBox.KeyDown
        Dim Prog As String = CodeTextBox.Text

        Select Case e.KeyCode
            Case Keys.Enter
                CodeTextBox._Refresh()
                DISPLAY_OUT.Text = ""
                Dim result = QuickCompile(GetCurrentLine())
                If result = "Compiled Successfully" Then
                    CompilerErrors.Text = "Compiled Successfully"
                    Compile()
                    For Each item In CodeTextBox.Lines
                        Dim MyCompiler As New Compiler(item)
                        Dim Eval = MyCompiler.ExecuteProgram()
                        DISPLAY_OUT.Text &= Eval & vbNewLine
                        TabControlOutput.SelectTab(TabPage_IDE_RESULTS)
                    Next
                    CodeTextBox._Refresh()


                Else

                    CompilerErrors.Text = ""
                    CompilerErrors.Text = "Did not Compiled Successfully" & vbNewLine
                    Dim Templine As Integer = 0
                    For Each item In CodeTextBox.Lines
                        Templine += 1
                        Dim MyCompiler As New Compiler(item)
                        Dim Eval = MyCompiler.CompileProgram()
                        CompilerErrors.Text &= "Repl Line :" & Templine & vbNewLine & " " & MyCompiler.GetCompilerDiagnostics & vbNewLine
                        TabControlOutput.SelectTab(TabPage_IDE_COMPILER_ERRORS)
                    Next

                End If


        End Select

    End Sub
    Public Function GetCurrentLine() As String
        '' Get current line index using char index of any selected text for that line

        Dim CurrentLineIndex = CodeTextBox.GetLineFromCharIndex(CodeTextBox.SelectionStart)

        '' Get current line string from array of lines using the index

        Dim CurrentLine = CodeTextBox.Lines(CurrentLineIndex)
        Return CurrentLine
    End Function
End Class
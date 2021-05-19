Imports System.Windows.Forms
Imports AI_BASIC
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.CodeAnalysis
Imports System.IO
Imports System.Drawing

Public Class IDE


#Region "SpydazWebBasicCompiler"
    Private Sub Small_PL_AstTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles PL_AstTreeView.AfterSelect
        AstSyntaxJson.Text = PL_AstTreeView.SelectedNode.Tag
        TabControl_AST_TEXT.SelectTab(TabPageAstSyntax)
    End Sub
    Public Sub Compile()
        DISPLAY_OUT.Text = ""
        Dim Prog As String = CodeTextBox.Text
        Dim MyCompiler As New Compiler(Prog)


        If MyCompiler.CompileProgram() = True Then
            If MyCompiler.GetCompilerDiagnostics.Length > 0 Then
                CompilerErrors.ForeColor = Drawing.Color.Red
                CompilerErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics

                For Each item In MyCompiler.ExpressionTrees
                    AddCompiledTree(item)

                Next
                For Each tree In MyCompiler.TokenTrees
                    For Each item In tree
                        AstSyntaxJson.ForeColor = Drawing.Color.Red
                        AstSyntaxJson.Text &= vbNewLine & item.ToJson
                    Next
                Next
            Else
                For Each item In MyCompiler.ExpressionTrees
                    AddCompiledTree(item)

                Next
                For Each tree In MyCompiler.TokenTrees
                    For Each item In tree
                        AstSyntaxJson.ForeColor = Drawing.Color.Green
                        AstSyntaxJson.Text &= vbNewLine & item.ToJson
                    Next
                Next
                TabControl_AST_TEXT.SelectTab(TabPageAstSyntax)
                CompilerErrors.ForeColor = Drawing.Color.Green
                CompilerErrors.Text = "Compiled Successfully"
                TabControlOutput.SelectTab(TabPage_IDE_COMPILER_ERRORS)
            End If

        Else
            CompilerErrors.ForeColor = Drawing.Color.Red
            '  Small_PL_TextBoxREPL_OUTPUT.Text = MyCompiler.CurrentSyntaxTree.ToJson
            CompilerErrors.Text = "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics
            TabControlOutput.SelectTab(TabPage_IDE_COMPILER_ERRORS)
        End If
    End Sub
    Private Sub CodeTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CodeTextBox.KeyDown
        Dim Prog As String = CodeTextBox.Text

        Select Case e.KeyCode
            Case Keys.Enter
                DoLineSyntax(GetCurrentLine())

                If GetCurrentLine() <> "" Then
                    CodeTextBox._Refresh()
                    DISPLAY_OUT.Text = ""
                    Dim DiagResult = QuickLineDiagnostics(GetCurrentLine())
                    CompilerErrors.Text = DiagResult
                    If DiagResult = "Compiled Successfully" Then
                        ClearTree()
                        DoRunText(CodeTextBox)
                    Else
                        ClearTree()
                        Compile()
                    End If
                Else
                End If



        End Select

    End Sub
    'Tree
    '
    Public Sub AddCompiledTree(ByRef Prog As SyntaxTree)
        '_:::ROOT:::_(0)
        Dim root As New TreeNode
        root.Text = UCase(Prog._ProgramScript)
        root.Tag = Prog.ToJson
        root.ImageIndex = 27
        root.ForeColor = Color.Black
        For Each item In Prog.Body
            If item IsNot Nothing Then
                '_:::MainNodes:::_(0)
                Dim MainNode As New TreeNode
                If item._SyntaxType = SyntaxType._UnknownToken Then
                    MainNode.ForeColor = Color.Red
                    MainNode.ImageIndex = 29
                Else
                    MainNode.ForeColor = Color.Blue
                    MainNode.ImageIndex = 26

                End If
                MainNode.Text = item._SyntaxTypeStr
                MainNode.Tag = item.ToJson
                Dim SubNode As New TreeNode
                If item._SyntaxType = SyntaxType._UnknownToken Then
                    SubNode.ForeColor = Color.IndianRed
                    SubNode.ImageIndex = 29
                Else
                    SubNode.ForeColor = Color.Blue
                    MainNode.ImageIndex = 25
                End If

                SubNode.Text = item.ToString
                SubNode.Tag = item.ToJson
                MainNode.Nodes.Add(SubNode)
                root.Nodes.Add(MainNode)
            End If
        Next
        PL_AstTreeView.Nodes.Add(root)
        PL_AstTreeView.ExpandAll()
    End Sub
    'Tree
    '
    Public Sub ClearTree()
        PL_AstTreeView.Nodes.Clear()
    End Sub
    'Function
    '
    Public Function GetCurrentLine() As String
        '' Get current line index using char index of any selected text for that line

        Dim CurrentLineIndex = CodeTextBox.GetLineFromCharIndex(CodeTextBox.SelectionStart)

        '' Get current line string from array of lines using the index

        Dim CurrentLine = CodeTextBox.Lines(CurrentLineIndex)
        Return CurrentLine
    End Function
    'Line Diagnostics
    ''
    Public Function QuickLineDiagnostics(ByRef Line As String) As String
        Dim Prog As String = Line
        Dim MyCompiler As New Compiler(Prog)
        If MyCompiler.CompileProgram() = True Then
            If MyCompiler.GetCompilerDiagnostics.Length > 0 Then
                CompilerErrors.ForeColor = Drawing.Color.Red
                Return "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics & vbNewLine

            Else
                CompilerErrors.ForeColor = Drawing.Color.Green
                Return "Compiled Successfully"

            End If
        Else
            CompilerErrors.ForeColor = Drawing.Color.Red
            Return "Did not Compiled Successfully" & vbNewLine & MyCompiler.GetCompilerDiagnostics & vbNewLine
        End If
    End Function
    'Highlight Line
    '
    Public Sub DoLineSyntax(ByRef Line As String)
        Dim Prog As String = Line
        Dim MyCompiler As New Compiler(Prog)
        Dim TokeTree As New List(Of SyntaxToken)
        Dim BadStr As String = ""
        If MyCompiler.CompileProgram() = False Then
            TokeTree = MyCompiler.CurrentTokenTree
            For Each item In TokeTree
                If item._SyntaxType = SyntaxType._UnknownToken Then
                    BadStr = item._Raw
                    CodeTextBox.HighlightTerm(item._Raw)
                Else
                    SyntaxTextBox.Highlighter.ColorSearchTerm(item._Raw, CodeTextBox, Drawing.Color.AliceBlue)
                End If
            Next
        Else
            TokeTree = MyCompiler.CurrentTokenTree
            For Each item In TokeTree
                SyntaxTextBox.Highlighter.ColorSearchTerm(item._Raw, CodeTextBox, Drawing.Color.Blue)
            Next
        End If
    End Sub
    'Run TextBox
    '
    Public Sub DoRunText(ByRef Text As RichTextBox)
        Dim ExpressionTree As SyntaxTree

        For Each line In Text.Lines
            ExpressionTree = SyntaxTree.Parse(line)
            ClearTree()
            AddCompiledTree(ExpressionTree)
            Dim Eval As New Evaluator(ExpressionTree)
            Dim Result = Eval._Evaluate
            DISPLAY_OUT.Text &= vbNewLine & "RESULTS RETUNED : " & Result & vbNewLine
            Dim Str As String = ""
            For Each item In Eval._Diagnostics
                Str &= item & vbNewLine
            Next
            CompilerErrors.Text = Str
            TabControlOutput.SelectedTab = TabPage_IDE_RESULTS
        Next
    End Sub
    'Debug TextBox
    '
    Public Function _DebugDiagnosticsText(ByRef Text As RichTextBox) As String
        Dim ResultsLst As New List(Of String)
        Dim Count As Integer = 0
        For Each item In Text.Lines
            Count += 1
            Dim Debug = QuickLineDiagnostics(item)
            If Debug = "Compiled Successfully" Then
            Else
                ResultsLst.Add("ERROR_ Line" & Count & vbNewLine & Debug)
            End If

        Next
        Dim Results As String = ""
        Count = 0
        If ResultsLst.Count > 0 Then
            For Each item In ResultsLst
                Count += 1
                Results &= vbNewLine & "Error number : " & item & vbNewLine
            Next
        End If
    End Function
    Private Sub But_Compile_Click(sender As Object, e As EventArgs) Handles But_Compile.Click
        ClearTree()
        Compile()
    End Sub
    Private Sub But_Run_Click(sender As Object, e As EventArgs) Handles But_Run.Click
        DoRunText(CodeTextBox)
    End Sub
#End Region
#Region "Flie"
    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Dim myStream = CodeTextBox.Text
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim str = saveFileDialog1.FileName
            My.Computer.FileSystem.WriteAllText _
                (str, CodeTextBox.Text, True)


        End If
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Dim myOpenFileDialog As New OpenFileDialog()


        myOpenFileDialog.CheckFileExists = True
        myOpenFileDialog.Filter = "CodeText files (*.*)|*.*"
        myOpenFileDialog.InitialDirectory = Application.StartupPath
        myOpenFileDialog.Multiselect = False

        If myOpenFileDialog.ShowDialog = DialogResult.OK Then
            Dim str = myOpenFileDialog.FileName
            Dim Text = My.Computer.FileSystem.ReadAllText(str)
            CodeTextBox.Text = Text
        End If

    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        CodeTextBox.Text = ""
    End Sub

    Private Sub AddEmbedded_Click(sender As Object, e As EventArgs) Handles AddEmbedded.Click
        Dim myOpenFileDialog As New OpenFileDialog()


        myOpenFileDialog.CheckFileExists = True
        myOpenFileDialog.Filter = "Embedded files (*.*)|*.*"
        myOpenFileDialog.InitialDirectory = Application.StartupPath
        myOpenFileDialog.Multiselect = False

        If myOpenFileDialog.ShowDialog = DialogResult.OK Then
            Dim str = myOpenFileDialog.FileName
            EmbeddedFilePaths.Items.Add(str)

        End If
    End Sub

    Private Sub AddRefferences_Click(sender As Object, e As EventArgs) Handles AddRefferences.Click
        Dim myOpenFileDialog As New OpenFileDialog()


        myOpenFileDialog.CheckFileExists = True
        myOpenFileDialog.Filter = "Refference files (*.*)|*.*"
        myOpenFileDialog.InitialDirectory = Application.StartupPath
        myOpenFileDialog.Multiselect = False

        If myOpenFileDialog.ShowDialog = DialogResult.OK Then
            Dim str = myOpenFileDialog.FileName
            RefferenceFilePaths.Items.Add(str)

        End If
    End Sub

    Private Sub RemoveEmbedded_Click(sender As Object, e As EventArgs) Handles RemoveEmbedded.Click

        EmbeddedFilePaths.Items.Remove(EmbeddedFilePaths.SelectedItem)
    End Sub

    Private Sub RemoveRefference_Click(sender As Object, e As EventArgs) Handles RemoveRefference.Click

        RefferenceFilePaths.Items.Remove(RefferenceFilePaths.SelectedItem)
    End Sub



#End Region
#Region "CutPaste"
    Private Sub PasteToolStripButton_Click(sender As Object, e As EventArgs) Handles PasteToolStripButton.Click
        CodeTextBox.Paste()
    End Sub

    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        CodeTextBox.Copy()
    End Sub

    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click
        CodeTextBox.Cut()
    End Sub
#End Region
#Region "VBCompiler"
    Private Sub CompileVBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileVBToolStripMenuItem.Click
        Dim Frm As New CompilerPropertys
        Frm.CodeText = CodeTextBox.Text
        Dim EmbeddedFiles As New List(Of String)
        Dim RefferenceFiles As New List(Of String)
        For Each item In EmbeddedFilePaths.Items
            EmbeddedFiles.Add(item)
        Next
        For Each item In RefferenceFilePaths.Items
            RefferenceFiles.Add(item)
        Next
        Frm.EmbeddedFilePaths = EmbeddedFilePaths
        Frm.RefferenceFilePaths = RefferenceFilePaths

        Frm.CodeReady = True
        Frm.Show()
    End Sub


#End Region
End Class
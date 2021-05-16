Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Syntax

Public Class SAL_ZX21_SPL_BASIC_REPL

    'TEMP SETUP!!

    Public Sub loadBasicTree(ByRef Prog As SyntaxTree)
        LOGO_TreeView.Nodes.Clear()
        Dim root As New TreeNode
        If Prog.Diagnostics.Count > 0 Then
            root.ForeColor = Color.Red
        Else
            root.ForeColor = Color.GreenYellow
        End If

        '_:::ROOT:::_(0)
        root.Text = "Program" & vbNewLine
        root.Tag = "SpydazWeb.SAL.Basic"
        '_:::BODY:::_(2)
        Dim Body As New TreeNode
        Body.Text = "Body"
        Body.Tag = Prog.ToJson

        For Each item In Prog.Body
            ':::_MAIN_NODE_::: (1)
            Dim MainNode As New TreeNode
            MainNode.Text = item.ToString
            MainNode.Tag = item.ToJson
            ':::_TYPES_::: (3)
            Dim _TypeNode As New TreeNode
            If Prog.Diagnostics.Count > 0 Then
                _TypeNode.ForeColor = Color.Red
            Else
                _TypeNode.ForeColor = Color.GreenYellow
            End If
            _TypeNode.Text = "_Type :" & item._SyntaxTypeStr
            _TypeNode.Tag = "_Type"
            '(3)
            MainNode.Nodes.Add(_TypeNode)
            '(2)
            Body.Nodes.Add(MainNode)

        Next
        '(1)
        root.Nodes.Add(Body)
        '(0)
        LOGO_TreeView.Nodes.Add(root)
        LOGO_TreeView.ExpandAll()
    End Sub
    Private Sub Small_PL_ToolStripButtonRunCode_Click(sender As Object, e As EventArgs) Handles Small_PL_ToolStripButtonRunCode.Click
        Dim ExpressionTree As SyntaxTree
        For Each line In Small_PL_TextBoxCodeInput.Lines
            ExpressionTree = SyntaxTree.Parse(Small_PL_TextBoxCodeInput.Text)
            loadBasicTree(ExpressionTree)
            Small_PL_TextBoxREPL_OUTPUT.Text = ExpressionTree.ToJson

            Dim Eval As New Evaluator(ExpressionTree)
            Dim Result = Eval._Evaluate
            Small_PL_TextboxErrors.Text = vbNewLine & vbNewLine & vbNewLine &
                vbNewLine & "RESULTS RETUNED : " & Result & vbNewLine

        Next

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
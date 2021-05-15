Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.Syntax
Imports System.Console
Module REPL_
    Private Line As String = ""
    Private SAL_VB_LEXER As Lexer
    Private TokenTree As List(Of SyntaxToken)
    Private ExpressionTree As SyntaxTree
    Private EvaluateScript As Boolean = False
    Private ShowTree As Boolean = True
    Private ShowTokens As Boolean = True
    Private ReadOnly Evaluateable As Boolean = False
    Private Sub _Show_title()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Welcome to SpydazWeb Basic")
    End Sub
    Sub main()
        _Show_title()


        While True
            SetReplCode()
            _GetInput()
#Region "REPL_CMDS"
            'ReplCmds
            If Line = "#ShowTree" Then
                ShowTree = True
                _GetInput()
            End If
            If Line = "#HideTree" Then
                ShowTree = False
                _GetInput()
            End If
            If Line = "#ShowTokens" Then
                ShowTokens = True
                _GetInput()
            End If
            If Line = "#HideTokens" Then
                ShowTokens = False
                _GetInput()
            End If
            If Line = "#Cls" Then
                Console.Clear()
                _Show_title()
                _GetInput()
            End If
            'Can be turned off / To reinstate #Run
            'Default always Run
            If Line = "#Run" Then

                EvaluateScript = True
                _GetInput()
            End If
            'Script Always Compiles
            If Line = "#CompileOnly" Then
                EvaluateScript = False
                _GetInput()
            End If
#End Region
            _CreateLexer()
            _LexTokens()


            DisplayDiagnostics()


        End While


    End Sub
    Private Sub DisplayToken_Tree()
        Console.ForegroundColor = ConsoleColor.White
    End Sub
    Public Sub _GetInput()
        Line = Console.ReadLine()
    End Sub
    Public Sub _CreateLexer()
        SAL_VB_LEXER = New Lexer(Line)
    End Sub
    Public Sub _LexTokens()
        TokenTree = New List(Of SyntaxToken)
        Dim Token As SyntaxToken
        DisplayToken_Tree()

        While True
            Token = SAL_VB_LEXER._NextToken
            If Token._SyntaxType = SyntaxType._EndOfFileToken Then

                Exit While

            Else
                ' Console.WriteLine(vbNewLine & "Tokens> " & vbNewLine & "Text: " & Itok._Text & vbNewLine & "Type: " & Itok._SyntaxStr & vbNewLine)
                Console.WriteLine(Token.ToJson)
            End If
        End While
    End Sub
    Private Sub SetReplCode()
        Console.ForegroundColor = ConsoleColor.Yellow
    End Sub
    Private Function DisplayDiagnostics() As Boolean
        ExpressionTree = SyntaxTree.Parse(Line)
        If TokenTree IsNot Nothing Then
            'Catch Errors
            If ExpressionTree.Diagnostics.Count > 0 Then
                'Tokens
                Console.ForegroundColor = ConsoleColor.Red
                'PARSER DIAGNOSTICS
                Console.WriteLine("Compiler Errors: " & vbNewLine)
                For Each item In ExpressionTree.Diagnostics
                    Console.ForegroundColor = ConsoleColor.DarkRed
                    Console.WriteLine(item & vbNewLine)
                Next
                'Tokens
                _LexTokens()
                Return False

            Else
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(ExpressionTree.ToJson)
                Return True

            End If
        Else
            Return False
        End If
        SetReplCode()
    End Function
End Module

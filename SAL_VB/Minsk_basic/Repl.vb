Imports SDK.SpydazWeb.AI.Basic.CodeAnalysis
Imports SDK.SpydazWeb.AI.Basic.CodeAnalysis.Syntax
Imports SDK.SpydazWeb.AI.Basic.CodeAnalysis.Syntax.SyntaxNodes
Module Repl
    Private History As New List(Of String)
    Private ShowTree As Boolean = True
    Private ShowTokens As Boolean = True
    Sub Main()
        Console.ForegroundColor = ConsoleColor.Black
        Console.WriteLine("Welcome to Small Basic")
        While (True)
            Console.ForegroundColor = ConsoleColor.Blue
            Console.WriteLine("> ")
            Console.ForegroundColor = ConsoleColor.Cyan


            Dim UserInput_LINE As String = Console.ReadLine()

            'ReplCmds
            If UserInput_LINE = "#ShowTree" Then
                ShowTree = True
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#HideTree" Then
                ShowTree = False
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#ShowTokens" Then
                ShowTokens = True
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#HideTokens" Then
                ShowTokens = False
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#Cls" Then
                Console.Clear()
                UserInput_LINE = Console.ReadLine()
            End If


            If String.IsNullOrWhiteSpace(UserInput_LINE) Then
                Return
            Else
                'Save Statments
                History.Add(UserInput_LINE)
            End If


            'Get Expression/Tree
            Dim ExpressionTree = Ast_SyntaxTree.Parse(UserInput_LINE)
            If ShowTree = True Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine("Expression>" & vbNewLine & ExpressionTree.ToJson)
            End If


            If ShowTokens = True Then
                'Tokens
                Console.ForegroundColor = ConsoleColor.DarkGray
                LexTokens(UserInput_LINE)
            End If


            'Catch Errors
            If ExpressionTree.Diagnostics.Count > 0 Then
                'Tokens
                Console.ForegroundColor = ConsoleColor.Red
                'PARSER DIAGNOSTICS
                For Each item In ExpressionTree.Diagnostics
                    Console.WriteLine(item & vbNewLine)
                Next
                'Tokens
                LexTokens(UserInput_LINE)
            Else
                'No Errors then Evaluate
                Console.ForegroundColor = ConsoleColor.Green
                Dim Eval As New Evaluator(ExpressionTree)
                Dim Result = Eval._Evaluate
                Console.WriteLine(Result & vbNewLine)
            End If


        End While
    End Sub
    ''' <summary>
    ''' Old Tokenize
    ''' </summary>
    ''' <param name="Line"></param>
    Public Sub LexTokens(ByRef Line As String)
        Dim Itok As SyntaxToken
        'Original Tokenizer
        Dim SAL_VB_LEXER As New Lexer(Line)
        While True
            Itok = SAL_VB_LEXER._NextToken
            If Itok._SyntaxType = SyntaxType.EOF Then

                Exit While

            Else
                ' Console.WriteLine(vbNewLine & "Tokens> " & vbNewLine & "Text: " & Itok._Text & vbNewLine & "Type: " & Itok._SyntaxStr & vbNewLine)
                Console.WriteLine(Itok.ToJson)
            End If
        End While
        'TOKENIZER DIAGNOSTICS
        If SAL_VB_LEXER._Diagnostics.Count > 0 Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Tokenizer Error:" & vbNewLine)
            For Each item In SAL_VB_LEXER._Diagnostics

                Console.WriteLine(item & vbNewLine)

            Next
        End If
    End Sub
End Module

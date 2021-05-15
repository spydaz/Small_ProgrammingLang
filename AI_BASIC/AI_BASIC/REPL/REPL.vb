Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace REPL
    Module REPL
        Private ShowTree As Boolean = True
        Private ShowTokens As Boolean = True
        ''' <summary>
        ''' History of Entered Statments in repl
        ''' </summary>
        Private History As New List(Of String)
        ''' <summary>
        ''' Parsed AST
        ''' </summary>
        Private ExpressionTree As SyntaxTree
        Private UserInput_LINE As String
        ''' <summary>
        ''' Check / Report Diagnostics
        ''' </summary>
        Private ReadOnly Evaluateable As Boolean = DisplayDiagnostics()
        ''' <summary>
        ''' disable for Compile only
        ''' </summary>
        Private EvaluateScript As Boolean = True

        ''' <summary>
        ''' Console REPL
        ''' </summary>
        Sub Main()
            'Intro
            _Show_title()
            'GetInput (line by Line)
            UserInput_LINE = Console.ReadLine()
#Region "REPL_CMDS"
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
            'Can be turned off / To reinstate #Run
            'Default always Run
            If UserInput_LINE = "#Run" Then
                EvaluateScript = True
                UserInput_LINE = Console.ReadLine()
            End If
            'Script Always Compiles
            If UserInput_LINE = "#CompileOnly" Then
                EvaluateScript = False
                UserInput_LINE = Console.ReadLine()
            End If
#End Region
#Region "Main Compiler"


            'Handle WhiteSpace Early in repl
            If String.IsNullOrWhiteSpace(UserInput_LINE) Then
                Return
            Else
                'Save Statments
                History.Add(UserInput_LINE)
            End If

            'OutPutTokens
            If ShowTokens = True Then
                DisplayToken_Tree()
                LexTokens(UserInput_LINE)
            End If

            'Get Expression/Tree
            ExpressionTree = SyntaxTree.Parse(UserInput_LINE)

            'OutputAST
            If ShowTree = True Then
                DisplayAST_Tree()
            End If

            'Evaluates and runs Script on console
            'Returning a result unless there is an error
            'Errors can be returned 
            'If Executable the script will evaluate. 
            'If Compile only then nothing will be evaluated
            ''although Errors will still be returned (Compiler Errors)
            RunScript()

#End Region

            'ColorCode
            SetReplCode()
        End Sub
        Public Sub RunScript()
            ':::_EVALUATE_::: 
            If Evaluateable = True Then
                If EvaluateScript = True Then
                    'No Errors then Evaluate
                    Console.ForegroundColor = ConsoleColor.Green
                    Dim Eval As New Evaluator(ExpressionTree)
                    Dim Result = Eval._Evaluate
                    Console.WriteLine(Result & vbNewLine)
                End If

            Else
                'Already displayed diagnostics
            End If
        End Sub
        Private Sub DisplayAST_Tree()
            Console.ForegroundColor = ConsoleColor.DarkYellow
        End Sub
        Private Sub DisplayToken_Tree()
            Console.ForegroundColor = ConsoleColor.White
        End Sub
        Private Sub _Show_title()
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("Welcome  Basic")
        End Sub
        Private Function DisplayDiagnostics() As Boolean
            Console.ForegroundColor = ConsoleColor.Red
            If ExpressionTree IsNot Nothing Then
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
                    LexTokens(UserInput_LINE)
                    Return True

                Else
                    Return False

                End If
            Else
                Return False
            End If
        End Function
        Private Sub SetReplCode()
            Console.ForegroundColor = ConsoleColor.Magenta
        End Sub
        Public Sub LexTokens(ByRef Line As String)
            Dim Itok As SyntaxToken
            'Original Tokenizer
            Dim SAL_VB_LEXER As New Lexer(Line)
            While True
                Itok = SAL_VB_LEXER._NextToken
                If Itok._SyntaxType = SyntaxType._EndOfFileToken Then

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

End Namespace
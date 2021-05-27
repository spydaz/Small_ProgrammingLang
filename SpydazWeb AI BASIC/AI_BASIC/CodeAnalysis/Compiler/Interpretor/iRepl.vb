'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\InterpretorRepl.vb
'
' summary:	Interpretor repl class
'---------------------------------------------------------------------------------------------------

Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.Syntax
Imports System.Console
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports System.Windows.Forms
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Interpretor
            Module iRepl
                ''' <summary>   The line. </summary>
                Private Line As String = ""
                ''' <summary>   The sal VB lexer. </summary>
                Private SAL_VB_LEXER As Lexer
                ''' <summary>   The token tree. </summary>
                Private TokenTree As List(Of SyntaxToken)
                ''' <summary>   The token trees. </summary>
                Private TokenTrees As List(Of List(Of SyntaxToken))
                ''' <summary>   The expression trees. </summary>
                Private ExpressionTrees As List(Of SyntaxTree)
                ''' <summary>   The expression tree. </summary>
                Private ExpressionTree As SyntaxTree
                ''' <summary>   True to evaluate script. </summary>
                Private EvaluateScript As Boolean = True
                ''' <summary>   True to show, false to hide the tree. </summary>
                Private ShowTree As Boolean = True
                ''' <summary>   True to show, false to hide the tokens. </summary>
                Private ShowTokens As Boolean = True
                ''' <summary>   True to show, false to hide the diagnostics. </summary>
                Private ShowDiagnostics As Boolean = True
                ''' <summary>   The diagnostics. </summary>
                Private _diagnostics As New List(Of String)
                ''' <summary>   The sal VB parser. </summary>
                Private SAL_VB_PARSER As Parser
                ''' <summary>   The environment. </summary>
                Public Env As New EnvironmentalMemory

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Shows the title. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub _Show_title()
                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.WriteLine("Welcome to SpydazWeb Basic")
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Executes the interpretor repl operation. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Sub RunInterpretorRepl()
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
                        If Line = "#cls" Then
                            Console.Clear()
                            _Show_title()
                            _GetInput()
                        End If
                        If Line = "#repl" Then

                            Call Application.Run(New Editor_IDE)
                            _GetInput()
                        End If
                        If Line = "#ide" Then


                            Call Application.Run(New IDE)
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
                        If ShowTokens = True Then _LexTokens()
                        RunScript()
                    End While
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Executes the script operation. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub RunScript()
                    ':::_EVALUATE_::: 
                    If DisplayDiagnostics() = True Then
                        Dim Result = ""
                        If EvaluateScript = True Then
                            'No Errors then Evaluate
                            Console.ForegroundColor = ConsoleColor.Green
                            Dim Eval As New Evaluator(ExpressionTree)

                            Console.ForegroundColor = ConsoleColor.DarkGray
                            Console.WriteLine(Eval._Evaluate(Env) & vbNewLine)
                        End If

                    Else
                        'Already displayed diagnostics
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        Console.WriteLine("Unable to Evaluate" & vbNewLine)
                        _diagnostics = New List(Of String)
                    End If
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the diagnostics. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub GetDiagnostics()
                    _diagnostics.AddRange(SAL_VB_LEXER._Diagnostics)
                    _diagnostics.AddRange(ExpressionTree.Diagnostics)
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Displays a token tree. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub DisplayToken_Tree()
                    If _diagnostics.Count > 0 Then
                        Console.ForegroundColor = ConsoleColor.Gray
                    Else
                        Console.ForegroundColor = ConsoleColor.DarkYellow
                    End If

                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the input. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub _GetInput()
                    Line = Console.ReadLine()
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Creates the lexer. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub _CreateLexer()
                    SAL_VB_LEXER = New Lexer(Line)
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Lex tokens. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub _LexTokens()
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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Sets repl code. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub SetReplCode()
                    Console.ForegroundColor = ConsoleColor.Yellow
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Displays the diagnostics. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function DisplayDiagnostics() As Boolean

                    SAL_VB_PARSER = New Parser(Line)
                    ExpressionTree = SAL_VB_PARSER.Parse()
                    GetDiagnostics()

                    If ExpressionTree IsNot Nothing Then
                        'Catch Errors
                        If _diagnostics.Count > 0 Then
                            'Tokens
                            Console.ForegroundColor = ConsoleColor.Red
                            'PARSER DIAGNOSTICS
                            Console.WriteLine("Compiler Errors: " & vbNewLine)
                            If ShowDiagnostics = True Then
                                For Each item In _diagnostics
                                    Console.ForegroundColor = ConsoleColor.DarkRed
                                    Console.WriteLine(item & vbNewLine)
                                Next

                                'Tokens
                                If ShowTokens = True Then _LexTokens()
                            End If

                            Return False

                        Else
                            If ShowTree = True Then
                                Console.ForegroundColor = ConsoleColor.Green
                                Console.WriteLine(ExpressionTree.ToJson)
                            End If

                            Return True

                        End If
                    Else
                        Return False
                    End If
                    SetReplCode()
                End Function
            End Module
        End Namespace

    End Namespace
End Namespace

﻿Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.Syntax
Imports System.Console
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor

Namespace Consoles
    Namespace Interpretor
        Module InterpretorRepl
            Private Line As String = ""
            Private SAL_VB_LEXER As Lexer
            Private TokenTree As List(Of SyntaxToken)
            Private ExpressionTree As SyntaxTree
            Private EvaluateScript As Boolean = True
            Private ShowTree As Boolean = True
            Private ShowTokens As Boolean = True
            Private ShowDiagnostics As Boolean = True
            Private _diagnostics As New List(Of String)
            Private SAL_VB_PARSER As Parser
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
                    If ShowTokens = True Then _LexTokens()
                    RunScript()
                End While


            End Sub
            Private Sub RunScript()
                ':::_EVALUATE_::: 
                If DisplayDiagnostics() = True Then
                    Dim Result = ""
                    If EvaluateScript = True Then
                        'No Errors then Evaluate
                        Console.ForegroundColor = ConsoleColor.Green
                        Dim Eval As New Evaluator(ExpressionTree)

                        Console.ForegroundColor = ConsoleColor.DarkGray
                        Console.WriteLine(Eval._Evaluate & vbNewLine)
                    End If

                Else
                    'Already displayed diagnostics
                    Console.ForegroundColor = ConsoleColor.DarkGray
                    Console.WriteLine("Unable to Evaluate" & vbNewLine)
                    _diagnostics = New List(Of String)
                End If
            End Sub
            Private Sub GetDiagnostics()
                _diagnostics.AddRange(SAL_VB_LEXER._Diagnostics)
                _diagnostics.AddRange(ExpressionTree.Diagnostics)
            End Sub
            Private Sub DisplayToken_Tree()
                If _diagnostics.Count > 0 Then
                    Console.ForegroundColor = ConsoleColor.Gray
                Else
                    Console.ForegroundColor = ConsoleColor.DarkYellow
                End If

            End Sub
            Public Sub _GetInput()
                Line = Console.ReadLine()
            End Sub
            Public Sub _CreateLexer()
                SAL_VB_LEXER = New Lexer(Line)
            End Sub
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
            Private Sub SetReplCode()
                Console.ForegroundColor = ConsoleColor.Yellow
            End Sub
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


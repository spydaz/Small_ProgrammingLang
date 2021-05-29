﻿'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\iRepl.vb
'
' summary:	Declares the iRepl interface
'---------------------------------------------------------------------------------------------------


Imports System.CodeDom.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Consoles
Imports AI_BASIC.Syntax.SyntaxNodes
'''////////////////////////////////////////////////////////////////////////////////////////////////////
''' <summary>   A repl. </summary>
'''
''' <remarks>   Leroy, 27/05/2021. </remarks>
'''////////////////////////////////////////////////////////////////////////////////////////////////////
Public Class InterpretorRepl
    ''' <summary>   The line. </summary>
    Public Line As String = ""
    ''' <summary>   True to show, false to hide the tree. </summary>
    Public ShowTree As Boolean = False
    ''' <summary>   True to show, false to hide the syntax. </summary>
    Public ShowSyntax As Boolean = True
    ''' <summary>   True to show, false to hide the code. </summary>
    Public ShowCode As Boolean = True
    Public ShowDiagnostics As Boolean = True
    Public Evaluate As Boolean = True

#Region "REPL"
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Check repl command. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub CheckReplCmd()
        Select Case UCase(Line)
            Case "CLS"
                _ClearScreen()
                _GetInput()
            Case "SHOWSYNTAX"
                ShowSyntax = True
                _GetInput()
            Case "SHOWTOKENS"
                ShowTree = True
                _GetInput()
            Case "HIDESYNTAX"
                ShowSyntax = False
                _GetInput()
            Case "HIDETOKENS"
                ShowTree = False
                _GetInput()
            Case "HIDECODE"
                ShowCode = False
                _GetInput()
            Case "SHOWCODE"
                ShowCode = True
                _GetInput()
            Case "SHOWDIAGNOSTICS"
                ShowDiagnostics = True
                _GetInput()
            Case "HIDEDIAGNOSTICS"
                ShowDiagnostics = False
                _GetInput()
        End Select
        _GetInput()
        _ClearScreen()
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Resets the console. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub ResetConsole()
        Console.ForegroundColor = ConsoleColor.White
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Writes the code line. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub _WriteCodeLine()
        Console.WriteLine(Line)
        ResetConsole()
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Displays a prompt. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub _DisplayPrompt()

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.Write("»")
        ResetConsole()
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Clears the screen. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub _ClearScreen()
        Console.Clear()
        ResetConsole()
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Gets the input. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub _GetInput()
        Line = Console.ReadLine().Replace("»", "")
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Shows the title. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub _Show_title()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Welcome to SpydazWeb Basic")
        ResetConsole()
    End Sub


#End Region
#Region "Compiler"
    ''' <summary>   The compiled result. </summary>
    Private CompiledResult As EvaluationResult
    Private HasDiagnostic As Boolean = False
    Public Sub CompileScript(ByRef Script As List(Of String))

        Dim Lst As New List(Of EvaluationResult)
        Dim Prog As New List(Of SyntaxNode)
        For Each item In Script
            Me.Line &= item & vbNewLine
            Dim iCompile As New Compiler(Line)
            Dim result = iCompile.Result
            'Trees are Already Compiled in Compiler
            If ShowTree = True Then
                iCompile.PrintTokenTreeToConsole()
            End If

            'Trees are Already Compiled in Compiler
            If ShowSyntax = True Then
                iCompile.PrintSyntaxTreeToConsole()
            End If
            If result.Diagnostics.HasErrors = True Then
                HasDiagnostic = True
                DisplayOutput()
            End If
            Lst.Add(result)
            Prog.AddRange(iCompile.GetSyntaxTree)
        Next

        If HasDiagnostic = True Then
            Console.WriteLine("Diagnostics" & vbNewLine)
            For Each item In Lst
                ConsoleWriter.WriteDiagnostics(item.Diagnostics.ToJson())
                ResetConsole()
            Next

        Else
            HasDiagnostic = False
            ResetConsole()
            Console.WriteLine("CODE: " & vbNewLine & Line & vbNewLine)
            ResetConsole()
            Eval(Prog)
        End If
        RunCmdLine()
    End Sub
    Private Sub CompileLine()
        Console.ForegroundColor = ConsoleColor.Magenta
        Dim Compile As New Compiler(Line)
        Dim result = Compile.Result
        CompiledResult = result

        'Trees are Already Compiled in Compiler
        If ShowTree = True Then
            Compile.PrintTokenTreeToConsole()
        End If

        'Trees are Already Compiled in Compiler
        If ShowSyntax = True Then
            Compile.PrintSyntaxTreeToConsole()
        End If

        If result.Diagnostics.HasErrors = True Then
            Console.WriteLine("Diagnostics" & vbNewLine)
            HasDiagnostic = True
            DisplayOutput()
        Else
            HasDiagnostic = False
            ResetConsole()

            Console.WriteLine("CODE: " & vbNewLine & Line & vbNewLine)
            Eval(Compile.GetSyntaxTree())


        End If


    End Sub
    Public Sub RunCmdLine()
        _Show_title()
        While True
            _DisplayPrompt()
            CheckReplCmd()
            CompileLine()

        End While
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Displays the diagnostics. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub DisplayDiagnostics()
        ConsoleWriter.WriteDiagnostics(CompiledResult.Diagnostics.ToJson())
        ResetConsole()
    End Sub
    Public Sub DisplayOutput()
        If HasDiagnostic = True Then
            If ShowDiagnostics = True Then
                DisplayDiagnostics()
            End If
        End If
        ResetConsole()
    End Sub
#End Region
    Public Sub Eval(ByRef _tree As List(Of SyntaxNode))
        Dim MyInterpretor As New Interpretor
        Console.WriteLine("Evaluation" & vbNewLine)
        Console.ForegroundColor = ConsoleColor.Green
        For Each item In _tree
            Dim x = MyInterpretor._EvaluateExpression(item)
            Console.WriteLine("Evaluation Result :" & vbNewLine & x.ToString & vbNewLine & vbNewLine)
        Next
        ResetConsole()
    End Sub

End Class

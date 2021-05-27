'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\iRepl.vb
'
' summary:	Declares the iRepl interface
'---------------------------------------------------------------------------------------------------


Imports System.CodeDom.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Consoles
'''////////////////////////////////////////////////////////////////////////////////////////////////////
''' <summary>   A repl. </summary>
'''
''' <remarks>   Leroy, 27/05/2021. </remarks>
'''////////////////////////////////////////////////////////////////////////////////////////////////////

Public MustInherit Class iRepl_Model
    ''' <summary>   The line. </summary>
    Public Line As String = ""
    ''' <summary>   True to show, false to hide the tree. </summary>
    Public ShowTree As Boolean = False
    ''' <summary>   True to show, false to hide the syntax. </summary>
    Public ShowSyntax As Boolean = False
    ''' <summary>   True to show, false to hide the code. </summary>
    Public ShowCode As Boolean = True
    Public ShowDiagnostics As Boolean = True


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
    Public Sub DisplayOutput()
        If HasDiagnostic = True Then
            If ShowDiagnostics = True Then
                DisplayDiagnostics()
            End If
        End If
        DisplayTrees()
        DisplayEvaluation()
    End Sub

#End Region
    Public Sub DisplayTrees()
        Dim icompiler As New Compiler(Line)
        If ShowTree = True Then
            icompiler.PrintTokenTreeToConsole()
        End If

        If ShowSyntax = True Then
            icompiler.PrintSyntaxTreeToConsole()
        End If

    End Sub
#Region "Compiler"
    ''' <summary>   The compiled result. </summary>
    Private CompiledResult As EvaluationResult
    Private HasDiagnostic As Boolean = False

    Private Sub CompileLine()
        Console.ForegroundColor = ConsoleColor.Magenta
        Dim Compile As New Compiler(Line)
        Dim result = Compile.Result
        CompiledResult = result
        If result.Diagnostics.HasErrors = True Then
            Console.WriteLine("Diagnostics" & vbNewLine)
            HasDiagnostic = True
        Else
            HasDiagnostic = False
        End If

    End Sub
    Public Sub RunCmdLine()
        _Show_title()
        While True
            _DisplayPrompt()
            CheckReplCmd()
            CompileLine()
            DisplayOutput()
        End While
    End Sub
    Public Sub DisplayEvaluation()
        Console.WriteLine("Evaluation" & vbNewLine)
        Console.ForegroundColor = ConsoleColor.Yellow
        For Each item In CompiledResult.Results
            Console.WriteLine(item.ToString())
        Next
        ResetConsole()
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


#End Region
End Class

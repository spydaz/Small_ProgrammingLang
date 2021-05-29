'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\iRepl.vb
'
' summary:	Declares the iRepl interface
'---------------------------------------------------------------------------------------------------

Imports System.Runtime.InteropServices
Imports System.CodeDom.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.Consoles
Imports AI_BASIC.Syntax.SyntaxNodes
Imports System.Windows.Forms


'''////////////////////////////////////////////////////////////////////////////////////////////////////
''' <summary>   A repl. </summary>
'''
''' <remarks>   Leroy, 27/05/2021. </remarks>
'''////////////////////////////////////////////////////////////////////////////////////////////////////
Public Class InterpretorRepl
    Public ConsoleEnabled As Boolean = True

    ''' <summary>   The line. </summary>
    Public Line As String = ""
    ''' <summary>   True to show, false to hide the tree. </summary>
    Public ShowTree As Boolean = True
    ''' <summary>   True to show, false to hide the syntax. </summary>
    Public ShowSyntax As Boolean = True
    ''' <summary>   True to show, false to hide the code. </summary>
    Public ShowCode As Boolean = True
    Public ShowDiagnostics As Boolean = True
    Public Evaluate As Boolean = True
    Public OutputStr As String = ""
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
            Case "HELP"
                ShowHelp()
                _GetInput()
            Case "SHOWDIAGNOSTICS"
                ShowDiagnostics = True
                _GetInput()
            Case "HIDEDIAGNOSTICS"
                ShowDiagnostics = False
                _GetInput()
            Case "IDE"
                LoadIDE()
                _GetInput()
        End Select
        _GetInput()
        _ClearScreen()
    End Sub
    Public Sub ShowHelp()
        _ClearScreen()

        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine("Basic Console Commands")
        Console.WriteLine()
        Console.WriteLine(
    "ShowDiagnostics - HideDiagnostics          - Display CompilerDiagnostics" & vbNewLine & vbNewLine &
          "ShowCode - hideCode                        - Shows entered Code" & vbNewLine & vbNewLine &
          "Showtokens - hideTokens                    - Shows Tokenier Tokens" & vbNewLine & vbNewLine &
          "ShowSyntax - hideyntax                    - Show Syntax Expression" & vbNewLine & vbNewLine &
          "CLS - Clears the screen                    - CLS" & vbNewLine & vbNewLine &
          "Help - Shows helpScreen                    - Shows Help ")
        Console.WriteLine()

        Console.WriteLine("IDE - Loads the Programming IDE")
        Console.WriteLine()
        Console.WriteLine()
        _Show_title()
        Console.WriteLine()

        _DisplayPrompt()
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
    Public Sub LoadIDE()

        Call Application.Run(New IDE())
        _ClearScreen()
        _Show_title()
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
    Public Function CompileLine(ByRef iLine As String) As String
        Console.ForegroundColor = ConsoleColor.Magenta
        Dim Compile As New Compiler(iLine)
        Dim result = Compile.Result
        CompiledResult = result
        Dim Str As String = ""
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
            Str &= "Diagnostics" & vbNewLine
            HasDiagnostic = True
            Str &= DisplayOutput()
        Else
            HasDiagnostic = False
            ResetConsole()
            Console.WriteLine("CODE: " & vbNewLine & Line & vbNewLine)
            Str &= "CODE: " & vbNewLine & Line & vbNewLine
            Str &= Eval(Compile.GetSyntaxTree())
        End If
        Return Str
    End Function
    Public Sub RunCmdLine()
        _Show_title()
        While True
            _DisplayPrompt()
            CheckReplCmd()
            CompileLine()

        End While
    End Sub
    Public Sub Run()
        RunCmdLine()
    End Sub
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Displays the diagnostics. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Function DisplayDiagnostics() As String
        ConsoleWriter.WriteDiagnostics(CompiledResult.Diagnostics.ToJson())
        ResetConsole()
        Return CompiledResult.Diagnostics.ToJson()
    End Function
    Public Function DisplayOutput() As String
        Dim x = ""
        If HasDiagnostic = True Then
            If ShowDiagnostics = True Then
                x = DisplayDiagnostics()
            End If
        End If
        ResetConsole()
        Return x
    End Function
#End Region
    Public Function Eval(ByRef _tree As List(Of SyntaxNode)) As String
        Dim Str As String = ""
        Dim MyInterpretor As New Interpretor
        Console.WriteLine("Evaluation" & vbNewLine)
        Console.ForegroundColor = ConsoleColor.Green
        For Each item In _tree
            Dim x = MyInterpretor._EvaluateExpression(item)
            If x IsNot Nothing Then
                Console.WriteLine("Evaluation Result :" & vbNewLine & x.ToString & vbNewLine & vbNewLine)
                Str &= "Evaluation Result :" & vbNewLine & x.ToString & vbNewLine & vbNewLine
            End If
        Next
        ResetConsole()
        Return Str
    End Function
    Public Class Win32
        <DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean

        End Function
        <DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean

        End Function

    End Class
End Class

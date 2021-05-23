Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation

Public Class ReplCompiler
    Inherits iRepl
    Dim CompiledResult As EvaluationResult

    Public Shadows Sub Run()
        _Show_title()
        While True
            _DisplayPrompt()
            _GetInput()
            ' CheckReplCmd()
            CompileLine()

        End While
    End Sub
    Public Sub DisplayEvaluation()
        Console.WriteLine("Evaluation" & vbNewLine)
        Console.ForegroundColor = ConsoleColor.Yellow
        For Each item In CompiledResult.Result
            Console.WriteLine(item.ToString())
        Next
        ResetConsole()
    End Sub
    Public Sub DisplayDiagnostics()
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine(CompiledResult.Diagnostics.ToJson())
        ResetConsole()
    End Sub
    Public Sub CompileLine()
        Console.ForegroundColor = ConsoleColor.Magenta
        Dim Compile As New CompilerII(Line)
        Dim result = Compile.Result
        CompiledResult = result
        If result.Diagnostics.HasErrors = True Then
            Console.WriteLine("Diagnostics" & vbNewLine)
            DisplayDiagnostics()
        Else

            DisplayEvaluation()
        End If

    End Sub

End Class

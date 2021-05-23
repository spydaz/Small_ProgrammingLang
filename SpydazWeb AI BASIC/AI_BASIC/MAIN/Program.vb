Imports System.Windows.Forms
Imports System.Console
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor

Module Program

    Sub Main()
        Call Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        InterpretorRepl.RunInterpretorRepl()

        Dim repl As New ReplCompiler
        'repl.Run()

    End Sub


End Module

Imports System.CodeDom.Compiler
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.Consoles
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Interpretor
            Public Class IDE_Repl

                Public ShowTree As Boolean = True
                Public ShowSyntax As Boolean = True
                Public ShowCode As Boolean = True
                Public ShowDiagnostics As Boolean = True
                Public Evaluate As Boolean = True
                Public OutputStr As String = ""
                Private CompiledResult As EvaluationResult
                Private HasDiagnostic As Boolean = False
                Public Function CompileLine(ByRef Line As String) As String
                    Console.ForegroundColor = ConsoleColor.Magenta
                    Dim Compile As New Compiler(Line)
                    Dim result = Compile.Result
                    CompiledResult = result

                    'Trees are Already Compiled in Compiler
                    If ShowTree = True Then
                        Win32.AllocConsole()
                        Compile.PrintTokenTreeToConsole()
                        Win32.AllocConsole()
                        Console.ReadLine()
                    End If

                    'Trees are Already Compiled in Compiler
                    If ShowSyntax = True Then
                        Win32.AllocConsole()
                        Compile.PrintSyntaxTreeToConsole()
                        Win32.AllocConsole()
                        Console.ReadLine()
                    End If

                    If result.Diagnostics.HasErrors = True Then
                        HasDiagnostic = True
                        Return DisplayDiagnostics()
                    Else
                        HasDiagnostic = False
                        Return Eval(Compile.GetSyntaxTree())
                    End If


                End Function
                Public Function _GetInput(ByRef CodeTextBox As RichTextBox) As String
                    Dim Line = GetCurrentLine(CodeTextBox)
                    Return Line
                End Function
                Private Function GetCurrentLine(ByRef CodeTextBox As RichTextBox) As String
                    '' Get current line index using char index of any selected text for that line
                    Dim CurrentLineIndex = CodeTextBox.GetLineFromCharIndex(CodeTextBox.SelectionStart)

                    '' Get current line string from array of lines using the index
                    Dim CurrentLine = CodeTextBox.Lines(CurrentLineIndex)
                    Return CurrentLine
                End Function
                Public Function Eval(ByRef _tree As List(Of SyntaxNode)) As String
                    Dim Str As String = ""
                    Dim MyInterpretor As New Interpretor

                    For Each item In _tree
                        Dim x = MyInterpretor._EvaluateExpression(item)
                        If x IsNot Nothing Then
                            Str &= "Evaluation Result :" & vbNewLine & x.ToString & vbNewLine & vbNewLine
                        End If
                    Next

                    Return Str
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Displays the diagnostics. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function DisplayDiagnostics() As String
                    Win32.AllocConsole()
                    ConsoleWriter.WriteDiagnostics(CompiledResult.Diagnostics.ToJson())
                    Win32.AllocConsole()
                    Console.ReadLine()
                    Return CompiledResult.Diagnostics.ToJson()
                End Function
                Public Function CompileScript(ByRef Script As List(Of String)) As String
                    Dim Str As String = ""
                    Dim Line As String = ""
                    Dim Lst As New List(Of EvaluationResult)
                    Dim Prog As New List(Of SyntaxNode)
                    For Each item In Script
                        Line &= item & vbNewLine
                        Dim iCompile As New Compiler(Line)
                        Dim result = iCompile.Result
                        If ShowTree = True Then
                            Win32.AllocConsole()
                            iCompile.PrintTokenTreeToConsole()
                            Win32.AllocConsole()
                            Console.ReadLine()
                        End If
                        If ShowSyntax = True Then
                            Win32.AllocConsole()
                            iCompile.PrintSyntaxTreeToConsole()
                            Win32.AllocConsole()
                            Console.ReadLine()
                        End If
                        If result.Diagnostics.HasErrors = True Then
                            HasDiagnostic = True
                            Return DisplayDiagnostics()
                        End If
                        Lst.Add(result)
                        Prog.AddRange(iCompile.GetSyntaxTree)
                    Next
                    Return Eval(Prog)
                End Function
            End Class
        End Namespace
    End Namespace
End Namespace

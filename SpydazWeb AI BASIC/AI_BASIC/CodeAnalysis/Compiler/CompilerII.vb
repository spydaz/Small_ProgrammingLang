
Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Consoles
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis

    Namespace Compiler
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class CompilerII
            Private Script As String = ""
            Public Result As EvaluationResult
            Private Final As Object
            Private ReadOnly Property FinalResult As Object
                Get
                    Return Final
                End Get
            End Property
            Public Sub New(script As String)
                If script Is Nothing Then
                    Throw New ArgumentNullException(NameOf(script))
                End If
                Me.Script = script
                Dim _debug = Compile()
                Dim Eval As New EvaluatorII
                Dim _results = Eval.EvaluateProgram(script, Program)
                Result = New EvaluationResult(_debug, _results)
                Final = Result.Results.last
            End Sub
            Public Sub PrintTokenTreeToConsole()
                ConsoleWriter.WriteTokenList(GetTokenTree)
            End Sub
            Public Sub PrintSyntaxTreeToConsole()
                ConsoleWriter.WriteExpressionList(GetSyntaxTree)
            End Sub
            Public Function GetTokenTree() As List(Of SyntaxToken)
                Dim Ast As New List(Of SyntaxToken)
                For Each lst In Debugger.ProduceTokenTree(Script)
                    Ast.AddRange(lst)
                Next
                Return Ast
            End Function
            Public Function GetSyntaxTree() As List(Of SyntaxNode)
                Dim Ast As New List(Of SyntaxNode)
                For Each lst In Debugger.ProduceExpressionTree(Script)
                    Ast.AddRange(lst.Body)
                Next
                Return Ast
            End Function
            Private Program As List(Of SyntaxTree)
            Private Function Compile() As CompilerDiagnosticResults
                Dim iDebugger As New Debugger(Script)
                Program = iDebugger.GetProgram
                Return iDebugger.DebugCodeScript(Script)
            End Function
#Region "TOSTRING"
            ''' <summary>
            ''' Serializes object to json
            ''' </summary>
            ''' <returns> </returns>
            Public Function ToJson() As String
                Return FormatJsonOutput(ToString)
            End Function
            Public Overrides Function ToString() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)
            End Function
            Private Function FormatJsonOutput(ByVal jsonString As String) As String
                Dim stringBuilder = New StringBuilder()
                Dim escaping As Boolean = False
                Dim inQuotes As Boolean = False
                Dim indentation As Integer = 0

                For Each character As Char In jsonString

                    If escaping Then
                        escaping = False
                        stringBuilder.Append(character)
                    Else

                        If character = "\"c Then
                            escaping = True
                            stringBuilder.Append(character)
                        ElseIf character = """"c Then
                            inQuotes = Not inQuotes
                            stringBuilder.Append(character)
                        ElseIf Not inQuotes Then

                            If character = ","c Then
                                stringBuilder.Append(character)
                                stringBuilder.Append(vbCrLf)
                                stringBuilder.Append(vbTab, indentation)
                            ElseIf character = "["c OrElse character = "{"c Then
                                stringBuilder.Append(character)
                                stringBuilder.Append(vbCrLf)
                                stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                            ElseIf character = "]"c OrElse character = "}"c Then
                                stringBuilder.Append(vbCrLf)
                                stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                                stringBuilder.Append(character)
                            ElseIf character = ":"c Then
                                stringBuilder.Append(character)
                                stringBuilder.Append(vbTab)
                            ElseIf Not Char.IsWhiteSpace(character) Then
                                stringBuilder.Append(character)
                            End If
                        Else
                            stringBuilder.Append(character)
                        End If
                    End If
                Next

                Return stringBuilder.ToString()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
#End Region
            Private Function GetDiagnostics() As String
                If Result.Diagnostics.HasErrors = True Then
                    Return Result.Diagnostics.CollectDiagnostics
                Else
                    Return "NO ERRORS"
                End If
            End Function
            Public Sub PrintDiagnostics()
                ConsoleWriter.WriteDiagnostics(GetDiagnostics)
            End Sub
        End Class
    End Namespace
End Namespace





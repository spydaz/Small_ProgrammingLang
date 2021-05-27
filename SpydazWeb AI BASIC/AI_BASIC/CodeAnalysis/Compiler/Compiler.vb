'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\CompilerII.vb
'
' summary:	Compiler ii class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Consoles
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Compiler
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A <see cref="compiler"/>. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class Compiler
            ''' <summary>   The script. </summary>
            Private Script As String = ""
            ''' <summary>   The result. </summary>
            Public Result As EvaluationResult

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets or sets the final result. </summary>
            '''
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Final As Object

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the final result. </summary>
            '''
            ''' <value> The final result. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private ReadOnly Property FinalResult As Object
                Get
                    Return Final
                End Get
            End Property

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="script">   The script. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(script As String)
                If script Is Nothing Then
                    Throw New ArgumentNullException(NameOf(script))
                End If
                Me.Script = script
                Dim _debug = Compile()
                Dim Eval As New Interpretor.Interpretor
                Dim _results = Eval.EvaluateProgram(script, Program)
                Result = New EvaluationResult(_debug, _results)
                Final = Result.Results.last
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Print token tree to console. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub PrintTokenTreeToConsole()
                ConsoleWriter.WriteTokenList(GetTokenTree)
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Print syntax tree to console. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub PrintSyntaxTreeToConsole()
                ConsoleWriter.WriteExpressionList(GetSyntaxTree)
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets token tree. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The token tree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function GetTokenTree() As List(Of SyntaxToken)
                Dim Ast As New List(Of SyntaxToken)
                For Each lst In Debugger.ProduceTokenTree(Script)
                    Ast.AddRange(lst)
                Next
                Return Ast
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets syntax tree. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The syntax tree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function GetSyntaxTree() As List(Of SyntaxNode)
                Dim Ast As New List(Of SyntaxNode)
                For Each lst In Debugger.ProduceExpressionTree(Script)
                    Ast.AddRange(lst.Body)
                Next
                Return Ast
            End Function
            ''' <summary>   The program. </summary>
            Private Program As List(Of SyntaxTree)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the compile. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The CompilerDiagnosticResults. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Function Compile() As CompilerDiagnosticResults
                Dim iDebugger As New Debugger(Script)
                Program = iDebugger.GetProgram
                Return iDebugger.DebugCodeScript(Script)
            End Function
#Region "TOSTRING"

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Converts this  to a JSON. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   This  as a String. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function ToJson() As String
                Return FormatJsonOutput(ToString)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Returns a string that represents the current object. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   A string that represents the current object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Overrides Function ToString() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Format JSON output. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="jsonString">   The JSON string. </param>
            '''
            ''' <returns>   The formatted JSON output. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
#End Region

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the diagnostics. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The diagnostics. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Function GetDiagnostics() As String
                If Result.Diagnostics.HasErrors = True Then
                    Return Result.Diagnostics.CollectDiagnostics
                Else
                    Return "NO ERRORS"
                End If
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Print diagnostics. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub PrintDiagnostics()
                ConsoleWriter.WriteDiagnostics(GetDiagnostics)
            End Sub
        End Class
    End Namespace
End Namespace





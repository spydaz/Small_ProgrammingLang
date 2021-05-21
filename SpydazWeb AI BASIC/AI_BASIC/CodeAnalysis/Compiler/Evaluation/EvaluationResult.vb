Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Diagnostics

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Evaluation
            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Public NotInheritable Class EvaluationResult
                Public ReadOnly Property Diagnostics As CompilerDiagnosticResults
                Public ReadOnly Property Result As Object

                Public Sub New(diagnostics As CompilerDiagnosticResults, result As Object)
                    If diagnostics Is Nothing Then
                        Throw New ArgumentNullException(NameOf(diagnostics))
                    End If

                    If result Is Nothing Then
                        Throw New ArgumentNullException(NameOf(result))
                    End If

                    Me.Diagnostics = diagnostics
                    Me.Result = result
                End Sub
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
#End Region
                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
            End Class

        End Namespace
    End Namespace
End Namespace


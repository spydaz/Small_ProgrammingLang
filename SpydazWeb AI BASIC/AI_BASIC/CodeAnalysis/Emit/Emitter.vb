'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Emit\Emitter.vb
'
' summary:	Emitter class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Web.Script.Serialization

Namespace CodeAnalysis

    Namespace Emit

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An emitter. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Emitter
#Region "TOSTRING"

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Converts this  to a JSON. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   This  as a String. </returns>
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
#End Region

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
        End Class

    End Namespace
End Namespace


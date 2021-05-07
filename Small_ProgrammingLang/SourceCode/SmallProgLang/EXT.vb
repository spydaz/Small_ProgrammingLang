Imports System.Text
Imports System.Web.Script.Serialization

Namespace SmallProgLang
    ''' <summary>
    ''' Minor Extension Methods; Required for json formatting
    ''' </summary>
    Public Module EXT
        <Runtime.CompilerServices.Extension()>
        Public Function FormatJsonOutput(ByVal jsonString As String) As String
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
        <Runtime.CompilerServices.Extension()>
        Public Function ToJson(ByRef item As Object) As String
            Dim Converter As New JavaScriptSerializer
            Return Converter.Serialize(item)

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function SplitAtNewLine(input As String) As IEnumerable(Of String)
            Return input.Split({Environment.NewLine}, StringSplitOptions.None)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractLastChar(ByRef InputStr As String) As String
            ExtractLastChar = Right(InputStr, 1)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractFirstChar(ByRef InputStr As String) As String
            ExtractFirstChar = Left(InputStr, 1)
        End Function
    End Module
End Namespace

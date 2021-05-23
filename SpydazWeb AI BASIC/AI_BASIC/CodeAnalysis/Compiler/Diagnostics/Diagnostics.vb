Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports AI_BASIC.Syntax
Namespace CodeAnalysis
    Namespace Diagnostics
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class DiagnosticsException
            Public ErrorMessage As String = ""
            ''' <summary>
            ''' Type of Exception for Data Management
            ''' </summary>
            Public _ExceptionType As ExceptionType
            ''' <summary>
            ''' Used as a SyntaxToken/Expression (Containing the token)
            ''' </summary>
            Public _Data As Object
            ''' <summary>
            ''' Used to identify Data object such as Expression or token
            ''' </summary>
            Public DataType As SyntaxType
            Public ReadOnly _Timestamp As String = Timestamp()
            Public Sub New(errorMessage As String, exceptionType As ExceptionType, data As Object, dataType As SyntaxType)
                If errorMessage Is Nothing Then
                    Throw New ArgumentNullException(NameOf(errorMessage))
                End If
                If data Is Nothing Then
                    Throw New ArgumentNullException(NameOf(data))
                End If

                Me.ErrorMessage = errorMessage
                _ExceptionType = exceptionType
                _Data = data
                Me.DataType = dataType
            End Sub
            Private Function Timestamp()
                Dim Stamp As String = ""
                Dim localDate = DateTime.Now
                Dim cultureNames() As String = {"en-GB"}
                For Each cultureName In cultureNames
                    Dim culture As New CultureInfo(cultureName)

                    Stamp = "   Local date and time: " & localDate.ToString(culture) & ", " & localDate.Kind
                Next
                Return Stamp
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
#End Region
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class DiagnosticOutput
            Public Diagnostics As New List(Of DiagnosticsException)
            Public ReadOnly NumberOfErrors = Diagnostics.Count
            Public DiagnosticsType As DiagnosticType
            Public Sub New(diagnostics As List(Of DiagnosticsException), DiagnosticsType As DiagnosticType)
                If diagnostics Is Nothing Then
                    Throw New ArgumentNullException(NameOf(diagnostics))
                End If
                Me.DiagnosticsType = DiagnosticsType


                Me.Diagnostics = diagnostics
                Me.NumberOfErrors = NumberOfErrors
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
        Public Class CompilerDiagnosticResults
            Private iDiagnostics As List(Of DiagnosticOutput)
            Public ReadOnly Property Diagnostics As List(Of DiagnosticOutput)
                Get
                    Return iDiagnostics
                End Get
            End Property
            Private iNumberOfErrors As Integer = 0
            Public ReadOnly Property NumberOfErrors As Integer
                Get
                    Return iNumberOfErrors
                End Get
            End Property
            Public ReadOnly Property HasErrors As Boolean
                Get
                    If NumberOfErrors > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End Get
            End Property

            Public Sub New()


                Me.iDiagnostics = New List(Of DiagnosticOutput)
            End Sub

            Public Sub Add(ByRef diagnostic As DiagnosticOutput)
                iDiagnostics.Add(diagnostic)
                For Each item In iDiagnostics
                    iNumberOfErrors += item.NumberOfErrors
                Next

            End Sub
            ''' <summary>
            ''' Returns a Diagnostics Report
            ''' </summary>
            ''' <returns></returns>
            Public Function CollectDiagnostics() As String
                Dim StrOut As String = ""

                For Each itemDiagnosticType In iDiagnostics
                    StrOut &= itemDiagnosticType.ToString & vbNewLine
                    For Each item In itemDiagnosticType.Diagnostics
                        StrOut &= item.ToString & vbNewLine

                    Next
                Next
                Return StrOut
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
#End Region
        End Class

    End Namespace
End Namespace

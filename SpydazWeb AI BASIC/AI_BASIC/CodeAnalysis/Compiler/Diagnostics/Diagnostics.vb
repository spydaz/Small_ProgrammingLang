'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Diagnostics\Diagnostics.vb
'
' summary:	Diagnostics class
'---------------------------------------------------------------------------------------------------

Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports AI_BASIC.Syntax
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Diagnostics

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Exception for signalling diagnostics errors. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class DiagnosticsException
            ''' <summary>   Message describing the error. </summary>
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
            ''' <summary>   The timestamp. </summary>
            Public ReadOnly _Timestamp As String = Timestamp()

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="errorMessage">     Message describing the error. </param>
            ''' <param name="exceptionType">    Type of Exception for Data Management. </param>
            ''' <param name="data">             Used as a SyntaxToken/Expression (Containing the token) </param>
            ''' <param name="dataType">         Used to identify Data object such as Expression or token. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(errorMessage As String, exceptionType As ExceptionType, data As Object, dataType As SyntaxType)
                If errorMessage Is Nothing Then
                    GeneralException.Add(New DiagnosticsException("Unable to register DiagnosticsException " & NameOf(errorMessage), ExceptionType.NullRefferenceError, NameOf(data), SyntaxType._String))

                End If
                If data Is Nothing Then
                    GeneralException.Add(New DiagnosticsException("Unable to register DiagnosticsException " & NameOf(data), ExceptionType.NullRefferenceError, NameOf(data), SyntaxType._String))

                End If

                Me.ErrorMessage = errorMessage
                _ExceptionType = exceptionType
                _Data = data
                Me.DataType = dataType
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Timestamps this.  </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   . </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A diagnostic output. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class DiagnosticOutput
            ''' <summary>   The diagnostics. </summary>
            Public Diagnostics As New List(Of DiagnosticsException)
            ''' <summary>   An enum constant representing the number of errors option. </summary>
            Public ReadOnly NumberOfErrors = Diagnostics.Count
            ''' <summary>   Type of the diagnostics. </summary>
            Public DiagnosticsType As DiagnosticType

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="diagnostics">      The diagnostics. </param>
            ''' <param name="DiagnosticsType">  Type of the diagnostics. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(diagnostics As List(Of DiagnosticsException), DiagnosticsType As DiagnosticType)
                If diagnostics Is Nothing Then
                    Throw New ArgumentNullException(NameOf(diagnostics))
                End If
                Me.DiagnosticsType = DiagnosticsType


                Me.Diagnostics = diagnostics
                Me.NumberOfErrors = NumberOfErrors
            End Sub
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

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A compiler diagnostic results. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Class CompilerDiagnosticResults
            ''' <summary>   Zero-based index of the diagnostics. </summary>
            Private iDiagnostics As List(Of DiagnosticOutput)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the diagnostics. </summary>
            '''
            ''' <value> The diagnostics. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property Diagnostics As List(Of DiagnosticOutput)
                Get
                    Return iDiagnostics
                End Get
            End Property
            ''' <summary>   Number of errors. </summary>
            Private iNumberOfErrors As Integer = 0

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the number of errors. </summary>
            '''
            ''' <value> The total number of errors. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property NumberOfErrors As Integer
                Get
                    Return iNumberOfErrors
                End Get
            End Property

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the has errors. </summary>
            '''
            ''' <value> The has errors. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property HasErrors As Boolean
                Get
                    If NumberOfErrors > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End Get
            End Property

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Initializes a new instance of the <see cref="T:System.Object" /> class. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New()


                Me.iDiagnostics = New List(Of DiagnosticOutput)
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Adds diagnostic. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="diagnostic">   [in,out] The diagnostic to add. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub Add(ByRef diagnostic As DiagnosticOutput)
                iDiagnostics.Add(diagnostic)
                For Each item In iDiagnostics
                    iNumberOfErrors += item.NumberOfErrors
                Next

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Collect diagnostics. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   A String. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
#End Region
        End Class

    End Namespace
End Namespace

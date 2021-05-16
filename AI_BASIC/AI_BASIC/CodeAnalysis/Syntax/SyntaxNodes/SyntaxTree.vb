
Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
''' <summary>
''' Use to contain the 'Program'
''' </summary>
Public Class SyntaxTree
    ''' <summary>
    ''' Original Raw Script of the program
    ''' </summary>
    Public _ProgramScript As String
    ''' <summary>
    ''' Expressions defined By the Parser
    ''' </summary>
    Public Body As List(Of SyntaxNode)
    ''' <summary>
    ''' Used to manage Diagnostics for the Program Erros
    ''' </summary>
    Public Diagnostics As List(Of String)

    Public Sub New(programScript As String, body As List(Of SyntaxNode), diagnostics As List(Of String))
        If programScript Is Nothing Then
            Throw New ArgumentNullException(NameOf(programScript))
        End If

        If body Is Nothing Then
            Throw New ArgumentNullException(NameOf(body))
        End If

        If diagnostics Is Nothing Then
            Throw New ArgumentNullException(NameOf(diagnostics))
        End If

        _ProgramScript = programScript
        Me.Body = body
        Me.Diagnostics = diagnostics
    End Sub

    Public Shared Function ParseBasic(ByRef _Script As String) As SyntaxTree
        Dim MyParser As New Parser(_Script)

        Return MyParser.Parse(LangTypes.BASIC)
    End Function
    Public Shared Function ParseSal(ByRef _Script As String) As SyntaxTree
        Dim MyParser As New Parser(_Script)
        Return MyParser.Parse(LangTypes.LOGO)
    End Function
    Public Shared Function ParseLogo(ByRef _Script As String) As SyntaxTree
        Dim MyParser As New Parser(_Script)
        Return MyParser.Parse(LangTypes.SAL)
    End Function
    Public Shared Function Parse(ByRef _Script As String) As SyntaxTree
        Dim MyParser As New Parser(_Script)

        Return MyParser.Parse()
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

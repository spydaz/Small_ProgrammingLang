'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Syntax\SyntaxNodes\SyntaxTree.vb
'
' summary:	Syntax tree class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.Typing

Namespace Syntax
    Namespace SyntaxNodes

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A syntax tree. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Class SyntaxTree
            ''' <summary>
            ''' Original Raw Script of the program
            ''' </summary>
            Public _ProgramScript As String
            ''' <summary>
            ''' Expressions defined By the Parser
            ''' </summary>
            Public Body As List(Of SyntaxNode)
            ''' <summary>   The tokens. </summary>
            Public Tokens As List(Of SyntaxToken)
            ''' <summary>
            ''' Used to manage Diagnostics for the Program Erros
            ''' </summary>
            Public Diagnostics As List(Of String)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="programScript">    Original Raw Script of the program. </param>
            ''' <param name="body">             Expressions defined By the Parser. </param>
            ''' <param name="Tokens">           The tokens. </param>
            ''' <param name="diagnostics">      Used to manage Diagnostics for the Program Erros. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(programScript As String, body As List(Of SyntaxNode), Tokens As List(Of SyntaxToken), diagnostics As List(Of String))
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Parse basic. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_Script">  [in,out] The script. </param>
            '''
            ''' <returns>   A SyntaxTree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Shared Function ParseBasic(ByRef _Script As String) As SyntaxTree
                Dim MyParser As New Parser(_Script)

                Return MyParser.Parse(LangTypes.BASIC)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Parse sal. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_Script">  [in,out] The script. </param>
            '''
            ''' <returns>   A SyntaxTree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Shared Function ParseSal(ByRef _Script As String) As SyntaxTree
                Dim MyParser As New Parser(_Script)
                Return MyParser.Parse(LangTypes.SAL)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Parse logo. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_Script">  [in,out] The script. </param>
            '''
            ''' <returns>   A SyntaxTree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Shared Function ParseLogo(ByRef _Script As String) As SyntaxTree
                Dim MyParser As New Parser(_Script)
                Return MyParser.Parse(LangTypes.SAL)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Parses the given script. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_Script">  [in,out] The script. </param>
            '''
            ''' <returns>   A SyntaxTree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Shared Function Parse(ByRef _Script As String) As SyntaxTree
                Dim MyParser As New Parser(_Script)

                Return MyParser.Parse()
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


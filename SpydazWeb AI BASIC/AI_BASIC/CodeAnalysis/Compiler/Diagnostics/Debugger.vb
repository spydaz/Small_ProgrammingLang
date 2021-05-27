'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Diagnostics\Debugger.vb
'
' summary:	Debugger class
'---------------------------------------------------------------------------------------------------

Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports System.Runtime.InteropServices
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports System.Linq.Expressions
Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Diagnostics

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A debugger. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class Debugger
            ''' <summary>   The script. </summary>
            Public Script As String

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_script">  [in,out] The script. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(ByRef _script As String)
                Script = _script
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the program. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The program. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function GetProgram() As List(Of SyntaxTree)
                Return ParserTree
            End Function
#Region "Produce Debugging"
            ''' <summary>   The debugging. </summary>
            Private Debugging As CompilerDiagnosticResults

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Debug code line. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Line"> [in,out]. </param>
            '''
            ''' <returns>   The CompilerDiagnosticResults. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function DebugCodeLine(ByRef Line As String) As CompilerDiagnosticResults
                ProduceTokenTreeLine(Line)
                ProduceExpressionSyntaxTreeLine(Line)
                GetDiagnostics()
                Return Debugging
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Debug code script. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Script">   [in,out]. </param>
            '''
            ''' <returns>   The CompilerDiagnosticResults. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function DebugCodeScript(ByRef Script As String) As CompilerDiagnosticResults
                ProduceTokenTree(Script)
                ProduceExpressionTree(Script)
                GetDiagnostics()
                Return Debugging
            End Function
#End Region
#Region "Produce ExpressionTrees with Diagnostics"
            ''' <summary>   The parser tree. </summary>
            Private Shared ParserTree As List(Of SyntaxTree)
            ''' <summary>   The parser diagnostics. </summary>
            Private Shared ParserDiagnostics As New List(Of DiagnosticsException)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Produce expression tree. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Script">   [in,out]. </param>
            '''
            ''' <returns>   A List(Of SyntaxTree) </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Shared Function ProduceExpressionTree(ByRef Script As String) As List(Of SyntaxTree)
                ParserTree = New List(Of SyntaxTree)
                Dim Program As List(Of String)
                Program = Script.Split(vbNewLine).ToList
                For Each Line In Program
                    ParserTree.Add(ProduceExpressionSyntaxTreeLine(Line))
                Next
                Return ParserTree
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Produce expression syntax tree line. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Line"> [in,out]. </param>
            '''
            ''' <returns>   A SyntaxTree. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Shared Function ProduceExpressionSyntaxTreeLine(ByRef Line As String) As SyntaxTree
                Dim iParser = New Parser(Line)
                Dim ExpressionTree As SyntaxTree
                ExpressionTree = iParser.Parse()
                Dim _LineDiagnostics As New List(Of DiagnosticsException)
                _LineDiagnostics.AddRange(iParser.ParserDiagnostics)
                ParserDiagnostics.AddRange(_LineDiagnostics)

                Return ExpressionTree
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets parser diagnostic. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The parser diagnostic. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Shared Function GetParserDiagnostic() As DiagnosticOutput
                Return New DiagnosticOutput(ParserDiagnostics, DiagnosticType.ExpressionSyntaxDiagnostics)
            End Function
#End Region
#Region "Produce TokenTrees with Diagnostics"
            ''' <summary>   The lexer diagnostics. </summary>

            Private Shared LexerDiagnostics As New List(Of DiagnosticsException)
            ''' <summary>   The token tree. </summary>
            Private Shared TokenTree As List(Of List(Of SyntaxToken))

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Produce token tree. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Script">   [in,out]. </param>
            '''
            ''' <returns>   A List(Of List(Of SyntaxToken)) </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Friend Shared Function ProduceTokenTree(ByRef Script As String) As List(Of List(Of SyntaxToken))
                TokenTree = New List(Of List(Of SyntaxToken))
                Dim Program As List(Of String)
                Program = Script.Split(vbNewLine).ToList
                For Each Line In Program
                    TokenTree.Add(ProduceTokenTreeLine(Line))
                Next
                Return TokenTree
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Produce token tree line. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Line"> [in,out]. </param>
            '''
            ''' <returns>   A List(Of SyntaxToken) </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Shared Function ProduceTokenTreeLine(ByRef Line As String) As List(Of SyntaxToken)
                Dim CurrentTree As New List(Of SyntaxToken)
                Dim iLexer As New Lexer(Line)
                Dim _LineDiagnostics As New List(Of DiagnosticsException)

#Region "Create TokenTree"
                While True
                    Dim Token = iLexer._NextToken
                    If Token._SyntaxType = SyntaxType._EndOfFileToken Then

                        Exit While

                    Else
                        CurrentTree.Add(Token)
                    End If
                End While

                'Add End Of FileToken
                CurrentTree.Add(New SyntaxToken(SyntaxType._EndOfLineToken, SyntaxType._EndOfLineToken.GetSyntaxTypeStr,
                                          "EOL", "EOL", Line.Length, Line.Length))
#End Region

                _LineDiagnostics.AddRange(iLexer.LexerDiagnostics)
                LexerDiagnostics.AddRange(_LineDiagnostics)
                Return CurrentTree
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets lexer diagnostic. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The lexer diagnostic. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function GetLexerDiagnostic() As DiagnosticOutput
                Return New DiagnosticOutput(LexerDiagnostics, DiagnosticType.TokenizerDiagnostics)
            End Function
#End Region

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets the diagnostics. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Sub GetDiagnostics()
                Debugging = New CompilerDiagnosticResults()
                Debugging.Add(GetLexerDiagnostic)
                Debugging.Add(GetParserDiagnostic)
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
        End Class
    End Namespace
End Namespace




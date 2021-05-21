Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports System.Runtime.InteropServices
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports System.Linq.Expressions
Imports System.Text
Imports System.Web.Script.Serialization

Namespace CodeAnalysis
    Namespace Diagnostics
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class Debugger
            Public Script As String
            Public Sub New(ByRef _script As String)
                Script = _script
            End Sub
            Public Function GetProgram() As List(Of SyntaxTree)
                Return ParserTree
            End Function
#Region "Produce Debugging"
            Private Debugging As CompilerDiagnosticResults
            ''' <summary>
            ''' Used for Individual Line Debugging
            ''' </summary>
            ''' <param name="Line"></param>
            ''' <returns></returns>
            Public Function DebugCodeLine(ByRef Line As String) As CompilerDiagnosticResults
                ProduceTokenTreeLine(Line)
                ProduceExpressionSyntaxTreeLine(Line)
                GetDiagnostics()
                Return Debugging
            End Function
            ''' <summary>
            ''' Used For Script Debugging
            ''' </summary>
            ''' <param name="Script"></param>
            ''' <returns></returns>
            Public Function DebugCodeScript(ByRef Script As String) As CompilerDiagnosticResults
                ProduceTokenTree(Script)
                ProduceExpressionTree(Script)
                GetDiagnostics()
                Return Debugging
            End Function
#End Region
#Region "Produce ExpressionTrees with Diagnostics"
            Private Shared ParserTree As List(Of SyntaxTree)
            Private Shared ParserDiagnostics As New List(Of DiagnosticsException)
            Public Shared Function ProduceExpressionTree(ByRef Script As String) As List(Of SyntaxTree)
                Dim Program As List(Of String)
                Program = Script.Split(vbNewLine).ToList
                For Each Line In Program
                    ParserTree.Add(ProduceExpressionSyntaxTreeLine(Line))
                Next
                Return ParserTree
            End Function
            Private Shared Function ProduceExpressionSyntaxTreeLine(ByRef Line As String) As SyntaxTree
                Dim iParser = New Parser(Line)
                Dim ExpressionTree As SyntaxTree
                ExpressionTree = iParser.Parse()
                Dim _LineDiagnostics As New List(Of DiagnosticsException)
                _LineDiagnostics.AddRange(iParser._Diagnostics)
                ParserDiagnostics.AddRange(_LineDiagnostics)

                Return ExpressionTree
            End Function
            Private Shared Function GetParserDiagnostic() As DiagnosticOutput
                Return New DiagnosticOutput(ParserDiagnostics, DiagnosticType.ExpressionSyntaxDiagnostics)
            End Function
#End Region
#Region "Produce TokenTrees with Diagnostics"

            Private Shared LexerDiagnostics As New List(Of DiagnosticsException)
            Private Shared TokenTree As List(Of List(Of SyntaxToken))
            Friend Shared Function ProduceTokenTree(ByRef Script As String) As List(Of List(Of SyntaxToken))
                Dim Program As List(Of String)
                Program = Script.Split(vbNewLine).ToList
                For Each Line In Program
                    TokenTree.Add(ProduceTokenTreeLine(Line))
                Next
                Return TokenTree
            End Function
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

                _LineDiagnostics.AddRange(iLexer._Diagnostics)
                LexerDiagnostics.AddRange(_LineDiagnostics)
                Return CurrentTree
            End Function
            Public Function GetLexerDiagnostic() As DiagnosticOutput
                Return New DiagnosticOutput(LexerDiagnostics, DiagnosticType.TokenizerDiagnostics)
            End Function
#End Region
            Private Sub GetDiagnostics()
                Debugging = New CompilerDiagnosticResults()
                Debugging.Add(GetLexerDiagnostic)
                Debugging.Add(GetParserDiagnostic)
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

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
#End Region
        End Class
    End Namespace
End Namespace




Imports System.Web
Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler
        Public Class Parser
            Private _Script As String
            Private _Tree As List(Of SyntaxToken)
            Private CursorPosition As Integer = 0
            Public _Diagnostics As New List(Of String)
            ''' <summary>
            ''' To hold the look ahead value without consuming the value
            ''' </summary>
            Public ReadOnly Property Lookahead As String
                Get
                    Return Tokenizer.ViewNextSlice
                End Get
            End Property
            Public ReadOnly Property LookaheadToken As SyntaxToken
                Get
                    Return Tokenizer.CheckIdentifiedToken(Lookahead)
                End Get
            End Property
            Public ReadOnly Property LookaheadSyntaxType As SyntaxType
                Get
                    Return Tokenizer.CheckIdentifiedToken(Lookahead)._SyntaxType
                End Get
            End Property
            ''' <summary>
            ''' Tokenizer !
            ''' </summary>
            Public Tokenizer As Lexer
            Private ReadOnly Property CurrentToken As SyntaxToken
                Get
                    Return _Peek(0)
                End Get
            End Property
            Private Function _Peek(ByVal offset As Integer) As SyntaxToken
                Dim index = CursorPosition + offset
                If index >= _Tree.Count Then Return _Tree(_Tree.Count - 1)
                Return _Tree(index)
            End Function
            Public Sub New(ByRef Script As String)
                _Tree = New List(Of SyntaxToken)
                If Script Is Nothing Then
                    Throw New ArgumentNullException(NameOf(Script))
                End If

                '::_FORMAT TEXT_::
                'In this case the newline char often causes issues in parsing,
                'so we can replace it with the c# end of line marker
                Script = Script.Replace(vbNewLine, ";")
                'Clear Whitespace
                Script = Script.Replace("  ", " ")
                'Clear Whitespace
                Script = RTrim(Script)
                Script = LTrim(Script)


                Me._Script = Script
                Tokenizer = New Lexer(Script)
                'LexWhole_Input
                _LexTokens()
            End Sub
            Public Sub _LexTokens()
                Dim MyTok = Tokenizer._NextToken
                Do While MyTok._SyntaxType <> SyntaxType._EndOfFileToken
                    'Clean Tokens As the arrive in the Tree
                    If MyTok._SyntaxType <> SyntaxType._UnknownToken OrElse
                        MyTok._SyntaxType <> SyntaxType._WhitespaceToken Then
                        _Tree.Add(MyTok)
                    End If

                    'Get And Check Next
                    MyTok = Tokenizer._NextToken
                Loop
            End Sub
            Public Function ParseSyntaxTree() As SyntaxTree
                '  Return New SyntaxTree(_Script, _Tree, _Diagnostics)
                Return Nothing
            End Function
            Public Function _GetNextToken() As SyntaxToken
                Dim iCurrentToken = CurrentToken
                CursorPosition += 1
                Return iCurrentToken
            End Function
            Public Function _MatchToken(ByRef Expected As SyntaxType) As SyntaxToken
                Dim iCurrentToken As SyntaxToken
                iCurrentToken = CurrentToken

                Try

                    If iCurrentToken._SyntaxType = Expected Then
                        'get Expected token
                        Return _GetNextToken()
                    Else
                        _Diagnostics.Add("Unrecognized Token in input: '" & iCurrentToken._SyntaxTypeStr & "' at Position : " & CursorPosition & vbNewLine &
                                     " Expected : " & Expected.ToString & vbNewLine)
                        'Generate Token (Expected Token)

                        Return New SyntaxToken(Expected, "Generated", Nothing, Nothing, CursorPosition, CursorPosition)
                    End If
                Catch ex As Exception
                    'MustbeNothing
                End Try

                'MustbeNothing
                Return Nothing
            End Function
            Public Function Parse(Optional Lang As LangTypes = LangTypes.Unknown) As SyntaxTree
                Select Case Lang
                    Case LangTypes.BASIC
                        Return ParseBasic()
                    Case LangTypes.LOGO
                        Return ParseLogo()
                    Case LangTypes.SAL
                        Return ParseSal()
                    Case LangTypes.Unknown
                        Return ParseSyntaxTree()
                End Select
                Return Nothing
            End Function
            Public Function ParseLogo() As SyntaxTree

                Return Nothing
            End Function
            Public Function ParseSal() As SyntaxTree

                Return Nothing
            End Function
            Public Function ParseBasic() As SyntaxTree

                Return Nothing
            End Function
        End Class
    End Namespace
End Namespace


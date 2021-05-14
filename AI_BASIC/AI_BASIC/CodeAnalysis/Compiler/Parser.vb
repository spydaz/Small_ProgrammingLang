Imports System.Web
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Compiler
        Public Class Parser
#Region "Propertys"
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
#End Region
#Region "Functions"
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
#End Region
#Region "Parser"

            Public Function ParseSyntaxTree() As SyntaxTree
                Return _ExpressionSyntaxTree()
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
                Return ParseSyntaxTree()
            End Function
            Public Function ParseLogo() As SyntaxTree
                Return _LogoExpressionSyntaxTree()
            End Function
            Public Function ParseSal() As SyntaxTree
                Return _SalExpressionSyntaxTree()
            End Function
            Public Function ParseBasic() As SyntaxTree
                Return _BasicExpressionSyntaxTree()
            End Function

#End Region
#Region "Basic _ExpressionSyntaxTree"
            Public Function _ExpressionSyntaxTree() As SyntaxTree
                Select Case LookaheadSyntaxType
                    Case SyntaxType._SAL_PROGRAM_BEGIN
                        Return _SalExpressionSyntaxTree()
                    Case SyntaxType.LOGO_LANG
                        Return _LogoExpressionSyntaxTree()
                    Case SyntaxType.BASIC_LANG
                        Return _BasicExpressionSyntaxTree()
                    Case Else
                        Return _GeneralExpressionSyntaxTree()
                End Select
            End Function
            Public Function _SalExpressionSyntaxTree() As SyntaxTree
                Return Nothing
            End Function
            Public Function _BasicExpressionSyntaxTree() As SyntaxTree
                Return Nothing
            End Function
            Public Function _LogoExpressionSyntaxTree() As SyntaxTree
                Return Nothing
            End Function
            Public Function _GeneralExpressionSyntaxTree() As SyntaxTree
                Return Nothing
            End Function
#End Region
#Region "MAIN_EXPRESSIONS"
            ''' <summary>
            ''' Literal
            ''' </summary>
            ''' <returns></returns>
            Public Function _LiteralExpression() As ExpressionSyntaxNode
                Select Case LookaheadSyntaxType

                    Case SyntaxType._Integer
                    Case SyntaxType._Boolean
                    Case SyntaxType._String
                    Case SyntaxType._arrayList
                    Case SyntaxType._Date

                End Select
            End Function
            ''' <summary>
            ''' (Identifer_KeyWord)
            ''' </summary>
            ''' <returns></returns>
            Public Function _PrimaryExpression() As ExpressionSyntaxNode


            End Function
            Public Function _LeftHandExpression()

            End Function
            ''' <summary>
            ''' Left Operator Right
            ''' </summary>
            ''' <returns></returns>
            Public Function _BinaryExpression() As ExpressionSyntaxNode


            End Function
            ''' <summary>
            ''' Addative +- Addative
            ''' </summary>
            ''' <returns></returns>
            Public Function _FactorExpression() As ExpressionSyntaxNode

                Select Case LookaheadSyntaxType
                    Case SyntaxType.Add_Operator
                    Case SyntaxType.Add_Equals_Operator
                    Case SyntaxType.Sub_Operator
                    Case SyntaxType.Minus_Equals_Operator
                End Select

            End Function
            ''' <summary>
            ''' Multiplicative */ Multiplicative
            ''' </summary>
            ''' <returns></returns>
            Public Function _TermExpression() As ExpressionSyntaxNode
                Select Case LookaheadSyntaxType
                    Case SyntaxType.Multiply_Operator
                    Case SyntaxType.Multiply_Equals_Operator
                    Case SyntaxType.Divide_Operator
                    Case SyntaxType.Divide_Equals_Operator
                End Select

            End Function
            ''' <summary>
            ''' Expression ComparesTo Expression
            ''' </summary>
            ''' <returns></returns>
            Public Function _ComparisionExpression() As ExpressionSyntaxNode
                Select Case LookaheadSyntaxType
                    Case SyntaxType.GreaterThan_Operator
                    Case SyntaxType.Add_Equals_Operator
                    Case SyntaxType.LessThanOperator
                    Case SyntaxType.LessThanEquals
                End Select
            End Function
            ''' <summary>
            ''' Identifier =
            ''' </summary>
            ''' <returns></returns>
            Public Function _AssignmentExpression() As ExpressionSyntaxNode
                Select Case LookaheadSyntaxType
                    Case SyntaxType._SIMPLE_ASSIGN

                End Select

            End Function
            ''' <summary>
            ''' DIM/LET
            ''' </summary>
            ''' <returns></returns>
            Public Function _VariableDeclarationExpression() As ExpressionSyntaxNode


            End Function
            ''' <summary>
            ''' {Expression;Expression;}
            ''' </summary>
            ''' <returns></returns>
            Public Function _CodeBlockExpression() As ExpressionSyntaxNode


            End Function
            ''' <summary>
            ''' (Expression)
            ''' </summary>
            ''' <returns></returns>
            Public Function _ParenthesizedExpression() As ExpressionSyntaxNode


            End Function
            ''' <summary>
            ''' [term,term] [identifer,identifer]
            ''' </summary>
            ''' <returns></returns>
            Public Function _ListExpression() As ExpressionSyntaxNode


            End Function
#End Region
        End Class
    End Namespace
End Namespace


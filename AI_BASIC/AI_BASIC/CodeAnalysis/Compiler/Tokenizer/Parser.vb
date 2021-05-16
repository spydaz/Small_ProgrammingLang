Imports System.Web
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports Microsoft.VisualBasic.CompilerServices

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Tokenizer

            Public Class Parser
#Region "Propertys"
                Private _Script As String
                Private _Tree As List(Of SyntaxToken)
                Private CursorPosition As Integer = 0
                Public _Diagnostics As New List(Of String)
                Private EOT_CursorPosition As Integer = 0

                ''' <summary>
                ''' To hold the look ahead value without consuming the value
                ''' </summary>
                Public ReadOnly Property Lookahead As String
                    Get
                        If EOT_CursorPosition <= CursorPosition + 1 Then
                            Return _Tree(CursorPosition + 1)._Value
                        Else
                            Return "EOF"
                        End If

                    End Get
                End Property
                Public ReadOnly Property LookaheadToken As SyntaxToken
                    Get
                        If CursorPosition + 1 <= EOT_CursorPosition Then
                            Return _Tree(CursorPosition + 1)
                        Else
                            Return _Tree(EOT_CursorPosition)
                        End If

                    End Get
                End Property
                Public ReadOnly Property LookaheadSyntaxType As SyntaxType
                    Get
                        If CursorPosition + 1 <= EOT_CursorPosition Then
                            Return _Tree(CursorPosition + 1)._SyntaxType
                        Else
                            Return SyntaxType._EndOfFileToken
                        End If

                    End Get
                End Property
                ''' <summary>
                ''' Tokenizer !
                ''' </summary>
                Private Tokenizer As Lexer
                Private ReadOnly Property CurrentToken As SyntaxToken
                    Get
                        Return _Peek(0)
                    End Get
                End Property
#End Region
#Region "Functions"
                Private Function _Peek(ByVal offset As Integer) As SyntaxToken
                    Dim index = CursorPosition + offset
                    If _Tree.Count > 0 Then


                        If index >= _Tree.Count Then
                            Return _Tree(_Tree.Count - 1)
                        Else
                            Return _Tree(index)
                        End If
                    Else

                    End If
                    Return Nothing
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
                    EOT_CursorPosition = _Tree.Count
                End Sub
                Public Sub New(ByRef TokenTree As List(Of SyntaxToken))
                    _Tree = TokenTree
                    EOT_CursorPosition = _Tree.Count
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
                    Select Case CurrentToken._SyntaxType
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
                    Return New SyntaxTree(_Script, ExpressionList, _Diagnostics)
                End Function
                Public Function _BasicExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Diagnostics)
                End Function
                Public Function _LogoExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Diagnostics)
                End Function
                Public Function _GeneralExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Diagnostics)
                End Function
#End Region
#Region "MAIN_EXPRESSIONS"
                Public Function ExpressionList() As List(Of SyntaxNode)
                    Dim Lst As New List(Of SyntaxNode)
                    Do While CursorPosition < EOT_CursorPosition
                        Lst.Add(_Expression())
                        CursorPosition += 1
                    Loop


                    Return Lst
                End Function
                Public Function _Expression()

                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.LetKeyword
                            Return _VariableDeclarationExpression()
                        Case SyntaxType.DimKeyword
                            Return _VariableDeclarationExpression()
                        Case Else
                            Return _PrimaryExpression()
                    End Select

                End Function
                Public Function _UnaryExpression(ByRef _Operator As SyntaxToken) As ExpressionSyntaxNode
                    Dim x = New UnaryExpression(_Operator, _NumericLiteralExpression)
                    CursorPosition += 2
                    Return x
                End Function
                ''' <summary>
                ''' (Identifer_KeyWord)
                ''' </summary>
                ''' <returns></returns>
                Public Function _PrimaryExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.Add_Operator
                            If LookaheadSyntaxType = SyntaxType._Integer Then
                                _Left = _UnaryExpression(_MatchToken(SyntaxType.Add_Operator))

                                Return _LeftHandExpression(_Left)
                            Else
                                Return _LeftHandExpression(_Left)
                            End If
                        Case SyntaxType.Sub_Operator
                            If LookaheadSyntaxType = SyntaxType._Integer Then
                                _Left = _UnaryExpression(_MatchToken(SyntaxType.Sub_Operator))

                                Return _LeftHandExpression(_Left)
                            Else
                                Return _LeftHandExpression(_Left)
                            End If
                        Case SyntaxType._Integer, SyntaxType._Decimal, SyntaxType._arrayList, SyntaxType._String
                            Return _LeftHandExpression(_LiteralExpression())
                        Case SyntaxType._Identifier
                            Return _LeftHandExpression(_IdentifierExpression())

                    End Select
                    _Diagnostics.Add("unknown _PrimaryExpression? " & vbNewLine & _Left.ToJson)
                    Return _Left
                End Function
                Public Function _LeftHandExpression(ByRef _left As ExpressionSyntaxNode)
                    Select Case CurrentToken._SyntaxType

                                    'Simple Assign
                        Case SyntaxType.Equals
                            Return _AssignmentExpression(_left)
                                    'Complex Assign
                        Case SyntaxType.Add_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Divide_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Minus_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Multiply_Equals_Operator
                            Return _AssignmentExpression(_left)
                                    'Compare
                        Case SyntaxType.GreaterThanEquals
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.LessThanEquals
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.NotEqual
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.EquivelentTo
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.GreaterThan_Operator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.LessThanOperator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                                    'Maths
                        Case SyntaxType.Add_Operator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.Divide_Operator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.Sub_Operator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                        Case SyntaxType.Multiply_Operator
                            Return _BinaryExpression(_left, CurrentToken._SyntaxType.GetBinaryOperatorPrecedence)
                    End Select
                    _Diagnostics.Add("unknown _BinaryExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function

                ''' <summary>
                ''' Left Operator Right
                ''' </summary>
                ''' <returns></returns>
                Public Function _BinaryExpression(ByRef _Left As ExpressionSyntaxNode, ByRef Precedence As Integer) As ExpressionSyntaxNode

                    Dim _Operator As New SyntaxToken
                    Dim Right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                                    'Simple Assign
                        Case SyntaxType.Equals
                            _Operator = _MatchToken(SyntaxType.Equals)

                                    'Complex Assign
                        Case SyntaxType.Add_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Add_Equals_Operator)

                        Case SyntaxType.Divide_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Divide_Equals_Operator)

                        Case SyntaxType.Minus_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Minus_Equals_Operator)

                        Case SyntaxType.Multiply_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Multiply_Equals_Operator)

                                    'Compare
                        Case SyntaxType.GreaterThanEquals
                            _Operator = _MatchToken(SyntaxType.GreaterThanEquals)

                        Case SyntaxType.LessThanEquals
                            _Operator = _MatchToken(SyntaxType.LessThanEquals)

                        Case SyntaxType.NotEqual
                            _Operator = _MatchToken(SyntaxType.NotEqual)

                        Case SyntaxType.EquivelentTo
                            _Operator = _MatchToken(SyntaxType.EquivelentTo)

                        Case SyntaxType.GreaterThan_Operator
                            _Operator = _MatchToken(SyntaxType.GreaterThan_Operator)

                        Case SyntaxType.LessThanOperator
                            _Operator = _MatchToken(SyntaxType.LessThanOperator)

                                    'Maths
                        Case SyntaxType.Add_Operator
                            _Operator = _MatchToken(SyntaxType.Add_Operator)

                        Case SyntaxType.Divide_Operator
                            _Operator = _MatchToken(SyntaxType.Divide_Operator)

                        Case SyntaxType.Sub_Operator
                            _Operator = _MatchToken(SyntaxType.Sub_Operator)

                        Case SyntaxType.Multiply_Operator
                            _Operator = _MatchToken(SyntaxType.Multiply_Operator)

                    End Select
                    Right = _PrimaryExpression()
                    Dim x = New BinaryExpression(_Left, Right, _Operator)
                    Return x
                End Function
#End Region
#Region "Blocks"
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
#Region "Variables"
                ''' <summary>
                ''' Identifier =
                ''' </summary>
                ''' <returns></returns>
                Public Function _AssignmentExpression(ByRef Left As IdentifierExpression) As ExpressionSyntaxNode

                    Select Case CurrentToken._SyntaxType

                        Case SyntaxType.Equals
                            Dim _Operator = _MatchToken(SyntaxType.Equals)

                        Case SyntaxType.Multiply_Equals_Operator
                            Dim _Operator = _MatchToken(SyntaxType.Multiply_Equals_Operator)
                        Case SyntaxType.Divide_Equals_Operator
                            Dim _Operator = _MatchToken(SyntaxType.Divide_Equals_Operator)
                        Case SyntaxType.Add_Equals_Operator
                            Dim _Operator = _MatchToken(SyntaxType.Add_Equals_Operator)
                        Case SyntaxType.Minus_Equals_Operator
                            Dim _Operator = _MatchToken(SyntaxType.Minus_Equals_Operator)

                    End Select
                    Dim _right = _PrimaryExpression()
                    'Todo: Requires Assignment Expression
                    _Diagnostics.Add("unknown _AssignmentExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function
                ''' <summary>
                ''' DIM/LET
                ''' </summary>
                ''' <returns></returns>
                Public Function _VariableDeclarationExpression() As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.LetKeyword
                            Dim _LetKeyword = _MatchToken(SyntaxType.LetKeyword)
                            CursorPosition += 1
                        Case SyntaxType.DimKeyword
                            Dim _DimKeyword = _MatchToken(SyntaxType.DimKeyword)
                            CursorPosition += 1
                    End Select


                    Dim left = _IdentifierExpression()
                    CursorPosition += 1
                    Dim _Operator = _MatchToken(SyntaxType.Equals)
                    CursorPosition += 1
                    'TODO : Requires Deleration Expression 
                    Dim _Type = _TypeExpression()
                    _Diagnostics.Add("unknown _VariableDeclarationExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function
                Public Function _TypeExpression() As SyntaxType
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._BooleanType
                            _MatchToken(SyntaxType._BooleanType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._IntegerType
                            _MatchToken(SyntaxType._IntegerType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._DecimalType
                            _MatchToken(SyntaxType._DecimalType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._StringType
                            _MatchToken(SyntaxType._StringType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._ArrayType
                            _MatchToken(SyntaxType._ArrayType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._DateType
                            _MatchToken(SyntaxType._DateType)
                            Return CurrentToken._SyntaxType
                        Case SyntaxType._NullType
                            _MatchToken(SyntaxType._NullType)
                            Return CurrentToken._SyntaxType
                    End Select
                    _Diagnostics.Add("unknown _TypeExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return SyntaxType._null
                End Function
#End Region
#Region "Literals"
                ''' <summary>
                ''' Literal
                ''' </summary>
                ''' <returns></returns>
                Public Function _LiteralExpression() As ExpressionSyntaxNode

                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Integer
                            Return _NumericLiteralExpression()
                        Case SyntaxType._Decimal
                            Return _NumericLiteralExpression()
                        Case SyntaxType._String
                        Case SyntaxType._arrayList
                    End Select
                    _Diagnostics.Add("unknown _Literal? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function
                Public Function _IdentifierExpression() As IdentifierExpression
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Identifier
                            Dim _left = New IdentifierExpression(_MatchToken(SyntaxType._Identifier))
                            CursorPosition += 1
                            Return _left
                    End Select


                    _Diagnostics.Add("unknown _IdentifierExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function
                Public Function _NumericLiteralExpression()
                    Dim NewNode As SyntaxToken
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Integer
                            'Consume Token
                            NewNode = _MatchToken(SyntaxType._Integer)
                            CursorPosition += 1
                            Return New NumericalExpression(NewNode)
                        Case SyntaxType._Decimal
                            'Consume Token
                            NewNode = _MatchToken(SyntaxType._Decimal)
                            CursorPosition += 1
                            Return New NumericalExpression(NewNode)
                    End Select
                    _Diagnostics.Add("unknown _NumericLiteralExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return Nothing
                End Function
#End Region
            End Class
        End Namespace
    End Namespace

End Namespace

'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Tokenizer\Parser.vb
'
' summary:	Parser class
'---------------------------------------------------------------------------------------------------

Imports System.Web
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.Typing
Imports Microsoft.VisualBasic.CompilerServices

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Tokenizer

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   A parser. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Friend Class Parser
                ''' <summary>   An enum constant representing the parser diagnostics option. </summary>
                Public ParserDiagnostics = New List(Of DiagnosticsException)
#Region "Propertys"
                ''' <summary>   The script. </summary>
                Private _Script As String
                ''' <summary>   The tree. </summary>
                Private _Tree As List(Of SyntaxToken)
                ''' <summary>   The cursor position. </summary>
                Private CursorPosition As Integer = 0
                ''' <summary>   The diagnostics. </summary>
                Public _Diagnostics As New List(Of String)
                ''' <summary>   The eot cursor position. </summary>
                Private EOT_CursorPosition As Integer = 0

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the lookahead. </summary>
                '''
                ''' <value> The lookahead. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public ReadOnly Property Lookahead As String
                    Get
                        If EOT_CursorPosition <= CursorPosition + 1 Then
                            Return _Tree(CursorPosition + 1)._Value
                        Else
                            Return "EOF"
                        End If

                    End Get
                End Property

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the lookahead token. </summary>
                '''
                ''' <value> The lookahead token. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public ReadOnly Property LookaheadToken As SyntaxToken
                    Get
                        If CursorPosition + 1 <= EOT_CursorPosition Then
                            Return _Tree(CursorPosition + 1)
                        Else
                            Return _Tree(EOT_CursorPosition)
                        End If

                    End Get
                End Property

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the type of the lookahead syntax. </summary>
                '''
                ''' <value> The type of the lookahead syntax. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the current token. </summary>
                '''
                ''' <value> The current token. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private ReadOnly Property CurrentToken As SyntaxToken
                    Get
                        Return _Peek(0)
                    End Get
                End Property
#End Region
#Region "Functions"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Returns the top-of-stack object without removing it. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="offset">   The offset. </param>
                '''
                ''' <returns>   The current top-of-stack object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Constructor. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
                '''                                             null. </exception>
                '''
                ''' <param name="Script">   [in,out] The script. </param>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Constructor. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="TokenTree">    [in,out] The token tree. </param>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub New(ByRef TokenTree As List(Of SyntaxToken))
                    _Tree = TokenTree
                    EOT_CursorPosition = _Tree.Count
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>
                ''' Lex tokens. from the tokenizer producing a full set of tokenized tokens When parsing we parse
                ''' the token tree.
                ''' </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Sub _LexTokens()
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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the next token. from the token tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The next token. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _GetNextToken() As SyntaxToken
                    Dim iCurrentToken = CurrentToken
                    CursorPosition += 1
                    Return iCurrentToken
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Match token. from the tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="Expected"> [in,out] The expected. token. </param>
                '''
                ''' <returns>   A SyntaxToken. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _MatchToken(ByRef Expected As SyntaxType) As SyntaxToken
                    Dim iCurrentToken As SyntaxToken
                    iCurrentToken = CurrentToken
                    Try
                        If iCurrentToken._SyntaxType = Expected Then
                            'get Expected token
                            Return _GetNextToken()
                        Else
                            'To be removed (over time)
                            _Diagnostics.Add("Unexpected Token in input: '" & iCurrentToken._SyntaxTypeStr & "' at Position : " & CursorPosition & vbNewLine &
                                     " Expected : " & Expected.ToString & vbNewLine)
                            'Generate Token (Expected Token)
                            Dim z = New SyntaxToken(Expected, "Generated", Nothing, Expected.GetSyntaxTypeStr, CursorPosition, CursorPosition)
                            Dim x As New DiagnosticsException("Unexpected Token in input: '" & iCurrentToken._SyntaxTypeStr & "' at Position : " & CursorPosition & vbNewLine &
                                     " Expected : " & Expected.ToString & vbNewLine, ExceptionType.UnabletoParseError, CurrentToken.ToJson, CurrentToken._SyntaxType)
                            Return z
                        End If
                    Catch ex As Exception
                        'MustbeNothing
                    End Try
                    'MustbeNothing
                    Return Nothing
                End Function
#End Region
#Region "Parser"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parse syntax tree. Main Entry Point. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ParseSyntaxTree() As SyntaxTree
                    Return _ExpressionSyntaxTree()
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parses the given language. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="Lang"> (Optional) The language. </param>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parse logo.Languge Still to be implemented. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ParseLogo() As SyntaxTree
                    Return _LogoExpressionSyntaxTree()
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parse SAL ASSMEBLY LANG. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ParseSal() As SyntaxTree
                    Return _SalExpressionSyntaxTree()
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parse basic.Lang. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ParseBasic() As SyntaxTree
                    Return _BasicExpressionSyntaxTree()
                End Function

#End Region
#Region "SAL"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Convert assignment operator. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Str">  [in,out] The string. </param>
                '''
                ''' <returns>   The assignment converted operator. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ConvertAssignmentOperator(ByRef Str As String) As String
                    Str = Str.Replace("<=", "_IS_LTE")
                    Str = Str.Replace("+=", "_INCR")
                    Str = Str.Replace("-=", "_DECR")
                    Str = Str.Replace(">=", "_IS_GTE")
                    Str = Str.Replace("<", "_IS_LT")
                    Str = Str.Replace(">", "_IS_GT")
                    Str = Str.Replace("==", "_IS_EQ")
                    Str = Str.Replace("+", "_ADD")
                    Str = Str.Replace("-", "_SUB")
                    Str = Str.Replace("/", "_DIV")
                    Str = Str.Replace("*", "_MUL")
                    Str = Str.Replace("=", "_ASSIGNS")
                    Return UCase(Str)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Sal expression list. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   A List(Of SyntaxNode) </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _SalExpressionList() As List(Of SyntaxNode)
                    Dim Body As New List(Of SyntaxNode)
                    Do While CursorPosition < EOT_CursorPosition
                        Body.Add(SalPrimeExpression)
                    Loop
                    Return Body

                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Sal prime expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   A SalExpressionSyntax. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function SalPrimeExpression() As SalExpressionSyntax
                    Select Case CurrentToken._SyntaxType

#Region "Binary Operations"




'Binary Compare
                        Case SyntaxType.SAL_IS_EQ
                        Case SyntaxType.SAL_IS_GT
                        Case SyntaxType.SAL_IS_GTE
                        Case SyntaxType.SAL_IS_LT
                        Case SyntaxType.SAL_IS_LTE
'Sal Binary Maths
                        Case SyntaxType.SAL_ADD
                        Case SyntaxType.SAL_SUB
                        Case SyntaxType.SAL_DIV
                        Case SyntaxType.SAL_MUL
'Jump
                        Case SyntaxType.SAL_JIF_EQ
                        Case SyntaxType.SAL_JIF_F
                        Case SyntaxType.SAL_JIF_GT
                        Case SyntaxType.SAL_JIF_LT
                        Case SyntaxType.SAL_JIF_T


'Boolean
                        Case SyntaxType.SAL_TO_POS
                        Case SyntaxType.SAL_TO_NEG
#End Region

#Region "SAL FUNCTIONS"

'Functions
                        Case SyntaxType.SAL_CALL
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_CALL))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_LOAD
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_LOAD))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_STORE
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_STORE))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_REMOVE
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_REMOVE))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)



                        Case SyntaxType.SAL_JMP
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_JMP))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_PUSH
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_PUSH))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_PULL
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_PULL))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_PEEK
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_PEEK))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_POP
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_POP))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _NumericLiteralExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_PRINT_C
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_PRINT_C))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _StringExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_PRINT_M
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_PRINT_M))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _StringExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)

                        Case SyntaxType.SAL_DECR
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_DECR))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _IdentifierExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
                        Case SyntaxType.SAL_INCR
                            Dim Cmd = New SalCommandExpression(_MatchToken(SyntaxType.SAL_INCR))
                            CursorPosition += 2
                            Dim Numbe = New SalLiteralExpression(SyntaxType._Integer, _IdentifierExpression)
                            Return New SalFunctionExpression(SyntaxType.SAL_JMP, Cmd, Numbe)
'Comands
                        Case SyntaxType.SAL_RESUME
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_RESUME))
                        Case SyntaxType.SAL_PAUSE
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_PAUSE))
                        Case SyntaxType.SAL_WAIT
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_WAIT))
                        Case SyntaxType.SAL_DUP
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_DUP))
                        Case SyntaxType.SAL_HALT
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_HALT))
                        Case SyntaxType.SAL_RET
                            Return New SalCommandExpression(_MatchToken(SyntaxType.SAL_RET))

#End Region

'Identification
                        Case SyntaxType._Sal_Program_title
                        Case SyntaxType._SAL_PROGRAM_BEGIN

                        Case Else
                            Return Nothing
                    End Select
                    Return Nothing
                End Function
#End Region

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Check logo statement. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="_left">    [in,out] The left. </param>
                '''
                ''' <returns>   A SyntaxType. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function CheckLogoStatement(ByRef _left As String) As SyntaxType
                    'Check if it is a left hand cmd
                    Select Case LCase(_left)
                        Case "ht"
                            Return SyntaxType.LOGO_ht
                        Case "hideturtle"
                            Return SyntaxType.LOGO_ht
                        Case "fd"
                            Return SyntaxType.LOGO_fd
                        Case "forward"
                            Return SyntaxType.LOGO_fd
                        Case "bk"
                            Return SyntaxType.LOGO_bk
                        Case "backward"
                            Return SyntaxType.LOGO_bk
                        Case "rt"
                            Return SyntaxType.LOGO_rt
                        Case "right"
                            Return SyntaxType.LOGO_rt
                        Case "lt"
                            Return SyntaxType.LOGO_lt
                        Case "left"
                            Return SyntaxType.LOGO_lt
                        Case "label"
                            Return SyntaxType.LOGO_label
                        Case "if"
                            Return SyntaxType.IfKeyword
                        Case "for"
                            Return SyntaxType.ForKeyword
                        Case "deref"
                            Return SyntaxType.LOGO_deref
                        Case "setxy"
                            Return SyntaxType.LOGO_setxy
                        Case "st"
                            Return SyntaxType.LOGO_st
                        Case "stop"
                            Return SyntaxType.LOGO_Stop
                        Case "pu"
                            Return SyntaxType.LOGO_pu
                        Case "pd"
                            Return SyntaxType.LOGO_pd
                        Case "make"
                            Return SyntaxType.LOGO_make
                        Case Else
                            'Must be a variable
                            Return False
                    End Select

                    Return Nothing
                End Function
#Region "Basic _ExpressionSyntaxTree"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>
                ''' Expression syntax tree. General Entrys Point Given a SyntaxType StarLang type.
                ''' </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _ExpressionSyntaxTree() As SyntaxTree
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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Sal expression syntax tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _SalExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, _SalExpressionList, _Tree, _Diagnostics)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Basic expression syntax tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _BasicExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Tree, _Diagnostics)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Logo expression syntax tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _LogoExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Tree, _Diagnostics)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   General expression syntax tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxTree. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _GeneralExpressionSyntaxTree() As SyntaxTree
                    Return New SyntaxTree(_Script, ExpressionList, _Tree, _Diagnostics)
                End Function
#End Region
#Region "Basic Language"
#Region "MAIN_EXPRESSIONS"

#Region "Expression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Expression list. Produces the list required by the main syntax tree. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A List(Of SyntaxNode) </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ExpressionList() As List(Of SyntaxNode)
                    Dim Lst As New List(Of SyntaxNode)
                    Do While CursorPosition < EOT_CursorPosition
                        Lst.Add(_Expression())
                        CursorPosition += 1
                    Loop


                    Return Lst
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Expressions this. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _Expression()

                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.VarKeyword
                            Return _VariableDeclarationExpression()
                        Case SyntaxType.DimKeyword
                            Return _VariableDeclarationExpression()

                        Case SyntaxType.IfKeyword
                            Return _IfExpression()
                        Case SyntaxType._WhitespaceToken
                            CursorPosition += 1
                            Return _Expression()
                        Case Else

                            Return _PrimaryExpression()
                    End Select

                End Function
#End Region

#Region "Unary Expression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Unary expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Operator">    [in,out] The operator. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _UnaryExpression(ByRef _Operator As SyntaxToken) As ExpressionSyntaxNode
                    Dim x = New UnaryExpression(_Operator, _NumericLiteralExpression)
                    CursorPosition += 2
                    Return x
                End Function
#End Region

#Region "Prime Expression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Primary expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _PrimaryExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._WhitespaceToken
                            'Manage Newline
                            _MatchToken(SyntaxType._WhitespaceToken)
                            CursorPosition += 1
                            Return _Expression()
                        Case SyntaxType._leftParenthes
                            _MatchToken(SyntaxType._leftParenthes)

                            Return _Expression()
                        Case SyntaxType._RightParenthes
                            _MatchToken(SyntaxType._RightParenthes)

                            Return _Expression()
#Region "UnaryValues"


'Left hand ; Unary Literals
                        'Case SyntaxType.Add_Operator
                        '    If LookaheadSyntaxType = SyntaxType._Integer Then
                        '        _Left = _UnaryExpression(_MatchToken(SyntaxType.Add_Operator))

                        '        Return _LeftHandExpression(_Left)
                        '    End If
                        'Case SyntaxType.Sub_Operator
                        '    If LookaheadSyntaxType = SyntaxType._Integer Then
                        '        _Left = _UnaryExpression(_MatchToken(SyntaxType.Sub_Operator))
                        '        CursorPosition += 1
                        '        Return _LeftHandExpression(_Left)
                        '    End If
#End Region

'Left hand Literals; may have following arguments
'Task Assignment
                        Case SyntaxType._Identifier
                            _Left = _IdentifierExpression()
                            Return _LeftHandExpression(_Left)



#Region "Literals"

                        Case SyntaxType._Integer, SyntaxType._Decimal
                            Return _BinaryExpression()

'Literals(right hand)

                        Case SyntaxType._String
                            _Left = _LiteralExpression()
                            CursorPosition += 1
                            Return _Left
                            'True/False is handled in Typing / and binding later
                        Case SyntaxType.TrueKeyword
                            _Left = _LiteralExpression()
                            CursorPosition += 1
                            Return _Left
                              'True/False is handled in Typing / and binding later
                        Case SyntaxType.FalseKeyword
                            _Left = _LiteralExpression()
                            CursorPosition += 1
                            Return _Left
                        Case SyntaxType._Date
                            _Left = _LiteralExpression()
                            CursorPosition += 1
                            Return _Left
#End Region

                    End Select
                    ' _Diagnostics.Add("unknown _PrimaryExpression? " & vbNewLine & CurrentToken.ToJson)
                    Return _BinaryExpression()
                End Function

#End Region

#Region "Left hand Functional Expressions"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Left hand expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _LeftHandExpression(ByRef _left As ExpressionSyntaxNode) As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                     'Simple Assign
                        Case SyntaxType._ASSIGN
                            Return _AssignmentExpression(_left)
                            'Complex Assign
                        Case SyntaxType.Multiply_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Divide_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Add_Equals_Operator
                            Return _AssignmentExpression(_left)
                        Case SyntaxType.Minus_Equals_Operator
                            Return _AssignmentExpression(_left)
                    End Select
                    'If not _LeftHandExpression then send value to binary Expression to continue
                    Return _BinaryExpression(_left)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Assignment expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_left">    [in,out] The left. </param>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _AssignmentExpression(ByRef _left As ExpressionSyntaxNode)
                    'Simple Assign

                    Select Case CurrentToken._SyntaxType
                     'Simple Assign
                        Case SyntaxType._ASSIGN
                            Return _SimpleAssignmentExpression(_left)
                            'Complex Assign
                        Case SyntaxType.Multiply_Equals_Operator
                            Return _ComplexAssignmentExpression(_left)
                        Case SyntaxType.Divide_Equals_Operator
                            Return _ComplexAssignmentExpression(_left)
                        Case SyntaxType.Add_Equals_Operator
                            Return _ComplexAssignmentExpression(_left)
                        Case SyntaxType.Minus_Equals_Operator
                            Return _ComplexAssignmentExpression(_left)
                    End Select

                    _Diagnostics.Add("unknown _AssignmentExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim xz As New DiagnosticsException("Error Unknown _AssignmentExpression: ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(xz)
                    Return Nothing
                End Function

#End Region

#Region "Binary Expression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Binary expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _BinaryExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    _Left = _LiteralExpression()

                    Select Case CurrentToken._SyntaxType
#Region "MathsExpression"
                        Case SyntaxType.Add_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Operator
                                Return _AddativeExpression(_Left)
                            End While
                        Case SyntaxType.Multiply_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Operator
                                Return _MultiplicativeExpression(_Left)
                            End While
                        Case SyntaxType.Sub_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Sub_Operator
                                Return _AddativeExpression(_Left)
                            End While
                        Case SyntaxType.Divide_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Operator
                                Return _MultiplicativeExpression(_Left)
                            End While
#End Region
#Region "Comparison Expression"

                        Case SyntaxType.GreaterThan_Operator
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThan_Operator
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.LessThanOperator
                            While CurrentToken._SyntaxType = SyntaxType.LessThanOperator
                                Return _ComparisonExpression(_Left)
                            End While

                        Case SyntaxType.EquivelentTo
                            While CurrentToken._SyntaxType = SyntaxType.EquivelentTo
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.NotEqual
                            While CurrentToken._SyntaxType = SyntaxType.NotEqual
                                Return _ComparisonExpression(_Left)
                            End While

                        Case SyntaxType.GreaterThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThanEquals
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.LessThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.LessThanEquals
                                Return _ComparisonExpression(_Left)
                            End While
#End Region
#Region "Assignment Expressions"
                        Case SyntaxType.Add_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Minus_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Minus_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Multiply_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Divide_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType._ASSIGN
                            While CurrentToken._SyntaxType = SyntaxType._ASSIGN
                                Return _LeftHandExpression(_Left)
                            End While

#End Region
                    End Select

                    Return _Left
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Binary expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _BinaryExpression(ByRef _Left As ExpressionSyntaxNode) As ExpressionSyntaxNode

                    Select Case CurrentToken._SyntaxType
#Region "Maths Expression"
                        Case SyntaxType.Add_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Operator
                                Return _AddativeExpression(_Left)
                            End While
                        Case SyntaxType.Multiply_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Operator
                                Return _MultiplicativeExpression(_Left)
                            End While

                        Case SyntaxType.Sub_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Sub_Operator
                                Return _AddativeExpression(_Left)
                            End While
                        Case SyntaxType.Divide_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Operator
                                Return _MultiplicativeExpression(_Left)
                            End While
#End Region
#Region "Comparison Expressions"

                        Case SyntaxType.GreaterThan_Operator
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThan_Operator
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.LessThanOperator
                            While CurrentToken._SyntaxType = SyntaxType.LessThanOperator
                                Return _ComparisonExpression(_Left)
                            End While

                        Case SyntaxType.EquivelentTo
                            While CurrentToken._SyntaxType = SyntaxType.EquivelentTo
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.NotEqual
                            While CurrentToken._SyntaxType = SyntaxType.NotEqual
                                Return _ComparisonExpression(_Left)
                            End While

                        Case SyntaxType.GreaterThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThanEquals
                                Return _ComparisonExpression(_Left)
                            End While
                        Case SyntaxType.LessThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.LessThanEquals
                                Return _ComparisonExpression(_Left)
                            End While
#End Region
#Region "Assignment Expressions"
                        Case SyntaxType.Add_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Minus_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Minus_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Multiply_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType.Divide_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Equals_Operator
                                Return _LeftHandExpression(_Left)
                            End While
                        Case SyntaxType._ASSIGN
                            While CurrentToken._SyntaxType = SyntaxType._ASSIGN
                                Return _LeftHandExpression(_Left)
                            End While

#End Region

                    End Select
                    Return _Left
                End Function

#Region "ComparisonExpression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Comparison expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ComparisonExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    _Left = _LiteralExpression()

                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.GreaterThan_Operator
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThan_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.LessThanOperator
                            While CurrentToken._SyntaxType = SyntaxType.LessThanOperator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.NotEqual
                            While CurrentToken._SyntaxType = SyntaxType.NotEqual
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.EquivelentTo
                            While CurrentToken._SyntaxType = SyntaxType.EquivelentTo
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While

                        Case SyntaxType.GreaterThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThanEquals
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While

                        Case SyntaxType.LessThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.LessThanEquals
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                    End Select
                    Return _Left
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Comparison expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ComparisonExpression(ByRef _Left As ExpressionSyntaxNode) As ExpressionSyntaxNode
                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.GreaterThan_Operator
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThan_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.LessThanOperator
                            While CurrentToken._SyntaxType = SyntaxType.LessThanOperator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.NotEqual
                            While CurrentToken._SyntaxType = SyntaxType.NotEqual
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.EquivelentTo
                            While CurrentToken._SyntaxType = SyntaxType.EquivelentTo
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.LessThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.LessThanEquals
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.GreaterThanEquals
                            While CurrentToken._SyntaxType = SyntaxType.GreaterThanEquals
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.ConditionalExpression
                                _Left._SyntaxTypeStr = SyntaxType.ConditionalExpression.GetSyntaxTypeStr

                                Return _Left
                            End While


                    End Select
                    Return _Left
                End Function

#End Region
#Region "AddativeExpression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Addative expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _AddativeExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    _Left = _LiteralExpression()

                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.Add_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.AddativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.AddativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While

                        Case SyntaxType.Sub_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Sub_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                Return _Left
                            End While

                    End Select


                    Return _Left
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Addative expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _AddativeExpression(ByRef _Left As ExpressionSyntaxNode) As ExpressionSyntaxNode
                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.Add_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.AddativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.AddativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Add_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Add_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.AddativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.AddativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Sub_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Sub_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.AddativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.AddativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Minus_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Minus_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.AddativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.AddativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                    End Select

                    Return _Left
                End Function

#End Region
#Region "MultiplicativeExpression"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Multiplicative expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _MultiplicativeExpression() As ExpressionSyntaxNode
                    Dim _Left As ExpressionSyntaxNode
                    _Left = _LiteralExpression()

                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.Multiply_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Multiply_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Divide_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While

                        Case SyntaxType.Divide_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                    End Select


                    Return _Left
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Multiplicative expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _MultiplicativeExpression(ByRef _Left As ExpressionSyntaxNode) As ExpressionSyntaxNode
                    Dim _Operator As New SyntaxToken
                    Dim _right As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.Multiply_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Multiply_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Multiply_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Divide_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                        Case SyntaxType.Divide_Equals_Operator
                            While CurrentToken._SyntaxType = SyntaxType.Divide_Equals_Operator
                                _Operator = _GetNextToken()
                                _right = _BinaryExpression()
                                _Left = New BinaryExpression(_Left, _right, _Operator)
                                _Left._SyntaxType = SyntaxType.MultiplicativeExpression
                                _Left._SyntaxTypeStr = SyntaxType.MultiplicativeExpression.GetSyntaxTypeStr

                                Return _Left
                            End While
                    End Select

                    Return _Left
                End Function

#End Region
#End Region

#End Region
#Region "Blocks"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Code block expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _CodeBlockExpression() As ExpressionSyntaxNode
                    Dim Body As New List(Of ExpressionSyntaxNode)
                    Dim y = _MatchToken(SyntaxType._CODE_BEGIN)
                    '  CursorPosition += 1
                    Do Until CurrentToken._SyntaxType = SyntaxType._CODE_END
                        'If CurrentToken._SyntaxType = SyntaxType._WhitespaceToken Then
                        '    CursorPosition += 1
                        'End If

                        Body.Add(_Expression)


                    Loop
                    CursorPosition += 1
                    Return New CodeBlockExpression(Body)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>
                ''' Identifier assignment list expression.
                ''' 
                ''' 
                ''' 
                ''' Syntax [a=3,b=4+3,c=4>5].
                ''' </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _IdentifierAssignmentListExpression() As ExpressionSyntaxNode
                    Dim Body As New List(Of ExpressionSyntaxNode)
                    _MatchToken(SyntaxType._LIST_BEGIN)
                    CursorPosition += 1
                    Do Until CurrentToken._SyntaxType = SyntaxType._LIST_END

                        Body.Add(_SimpleAssignmentExpression(_IdentifierExpression))
                        Dim sperator = _MatchToken(SyntaxType._LIST_SEPERATOR)
                        CursorPosition += 1
                    Loop
                    _MatchToken(SyntaxType._LIST_END)
                    CursorPosition += 1

                    Return New CodeBlockExpression(Body)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Literal list expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _LiteralListExpression() As ExpressionSyntaxNode
                    Dim Body As New List(Of ExpressionSyntaxNode)
                    _MatchToken(SyntaxType._LIST_BEGIN)
                    CursorPosition += 1
                    Do Until CurrentToken._SyntaxType = SyntaxType._LIST_END

                        Body.Add(_PrimaryExpression)
                    Loop
                    _MatchToken(SyntaxType._LIST_END)
                    CursorPosition += 1

                    Return New CodeBlockExpression(Body)
                End Function

#Region "If/Then"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Parenthesized expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ParenthesizedExpression() As ExpressionSyntaxNode
                    Dim Body As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._leftParenthes
                            Dim lft = _MatchToken(SyntaxType._leftParenthes)
                            Body = _BinaryExpression()
                            _MatchToken(SyntaxType._RightParenthes)
                            CursorPosition += 1
                            Return New ParenthesizedExpression(Body)

                    End Select

                    _MatchToken(SyntaxType._RightParenthes)
                    CursorPosition += 1
                    Return _BinaryExpression()

                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   If expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _IfExpression() As ExpressionSyntaxNode
                    Dim ThenExpression As CodeBlockExpression
                    Dim elseExpression As CodeBlockExpression
                    Dim IfCondition = _IfCondition()
                    ThenExpression = _ThenCodeBlockExpression()
                    If CursorPosition + 1 < EOT_CursorPosition Then
                        Select Case _Tree(CursorPosition + 1)._SyntaxType
                            Case SyntaxType.ElseKeyword
                                CursorPosition += 1
                                elseExpression = _ElseCodeBlockExpression()
                                Return New IfElseExpression(IfCondition, ThenExpression, elseExpression)
                            Case Else


                                Return New IfThenExpression(IfCondition, ThenExpression)
                        End Select
                    End If
                    Return New IfThenExpression(IfCondition, ThenExpression)

                    'If _Tree(CursorPosition + 1)._SyntaxType = SyntaxType.ElseKeyword = True Then

                    '    CursorPosition += 1
                    '    elseExpression = _ElseCodeBlockExpression()
                    '    Return New IfElseExpression(IfCondition, ThenExpression, elseExpression)


                    'Else
                    '    Return New IfThenExpression(IfCondition, ThenExpression)
                    'End If

                    _Diagnostics.Add("unknown _IfExpression ? " & vbNewLine & CurrentToken.ToJson)
                    Dim xz As New DiagnosticsException("Error Unknown _IfExpression: ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(xz)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   If condition. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A BinaryExpression. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _IfCondition() As ExpressionSyntaxNode
                    Dim x = _MatchToken(SyntaxType.IfKeyword)
                    CursorPosition += 1
                    Do
                        Select Case CurrentToken._SyntaxType
                            Case SyntaxType._WhitespaceToken
                                _MatchToken(SyntaxType._WhitespaceToken)
                                CursorPosition += 1
                            Case Else
                                Return _ComparisonExpression()
                        End Select
                    Loop

                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Then code block expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ThenCodeBlockExpression() As ExpressionSyntaxNode
                    CursorPosition += 1
                    Dim x = _MatchToken(SyntaxType.ThenKeyword)
                    CursorPosition += 1

                    Return _CodeBlockExpression()

                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Else code block expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ElseCodeBlockExpression() As ExpressionSyntaxNode
                    CursorPosition += 1
                    Dim x = _MatchToken(SyntaxType.ElseKeyword)
                    CursorPosition += 1

                    Return _CodeBlockExpression()


                End Function
#End Region
#Region "Do/While/Until"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the do. Cmd. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _Do() As ExpressionSyntaxNode
                    _MatchToken(SyntaxType.DoKeyword)
                    CursorPosition += 1

                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.WhileKeyword
                            Dim cond = _ComparisonExpression()
                            Dim blk = _CodeBlockExpression()

                        Case SyntaxType.UntilKeyword
                            Dim cond = _ComparisonExpression()
                            Dim blk = _CodeBlockExpression()

                    End Select

                    Return Nothing
                End Function
#End Region
#Region "For/Next"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   For next. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ForNext()
                    _MatchToken(SyntaxType.ForKeyword)
                    _IdentifierExpression()
                    _MatchToken(SyntaxType._ASSIGN)
                    _NumericLiteralExpression()
                    _MatchToken(SyntaxType.ToKeyword)
                    _NumericLiteralExpression()
                    _CodeBlockExpression()
                    Dim x As New DiagnosticsException("unknown _ForNext? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Applies an operation to all items in this collection.'. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _ForEach()
                    _MatchToken(SyntaxType.ForKeyword)
                    _MatchToken(SyntaxType.EachKeyWord)
                    _IdentifierExpression()
                    _MatchToken(SyntaxType.InKeyWord)
                    _IdentifierExpression()
                    _CodeBlockExpression()
                    Dim x As New DiagnosticsException("unknown _ForEach? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function
#End Region
#End Region
#Region "Variables"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Simple assignment expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _SimpleAssignmentExpression(ByRef _Left As IdentifierExpression) As ExpressionSyntaxNode
                    Dim _Operator As SyntaxToken

                    Select Case CurrentToken._SyntaxType

                        Case SyntaxType._ASSIGN
                            _Operator = _MatchToken(SyntaxType._ASSIGN)
                            CursorPosition += 1
                            Dim _right = _BinaryExpression()
                            Return New AssignmentExpression(_Left, _Operator, _right)

                    End Select

                    'Todo: Requires Assignment Expression
                    ' 
                    _Diagnostics.Add("unknown _SimpleAssignmentExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _SimpleAssignmentExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Complex assignment expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="_Left">    [in,out] The left. </param>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _ComplexAssignmentExpression(ByRef _Left As IdentifierExpression) As ExpressionSyntaxNode
                    Dim _Operator As SyntaxToken

                    Select Case CurrentToken._SyntaxType

                        Case SyntaxType.Multiply_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Multiply_Equals_Operator)
                            CursorPosition += 1
                            Dim _right = _BinaryExpression()
                            Return New AssignmentExpression(_Left, _Operator, _right)
                        Case SyntaxType.Divide_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Divide_Equals_Operator)
                            CursorPosition += 1
                            Dim _right = _BinaryExpression()
                            Return New AssignmentExpression(_Left, _Operator, _right)
                        Case SyntaxType.Add_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Add_Equals_Operator)
                            CursorPosition += 1
                            Dim _right = _BinaryExpression()
                            Return New AssignmentExpression(_Left, _Operator, _right)
                        Case SyntaxType.Minus_Equals_Operator
                            _Operator = _MatchToken(SyntaxType.Minus_Equals_Operator)
                            CursorPosition += 1
                            Dim _right = _BinaryExpression()
                            Return New AssignmentExpression(_Left, _Operator, _right)

                    End Select

                    'Todo: Requires Assignment Expression
                    ' 
                    _Diagnostics.Add("unknown _ComplexAssignmentExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _ComplexAssignmentExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Variable declaration expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _VariableDeclarationExpression() As ExpressionSyntaxNode
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.DimKeyword
                            Dim _DimKeyword = _MatchToken(SyntaxType.DimKeyword)
                            CursorPosition += 1

                            Dim _left = _MatchToken(SyntaxType._Identifier)
                            CursorPosition += 1

                            Dim _AsKeyword = _MatchToken(SyntaxType.AsKeyWord)
                            CursorPosition += 1

                            'Whitespace
                            Dim _Type = _TypeExpression()

                            Return New VariableDeclarationExpression(SyntaxType._VariableDeclaration, SyntaxType._VariableDeclaration.GetSyntaxTypeStr, _left, _Type)
                        Case SyntaxType.VarKeyword
                            Dim _DimKeyword = _MatchToken(SyntaxType.VarKeyword)
                            CursorPosition += 1

                            Dim _left = _MatchToken(SyntaxType._Identifier)
                            CursorPosition += 1

                            Dim _AsKeyword = _MatchToken(SyntaxType.AsKeyWord)
                            CursorPosition += 1

                            'Whitespace
                            Dim _Type = _TypeExpression()
                            Return New VariableDeclarationExpression(SyntaxType._VariableDeclaration, SyntaxType._VariableDeclaration.GetSyntaxTypeStr, _left, _Type)

                    End Select
                    'TODO : Requires Deleration Expression 
                    _Diagnostics.Add("unknown _VariableDeclarationExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _VariableDeclarationExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Type expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A LiteralType. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function _TypeExpression() As LiteralType
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._BooleanType
                            _MatchToken(SyntaxType._BooleanType)
                            CursorPosition += 1
                            Return LiteralType._Boolean
                        Case SyntaxType._IntegerType
                            _MatchToken(SyntaxType._IntegerType)
                            CursorPosition += 1
                            Return LiteralType._Integer
                        Case SyntaxType._DecimalType
                            _MatchToken(SyntaxType._DecimalType)
                            CursorPosition += 1
                            Return LiteralType._Decimal
                        Case SyntaxType._StringType
                            _MatchToken(SyntaxType._StringType)
                            CursorPosition += 1
                            Return LiteralType._String
                        Case SyntaxType._ArrayType
                            _MatchToken(SyntaxType._ArrayType)
                            CursorPosition += 1
                            Return LiteralType._Array
                        Case SyntaxType._DateType
                            _MatchToken(SyntaxType._DateType)
                            CursorPosition += 1
                            Return LiteralType._Date
                        Case SyntaxType._NullType
                            _MatchToken(SyntaxType._NullType)
                            CursorPosition += 1
                            Return LiteralType._NULL
                        Case Else
                            CursorPosition += 1
                            Return LiteralType._NULL
                    End Select
                    _Diagnostics.Add("unknown _TypeExpression? " & vbNewLine & CurrentToken.ToJson)
                    Dim x As New DiagnosticsException("unknown _TypeExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    CursorPosition += 1
                    Return SyntaxType._null
                End Function
#End Region
#Region "Literals"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Literal expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An ExpressionSyntaxNode. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _LiteralExpression() As ExpressionSyntaxNode

                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Integer
                            Return _NumericLiteralExpression()
                        Case SyntaxType._Decimal
                            Return _NumericLiteralExpression()
                        Case SyntaxType._String
                            Return _StringExpression()
                        Case SyntaxType.TrueKeyword
                            Return _BooleanExpression()
                        Case SyntaxType.FalseKeyword
                            Return _BooleanExpression()
                        Case SyntaxType._LIST_BEGIN
                        Case SyntaxType._Date
                    End Select
                    _Diagnostics.Add("unknown _Literal? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _Literal? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Identifier expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   An IdentifierExpression. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _IdentifierExpression() As IdentifierExpression
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Identifier
                            Dim _left = New IdentifierExpression(_MatchToken(SyntaxType._Identifier))
                            CursorPosition += 1
                            Return _left
                    End Select

                    Dim x As New DiagnosticsException("unknown _IdentifierExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    _Diagnostics.Add("unknown _IdentifierExpression? " & vbNewLine & CurrentToken.ToString)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Numeric literal expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A NumericalExpression. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _NumericLiteralExpression() As NumericalExpression
                    Dim NewNode As SyntaxToken
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._Integer
                            'Consume Token
                            NewNode = _MatchToken(SyntaxType._Integer)

                            Return New NumericalExpression(NewNode)
                        Case SyntaxType._Decimal
                            'Consume Token
                            NewNode = _MatchToken(SyntaxType._Decimal)

                            Return New NumericalExpression(NewNode)
                    End Select
                    _Diagnostics.Add("unknown _NumericLiteralExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _NumericLiteralExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   String expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A StringExpression. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _StringExpression() As StringExpression
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType._String
                            Dim NewNode As SyntaxToken
                            NewNode = _MatchToken(SyntaxType._String)
                            CursorPosition += 1
                            Return New StringExpression(NewNode)
                    End Select
                    _Diagnostics.Add("unknown _StringExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _StringExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Boolean expression. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A BooleanLiteralExpression. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function _BooleanExpression() As BooleanLiteralExpression
                    Select Case CurrentToken._SyntaxType
                        Case SyntaxType.TrueKeyword
                            Dim NewNode As SyntaxToken
                            NewNode = _MatchToken(SyntaxType.TrueKeyword)
                            CursorPosition += 1
                            Return New BooleanLiteralExpression(NewNode)
                        Case SyntaxType.FalseKeyword
                            Dim NewNode As SyntaxToken
                            NewNode = _MatchToken(SyntaxType.FalseKeyword)
                            CursorPosition += 1
                            Return New BooleanLiteralExpression(NewNode)
                    End Select
                    _Diagnostics.Add("unknown _BooleanExpression? " & vbNewLine & CurrentToken.ToString)
                    Dim x As New DiagnosticsException("unknown _BooleanExpression? ", ExceptionType.UnabletoParseError, CurrentToken.ToJson, SyntaxType._UnknownToken)
                    ParserDiagnostics.add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets debugger display. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The debugger display. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
#End Region
#End Region
            End Class
        End Namespace
    End Namespace
End Namespace

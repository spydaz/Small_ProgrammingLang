Imports SDK.SmallProgLang.Ast_ExpressionFactory
Imports SDK.SmallProgLang.GrammarFactory

'THE PARSER - AST CREATOR
'
#Region "THE PARSER"
'Author : Leroy Samuel Dyer ("Spydaz")
'-------------------------------------
'NOTE: 

'MODEL_
'LEX _ PARSE _ EVAL 
Namespace SmallProgLang
    'LOGO
    '
    Namespace Compiler
        'Logo Parser
        '
        Public Class LogoParser
#Region "Propertys"
            Public ParserErrors As New List(Of String)
            ''' <summary>
            ''' Currently held script
            ''' </summary>
            Public iScript As String = ""
            ''' <summary>
            ''' To hold the look ahead value without consuming the value
            ''' </summary>
            Public Lookahead As String
            ''' <summary>
            ''' Tokenizer !
            ''' </summary>
            Dim Tokenizer As Lexer
            Private iProgram As AstProgram
            Public ReadOnly Property Program As AstProgram
                Get
                    Return iProgram
                End Get
            End Property
#End Region
            Public Function _Parse(ByRef nScript As String) As AstProgram
                Dim Body As List(Of Ast_ExpressionStatement)
                Body = New List(Of Ast_ExpressionStatement)
                Me.ParserErrors = New List(Of String)
                iScript = nScript.Replace(vbNewLine, ";")
                iScript = RTrim(iScript)
                iScript = LTrim(iScript)
                Tokenizer = New Lexer(iScript)
                'Dim TokType As GrammarFactory.Grammar.Type_Id


                'GetProgram
                iProgram = _LOGO_ProgramNode()


                'Preserve InClass
                Return iProgram
            End Function
            Public Function _LOGO_ProgramNode() As AstProgram
                Dim nde = New AstProgram(_StatementList)
                nde._Raw = iScript
                nde._Start = 0
                nde._End = iScript.Length
                nde._TypeStr = "LOGO PROGRAM"
                Return nde
            End Function

            '
            'Syntax;
            '
            '
            Public Function _StatementList() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)
                lst.AddRange(_LogoExpressionList)
                Return lst
            End Function
            Public Function _LogoExpressionList() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)
                Lookahead = Tokenizer.ViewNext
                Dim tok As Type_Id
                tok = Tokenizer.IdentifiyToken(Lookahead)
                Do Until tok = Type_Id._EOF
                    'DetectNewline
                    Select Case tok
                        Case Type_Id._WHITESPACE
                            _WhitespaceNode()
                        Case Else
                            lst.Add(_Expression)
                    End Select

                    tok = Tokenizer.IdentifiyToken(Lookahead)
                Loop
                Return lst
            End Function
            Public Function _Literal() As Ast_Literal
                Lookahead = Tokenizer.ViewNext
                Dim tok As Type_Id
                tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok


                    Case Type_Id._STRING
                        Return _StringLiteralNode()
                    Case Type_Id.LOGO_number
                        Return _NumericLiteralNode()
                    Case Else
                        Return New Ast_Logo_Value(AST_NODE._UnknownStatement, Lookahead)
                End Select
                Return Nothing
            End Function
            Public Function _Expression() As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim tok As Type_Id
                tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok

                    Case Type_Id.LOGO_name
                        Dim _Left = _IdentifierLiteralNode()
                        Return _ComandFunction(_Left)
                    Case Else
                        Return _PrimaryExpression()
                End Select
                Return Nothing
            End Function
            Public Function _PrimaryExpression() As AstExpression
                Return New Ast_Logo_Expression(_Literal)
            End Function
            Public Function _ComandFunction(ByRef _Left As Ast_LogoIdentifer) As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim tok As Type_Id
                tok = Tokenizer.IdentifiyToken(Lookahead)
                If tok = Type_Id._WHITESPACE Then
                    Do Until tok <> Type_Id._WHITESPACE
                        _WhitespaceNode()
                        tok = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                End If

                Select Case LCase(_Left._Name)
                    Case "ht"
                        Return New Ast_Logo_Expression(_Left)
                    Case "hideturtle"
                        Return New Ast_Logo_Expression(_Left)
                    Case "fd"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "forward"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "bk"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "backward"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "bk"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "backward"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "rt"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "right"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "lt"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "label"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "if"
                    Case "for"
                    Case "deref"
                    Case "setxy"
                        ' _Left, _
                        Dim _right_X = _NumericLiteralNode()
                        _WhitespaceNode()
                        Dim _right_Y = _NumericLiteralNode()

                    Case "st"
                        Return New Ast_Logo_Expression(_Left)
                    Case "stop"
                        Return New Ast_Logo_Expression(_Left)
                    Case "pu"
                        Return New Ast_Logo_Expression(_Left)
                    Case "pd"
                        Return New Ast_Logo_Expression(_Left)
                    Case "make"

                    Case Else

                End Select
                'Must be a variable
                Dim x = New Ast_Logo_Expression(_Left)
                x._Type = AST_NODE._UnknownStatement
                x._TypeStr = "_UnknownFunction"
                Return x

            End Function
            Public Function _GetAssignmentOperator() As String
                Dim str = Tokenizer.GetIdentifiedToken(Lookahead).Value
                str = str.Replace("\u003c", "<")
                str = str.Replace("\u003e", ">")
                ' \U003c < Less-than sign
                ' \U003e > Greater-than sign


                Return UCase(str)
            End Function
            Public Function _IdentifierLiteralNode() As Ast_Identifier
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)

                Dim nde = New Ast_Identifier(tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_name"

                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            Public Function __EndStatementNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._endStatement, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_endStatement"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Collects bad token
            ''' </summary>
            ''' <returns></returns>
            Public Function __UnknownStatementNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._UnknownStatement, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_UnknownStatement"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Used when data has already been collected
            ''' </summary>
            ''' <param name="ErrorTok"></param>
            ''' <returns></returns>
            Public Function __UnknownStatementNode(ByRef ErrorTok As Token) As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                Dim tok As Token = ErrorTok
                Dim nde = New Ast_Literal(AST_NODE._UnknownStatement, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_UnknownStatement"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Used to denote white space as it is often important later
            ''' Some Parsers ignore this token ; 
            ''' It is thought also; to be prudent to collect all tokens to let the Evaluator deal with this later
            ''' </summary>
            ''' <returns></returns>
            Public Function _WhitespaceNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._WhiteSpace, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_whitespace"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Syntax:
            ''' "hjk"
            ''' String Literal:
            '''  -String
            ''' </summary>
            ''' <returns></returns>
            Public Function _StringLiteralNode() As Ast_Logo_Value
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)

                Dim str As String = ""
                If tok.Value.Contains("'") Then
                    str = tok.Value.Replace("'", "")
                Else
                End If
                If tok.Value.Contains(Chr(34)) Then
                    str = tok.Value.Replace(Chr(34), "")
                End If

                Dim nde = New Ast_Logo_Value(AST_NODE._string, str)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_string"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Syntax:
            ''' 
            ''' Numeric Literal:
            '''  -Number
            ''' </summary>
            ''' <returns></returns>
            Public Function _NumericLiteralNode() As Ast_Literal
                Dim Str As Integer = 0
                ' Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                If Integer.TryParse(tok.Value, Str) = True Then
                    Dim nde = New Ast_Literal(AST_NODE._integer, Str)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_integer"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                Else
                    'Unable to parse default 0 to preserve node listeral as integer
                    Dim nde = New Ast_Literal(AST_NODE._integer, 0)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_integer"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                End If
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace
End Namespace
#End Region

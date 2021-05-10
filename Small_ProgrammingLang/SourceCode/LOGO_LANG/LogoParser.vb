Imports System.Linq.Expressions
Imports Microsoft.VisualBasic.CompilerServices
Imports SDK.AI_GRAMMAR_ANAYLIZER.KnowledgeStructures
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
            'Syntax
            '
            'Program : Body = StatementLists
            '
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
            'Syntax;
            '
            'Logo Expression; Logo expression;
            '
            Public Function _StatementList() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)
                lst.AddRange(_LogoStatements)
                Return lst
            End Function
            Public Function CheckLogoStatement(ByRef _left As String) As Boolean
                'Check if it is a left hand cmd
                Select Case LCase(_left)
                    Case "ht"
                        Return True
                    Case "hideturtle"
                        Return True
                    Case "fd"
                        Return True
                    Case "forward"
                        Return True
                    Case "bk"
                        Return True
                    Case "backward"
                        Return True
                    Case "rt"
                        Return True
                    Case "right"
                        Return True
                    Case "lt"
                        Return True
                    Case "label"
                        Return True
                    Case "if"
                        Return True
                    Case "for"
                        Return True
                    Case "deref"
                        Return True
                    Case "setxy"
                        Return True
                    Case "st"
                        Return True
                    Case "stop"
                        Return True
                    Case "pu"
                        Return True
                    Case "pd"
                        Return True
                    Case "make"
                        Return True
                    Case Else
                        'Must be a variable
                        Return False
                End Select

                Return Nothing
            End Function
            'Syntax
            '
            '
            'Logo Expression; Logo expression;
            'LogoEvaluation
            'LogoFunction
            'Literal
            Public Function _LogoStatements() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)

                Dim tok As Type_Id
                Lookahead = Tokenizer.ViewNext
                tok = Tokenizer.IdentifiyToken(Lookahead)

                Do Until tok = Type_Id._EOF


                    Select Case tok
                    'End of line
                        Case Type_Id.LOGO_EOL
                            __EndStatementNode()
                        Case Type_Id._VARIABLE
                            Dim _Left = _IdentifierLiteralNode()
                            'Check if it is a left hand cmd
                            If CheckLogoStatement(_Left._Name) = True Then
                                lst.Add(_ComandFunction(_Left))
                            Else
                                lst.Add(New Ast_Logo_Expression(_Left))
                            End If

                        Case Type_Id._STRING
                            lst.Add(New Ast_Logo_Expression(_StringLiteralNode()))
                        Case Type_Id._INTEGER
                            lst.Add(_EvaluationExpression)
                        Case Type_Id._WHITESPACE
                            _WhitespaceNode()
                        Case Type_Id._STATEMENT_END
                            __EndStatementNode()
                    End Select
                    Lookahead = Tokenizer.ViewNext
                    tok = Tokenizer.IdentifiyToken(Lookahead)
                Loop

                Return lst
            End Function
            'syntax
            '
            '
            'literal +-*/<>= Literal 
            '
            Public Function _EvaluationExpression() As AstExpression
                Dim _left As AstExpression
                Dim toktype As Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                'Remove Erronious WhiteSpaces
                If toktype = Type_Id._WHITESPACE Then
                    Do While toktype = Type_Id._WHITESPACE
                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                End If
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Primary
                _left = New Ast_Logo_Expression(_NumericLiteralNode())

                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Select Case toktype

                    Case Type_Id._ADDITIVE_OPERATOR

                        Return _Evaluation(_left)

                    Case Type_Id._MULTIPLICATIVE_OPERATOR

                        Return _Evaluation(_left)

                    Case Type_Id._RELATIONAL_OPERATOR

                        Return _Evaluation(_left)
                End Select
                'Simple Number
                Return _left
            End Function
            'syntax
            '
            '
            'literal +-*/<>= Literal 
            'Literal
            Public Function _Evaluation(ByRef _left As AstExpression)

                Dim _Operator As String = ""
                Dim _Right As AstExpression
                Dim toktype As Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                _Operator = _GetAssignmentOperator()
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                _Right = _EvaluationExpression()
                _left = New Ast_logoEvaluation(AST_NODE.Logo_Expression, _left, _Operator, _Right)
                _left._TypeStr = "EvaluationExpression"

                Return _left
            End Function
            'syntax
            '
            'identifier Value
            '
            '
            '
            Public Function _ComandFunction(ByRef _Left As Ast_Identifier) As Ast_LogoCmdExpression
                _WhitespaceNode()

                Select Case LCase(_Left._Name)
                    Case "ht"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "hideturtle"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "fd"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                    Case "forward"
                        Dim xde = New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, _NumericLiteralNode)
                        Return xde

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
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "if"
                    Case "for"
                    Case "deref"
                    Case "setxy"
                    Case "st"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "stop"
                    Case "pu"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "pd"
                        Return New Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left, __EndStatementNode)
                    Case "make"
                    Case Else

                End Select
                'Must be a variable
                '  Return Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left)
                Return Nothing
            End Function
            'syntax
            '
            ' +-*/=<>
            '
            '
            Public Function _GetAssignmentOperator() As String
                Dim str = Tokenizer.GetIdentifiedToken(Lookahead).Value
                str = str.Replace("\u003c", "<")
                str = str.Replace("\u003e", ">")
                ' \U003c < Less-than sign
                ' \U003e > Greater-than sign


                Return UCase(str)
            End Function
            'syntax
            '
            'Identifier
            '
            '
            Public Function _IdentifierLiteralNode() As Ast_Identifier
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)

                Dim nde = New Ast_LogoIdentifer(tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_Identifer"

                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            'syntax
            '
            ';
            '
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
            'syntax:
            '
            'Unknown
            '
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
            'syntax
            '
            '" "
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
            'Syntax:
            '
            'String Literal
            '
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
            'syntax
            '
            'numeric integer
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
                    Dim nde = New Ast_Logo_Value(Str)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_integer"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                Else
                    'Unable to parse default 0 to preserve node listeral as integer
                    Dim nde = New Ast_Logo_Value(AST_NODE._integer, 0)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_integer"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                End If
            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace
#End Region

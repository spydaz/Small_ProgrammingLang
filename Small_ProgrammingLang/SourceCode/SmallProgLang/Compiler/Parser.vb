Imports System.Linq.Expressions
Imports System.Text.Json
Imports System.Web
Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic.CompilerServices
Imports SDK.Repl
Imports SDK.SmallProgLang
Imports SDK.SmallProgLang.Ast_ExpressionFactory
Imports SDK.SmallProgLang.GrammarFactory

'Author : Leroy Samuel Dyer ("Spydaz")
'-------------------------------------
'NOTE: 
'Loosly - Based on DIMTRY (Building a Parser from Scratch)
'This is a test of that style of AST creation _  
'MODEL_
'LEX _ PARSE _ EVAL 

Namespace SmallProgLang
    Namespace Compiler
        ''' <summary>
        ''' Known Langs
        ''' </summary>
        Public Enum ProgramLangs
            SAL = 1
            Small_PL = 2
            Unknown = 3
        End Enum


        ''' <summary>
        ''' Programming Language Parser to AST
        ''' </summary>
        Public Class Parser
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
#Region "PARSER FACTORY"
            ''' <summary>
            ''' Main Parser Function  
            ''' Parses whole Script into a AST tree ; 
            ''' Which can be used later for evaluation to be run on a vm 
            ''' or to generate code for a different language (interpretor) 
            ''' or (evaluator - Compiler(Executor)
            ''' </summary>
            ''' <param name="nScript">Script to be compiled </param>
            ''' <returns>AST PROGRAM</returns>
            <System.ComponentModel.Description("Main Parser Function Parses whole Script into a AST tree ; Which can be used later for evaluation to be run on a vm or to generate code for a different language (interpretor) or (evaluator - Compiler(Executor)")>
            Public Function _Parse(ByRef nScript As String) As AstProgram
                Dim Body As New List(Of Ast_ExpressionStatement)
                Me.ParserErrors = New List(Of String)
                iScript = nScript.Replace(vbNewLine, " ")
                iScript = RTrim(iScript)
                iScript = LTrim(iScript)

                'iScript = nScript.Replace(" ", "")
                'iScript = nScript.Replace(";", "")
                Tokenizer = New Lexer(iScript)
                'Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok
                    Case Grammar.Type_Id._SAL_PROGRAM_BEGIN
                        'Get title
                        Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        Tokenizer.IdentifiyToken(Lookahead)
                        'GetEmptystatement
                        Dim empt = Tokenizer.GetIdentifiedToken(Lookahead)
                        Tokenizer.IdentifiyToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        'GetProgram
                        iProgram = _SAL_ProgramNode()
                    Case Grammar.Type_Id._PL_PROGRAM_BEGIN
                        Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        Tokenizer.IdentifiyToken(Lookahead)
                        iProgram = _ProgramNode()
                    Case Else
                        'GetProgram
                        iProgram = _ProgramNode()
                End Select
                'Preserve InClass
                Return iProgram
            End Function
            ''' <summary>
            ''' Main Parser Function  
            ''' Parses whole Script into a AST tree ; 
            ''' Which can be used later for evaluation to be run on a vm 
            ''' or to generate code for a different language (interpretor) 
            ''' or (evaluator - Compiler(Executor)
            ''' </summary>
            ''' <param name="nScript">Script to be compiled </param>
            ''' <param name="nGrammar">Uses Custom Grammar to create tokens based on Stored Grammar ID's</param>
            ''' <returns>AST PROGRAM</returns>
            Public Function _Parse(ByRef nScript As String, ByRef nGrammar As List(Of GrammarFactory.Grammar)) As AstProgram
                Dim Body As New List(Of Ast_ExpressionStatement)
                Me.ParserErrors = New List(Of String)
                iScript = nScript.Replace(vbNewLine, " ")
                Tokenizer = New Lexer(iScript, nGrammar)
                'Dim TokType As GrammarFactory.Grammar.Type_Id
                ' uses the first token to determine the program type
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok
                    Case Grammar.Type_Id._SAL_PROGRAM_BEGIN
                        'Get title
                        Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        Tokenizer.IdentifiyToken(Lookahead)
                        'GetEmptystatement
                        Dim empt = Tokenizer.GetIdentifiedToken(Lookahead)
                        Tokenizer.IdentifiyToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        'GetProgram
                        iProgram = _SAL_ProgramNode()
                    Case Grammar.Type_Id._PL_PROGRAM_BEGIN
                        Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        Tokenizer.IdentifiyToken(Lookahead)
                        iProgram = _ProgramNode()
                    Case Else
                        'GetProgram
                        iProgram = _ProgramNode()
                End Select
                'Preserve InClass
                Return iProgram
            End Function
            Public Function ParseFactory(ByRef nScript As String, Optional PL As ProgramLangs = Nothing) As AstProgram
                Select Case PL
                    Case ProgramLangs.SAL
                        'Get title
                        Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        Tokenizer.IdentifiyToken(Lookahead)
                        'GetEmptystatement
                        Dim empt = Tokenizer.GetIdentifiedToken(Lookahead)
                        Tokenizer.IdentifiyToken(Lookahead)
                        Lookahead = Tokenizer.ViewNext
                        'GetProgram
                        iProgram = _SAL_ProgramNode()
                    Case ProgramLangs.Small_PL
                        iProgram = _ParsePL(nScript)
                    Case Nothing
                        iProgram = _Parse(nScript)
                    Case ProgramLangs.Unknown
                        iProgram = _Parse(nScript)
                    Case Else
                        iProgram = _Parse(nScript)
                End Select
                Return iProgram
            End Function
            Public Function _ParsePL(ByRef nScript As String) As AstProgram
                Dim Body As New List(Of Ast_ExpressionStatement)
                Me.ParserErrors = New List(Of String)
                iScript = nScript.Replace(vbNewLine, " ")
                'iScript = nScript.Replace(" ", "")
                'iScript = nScript.Replace(";", "")
                Tokenizer = New Lexer(iScript)
                'Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)

                'GetProgram
                iProgram = _ProgramNode()


                'Preserve InClass
                Return iProgram
            End Function
            Public Function _ParseSAL(ByRef nScript As String) As AstProgram
                Dim Body As New List(Of Ast_ExpressionStatement)
                Me.ParserErrors = New List(Of String)
                iScript = nScript.Replace(vbNewLine, " ")
                'iScript = nScript.Replace(" ", "")
                'iScript = nScript.Replace(";", "")
                Tokenizer = New Lexer(iScript)
                'Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)

                'Get title
                Dim Decl = Tokenizer.GetIdentifiedToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                Tokenizer.IdentifiyToken(Lookahead)
                'GetEmptystatement
                Dim empt = Tokenizer.GetIdentifiedToken(Lookahead)
                Tokenizer.IdentifiyToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                'GetProgram
                iProgram = _SAL_ProgramNode()


                'Preserve InClass
                Return iProgram
            End Function
#End Region
#Region "AstNode Handlers/Generators"
#Region "Main Program"
            ''' <summary>
            ''' Main Entry Point. 
            ''' Syntax:
            ''' 
            ''' Program:
            ''' -Literals
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _ProgramNode() As AstProgram
                Dim nde = New AstProgram(_StatementList)
                nde._Raw = iScript
                nde._Start = 0
                nde._End = iScript.Length
                nde._TypeStr = "PL PROGRAM"
                Return nde
            End Function
            Public Function _SAL_ProgramNode() As AstProgram
                Dim nde = New AstProgram(_SAL_StatementList)
                nde._Raw = iScript
                nde._Start = 0
                nde._End = iScript.Length
                nde._TypeStr = "SAL PROGRAM"
                Return nde
            End Function
            ''' <summary>
            ''' 
            ''' Syntax
            ''' -Statement
            ''' -Statementlist Statement -> Statement Statement Statement
            ''' </summary>
            ''' <returns></returns>
            Public Function _StatementList() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)

                Do While (Lookahead <> "EOF")
                    'CHECK IF ITS A SAL STATMENT
                    If Tokenizer.IdentifiyToken(Lookahead) = Grammar.Type_Id._SAL_EXPRESSION_BEGIN Then
                        Dim nde = _SAL_Expression()
                        If nde IsNot Nothing Then
                            lst.Add(nde)
                            Lookahead = Tokenizer.ViewNext

                        End If
                    Else
                        Dim nde = _Statement()
                        If nde IsNot Nothing Then
                            lst.Add(nde)
                            Lookahead = Tokenizer.ViewNext

                        End If
                    End If

                Loop
                Return lst
            End Function
#End Region
#Region "SAL_LITERALS"
            ''' <summary>
            ''' Sal Literals 
            ''' The SAL Assembly language is of Pure Literals;
            ''' Operators Also need to be handled as literals ;
            ''' Each Expresion Statement needs to be terminated with a HALT command
            ''' Sal Expressions are Inititiated as Statring with a "SAL" ending in "HALT"
            ''' All Captured between will be Directly by the SAL Virtual Machine Interpretor
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _SAL_Expression() As Ast_SalExpression
                Dim lst As New List(Of Ast_Literal)
                'First token SAL BEGIN
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                'End of Expression is "HALT"
                Do Until tok = Grammar.Type_Id.SAL_HALT
                    Lookahead = Tokenizer.ViewNext
                    tok = Tokenizer.IdentifiyToken(Lookahead)
                    Select Case tok
                        Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                        Case GrammarFactory.Grammar.Type_Id._INTEGER
                            Dim fnd = _NumericLiteralNode()
                            fnd._TypeStr = "_Integer"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._STRING
                            Dim fnd = _StringLiteralNode()
                            fnd._TypeStr = "_string"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._STATEMENT_END
                            lst.Add(__EmptyStatementNode())
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id._SAL_EXPRESSION_BEGIN
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE._Sal_BeginStatement, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "_Sal_BeginStatement"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "_Sal_BeginStatement"
                            '  lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id._SAL_PROGRAM_BEGIN
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE._Sal_Program_title, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "_SAL_PROGRAM_BEGIN"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "_SAL_PROGRAM_BEGIN"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_ADD
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_ADD, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "ADD"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "ADD"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_AND
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_AND, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "AND"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "AND"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_CALL
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_CALL, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "CALL"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "CALL"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_DECR
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_DECR, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "DECR"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "DECR"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_DIV
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_DIV, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "DIV"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "DIV"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_DUP
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_DUP, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "DUP"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "DUP"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_HALT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_HALT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "HALT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "HALT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                            Exit Select
                        Case Grammar.Type_Id.SAL_INCR
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_INCR, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "INCR"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "INCR"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_IS_EQ
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_IS_EQ, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "IS_EQ"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "IS_EQ"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_IS_GT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_IS_GT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "IS_GT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "IS_GT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_IS_GTE
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_IS_GTE, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "IS_GTE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "IS_GTE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_IS_LT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_IS_LT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "IS_LT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "IS_LT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_IS_LTE
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_IS_EQ, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "IS_LTE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "IS_LTE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JIF_EQ
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_EQ, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JIF_EQ"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JIF_EQ"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JIF_F
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_F, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JIF_F"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JIF_F"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JIF_GT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_GT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JIF_GT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JIF_GT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JIF_LT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_LT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JIF_LT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JIF_LT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JIF_T
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_T, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JIF_T"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JIF_T"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_JMP
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JMP, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "JMP"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "JMP"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_LOAD
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_LOAD, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "LOAD"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "LOAD"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_MUL
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_MUL, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "MUL"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "MUL"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_NOT
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_NOT, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "NOT"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "NOT"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_NULL
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_NULL, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "NULL"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "NULL"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_OR
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_OR, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "OR"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "OR"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PAUSE
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PAUSE, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PAUSE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PAUSE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PEEK
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PEEK, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PEEK"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PEEK"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PRINT_C
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PRINT_C, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PRINT_C"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PRINT_C"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PRINT_M
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PRINT_M, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PRINT_M"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PRINT_M"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PULL
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PULL, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PULL"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PULL"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_PUSH
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_PUSH, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "PUSH"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "PUSH"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_REMOVE
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_REMOVE, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "REMOVE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "REMOVE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_RESUME
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_RESUME, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "RESUME"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "RESUME"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_RET
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_RET, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "RET"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "RET"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_STORE
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_T, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "STORE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "STORE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_SUB
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_JIF_T, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "STORE"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "STORE"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_TO_NEG
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_TO_NEG, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "TO_NEG"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "TO_NEG"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id.SAL_TO_POS
                            Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                            Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_TO_POS, nTok.Value)
                            fnd._End = nTok._End
                            fnd._Raw = "TO_POS"
                            fnd._Start = nTok._start
                            fnd._TypeStr = "TO_POS"
                            lst.Add(fnd)
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                        Case Grammar.Type_Id._BAD_TOKEN
                            'Technically badtoken try capture
                            Dim etok = __UnknownStatementNode()
                            ParserErrors.Add("Unknown Statement/Expression Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput & vbNewLine)
                            lst.Add(etok)
                            Lookahead = Tokenizer.ViewNext
                            Lookahead = "EOF"
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                            'Set End of File
                            Return New Ast_SalExpression(lst)
                        Case Else
                            Lookahead = Tokenizer.ViewNext
                            tok = Tokenizer.IdentifiyToken(Lookahead)
                            Return New Ast_SalExpression(lst)

                    End Select
                Loop
                Lookahead = Tokenizer.ViewNext
                tok = Tokenizer.IdentifiyToken(Lookahead)
                If tok = Grammar.Type_Id.SAL_HALT Then
                    Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                    Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_HALT, nTok.Value)
                    fnd._End = nTok._End
                    fnd._Raw = "HALT"
                    fnd._Start = nTok._start
                    fnd._TypeStr = "HALT"
                    lst.Add(fnd)
                    Lookahead = Tokenizer.ViewNext
                    tok = Tokenizer.IdentifiyToken(Lookahead)
                    Dim stat = New Ast_SalExpression(lst)
                    For Each item In lst
                        stat._Raw &= item._Raw & " "
                    Next
                    Return stat
                Else
                    'Technically badtoken try capture
                    Dim etok = __UnknownStatementNode()
                    ParserErrors.Add("Missing Halt maker" & vbNewLine & etok.ToJson.FormatJsonOutput)
                    lst.Add(etok)
                    Dim stat = New Ast_SalExpression(lst)
                    For Each item In lst
                        stat._Raw &= item._Raw & " "
                    Next
                    Return stat
                End If
            End Function
            Public Function _SAL_StatementList() As List(Of AstExpression)
                Dim lst As New List(Of AstExpression)
                Do While (Tokenizer.ViewNext <> "EOF")
                    Dim nde = _SAL_Expression()
                    If nde IsNot Nothing Then
                        lst.Add(nde)
                    End If
                Loop
                Return lst
            End Function
#End Region
#Region "Literals"
            ''' <summary>
            ''' Syntax
            ''' 
            ''' -Literal => (_PrimaryExpression)
            ''' -EatExtra WhiteSpace
            ''' -EatExtra ";"
            ''' </summary>
            ''' <returns></returns>
            Public Function _PrimaryExpression() As AstExpression
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok
                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                    Case GrammarFactory.Grammar.Type_Id._STATEMENT_END
                        '  __EmptyStatementNode()
                        Dim temp = New Ast_ExpressionStatement(__EmptyStatementNode)
                        temp._TypeStr = "_PrimaryExpression"
                        Return temp

                    Case Else
                        'Literal - Node!
                        Dim Expr As Ast_ExpressionStatement
                        Dim nde = _literalNode()
                        If nde IsNot Nothing Then
                            Expr = New Ast_ExpressionStatement(nde)
                            'Advances to the next cursor
                            Lookahead = Tokenizer.ViewNext
                            Expr._TypeStr = "_PrimaryExpression"
                            Return Expr
                        Else
                            'Technically badtoken try capture
                            Dim etok = __UnknownStatementNode()
                            Lookahead = "EOF"
                            ParserErrors.Add("Unknown Statement/Expression/Function Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput.Replace("  ", "") & vbNewLine)
                            Dim Lit = New Ast_ExpressionStatement(etok)
                            Lit._TypeStr = "_PrimaryExpression"
                        End If
                        Exit Select

                End Select
                'Technically badtoken try capture
                Dim ertok = __UnknownStatementNode()
                Lookahead = "EOF"
                ParserErrors.Add("Unknown Statement/LiteralExpression Uncountered" & vbNewLine & ertok.ToJson.FormatJsonOutput.Replace("  ", "") & vbNewLine)
                Return New Ast_ExpressionStatement(ertok)
            End Function
            ''' <summary>
            ''' 
            ''' Syntax:
            ''' -EatWhiteSpace
            ''' -SalExpression
            ''' -ParenthesizedExpresion
            ''' -_VariableExpression
            ''' -_COMMENTS
            ''' _CommandFunction
            ''' -_BinaryExpression
            ''' 
            ''' 'Added Glitch(Select case on tokenvalue) ..... Not sure if it is the right way
            ''' as the variables are blocking the keywords?
            ''' </summary>
            ''' <returns></returns>
            Public Function _LeftHandExpression() As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)

                Select Case toktype
                    Case Grammar.Type_Id._VARIABLE
                        'Check if misIdentified
                        Dim iTok As Token = Tokenizer.CheckIdentifiedToken(Lookahead)
                        If CheckFunction(iTok.Value) = True Then
                            Return _CommandFunction()
                        Else
                            'Do Variable Expression
                            Return _BinaryExpression(_VariableExpression())
                        End If
                    Case Grammar.Type_Id._COMMENTS
                        Return _CommentsListExpression()
                    Case GrammarFactory.Grammar.Type_Id._SAL_EXPRESSION_BEGIN
                        Return _SAL_Expression()
                    Case GrammarFactory.Grammar.Type_Id._CONDITIONAL_BEGIN
                        Return _ParenthesizedExpression()
                    Case Else
                        'Must be a primaryExpression With binary
                        Return _BinaryExpression()
                End Select

                'Technically badtoken try capture
                Dim etok = __UnknownStatementNode()
                ParserErrors.Add("Unknown Statement/_LeftHandExpression Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput & vbNewLine)
                Return New Ast_ExpressionStatement(etok)
            End Function
            ''' <summary>
            ''' -Literals
            ''' Syntax:
            '''     
            '''     -Numeric Literal
            '''     -String Literal
            '''     -Comments
            '''     -Nullable
            '''     -BooleanLiteral
            '''     -ArrayLiteral
            '''     -EatWhiteSpace
            '''     -EatEmptyStatment
            ''' </summary>
            ''' <returns></returns>
            Public Function _literalNode() As Ast_Literal
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok
                    Case GrammarFactory.Grammar.Type_Id._INTEGER
                        Return _NumericLiteralNode()
                    Case GrammarFactory.Grammar.Type_Id._STRING
                        Return _StringLiteralNode()

                    'Case GrammarFactory.Grammar.Type_Id._VARIABLE
                    '    Dim ntok = Tokenizer.GetIdentifiedToken(Lookahead)
                    '    Dim xc = New Ast_Literal(AST_NODE._variable, ntok.Value)
                    '    xc._Start = ntok._start
                    '    xc._End = ntok._End
                    '    xc._Raw = ntok.Value
                    Case GrammarFactory.Grammar.Type_Id._LIST_BEGIN
                        Return _ArrayListLiteral()
                        Exit Select
                    Case GrammarFactory.Grammar.Type_Id._NULL
                        Return _NullableNode()
                    Case GrammarFactory.Grammar.Type_Id._TRUE
                        Return _BooleanNode()
                    Case GrammarFactory.Grammar.Type_Id._FALSE
                        Return _BooleanNode()
                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        Return _WhitespaceNode()
                    Case GrammarFactory.Grammar.Type_Id._STATEMENT_END
                        Return __EmptyStatementNode()
                    Case Grammar.Type_Id.SAL_HALT
                        Dim nTok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim fnd = New Ast_SAL_Literal(AST_NODE.SAL_HALT, nTok.Value)
                        fnd._End = nTok._End
                        fnd._Raw = "HALT"
                        fnd._Start = nTok._start
                        fnd._TypeStr = "HALT"

                        Lookahead = Tokenizer.ViewNext
                        tok = Tokenizer.IdentifiyToken(Lookahead)
                        Return fnd
                        Exit Select
                    Case Else
                        'Technically badtoken try capture
                        Dim etok = __UnknownStatementNode()
                        ParserErrors.Add("Unknown Literal Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput.Replace("  ", "") & vbNewLine)
                        Lookahead = "EOF"
                        Return etok
                End Select
                'Technically badtoken try capture
                Dim itok = __UnknownStatementNode()
                Lookahead = "EOF"
                ParserErrors.Add("Unknown Literal Uncountered" & vbNewLine & itok.ToJson.FormatJsonOutput.Replace("  ", "") & vbNewLine)
                Return itok
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
            ''' <summary>
            ''' Syntax:
            ''' 
            ''' Nullable Literal:
            '''  -Null
            ''' </summary>
            ''' <returns></returns>
            Public Function _NullableNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._null, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_null"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Used for end of statement
            ''' </summary>
            ''' <returns></returns>
            Public Function __EmptyStatementNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._emptyStatement, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_emptyStatement"
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
            ''' Used to Eat Node
            ''' </summary>
            ''' <returns></returns>
            Public Function _CodeBeginNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._Code_Begin, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_Code_Begin"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            Public Function _ConditionalBeginNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._OperationBegin, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_OperationBegin"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            Public Function _ListEndNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Dim x = Tokenizer.GetIdentifiedToken(Lookahead)
                'Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                'nde._Start = tok._start
                'nde._End = tok._End
                'nde._Raw = tok.Value
                'nde._TypeStr = "_Code_End"
                'Lookahead = Tokenizer.ViewNext
                Dim xDC = New Ast_Literal(AST_NODE._ListEnd)
                xDC._Start = x._start
                xDC._End = x._End
                xDC._Raw = x.Value
                Lookahead = Tokenizer.ViewNext
                Return xDC
            End Function
            Public Function _ListBeginNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Dim x = Tokenizer.GetIdentifiedToken(Lookahead)
                'Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                'nde._Start = tok._start
                'nde._End = tok._End
                'nde._Raw = tok.Value
                'nde._TypeStr = "_Code_End"
                'Lookahead = Tokenizer.ViewNext
                Dim xDC = New Ast_Literal(AST_NODE._ListEnd)
                xDC._Start = x._start
                xDC._End = x._End
                xDC._Raw = x.Value
                Lookahead = Tokenizer.ViewNext
                Return xDC
            End Function
            ''' <summary>
            ''' Used to Eat Node 
            ''' </summary>
            ''' <returns></returns>
            Public Function _CodeEndNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Dim x = Tokenizer.GetIdentifiedToken(Lookahead)
                'Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                'nde._Start = tok._start
                'nde._End = tok._End
                'nde._Raw = tok.Value
                'nde._TypeStr = "_Code_End"
                'Lookahead = Tokenizer.ViewNext
                Dim xDC = New Ast_Literal(AST_NODE._Code_End)
                xDC._Start = x._start
                xDC._End = x._End
                xDC._Raw = x.Value
                Lookahead = Tokenizer.ViewNext
                Return xDC
            End Function
            Public Function _ConditionalEndNode() As Ast_Literal
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._OperationEnd, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_OperationEnd"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            ''' <summary>
            ''' Used to return boolean literals if badly detected it will return false
            ''' </summary>
            ''' <returns></returns>
            Public Function _BooleanNode() As Ast_Literal
                Dim Str As Boolean = False

                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                If Boolean.TryParse(tok.Value, Str) = True Then
                    Dim nde = New Ast_Literal(AST_NODE._boolean, Str)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_boolean"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                Else
                    'Default to false
                    Dim nde = New Ast_Literal(AST_NODE._boolean, False)
                    nde._Start = tok._start
                    nde._End = tok._End
                    nde._Raw = tok.Value
                    nde._TypeStr = "_boolean"
                    Lookahead = Tokenizer.ViewNext
                    Return nde
                End If
            End Function
            ''' <summary>
            ''' Syntax:
            ''' 
            ''' Comments Literal:
            '''  -Comments
            ''' </summary>
            ''' <returns></returns>
            Public Function _CommentsNode() As Ast_Literal
                ' Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._COMMENTS)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)
                Dim nde = New Ast_Literal(AST_NODE._comments, tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_comments"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            Public Function _CommentsListExpression() As AstExpression
                Dim Body As New List(Of Ast_Literal)
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Do While tok = Grammar.Type_Id._COMMENTS
                    Body.Add(_CommentsNode)
                Loop
                Dim x = New Ast_ExpressionStatement(New Ast_Literal(AST_NODE._comments, Body))
                x._TypeStr = "_CommentsExpression"
                Return x
            End Function
            ''' <summary>
            ''' Syntax:
            ''' "hjk"
            ''' String Literal:
            '''  -String
            ''' </summary>
            ''' <returns></returns>
            Public Function _StringLiteralNode() As Ast_Literal
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)

                Dim str As String = ""
                If tok.Value.Contains("'") Then
                    str = tok.Value.Replace("'", "")
                Else
                End If
                If tok.Value.Contains(Chr(34)) Then
                    str = tok.Value.Replace(Chr(34), "")
                End If

                Dim nde = New Ast_Literal(AST_NODE._string, str)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_string"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function
            Public Function _IdentifierLiteralNode() As Ast_Identifier
                '   Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Dim tok As Token = Tokenizer.GetIdentifiedToken(Lookahead)

                Dim nde = New Ast_Identifier(tok.Value)
                nde._Start = tok._start
                nde._End = tok._End
                nde._Raw = tok.Value
                nde._TypeStr = "_variable"
                Lookahead = Tokenizer.ViewNext
                Return nde
            End Function


#End Region
#Region "STATEMENTS"
            ''' <summary>
            ''' 
            ''' Syntax
            ''' -ExpressionStatement
            ''' -BlockStatement
            ''' -IterationStatement
            ''' </summary>
            ''' <returns></returns>
            Public Function _Statement() As AstExpression
                Dim tok = Tokenizer.IdentifiyToken(Lookahead)
                Select Case tok
            'Begin Block
                    Case GrammarFactory.Grammar.Type_Id._CODE_BEGIN
                        Return _BlockStatement()
                        'due to most tokens detecting as variable (they can also be function names)
                        'we must check if is a fucntion command

                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        Do While tok = GrammarFactory.Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                        Loop
                        'enable machine code in script;
                        ''when Evaluating can be executed on VM
                    Case Grammar.Type_Id._SAL_EXPRESSION_BEGIN
                        Return _SAL_Expression()
                    Case Grammar.Type_Id._SAL_PROGRAM_BEGIN
                        Return _SAL_Expression()
                        'Standard Expression
                    Case Else
                        Return _ExpressionStatement()

                End Select
                'Technically badtoken try capture
                Dim etok = __UnknownStatementNode()
                ParserErrors.Add("Unknown Statement syntax" & vbNewLine & etok.ToJson.FormatJsonOutput)
                Return New Ast_ExpressionStatement(etok)
            End Function
            ''' <summary>
            ''' Gets Expression Statement All functions etc are some form of Expression
            ''' Syntax
            ''' -Expression ";"
            ''' 
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _ExpressionStatement() As AstExpression
                Return _Expression()
            End Function
            ''' <summary>
            ''' 
            ''' Syntax:
            '''  -_PrimaryExpression(literal)
            '''  -_MultiplicativeExpression
            '''  -_AddativeExpression
            '''  -_RelationalExpression
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _Expression() As AstExpression

                Return _LeftHandExpression()


            End Function
            ''' <summary>
            ''' 
            ''' Syntax: 
            ''' Could be Empty list So Prefix Optional
            ''' { OptionalStatmentList } 
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _BlockStatement() As Ast_BlockExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim Body As New List(Of AstExpression)
                _CodeBeginNode()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Detect Empty List
                If toktype = GrammarFactory.Grammar.Type_Id._CODE_END Then

                    Body.Add(New Ast_ExpressionStatement(__EmptyStatementNode))
                    _CodeEndNode()
                    Return New Ast_BlockExpression(Body)
                Else
                    Do While ((toktype) <> GrammarFactory.Grammar.Type_Id._CODE_END)
                        Body.Add(_LeftHandExpression)
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                    _CodeEndNode()
                    Return New Ast_BlockExpression(Body)
                End If
                Return New Ast_BlockExpression(Body)
            End Function
            Public Function _ArrayListLiteral() As Ast_Literal
                Lookahead = Tokenizer.ViewNext
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim Body As New List(Of AstNode)
                _ListBeginNode()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                If toktype = GrammarFactory.Grammar.Type_Id._LIST_END = True Then Body.Add(__EmptyStatementNode)

                Do Until toktype = GrammarFactory.Grammar.Type_Id._LIST_END
                    Select Case toktype
                        Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._VARIABLE
                            Body.Add(_VariableInitializer(_IdentifierLiteralNode))
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._LIST_END
                            _ListEndNode()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                            Dim de = New Ast_Literal(AST_NODE._array, Body)
                            de._TypeStr = "_array"
                            Lookahead = Tokenizer.ViewNext
                            Return de
                        Case GrammarFactory.Grammar.Type_Id._LIST_SEPERATOR
                            _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case Else
                            Body.Add(_literalNode())
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                    End Select
                Loop
                _ListEndNode()
                'Error at this point
                Dim nde = New Ast_Literal(AST_NODE._array, Body)
                nde._TypeStr = "_array"
                Return nde
            End Function
            Public Function _IdentifierList() As List(Of Ast_Identifier)
                Lookahead = Tokenizer.ViewNext
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim Body As New List(Of Ast_Identifier)
                _ListBeginNode()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                Do Until toktype = GrammarFactory.Grammar.Type_Id._LIST_END
                    Select Case toktype
                        Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._VARIABLE
                            Body.Add(_IdentifierLiteralNode())
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._LIST_END

                        Case GrammarFactory.Grammar.Type_Id._LIST_SEPERATOR
                            _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case Else
                            'Technically badtoken try capture
                            Dim etok = __UnknownStatementNode()
                            ParserErrors.Add("Unknown _Identifier Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput & vbNewLine)
                            Body.Add(New Ast_Identifier("Error"))
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                            Return Body
                    End Select
                Loop
                _ListEndNode()

                Return Body
            End Function
            Public Function _VariableDeclarationList() As List(Of Ast_VariableDeclarationExpression)
                Lookahead = Tokenizer.ViewNext
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim Body As New List(Of Ast_VariableDeclarationExpression)
                _ListBeginNode()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                Do Until toktype = GrammarFactory.Grammar.Type_Id._LIST_END
                    Select Case toktype
                        Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                            _WhitespaceNode()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._VARIABLE
                            Body.Add(_VariableDeclaration(_IdentifierLiteralNode))
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case GrammarFactory.Grammar.Type_Id._LIST_END

                        Case GrammarFactory.Grammar.Type_Id._LIST_SEPERATOR
                            _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Case Else
                            'Technically badtoken try capture
                            Dim etok = __UnknownStatementNode()
                            ParserErrors.Add("Unknown _VariableDeclaration Uncountered" & vbNewLine & etok.ToJson.FormatJsonOutput & vbNewLine)

                            Lookahead = Tokenizer.ViewNext
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                            Return Body
                    End Select
                Loop
                _ListEndNode()

                Return Body
            End Function
#End Region
#Region "Expressions"
            ''' <summary>
            ''' Syntax:
            ''' Variable: -Identifier as expression
            ''' - identifer = binaryExpression
            ''' </summary>
            ''' <returns></returns>
            Public Function _VariableExpression() As AstExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim _left As Ast_Identifier = Nothing
                'Token ID
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                'Get Identifier (All Variable statements start with a Left)
                _left = _IdentifierLiteralNode()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                If toktype = GrammarFactory.Grammar.Type_Id._WHITESPACE Then
                    Do Until toktype <> GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                End If
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'if the next operation is here then do it
                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN
                        Return _VariableInitializer(_left)
                    Case Else
                        'Carry Variable forwards to binary function
                        Return _BinaryExpression(New Ast_VariableExpressionStatement(_left))
                End Select
            End Function
            Public Function _VariableInitializer(ByRef _left As Ast_Identifier) As AstBinaryExpression
                Lookahead = Tokenizer.ViewNext
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)

                Return _BinaryExpression(New Ast_VariableExpressionStatement(_left))

            End Function
            Public Function _VariableInitializer(ByRef _left As Ast_VariableDeclarationExpression) As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)

                Return _BinaryExpression(New Ast_VariableExpressionStatement(_left._iLiteral))

            End Function
            ''' <summary>
            '''  Variable declaration, no init:OR INIT
            '''  DIM A 
            '''  DIM A AS STRING / INTEGER / BOOLEAN / LIST
            '''  let a as string
            '''  
            ''' </summary>
            ''' <param name="_left">IDENTIFIER</param>
            ''' <returns></returns>
            Public Function _VariableDeclaration(ByRef _left As Ast_Identifier) As Ast_VariableDeclarationExpression
                _WhitespaceNode()
                Lookahead = Tokenizer.ViewNext
                Dim Tok = Tokenizer.CheckIdentifiedToken(Lookahead)
                'SELECT lITERAL TYPE
                Select Case UCase(Tok.Value)
                    Case = UCase("string")
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._string)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else

                            Lookahead = Tokenizer.ViewNext
                            Return X
                        End If
                    Case = UCase("array")
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._array)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        End If
                    Case = UCase("array")
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._array)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        End If
                    Case = UCase("integer")
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._integer)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        End If

                    Case = UCase("int")
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._integer)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        End If
                    Case Else
                        Tokenizer.GetIdentifiedToken(Lookahead)
                        Dim X = New Ast_VariableDeclarationExpression(_left, AST_NODE._null)
                        Lookahead = Tokenizer.ViewNext
                        If Lookahead = ";" = True Then
                            __EndStatementNode()
                            Lookahead = Tokenizer.ViewNext
                            Return X
                        Else
                            Return X
                        End If
                End Select
                Return New Ast_VariableDeclarationExpression(_left, AST_NODE._null)
            End Function

            ''' <summary>
            ''' _Simple Assign (variable)
            ''' _Complex Assign (variable)
            ''' 
            ''' </summary>
            ''' <param name="_left"></param>
            ''' <returns></returns>
            Public Function _AssignmentExpression(ByRef _left As Ast_Identifier) As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)
                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN

                        Return _VariableInitializer(_left)

                End Select
                Return _VariableInitializer(_left)
            End Function
            Public Function _AssignmentExpression(ByRef _left As AstExpression) As AstExpression
                Lookahead = Tokenizer.ViewNext
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)
                Select Case toktype


                    Case GrammarFactory.Grammar.Type_Id._COMPLEX_ASSIGN
                        'Complex Assignments are 
                        Dim _operator = _GetAssignmentOperator()

                        Dim x = New AstBinaryExpression(AST_NODE._assignExpression, _left, _operator, _LeftHandExpression)
                        x._TypeStr = "_COMPLEX_ASSIGN"
                        Return x
                End Select
                Return _BinaryExpression(_left)
            End Function

            ''' <summary>
            ''' 
            ''' Syntax: 
            ''' 
            ''' ( OptionalStatmentList; )
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _ParenthesizedExpression() As Ast_ParenthesizedExpresion
                Dim toktype As GrammarFactory.Grammar.Type_Id
                Dim Body As New List(Of AstExpression)


                _ConditionalBeginNode()
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Detect Empty List
                If toktype = GrammarFactory.Grammar.Type_Id._CONDITIONAL_END Then

                    Body.Add(New Ast_ExpressionStatement(__EmptyStatementNode))
                    _ConditionalEndNode()
                Else
                    Do Until ((toktype) = GrammarFactory.Grammar.Type_Id._CONDITIONAL_END)
                        Body.Add(_ExpressionStatement)
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                    _ConditionalEndNode()
                End If

                Return New Ast_ParenthesizedExpresion(Body)
            End Function
#Region "Binary Operations/Expressions"
            ''' <summary>
            ''' Syntax:
            '''      -Multiplicative Expression
            ''' Literal */ Literal
            ''' </summary>
            ''' <returns></returns>
            Public Function _MultiplicativeExpression() As AstExpression
                Return _BinaryExpression(GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR, AST_NODE._MultiplicativeExpression, "_MultiplicativeExpression")
            End Function
            ''' <summary>
            ''' Syntax:
            '''      -Addative Expression
            ''' Literal +- Literal
            ''' </summary>
            ''' <returns></returns>
            Public Function _AddativeExpression() As AstExpression
                Return _BinaryExpression(GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR, AST_NODE._AddativeExpression, "_AddativeExpression")
            End Function
            ''' <summary>
            ''' Syntax: 
            ''' 
            ''' _RelationalExpression
            ''' </summary>
            ''' <returns></returns>
            Public Function _RelationalExpression()

                Return _BinaryExpression(GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR, AST_NODE._ConditionalExpression, "_ConditionalExpression")
            End Function
            ''' <summary>
            ''' syntax:
            ''' 
            ''' 
            ''' -Literal(Primary Expression)
            ''' -Multiplicative Expression
            ''' -Addative Expression
            ''' -ConditionalExpression(OperationalExpression)
            ''' _LeftHandExpression
            ''' __BinaryExpression
            ''' </summary>
            ''' <param name="NType"></param>
            ''' <param name="AstType"></param>
            ''' <param name="AstTypeStr"></param>
            ''' <returns></returns>
            Public Function _BinaryExpression(ByRef NType As GrammarFactory.Grammar.Type_Id, AstType As AST_NODE, AstTypeStr As String) As AstExpression
                Dim _left As AstExpression
                Dim _Operator As String = ""
                Dim _Right As AstExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Remove Erronious WhiteSpaces
                If toktype = Grammar.Type_Id._WHITESPACE Then
                    Do While toktype = Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                Else

                End If

                _left = _PrimaryExpression()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()
                            _left = New AstBinaryExpression(AST_NODE._assignExpression, _left, _Operator, _Right)
                            _left._TypeStr = "AssignmentExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._AddativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR)
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                            _Operator = _GetAssignmentOperator()

                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._MultiplicativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._ConditionalExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop

                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()

                End Select
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                If toktype = Grammar.Type_Id._STATEMENT_END Then
                    Dim x = __EmptyStatementNode()
                    Return _left
                Else
                    Return _left
                End If
                'End of file Marker
                Return _left
            End Function
            Public Function _BinaryExpression() As AstExpression
                Dim _left As AstExpression
                Dim _Operator As String = ""
                Dim _Right As AstExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Remove Erronious WhiteSpaces
                If toktype = Grammar.Type_Id._WHITESPACE Then
                    Do While toktype = Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                Else

                End If

                _left = _PrimaryExpression()
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()
                            _left = New AstBinaryExpression(AST_NODE._assignExpression, _left, _Operator, _Right)
                            _left._TypeStr = "AssignmentExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._AddativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._MultiplicativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR)
                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._ConditionalExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop

                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                End Select
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                If toktype = Grammar.Type_Id._STATEMENT_END Then
                    Dim x = __EmptyStatementNode()
                    Return _left
                Else
                    Return _left
                End If
                'End of file Marker
                Return _left
            End Function
            Public Function _BinaryExpression(ByRef _left As AstExpression) As AstExpression

                Dim _Operator As String = ""
                Dim _Right As AstExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'Remove Erronious WhiteSpaces
                If toktype = Grammar.Type_Id._WHITESPACE Then
                    Do While toktype = Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        toktype = Tokenizer.IdentifiyToken(Lookahead)
                    Loop
                Else

                End If


                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)

                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._COMPLEX_ASSIGN
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._COMPLEX_ASSIGN)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()
                            _left = New AstBinaryExpression(AST_NODE._assignExpression, _left, _Operator, _Right)
                            _left._TypeStr = "AssignmentExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._SIMPLE_ASSIGN)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()
                            _left = New AstBinaryExpression(AST_NODE._assignExpression, _left, _Operator, _Right)
                            _left._TypeStr = "AssignmentExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._ADDITIVE_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._AddativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._MULTIPLICATIVE_OPERATOR)

                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._MultiplicativeExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop
                    Case GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR
                        Do While ((toktype) = GrammarFactory.Grammar.Type_Id._RELATIONAL_OPERATOR)
                            _Operator = _GetAssignmentOperator()
                            Lookahead = Tokenizer.ViewNext
                            'NOTE: When adding further binary expressions maybe trickle down with this side
                            'the final level will need to be primary expression? 
                            _Right = _LeftHandExpression()

                            _left = New AstBinaryExpression(AST_NODE._ConditionalExpression, _left, _Operator, _Right)
                            _left._TypeStr = "BinaryExpression"
                            toktype = Tokenizer.IdentifiyToken(Lookahead)
                        Loop

                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                End Select
                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                If toktype = Grammar.Type_Id._STATEMENT_END Then
                    Dim x = __EmptyStatementNode()
                    Return _left
                Else
                    Return _left
                End If
                'End of file Marker
                Return _left
            End Function

#End Region
#End Region
            Public Function _GetAssignmentOperator() As String
                Dim str = Tokenizer.GetIdentifiedToken(Lookahead).Value
                str = str.Replace("\u003c", " Less than")
                str = str.Replace("\u003e", " Greater Than ")
                ' \U003c < Less-than sign
                ' \U003e > Greater-than sign
                str = str.Replace("<=", " Less than equals ")
                str = str.Replace(">=", " Greater Than equals ")
                str = str.Replace("<", " Less than ")
                str = str.Replace(">", " Greater Than ")

                Return UCase(str)
            End Function
#End Region

#Region "Functions"
            ''' <summary>
            ''' syntax : 
            ''' -Functions
            ''' _DimFunction
            ''' FOR
            ''' WHILE
            ''' UNTIL
            ''' IF
            ''' </summary>
            ''' <returns></returns>
            Public Function _CommandFunction() As AstExpression
                Dim iTok As Token = Tokenizer.CheckIdentifiedToken(Lookahead)
                Select Case UCase(iTok.Value)
                            'Check Fucntion name
                    Case "DIM"
                        Dim nde = _DimFunction()

                        Lookahead = Tokenizer.ViewNext
                        Return nde
                    Case "FOR"
                        Return _IterationStatment()
                    Case "WHILE"
                        Return _IterationStatment()
                    Case "UNTIL"
                        Return _IterationStatment()
                    Case "IF"
                        Return _IterationStatment()
                End Select
                Return Nothing
            End Function
            Public Function CheckFunction(ByRef Str As String) As Boolean
                Select Case UCase(Str)
                            'Check Fucntion name
                    Case "DIM"
                        Return True
                    Case "FOR"
                        Return True
                    Case "WHILE"
                        Return True
                    Case "UNTIL"
                        Return True

                End Select
                Return False

            End Function
            Public Function _DimFunction() As AstExpression
                Dim toktype = Tokenizer.IdentifiyToken(Lookahead)

                Lookahead = Tokenizer.ViewNext
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                'GET the identified token as it is a command but detected as variable
                'DIM
                Tokenizer.GetIdentifiedToken(Lookahead)
                Lookahead = Tokenizer.ViewNext
                '_
                _WhitespaceNode()
                Dim _left = _IdentifierLiteralNode()
                _WhitespaceNode()
                Lookahead = Tokenizer.ViewNext
                Dim tok = Tokenizer.CheckIdentifiedToken(Lookahead)
                If UCase(tok.Value) = UCase("AS") Then
                    Dim DecNode As Ast_VariableDeclarationExpression
                    'Eat as
                    Tokenizer.GetIdentifiedToken(Lookahead)
                    Lookahead = Tokenizer.ViewNext
                    'GetVar
                    DecNode = _VariableDeclaration(_left)
                    ' nde = _VariableInitializer(_left)
                    Lookahead = Tokenizer.ViewNext
                    tok = Tokenizer.CheckIdentifiedToken(Lookahead)
                    If tok.ID = Grammar.Type_Id._WHITESPACE Then

                        _WhitespaceNode()
                        Lookahead = Tokenizer.ViewNext
                        tok = Tokenizer.CheckIdentifiedToken(Lookahead)
                        If tok.ID = Grammar.Type_Id._SIMPLE_ASSIGN Then

                            Dim lst As New List(Of AstExpression)
                            lst.Add(DecNode)
                            Dim Empt = New Ast_ExpressionStatement(New Ast_Literal(AST_NODE._emptyStatement))
                            Empt._TypeStr = "_emptyStatement"
                            lst.Add(Empt)
                            lst.Add(_BinaryExpression(New Ast_VariableExpressionStatement(_left)))
                            '   Return 
                            Dim block As New Ast_BlockExpression(lst)
                            block._hasReturn = True
                            block._ReturnValues.Add(New Ast_VariableExpressionStatement(_left))
                            Return block
                        End If
                    Else
                        Return DecNode
                    End If

                Else
                    'Complex
                    'View next (for next function)
                    Dim nde As AstExpression
                    Lookahead = Tokenizer.ViewNext

                    nde = _VariableInitializer(_left)


                    Return nde

                End If

                Return New Ast_VariableExpressionStatement(_left)


            End Function
            ''' <summary>
            ''' Syntax 
            ''' -DoWhile
            ''' -DoUntil
            ''' _ForNext
            ''' 
            ''' </summary>
            ''' <returns></returns>
            Public Function _IterationStatment() As AstExpression
                Dim toktype As GrammarFactory.Grammar.Type_Id
                toktype = Tokenizer.IdentifiyToken(Lookahead)
                Select Case toktype
                    Case GrammarFactory.Grammar.Type_Id._WHITESPACE
                        _WhitespaceNode()
                    Case GrammarFactory.Grammar.Type_Id._WHILE
                        Exit Select
                    Case GrammarFactory.Grammar.Type_Id._UNTIL
                        Exit Select
                    Case GrammarFactory.Grammar.Type_Id._FOR
                        Exit Select
                    Case Else
                        Exit Select
                End Select
                Return Nothing
            End Function
#End Region

        End Class

    End Namespace
End Namespace
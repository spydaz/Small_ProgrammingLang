Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Ast NodeTypes Used to Describe AST Node 
        ''' </summary>
        Public Enum AST_NODE
            _array = 1
            _boolean = 2
            _string = 3
            _integer = 4
            _variable = 5
            _null = 6
            _endStatement = 7
            _blockCode = 8
            _binaryExpression = 9
            _ParenthesizedExpresion = 10
            _MultiplicativeExpression = 11
            _AddativeExpression = 12
            _assignExpression = 13
            _Dim = 14
            _For = 15
            _If = 16
            _function = 17
            _sub = 18
            _class = 19
            _else = 20
            _then = 21
            _Do_while = 22
            _Do_until = 23
            _Program = 24
            _comments = 25
            _ExpressionStatement = 26
            _WhiteSpace = 27
            _UnknownStatement = 28
            _Code_Begin = 29
            _Code_End = 30
            _emptyStatement = 31
            _OperationBegin = 32
            _OperationEnd = 33
            _ConditionalExpression = 34
            'Sal token_IDs
            SAL_NULL
            SAL_REMOVE
            SAL_RESUME
            SAL_PUSH
            SAL_PULL
            SAL_PEEK
            SAL_WAIT
            SAL_PAUSE
            SAL_HALT
            SAL_DUP
            SAL_JMP
            SAL_JIF_T
            SAL_JIF_F
            SAL_JIF_EQ
            SAL_JIF_GT
            SAL_JIF_LT
            SAL_LOAD
            SAL_STORE
            SAL_CALL
            SAL_RET
            SAL_PRINT_M
            SAL_PRINT_C
            SAL_ADD
            SAL_SUB
            SAL_MUL
            SAL_DIV
            SAL_AND
            SAL_OR
            SAL_NOT
            SAL_IS_EQ
            SAL_IS_GT
            SAL_IS_GTE
            SAL_IS_LT
            SAL_IS_LTE
            SAL_TO_POS
            SAL_TO_NEG
            SAL_INCR
            SAL_DECR
            _SAL_Expression
            _Sal_Program_title
            _Sal_BeginStatement
            _ListBegin
            _ListEnd
            _DeclareVariable
        End Enum
        ''' <summary>
        ''' Syntax: 
        ''' 
        ''' Root Ast node Type
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public MustInherit Class AstNode
            ''' <summary>
            ''' Type Of Node
            ''' </summary>
            Public _Type As AST_NODE
            ''' <summary>
            '''  String version of the Type due to not being printed
            ''' </summary>
            Public _TypeStr As String
            ''' <summary>
            ''' Start Position
            ''' </summary>
            Public _Start As Integer
            ''' <summary>
            ''' End Position
            ''' </summary>
            Public _End As Integer
            ''' <summary>
            ''' Raw data of token
            ''' </summary>
            Public _Raw As String
            Public MustOverride Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
            ''' <summary>
            ''' Instanciate
            ''' </summary>
            ''' <param name="ntype">Type of Node</param>
            Public Sub New(ByRef ntype As AST_NODE)
                Me._Type = ntype
            End Sub
            Public Overridable Function ToArraylist() As List(Of String)
                Dim lst As New List(Of String)
                lst.Add(_TypeStr)
                Return lst
            End Function
            ''' <summary>
            ''' get raw expression
            ''' </summary>
            ''' <returns></returns>
            Public Function GetExpr() As String
                Return _Raw
            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace

End Namespace
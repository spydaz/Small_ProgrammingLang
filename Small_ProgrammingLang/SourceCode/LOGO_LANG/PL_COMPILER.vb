Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports SDK.SAL
Imports SDK.SmallProgLang.Ast_ExpressionFactory
Imports SDK.SmallProgLang.Evaluator
Imports SDK.SmallProgLang.GrammarFactory
Imports SDK.SmallProgLang.GrammarFactory.Grammar



'INTERPRETOR
' 
#Region "INTERPRETATION"
'THE AST CREATON FACTORY - ALL LANGUAGES
'
#Region "AST - FACTORY"
'AST FACTORY
'
Namespace SmallProgLang

    'THE SMALL PROGRAMMING LANGUGE - TOP LEVEL 
    '
#Region "SPL SMALL PRAGRAMING LANG"
#Region "MODELS"
    'AST PROGRAM
    '
    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' 
        ''' 
        ''' Syntax : 
        ''' Based on AST Explorer output
        '''{
        '''  "type": "Program",
        '''  "start": 0,
        '''  "end": 2,
        '''  "body": [
        '''    {
        '''      "type": "ExpressionStatement",
        '''      "start": 0,
        '''      "end": 2,
        '''      "expression": {
        '''        "type": "Literal",
        '''        "start": 0,
        '''        "end": 2,
        '''        "value": 42,
        '''        "raw": "42"
        '''      }
        '''    }
        '''  ],
        '''}
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class AstProgram
            Inherits AstNode
            ''' <summary>
            ''' Expression List
            ''' </summary>
            Public Body As List(Of AstExpression)

            ''' <summary>
            ''' Instanciate Expression
            ''' </summary>
            ''' <param name="nBody">Expressions</param>
            Public Sub New(ByRef nBody As List(Of AstExpression))
                MyBase.New(AST_NODE._Program)
                Me.Body = nBody
                Me._TypeStr = "_Program"

            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                For Each item In Body
                    lst.AddRange(item.ToArraylist)
                Next
                Return lst
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    'AST NODE
    '
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
            _TRUE
            Logo_Expression
            Logo_Function
            Logo_name
            Logo_Value
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
            ''' <summary>
            ''' All Literals contain values 
            ''' All node should be evaluated Except literals which should simply return thier value
            ''' 
            ''' </summary>
            ''' <param name="ParentEnv"></param>
            ''' <returns></returns>
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
                Return ToJson
            End Function
        End Class
    End Namespace
    'AST EXPRESSION
    '
#End Region
#Region "LITERALS"
    Namespace Ast_ExpressionFactory

        ''' <summary>
        ''' Expression Model Used To Group Expressions
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public MustInherit Class AstExpression
            Inherits AstNode

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub
            ''' <summary>
            ''' All Expressions Return values but also need to be evaluted ; 
            ''' Thier output results should be a value or the environment being return updated with its new values
            ''' Expressions such as FOR/DIM/IF/WHILE 
            ''' all return a value of true also to signify the completion of the evaluation; 
            ''' If the node is not evaluated then it will simply return true but stay unevaluated 
            ''' hence always evalating all expressions; 
            ''' When designing expressions ; 
            ''' thier evaluate function should use either evaluate or getvalue for its subordinate propertys
            ''' We shall attempt to evaluate every expression inside of itself to return the values within.
            ''' The expression uses the Environment delivered as its own global record;
            ''' the environment is returned to the sender 
            ''' with any values updated;
            ''' This function must be overridden
            ''' All 
            ''' </summary>
            ''' <param name="ParentEnv">sets the environment for the expression; 
            ''' the environment contains the current record of variables in use by the global script </param>
            ''' <returns></returns>
            Public MustOverride Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
            ''' <summary>
            ''' Generates a String to be run on the sal virtual machine
            ''' The node should be evaluated first to return the values for the function 
            ''' then produce the code with the values -Pluged in
            ''' </summary>
            ''' <param name="ParentEnv"></param>
            ''' <returns></returns>
            Public MustOverride Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String


            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace
    'AST LITERAL
    '
    Namespace Ast_ExpressionFactory

        ''' <summary>
        ''' 
        ''' Syntax: 
        ''' 
        ''' Used to hold Literals and values
        ''' 
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_Literal
            Inherits AstNode
            ''' <summary>
            ''' Holds value (in its type)
            ''' </summary>
            Public iLiteral As Object
            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub
            Public Sub New(ByRef ntype As AST_NODE, ByRef nValue As Object)
                MyBase.New(ntype)
                iLiteral = nValue
            End Sub
            Public Sub New(ByRef nValue As Integer)
                MyBase.New(AST_NODE._integer)
                iLiteral = 0
                iLiteral = nValue
            End Sub
            Public Sub New(ByRef nValue As Double)
                MyBase.New(AST_NODE._integer)
                iLiteral = 0.0
                iLiteral = nValue
            End Sub
            Public Sub New(ByRef nValue As String)
                MyBase.New(AST_NODE._string)
                iLiteral = ""
                iLiteral = nValue
            End Sub
            Public Sub New(ByRef nValue As Boolean)
                MyBase.New(AST_NODE._boolean)
                iLiteral = False
                iLiteral = nValue
            End Sub
            Public Sub New(ByRef nValue As List(Of Object))
                MyBase.New(AST_NODE._array)
                iLiteral = New List(Of Object)
                iLiteral = nValue
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                lst.Add(iLiteral.ToString)
                Return lst
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object

                Select Case Me._Type
                    Case AST_NODE._integer
                        Dim Obj As Integer = 0
                        Obj = Integer.Parse(iLiteral)
                        Return Obj
                    Case AST_NODE._string
                        Dim Obj As String = ""
                        Obj = iLiteral.ToString
                        Return Obj
                    Case AST_NODE._array
                        Return iLiteral
                    Case AST_NODE._boolean
                        Dim Obj As Boolean = False
                        Obj = Boolean.Parse(iLiteral)
                        Return Obj
                    Case Else
                        Return iLiteral
                End Select
            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function

        End Class
    End Namespace
    'AST IDENTIFIER
    '
    Namespace Ast_ExpressionFactory

        ''' <summary>
        ''' Syntax:
        ''' 
        ''' Used to hold Varnames or Identifiers
        ''' 
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_Identifier
            Inherits Ast_Literal
            Public _Name As String
            Public Sub New(ByRef nName As String)
                MyBase.New(AST_NODE._variable)
                Me._TypeStr = "Identifier"
                Me._Name = nName
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                lst.Add(_Name)
                Return lst
            End Function
            Public Function CheckVar(ByRef ParentEnv As EnvironmentalMemory) As Boolean


                Return ParentEnv.CheckVar(_Name)
            End Function
            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckVar(_Name) = True Then
                    Return ParentEnv.GetVar(_Name)
                Else
                    Return Nothing
                End If


            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
#End Region
#Region "EXPRESSIONS"
    'AST EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Syntax:
        ''' 
        ''' Expression Statement Types
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_ExpressionStatement
            Inherits AstExpression
            ''' <summary>
            ''' Literal Value
            ''' </summary>
            Public _iLiteral As Ast_Literal
            ''' <summary>
            ''' 
            ''' </summary>
            ''' <param name="nValue">Literal Value to be stored </param>
            Public Sub New(ByRef nValue As Ast_Literal)
                MyBase.New(AST_NODE._ExpressionStatement)
                Me._iLiteral = nValue
                Me._TypeStr = "_PrimaryExpression"
                Me._Start = _iLiteral._Start
                Me._End = _iLiteral._End
                Me._Raw = nValue._Raw
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst As List(Of String) = MyBase.ToArraylist()
                lst.AddRange(_iLiteral.iLiteral.toarraylist)
                Return lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _iLiteral.GetValue(ParentEnv)
            End Function


            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    'AST VARIABLE EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_VariableExpressionStatement
            Inherits AstExpression
            ''' <summary>
            ''' Literal Value
            ''' </summary>
            Public _iLiteral As Ast_Identifier
            ''' <summary>
            ''' 
            ''' </summary>
            ''' <param name="nValue">Literal Value to be stored </param>
            Public Sub New(ByRef nValue As Ast_Identifier)
                MyBase.New(AST_NODE._ExpressionStatement)
                Me._iLiteral = nValue
                Me._TypeStr = "_PrimaryExpression"
                Me._Start = _iLiteral._Start
                Me._End = _iLiteral._End
                Me._Raw = nValue._Raw
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                lst.AddRange(_iLiteral.ToArraylist)
                Return lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object

                Return GetValue(ParentEnv)
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _iLiteral.GetValue(ParentEnv)
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    'AST UNARY EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_UnaryExpression
            Inherits AstExpression
            Public _Value As Ast_Literal
            Public _name As Ast_Identifier
            ' Public _Value As AstExpression
            Public Sub New(ByRef nName As Ast_Identifier, ByRef nValue As Ast_Literal)
                MyBase.New(AST_NODE._ExpressionStatement)
                Me._Value = nValue
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return ParentEnv
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    ' AST BINARY EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Used for Binary Operations
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class AstBinaryExpression
            Inherits AstExpression
            Public _Left As AstExpression
            Public _Right As AstExpression
            Public _Operator As String
            Public LocalEnvironment As New EnvironmentalMemory
            Public Sub New(ByRef nType As AST_NODE, ByRef nLeft As AstExpression, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(nType)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "BinaryExpression"
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                lst.Add(_Operator)
                lst.AddRange(_Left.ToArraylist)
                lst.AddRange(_Right.ToArraylist)
                Return lst
            End Function

            Public Sub New(ByRef nType As AST_NODE, ByRef nLeft As Ast_VariableDeclarationExpression, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(nType)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "BinaryExpression"
            End Sub
            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case _Operator
'Mathmatical
                    Case "+"
                        Return EvaluateAddative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "-"
                        Return EvaluateAddative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "*"
                        Return EvaluateMultiplicative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "/"
                        Return EvaluateMultiplicative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
'Relational

                    Case ">="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "<="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case ">"
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "<"
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

'Complex assign
                    Case "+="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "-="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "*="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "/="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)


                End Select
                Return ParentEnv
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Me.LocalEnvironment = ParentEnv

                Return GetValue(ParentEnv)


            End Function
            ''' <summary>
            ''' Allows for evaluation of the node : Imeadialty invoked expression
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="iOperator"></param>
            ''' <param name="Right"></param>
            ''' <returns></returns>
            Private Function EvaluateMultiplicative(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer

                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "*"
                            Return (Left.GetValue(ParentEnv) * Right.GetValue(ParentEnv))
                        Case "/"
                            Return (Left.GetValue(ParentEnv) / Right.GetValue(ParentEnv))
                    End Select


                End If
                Return (Left.GetValue(ParentEnv))
            End Function
            Private Function EvaluateAddative(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer

                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "+"
                            Return (Left.GetValue(ParentEnv) + Right.GetValue(ParentEnv))
                        Case "-"
                            Return (Left.GetValue(ParentEnv) - Right.GetValue(ParentEnv))
                    End Select


                End If
                Return (Left.GetValue(ParentEnv))
            End Function
            ''' <summary>
            ''' Evaluate node values ( imeadiatly invoked expression )
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="iOperator"></param>
            ''' <param name="Right"></param>
            ''' <returns></returns>
            Private Function EvaluateBoolean(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Boolean


                Select Case iOperator
                    Case ">="
                        Return (Left.GetValue(ParentEnv) >= Right.GetValue(ParentEnv))
                    Case "<="
                        Return (Left.GetValue(ParentEnv) <= Right.GetValue(ParentEnv))
                    Case ">"
                        Return (Left.GetValue(ParentEnv) > Right.GetValue(ParentEnv))
                    Case "<"
                        Return (Left.GetValue(ParentEnv) < Right.GetValue(ParentEnv))
                    Case "="
                        Return (Left.GetValue(ParentEnv) = Right.GetValue(ParentEnv))

                End Select



                Return (Left.GetValue(ParentEnv))
            End Function
            Private Function EvaluateComplex(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer
                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "+="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf += rt
                            Return lf
                        Case "-="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf -= rt
                            Return lf
                        Case "*="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf *= rt
                            Return lf
                        Case "/="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf /= rt
                            Return lf


                    End Select


                End If
                Return Integer.Parse(((Left.GetValue(ParentEnv))))
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function
        End Class
    End Namespace

    ' AST BLOCK EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Used for CodeBlocks
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_BlockExpression
            Inherits AstExpression
            Public Body As List(Of AstExpression)
            Public _ReturnValues As New List(Of AstExpression)
            Public _hasReturn As Boolean = False
            Public Sub New(ByRef iBody As List(Of AstExpression))
                MyBase.New(AST_NODE._blockCode)
                Me._TypeStr = "_blockCode"
                Body = iBody
                Me._Start = iBody(0)._Start
                For Each item In iBody
                    Me._Raw &= "," & item._Raw
                Next
                Me._End = iBody(iBody.Count - 1)._End
                _ReturnValues = New List(Of AstExpression)
                _hasReturn = False
            End Sub
            Public Sub New(ByRef iBody As List(Of AstExpression), ByRef iReturnValues As List(Of AstExpression))
                MyBase.New(AST_NODE._blockCode)
                Me._TypeStr = "_blockCode"
                Body = iBody
                Me._Start = iBody(0)._Start
                For Each item In iBody
                    Me._Raw &= item._Raw
                Next
                Me._End = iBody(iBody.Count - 1)._End
                _ReturnValues = iReturnValues
                _hasReturn = True
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                For Each item In Body
                    lst.AddRange(item.ToArraylist)
                Next
                lst.Add(_hasReturn.ToString)
                For Each item In _ReturnValues
                    lst.AddRange(item.ToArraylist)
                Next
                Return lst
            End Function
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function
            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If _hasReturn = False Then
                    For Each item In Body
                        item.Evaluate(ParentEnv)
                    Next
                    Return ParentEnv
                Else
                    Dim Values As New List(Of Object)

                    For Each item In _ReturnValues
                        Values.Add(item.Evaluate(ParentEnv))
                    Next
                    Return _ReturnValues
                End If

            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    ' AST PARENTESIZED EXPRESSION   
    '
    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' syntax:
        ''' -_ParenthesizedExpresion
        ''' 
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_ParenthesizedExpresion
            Inherits AstExpression
            Public Body As List(Of AstExpression)
            Public Sub New(ByRef iBody As List(Of AstExpression))
                MyBase.New(AST_NODE._ParenthesizedExpresion)
                Me._TypeStr = "_ParenthesizedExpresion"
                Body = iBody
                Me._Start = iBody(0)._Start
                For Each item In iBody
                    Me._Raw &= item._Raw
                Next
                Me._End = iBody(iBody.Count - 1)._End
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object


                Return GetValue(ParentEnv)
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object

                For Each item In Body
                    item.Evaluate(ParentEnv)
                Next
                Return ParentEnv
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
#End Region
#Region "FUNCTIONS"
    ' AST VARIABLE DECLARATION EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_VariableDeclarationExpression
            Inherits Ast_VariableExpressionStatement
            Public _LiteralType As AST_NODE
            Public _LiteralTypeStr As String
            Public Sub New(ByRef nValue As Ast_Identifier, ByRef iLiteralType As AST_NODE)
                MyBase.New(nValue)
                Me._LiteralType = iLiteralType
                Me._Type = AST_NODE._DeclareVariable
                Me._TypeStr = "_VariableDeclaration"

                Select Case iLiteralType
                    Case AST_NODE._string
                        Me._LiteralTypeStr = "_string"
                    Case AST_NODE._array
                        Me._LiteralTypeStr = "_array"
                    Case AST_NODE._integer
                        Me._LiteralTypeStr = "_integer"
                    Case AST_NODE._boolean
                        Me._LiteralTypeStr = "_boolean"
                    Case Else
                        Me._LiteralTypeStr = Nothing
                End Select


            End Sub
            Public Sub New(ByRef nValue As Ast_VariableExpressionStatement, ByRef iLiteralType As AST_NODE)
                MyBase.New(nValue._iLiteral)
                Me._LiteralType = iLiteralType
                Me._Type = AST_NODE._DeclareVariable
                Me._TypeStr = "_VariableDeclaration"

                Select Case iLiteralType
                    Case AST_NODE._string
                        Me._LiteralTypeStr = "_string"
                    Case AST_NODE._array
                        Me._LiteralTypeStr = "_array"
                    Case AST_NODE._integer
                        Me._LiteralTypeStr = "_integer"
                    Case Else
                        Me._LiteralTypeStr = "_null"
                End Select


            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckVar(Me._iLiteral._Name) = False Then
                    Select Case _LiteralType
                        Case AST_NODE._null
                            ParentEnv.AssignValue(_iLiteral._Name, Nothing)
                        Case AST_NODE._integer
                            ParentEnv.AssignValue(_iLiteral._Name, 0)
                        Case AST_NODE._string
                            ParentEnv.AssignValue(_iLiteral._Name, "")
                        Case AST_NODE._array
                            ParentEnv.AssignValue(_iLiteral._Name, New List(Of Object))
                    End Select


                End If
                Return _iLiteral.GetValue(ParentEnv)
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    ' AST ASSIGNMENT EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_AssignmentExpression
            Inherits AstExpression
            ''' <summary>
            ''' Must be var literal type
            ''' </summary>
            Public _Left As Ast_Identifier
            Public _Right As AstExpression
            Public _Operator As String
            Public Sub New(ByRef nLeft As Ast_Identifier, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(AST_NODE._assignExpression)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "_assignExpression"
            End Sub

            Public Overrides Function ToArrayList() As List(Of String)

                Dim lst As List(Of String) = MyBase.ToArraylist
                lst.Add(_Operator)
                lst.Add(_Left._Name.ToString)
                lst.AddRange(_Right.ToArraylist)
                Return lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Function GetVar(ByRef ParentEnv As EnvironmentalMemory) As EnvironmentalMemory.Variable
                Dim nvar As New EnvironmentalMemory.Variable
                nvar.Value = _Right.Evaluate(ParentEnv)
                nvar.Name = _Left._Name
                nvar.Type = Me._Type
                Return nvar
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Dim nvar As New EnvironmentalMemory.Variable
                nvar.Value = _Right.Evaluate(ParentEnv)
                nvar.Name = _Left._Name
                nvar.Type = Me._Type
                If ParentEnv.CheckVar(nvar.Name) = True Then
                    Return _Right.Evaluate(ParentEnv)
                End If
                Return Nothing
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    ' AST DO EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        'DO_ While/True Until/True
        '   while (x > 10) {      x -= 1;    } 
        '   Until (x > 10) {      x -= 1;    } 
        ''' <summary>
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_DoExpression
            Inherits AstExpression
            Public Body As Ast_BlockExpression
            Public Test As AstBinaryExpression
            Public Expected As Boolean
            Public Sub New(ByRef ntype As AST_NODE, ByRef iTest As AstBinaryExpression, ByRef iBody As Ast_BlockExpression, Optional iExpected As Boolean = True)
                MyBase.New(ntype)
                Body = iBody
                Expected = iExpected
                Test = iTest
                Select Case _Type

                    Case AST_NODE._Do_until
                        _TypeStr = "_Do_until"

                    Case AST_NODE._Do_while
                        _TypeStr = "_Do_while"

                End Select
                Body = iBody
                Body._hasReturn = True
                Body._ReturnValues.Add(New Ast_ExpressionStatement(New Ast_Literal(AST_NODE._TRUE)))
            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As SmallProgLang.Evaluator.EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function
            Public Overrides Function GetValue(ByRef ParentEnv As SmallProgLang.Evaluator.EnvironmentalMemory) As Object
                Dim MyTest As Boolean = False
                Select Case _Type
                    Case AST_NODE._Do_until
                        Do Until MyTest = Expected
                            MyTest = Test.Evaluate(ParentEnv)
                            For Each item In Body.Body
                                item.Evaluate(ParentEnv)
                            Next
                        Loop
                    Case AST_NODE._Do_while
                        Do While MyTest = Expected
                            MyTest = Test.Evaluate(ParentEnv)
                            For Each item In Body.Body
                                item.Evaluate(ParentEnv)
                            Next
                        Loop
                End Select
                Return True
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    ' AST FOR EXPRESSION
    '
    Namespace Ast_ExpressionFactory

        'for (dim i = 0); (i < 10); (i += 1) {      x += i;    }
        'for (Init); (Test); (Increment) { <BODY >    x += i;    }
        ''' <summary>
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_ForExpression
            Inherits AstExpression

            Public Init As Ast_VariableDeclarationExpression
            Public Test As AstBinaryExpression
            Public Increment As AstExpression
            Public Body As Ast_BlockExpression

            Public Sub New(ByRef Init As Ast_VariableDeclarationExpression, ByRef Test As AstBinaryExpression, ByRef Increment As AstExpression, ByRef Body As Ast_BlockExpression)
                MyBase.New(AST_NODE._For)
                Body._hasReturn = True
                Body._ReturnValues.Add(New Ast_ExpressionStatement(New Ast_Literal(AST_NODE._TRUE)))
            End Sub
            'Always 
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                For Each item In Body.Body
                    item.Evaluate(ParentEnv)
                Next
                Return GetValue(ParentEnv)
            End Function
            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return True
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace
    ' AST IF EXPRESSION
    '
    Namespace Ast_ExpressionFactory
        'if (x>7) then { x = 1; } else { x = 2; }
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_IfExpression
            Inherits AstExpression
            Public Test As AstExpression
            Public Consequent As Ast_BlockExpression
            Public Alternate As Ast_BlockExpression

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function



            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
#End Region
#End Region


    'THE SPYDAZWEB ASSEMBLY LANGUAGE - LOW LEVEL
    '
#Region "SAL_ASSEMBLY LANG"
    'AST LITERALS
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_SAL_Literal
            Inherits Ast_Literal

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Sub New(ByRef ntype As AST_NODE, ByRef nValue As Object)
                MyBase.New(ntype, nValue)
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
    'AST EXPRESSIONS
    '
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_SalExpression
            Inherits AstExpression
            Public Program As List(Of Ast_Literal)
            Public Sub New(ByRef nProgram As List(Of Ast_Literal))
                MyBase.New(AST_NODE._SAL_Expression)
                Me._TypeStr = "_SAL_Expression"
                Me.Program = nProgram
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function



            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Dim Str As String = ""
                For Each item In Program
                    Str &= " " & item._Raw
                Next
                Return Str
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
#End Region

End Namespace
#End Region
'GRAMMAR FACTORY
'
'
#Region "THE TOKENIZER GRAMMAR FACTORY"
'GRAMMAR FACTORY
'
'
Namespace SmallProgLang
    'GRAMMAR
    '
    Namespace GrammarFactory
        ''' <summary>
        ''' GRAMMAR OBJECT ID
        ''' </summary>
        Public Enum Type_Id
            ''' <summary>
            ''' Literal
            ''' </summary>
            _INTEGER
            ''' <summary>
            ''' Literal
            ''' </summary>
            _STRING
            ''' <summary>
            ''' Print Literal/Value/String
            ''' </summary>
            _PRINT
            ''' <summary>
            ''' Declare Var
            ''' </summary>
            _DIM
            ''' <summary>
            ''' Begin Iteration of list
            ''' For (Iterator = (Increment to Completion)
            ''' </summary>
            _FOR
            ''' <summary>
            ''' Additional = Step +1
            ''' </summary>
            _EACH
            ''' <summary>
            ''' From item in list
            ''' </summary>
            _IN
            ''' <summary>
            ''' End of iteration marker
            ''' </summary>
            _TO
            ''' <summary>
            ''' Increment Iterator
            ''' </summary>
            _NEXT
            ''' <summary>
            ''' If Condition = Outcome Then (code) Else (code)
            ''' </summary>
            _IF
            ''' <summary>
            ''' Then (block)
            ''' </summary>
            _THEN
            ''' <summary>
            ''' Else (Block)
            ''' </summary>
            _ELSE
            ''' <summary>
            ''' Until Condition = true
            ''' </summary>
            _UNTIL
            ''' <summary>
            ''' While Condition = true
            ''' </summary>
            _WHILE
            ''' <summary>
            ''' Signify begining of Do...While/Until
            ''' </summary>
            _DO
            _RETURN
            _FUNCTION
            _SUB
            _CLASS
            _NEW
            ''' <summary>
            ''' Used in Declaration Assignment
            ''' Left (var) assign as (LiteralType)
            ''' </summary>
            _AS
            ''' <summary>
            ''' End of While loop (marker)(check expression Condition)
            ''' </summary>
            _LOOP
            ''' <summary>
            ''' xLeft = output of right (9+4) (+= 9) (-=2) (3) (true)
            ''' </summary>
            _SIMPLE_ASSIGN
            ''' <summary>
            ''' xLeft assigns output of right -=(9+4) (+= 9) (-=2) (3) (true)
            ''' Complex assign ... x=x+(ouput)x=x-(ouput) etc
            ''' </summary>
            _COMPLEX_ASSIGN
            ''' <summary>
            ''' Boolean Literal Env Variable
            ''' </summary>
            _TRUE
            ''' <summary>
            ''' Boolean Literal - Env Variable
            ''' </summary>
            _FALSE
            ''' <summary>
            ''' Boolean literal -Env Variable
            ''' </summary>
            _NULL
            ''' <summary>
            ''' Used for Args List (Lists) = Arrays
            ''' Args are lists of Vars (function Environment Vars)
            ''' </summary>
            _LIST_BEGIN
            ''' <summary>
            ''' End of List
            ''' </summary>
            _LIST_END
            ''' <summary>
            ''' Used for Blocks of code
            ''' </summary>
            _CODE_BEGIN
            ''' <summary>
            ''' End of Code block
            ''' </summary>
            _CODE_END
            ''' <summary>
            ''' Used for operation blocks as well as 
            ''' ordering prioritizing evals
            ''' Begin
            ''' </summary>
            _CONDITIONAL_BEGIN
            ''' <summary>
            ''' End of Condition
            ''' </summary>
            _CONDITIONAL_END
            ''' <summary>
            '''  - AND
            ''' </summary>
            _LOGICAL_AND
            ''' <summary>
            '''  | OR
            ''' </summary>
            _LOGICAL_OR
            ''' <summary>
            ''' ! NOT
            ''' </summary>
            _LOGICAL_NOT
            ''' <summary>
            ''' Greater than / Less Than
            ''' </summary>
            _RELATIONAL_OPERATOR
            ''' <summary>
            ''' +-
            ''' </summary>
            _ADDITIVE_OPERATOR
            ''' <summary>
            ''' */
            ''' </summary>
            _MULTIPLICATIVE_OPERATOR
            ''' <summary>
            ''' ;
            ''' </summary>
            _STATEMENT_END
            ''' <summary>
            ''' end of file
            ''' </summary>
            _EOF
            ''' <summary>
            ''' Bad / Unrecognized token
            ''' </summary>
            _BAD_TOKEN
            ''' <summary>
            ''' Seperates items in list
            ''' </summary>
            _LIST_SEPERATOR
            ''' <summary>
            ''' !=
            ''' </summary>
            _NOT_EQUALS
            ''' <summary>
            ''' DECLARE VAR 
            ''' </summary>
            _VARIABLE_DECLARE
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
            _SAL_PROGRAM_BEGIN
            _SAL_EXPRESSION_BEGIN
            _PL_PROGRAM_BEGIN
            _Def
            _EQUALITY
            _JSON_digit
            _JSON_esc
            _JSON_int
            _JSON_exp
            _JSON_frac
            _FUNCTION_DECLARE
            _DOT
            _OBJ_string
            _OBJ_integer
            _OBJ_boolean
            _OBJ_array
            _OBJ_null
            ''' <summary>
            ''' Literal
            ''' </summary>
            _VARIABLE
            _WHITESPACE
            _COMMENTS
            'LOGO
            _repeat
            LOGO_fd
            LOGO_bk
            LOGO_rt
            LOGO_lt
            LOGO_cs
            LOGO_pu
            LOGO_pd
            LOGO_ht
            LOGO_st
            LOGO_deref
            LOGO_home
            LOGO_label
            LOGO_setxy
            LOGO_make
            LOGO_procedureInvocation
            LOGO_procedureDeclaration
            LOGO_parameterDeclarations
            LOGO_comparison
            LOGO_comparisonOperator
            LOGO_ife
            LOGO_Stop
            LOGO_fore
            LOGO_LANG
            LOGO_signExpression
            LOGO_multiplyingExpression
            LOGO_expression
            LOGO_EOL
            LOGO_number
            LOGO_name
        End Enum
        ''' <summary>
        ''' Simple Gramar object (Expected token Shape or from)
        ''' </summary>
        Public Class Grammar

            ''' <summary>
            ''' Identifier
            ''' </summary>
            Public ID As Type_Id
            ''' <summary>
            ''' RegEx Expression to search
            ''' </summary>
            Public Exp As String
            Public Shared Function GetExtendedGrammar() As List(Of Grammar)
                Dim lst As New List(Of Grammar)
                lst.AddRange(SALGrammar.GetSALGrammar)
                lst.AddRange(PLGrammar.GetPLGrammar)
                lst.AddRange(BaseGrammar.GetLogicGrammar)
                lst.AddRange(BaseGrammar.GetSymbolsGrammar)
                lst.AddRange(BaseGrammar.GetLiteralsGrammar)
                lst.AddRange(LogoGrammar.GetLOGOGrammar)
                Return lst
            End Function
            ''' <summary>
            ''' Still Developing grammar:
            ''' </summary>
            ''' <returns></returns>
            Public Shared Function GetJsonGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar

                '"tokens" "STRING NUMBER { } [ ] , : TRUE FALSE NULL",
                '"start": "JSONText",

                ''"JSONString" [[ "STRING", "$$ = yytext;" ]],

                ''"JSONNumber" [[ "NUMBER", "$$ = Number(yytext);" ]],

                ''"JSONNullLiteral" [[ "NULL", "$$ = null;" ]],

                ''"JSONBooleanLiteral" [[ "TRUE", "$$ = true;" ],
                ''                       [ "FALSE", "$$ = false;" ]],


                ''"JSONText" [[ "JSONValue", "return $$ = $1;" ]],

                ''"JSONValue" [[ "JSONNullLiteral",    "$$ = $1;" ],
                ''              [ "JSONBooleanLiteral", "$$ = $1;" ],
                ''              [ "JSONString",         "$$ = $1;" ],
                ''              [ "JSONNumber",         "$$ = $1;" ],
                ''              [ "JSONObject",         "$$ = $1;" ],
                ''              [ "JSONArray",          "$$ = $1;" ]],

                ''"JSONObject" [[ "{ }", "$$ = {};" ],
                ''               [ "{ JSONMemberList }", "$$ = $2;" ]],

                ''"JSONMember" [[ "JSONString : JSONValue", "$$ = [$1, $3];" ]],

                ''"JSONMemberList" [[ "JSONMember", "$$ = {}; $$[$1[0]] = $1[1];" ],
                ''                   [ "JSONMemberList , JSONMember", "$$ = $1; $1[$3[0]] = $3[1];" ]],

                ''"JSONArray" [[ "[ ]", "$$ = [];" ],
                ''              [ "[ JSONElementList ]", "$$ = $2;" ]],

                ''"JSONElementList" [[ "JSONValue", "$$ = [$1];" ],
                ''                    [ "JSONElementList , JSONValue", "$$ = $1; $1.push($3);" ]]


                '["\\s+", "/* skip whitespace */"],
                '["{int}{frac}?{exp}?\\b", "return 'NUMBER';"],
                '["\"(?:{esc}[\"bfnrt/{esc}]|{esc}u[a-fA-F0-9]{4}|[^\"{esc}])*\"", "yytext = yytext.substr(1,yyleng-2); return 'STRING';"],
                '["\\{", "return '{'"],
                '["\\}", "return '}'"],
                '["\\[", "return '['"],
                '["\\]", "return ']'"],
                '[",", "return ','"],
                '[":", "return ':'"],
                '["true\\b", "return 'TRUE'"],
                '["false\\b", "return 'FALSE'"],
                '["null\\b", "return 'NULL'"]


                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_digit
                NewGram.Exp = "^[0-9]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_esc
                NewGram.Exp = "^\\\\"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_int
                NewGram.Exp = "^-?(?:[0-9]|[1-9][0-9]+)"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_exp
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_frac
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)"
                iSpec.Add(NewGram)


                Return iSpec
            End Function

        End Class
    End Namespace
    '
    Public Class BaseGrammar

        Public Shared Function GetSymbolsGrammar() As List(Of Grammar)
            Dim iSpec As New List(Of Grammar)
            Dim NewGram As New Grammar
#Region "Seperators"
            'BLOCK CODE: LEFT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._CODE_BEGIN
            NewGram.Exp = "^\{"
            iSpec.Add(NewGram)
            'BLOCK CODE: RIGHT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._CODE_END
            NewGram.Exp = "^\}"
            iSpec.Add(NewGram)
            'END STATEMENT or EMPTY STATEMENT
            'EMPTY CODE BLOCKS CONTAIN (1 EMPTY STATEMENT)
            NewGram = New Grammar
            NewGram.ID = Type_Id._STATEMENT_END
            NewGram.Exp = "^\;"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._LIST_SEPERATOR
            NewGram.Exp = "^\,"
            iSpec.Add(NewGram)
            'ARGS LIST : LEFT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._LIST_BEGIN
            NewGram.Exp = "^\["
            iSpec.Add(NewGram)
            'ARGS LIST: RIGHT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._LIST_END
            NewGram.Exp = "^\]"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._DOT
            NewGram.Exp = "^\\."
            iSpec.Add(NewGram)

#End Region
            Return iSpec
        End Function
        Public Shared Function GetLogicGrammar() As List(Of Grammar)
            Dim iSpec As New List(Of Grammar)
            Dim NewGram As New Grammar


            'logical(boolean) - Literal
            NewGram = New Grammar
            NewGram.ID = Type_Id._TRUE
            NewGram.Exp = "^\btrue\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._FALSE
            NewGram.Exp = "^\bfalse\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._NULL
            NewGram.Exp = "^\bnull\b"
            iSpec.Add(NewGram)

            ''=
            NewGram = New Grammar
            NewGram.ID = Type_Id._SIMPLE_ASSIGN
            NewGram.Exp = "^\="
            iSpec.Add(NewGram)

            '*=, /=, +=, -=,
            NewGram = New Grammar
            NewGram.ID = Type_Id._COMPLEX_ASSIGN
            NewGram.Exp = "^(\*|\/|\+|\-)="
            iSpec.Add(NewGram)


            'Conditional BLOCK CODE: LEFT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._CONDITIONAL_BEGIN
            NewGram.Exp = "^\("
            iSpec.Add(NewGram)
            'Conditional BLOCK CODE: RIGHT BOUNDRY
            NewGram = New Grammar
            NewGram.ID = Type_Id._CONDITIONAL_END
            NewGram.Exp = "^\)"
            iSpec.Add(NewGram)

            'Logical Operators:  &&, ||
            NewGram = New Grammar
            NewGram.ID = Type_Id._LOGICAL_AND
            NewGram.Exp = "^\band\b"
            iSpec.Add(NewGram)

            NewGram = New Grammar
            NewGram.ID = Type_Id._LOGICAL_OR
            NewGram.Exp = "^\bor\b"
            iSpec.Add(NewGram)

            NewGram = New Grammar
            NewGram.ID = Type_Id._LOGICAL_NOT
            NewGram.Exp = "^\bnot\b"
            iSpec.Add(NewGram)

            'Equality operators: ==, !=
            NewGram = New Grammar
            NewGram.ID = Type_Id._EQUALITY
            NewGram.Exp = "^(=|!)=\="

            iSpec.Add(NewGram)

            'Relational operators: >, >=, <, <=
            NewGram = New Grammar
            NewGram.ID = Type_Id._RELATIONAL_OPERATOR
            NewGram.Exp = "^[><]\=?"
            iSpec.Add(NewGram)
            'Math operators: +, -, *, /
            NewGram = New Grammar
            NewGram.ID = Type_Id._ADDITIVE_OPERATOR
            NewGram.Exp = "^[+\-]"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._MULTIPLICATIVE_OPERATOR
            NewGram.Exp = "^[*/]"
            iSpec.Add(NewGram)

            Return iSpec
        End Function
        Public Shared Function GetLiteralsGrammar() As List(Of Grammar)
            Dim iSpec As New List(Of Grammar)

            Dim NewGram As New Grammar
            'Literals
            NewGram.ID = Type_Id._INTEGER
            NewGram.Exp = "^\d+"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._STRING
            NewGram.Exp = "^" & Chr(34) & "[^" & Chr(34) & "]*" & Chr(34)
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._STRING
            NewGram.Exp = "^'[^']*'"
            iSpec.Add(NewGram)

            NewGram = New Grammar
            NewGram.ID = Type_Id._WHITESPACE
            NewGram.Exp = "^\s"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._COMMENTS
            NewGram.Exp = "^\/\/.*"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._COMMENTS
            NewGram.Exp = "^\/\*[\s\S]*?\*\/"
            iSpec.Add(NewGram)

            'Variable
            NewGram = New Grammar
            NewGram.ID = Type_Id._VARIABLE
            NewGram.Exp = "^\b[a-z][a-z0-9]+\b"
            iSpec.Add(NewGram)
#Region "literal Object types"
#Region "ARRAY"
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_array
            NewGram.Exp = "\blist\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_array
            NewGram.Exp = "\barray\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_array
            NewGram.Exp = "\barraylist\b"
            iSpec.Add(NewGram)
#End Region
#Region "boolean"
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_boolean
            NewGram.Exp = "\bbool\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_boolean
            NewGram.Exp = "\bboolean\b"
            iSpec.Add(NewGram)
#End Region
#Region "NULL"
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_null
            NewGram.Exp = "\bnothing\b"
            iSpec.Add(NewGram)
#End Region
#Region "integer"
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_integer
            NewGram.Exp = "\bint\b"
            iSpec.Add(NewGram)
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_integer
            NewGram.Exp = "\binteger\b"
            iSpec.Add(NewGram)
#End Region
#Region "string"
            NewGram = New Grammar
            NewGram.ID = Type_Id._OBJ_string
            NewGram.Exp = "\bstring\b"
            iSpec.Add(NewGram)

#End Region
#End Region
            Return iSpec
        End Function
    End Class
    'PL GRAMMAR
    '
    '
    Namespace GrammarFactory
        Public Class PLGrammar
            ''' <summary>
            ''' Set OF KeyWords for Language with RegEx Search Expressions
            ''' Based on basic programming languge keywords and symbols /Literals
            ''' This is a preloaded Grammar (list of Grammar objects)
            ''' </summary>
            ''' <returns></returns>
            Public Shared Function GetPLGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar

                NewGram = New Grammar
                NewGram.ID = Type_Id._PL_PROGRAM_BEGIN
                NewGram.Exp = "\bspl_lang\b"
                iSpec.Add(NewGram)

#Region "Print"
                'Prints
                NewGram = New Grammar
                NewGram.ID = Type_Id._PRINT
                NewGram.Exp = "^\bprint\b"
                iSpec.Add(NewGram)
#End Region
#Region "Functions/Classes"
#Region "Return Value"
                'Functions/Classes
                NewGram = New Grammar
                NewGram.ID = Type_Id._RETURN
                NewGram.Exp = "^\breturn\b"
                iSpec.Add(NewGram)
#End Region

#Region "Declare Function"
                NewGram = New Grammar
                NewGram.ID = Type_Id._FUNCTION_DECLARE
                NewGram.Exp = "\bdef\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FUNCTION_DECLARE
                NewGram.Exp = "^\bfunction\b"
                iSpec.Add(NewGram)
#End Region



                NewGram = New Grammar
                NewGram.ID = Type_Id._CLASS
                NewGram.Exp = "^\bclass\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NEW
                NewGram.Exp = "^\bnew\b"
                iSpec.Add(NewGram)
#End Region
#Region "ASSIGNMENT"
                'ASSIGNMENT : Syntax  _Variable _AS 
                'Reconsidered Using Dim (Could Still Implement by changing Assignment handler/Generator)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\bdim\b\s"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\blet\s\b"
                iSpec.Add(NewGram)
                'Assignment operators: xLeft assigns output of right (9+4) (+= 9) (-=2) (3) (true)
#End Region
#Region "IF/THEN"
                'IF/THEN
                NewGram = New Grammar
                NewGram.ID = Type_Id._IF
                NewGram.Exp = "^\bif\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._ELSE
                NewGram.Exp = "^\belse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._THEN
                NewGram.Exp = "^\bthen\b"
                iSpec.Add(NewGram)


#End Region
#Region "DO WHILE/UNTIL"
                'DO WHILE/UNTIL
                NewGram = New Grammar
                NewGram.ID = Type_Id._WHILE
                NewGram.Exp = "^\bwhile\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\bas\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._UNTIL
                NewGram.Exp = "^\buntil\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._LOOP
                NewGram.Exp = "^\bloop\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._DO
                NewGram.Exp = "^\bdo\b"
                iSpec.Add(NewGram)
#End Region
#Region "For/Next"

                'For/To  For/Each/in /Next
                NewGram = New Grammar
                NewGram.ID = Type_Id._FOR
                NewGram.Exp = "^\bfor\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._EACH
                NewGram.Exp = "^\beach\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._TO
                NewGram.Exp = "^\bto\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NEXT
                NewGram.Exp = "^\bnext\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._IN
                NewGram.Exp = "^\bin\b"
                iSpec.Add(NewGram)

#End Region




                iSpec.AddRange(BaseGrammar.GetLogicGrammar)
                iSpec.AddRange(BaseGrammar.GetSymbolsGrammar)
                iSpec.AddRange(BaseGrammar.GetLiteralsGrammar)
                'ARGS LIST: RIGHT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._EOF
                NewGram.Exp = "EOF"
                iSpec.Add(NewGram)

                Return iSpec
            End Function
        End Class
    End Namespace
    'GRAMMAR-TOKEN
    '
    Namespace GrammarFactory
        ''' <summary>
        ''' Token to be returned 
        ''' </summary>
        Public Structure Token
            ''' <summary>
            ''' Simple identifier
            ''' </summary>
            Public ID As Type_Id
            ''' <summary>
            ''' Held Data
            ''' </summary>
            Public Value As String
            ''' <summary>
            ''' Start of token(Start position)
            ''' </summary>
            Public _start As Integer
            ''' <summary>
            ''' End of token (end Position)
            ''' </summary>
            Public _End As Integer

            Public Function ToJson() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)

            End Function
        End Structure
    End Namespace
    'SAL GRAMMAR
    '
    Namespace GrammarFactory
        Public Class SALGrammar
            Public Shared Function GetSALGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar
                NewGram = New Grammar
                NewGram.ID = Type_Id._SAL_PROGRAM_BEGIN
                NewGram.Exp = "^\bsal_lang\b"
                iSpec.Add(NewGram)

                NewGram = New Grammar
                NewGram.ID = Type_Id._SAL_EXPRESSION_BEGIN
                NewGram.Exp = "^\bsal\b"
                iSpec.Add(NewGram)


                NewGram = New Grammar
                NewGram.ID = Type_Id._WHITESPACE
                NewGram.Exp = "^\s"
                iSpec.Add(NewGram)

                NewGram.ID = Type_Id._SIMPLE_ASSIGN
                NewGram.Exp = "^\bassigns\b"
                iSpec.Add(NewGram)


#Region "SAL"
                'Sal_Cmds
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_REMOVE
                NewGram.Exp = "^\bremove\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_RESUME
                NewGram.Exp = "^\bresume\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PUSH
                NewGram.Exp = "^\bpush\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PULL
                NewGram.Exp = "^\bpull\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PEEK
                NewGram.Exp = "^\bpeek\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_WAIT
                NewGram.Exp = "^\bwait\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PAUSE
                NewGram.Exp = "^\bpause\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_HALT
                NewGram.Exp = "^\bhalt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DUP
                NewGram.Exp = "^\bdup\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JMP
                NewGram.Exp = "^\bjmp\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_T
                NewGram.Exp = "^\bjif_t\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_F
                NewGram.Exp = "^\bjif_f\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_EQ
                NewGram.Exp = "^\bjif_eq\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_GT
                NewGram.Exp = "^\bjif_gt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_LT
                NewGram.Exp = "^\bjif_lt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_LOAD
                NewGram.Exp = "^\bload\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_STORE
                NewGram.Exp = "^\bstore\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_CALL
                NewGram.Exp = "^\bcall\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_RET
                NewGram.Exp = "^\bret\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PRINT_M
                NewGram.Exp = "^\bprint_m\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PRINT_C
                NewGram.Exp = "^\bprint_c\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_ADD
                NewGram.Exp = "^\badd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_SUB
                NewGram.Exp = "^\bsub\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_MUL
                NewGram.Exp = "^\bmul\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DIV
                NewGram.Exp = "^\bdiv\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_ADD
                NewGram.Exp = "^\band\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_OR
                NewGram.Exp = "^\bor\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_NOT
                NewGram.Exp = "^\bnot\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_LT
                NewGram.Exp = "^\bis_eq\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_GT
                NewGram.Exp = "^\bis_gt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_GTE
                NewGram.Exp = "^\bis_gte\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_LT
                NewGram.Exp = "^\bis_lt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_LT
                NewGram.Exp = "^\bis_lte\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_TO_POS
                NewGram.Exp = "^\bto_pos\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_TO_NEG
                NewGram.Exp = "^\bto_neg\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_INCR
                NewGram.Exp = "^\bincr\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DECR
                NewGram.Exp = "^\bdecr\b"
                iSpec.Add(NewGram)
                'logical(boolean) - Literal
                NewGram = New Grammar
                NewGram.ID = Type_Id._TRUE
                NewGram.Exp = "^\btrue\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FALSE
                NewGram.Exp = "^\bfalse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)

#End Region

                iSpec.AddRange(BaseGrammar.GetLogicGrammar)
                iSpec.AddRange(BaseGrammar.GetSymbolsGrammar)
                iSpec.AddRange(BaseGrammar.GetLiteralsGrammar)

                NewGram = New Grammar
                NewGram.ID = Type_Id._EOF
                NewGram.Exp = "EOF"
                iSpec.Add(NewGram)
                Return iSpec
            End Function
        End Class
    End Namespace

End Namespace
Namespace SmallProgLang
    'LOGO GRAMMAR
    '
    Namespace GrammarFactory
        'https://www.tutorialspoint.com/logo/logo_introduction.htm
        'https://www.transum.org/software/Logo/ (Online logo)
        'https://www.calormen.com/jslogo/

        'FORWARD	    fd	            FORWARD(space)<number Of steps To move forward>	Moves turtle forward For number Of times specified	"forward 100" Or "fd 100"
        'BACK	        bk	            BACK(space) <number Of steps To move backward>	Moves turtle back For number Of times specified	"back 100" Or "bk 100"
        'RIGHT	        rt	            RIGHT(space) <degrees To rotate toward right	Turns turtle right For number Of degrees specified	"right 228" Or "rt 228"
        'LEFT	        lt	            LEFT(space) <degrees To rotate toward left >	Turns turtle left For number Of degrees specified	"left 228" Or "lt 228"
        'HOME	        home	        Home	Comes To screen center but does Not clear the screen	"home"
        'CLEAN	        ct cs	        Clean	Clears the screen Of trails but the turtle remains where it Is without moving	"clean"
        'CLEARSCREEN	CS	            Clearscreen	Clears the screen Of trails And comes To screen center	"cs"
        'HIDETURTLE	    HT	            Hide turtle	Hides the turtle And aids viewing a clear drawing On the screen	"ht"
        'SHOWTURTLE	    ST	            Show turtle	Shows the turtle after it Is hidden from the screen	"st"
        'PENUP	        PU(set)         Pen up	Sets the turtle To move without drawing	"pu"
        'PENDOWN	    PD(resets)      Pen	Resets To a drawing pen When ordered To move	"pd"
        'CLEARTEXT	    CT	Clear text	Clears all text In the command screen	"ct"



        '
        'signExpression
        '   (('+' | '-'))* (number | deref | func)
        '
        'multiplyingExpression
        '    : signExpression (('*' | '/') signExpression)*
        '
        'expression
        '    : multiplyingExpression (('+' | '-') multiplyingExpression)*
        '
        'parameterDeclarations
        '       : ':' name (',' parameterDeclarations)*
        '
        'procedureDeclaration
        '       : 'to' name parameterDeclarations* EOL? (line? EOL) + 'end'
        '
        'procedureInvocation
        '       : name expression*
        '
        'deref
        '   ':' name
        '
        'fd
        '   : ('fd' | 'forward') expression
        '
        'bk
        '   : ('bk' | 'backward') expression
        '
        'rt
        '   : ('rt' | 'right') expression
        '
        'lt 
        '    : ('lt' | 'left') expression
        '
        'cs
        '    : cs
        '    : clearscreen
        '
        'pu
        '    : pu
        '    : penup
        '
        'pd
        '
        '    : pd
        '    : pendown
        '
        'ht
        '
        '    : ht
        '    : hideturtle
        '
        'st'
        '
        '    : st
        '    : showturtle
        '
        '    : Home
        '
        '    : Stop
        '
        '    : label
        '
        'setxy
        '    : setxy expression expression
        '
        'random
        '
        '    : random expression
        '
        'for
        '    : 'for' '[' name expression expression expression ']' block
        '
        'value
        ' String / Expression / deref
        '
        'name
        '   String
        '
        'print
        '    : 'print' (value | quotedstring)
        '
        'make
        '    : 'make' STRINGLITERAL value
        '
        'comparison
        ' : expression comparisonOperator expression
        '
        'comparisonOperator
        '       '<'
        '       '>'
        '       '='
        '
        'if
        '       'if' comparison block
        ' block
        '       '[' cmd + ']'
        '
        'repeat
        '       : 'repeat' number block
        '
        'func
        '   : random
        '
        'line
        '       : cmd + comment?
        '       : comment
        '       : print comment?
        '       : procedureDeclaration
        '
        'prog
        '       :(line? EOL) + line?
        '
        'comment
        '       : COMMENT
        '       :  ~ [\r\n]*';'
        ''' <summary>
        ''' Logo Programming Language
        ''' </summary>
        Public Class LogoGrammar
            Public Shared Function GetLOGOGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar
                NewGram = New Grammar
                NewGram.ID = Type_Id._COMMENTS
                NewGram.Exp = "^\~"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_LANG
                NewGram.Exp = "\blogo_lang\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_EOL
                NewGram.Exp = "^\;"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_deref
                NewGram.Exp = "^\:"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_fd
                NewGram.Exp = "\bfd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_fd
                NewGram.Exp = "\bforward\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_bk
                NewGram.Exp = "\bbackward\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_bk
                NewGram.Exp = "\bbk\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_rt
                NewGram.Exp = "\brt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_rt
                NewGram.Exp = "\bright\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_lt
                NewGram.Exp = "\blt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_lt
                NewGram.Exp = "\bleft\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_cs
                NewGram.Exp = "\bcs\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_cs
                NewGram.Exp = "\bclearscreen\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_pu
                NewGram.Exp = "\bpu\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_pu
                NewGram.Exp = "\bpenup\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_pd
                NewGram.Exp = "\bpd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_pd
                NewGram.Exp = "\bpendown\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_ht
                NewGram.Exp = "\bht\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_ht
                NewGram.Exp = "\bhideturtle\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_st
                NewGram.Exp = "\bst\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_st
                NewGram.Exp = "\bshowturtle\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_label
                NewGram.Exp = "\blabel\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_setxy
                NewGram.Exp = "\bsetxy\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_make
                NewGram.Exp = "\bmake\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_ife
                NewGram.Exp = "\bife\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_fore
                NewGram.Exp = "\bfore\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._STRING
                NewGram.Exp = "^" & Chr(34) & "[^" & Chr(34) & "]*" & Chr(34)
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._STRING
                NewGram.Exp = "^'[^']*'"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._WHITESPACE
                NewGram.Exp = "^\s"
                iSpec.Add(NewGram)
                'logical(boolean) - Literal
                NewGram = New Grammar
                NewGram.ID = Type_Id._TRUE
                NewGram.Exp = "^\btrue\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FALSE
                NewGram.Exp = "^\bfalse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)
                'Variable
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_name
                NewGram.Exp = "^\b[a-z][a-z0-9]+\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_number
                NewGram.Exp = "^\d+"
                iSpec.Add(NewGram)
                ''=
                NewGram = New Grammar
                NewGram.ID = Type_Id._SIMPLE_ASSIGN
                NewGram.Exp = "^\="
                iSpec.Add(NewGram)

                '*=, /=, +=, -=,
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_signExpression
                NewGram.Exp = "^(\*|\/|\+|\-)="
                iSpec.Add(NewGram)

                'Equality operators: ==, !=
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_comparisonOperator
                NewGram.Exp = "^(=|!)=\="
                iSpec.Add(NewGram)
                'Relational operators: >, >=, <, <=
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_comparisonOperator
                NewGram.Exp = "^[><]\=?"
                iSpec.Add(NewGram)
                'Math operators: +, -, *, /
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_signExpression
                NewGram.Exp = "^[+\-]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.LOGO_multiplyingExpression
                NewGram.Exp = "^[*/]"
                iSpec.Add(NewGram)





                Return iSpec
            End Function

            Public Function GetLogoRef() As String
                Dim str As String = ""
                str = My.Resources.LOGO_QUICK_REF
                Return str
            End Function
        End Class
    End Namespace
End Namespace
#End Region
'THE TOKENIZER (LEXER)
'
#Region "TOKENIZER"
'LEXER - TOKENIZER - SCANNER
'
Namespace SmallProgLang
    Namespace Compiler
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Lexer
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private Cursor As Integer = 0
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private EoFCursor As Integer = 0
            ''' <summary>
            ''' Program being Tokenized
            ''' </summary>
            Private CurrentScript As String = ""
            Private iPastTokens As New List(Of Token)
            Private iLastToken As Token
            Public ReadOnly Property PastTokens As List(Of Token)
                Get
                    Return iPastTokens
                End Get
            End Property
            Private Property LastToken As Token
                Get
                    Return iLastToken
                End Get
                Set(value As Token)
                    iLastToken = value
                    iPastTokens.Add(value)
                End Set
            End Property
            Public Function GetLastToken() As Token
                Return LastToken
            End Function
            ''' <summary>
            ''' Returns from index to end of file (Universal function)
            ''' </summary>
            ''' <param name="Str">String</param>
            ''' <param name="indx">Index</param>
            ''' <returns></returns>
            Public Shared Function GetSlice(ByRef Str As String, ByRef indx As Integer) As String
                If indx <= Str.Length Then
                    Str.Substring(indx)
                    Return Str.Substring(indx)
                Else
                End If
                Return Nothing
            End Function
            Public ReadOnly Property EndOfFile As Boolean
                Get
                    If Cursor >= EoFCursor Then
                        Return True
                    Else
                        Return False
                    End If
                End Get

            End Property
            ''' <summary>
            ''' Gets next token moves cursor forwards
            ''' </summary>
            ''' <returns></returns>
            Public Function GetNext() As String
                If EndOfFile = False Then
                    Dim slice = GetSlice(Me.CurrentScript, Me.Cursor)
                    If slice IsNot Nothing Then
                        Cursor += slice.Length
                        Return slice

                    Else
                        'Errors jump straight to enod of file
                        Cursor = EoFCursor
                        Return "EOF"
                    End If
                Else
                    Return "EOF"
                End If
            End Function
            ''' <summary>
            ''' Checks token without moving the cursor
            ''' </summary>
            ''' <returns></returns>
            Public Function ViewNext() As String
                If EndOfFile = False Then
                    Dim slice = GetSlice(Me.CurrentScript, Me.Cursor)
                    If slice IsNot Nothing Then
                        Return slice
                    Else
                        'Does not change anything
                        Return "EOF"
                    End If
                Else
                    Return "EOF"
                End If
            End Function
            ''' <summary>
            ''' Main Searcher
            ''' </summary>
            ''' <param name="Text">to be searched </param>
            ''' <param name="Pattern">RegEx Search String</param>
            ''' <returns></returns>
            Private Shared Function RegExSearch(ByRef Text As String, Pattern As String) As List(Of String)
                Dim Searcher As New Regex(Pattern)
                Dim iMatch As Match = Searcher.Match(Text)
                Dim iMatches As New List(Of String)
                Do While iMatch.Success
                    iMatches.Add(iMatch.Value)
                    iMatch = iMatch.NextMatch
                Loop
                Return iMatches
            End Function
            ''' <summary>
            ''' Steps the tokenizer backwards
            ''' </summary>
            ''' <param name="TokenStr"></param>
            Public Sub StepBack(ByRef TokenStr As String)
                'If last value was the same then do.
                If TokenStr = LastToken.Value Then
                    Try
                        Cursor = Cursor - TokenStr.Length
                        'Removes last token
                        iPastTokens.RemoveAt(PastTokens.Count - 1)
                        'sets last token to newlast on the list
                        iLastToken = PastTokens(PastTokens.Count - 1)
                    Catch ex As Exception
                        'Error (cant go back)
                    End Try
                Else
                    'Was not the last value in stack
                End If


            End Sub
            Public Function Eat(ByRef TokenType As Type_Id) As Token
                Dim Strt As Integer = Cursor
                If IdentifiyToken(ViewNext()) = TokenType Then

                    'With return "" if nothing is detected
                    Dim str As String = ""
                    'Build token until token is not the type (if token is of type then add it)

                    'Get next advances the cursor
                    str &= GetNext()

                    Dim _end As Integer = Cursor
                    Dim Toke As New Token
                    Toke.ID = TokenType
                    Toke.Value = str
                    Toke._start = Strt
                    Toke._End = _end
                    'Preserve token returned
                    LastToken = Toke

                    Return Toke
                Else
                    'Not match tokentype
                    Return Nothing
                End If

            End Function
            ''' <summary>
            ''' Identifys token but due to some tokens maybe cross identifying 
            ''' CheckIdentifiedToken will return the full token without moving the cursor
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function IdentifiyToken(ByRef CurrentTok As String) As GrammarFactory.Type_Id

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then



                        Return item.ID
                    Else
                        'Check next
                    End If
                Next

                Return Type_Id._BAD_TOKEN
            End Function
            ''' <summary>
            ''' Identifys token
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function GetIdentifiedToken(ByRef CurrentTok As String) As Token

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New Token
                        tok.ID = item.ID
                        tok.Value = matches(0)
                        tok._start = Cursor
                        tok._End = Cursor + tok.Value.Length
                        Cursor = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New Token
                btok.ID = Type_Id._BAD_TOKEN
                btok.Value = CurrentTok
                btok._start = Cursor
                btok._End = Cursor + CurrentScript.Length
                Cursor = EoFCursor
                Return btok

            End Function
            Public Function CheckIdentifiedToken(ByRef CurrentTok As String) As Token

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New Token
                        tok.ID = item.ID
                        tok.Value = matches(0)
                        tok._start = Cursor
                        tok._End = Cursor + tok.Value.Length
                        ' Cursor = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New Token
                btok.ID = Type_Id._BAD_TOKEN
                btok.Value = CurrentTok
                btok._start = Cursor
                btok._End = Cursor + CurrentScript.Length

                Return btok

            End Function
            Public CurrentGrammar As List(Of GrammarFactory.Grammar)
            Public Sub New(ByRef Script As String)
                Me.CurrentScript = Script
                EoFCursor = Script.Length
                CurrentGrammar = GrammarFactory.Grammar.GetExtendedGrammar
            End Sub
            ''' <summary>
            ''' Use for Sal and OtherLangs
            ''' </summary>
            ''' <param name="Script"></param>
            ''' <param name="Grammar"></param>
            Public Sub New(ByRef Script As String, ByRef Grammar As List(Of GrammarFactory.Grammar))
                Me.CurrentScript = Script
                EoFCursor = Script.Length
                CurrentGrammar = Grammar
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace
#End Region
' => SAL TRANSPLIER
'
#Region "THE TRANSPILER"
Namespace SmallProgLang
    Namespace Transpiler
        ''' <summary>
        ''' Transpiles to SAL Code; 
        ''' Can be run on SAL Assembler
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Sal_Transpiler
            ''' <summary>
            ''' Line number counter
            ''' </summary>
            Private LineNumber As Integer
            ''' <summary>
            ''' Increases the current line number to track the line number in the program
            ''' </summary>
            Private Sub IncrLineNumber()
                LineNumber += 1
            End Sub
            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
            ''' <summary>
            ''' As Each Expression is consumed an program will be returned 
            ''' Each Expression Should be an array 
            ''' Provided by the AST toArray Function 
            ''' Which only Creates the Desired nodes in the order Expected by the Transpiler(RPL)
            ''' </summary>
            ''' <param name="Ast"></param>
            ''' <returns></returns>
            Public Function Transpile(ByRef Ast As AstExpression) As List(Of String)
                Dim Expr = Ast.ToArraylist
                'Created for tracking 
                IncrLineNumber()

                'Detect Binary Expressions
                Select Case Expr.Count
                    Case 2
                        Return (_print(Expr(1)))
                    Case 3
                        'Function : to get literal will be required 
                        Return _Binary_op(Expr(1), Expr(2), Expr(0))
                End Select

                'If it is not a binary op
                ''then it can be identified by its type
                Select Case Ast._Type

                End Select

                Dim str As New List(Of String)
                str.Add("Not Implemented Bad Expression Syntax =" & FormatJsonOutput(Expr.ToJson) & vbNewLine & "LineNumber =" & LineNumber)
                Return str
            End Function

#Region "Generators"

            ''' <summary>
            ''' Generates SalCode For Binary ops
            ''' -Simple Assign
            ''' -Conditional
            ''' -Addative
            ''' -Multiplicative
            ''' 
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="Right"></param>
            ''' <param name="iOperator"></param>
            ''' <returns></returns>
            Private Function _Binary_op(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As List(Of String)

                Dim PROGRAM As New List(Of String)
                Select Case iOperator
                    Case "-"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("SUB")
                        PROGRAM.Add("PRINT_M")

                    Case "+"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("ADD")
                        PROGRAM.Add("PRINT_M")

                    Case "/"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("DIV")
                        PROGRAM.Add("PRINT_M")

                    Case "*"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("MUL")
                        PROGRAM.Add("PRINT_M")
                    Case ">"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_GT")
                        PROGRAM.Add("PRINT_M")

                    Case "<"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_LT")
                        PROGRAM.Add("PRINT_M")

                    Case ">="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_GTE")
                        PROGRAM.Add("PRINT_M")

                    Case "<="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_LTE")
                        PROGRAM.Add("PRINT_M")

                    Case "="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_EQ")
                        PROGRAM.Add("PRINT_M")

                End Select
                Return PROGRAM
            End Function
            Private Function _print(ByRef Str As String) As List(Of String)

                Dim PROGRAM As New List(Of String)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Str.Replace("'", ""))
                PROGRAM.Add("PRINT_M")
                Return PROGRAM

            End Function
#End Region

        End Class
    End Namespace
End Namespace
#End Region
#End Region
'EXECUTION
'
#Region "THE EVALUATOR"
'THE EVALUATOR
'
#Region "EVAULATOR"
'S-EXPRESSION EVALUATOR
'
Namespace SmallProgLang
    Namespace Evaluator

        ''' <summary>
        ''' Evaluates Arrays of tokens, 
        ''' Taken from the populated AST NODES, 
        ''' The Expected format is Reverse poilsh Notation. Operator/ (Operands)
        ''' The Product is returned ; 
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class S_Expression_Evaluator
            Private GlobalEnvironment As EnvironmentalMemory
            ''' <summary>
            ''' Create a new instance of the PROGRAMMING Interpretor
            ''' </summary>
            ''' <param name="iGlobal">Used to provide preloaded environment</param>
            Public Sub New(ByRef iGlobal As EnvironmentalMemory)
                GlobalEnvironment = iGlobal
                LineNumber = 0
            End Sub
            ''' <summary>
            ''' Line number counter
            ''' </summary>
            Private LineNumber As Integer
            ''' <summary>
            ''' Increases the current line number to track the line number in the program
            ''' </summary>
            Private Sub IncrLineNumber()
                LineNumber += 1
            End Sub
            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
            ''' <summary>
            ''' Used for imediate Evaluations
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <param name="Env"></param>
            ''' <returns></returns>
            Public Function Eval(ByRef Expr As Object, Optional Env As EnvironmentalMemory = Nothing) As Object
                'Created for tracking 
                IncrLineNumber()
                If Env Is Nothing Then
                    Env = GlobalEnvironment
                End If
#Region "Get Literal Literals"
                '[Literals]
                '- 3 0r 5.6 [integer]
                If IsNumberInt(Expr) = True Then
                    Dim num As Integer = Integer.Parse(Expr)
                    Return num
                End If
                '- "CAT"[String]
                If IsString(Expr) = True Then
                    Return Expr.ToString
                End If
                ' - $VAR$
                If IsVariableName(Expr) = True Then
                    Return Env.GetVar(Expr(0))
                End If
                ' - "TRUE" "FALSE"
                If IsBoolean(Expr) = True Then
                    If Expr = "TRUE" Then
                        Return True
                    Else
                        Return False
                    End If
                End If
#End Region
                'To Identify What type of Expression
                'A Count of the Supplied elements is taken: 

                If IsArray(Expr) = True Then
                    Select Case Expr.count
                        Case 4
                            '       Case Expr(0) = "DIM"
                            'Syntax: 
                            'Dim Var = 92
                            'Dim var = False
                            'Dim var = ""
                            'Assign in Global Memory
                            Env.Define(Expr(1), Expr(3))
                            Return True

                        'Syntax: 
                            '4 * 92
                            'A >= B
                            '4 + 92
                            'A <= B
                        Case 3
                            Select Case Expr(0)
         'Maths Expressions (Recursive)
            'Syntax: Basic Arithmatic + 4 6
         '+ 3 5
         '+ $VAR$ $VAR$
         '+ $VAR$ 3
                                Case "+"
                                    Return Eval(Expr(1)) + Eval(Expr(2))
                                Case "-"
                                    Return Eval(Expr(1)) - Eval(Expr(2))
                                Case "*"
                                    Return Eval(Expr(1)) * Eval(Expr(2))
                                Case "/"
                                    Return Eval(Expr(1)) / Eval(Expr(2))
    'Conditionals: (Recursive)
    'Syntax: Basic Conditionals < 4 6
         '< 3 5
         '< $VAR$ $VAR$
         '< $VAR$ 3
                                Case ">"
                                    Return Eval(Expr(1)) > Eval(Expr(2))
                                Case "<"
                                    Return Eval(Expr(1)) > Eval(Expr(2))
                                Case ">="
                                    Return Eval(Expr(1)) >= Eval(Expr(2))
                                Case "<="
                                    Return Eval(Expr(1)) <= Eval(Expr(2))
                                Case "="
                                    Return Eval(Expr(1)) = Eval(Expr(2))



                            End Select

                        Case 6
                            Select Case Expr(0)
                                'Syntax: 
                    'While(0)
                    '(>(1)a(2)b(3)) = true(4)
                    '{Codeblock(5)}
                                Case "WHILE"
                                    ' #while Op var1 var2 EQVAR Codeblock #loop
                                    Env.Define("WHILE", "BOOLEAN")
                                    'Get Result expression (conditional) or (maths)
                                    Dim Result() As String = ({Eval(Expr(1)), Eval(Expr(2)), Eval(Expr(3))})
                                    'WhileCmd
                                    Do While (Eval(Result) = Eval(Expr(4)))
                                        Dim WhileEnv As New EnvironmentalMemory(Env)
                                        'RETURN ENVIRONEMT? (TEST) PERHAPS NOTHING NEEDED TO BE RETURNED
                                        Env = EvalBlock(Expr(5), WhileEnv)
                                    Loop
                                    ' #expr(6) = "Loop" (Signify End of Loop)
                                    Return True
                            End Select
                    End Select
                End If

                Return "Not Implemented Bad Expression Syntax =" & Expr & vbNewLine & "LineNumber =" & LineNumber
            End Function
            ''' <summary>
            ''' Evaluates a block of code returning the Global Environment the block environment is disposed of
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <param name="Env"></param>
            ''' <returns></returns>
            Public Function EvalBlock(ByRef Expr As Object, Optional Env As EnvironmentalMemory = Nothing) As EnvironmentalMemory
                If Env Is Nothing Then
                    Env = GlobalEnvironment
                End If
                For Each item In Expr
                    Eval(Expr, Env)
                Next
                ' CHanges to the global memory enviroment need to be made? 
                Return Env.GlobalMemory
            End Function
            Private Function _CheckCondition(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As Boolean

                Select Case iOperator

                    Case ">"
                        If Left > Right Then
                            Return True
                        Else
                            Return False
                        End If


                    Case "<"
                        If Left < Right Then
                            Return True
                        Else
                            Return False
                        End If

                    Case ">="
                        If Left >= Right Then
                            Return True
                        Else
                            Return False
                        End If

                    Case "<="
                        If Left <= Right Then
                            Return True
                        Else
                            Return False
                        End If

                    Case "="
                        If Left = Right Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Else
                        Return False
                End Select

            End Function


#Region "Literals"
            ''' <summary>
            ''' Checks if expr is a number, Returning number
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <returns></returns>
            Function IsNumberInt(ByRef Expr As Object) As Boolean
                Try
                    If TypeOf Expr Is Integer Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try
            End Function
            ''' <summary>
            ''' Checks if Expr is string returning the string
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <returns></returns>
            Function IsString(ByRef Expr As Object) As Boolean
                '########## ############# ######
                'Align with front and back char "


                Try
                    If TypeOf Expr Is String Then
                        If Expr.contains(Chr(34)) Then
                            If Expr.contains(Chr(36)) Then
                                Return False
                            Else
                                Return True
                            End If
                        Else
                        End If

                    Else
                        Return False
                    End If

                Catch ex As Exception
                    Return False
                End Try
                Return False
            End Function
            ''' <summary>
            ''' If Epr token is variable ake then it can be handled correctlly
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <returns></returns>
            Function IsVariableName(ByRef Expr As Object) As Boolean

                '# ########################################## ##
                '#Align with front and back char $

                Try
                    If TypeOf Expr Is String Then
                        If Expr.contains(Chr(36)) Then
                            If Expr.contains(Chr(34)) Then
                                Return False
                            Else
                            End If
                            Return True
                        Else
                            Return False
                        End If

                    End If

                Catch ex As Exception
                    Return False
                End Try
                Return False
            End Function
            Function IsBoolean(ByRef Expr As Object) As Boolean
                IsBoolean = False
                If Expr = "TRUE" Or Expr = "FALSE" Then
                    Return True
                End If
            End Function
            ''' <summary>
            ''' if token is an array then it contains an expression
            ''' </summary>
            ''' <param name="Expr"></param>
            ''' <returns></returns>
            Function IsArray(ByRef Expr As Object) As Boolean
                Try
                    If TypeOf Expr Is Array Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    Return False
                End Try
            End Function
#End Region
        End Class
    End Namespace
End Namespace
#End Region
'EVALUATOR - ENVIRONMANTAL MEMORY
'
Namespace SmallProgLang
    Namespace Evaluator
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class EnvironmentalMemory
            ''' <summary>
            ''' Structure for variable
            ''' </summary>
            Public Structure Variable
                ''' <summary>
                ''' Variabel name
                ''' </summary>
                Public Name As String
                ''' <summary>
                ''' Value of variable
                ''' </summary>
                Public Value As Object
                ''' <summary>
                ''' Type ass string identifier
                ''' </summary>
                Public Type As Ast_ExpressionFactory.AST_NODE
            End Structure
            ''' <summary>
            ''' Memory for variables
            ''' </summary>
            Private LocalMemory As List(Of Variable)
            Private mGlobalMemory As EnvironmentalMemory
            ''' <summary>
            ''' Global memeory passed in from parent environment
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property GlobalMemory As EnvironmentalMemory
                Get
                    Return mGlobalMemory
                End Get
            End Property
            ''' <summary>
            ''' A global memeory is contained within environmemt for referencing
            ''' </summary>
            ''' <param name="GlobalMemory"></param>
            Public Sub New(ByRef GlobalMemory As EnvironmentalMemory)
                LocalMemory = New List(Of Variable)
                Me.mGlobalMemory = GlobalMemory

            End Sub
            Private Function _AddInternalLiterals() As List(Of Variable)
                Dim Lst As New List(Of Variable)
                Dim BTrue As New Variable
                BTrue.Name = "TRUE"
                BTrue.Value = True
                BTrue.Type = Ast_ExpressionFactory.AST_NODE._boolean
                Lst.Add(BTrue)
                Dim BFalse As New Variable
                BFalse.Name = "FALSE"
                BFalse.Value = False
                BFalse.Type = Ast_ExpressionFactory.AST_NODE._boolean
                Lst.Add(BFalse)
                Dim BNull As New Variable
                BNull.Name = "NULL"
                BNull.Value = Nothing
                BNull.Type = Ast_ExpressionFactory.AST_NODE._null
                Lst.Add(BNull)
                Return Lst
            End Function
            ''' <summary>
            ''' Has no Global Memory
            ''' </summary>
            Public Sub New()
                LocalMemory = _AddInternalLiterals()
                '  mGlobalMemory = New EnvironmentalMemory


                '  Me.GlobalMemory = GlobalMemory
            End Sub
            ''' <summary>
            ''' Defines variable in environemnt if not previoulsy exisiting
            ''' </summary>
            ''' <param name="Name">Variable name</param>
            ''' <param name="nType">stype such as string/ integer etc</param>
            ''' <returns></returns>
            Public Function Define(ByRef Name As String, nType As String) As Object
                Dim detected As Boolean = False
                Dim var As New Variable
                var.Name = Name
                var.Type = nType
                If Me.GetVar(Name) = "NULL" Then
                    LocalMemory.Add(var)
                    Return True
                End If
                Return "ERROR : Unable to define Variable Name: -: -" & Name & " -: -:Exists Previously in memory"
            End Function
            ''' <summary>
            ''' Assigns a value to the variable
            ''' </summary>
            ''' <param name="Name">Variable name(Previoulsy Exisiting)</param>
            ''' <param name="Value">Value to be stored</param>
            ''' <returns></returns>
            Public Function AssignValue(ByRef Name As String, ByRef Value As Object) As Object
                For Each item In LocalMemory
                    If item.Name = Name Then
                        'IN LOCAL
                        item.Value = Value
                        Return True
                    End If
                Next
                If GlobalMemory IsNot Nothing Then
                    For Each item In GlobalMemory.LocalMemory
                        If item.Name = Name Then
                            '                    'Found in Global
                            item.Value = Value
                            Return True
                        End If
                    Next
                Else
                    'THIS IS THE GLOBAL MEMORY
                End If
                Return "ERROR : Unable to Assign Value  :=" & Value & " -: Not in Memory :- Variable Unknown :=" & Name
            End Function
            ''' <summary>
            ''' Returns the value from the stored variable
            ''' </summary>
            ''' <param name="Name"></param>
            ''' <returns></returns>
            Public Function GetVar(ByRef Name As String) As Object
                For Each item In LocalMemory
                    If item.Name = Name Then
                        'Found in Local Memeory
                        Return item.Value
                    End If
                Next
                If GlobalMemory IsNot Nothing Then

                    If GlobalMemory.GetVar(Name) <> "NULL" Then
                        Return GlobalMemory.GetVar(Name)
                    End If

                Else
                    'THIS IS THE GLOBAL MEMORY
                End If
                Return "NULL"

            End Function
            Public Function CheckVar(ByRef VarName As String) As Boolean
                For Each item In LocalMemory
                    If item.Name = VarName Then
                        Return True
                    End If
                Next
                CheckVar = False
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace
#End Region
'HELPER EXTENSIONS
'
#Region "EXTENSIONS"
Namespace SmallProgLang
    ''' <summary>
    ''' Minor Extension Methods; Required for json formatting
    ''' </summary>
    Public Module EXT
        <Runtime.CompilerServices.Extension()>
        Public Function FormatJsonOutput(ByVal jsonString As String) As String
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
        <Runtime.CompilerServices.Extension()>
        Public Function ToJson(ByRef item As Object) As String
            Dim Converter As New JavaScriptSerializer
            Return Converter.Serialize(item)

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function SplitAtNewLine(input As String) As IEnumerable(Of String)
            Return input.Split({Environment.NewLine}, StringSplitOptions.None)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractLastChar(ByRef InputStr As String) As String
            ExtractLastChar = Right(InputStr, 1)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractFirstChar(ByRef InputStr As String) As String
            ExtractFirstChar = Left(InputStr, 1)
        End Function
    End Module
End Namespace
#End Region
'SPYDAZWEB AL VIRTUAL MACHINE
'
'
#Region "SPYDAZWEB ASSEMBLY LANGUAGE VIRTUAL MACHINE"
'EXTENSIONS
'
Namespace SAL
    Public Module Ext
        <System.Runtime.CompilerServices.Extension()>
        Public Function SplitAtNewLine(input As String) As IEnumerable(Of String)
            Return input.Split({Environment.NewLine}, StringSplitOptions.None)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractLastChar(ByRef InputStr As String) As String
            ExtractLastChar = Right(InputStr, 1)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractFirstChar(ByRef InputStr As String) As String
            ExtractFirstChar = Left(InputStr, 1)
        End Function

    End Module
End Namespace
'HELPER FUNCTIONS
'
Namespace SAL
    Public Module SalCode_Helpers
        Private Function _Binary_op(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As List(Of String)

            Dim PROGRAM As New List(Of String)
            Select Case iOperator
                Case "-"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("SUB")
                    PROGRAM.Add("PRINT_M")

                Case "+"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("ADD")
                    PROGRAM.Add("PRINT_M")

                Case "/"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("DIV")
                    PROGRAM.Add("PRINT_M")

                Case "*"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("MUL")
                    PROGRAM.Add("PRINT_M")
                Case ">"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_GT")
                    PROGRAM.Add("PRINT_M")

                Case "<"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_LT")
                    PROGRAM.Add("PRINT_M")

                Case ">="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_GTE")
                    PROGRAM.Add("PRINT_M")

                Case "<="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_LTE")
                    PROGRAM.Add("PRINT_M")

                Case "="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_EQ")
                    PROGRAM.Add("PRINT_M")

            End Select
            Return PROGRAM
        End Function
        Private Function _CheckCondition(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As Boolean

            Select Case iOperator

                Case ">"
                    If Left > Right Then
                        Return True
                    Else
                        Return False
                    End If


                Case "<"
                    If Left < Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case ">="
                    If Left >= Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case "<="
                    If Left <= Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case "="
                    If Left = Right Then
                        Return True
                    Else
                        Return False
                    End If
                Case Else
                    Return False
            End Select

        End Function
        Private Function _print(ByRef Str As String) As List(Of String)

            Dim PROGRAM As New List(Of String)
            PROGRAM.Add("PUSH")
            PROGRAM.Add(Str.Replace("'", ""))
            PROGRAM.Add("PRINT_M")
            Return PROGRAM

        End Function
#Region "IF"
        ''' <summary>
        '''       If ["condition"] Then ["If-True"]  End
        ''' </summary>
        ''' <param name="_If">If ["condition"]</param>
        ''' <param name="_Then">Then ["If-True"]  End</param>
        Private Function _if_then(ByRef _If As List(Of String), ByRef _Then As List(Of String)) As List(Of String)
            Dim PROGRAM As New List(Of String)
            'ADD CONDITION
            PROGRAM.AddRange(_If)
            'JUMP TO END - IF FALSE
            PROGRAM.Add("JIF_F")
            PROGRAM.Add(_Then.Count)
            PROGRAM.AddRange(_Then)
            'END

            Return PROGRAM
        End Function
        ''' <summary>
        '''     If ["condition"] Then ["If-True"] ELSE ["If-False"] End
        ''' </summary>
        ''' <param name="_If">If ["condition"]</param>
        ''' <param name="_Then">Then ["If-True"]</param>
        ''' <param name="_Else">ELSE ["If-False"]</param>
        Private Function _if_then_else(ByRef _If As List(Of String), ByRef _Then As List(Of String), ByRef _Else As List(Of String)) As List(Of String)

            Dim PROGRAM As New List(Of String)
            'ADD CONDITION
            PROGRAM.AddRange(_If)
            'JUMP TO ELSE IF FALSE
            PROGRAM.Add("JIF_F")
            PROGRAM.Add(_If.Count + _Then.Count + 2)
            'THEN PART
            PROGRAM.AddRange(_Then)
            'JUMP TO END
            PROGRAM.Add("JMP")
            PROGRAM.Add(_If.Count + _Then.Count + 4 + _Else.Count)
            'ELSE PART
            PROGRAM.AddRange(_Else)
            'END
            Return PROGRAM
        End Function
#End Region
    End Module
End Namespace
'STACK-MEMORY-FRAME
'
Namespace SAL
    ''' <summary>
    ''' Memory frame for Variables 
    ''' </summary>
    Public Class StackMemoryFrame
        Public Structure Var
            Public Value As Integer
            Public VarNumber As String
        End Structure
        Public ReturnAddress As Integer
        Public Variables As List(Of Var)

        Public Sub New(ByRef ReturnAddress As Integer)
            ReturnAddress = ReturnAddress
            Variables = New List(Of Var)
        End Sub
        Public Function GetReturnAddress() As Integer

            Return ReturnAddress
        End Function
        Public Function GetVar(ByRef VarName As String) As Integer
            For Each item In Variables
                If item.VarNumber = VarName Then
                    Return item.Value

                End If
            Next
            Return 0
        End Function
        Public Sub SetVar(ByRef VarName As String, ByRef value As Object)
            Dim item As New Var
            item.VarNumber = VarName
            item.Value = value

            Variables.Add(item)
        End Sub
    End Class
End Namespace
'API
'
Namespace SAL
    Public Class X86API
        Public Shared Function RunMachineCode(ByRef Code As String) As String
            Code = UCase(Code)
            Dim PROG() As String = Split(Code.Replace(vbCrLf, " "), " ")
            Dim InstructionLst As New List(Of String)
            Dim ROOT As New TreeNode
            ROOT.Text = "ASSEMBLY_CODE"
            Dim Count As Integer = 0
            For Each item In PROG
                Count += 1
                If item <> "" Then
                    Dim NDE As New TreeNode
                    NDE.Text = Count & ": " & item
                    ROOT.Nodes.Add(NDE)
                    InstructionLst.Add(item)
                End If
            Next
            'cpu - START



            Dim CPU As ZX81_CPU = New ZX81_CPU("Test", InstructionLst)
            CPU.RUN()

            Tree = ROOT
            Return "CURRENT POINTER = " & CPU.Get_Instruction_Pointer_Position & vbCr & "CONTAINED DATA = " & CPU.Peek


        End Function
        Public Shared Tree As New TreeNode
    End Class
End Namespace
'CPU
'
Namespace SAL
    ''' <summary>
    ''' SpydazWeb X86 Assembly language Virtual X86 Processor
    ''' </summary>
    Public Class ZX81_CPU
        Public GPU As New ZX81_GPU

#Region "CPU"


        ''' <summary>
        ''' Used to monitor the Program status ; 
        ''' If the program is being executed then the cpu must be running
        ''' the Property value can only be changed within the program
        ''' </summary>
        Public ReadOnly Property RunningState As Boolean
            Get
                If mRunningState = State.RUN Then
                    Return True
                Else
                    Return False
                End If

                Return mRunningState
            End Get
        End Property
        Private mRunningState As State = State.HALT
        ''' <summary>
        ''' This is the cpu stack memory space; 
        ''' Items being interrogated will be placed in this memeory frame
        ''' calling functions will access this frame ; 
        ''' the cpu stack can be considered to be a bus; Functions are devices / 
        ''' or gate logic which is connected to the bus via the cpu; 
        ''' </summary>
        Private CPU_CACHE As New Stack
        ''' <summary>
        ''' Returns the Current position of the instruction Pointer 
        ''' in the Program being executed The instruction Pionet can be manipulated 
        ''' Jumping backwards and forwards in the program code.
        ''' </summary>
        ''' <returns></returns>
        Private ReadOnly Property GetInstructionAddress() As Integer
            Get
                Return InstructionAdrress
            End Get
        End Property
        Public ReadOnly Property Get_Instruction_Pointer_Position As Integer
            Get
                Return GetInstructionAddress
            End Get
        End Property
        ''' <summary>
        ''' Returns the current data in the stack
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Get_Current_Stack_Data As List(Of String)
            Get
                Get_Current_Stack_Data = New List(Of String)
                For Each item In CPU_CACHE
                    Get_Current_Stack_Data.Add(item.ToString)
                Next
            End Get
        End Property
        ''' <summary>
        ''' Returns the Current Cache (the stack)
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property View_C_P_U As Stack
            Get
                Return CPU_CACHE
            End Get
        End Property
        ''' <summary>
        ''' Returns the current object on top of the stack
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Get_Current_Stack_Item As Object
            Get
                Return Peek()
            End Get
        End Property
        ''' <summary>
        ''' Used to pass the intensive error messaging required; 
        ''' </summary>
        Private CPU_ERR As VM_ERR
        ''' <summary>
        ''' Used to hold the Program being sent to the CPU
        ''' A list of objects has been chosen to allow for a Richer CPU
        ''' enabling for objects to be passed instead of strings; 
        ''' due to this being a compiler as well as a morenized CPU
        ''' converting strings to string or integers or booleans etc 
        ''' makes it much harder to create quick easy code;
        ''' the sender is expeected to understand the logic of the items in the program
        ''' the decoder only decodes bassed on what is expected; 
        ''' </summary>
        Private ProgramData As New List(Of Object)
        ''' <summary>
        ''' the InstructionAdrress is the current position in the program; 
        ''' it could be considered to be the line numbe
        ''' </summary>
        Private InstructionAdrress As Integer
        ''' <summary>
        ''' Name of current program or process running in CPU thread
        ''' </summary>
        Public PROCESS_NAME As String = ""
        ''' <summary>
        ''' Used for local memory frame
        ''' </summary>
        Private CurrentCache As StackMemoryFrame
        ''' <summary>
        ''' Used to Store memory frames (The Heap)
        ''' </summary>
        Private R_A_M As New Stack
        ''' <summary>
        ''' Returns the Ram as a Stack of Stack Memeory frames;
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property View_R_A_M As Stack
            Get
                Return R_A_M
            End Get
        End Property
        Private WaitTime As Integer = 0


        ''' <summary>
        ''' Each Program can be considered to be a task or thread; 
        ''' A name should be assigned to the Process; 
        ''' Processes themselves can be stacked in a higher level processor,
        ''' allowing for paralel processing of code
        ''' This process allows for the initialization of the CPU; THe Prgram will still need to be loaded
        ''' </summary>
        ''' <param name="ThreadName"></param>
        Public Sub New(ByRef ThreadName As String)
            Me.PROCESS_NAME = ThreadName
            'Initializes a Stack for use (Memory for variables in code can be stored here)
            CurrentCache = New StackMemoryFrame(0)
        End Sub
        ''' <summary>
        ''' Load Program and Executes Code on CPU
        ''' </summary>
        ''' <param name="ThreadName">A name is required to Identify the Process</param>
        ''' <param name="Program"></param>
        Public Sub New(ByRef ThreadName As String, ByRef Program As List(Of String))
            Me.PROCESS_NAME = ThreadName
            'Initializes a Stack for use (Memory for variables in code can be stored here)
            CurrentCache = New StackMemoryFrame(0)
            LoadProgram(Program)
            mRunningState = State.RUN
            RUN()
        End Sub
        ''' <summary>
        '''  Loads items in to the program cache; 
        '''  this has been added to allow for continuious running of the VM
        '''  the run/wait Command will be added to the assembler 
        '''  enabling for the pausing of the program and restarting of the program stack
        ''' </summary>
        ''' <param name="Prog"></param>
        Public Sub LoadProgram(ByRef Prog As List(Of String))
            ProgramData.AddRange(Prog)

        End Sub
        ''' <summary>
        '''  Loads items in to the program cache; 
        '''  this has been added to allow for continuious running of the VM
        '''  the run/wait Command will be added to the assembler 
        '''  enabling for the pausing of the program and restarting of the program stack
        ''' </summary>
        ''' <param name="Prog"></param>
        Public Sub LoadProgram(ByRef Prog As List(Of Object))
            ProgramData.AddRange(Prog)
            'Initializes a Stack for use (Memory for variables in code can be stored here)
            CurrentCache = New StackMemoryFrame(0)
        End Sub
        ''' <summary>
        ''' Begins eexecution of the instructions held in program data
        ''' </summary>
        Public Sub RUN()

            Do While (IsHalted = False)
                If IsWait = True Then
                    For I = 0 To WaitTime
                    Next
                    EXECUTE()
                    mRunningState = State.RUN
                Else
                    EXECUTE()
                End If
            Loop
        End Sub
        ''' <summary>
        ''' Checks the status of the cpu
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsHalted As Boolean
            Get
                If mRunningState = State.HALT = True Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property IsWait As Boolean
            Get
                If RunningState = State.PAUSE Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        ''' <summary>
        ''' Executes the next instruction in the Program
        ''' Each Instruction is fed individually to the decoder : 
        ''' The Execute cycle Checks the Current State to determine 
        ''' if to fetch the next instruction to be decoded;(or EXECUTED) -
        ''' The decoder contains the Chip logic
        ''' </summary>
        Public Sub EXECUTE()
            'The HALT command is used to STOP THE CPU
            If IsHalted = False Then
                DECODE(Fetch)
            Else
                '  CPU_ERR = New VM_ERR("CPU HALTED", Me)
                '    CPU_ERR.RaiseErr()
                'ERR - state stopped
            End If

        End Sub
        ''' <summary>
        ''' Program Instructions can be boolean/String or integer 
        ''' so an object is assumed enabling for later classification
        ''' of the instructions at the decoder level : 
        ''' The Fetch Cycle Fetches the next Instruction in the Program to be executed:
        ''' It is fed to the decoder to be decoded and executed
        ''' </summary>
        ''' <returns></returns>
        Private Function Fetch() As Object
            'Check that it is not the end of the program
            If InstructionAdrress >= ProgramData.Count Then
                CPU_ERR = New VM_ERR("End of instruction list reached! No more instructions in Program data Error Invalid Address -FETCH(Missing HALT command)", Me)
                CPU_ERR.RaiseErr()
                'End of instruction list reached no more instructions in program data
                'HALT CPU!!
                Me.mRunningState = State.HALT
                '
            Else
                'Each Instruction is considered to be a string 
                'or even a integer or boolean the string is the most universal
                Dim CurrentInstruction As Object = ProgramData(InstructionAdrress)
                'Move to next instruction
                InstructionAdrress += 1
                Return CurrentInstruction
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Contains MainInstruction Set: Decode Cycle Decodes program instructions from the program 
        ''' the Insruction pointer points to the current Instruction being feed into the decoder: 
        ''' Important Note : the stack will always point to the data at top of the CPU CACHE (Which is Working Memory);
        '''                 THe memory frames being used are Extensions of this memeory and can be seen as registers, 
        '''                 itself being a memory stack (stack of memory frames)
        ''' </summary>
        ''' <param name="ProgramInstruction"></param>
        Public Sub DECODE(ByRef ProgramInstruction As Object)

            Select Case ProgramInstruction

#Region "Basic Assembly"
                Case "WAIT"
                    WaitTime = Integer.Parse(Fetch().ToString)
                    mRunningState = State.PAUSE
                Case "HALT"
                    'Stop the CPU
                    Me.mRunningState = State.HALT
                Case "PAUSE"
                    WaitTime = Integer.Parse(Fetch().ToString)
                    mRunningState = State.PAUSE
                Case "RESUME"
                    If mRunningState = State.PAUSE Then
                        mRunningState = State.RUN
                        RUN()
                    End If
                ' A Wait time Maybe Neccasary
                Case "DUP"
                    Try
                        If CPU_CACHE.Count >= 1 Then
                            'Get Current item on the stack
                            Dim n As String = Peek()
                            'Push another copy onto the stack
                            Push(n)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("STACK ERROR : Stack Not intialized - DUP", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DUP", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "POP"
                    CheckStackHasAtLeastOneItem()
                    Pop()
                Case "PUSH"
                    'Push
                    Push(Fetch)
                Case "JMP"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JMP instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JUMP(address)
                Case "JIF_T"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JumpIf_TRUE(address)
                Case "JIF_F"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JumpIf_False(address)
                Case "LOAD"
                    CheckStackHasAtLeastOneItem()
                    'lOADS A VARIABLE
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    CPU_CACHE.Push(GetCurrentFrame.GetVar(varNumber))
                Case "REMOVE"
                    'lOADS A VARIABLE
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    GetCurrentFrame.RemoveVar(varNumber)
                Case "STORE"
                    ' "Should have the variable number after the STORE instruction"
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    CheckStackHasAtLeastOneItem()
                    CurrentCache.SetVar(varNumber, CPU_CACHE.Peek())
                    R_A_M.Push(CurrentCache)
                Case "CALL"
                    ' The word after the instruction will contain the function address
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    CheckJumpAddress(address)
                    R_A_M.Push(New StackMemoryFrame(InstructionAdrress))  '// Push a New stack frame on to the memory heap
                    InstructionAdrress = address '                   // And jump!
                Case "RET"
                    ' Pop the stack frame And return to the previous address from the memory heap
                    CheckThereIsAReturnAddress()
                    Dim returnAddress = GetCurrentFrame().GetReturnAddress()
                    InstructionAdrress = returnAddress
                    R_A_M.Pop()
#End Region
#Region "PRINT"
                ' PRINT TO MONITOR
                Case "PRINT_M"
                    GPU.ConsolePrint(Pop)

                Case "CLS"
                    GPU.Console_CLS()
                ' PRINT TO CONSOLE
                Case "PRINT_C"
                    '  System.Console.WriteLine("------------ ZX 81 ----------" & vbNewLine & Peek())
                    System.Console.ReadKey()
#End Region
#Region "Operations"
                Case "ADD"
                    'ADD
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "SUB"
                    'SUB
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "MUL"
                    'MUL
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "DIV"
                    'DIV
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DIV", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DIV", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "NOT"
                    CheckStackHasAtLeastOneItem()
                    Push(ToInt(NOT_ToBool(Pop())))
                Case "AND"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "OR"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_EQ"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ISEQ", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GTE"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GT"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LT"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LTE"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try
#End Region
#Region "POSITIVE VS NEGATIVE"
                Case "TO_POS"
                    If CPU_CACHE.Count >= 1 Then
                        Push(ToPositive(Integer.Parse(Pop)))
                    Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - POS", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Case "TO_NEG"
                    If CPU_CACHE.Count >= 1 Then
                        Push(ToNegative(Integer.Parse(Pop)))
                    Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - NEG", Me)
                        CPU_ERR.RaiseErr()
                    End If
#End Region
#Region "Extended JmpCmds"
                Case "JIF_GT"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_GT", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_GT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_GT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "JIF_LT"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_LT", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_LT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_LT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "JIF_EQ"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_EQ", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_EQ", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_EQ", Me)
                        CPU_ERR.RaiseErr()
                    End Try
#End Region
#Region "INCREMENT/DECREMENT"
                Case "INCR"
                    CheckStackHasAtLeastOneItem()
                    Push(Integer.Parse(Pop) + 1)
                Case "DECR"
                    CheckStackHasAtLeastOneItem()
                    Push(Integer.Parse(Pop) - 1)
#End Region

                Case Else
                    Me.mRunningState = State.HALT
                    ' CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction", Me)
                    ' CPU_ERR.RaiseErr()

            End Select

        End Sub
#End Region
#Region "functions required by cpu and assembly language"
#Region "Handle Boolean"
        Private Function ToInt(ByRef Bool As Boolean) As String
            If Bool = False Then
                Return 0
            Else
                Return 1
            End If
        End Function
        Private Function ToBool(ByRef Val As Integer) As Boolean
            If Val = 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function NOT_ToBool(ByRef Val As Integer) As Integer
            If Val = 1 Then
                Return 0
            Else
                Return 1
            End If
        End Function
#End Region
#Region "Functional Parts"
        ''' <summary>
        ''' Checks if there is a jump address available
        ''' </summary>
        ''' <param name="address"></param>
        ''' <returns></returns>
        Private Function CheckJumpAddress(ByRef address As Integer) As Boolean
            Try
                'Check if it is in range
                If address < 0 Or address >= ProgramData.Count Then
                    'Not in range
                    Me.mRunningState = State.HALT
                    CPU_ERR = New VM_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Me.mRunningState = State.HALT
                CPU_ERR = New VM_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Function used by the internal functions to check if there is a return address
        ''' </summary>
        ''' <returns></returns>
        Private Function CheckThereIsAReturnAddress() As Boolean
            Try
                If (R_A_M.Count >= 1) Then
                    Return True
                Else
                    Me.mRunningState = State.HALT
                    CPU_ERR = New VM_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False
                End If
            Catch ex As Exception
                Me.mRunningState = State.HALT
                CPU_ERR = New VM_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            End Try
        End Function
        ''' <summary>
        ''' RAM is a STACK MEMORY - Here we can take a look at the stack item
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCurrentFrame() As StackMemoryFrame
            If R_A_M.Count > 0 Then
                Return R_A_M.Pop()
            Else
                Return Nothing
                Me.mRunningState = State.HALT
                CPU_ERR = New VM_ERR("Error Decoding STACK MEMORY FRAME - GetCurrentFrame", Me)
                CPU_ERR.RaiseErr()
            End If
        End Function
        ''' <summary>
        ''' Outputs stack data for verbose output
        ''' </summary>
        ''' <returns></returns>
        Public Function GetStackData() As String
            Dim Str As String = ""
            Str = ToJson(Me)
            Return Str
        End Function
        Private Function ToJson(ByRef OBJ As Object) As String
            Dim Converter As New JavaScriptSerializer
            Return Converter.Serialize(OBJ)

        End Function
#End Region
#Region "Operational Functions"
        ''' <summary>
        ''' REQUIRED TO SEE IN-SIDE CURRENT POINTER LOCATION
        ''' ----------Public For Testing Purposes-----------
        ''' </summary>
        ''' <returns></returns>
        Public Function Peek() As String
            Try
                If CPU_CACHE.Count > 0 Then
                    Return CPU_CACHE.Peek().ToString
                Else
                    mRunningState = State.HALT
                    Return "NULL"
                End If
            Catch ex As Exception
                mRunningState = State.HALT
                CPU_ERR = New VM_ERR("NULL POINTER CPU HALTED", Me)
                CPU_ERR.RaiseErr()

                Return "NULL"
            End Try
        End Function
        Private Function BINARYOP(ByRef INSTRUCTION As String, LEFT As Integer, RIGHT As Integer) As String
            If INSTRUCTION IsNot Nothing Then


                Select Case INSTRUCTION
                    Case "IS_EQ"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) = ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation - isEQ", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_GT"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) < ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation - isGT", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_GTE"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) <= ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isGTE", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_LT"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) > ToBool(RIGHT))))

                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isLT", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_LE"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) >= ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isLTE", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "ADD"
                        Try
                            Return LEFT + RIGHT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -add", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "SUB"
                        Try
                            Return RIGHT - LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -sub", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "MUL"
                        Try
                            Return RIGHT * LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -mul", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "DIV"
                        Try
                            Return RIGHT / LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -div", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "OR"
                        Try
                            Return ToInt((ToBool(LEFT) Or ToBool(RIGHT)))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -or", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "AND"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) And ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -and", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "NOT"
                        CheckStackHasAtLeastOneItem()
                        Push(ToInt(NOT_ToBool(Pop())))
                    Case Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Invalid Operation -not", Me)
                        CPU_ERR.RaiseErr()
                End Select


            Else
                Me.mRunningState = State.HALT
            End If

            Return "NULL"
        End Function
        Private Function CheckStackHasAtLeastOneItem(ByRef Current As Stack) As Boolean
            If Current.Count >= 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function CheckStackHasAtLeastOneItem() As Boolean
            If CPU_CACHE.Count >= 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function CheckRamHasAtLeastOneItem() As Boolean
            If CPU_CACHE.Count >= 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Sub JumpIf_TRUE(ByRef Address As Integer)
            If CheckJumpAddress(Address) = True And CheckStackHasAtLeastOneItem() = True Then
                If (ToBool(Pop)) Then
                    InstructionAdrress = Address
                Else
                End If
            Else
            End If
        End Sub
        Private Sub JUMP(ByRef Address As Integer)
            If CheckJumpAddress(Address) = True Then
                InstructionAdrress = Address
            End If

        End Sub
        Private Sub JumpIf_False(ByRef Address As Integer)
            If CheckJumpAddress(Address) = True And CheckStackHasAtLeastOneItem() = True Then
                If (NOT_ToBool(Pop)) Then
                    InstructionAdrress = Address
                Else
                End If
            Else
            End If
        End Sub
        ''' <summary>
        ''' Puts a value on the cpu stack to be available to funcitons
        ''' </summary>
        ''' <param name="Value"></param>
        Private Sub Push(ByRef Value As Object)
            Try
                CPU_CACHE.Push(Value)
            Catch ex As Exception
                CPU_ERR = New VM_ERR("STACK ERROR - CPU HALTED -push", Me)
                CPU_ERR.RaiseErr()
                mRunningState = State.HALT
            End Try
        End Sub
        ''' <summary>
        ''' Pops a value of the cpu_Stack (current workspace)
        ''' </summary>
        ''' <returns></returns>
        Private Function Pop() As Object
            Try
                If CPU_CACHE.Count >= 1 Then
                    Return CPU_CACHE.Pop()
                Else
                    CPU_ERR = New VM_ERR("STACK ERROR - NULL POINTER CPU HALTED -pop", Me)
                    CPU_ERR.RaiseErr()
                    mRunningState = State.HALT
                    Return "NULL"
                End If
            Catch ex As Exception
                CPU_ERR = New VM_ERR("STACK ERROR - NULL POINTER CPU HALTED -pop", Me)
                CPU_ERR.RaiseErr()
                mRunningState = State.HALT
                Return "NULL"
            End Try
            Return "NULL"
        End Function
#End Region
        Private Function ToPositive(Number As Integer)
            Return Math.Abs(Number)
        End Function
        Private Function ToNegative(Number As Integer)
            Return Math.Abs(Number) * -1
        End Function
#End Region
#Region "CPU _ INTERNAL _ Components"
        Private Enum State
            RUN
            HALT
            PAUSE
        End Enum
        ''' <summary>
        ''' Memory frame for Variables
        ''' </summary>
        Public Class StackMemoryFrame
            Public Structure Var
                Public Value As String
                Public VarNumber As String
            End Structure
            Public ReturnAddress As Integer
            Public Variables As List(Of Var)
            Public Sub New(ByRef ReturnAddress As Integer)
                ReturnAddress = ReturnAddress
                Variables = New List(Of Var)
            End Sub
            Public Function GetReturnAddress() As Integer
                Return ReturnAddress
            End Function
            Public Function GetVar(ByRef VarNumber As String) As String
                For Each item In Variables
                    If item.VarNumber = VarNumber Then
                        Return item.Value
                    End If
                Next
                Return 0
            End Function
            Public Sub SetVar(ByRef VarNumber As String, ByRef value As String)
                Dim item As New Var
                Dim added As Boolean = False
                item.VarNumber = VarNumber
                item.Value = value
                For Each ITM In Variables
                    If ITM.VarNumber = VarNumber Then
                        ITM.Value = value
                    End If
                Next
                Variables.Add(item)

            End Sub
            Public Sub RemoveVar(ByRef VarNumber As String)
                For Each item In Variables
                    If item.VarNumber = VarNumber = True Then
                        Variables.Remove(item)
                        Exit For
                    Else
                    End If
                Next
            End Sub
        End Class
        Public Class VM_ERR
            Private ErrorStr As String = ""
            Private frm As New FormDisplayConsole
            Private CpuCurrentState As ZX81_CPU

            Public Sub New(ByRef Err As String, ByVal CPUSTATE As ZX81_CPU)
                ErrorStr = Err
                CpuCurrentState = CPUSTATE
            End Sub
            Public Sub RaiseErr()
                If frm Is Nothing Then
                    frm = New FormDisplayConsole
                    frm.Show()
                    frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
                Else
                    frm.Show()
                    frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
                End If

            End Sub




        End Class
#End Region
        ''' <summary>
        ''' COMMANDS FOR ASSEMBLY LANGUAGE FOR THIS CPU
        ''' SPYDAZWEB_VM_X86
        ''' </summary>
        Public Enum VM_x86_Cmds
            _NULL
            _REMOVE
            _RESUME
            _PUSH
            _PULL
            _PEEK
            _WAIT
            _PAUSE
            _HALT
            _DUP
            _JMP
            _JIF_T
            _JIF_F
            _JIF_EQ
            _JIF_GT
            _JIF_LT
            _LOAD
            _STORE
            _CALL
            _RET
            _PRINT_M
            _PRINT_C
            _ADD
            _SUB
            _MUL
            _DIV
            _AND
            _OR
            _NOT
            _IS_EQ
            _IS_GT
            _IS_GTE
            _IS_LT
            _IS_LTE
            _TO_POS
            _TO_NEG
            _INCR
            _DECR
        End Enum
    End Class


End Namespace
'GPU
'
Namespace SAL
    Public Class ZX81_GPU
        Private iMonitorConsole As FormDisplayConsole

        Public Sub New()
            iMonitorConsole = New FormDisplayConsole
        End Sub

        Public Sub ConsolePrint(ByRef Str As String)
            If iMonitorConsole.Visible = False Then
                iMonitorConsole.Show()
            Else
            End If
            iMonitorConsole.Print(Str)
        End Sub
        Public Sub Console_CLS()
            If iMonitorConsole.Visible = False Then
                iMonitorConsole.Show()
            Else
            End If
            iMonitorConsole.CLS()
        End Sub
    End Class
End Namespace
'RAM
'
Namespace SAL
    Public Class ZX81_RAM
        Public Structure Variable
            Public iName As String
            Public iValue As String
            Public iType As String
        End Structure
        ''' <summary>
        ''' Currently only Variables can be stored
        ''' </summary>
        Public CurrentVars As List(Of Variable)
        Public Sub New()
            CurrentVars = New List(Of Variable)
        End Sub

        'Variables

        Public Sub UpdateVar(ByRef VarName As String, ByRef iVALUE As String)
            For Each item In CurrentVars
                If item.iName = VarName Then
                    Dim NiTEM As Variable = item
                    NiTEM.iValue = iVALUE
                    CurrentVars.Remove(item)
                    CurrentVars.Add(NiTEM)
                    Exit For
                Else
                End If
            Next
        End Sub
        Public Function RemoveVar(ByRef Var As Variable)
            For Each item In CurrentVars
                If item.iName = Var.iName Then
                    CurrentVars.Remove(item)
                End If
            Next
            Return Var
        End Function
        Public Sub AddVar(ByRef Var As Variable)
            If CheckVar(Var.iName) = False Then
                CurrentVars.Add(Var)
            End If
        End Sub
        Public Function CheckVar(ByRef VarName As String) As Boolean
            For Each item In CurrentVars
                If item.iName = VarName Then
                    Return True
                End If
            Next
            CheckVar = False
        End Function
        Public Function GetVar(ByRef VarName As String) As String
            For Each item In CurrentVars
                If item.iName = VarName = True Then
                    If item.iType = "BOOLEAN" Then
                        Select Case item.iValue
                            Case 0
                                Return "False"
                            Case 1
                                Return "True"
                            Case Else
                                Return item.iValue
                        End Select
                    Else
                        Return item.iValue
                    End If

                Else
                End If
            Next
            Return VarName
        End Function
    End Class
End Namespace
#End Region

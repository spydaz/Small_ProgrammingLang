Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace Syntax
    Namespace Bindings
        Namespace BoundNodes
            Public Interface Bound_Expression
                Property _ExpressionType As ExpressionType
            End Interface
            Public Class Bound_LiteralExpression
                Inherits LiteralExpression
                Implements Bound_Expression

                Public LiteralType As LiteralType
                Private iExpressionType As ExpressionType
                Public Property _ExpressionType As ExpressionType Implements Bound_Expression._ExpressionType
                    Get
                        Return iExpressionType
                    End Get
                    Set(value As ExpressionType)
                        iExpressionType = value
                    End Set
                End Property

                Public Sub New(ExpressionSyntax As LiteralExpression)
                    MyBase.New(ExpressionSyntax._SyntaxType, ExpressionSyntax._SyntaxType.GetSyntaxTypeStr, ExpressionSyntax._Literal)

                    Select Case ExpressionSyntax._Literal.SyntaxType
                        Case SyntaxType.TrueKeyword
                            Me._Literal = New Boolean
                            Me._Literal = True
                            Me._SyntaxType = SyntaxType._Boolean
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._Boolean
                        Case SyntaxType.FalseKeyword
                            Me._Literal = New Boolean
                            Me._Literal = False
                            Me._SyntaxType = SyntaxType._Boolean
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._Boolean
                        Case SyntaxType._Integer
                            Me._Literal = New Integer
                            Me._Literal = ExpressionSyntax._Literal._value
                            Me._SyntaxType = SyntaxType._NumericLiteralExpression
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._Integer
                        Case SyntaxType._Decimal
                            Me._Literal = New Double
                            Me._Literal = ExpressionSyntax._Literal._value
                            Me._SyntaxType = SyntaxType._NumericLiteralExpression
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._Decimal
                        Case SyntaxType._StringExpression
                            Me._Literal = ""
                            Me._Literal = ExpressionSyntax._Literal._value
                            Me._SyntaxType = SyntaxType._StringExpression
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._String
                        Case SyntaxType._IdentifierExpression
                            Me._Literal = ExpressionSyntax._Literal._value
                            Me._SyntaxType = SyntaxType._IdentifierExpression
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._String
                        Case SyntaxType._null
                            Me._Literal = ExpressionSyntax._Literal._value
                            Me._SyntaxType = SyntaxType._null
                            Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
                            Me.LiteralType = LiteralType._NULL
                        Case Else
                            Throw New ConstraintException("Wrong SyntaxType : Unmanaged _Literal: " & ExpressionSyntax._Literal._value.ToJson)
                    End Select
                    iExpressionType = ExpressionType.Literal
                End Sub



                Public Overrides Function GetChildren() As List(Of SyntaxToken)
                    Dim Lst As New List(Of SyntaxToken)
                    Lst.Add(_Literal)
                    Return Lst
                End Function
                Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                    Return _Literal
                End Function


#Region "Get Identifer Value"

                Public Function GetLiteralType(ByRef ParentEnv As EnvironmentalMemory) As LiteralType
                    Select Case _SyntaxType
                        Case SyntaxType._IdentifierExpression
                            Dim iName = _Literal.evaluate(ParentEnv)
                            Return ParentEnv.GetVarType(iName)
                        Case SyntaxType._VariableDeclaration
                            Dim iName = _Literal.evaluate(ParentEnv)
                            Return ParentEnv.GetVarType(iName)
                        Case Else
                            Return LiteralType
                    End Select
                End Function

#End Region

            End Class
            Public Class Bound_IdentifierExpression
                Inherits Bound_LiteralExpression
                Implements Bound_Expression
                Public Sub New(ExpressionSyntax As LiteralExpression)
                    MyBase.New(ExpressionSyntax)
                    Me._ExpressionType = ExpressionType.Variable
                End Sub
                Private Function CheckVar(ByRef ParentEnv As EnvironmentalMemory) As Boolean
                    Return ParentEnv.CheckVar(_Literal)
                End Function
                Private Function SetValue(ByRef ParentEnv As EnvironmentalMemory, ByRef Value As Bound_LiteralExpression) As Boolean
                    If ParentEnv.CheckVar(_Literal) = True Then

                        Select Case Value.GetLiteralType(ParentEnv)
                            Case LiteralType._Boolean
                                If ParentEnv.GetVarType(_Literal) = LiteralType._Boolean Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If

                            Case LiteralType._String
                                If ParentEnv.GetVarType(_Literal) = LiteralType._String Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If

                            Case LiteralType._Array
                                If ParentEnv.GetVarType(_Literal) = LiteralType._Array Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If

                            Case LiteralType._Integer
                                If ParentEnv.GetVarType(_Literal) = LiteralType._Integer Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If

                            Case LiteralType._Decimal
                                If ParentEnv.GetVarType(_Literal) = LiteralType._Decimal Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If
                            Case LiteralType._Date
                                If ParentEnv.GetVarType(_Literal) = LiteralType._Date Then
                                    ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))
                                    Return True
                                End If
                            Case LiteralType._NULL

                                ParentEnv.AssignValue(_Literal, Value.Evaluate(ParentEnv))


                        End Select


                    Else

                    End If
                    Return False
                End Function
                Private Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                    If ParentEnv.CheckVar(_Literal) = True Then
                        Return ParentEnv.GetVar(_Literal)
                    Else
                        Return "Does not Exist in Record"
                    End If
                End Function
                Public Sub SetVar(ByRef ParentEnv As EnvironmentalMemory)
                    Dim iName = _Literal.evaluate(ParentEnv)

                    ParentEnv.Define(iName, GetLiteralType(ParentEnv))
                End Sub
                Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object

                    Return GetValue(ParentEnv)
                End Function
            End Class
            Public Class Bound_BinaryExpression
                Inherits SyntaxNodes.BinaryExpression
                Implements Bound_Expression

                Public _OperatorType As OperatorType
                Public _BinaryExpresionType As BinaryOperation
                Public _Bound_Left As Bound_LiteralExpression
                Public _Bound_Right As Bound_LiteralExpression
                Private iExpressionType As ExpressionType
                Public Property _ExpressionType As ExpressionType Implements Bound_Expression._ExpressionType
                    Get
                        Return iExpressionType
                    End Get
                    Set(value As ExpressionType)
                        iExpressionType = value
                    End Set
                End Property
                Public Sub New(ExpressionSyntax As SyntaxNodes.BinaryExpression)
                    MyBase.New(ExpressionSyntax._Left, ExpressionSyntax._Right, ExpressionSyntax._Operator)
                    'Operator Types
                    Select Case _Operator._SyntaxType
                        Case SyntaxType.Add_Equals_Operator
                            _OperatorType = OperatorType.AddEquals
                            _BinaryExpresionType = BinaryOperation._Assignment
                        Case SyntaxType.Add_Operator
                            _OperatorType = OperatorType.Add
                            _BinaryExpresionType = BinaryOperation._Addative
                        Case SyntaxType.Sub_Operator
                            _OperatorType = OperatorType.Minus
                            _BinaryExpresionType = BinaryOperation._Addative
                        Case SyntaxType.Minus_Equals_Operator
                            _OperatorType = OperatorType.MinusEquals
                            _BinaryExpresionType = BinaryOperation._Assignment
                        Case SyntaxType.Divide_Equals_Operator
                            _OperatorType = OperatorType.DivideEquals
                            _BinaryExpresionType = BinaryOperation._Assignment
                        Case SyntaxType.Divide_Operator
                            _OperatorType = OperatorType.Divide
                            _BinaryExpresionType = BinaryOperation._Multiplcative
                        Case SyntaxType.Multiply_Equals_Operator
                            _OperatorType = OperatorType.MultiplyEquals
                            _BinaryExpresionType = BinaryOperation._Assignment
                        Case SyntaxType.Multiply_Operator
                            _OperatorType = OperatorType.Multiply
                            _BinaryExpresionType = BinaryOperation._Multiplcative
                        Case SyntaxType.GreaterThanEquals
                            _OperatorType = OperatorType.GreaterThanEquals
                            _BinaryExpresionType = BinaryOperation._Comparison
                        Case SyntaxType.GreaterThan_Operator
                            _OperatorType = OperatorType.GreaterThan
                            _BinaryExpresionType = BinaryOperation._Comparison
                        Case SyntaxType.LessThanEquals
                            _OperatorType = OperatorType.LessThanEquals
                            _BinaryExpresionType = BinaryOperation._Comparison
                        Case SyntaxType.LessThanOperator
                            _OperatorType = OperatorType.LessThan
                            _BinaryExpresionType = BinaryOperation._Comparison
                        Case SyntaxType.NotEqual
                            _OperatorType = OperatorType.NotEquals
                            _BinaryExpresionType = BinaryOperation._Comparison
                        Case SyntaxType.EquivelentTo
                            _OperatorType = OperatorType.Equivilent
                            _BinaryExpresionType = BinaryOperation._Comparison

                    End Select
                End Sub



                Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                    Return MyBase.Evaluate(ParentEnv)


                    Select Case _BinaryExpresionType
                        Case BinaryOperation._Comparison
                            Return PerformComparison(ParentEnv)
                        Case BinaryOperation._Multiplcative
                            Return PerformCaluation(ParentEnv)
                        Case BinaryOperation._Addative
                            Return PerformCaluation(ParentEnv)
                        Case BinaryOperation._Assignment
                            Return PerformAssignment(ParentEnv)
                    End Select
                End Function

                Public Function PerformCaluation(ByRef ParentEnv As EnvironmentalMemory) As Object
                    Dim Ltype As LiteralType

                    Select Case Ltype
                        Case LiteralType._Decimal
                            Return PerformCalulationDoubleResult(ParentEnv)
                        Case LiteralType._Integer
                            Return PerformCalulationIntegerResult(ParentEnv)
                        Case LiteralType._NULL
                            Return 0


                    End Select

                End Function
                Public Function PerformCalulationIntegerResult(ByRef ParentEnv As EnvironmentalMemory) As Integer
                    Return Nothing
                End Function
                Public Function PerformCalulationDoubleResult(ByRef ParentEnv As EnvironmentalMemory) As Double
                    Return Nothing
                End Function
                Public Function PerformComparison(ByRef ParentEnv As EnvironmentalMemory) As Double
                    Return Nothing
                End Function
                Public Function PerformAssignment(ByRef ParentEnv As EnvironmentalMemory) As Boolean
                    Return Nothing
                End Function
            End Class

        End Namespace
    End Namespace
End Namespace
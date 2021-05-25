
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.Syntax

Namespace Syntax
    Namespace SyntaxNodes
#Region "Abstract Models"

        'Abstract Models
        ''' <summary>
        ''' All nodes must use this model to Create SyntaxNodes
        ''' Defines the Syntax For the Language
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public MustInherit Class SyntaxNode
            ''' <summary>
            ''' Text String of Type(for diagnostics)
            ''' </summary>
            Public _SyntaxTypeStr As String

            ''' <summary>
            ''' Enum Strong Type
            ''' </summary>
            Public _SyntaxType As SyntaxType
            ''' <summary>
            ''' Initializes the Type for the Syntax Node to identify the node
            ''' </summary>
            ''' <param name="syntaxType">SyntaxType</param>
            ''' <param name="syntaxTypeStr">String version of the Type</param>
            Protected Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                If syntaxTypeStr Is Nothing Then
                    Throw New ArgumentNullException(NameOf(syntaxTypeStr))
                End If

                _SyntaxType = syntaxType
                _SyntaxTypeStr = syntaxTypeStr
            End Sub
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ; 
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions,
            '''     which the expression will have access to to evalute correctly.
            ''' (2) To get the values use Get Children , 
            '''        Evaluating the Correct values returned 
            ''' </summary>
            ''' <returns></returns>
            Public MustOverride Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object


#Region "TOSTRING"
            ''' <summary>
            ''' Serializes object to json
            ''' </summary>
            ''' <returns> </returns>
            Public Function ToJson() As String
                Return FormatJsonOutput(ToString)
            End Function
            Public Overrides Function ToString() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)
            End Function
            Private Function FormatJsonOutput(ByVal jsonString As String) As String
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

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
#End Region
        End Class
        ''' <summary>
        ''' Used for Strong Typing All Expressions must inherit this class
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend MustInherit Class ExpressionSyntaxNode
            Inherits SyntaxNode
            Protected Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(syntaxType, syntaxTypeStr)
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

#Region "Expressions"
        'Concrete
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class BinaryExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>
            ''' Value / Identifier / Expression
            ''' </summary>
            Public _Left As ExpressionSyntaxNode
            ''' <summary>
            ''' Value / Identifier / Expression 
            ''' </summary>
            Public _Right As ExpressionSyntaxNode
            ''' <summary>
            ''' Operator
            ''' </summary>
            Public _Operator As SyntaxToken
            ''' <summary>
            ''' Used to perform Calulations,
            ''' Comparison Operations as well as logical operations and assignments.
            ''' 
            ''' Syntax:
            ''' LeftValue Operator RightValue
            ''' </summary>
            ''' <param name="ileft">Left Value or Identifier</param>
            ''' <param name="iright">Right Value or Identifier</param>
            ''' <param name="iOperator">Operator Token</param>
            Public Sub New(ileft As ExpressionSyntaxNode, iright As ExpressionSyntaxNode, ioperator As SyntaxToken)
                MyBase.New(SyntaxType._BinaryExpression, SyntaxType._BinaryExpression.GetSyntaxTypeStr)

                If ileft Is Nothing Then
                    '    MsgBox(ileft.ToString)
                    Throw New ArgumentNullException(NameOf(ileft))
                End If

                If iright Is Nothing Then
                    Throw New ArgumentNullException(NameOf(iright))
                End If

                _Left = ileft
                _Right = iright
                _Operator = ioperator
            End Sub
            ''' <summary>
            ''' Returns a evaluated result for this expression,
            ''' Based on the input from the parent environment,
            ''' and the information held in the expression.
            ''' </summary>
            ''' <returns></returns>
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case _Operator._SyntaxType
        'Calc
                    Case SyntaxType.Add_Operator
                        Dim NewVal As Integer = 0
                        NewVal = _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)
                        Return NewVal

                    Case SyntaxType.Sub_Operator
                        Dim NewVal As Integer = 0
                        NewVal = _Left.Evaluate(ParentEnv) - _Right.Evaluate(ParentEnv)
                        Return NewVal

                    Case SyntaxType.Divide_Operator
                        Dim NewVal As Integer = 0
                        NewVal = _Left.Evaluate(ParentEnv) / _Right.Evaluate(ParentEnv)
                        Return NewVal

                    Case SyntaxType.Multiply_Operator
                        Dim NewVal As Integer = 0
                        NewVal = _Left.Evaluate(ParentEnv) * _Right.Evaluate(ParentEnv)
                        Return NewVal


                        'Compare ( Cast to boolean )
                    Case SyntaxType.GreaterThan_Operator
                        Dim NewComp As Boolean = False
                        NewComp = _Left.Evaluate(ParentEnv) > _Right.Evaluate(ParentEnv)
                        Return NewComp
                    Case SyntaxType.LessThanOperator
                        Dim NewComp As Boolean = False
                        NewComp = _Left.Evaluate(ParentEnv) > _Right.Evaluate(ParentEnv)
                        Return NewComp
                    Case SyntaxType.GreaterThanEquals
                        Dim NewComp As Boolean = False
                        NewComp = _Left.Evaluate(ParentEnv) >= _Right.Evaluate(ParentEnv)
                        Return NewComp
                    Case SyntaxType.LessThanEquals
                        Dim NewComp As Boolean = False
                        NewComp = _Left.Evaluate(ParentEnv) <= _Right.Evaluate(ParentEnv)
                        Return NewComp

                        'Complex Assign
                    Case SyntaxType.Add_Equals_Operator

                        Dim _L = _Left.Evaluate(ParentEnv)
                        Dim _R = _Right.Evaluate(ParentEnv)
                        Dim _A = _L + _R
                        Return _A
                    Case SyntaxType.Minus_Equals_Operator
                        Dim _L = _Left.Evaluate(ParentEnv)
                        Dim _R = _Right.Evaluate(ParentEnv)
                        Dim _A = _L - _R
                        Return _A

                    Case SyntaxType.Multiply_Equals_Operator
                        Dim _L = _Left.Evaluate(ParentEnv)
                        Dim _R = _Right.Evaluate(ParentEnv)
                        Dim _A = _L * _R
                        Return _A
                    Case SyntaxType.Divide_Equals_Operator
                        Dim _L = _Left.Evaluate(ParentEnv)
                        Dim _R = _Right.Evaluate(ParentEnv)
                        Dim _A = _L / _R
                        Return _A
                End Select
                Return 0
            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class UnaryExpression
            Inherits ExpressionSyntaxNode
            Public NumericLiteral As NumericalExpression
            Public OperatorToken As SyntaxToken
            Public Sub New(ByRef _OperatorToken As SyntaxToken, ByRef _NumericLiteral As NumericalExpression)
                MyBase.New(SyntaxType._UnaryExpression, SyntaxType._UnaryExpression.GetSyntaxTypeStr)
                OperatorToken = _OperatorToken
                NumericLiteral = _NumericLiteral
            End Sub

            ''' <summary>
            ''' Returns a value
            ''' </summary>
            ''' <param name="ParentEnv"></param>
            ''' <returns></returns>
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case OperatorToken._SyntaxType
                    Case SyntaxType.Sub_Operator
                        Return -NumericLiteral.Evaluate(ParentEnv)
                    Case SyntaxType.Add_Operator
                        Return NumericLiteral.Evaluate(ParentEnv)
                End Select
                Return Nothing
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   if expression. </summary>
        ''' Condition: This position must contain a condition – a comparison between two values – where one or both values can be cell references. The possible conditions are:
        ''' Equal (==)
        ''' Unequal 
        ''' Less than
        ''' Greater than 
        ''' Less than or equal to 
        ''' Greater than or equal to 
        '''
        ''' <remarks>   Leroy, 24/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IfThenExpression
            Inherits ExpressionSyntaxNode
            Public ThenCondition As CodeBlockExpression
            Public IfCondition As BinaryExpression
            Public Sub New(ifCondition As BinaryExpression,
                           thenCondition As CodeBlockExpression)
                MyBase.New(SyntaxType.IfExpression, SyntaxType.IfExpression.GetSyntaxTypeStr)


                Me.IfCondition = ifCondition
                Me.ThenCondition = thenCondition

            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                If IfCondition.Evaluate(ParentEnv) = True Then
                    Return ThenCondition.Evaluate(ParentEnv)
                Else
                    Return False
                End If


            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IfElseExpression
            Inherits IfThenExpression
            Public ElseCondition As CodeBlockExpression
            Public Sub New(_ifCondition As BinaryExpression, _thenCondition As CodeBlockExpression, _ElseCondition As CodeBlockExpression)
                MyBase.New(_ifCondition, _thenCondition)
                Me.ElseCondition = _ElseCondition
            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                If IfCondition.Evaluate(ParentEnv) = True Then
                    Return ThenCondition.Evaluate(ParentEnv)
                Else
                    Return ElseCondition.Evaluate(ParentEnv)
                End If


            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class CodeBlockExpression
            Inherits ExpressionSyntaxNode
            Public LocalMemory As EnvironmentalMemory
            Public Body As List(Of ExpressionSyntaxNode)
            '   Public BodyCount As Integer
            Public Sub New(ibody As List(Of ExpressionSyntaxNode))
                MyBase.New(SyntaxType._CodeBlock,
                           SyntaxType._CodeBlock.GetSyntaxTypeStr)
                If ibody Is Nothing Then
                    Throw New ArgumentNullException(NameOf(ibody))
                End If
                LocalMemory = New EnvironmentalMemory
                Me.Body = ibody

            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                LocalMemory = New EnvironmentalMemory(ParentEnv)

                For Each item In Body
                    item.Evaluate(LocalMemory)
                Next
                Return True
            End Function
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ReturnExpression
            Inherits CodeBlockExpression
            Public _Returns As IdentifierExpression
            Public ReturnType As LiteralType

            Public Sub New(_body As List(Of ExpressionSyntaxNode), ByRef iReturns As IdentifierExpression, iReturnType As LiteralType)
                MyBase.New(_body)
                Me._Returns = iReturns
                Me.ReturnType = iReturnType
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                LocalMemory = New EnvironmentalMemory(ParentEnv)
                LocalMemory.DefineValue(_Returns._Literal, ReturnType)
                For Each item In Body
                    item.Evaluate(LocalMemory)
                Next
                Return LocalMemory.GetVarValue(_Returns._Literal)
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ParenthesizedExpression
            Inherits ExpressionSyntaxNode
            Public Body As ExpressionSyntaxNode
            Public Sub New(ByRef Body As ExpressionSyntaxNode)
                MyBase.New(SyntaxType._ParenthesizedExpresion, SyntaxType._ParenthesizedExpresion.GetSyntaxTypeStr)
                Me.Body = Body
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object


                Body.Evaluate(ParentEnv)

                Return True
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

#Region "Literals"
        'Abstract
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend MustInherit Class LiteralExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>
            ''' Holds the Value of the literal :
            ''' Preffered method is to Initate the Object before assigning a value
            ''' Or
            ''' Strongly type the object in the inheriting class
            ''' </summary>
            Public _Literal As Object
            ''' <summary>
            ''' Used To create Literals (atomic values)
            ''' </summary>
            ''' <param name="Value">Typed Value of Expression </param>
            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String, ByRef Value As Object)
                MyBase.New(syntaxType, syntaxTypeStr)
                _Literal = Value
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        Friend Class NumericalExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Integer Expression
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._NumericLiteralExpression, SyntaxType._NumericLiteralExpression.GetSyntaxTypeStr, Value)

                Me._Literal = Value._Value

            End Sub



            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class StringExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Integer Expression
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._StringExpression, SyntaxType._StringExpression.GetSyntaxTypeStr, Value)

                Me._Literal = Value._Raw

            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IdentifierExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Identifier Expression Its value is its name or identifier tag
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._IdentifierExpression, SyntaxType._IdentifierExpression.GetSyntaxTypeStr, Value)
                Me._Literal = Value._Value
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckIfExists(_Literal) = False Then
                    Return "-Unknown Identifier-"
                End If
                Dim ivar = ParentEnv.GetVar(_Literal)
                Select Case ivar.Type
                    Case LiteralType._Boolean
                        Return New Boolean = ivar.Value
                    Case LiteralType._String
                        Dim x As String = ivar.Value
                        Return x
                    Case LiteralType._Array
                        Exit Select
                    Case LiteralType._Integer
                        Return New Integer = ivar.Value
                    Case LiteralType._Decimal
                        Return New Decimal = ivar.Value
                    Case LiteralType._Date
                        Return New Date = ivar.Value
                    Case LiteralType._NULL
                        Return ivar.Value

                End Select
                Return ivar.Value
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        Friend Class BooleanLiteralExpression
            Inherits LiteralExpression
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._BooleanLiteralExpression, SyntaxType._BooleanLiteralExpression.GetSyntaxTypeStr, Value)
                _Literal = New Boolean = False
                Select Case Value._SyntaxType
                    Case SyntaxType.TrueKeyword
                        Me._Literal = True
                    Case SyntaxType.FalseKeyword
                        Me._Literal = False
                End Select


            End Sub


            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ArrayLiteralExpression
            Inherits LiteralExpression

            Public Sub New(ByRef Value As Object)
                MyBase.New(SyntaxType._arrayList, SyntaxType._arrayList.GetSyntaxTypeStr, Value)
            End Sub



            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal._value
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

#Region "Variables"
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class AssignmentExpression
            Inherits ExpressionSyntaxNode
            Public _identifier As IdentifierExpression
            Public Operand As SyntaxToken
            ''' <summary>
            ''' Can be any expression
            ''' </summary>
            Public Value As ExpressionSyntaxNode
            Public Sub New(identifier As IdentifierExpression, operand As SyntaxToken, value As Object)
                MyBase.New(SyntaxType._AssignmentExpression, SyntaxType._AssignmentExpression.GetSyntaxTypeStr)

                If identifier Is Nothing Then
                    Throw New ArgumentNullException(NameOf(identifier))
                End If

                If value Is Nothing Then
                    Throw New ArgumentNullException(NameOf(value))
                End If

                _identifier = identifier
                Me.Operand = operand
                Me.Value = value
            End Sub
            Private Sub SetVar(ByRef ParentEnv As EnvironmentalMemory)


                'MustExist in record


                If ParentEnv.CheckIfExists(_identifier._Literal) = True Then

                    Dim iValue = calcValue(ParentEnv.GetVarValue(_identifier._Literal), ParentEnv)
                    ParentEnv.AssignValue(_identifier._Literal, iValue)
                Else

                End If


            End Sub


            Function calcValue(ByRef IdentifierValue As Object, ByRef ParentEnv As EnvironmentalMemory) As Object

                Dim nValue = IdentifierValue
                Dim result As Object

                Select Case Operand._SyntaxType
                    Case SyntaxType.Add_Equals_Operator
                        result = nValue + Value.Evaluate(ParentEnv)
                        Return New Integer = result
                    Case SyntaxType.Minus_Equals_Operator
                        result = nValue - Value.Evaluate(ParentEnv)
                        Return New Integer = result
                    Case SyntaxType.Multiply_Equals_Operator
                        result = nValue * Value.Evaluate(ParentEnv)
                        Return New Integer = result
                    Case SyntaxType.Divide_Equals_Operator
                        result = nValue / Value.Evaluate(ParentEnv)
                        Return New Integer = result
                    Case SyntaxType.Equals
                        result = Value.Evaluate(ParentEnv)
                        Return result
                    Case SyntaxType._ASSIGN
                        result = Value.Evaluate(ParentEnv)
                        Return result

                End Select
                Return "-Unknown Assignment Operation-"
            End Function
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                SetVar(ParentEnv)
                Return ParentEnv.GetVarValue(_identifier._Literal)
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class VariableDeclarationExpression
            Inherits IdentifierExpression
            Public _literalTypeStr As String
            Public _literalType As LiteralType
            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String, value As SyntaxToken, literalType As LiteralType)
                MyBase.New(value)
                Me._literalType = literalType
                _SyntaxType = SyntaxType._VariableDeclaration
                _SyntaxTypeStr = SyntaxType._VariableDeclaration.GetSyntaxTypeStr
                _Literal = value._Value
                _literalTypeStr = literalType.GetLiteralTypeStr
            End Sub


            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case _literalType
                    Case LiteralType._Boolean
                        ParentEnv.DefineValue(_Literal, _literalType, False)
                    Case LiteralType._String
                        ParentEnv.DefineValue(_Literal, _literalType, "")
                    Case LiteralType._Array
                        Exit Select
                    Case LiteralType._Integer
                        ParentEnv.DefineValue(_Literal, _literalType, 0)
                    Case LiteralType._Decimal
                        ParentEnv.DefineValue(_Literal, _literalType, 0)
                    Case LiteralType._Date
                        ParentEnv.DefineValue(_Literal, _literalType, Date.Now)
                    Case LiteralType._NULL
                        Exit Select
                End Select

                Return ParentEnv.GetVar(_Literal).Value
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

    End Namespace
End Namespace

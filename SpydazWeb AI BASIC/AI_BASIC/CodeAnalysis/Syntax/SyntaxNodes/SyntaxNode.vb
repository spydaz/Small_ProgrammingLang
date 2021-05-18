
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
        Public MustInherit Class SyntaxNode
            ''' <summary>
            ''' Gets the values for the held tokens
            ''' </summary>
            ''' <returns></returns>
            Public MustOverride Function GetChildren() As List(Of SyntaxToken)
            ''' <summary>
            ''' Enum Strong Type
            ''' </summary>
            Public _SyntaxType As SyntaxType
            ''' <summary>
            ''' Text String of Type(for diagnostics)
            ''' </summary>
            Public _SyntaxTypeStr As String
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
#End Region
        End Class
        ''' <summary>
        ''' Used for Strong Typing All Expressions must inherit this class
        ''' </summary>
        Public MustInherit Class ExpressionSyntaxNode
            Inherits SyntaxNode

            Protected Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(syntaxType, syntaxTypeStr)
            End Sub
        End Class
#End Region

#Region "Expressions"
        'Concrete
        Public Class BinaryExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>
            ''' Operator
            ''' </summary>
            Public _Operator As SyntaxToken
            ''' <summary>
            ''' Value / Identifier / Expression
            ''' </summary>
            Public _Left As ExpressionSyntaxNode
            ''' <summary>
            ''' Value / Identifier / Expression 
            ''' </summary>
            Public _Right As ExpressionSyntaxNode

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
            Public Sub New(ileft As ExpressionSyntaxNode, iright As ExpressionSyntaxNode, iOperator As SyntaxToken)
                MyBase.New(SyntaxType._BinaryExpression, SyntaxType._BinaryExpression.GetSyntaxTypeStr)



                _Left = ileft
                _Right = iright
                _Operator = iOperator

            End Sub

            ''' <summary>
            ''' Here we return a list of Syntax Tokens in the Order required by the Transpiler
            ''' s-Expression is Preffered;
            ''' 
            ''' Syntax:
            ''' OPERATOR , LITERAL, LITERAL
            ''' </summary>
            ''' <returns></returns>
            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim lst As New List(Of SyntaxToken)
                'RPL = OPERATOR , LITERAL, LITERAL
                lst.Add(_Operator)
                lst.AddRange(_Left.GetChildren)
                lst.AddRange(_Right.GetChildren)

                Return lst
            End Function
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
                        Return _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Sub_Operator
                        Return _Left.Evaluate(ParentEnv) - _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Divide_Operator
                        Return _Left.Evaluate(ParentEnv) / _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Multiply_Operator
                        Return _Left.Evaluate(ParentEnv) * _Right.Evaluate(ParentEnv)
        'Compare
                    Case SyntaxType.GreaterThan_Operator
                        Return _Left.Evaluate(ParentEnv) > _Right.Evaluate(ParentEnv)
                    Case SyntaxType.LessThanOperator
                        Return _Left.Evaluate(ParentEnv) > _Right.Evaluate(ParentEnv)
                    Case SyntaxType.GreaterThanEquals
                        Return _Left.Evaluate(ParentEnv) >= _Right.Evaluate(ParentEnv)
                    Case SyntaxType.LessThanEquals
                        Return _Left.Evaluate(ParentEnv) <= _Right.Evaluate(ParentEnv)
       'Complex Assign
                    Case SyntaxType.Add_Equals_Operator
                        Return _Left.Evaluate(ParentEnv) = _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Minus_Equals_Operator
                        Return _Left.Evaluate(ParentEnv) = _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Multiply_Equals_Operator
                        Return _Left.Evaluate(ParentEnv) = _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)
                    Case SyntaxType.Divide_Equals_Operator
                        Return _Left.Evaluate(ParentEnv) = _Left.Evaluate(ParentEnv) + _Right.Evaluate(ParentEnv)

                End Select
                Return 0
            End Function
        End Class
        Public Class UnaryExpression
            Inherits ExpressionSyntaxNode
            Public OperatorToken As SyntaxToken
            Public NumericLiteral As NumericalExpression
            Public Sub New(ByRef _OperatorToken As SyntaxToken, ByRef _NumericLiteral As NumericalExpression)
                MyBase.New(SyntaxType._UnaryExpression, SyntaxType._UnaryExpression.GetSyntaxTypeStr)
                OperatorToken = _OperatorToken
                NumericLiteral = _NumericLiteral


            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim lst As New List(Of SyntaxToken)
                Dim x = New SyntaxToken(SyntaxType._Integer, SyntaxType._Integer.GetSyntaxTypeStr, OperatorToken._Value & NumericLiteral._Literal, OperatorToken._Value & NumericLiteral._Literal, OperatorToken._start, OperatorToken._start + 2)
                lst.Add(x)
                Return lst
            End Function
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
        End Class
#End Region

#Region "Literals"
        'Abstract
        Public MustInherit Class LiteralExpression
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
        End Class
        Public Class NumericalExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Integer Expression
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._NumericLiteralExpression, SyntaxType._NumericLiteralExpression.GetSyntaxTypeStr, Value)
                _Literal = New SyntaxToken
                Me._Literal = Value

            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function


            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal._value
            End Function
        End Class
        Public Class StringExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Integer Expression
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._StringExpression, SyntaxType._StringExpression.GetSyntaxTypeStr, Value)

                Me._Literal = Value

            End Sub
            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal.value
            End Function
        End Class
        Public Class IdentifierExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Identifier Expression Its value is its name or identifier tag
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._IdentifierExpression, SyntaxType._IdentifierExpression.GetSyntaxTypeStr, Value)
            End Sub
            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal._value
            End Function
            Private Function CheckVar(ByRef ParentEnv As EnvironmentalMemory) As Boolean
                Return ParentEnv.CheckVar(_Literal._value)
            End Function
            Public Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckVar(_Literal._value) = True Then
                    Return ParentEnv.GetVar(_Literal._value)
                Else
                    Return "Does not Exist in Record"
                End If
            End Function
        End Class
        Public Class BooleanLiteralExpression
            Inherits LiteralExpression
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._BooleanLiteralExpression, SyntaxType._BooleanLiteralExpression.GetSyntaxTypeStr, Value)



            End Sub
            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal._value
            End Function
        End Class
        Public Class ArrayLiteralExpression
            Inherits LiteralExpression

            Public Sub New(ByRef Value As Object)
                MyBase.New(SyntaxType._arrayList, SyntaxType._arrayList.GetSyntaxTypeStr, Value)
            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function



            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal.value
            End Function
        End Class
#End Region

#Region "Variables"
        Public Class AssignmentExpression
            Inherits ExpressionSyntaxNode
            Public _identifier As IdentifierExpression
            Public Operand As SyntaxToken
            Public Value As ExpressionSyntaxNode

            Public Sub New(identifier As IdentifierExpression, operand As SyntaxToken, value As ExpressionSyntaxNode)
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
                Dim iName = _identifier.Evaluate(ParentEnv)
                Dim iValue = calcValue(ParentEnv)
                ParentEnv.AssignValue(iName, iValue)
            End Sub

            Private Function CheckVar(ByRef ParentEnv As EnvironmentalMemory) As Boolean
                Return ParentEnv.CheckVar(_identifier.Evaluate(ParentEnv))
            End Function
            Private Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckVar(_identifier.Evaluate(ParentEnv)) = True Then
                    Return ParentEnv.GetVar(_identifier.Evaluate(ParentEnv))
                Else
                    Return "Does not Exist in Record"
                End If
            End Function

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Throw New NotImplementedException()
            End Function
            Function calcValue(ByRef ParentEnv As EnvironmentalMemory) As Double
                Dim iValue = Value.Evaluate(ParentEnv)
                Dim nValue = GetValue(ParentEnv)
                Dim result As Object

                Select Case Operand._SyntaxType
                    Case SyntaxType.Add_Equals_Operator
                        result = nValue + iValue
                    Case SyntaxType.Minus_Equals_Operator
                        result = nValue - iValue
                    Case SyntaxType.Multiply_Equals_Operator
                        result = nValue * iValue
                    Case SyntaxType.Divide_Equals_Operator
                        result = nValue / iValue
                    Case SyntaxType.Equals
                        result = iValue
                    Case Else
                        result = 0
                End Select
                Return result
            End Function
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                SetVar(ParentEnv)
                Return GetValue(ParentEnv)
            End Function
        End Class
        Public Class VariableDeclarationExpression
            Inherits IdentifierExpression
            Public _literalType As LiteralType
            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String, value As SyntaxToken, literalType As LiteralType)
                MyBase.New(value)
                Me._literalType = literalType
                _SyntaxType = SyntaxType._VariableDeclaration
            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim lst As New List(Of SyntaxToken)
                lst.Add(_Literal.value)
                Return lst
            End Function

            Private Sub SetVar(ByRef ParentEnv As EnvironmentalMemory)
                Dim iType = _literalType
                Dim iName = _Literal.evaluate(ParentEnv)
                ParentEnv.Define(iName, iType)
            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                SetVar(ParentEnv)
                Return True
            End Function




        End Class
#End Region

    End Namespace
End Namespace

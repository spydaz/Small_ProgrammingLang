﻿
Imports System.Text
Imports System.Web.Script.Serialization
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
            ''' Gets the value for the Child
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
            ''' <param name="left">Left Value or Identifier</param>
            ''' <param name="right">Right Value or Identifier</param>
            ''' <param name="iOperator">Operator Token</param>
            Public Sub New(left As ExpressionSyntaxNode, right As ExpressionSyntaxNode, iOperator As SyntaxToken)
                MyBase.New(SyntaxType._BinaryExpression, "_BinaryExpression")
                If left Is Nothing Then
                    Throw New ArgumentNullException(NameOf(left))
                End If

                If right Is Nothing Then
                    Throw New ArgumentNullException(NameOf(right))
                End If


                _Left = left
                _Right = right
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
                Throw New NotImplementedException()
            End Function
        End Class

        Public Class UnaryExpression
            Inherits ExpressionSyntaxNode
            Public OperatorToken As SyntaxToken
            Public NumericLiteral As NumericalExpression
            Public Sub New(ByRef _OperatorToken As SyntaxToken, ByRef _NumericLiteral As NumericalExpression)
                MyBase.New(SyntaxType._UnaryExpression, "UnaryExpression")
                OperatorToken = _OperatorToken
                NumericLiteral = _NumericLiteral
            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)

                Select Case OperatorToken._SyntaxType
                    Case SyntaxType.Sub_Operator
                        Return -NumericLiteral._Literal
                    Case SyntaxType.Add_Operator
                        Return NumericLiteral._Literal
                End Select

                Throw New NotImplementedException()
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
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
            End Sub
        End Class
        'Concrete
        Public Class NumericalExpression
            Inherits LiteralExpression
            ''' <summary>
            ''' Initiates a Integer Expression
            ''' </summary>
            ''' <param name="Value"></param>
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._Integer, "_Integer", Value)


                Select Case Value._SyntaxType
                    Case SyntaxType._Integer
                        Me._SyntaxType = SyntaxType._Integer
                        Me._SyntaxTypeStr = "_Integer"
                    Case SyntaxType._Decimal
                        Me._SyntaxType = SyntaxType._Decimal
                        Me._SyntaxTypeStr = "_Decimal"
                End Select
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
                MyBase.New(SyntaxType._Identifier, "_Identifier", Value)
            End Sub

            Public Overrides Function GetChildren() As List(Of SyntaxToken)
                Dim Lst As New List(Of SyntaxToken)
                Lst.Add(_Literal)
                Return Lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Private Function CheckVar(ByRef ParentEnv As EnvironmentalMemory) As Boolean
                Return ParentEnv.CheckVar(_Literal)
            End Function
            Private Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If ParentEnv.CheckVar(_Literal) = True Then
                    Return ParentEnv.GetVar(_Literal)
                Else
                    Return Nothing
                End If
            End Function
        End Class
        Public Class BooleanLiteralExpression
            Inherits LiteralExpression

            Public Sub New(ByRef Value As Object)
                MyBase.New(SyntaxType._Boolean, "_Boolean", Value)
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
        Public Class ArrayLiteralExpression
            Inherits LiteralExpression

            Public Sub New(ByRef Value As Object)
                MyBase.New(SyntaxType._arrayList, "_arrayList", Value)
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



    End Namespace
End Namespace

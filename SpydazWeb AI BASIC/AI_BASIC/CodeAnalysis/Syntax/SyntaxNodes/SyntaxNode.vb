'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Syntax\SyntaxNodes\SyntaxNode.vb
'
' summary:	Syntax node class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Typing

Namespace Syntax
    Namespace SyntaxNodes
#Region "Abstract Models"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A syntax node. 
        ''' All nodes must use this model to Create SyntaxNodes
        ''' Defines the Syntax For the Language            </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Specialized constructor for use only by derived class. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="syntaxType">       Enum Strong Type. </param>
            ''' <param name="syntaxTypeStr">    Text String of Type(for diagnostics) </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Converts this  to a JSON. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   This  as a String. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Function ToJson() As String
                Return FormatJsonOutput(ToString)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Returns a string that represents the current object. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   A string that represents the current object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function ToString() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Format JSON output. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="jsonString">   The JSON string. </param>
            '''
            ''' <returns>   The formatted JSON output. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
#End Region
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An expression syntax node. 
        '''             Used for Strong Typing All Expressions must inherit this class</summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend MustInherit Class ExpressionSyntaxNode
            Inherits SyntaxNode

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Initializes the Type for the Syntax Node to identify the node. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="syntaxType">       SyntaxType. </param>
            ''' <param name="syntaxTypeStr">    String version of the Type. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Protected Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(syntaxType, syntaxTypeStr)
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

#Region "Expressions"

#Region "MathMatical"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A binary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    GeneralException.Add(New DiagnosticsException("Unable to register BinaryExpression ", ExceptionType.NullRefferenceError, "", SyntaxType._String))

                End If

                If iright Is Nothing Then
                    GeneralException.Add(New DiagnosticsException("Unable to register BinaryExpression ", ExceptionType.NullRefferenceError, "", SyntaxType._String))

                End If

                _Left = ileft
                _Right = iright
                _Operator = ioperator
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An unary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////


        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class UnaryExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>   The numeric literal. </summary>
            Public NumericLiteral As NumericalExpression
            Public OperatorToken As SyntaxToken

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_OperatorToken">   [in,out] The operator token. </param>
            ''' <param name="_NumericLiteral">  [in,out] The numeric literal. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(ByRef _OperatorToken As SyntaxToken, ByRef _NumericLiteral As NumericalExpression)
                MyBase.New(SyntaxType._UnaryExpression, SyntaxType._UnaryExpression.GetSyntaxTypeStr)
                OperatorToken = _OperatorToken
                NumericLiteral = _NumericLiteral
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case OperatorToken._SyntaxType
                    Case SyntaxType.Sub_Operator
                        Return -NumericLiteral.Evaluate(ParentEnv)
                    Case SyntaxType.Add_Operator
                        Return NumericLiteral.Evaluate(ParentEnv)
                End Select
                Return Nothing
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

#End Region


        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A code block expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class CodeBlockExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>   The local memory. </summary>
            Public LocalMemory As EnvironmentalMemory
            ''' <summary>   The body. </summary>
            Public Body As List(Of ExpressionSyntaxNode)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="ibody">    The ibody. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(ibody As List(Of ExpressionSyntaxNode))
                MyBase.New(SyntaxType._CodeBlock,
                           SyntaxType._CodeBlock.GetSyntaxTypeStr)
                If ibody Is Nothing Then
                    GeneralException.Add(New DiagnosticsException("Unable to register CodeBlockExpression " & NameOf(ibody), ExceptionType.NullRefferenceError, NameOf(ibody), SyntaxType._String))

                End If
                LocalMemory = New EnvironmentalMemory
                Me.Body = ibody

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                LocalMemory = New EnvironmentalMemory(ParentEnv)

                For Each item In Body
                    item.Evaluate(LocalMemory)
                Next
                Return True
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A return expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ReturnExpression
            Inherits CodeBlockExpression
            ''' <summary>   The returns. </summary>
            Public _Returns As IdentifierExpression
            ''' <summary>   Type of the return. </summary>
            Public ReturnType As LiteralType

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_body">        The body. </param>
            ''' <param name="iReturns">     [in,out] Zero-based index of the returns. </param>
            ''' <param name="iReturnType">  Type of the return. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(_body As List(Of ExpressionSyntaxNode), ByRef iReturns As IdentifierExpression, iReturnType As LiteralType)
                MyBase.New(_body)
                Me._Returns = iReturns
                Me.ReturnType = iReturnType
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                LocalMemory = New EnvironmentalMemory(ParentEnv)
                LocalMemory.DefineValue(_Returns._Literal, ReturnType)
                For Each item In Body
                    item.Evaluate(LocalMemory)
                Next
                Return LocalMemory.GetVarValue(_Returns._Literal)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A parenthesized expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ParenthesizedExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>   The body. </summary>
            Public Body As ExpressionSyntaxNode

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Body"> [in,out] The body. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(ByRef Body As ExpressionSyntaxNode)
                MyBase.New(SyntaxType._ParenthesizedExpresion, SyntaxType._ParenthesizedExpresion.GetSyntaxTypeStr)
                Me.Body = Body
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object


                Body.Evaluate(ParentEnv)

                Return True
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class


#Region "IF THEN ELSE"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   if expression. </summary>
        '''
        ''' <remarks>   Leroy, 24/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IfThenExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>   The then condition. </summary>
            Public ThenCondition As CodeBlockExpression
            ''' <summary>   if condition. </summary>
            Public IfCondition As BinaryExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ifCondition">      if condition. </param>
            ''' <param name="thenCondition">    The then condition. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(ifCondition As BinaryExpression,
                           thenCondition As CodeBlockExpression)
                MyBase.New(SyntaxType.ifThenExpression, SyntaxType.ifThenExpression.GetSyntaxTypeStr)


                Me.IfCondition = ifCondition
                Me.ThenCondition = thenCondition

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                If IfCondition.Evaluate(ParentEnv) = True Then
                    Return ThenCondition.Evaluate(ParentEnv)
                Else
                    Return True
                End If


            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   if else expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IfElseExpression
            Inherits IfThenExpression
            ''' <summary>   The else condition. </summary>
            Public ElseCondition As CodeBlockExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_ifCondition">     if condition. </param>
            ''' <param name="_thenCondition">   The then condition. </param>
            ''' <param name="_ElseCondition">   The else condition. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(_ifCondition As BinaryExpression, _thenCondition As CodeBlockExpression, _ElseCondition As CodeBlockExpression)
                MyBase.New(_ifCondition, _thenCondition)
                Me.ElseCondition = _ElseCondition
                Me._SyntaxType = SyntaxType.ifElseExpression
                Me._SyntaxTypeStr = Me._SyntaxType.GetSyntaxTypeStr
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                If IfCondition.Evaluate(ParentEnv) = True Then
                    Return ThenCondition.Evaluate(ParentEnv)
                Else
                    Return ElseCondition.Evaluate(ParentEnv)
                End If


            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

#End Region
#Region "DO"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A while expression.
        '''             
        ''' do
        '''   body
        ''' while condition
        '''
        ''' -------
        '''               
        '''                
        '''                  </summary>
        '''
        ''' <remarks>   Leroy, 29/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class WhileExpression
            Inherits ExpressionSyntaxNode
            Public Condition As ExpressionSyntaxNode
            Public Body As CodeBlockExpression

            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(SyntaxType.DO_WhileExpression, SyntaxType.DO_WhileExpression.GetSyntaxTypeStr)
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Do While Condition.Evaluate(ParentEnv) = True
                    Body.Evaluate(ParentEnv)
                Loop
                Return True
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An until expression.
        ''' do
        '''   body
        ''' until condition
        '''
        ''' -------            
        '''               </summary>
        '''
        ''' <remarks>   Leroy, 29/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class UntilExpression
            Inherits ExpressionSyntaxNode
            Public Condition As ExpressionSyntaxNode
            Public Body As CodeBlockExpression

            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(SyntaxType.DO_UntilExpression, SyntaxType.DO_UntilExpression.GetSyntaxTypeStr)
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Do Until Condition.Evaluate(ParentEnv) = True
                    Body.Evaluate(ParentEnv)
                Loop
                Return True
            End Function
        End Class
#End Region
#Region "For"
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   for expression. 
        '''             
        '''    for i = lower to upper
        '''     body
        '''
        '''  ------
        '''             
        '''             </summary>
        '''
        ''' <remarks>   Leroy, 29/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class ForExpression
            Inherits ExpressionSyntaxNode
            Public _Identifier As IdentifierExpression
            Public LowerStart As NumericalExpression
            Public UpperStart As NumericalExpression
            Public Body As CodeBlockExpression
            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(SyntaxType.ForExpression, SyntaxType.ForExpression.GetSyntaxTypeStr)
            End Sub
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                ParentEnv.DefineValue(_Identifier.Evaluate(ParentEnv), LiteralType._Integer, LowerStart.Evaluate(ParentEnv))
                For i = LowerStart.Evaluate(ParentEnv) To LowerStart.Evaluate(ParentEnv)
                    ParentEnv.AssignValue(_Identifier.Evaluate(ParentEnv), i)
                    Body.Evaluate(ParentEnv)
                Next i
                Return True
            End Function
        End Class
#End Region

#End Region

#Region "Literals"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="syntaxType">       Type of the syntax. </param>
            ''' <param name="syntaxTypeStr">    The syntax type string. </param>
            ''' <param name="Value">            [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String, ByRef Value As Object)
                MyBase.New(syntaxType, syntaxTypeStr)
                _Literal = Value
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A numerical expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class NumericalExpression
            Inherits LiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Value">    [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._NumericLiteralExpression, SyntaxType._NumericLiteralExpression.GetSyntaxTypeStr, Value)

                Me._Literal = Value._Value

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A string expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class StringExpression
            Inherits LiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Value">    [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._StringExpression, SyntaxType._StringExpression.GetSyntaxTypeStr, Value)

                Me._Literal = Value._Raw

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An identifier expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class IdentifierExpression
            Inherits LiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>    Initiates a Identifier Expression Its value is its name or identifier tag. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Value">    [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Sub New(ByRef Value As SyntaxToken)
                MyBase.New(SyntaxType._IdentifierExpression, SyntaxType._IdentifierExpression.GetSyntaxTypeStr, Value)
                Me._Literal = Value._Value
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets a value. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   The value. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A boolean literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class BooleanLiteralExpression
            Inherits LiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Value">    [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An array literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class ArrayLiteralExpression
            Inherits LiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Value">    [in,out] The value. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(ByRef Value As Object)
                MyBase.New(SyntaxType._arrayList, SyntaxType._arrayList.GetSyntaxTypeStr, Value)
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal._value
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region
#Region "Variables"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   An assignment expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class AssignmentExpression
            Inherits ExpressionSyntaxNode
            ''' <summary>   The identifier. </summary>
            Public _identifier As IdentifierExpression
            ''' <summary>   The operand. </summary>
            Public Operand As SyntaxToken
            ''' <summary>
            ''' Can be any expression
            ''' </summary>
            Public Value As ExpressionSyntaxNode

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
            '''                                             null. </exception>
            '''
            ''' <param name="identifier">   The identifier. </param>
            ''' <param name="operand">      The operand. </param>
            ''' <param name="value">        Can be any expression. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Sets a variable. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Sub SetVar(ByRef ParentEnv As EnvironmentalMemory)


                'MustExist in record


                If ParentEnv.CheckIfExists(_identifier._Literal) = True Then

                    Dim iValue = calcValue(ParentEnv.GetVarValue(_identifier._Literal), ParentEnv)
                    ParentEnv.AssignValue(_identifier._Literal, iValue)
                Else

                End If


            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Calculates the value. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="IdentifierValue">  [in,out] The identifier value. </param>
            ''' <param name="ParentEnv">        [in,out] The parent environment. </param>
            '''
            ''' <returns>   The calculated value. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                SetVar(ParentEnv)
                Return ParentEnv.GetVarValue(_identifier._Literal)
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A variable declaration expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Friend Class VariableDeclarationExpression
            Inherits IdentifierExpression
            ''' <summary>   The literal type string. </summary>
            Public _literalTypeStr As String
            ''' <summary>   Type of the literal. </summary>
            Public _literalType As LiteralType

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="syntaxType">       Type of the syntax. </param>
            ''' <param name="syntaxTypeStr">    The syntax type string. </param>
            ''' <param name="value">            The value. </param>
            ''' <param name="literalType">      Type of the literal. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String, value As SyntaxToken, literalType As LiteralType)
                MyBase.New(value)
                Me._literalType = literalType
                _SyntaxType = SyntaxType._VariableDeclaration
                _SyntaxTypeStr = SyntaxType._VariableDeclaration.GetSyntaxTypeStr
                _Literal = value._Value
                _literalTypeStr = literalType.GetLiteralTypeStr
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Gets debugger display. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns>   The debugger display. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
#End Region

    End Namespace
End Namespace

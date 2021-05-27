'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundUnaryOperator.vb
'
' summary:	Bound unary operator class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Syntax

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundUnaryOperator

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="syntaxKind">   The syntax kind. </param>
        ''' <param name="kind">         The kind. </param>
        ''' <param name="operatorType"> Type of the operator. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(syntaxKind As SyntaxKind, kind As BoundUnaryOperatorKind, operatorType As TypeSymbol)
            Me.New(syntaxKind, kind, operatorType, operatorType)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="syntaxKind">   The syntax kind. </param>
        ''' <param name="kind">         The kind. </param>
        ''' <param name="operandType">  Type of the operand. </param>
        ''' <param name="resultType">   Type of the result. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(syntaxKind As SyntaxKind, kind As BoundUnaryOperatorKind, operandType As TypeSymbol, resultType As TypeSymbol)
            Me.SyntaxKind = syntaxKind
            Me.Kind = kind
            Me.OperandType = operandType
            Type = resultType
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the syntax kind. </summary>
        '''
        ''' <value> The syntax kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property SyntaxKind As SyntaxKind

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Kind As BoundUnaryOperatorKind

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type of the operand. </summary>
        '''
        ''' <value> The type of the operand. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OperandType As TypeSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Type As TypeSymbol
        ''' <summary>   The operators. </summary>

        Private Shared ReadOnly m_operators() As BoundUnaryOperator = {
              New BoundUnaryOperator(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, TypeSymbol.Bool),
              New BoundUnaryOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, TypeSymbol.Int),
              New BoundUnaryOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, TypeSymbol.Int),
              New BoundUnaryOperator(SyntaxKind.TildeToken, BoundUnaryOperatorKind.Onescomplement, TypeSymbol.Int)
            }

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Binds. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="syntaxKind">   The syntax kind. </param>
        ''' <param name="operandType">  Type of the operand. </param>
        '''
        ''' <returns>   A BoundUnaryOperator. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Bind(syntaxKind As SyntaxKind, operandType As TypeSymbol) As BoundUnaryOperator
      For Each op In m_operators
        If op.SyntaxKind = syntaxKind AndAlso op.OperandType Is operandType Then
          Return op
        End If
      Next
      Return Nothing
    End Function

  End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\UnaryExpressionSyntax.vb
'
' summary:	Unary expression syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An unary expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class UnaryExpressionSyntax
        Inherits ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="operatorToken">    The operator token. </param>
        ''' <param name="operand">          The operand. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, operatorToken As SyntaxToken, operand As ExpressionSyntax)
            MyBase.New(tree)
            Me.OperatorToken = operatorToken
            Me.Operand = operand
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.UnaryExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the operator token. </summary>
        '''
        ''' <value> The operator token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OperatorToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the operand. </summary>
        '''
        ''' <value> The operand. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Operand As ExpressionSyntax

    End Class

End Namespace
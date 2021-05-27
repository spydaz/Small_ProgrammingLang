'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\ParenExpressionSyntax.vb
'
' summary:	Paren expression syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A paren expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class ParenExpressionSyntax
        Inherits ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="openParenToken">   The open paren token. </param>
        ''' <param name="expression">       The expression. </param>
        ''' <param name="closeParenToken">  The close paren token. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, openParenToken As SyntaxToken, expression As ExpressionSyntax, closeParenToken As SyntaxToken)
            MyBase.New(tree)
            Me.OpenParenToken = openParenToken
            Me.Expression = expression
            Me.CloseParenToken = closeParenToken
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.ParenExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the open paren token. </summary>
        '''
        ''' <value> The open paren token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OpenParenToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the expression. </summary>
        '''
        ''' <value> The expression. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Expression As ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the close paren token. </summary>
        '''
        ''' <value> The close paren token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property CloseParenToken As SyntaxToken

    End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\AssignmentExpressionSyntax.vb
'
' summary:	Assignment expression syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An assignment expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class AssignmentExpressionSyntax
        Inherits ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="identifierToken">  The identifier token. </param>
        ''' <param name="equalsToken">      The equals token. </param>
        ''' <param name="expression">       The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, identifierToken As SyntaxToken, equalsToken As SyntaxToken, expression As ExpressionSyntax)
            MyBase.New(tree)
            Me.IdentifierToken = identifierToken
            Me.EqualsToken = equalsToken
            Me.Expression = expression
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.AssignmentExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the identifier token. </summary>
        '''
        ''' <value> The identifier token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IdentifierToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the equals token. </summary>
        '''
        ''' <value> The equals token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EqualsToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the expression. </summary>
        '''
        ''' <value> The expression. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Expression As ExpressionSyntax

    End Class

End Namespace
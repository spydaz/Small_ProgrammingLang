'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\ReturnStatementSyntax.vb
'
' summary:	Return statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A return statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class ReturnStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="returnKeyword">    The return keyword. </param>
        ''' <param name="expression">       The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(tree As SyntaxTree, returnKeyword As SyntaxToken, expression As ExpressionSyntax)
            MyBase.New(tree)
            Me.ReturnKeyword = returnKeyword
            Me.Expression = expression
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind() As SyntaxKind = SyntaxKind.ReturnStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the return keyword. </summary>
        '''
        ''' <value> The return keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ReturnKeyword() As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the expression. </summary>
        '''
        ''' <value> The expression. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Expression() As ExpressionSyntax

    End Class

End Namespace
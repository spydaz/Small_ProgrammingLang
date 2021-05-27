'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\DoWhileStatementSyntax.vb
'
' summary:	Do while statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A do while statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class DoWhileStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="doKeyword">    The do keyword. </param>
        ''' <param name="body">         The body. </param>
        ''' <param name="whileKeyword"> The while keyword. </param>
        ''' <param name="condition">    The condition. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, doKeyword As SyntaxToken, body As StatementSyntax, whileKeyword As SyntaxToken, condition As ExpressionSyntax)
            MyBase.New(tree)
            Me.DoKeyword = doKeyword
            Me.Body = body
            Me.WhileKeyword = whileKeyword
            Me.Condition = condition
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.DoWhileStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the do keyword. </summary>
        '''
        ''' <value> The do keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property DoKeyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the while keyword. </summary>
        '''
        ''' <value> The while keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property WhileKeyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As ExpressionSyntax

    End Class

End Namespace
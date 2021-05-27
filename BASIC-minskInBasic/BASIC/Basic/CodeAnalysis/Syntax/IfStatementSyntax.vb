'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\IfStatementSyntax.vb
'
' summary:	If statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> if statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class IfStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="ifKeyword">        if keyword. </param>
        ''' <param name="condition">        The condition. </param>
        ''' <param name="thenStatement">    The then statement. </param>
        ''' <param name="elseClause">       The else clause. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, ifKeyword As SyntaxToken, condition As ExpressionSyntax, thenStatement As StatementSyntax,
            elseClause As ElseClauseSyntax)
            MyBase.New(tree)
            Me.IfKeyword = ifKeyword
            Me.Condition = condition
            Me.ThenStatement = thenStatement
            Me.ElseClause = elseClause
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.IfStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets if keyword. </summary>
        '''
        ''' <value> if keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IfKeyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the then statement. </summary>
        '''
        ''' <value> The then statement. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ThenStatement As StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the else clause. </summary>
        '''
        ''' <value> The else clause. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ElseClause As ElseClauseSyntax

    End Class

End Namespace
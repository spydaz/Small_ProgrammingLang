'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\ForStatementSyntax.vb
'
' summary:	For statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> for statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class ForStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="keyword">      The keyword. </param>
        ''' <param name="identifier">   The identifier. </param>
        ''' <param name="equalsToken">  The equals token. </param>
        ''' <param name="lowerBound">   The lower bound. </param>
        ''' <param name="toKeyword">    to keyword. </param>
        ''' <param name="upperBound">   The upper bound. </param>
        ''' <param name="body">         The body. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, keyword As SyntaxToken, identifier As SyntaxToken, equalsToken As SyntaxToken, lowerBound As ExpressionSyntax, toKeyword As SyntaxToken, upperBound As ExpressionSyntax, body As StatementSyntax)
            MyBase.New(tree)
            Me.Keyword = keyword
            Me.Identifier = identifier
            Me.EqualsToken = equalsToken
            Me.LowerBound = lowerBound
            Me.ToKeyword = toKeyword
            Me.UpperBound = upperBound
            Me.Body = body
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.ForStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the keyword. </summary>
        '''
        ''' <value> The keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Keyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the identifier. </summary>
        '''
        ''' <value> The identifier. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Identifier As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the equals token. </summary>
        '''
        ''' <value> The equals token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EqualsToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the lower bound. </summary>
        '''
        ''' <value> The lower bound. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property LowerBound As ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets to keyword. </summary>
        '''
        ''' <value> to keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ToKeyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the upper bound. </summary>
        '''
        ''' <value> The upper bound. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property UpperBound As ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As StatementSyntax

    End Class

End Namespace
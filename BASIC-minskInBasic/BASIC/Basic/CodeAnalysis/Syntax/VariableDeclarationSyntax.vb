'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\VariableDeclarationSyntax.vb
'
' summary:	Variable declaration syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A variable declaration syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    ' var x = 10 ' variable you can assign again
    ' let x = 10 ' variable that is readonly
    ' dim x = 10 ' same as "var"
    Partial Public NotInheritable Class VariableDeclarationSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="keyword">      The keyword. </param>
        ''' <param name="identifier">   The identifier. </param>
        ''' <param name="typeClause">   The type clause. </param>
        ''' <param name="equalsToken">  The equals token. </param>
        ''' <param name="initializer">  The initializer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, keyword As SyntaxToken, identifier As SyntaxToken, typeClause As TypeClauseSyntax, equalsToken As SyntaxToken, initializer As ExpressionSyntax)
            MyBase.New(tree)
            Me.Keyword = keyword
            Me.Identifier = identifier
            Me.TypeClause = typeClause ' ?
            Me.EqualsToken = equalsToken
            Me.Initializer = initializer
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.VariableDeclaration

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
        ''' <summary>   Gets or sets the type clause. </summary>
        '''
        ''' <value> The type clause. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property TypeClause As TypeClauseSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the equals token. </summary>
        '''
        ''' <value> The equals token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EqualsToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the initializer. </summary>
        '''
        ''' <value> The initializer. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Initializer As ExpressionSyntax

    End Class

End Namespace
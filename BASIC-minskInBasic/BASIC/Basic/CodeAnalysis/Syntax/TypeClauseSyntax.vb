'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\TypeClauseSyntax.vb
'
' summary:	Type clause syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A type clause syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class TypeClauseSyntax
        Inherits SyntaxNode

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="colonToken">   The colon token. </param>
        ''' <param name="identifier">   The identifier. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, colonToken As SyntaxToken, identifier As SyntaxToken)
            MyBase.New(tree)
            Me.ColonToken = colonToken
            Me.Identifier = identifier
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.TypeClause

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the colon token. </summary>
        '''
        ''' <value> The colon token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ColonToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the identifier. </summary>
        '''
        ''' <value> The identifier. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Identifier As SyntaxToken

    End Class

End Namespace
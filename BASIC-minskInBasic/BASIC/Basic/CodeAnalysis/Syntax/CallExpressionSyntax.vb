'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\CallExpressionSyntax.vb
'
' summary:	Call expression syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A call expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class CallExpressionSyntax
        Inherits ExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="identifier">   The identifier. </param>
        ''' <param name="openParen">    The open paren. </param>
        ''' <param name="arguments">    The arguments. </param>
        ''' <param name="closeParen">   The close paren. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, identifier As SyntaxToken, openParen As SyntaxToken, arguments As SeparatedSyntaxList(Of ExpressionSyntax), closeParen As SyntaxToken)
            MyBase.New(tree)
            Me.Identifier = identifier
            Me.OpenParen = openParen
            Me.Arguments = arguments
            Me.CloseParen = closeParen
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.CallExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the identifier. </summary>
        '''
        ''' <value> The identifier. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Identifier As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the open paren. </summary>
        '''
        ''' <value> The open paren. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OpenParen As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the arguments. </summary>
        '''
        ''' <value> The arguments. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Arguments As SeparatedSyntaxList(Of ExpressionSyntax)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the close paren. </summary>
        '''
        ''' <value> The close paren. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property CloseParen As SyntaxToken

    End Class

End Namespace
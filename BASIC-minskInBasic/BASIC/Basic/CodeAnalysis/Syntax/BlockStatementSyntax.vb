'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\BlockStatementSyntax.vb
'
' summary:	Block statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A block statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class BlockStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="openBraceToken">   The open brace token. </param>
        ''' <param name="statements">       The statements. </param>
        ''' <param name="closeBraceToken">  The close brace token. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, openBraceToken As SyntaxToken, statements As ImmutableArray(Of StatementSyntax), closeBraceToken As SyntaxToken)
            MyBase.New(tree)
            Me.OpenBraceToken = openBraceToken
            Me.Statements = statements
            Me.CloseBraceToken = closeBraceToken
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.BlockStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the open brace token. </summary>
        '''
        ''' <value> The open brace token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OpenBraceToken As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the statements. </summary>
        '''
        ''' <value> The statements. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Statements As ImmutableArray(Of StatementSyntax)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the close brace token. </summary>
        '''
        ''' <value> The close brace token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property CloseBraceToken As SyntaxToken

    End Class

End Namespace
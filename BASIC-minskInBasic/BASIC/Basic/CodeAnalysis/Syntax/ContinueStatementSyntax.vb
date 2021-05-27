'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\ContinueStatementSyntax.vb
'
' summary:	Continue statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A continue statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Friend Class ContinueStatementSyntax
        Inherits StatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">     The tree. </param>
        ''' <param name="keyword">  The keyword. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(tree As SyntaxTree, keyword As SyntaxToken)
            MyBase.New(tree)
            Me.Keyword = keyword
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind() As SyntaxKind
            Get
                Return SyntaxKind.ContinueStatement
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the keyword. </summary>
        '''
        ''' <value> The keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Keyword() As SyntaxToken

    End Class

End Namespace
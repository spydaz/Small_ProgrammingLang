'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\StatementSyntax.vb
'
' summary:	Statement syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class StatementSyntax
        Inherits SyntaxNode

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree"> The tree. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Sub New(tree As SyntaxTree)
      MyBase.New(tree)
    End Sub

  End Class

End Namespace
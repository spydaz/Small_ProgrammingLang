'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\ExpressionSyntax.vb
'
' summary:	Expression syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class ExpressionSyntax
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
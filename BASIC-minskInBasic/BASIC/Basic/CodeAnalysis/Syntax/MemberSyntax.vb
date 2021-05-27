'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\MemberSyntax.vb
'
' summary:	Member syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A member syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class MemberSyntax
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
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\CompilationUnitSyntax.vb
'
' summary:	Compilation unit syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A compilation unit syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class CompilationUnitSyntax
        Inherits SyntaxNode

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="members">          The members. </param>
        ''' <param name="endOfFileToken">   The end of file token. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, members As ImmutableArray(Of MemberSyntax), endOfFileToken As SyntaxToken)
            MyBase.New(tree)
            Me.Members = members
            Me.EndOfFileToken = endOfFileToken
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.CompilationUnit

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the members. </summary>
        '''
        ''' <value> The members. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Members As ImmutableArray(Of MemberSyntax)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the end of file token. </summary>
        '''
        ''' <value> The end of file token. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EndOfFileToken As SyntaxToken

    End Class

End Namespace
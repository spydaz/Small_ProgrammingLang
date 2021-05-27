'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundBlockStatement.vb
'
' summary:	Bound block statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundBlockStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="statements">   The statements. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(statements As ImmutableArray(Of BoundStatement))
            Me.Statements = statements
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.BlockStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the statements. </summary>
        '''
        ''' <value> The statements. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Statements As ImmutableArray(Of BoundStatement)

    End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\EvaluationResult.vb
'
' summary:	Evaluation result class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Encapsulates the result of an evaluation. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class EvaluationResult

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="diagnostics">  The diagnostics. </param>
        ''' <param name="value">        The value. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(diagnostics As ImmutableArray(Of Diagnostic), value As Object)
            Me.Diagnostics = diagnostics
            Me.Value = value
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the diagnostics. </summary>
        '''
        ''' <value> The diagnostics. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Diagnostics As ImmutableArray(Of Diagnostic)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the value. </summary>
        '''
        ''' <value> The value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Value As Object

    End Class
End Namespace
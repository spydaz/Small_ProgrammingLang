'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundLabelStatement.vb
'
' summary:	Bound label statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundLabelStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="label">    The label. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(label As BoundLabel)
            Me.Label = label
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.LabelStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the label. </summary>
        '''
        ''' <value> The label. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Label As BoundLabel

    End Class

End Namespace
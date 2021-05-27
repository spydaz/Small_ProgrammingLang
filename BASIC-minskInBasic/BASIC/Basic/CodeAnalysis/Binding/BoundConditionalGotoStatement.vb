'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundConditionalGotoStatement.vb
'
' summary:	Bound conditional goto statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundConditionalGotoStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="label">        The label. </param>
        ''' <param name="condition">    The condition. </param>
        ''' <param name="jumpIfTrue">   (Optional) True to jump if true. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(label As BoundLabel, condition As BoundExpression, Optional jumpIfTrue As Boolean = True)
            Me.Label = label
            Me.Condition = condition
            Me.JumpIfTrue = jumpIfTrue
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.ConditionalGotoStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the label. </summary>
        '''
        ''' <value> The label. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Label As BoundLabel

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the jump if true. </summary>
        '''
        ''' <value> The jump if true. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property JumpIfTrue As Boolean

    End Class

End Namespace
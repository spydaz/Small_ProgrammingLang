'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundLoopStatement.vb
'
' summary:	Bound loop statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A bound loop statement. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend MustInherit Class BoundLoopStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Specialized constructor for use only by derived class. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="breakLabel">       The break label. </param>
        ''' <param name="continueLabel">    The continue label. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Sub New(breakLabel As BoundLabel, continueLabel As BoundLabel)
            Me.BreakLabel = breakLabel
            Me.ContinueLabel = continueLabel
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the break label. </summary>
        '''
        ''' <value> The break label. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property BreakLabel() As BoundLabel

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the continue label. </summary>
        '''
        ''' <value> The continue label. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ContinueLabel() As BoundLabel

    End Class

End Namespace
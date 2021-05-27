'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundConstant.vb
'
' summary:	Bound constant class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Constructor. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="value">  The value. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend NotInheritable Class BoundConstant

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="value">    The value. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(value As Object)
            Me.Value = value
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the value. </summary>
        '''
        ''' <value> The value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Value As Object

  End Class

End Namespace
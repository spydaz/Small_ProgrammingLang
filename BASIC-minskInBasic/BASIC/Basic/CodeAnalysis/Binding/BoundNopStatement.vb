'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundNopStatement.vb
'
' summary:	Bound nop statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Gets or sets the kind. </summary>
    '''
    ''' <value>   The kind. </value>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend NotInheritable Class BoundNopStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind
      Get
        Return BoundNodeKind.NopStatement
      End Get
    End Property

  End Class


End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundLabel.vb
'
' summary:	Bound label class
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
    ''' <param name="name">   The name. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend NotInheritable Class BoundLabel

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name"> The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Sub New(name As String)
            Me.Name = name
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the name. </summary>
        '''
        ''' <value> The name. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Name As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
      Return Name
    End Function

  End Class

End Namespace
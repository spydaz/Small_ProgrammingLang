'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Diagnostic.vb
'
' summary:	Diagnostic class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Text

Namespace Global.Basic.CodeAnalysis

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A diagnostic. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class Diagnostic

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="message">  The message. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(location As TextLocation, message As String)
            Me.Location = location
            Me.Message = message
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the location. </summary>
        '''
        ''' <value> The location. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Location As TextLocation

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the message. </summary>
        '''
        ''' <value> The message. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Message As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
      Return Message
    End Function

  End Class

End Namespace
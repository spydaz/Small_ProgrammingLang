'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Text\TextSpan.vb
'
' summary:	Text span class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Text

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A text span. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Structure TextSpan

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="start">    The start. </param>
        ''' <param name="length">   The length. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(start As Integer, length As Integer)
            Me.Start = start
            Me.Length = length
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the start. </summary>
        '''
        ''' <value> The start. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Start As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the length. </summary>
        '''
        ''' <value> The length. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Length As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the end. </summary>
        '''
        ''' <value> The end. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property [End] As Integer
            Get
                Return Start + Length
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   From bounds. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="start">        The start. </param>
        ''' <param name="![end]">       The [end]. </param>
        ''' <param name="As Integer">   as integer. </param>
        '''
        ''' <returns>   A TextSpan. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function FromBounds(start As Integer, [end] As Integer) As TextSpan
            Dim length = [end] - start
            Return New TextSpan(start, length)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Overlaps with. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="span"> The span. </param>
        '''
        ''' <returns>   True if it succeeds, false if it fails. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function OverlapsWith(span As TextSpan) As Boolean
            Return Start < span.End AndAlso
                   [End] > span.Start
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns the fully qualified type name of this instance. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The fully qualified type name. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
      Return $"{Start}...{[End]}"
    End Function

  End Structure

End Namespace
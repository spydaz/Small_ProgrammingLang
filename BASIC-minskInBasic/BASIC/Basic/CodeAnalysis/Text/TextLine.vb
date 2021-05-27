'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Text\TextLine.vb
'
' summary:	Text line class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Text

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A text line. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class TextLine

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">                     The text. </param>
        ''' <param name="start">                    The start. </param>
        ''' <param name="length">                   The length. </param>
        ''' <param name="lengthIncludingLineBreak"> The length including line break. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(text As SourceText, start As Integer, length As Integer, lengthIncludingLineBreak As Integer)
            Me.Text = text
            Me.Start = start
            Me.Length = length
            Me.LengthIncludingLineBreak = lengthIncludingLineBreak
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the text. </summary>
        '''
        ''' <value> The text. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Text As SourceText

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
        ''' <summary>   Gets the length including line break. </summary>
        '''
        ''' <value> The length including line break. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property LengthIncludingLineBreak As Integer

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
        ''' <summary>   Gets the span. </summary>
        '''
        ''' <value> The span. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Span As TextSpan
            Get
                Return New TextSpan(Start, Length)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the span including line break. </summary>
        '''
        ''' <value> The span including line break. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property SpanIncludingLineBreak As TextSpan
            Get
                Return New TextSpan(Start, LengthIncludingLineBreak)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
      Return Text.ToString(Span)
    End Function

  End Class

End Namespace
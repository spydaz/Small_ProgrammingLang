'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Text\TextLocation.vb
'
' summary:	Text location class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Text

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A text location. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Structure TextLocation

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        ''' <param name="span"> The span. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(text As SourceText, span As TextSpan)
            Me.Text = text
            Me.Span = span
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the text. </summary>
        '''
        ''' <value> The text. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Text As SourceText

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the span. </summary>
        '''
        ''' <value> The span. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Span As TextSpan

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the filename of the file. </summary>
        '''
        ''' <value> The name of the file. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property FileName As String
            Get
                Return Text.FileName
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the start line. </summary>
        '''
        ''' <value> The start line. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property StartLine As Integer
            Get
                Return Text.GetLineIndex(Span.Start)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the end line. </summary>
        '''
        ''' <value> The end line. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EndLine As Integer
            Get
                Return Text.GetLineIndex(Span.End)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the start character. </summary>
        '''
        ''' <value> The start character. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property StartCharacter As Integer
            Get
                Return Span.Start - Text.Lines(StartLine).Start
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the end character. </summary>
        '''
        ''' <value> The end character. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property EndCharacter As Integer
      Get
        Return Span.End - Text.Lines(StartLine).Start
      End Get
    End Property

  End Structure

End Namespace
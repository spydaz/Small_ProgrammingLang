'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Text\SourceText.vb
'
' summary:	Source text class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Text

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A source text. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class SourceText
        ''' <summary>   The text. </summary>

        Private ReadOnly Text As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the filename of the file. </summary>
        '''
        ''' <value> The name of the file. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property FileName As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">     The text. </param>
        ''' <param name="fileName"> Filename of the file. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub New(text As String, fileName As String)
            Me.Text = text
            Me.FileName = fileName
            Lines = ParseLines(Me, text)
        End Sub

        Public Shared Function [From](text As String, Optional fileName As String = "") As SourceText
            Return New SourceText(text, fileName)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the lines. </summary>
        '''
        ''' <value> The lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Lines As ImmutableArray(Of TextLine)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the item. </summary>
        '''
        ''' <value> The item. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Default Public ReadOnly Property Item(index As Integer) As Char
            Get
                Return Text(index)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the length. </summary>
        '''
        ''' <value> The length. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Length As Integer
            Get
                Return Text.Length
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets line index. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="position"> The position. </param>
        '''
        ''' <returns>   The line index. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetLineIndex(position As Integer) As Integer

            ' Implementing a "Binary Search".

            Dim lower = 0
            Dim upper = Lines.Length - 1

            While lower <= upper

                Dim index = lower + ((upper - lower) \ 2)
                Dim start = Lines(index).Start

                If position = start Then
                    ' Found it!
                    Return index
                End If

                If start > position Then
                    ' "discard" the upper window.
                    upper = index - 1
                Else
                    ' "discard" the lower window.
                    lower = index + 1
                End If

            End While

            ' We've run out of stuff to search, return where we ended up.
            Return lower - 1

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse lines. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sourceText">   Source text. </param>
        ''' <param name="text">         The text. </param>
        '''
        ''' <returns>   An ImmutableArray(Of TextLine) </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Function ParseLines(sourceText As SourceText, text As String) As ImmutableArray(Of TextLine)

            Dim result = ImmutableArray.CreateBuilder(Of TextLine)

            Dim position = 0
            Dim lineStart = 0

            While position < text.Length
                Dim lineBreakWidth = GetLineBreakWidth(text, position)
                If lineBreakWidth = 0 Then
                    position += 1
                Else
                    AddLine(result, sourceText, position, lineStart, lineBreakWidth)
                    position += lineBreakWidth
                    lineStart = position
                End If
            End While

            If position >= lineStart Then
                AddLine(result, sourceText, position, lineStart, 0)
            End If

            Return result.ToImmutable

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Adds a line. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="result">           The result. </param>
        ''' <param name="sourceText">       Source text. </param>
        ''' <param name="position">         The position. </param>
        ''' <param name="lineStart">        The line start. </param>
        ''' <param name="lineBreakWidth">   Width of the line break. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Sub AddLine(result As ImmutableArray(Of TextLine).Builder, sourceText As SourceText, position As Integer, lineStart As Integer, lineBreakWidth As Integer)
            Dim lineLength = position - lineStart
            Dim lineLengthIncludingLineBreak = lineLength + lineBreakWidth
            Dim line = New TextLine(sourceText, lineStart, lineLength, lineLengthIncludingLineBreak)
            result.Add(line)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets line break width. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">     The text. </param>
        ''' <param name="position"> The position. </param>
        '''
        ''' <returns>   The line break width. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Function GetLineBreakWidth(text As String, position As Integer) As Integer

            Dim c = text(position)
            Dim l = If(position >= text.Length - 1, ChrW(0), text(position + 1))

            If c = ChrW(13) AndAlso l = ChrW(10) Then
                Return 2
            End If

            If c = ChrW(13) OrElse c = ChrW(10) Then
                Return 1
            End If

            Return 0

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
            Return Text
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Convert this  into a string representation. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="start">    The start. </param>
        ''' <param name="length">   The length. </param>
        '''
        ''' <returns>   A String that represents this. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overloads Function ToString(start As Integer, length As Integer) As String
            Return Text.Substring(start, length)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Convert this  into a string representation. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="span"> The span. </param>
        '''
        ''' <returns>   A String that represents this. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overloads Function ToString(span As TextSpan) As String
      Return Text.Substring(span.Start, span.Length)
    End Function

  End Class

End Namespace
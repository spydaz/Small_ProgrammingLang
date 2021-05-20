﻿Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Text

  Public NotInheritable Class SourceText

    Private ReadOnly Text As String

    Public ReadOnly Property FileName As String

    Private Sub New(text As String, fileName As String)
      Me.Text = text
      Me.FileName = fileName
      Lines = ParseLines(Me, text)
    End Sub

    Public Shared Function [From](text As String, Optional fileName As String = "") As SourceText
      Return New SourceText(text, fileName)
    End Function

    Public ReadOnly Property Lines As ImmutableArray(Of TextLine)

    Default Public ReadOnly Property Item(index As Integer) As Char
      Get
        Return Text(index)
      End Get
    End Property

    Public ReadOnly Property Length As Integer
      Get
        Return Text.Length
      End Get
    End Property

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

    Private Shared Sub AddLine(result As ImmutableArray(Of TextLine).Builder, sourceText As SourceText, position As Integer, lineStart As Integer, lineBreakWidth As Integer)
      Dim lineLength = position - lineStart
      Dim lineLengthIncludingLineBreak = lineLength + lineBreakWidth
      Dim line = New TextLine(sourceText, lineStart, lineLength, lineLengthIncludingLineBreak)
      result.Add(line)
    End Sub

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

    Public Overrides Function ToString() As String
      Return Text
    End Function

    Public Overloads Function ToString(start As Integer, length As Integer) As String
      Return Text.Substring(start, length)
    End Function

    Public Overloads Function ToString(span As TextSpan) As String
      Return Text.Substring(span.Start, span.Length)
    End Function

  End Class

End Namespace
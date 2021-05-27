'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxToken.vb
'
' summary:	Syntax token class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports Basic.CodeAnalysis.Text

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A syntax token. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class SyntaxToken
        Inherits SyntaxNode

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="kind">             The kind. </param>
        ''' <param name="position">         The position. </param>
        ''' <param name="text">             The text. </param>
        ''' <param name="value">            The value. </param>
        ''' <param name="leadingTrivia">    The leading trivia. </param>
        ''' <param name="trailingTrivia">   The trailing trivia. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, kind As SyntaxKind, position As Integer, text As String, value As Object, leadingTrivia As ImmutableArray(Of SyntaxTrivia), trailingTrivia As ImmutableArray(Of SyntaxTrivia))
            MyBase.New(tree)
            Me.Kind = kind
            Me.Position = position
            Me.Text = If(text, String.Empty)
            IsMissing = text Is Nothing
            Me.Value = value
            Me.LeadingTrivia = leadingTrivia
            Me.TrailingTrivia = trailingTrivia
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the position. </summary>
        '''
        ''' <value> The position. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Position As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the text. </summary>
        '''
        ''' <value> The text. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Text As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the value. </summary>
        '''
        ''' <value> The value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Value As Object

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the span. </summary>
        '''
        ''' <value> The span. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Span As TextSpan
            Get
                Return New TextSpan(Position, Text.Length)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the full span. </summary>
        '''
        ''' <value> The full span. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property FullSpan As TextSpan
            Get
                Dim start = If(LeadingTrivia.Length = 0, Span.Start, LeadingTrivia.First.Span.Start)
                Dim [end] = If(TrailingTrivia.Length = 0, Span.End, TrailingTrivia.Last.Span.End)
                Return TextSpan.FromBounds(start, [end])
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the leading trivia. </summary>
        '''
        ''' <value> The leading trivia. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property LeadingTrivia As ImmutableArray(Of SyntaxTrivia)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the trailing trivia. </summary>
        '''
        ''' <value> The trailing trivia. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property TrailingTrivia As ImmutableArray(Of SyntaxTrivia)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function GetChildren() As IEnumerable(Of SyntaxNode)
            Return Array.Empty(Of SyntaxNode)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the is missing. </summary>
        '''
        ''' <value> The is missing. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        ''' <summary>
        ''' A token is missing if it was inserted by the parser and doesn't appear in source
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsMissing As Boolean

  End Class

End Namespace
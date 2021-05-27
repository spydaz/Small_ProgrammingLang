'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxTrivia.vb
'
' summary:	Syntax trivia class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Text

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A syntax trivia. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class SyntaxTrivia

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="syntaxTree">   The syntax tree. </param>
        ''' <param name="kind">         The kind. </param>
        ''' <param name="position">     The position. </param>
        ''' <param name="text">         The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(syntaxTree As SyntaxTree, kind As SyntaxKind, position As Integer, text As String)
            Me.SyntaxTree = syntaxTree
            Me.Kind = kind
            Me.Position = position
            Me.Text = text
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the syntax tree. </summary>
        '''
        ''' <value> The syntax tree. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property SyntaxTree As SyntaxTree

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Kind As SyntaxKind

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the position. </summary>
        '''
        ''' <value> The position. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Position As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the span. </summary>
        '''
        ''' <value> The span. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Span As TextSpan
            Get
                Return New TextSpan(Position, If(Text?.Length, 0))
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the text. </summary>
        '''
        ''' <value> The text. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Text As String

    End Class

End Namespace
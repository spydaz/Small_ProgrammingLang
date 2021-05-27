'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Lexer.vb
'
' summary:	Lexer class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports System.Text
Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Text

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Gets or sets the diagnostics. </summary>
    '''
    ''' <value>   The diagnostics. </value>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend NotInheritable Class Lexer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the diagnostics. </summary>
        '''
        ''' <value> The diagnostics. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Diagnostics As DiagnosticBag = New DiagnosticBag
        ''' <summary>   The syntax tree. </summary>
        Private ReadOnly m_syntaxTree As SyntaxTree
        ''' <summary>   The text. </summary>
        Private ReadOnly m_text As SourceText
        ''' <summary>   The position. </summary>
        Private m_position As Integer
        ''' <summary>   The start. </summary>

        Private m_start As Integer
        ''' <summary>   The kind. </summary>
        Private m_kind As SyntaxKind
        ''' <summary>   The value. </summary>
        Private m_value As Object
        ''' <summary>   The trivia builder. </summary>
        Private ReadOnly m_triviaBuilder As ImmutableArray(Of SyntaxTrivia).Builder = ImmutableArray.CreateBuilder(Of SyntaxTrivia)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree"> The tree. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(tree As SyntaxTree)
            m_syntaxTree = tree
            m_text = tree.Text
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the current. </summary>
        '''
        ''' <value> The current. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private ReadOnly Property Current As Char
            Get
                Return Peek(0)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the look ahead. </summary>
        '''
        ''' <value> The look ahead. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private ReadOnly Property LookAhead As Char
            Get
                Return Peek(1)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the peek. </summary>
        '''
        ''' <value> The peek. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private ReadOnly Property Peek(offset As Integer) As Char
            Get
                Dim index = m_position + offset
                If index >= m_text.Length Then
                    Return ChrW(0)
                End If
                Return m_text(index)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the lex. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A SyntaxToken. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function Lex() As SyntaxToken

            ReadTrivia(True)

            Dim leadingTrivia = m_triviaBuilder.ToImmutable
            Dim tokenStart = m_position

            ReadToken()

            Dim tokenKind = m_kind
            Dim tokenValue = m_value
            Dim tokenLength = m_position - m_start

            ReadTrivia(False)

            Dim trailingTrivia = m_triviaBuilder.ToImmutable

            Dim tokenText = SyntaxFacts.GetText(tokenKind)
            If tokenText Is Nothing Then
                tokenText = m_text.ToString(tokenStart, tokenLength)
            End If

            Return New SyntaxToken(m_syntaxTree, tokenKind, tokenStart, tokenText, tokenValue, leadingTrivia, trailingTrivia)

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads a trivia. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="leading">  True to leading. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadTrivia(leading As Boolean)

            m_triviaBuilder.Clear()

            Dim done = False

            While Not done

                m_start = m_position
                m_kind = SyntaxKind.BadToken
                m_value = Nothing

                Select Case Current
                    Case ChrW(0)
                        done = True
                    Case "/"c
                        If LookAhead = "/" Then
                            ReadSingleLineComment()
                        ElseIf LookAhead = "*" Then
                            ReadMultiLineComment()
                        Else
                            done = True
                        End If
                    Case ChrW(10), ChrW(13)
                        If Not leading Then done = True
                        ReadLineBreak()
                    Case " "c, ChrW(9) ' Short-circuit whitespace checking (common).
                        ReadWhiteSpace()
                    Case Else
                        If Char.IsWhiteSpace(Current) Then
                            ReadWhiteSpace()
                        Else
                            done = True
                        End If
                End Select
                Dim length = m_position - m_start
                If length > 0 Then
                    Dim text = m_text.ToString(m_start, length)
                    Dim trivia = New SyntaxTrivia(m_syntaxTree, m_kind, m_start, text)
                    m_triviaBuilder.Add(trivia)
                End If
            End While

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads line break. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadLineBreak()

            If Current = ChrW(13) AndAlso LookAhead = ChrW(10) Then
                m_position += 2
            Else
                m_position += 1
            End If

            m_kind = SyntaxKind.LineBreakTrivia

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads white space. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadWhiteSpace()

            Dim done = False
            While Not done
                Select Case Current
                    Case ChrW(0), ChrW(10), ChrW(13)
                        done = True
                    Case Else
                        If Not Char.IsWhiteSpace(Current) Then
                            done = True
                        Else
                            m_position += 1
                        End If
                End Select
            End While

            m_kind = SyntaxKind.WhitespaceTrivia

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads single line comment. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadSingleLineComment()

            m_position += 2

            Dim done = False
            While Not done
                Select Case Current
                    Case ChrW(0), ChrW(13), ChrW(10)
                        done = True
                    Case Else
                        m_position += 1
                End Select
            End While

            m_kind = SyntaxKind.SingleLineCommentTrivia

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads multi line comment. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadMultiLineComment()

            m_position += 2

            Dim done = False
            While Not done
                Select Case Current
                    Case ChrW(0)
                        Dim span = New TextSpan(m_start, 2)
                        Dim location = New TextLocation(m_text, span)
                        Diagnostics.ReportUnterminatedMultiLineComment(location)
                        done = True
                    Case "*"c
                        If LookAhead = "/" Then
                            done = True
                            m_position += 1
                        End If
                        m_position += 1
                    Case Else
                        m_position += 1
                End Select
            End While

            m_kind = SyntaxKind.MultiLineCommentTrivia

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads the token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadToken()

            m_start = m_position
            m_kind = SyntaxKind.BadToken
            m_value = Nothing

            ' + Plus (Addition, String Concat)
            ' - Minus (Subtraction, Negation)
            ' * Multiply
            ' / Divide (Floating Point)
            ' \ Divide (Integer)
            ' ^ Raise Power
            ' : Command Separator

            Select Case Current
                Case ChrW(0) : m_kind = SyntaxKind.EndOfFileToken
                Case "+"c : m_kind = SyntaxKind.PlusToken : m_position += 1
                Case "-"c : m_kind = SyntaxKind.MinusToken : m_position += 1
                Case "*"c : m_kind = SyntaxKind.StarToken : m_position += 1
                Case "/"c : m_kind = SyntaxKind.SlashToken : m_position += 1
        'Case "\"c : m_kind = SyntaxKind.BackslashToken : m_position += 1
                Case "("c : m_kind = SyntaxKind.OpenParenToken : m_position += 1
                Case ")"c : m_kind = SyntaxKind.CloseParenToken : m_position += 1
                Case "{"c : m_kind = SyntaxKind.OpenBraceToken : m_position += 1
                Case "}"c : m_kind = SyntaxKind.CloseBraceToken : m_position += 1
                Case ":"c : m_kind = SyntaxKind.ColonToken : m_position += 1
                Case ","c : m_kind = SyntaxKind.CommaToken : m_position += 1
        'Case ";"c : m_kind = SyntaxKind.SemicolonToken : m_position += 1
                Case "~"c : m_kind = SyntaxKind.TildeToken : m_position += 1
        'Case "'"c : m_kind = SyntaxKind.ApostropheToken : m_position += 1
                Case "^"c : m_kind = SyntaxKind.HatToken : m_position += 1
                Case "&"c
                    If LookAhead = "&"c Then
                        m_kind = SyntaxKind.AmpersandAmpersandToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.AmpersandToken : m_position += 1
                    End If
                Case "|"c
                    If LookAhead = "|"c Then
                        m_kind = SyntaxKind.PipePipeToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.PipeToken : m_position += 1
                    End If
                Case "="c
                    If LookAhead = "="c Then
                        m_kind = SyntaxKind.EqualsEqualsToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.EqualsToken : m_position += 1
                    End If
                Case "!"c
                    If LookAhead = "="c Then
                        m_kind = SyntaxKind.BangEqualsToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.BangToken : m_position += 1
                    End If
                Case "<"c
                    'If LookAhead = ">"c Then
                    '  Kind = SyntaxKind.LessThanGreaterThanToken : Position += 2
                    If LookAhead = "="c Then
                        m_kind = SyntaxKind.LessThanEqualsToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.LessThanToken : m_position += 1
                    End If
                Case ">"c
                    If LookAhead = "="c Then
                        m_kind = SyntaxKind.GreaterThanEqualsToken : m_position += 2
                    Else
                        m_kind = SyntaxKind.GreaterThanToken : m_position += 1
                    End If
                Case ChrW(34)
                    ReadString()
                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                    ReadNumberToken()
                Case "_"c
                    ReadIdentifierOrKeyword()
                Case Else
                    If Char.IsLetter(Current) OrElse Current = "$"c Then
                        ReadIdentifierOrKeyword()
                    Else
                        Dim span = New TextSpan(m_position, 1)
                        Dim location = New TextLocation(m_text, span)
                        Diagnostics.ReportBadCharacter(location, Current)
                        m_position += 1
                    End If

            End Select

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads the string. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadString()

            ' "Test \" dddd"
            ' "Test "" dddd"

            ' skip the current quote
            m_position += 1

            Dim sb = New StringBuilder
            Dim done = False

            While Not done
                Select Case Current
                    Case ChrW(0), ChrW(13), ChrW(10)
                        Dim span = New TextSpan(m_start, 1)
                        Dim location = New TextLocation(m_text, span)
                        Diagnostics.ReportUnterminatedString(location)
                        done = True
                    Case """"c
                        If LookAhead = """"c Then
                            sb.Append(Current)
                            m_position += 2
                        Else
                            m_position += 1
                            done = True
                        End If
                    Case Else
                        sb.Append(Current)
                        m_position += 1
                End Select
            End While

            m_kind = SyntaxKind.StringToken
            m_value = sb.ToString

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads number token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadNumberToken()

            While Char.IsDigit(Current)
                m_position += 1
            End While

            Dim length = m_position - m_start
            Dim text = m_text.ToString(m_start, length)
            Dim value As Integer
            If Not Integer.TryParse(text, value) Then
                Dim location = New TextLocation(m_text, New TextSpan(m_start, length))
                Diagnostics.ReportInvalidNumber(location, text, TypeSymbol.Int)
            End If

            m_value = value
            m_kind = SyntaxKind.NumberToken

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reads identifier or keyword. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub ReadIdentifierOrKeyword()

      While Char.IsLetter(Current) OrElse Current = "_"c OrElse Current = "$"c
        m_position += 1
      End While

      Dim length = m_position - m_start
      Dim text = m_text.ToString(m_start, length)

      m_kind = SyntaxFacts.GetKeywordKind(text)

    End Sub

  End Class

End Namespace
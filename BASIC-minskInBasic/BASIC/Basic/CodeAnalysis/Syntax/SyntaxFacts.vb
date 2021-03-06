'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxFacts.vb
'
' summary:	Syntax facts class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices

Namespace Global.Basic.CodeAnalysis.Syntax

    Public Module SyntaxFacts

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets unary operator precedence. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The unary operator precedence. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension()>
    Public Function GetUnaryOperatorPrecedence(kind As SyntaxKind) As Integer

      Select Case kind
        Case SyntaxKind.PlusToken,
             SyntaxKind.MinusToken,
             SyntaxKind.BangToken,
             SyntaxKind.TildeToken
          Return 6

        Case Else
          Return 0
      End Select

    End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets binary operator precedence. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The binary operator precedence. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension()>
    Public Function GetBinaryOperatorPrecedence(kind As SyntaxKind) As Integer

      Select Case kind

        Case SyntaxKind.StarToken, SyntaxKind.SlashToken
          Return 5
        Case SyntaxKind.PlusToken, SyntaxKind.MinusToken
          Return 4

        Case SyntaxKind.EqualsEqualsToken,
             SyntaxKind.BangEqualsToken,
             SyntaxKind.LessThanToken,
             SyntaxKind.LessThanEqualsToken,
             SyntaxKind.GreaterThanEqualsToken,
             SyntaxKind.GreaterThanToken
          Return 3

        Case SyntaxKind.AmpersandToken,
             SyntaxKind.AmpersandAmpersandToken
          Return 2

        Case SyntaxKind.PipeToken,
             SyntaxKind.PipePipeToken,
             SyntaxKind.HatToken
          Return 1

        Case Else
          Return 0
      End Select

    End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets keyword kind. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        '''
        ''' <returns>   The keyword kind. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Function GetKeywordKind(text As String) As SyntaxKind

            Select Case text

                Case "true"
                    Return SyntaxKind.TrueKeyword
                Case "false"
                    Return SyntaxKind.FalseKeyword

                Case "let"
                    Return SyntaxKind.LetKeyword
                Case "return"
                    Return SyntaxKind.ReturnKeyword
                Case "var"
                    Return SyntaxKind.VarKeyword

                Case "function"
                    Return SyntaxKind.FunctionKeyword
                Case "if"
                    Return SyntaxKind.IfKeyword
        'Case "then"
        '  Return SyntaxKind.ThenKeyword
                Case "break"
                    Return SyntaxKind.BreakKeyword
                Case "continue"
                    Return SyntaxKind.ContinueKeyword
                Case "else"
                    Return SyntaxKind.ElseKeyword
        'Case "elseif"
        '  Return SyntaxKind.ElseIfKeyword
        'Case "endif"
        '  Return SyntaxKind.ElseIfKeyword

                Case "while"
                    Return SyntaxKind.WhileKeyword
                Case "do"
                    Return SyntaxKind.DoKeyword

                Case "for"
                    Return SyntaxKind.ForKeyword
                Case "to"
                    Return SyntaxKind.ToKeyword

                Case Else
                    Return SyntaxKind.IdentifierToken
            End Select

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the unary operator kinds in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the unary operator kinds in this
        ''' collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Iterator Function GetUnaryOperatorKinds() As IEnumerable(Of SyntaxKind)

            Dim kinds = DirectCast([Enum].GetValues(GetType(SyntaxKind)), SyntaxKind())

            For Each kind In kinds
                If GetUnaryOperatorPrecedence(kind) > 0 Then
                    Yield kind
                End If
            Next

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the binary operator kinds in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the binary operator kinds in this
        ''' collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Iterator Function GetBinaryOperatorKinds() As IEnumerable(Of SyntaxKind)

            Dim kinds = DirectCast([Enum].GetValues(GetType(SyntaxKind)), SyntaxKind())

            For Each kind In kinds
                If GetBinaryOperatorPrecedence(kind) > 0 Then
                    Yield kind
                End If
            Next

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets a text. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The text. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetText(kind As SyntaxKind) As String
            Select Case kind
                Case SyntaxKind.PlusToken : Return "+"
                Case SyntaxKind.MinusToken : Return "-"
                Case SyntaxKind.StarToken : Return "*"
                Case SyntaxKind.SlashToken : Return "/"
                Case SyntaxKind.BangToken : Return "!"
                Case SyntaxKind.EqualsToken : Return "="
                Case SyntaxKind.TildeToken : Return "~"
                Case SyntaxKind.LessThanToken : Return "<"
                Case SyntaxKind.LessThanEqualsToken : Return "<="
                Case SyntaxKind.GreaterThanToken : Return ">"
                Case SyntaxKind.GreaterThanEqualsToken : Return ">="
                Case SyntaxKind.AmpersandToken : Return "&"
                Case SyntaxKind.AmpersandAmpersandToken : Return "&&"
                Case SyntaxKind.PipeToken : Return "|"
                Case SyntaxKind.PipePipeToken : Return "||"
                Case SyntaxKind.HatToken : Return "^"
                Case SyntaxKind.EqualsEqualsToken : Return "=="
                Case SyntaxKind.BangEqualsToken : Return "!="
        'Case SyntaxKind.LessThanGreaterThanToken : Return "<>"
                Case SyntaxKind.OpenParenToken : Return "("
                Case SyntaxKind.CloseParenToken : Return ")"
                Case SyntaxKind.OpenBraceToken : Return "{"
                Case SyntaxKind.CloseBraceToken : Return "}"
                Case SyntaxKind.ColonToken : Return ":"
                Case SyntaxKind.CommaToken : Return ","
                Case SyntaxKind.BreakKeyword : Return "break"
                Case SyntaxKind.ContinueKeyword : Return "continue"
                Case SyntaxKind.ElseKeyword : Return "else"
                Case SyntaxKind.FalseKeyword : Return "false"
                Case SyntaxKind.ForKeyword : Return "for"
                Case SyntaxKind.FunctionKeyword : Return "function"
                Case SyntaxKind.IfKeyword : Return "if"
                Case SyntaxKind.LetKeyword : Return "let"
                Case SyntaxKind.ReturnKeyword : Return "return"
                Case SyntaxKind.ToKeyword : Return "to"
                Case SyntaxKind.TrueKeyword : Return "true"
                Case SyntaxKind.VarKeyword : Return "var"
                Case SyntaxKind.WhileKeyword : Return "while"
                Case SyntaxKind.DoKeyword : Return "do"

                    'Case SyntaxKind.NotKeyword : Return "not"
                    'Case SyntaxKind.AndKeyword : Return "and"
                    'Case SyntaxKind.AndAlsoKeyword : Return "andalso"
                    'Case SyntaxKind.OrKeyword : Return "or"
                    'Case SyntaxKind.OrElseKeyword : Return "orelse"
                    'Case SyntaxKind.DimKeyword : Return "dim"

                Case Else
                    Return Nothing
            End Select

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is comment. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if comment, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Function IsComment(kind As SyntaxKind) As Boolean
      Select Case kind
        Case SyntaxKind.SingleLineCommentTrivia,
             SyntaxKind.MultiLineCommentTrivia
          Return True
        Case Else
          Return False
      End Select
    End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is trivia. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if trivia, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Function IsTrivia(kind As SyntaxKind) As Boolean
      Select Case kind
        Case SyntaxKind.SkippedTextTrivia,
             SyntaxKind.LineBreakTrivia,
             SyntaxKind.WhitespaceTrivia,
             SyntaxKind.SingleLineCommentTrivia,
             SyntaxKind.MultiLineCommentTrivia
          Return True
        Case Else
          Return False
      End Select
    End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is keyword. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if keyword, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Function IsKeyword(kind As SyntaxKind) As Boolean
      Return kind.ToString.EndsWith("Keyword")
    End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if token, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Function IsToken(kind As SyntaxKind) As Boolean
      Return Not kind.IsTrivia AndAlso
             (kind.IsKeyword OrElse kind.ToString.EndsWith("Token"))
    End Function

  End Module

End Namespace
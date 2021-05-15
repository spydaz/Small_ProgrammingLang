
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Windows.Forms
Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler
        Friend Class Lexer
            Public _Diagnostics As New List(Of String)
            Private _Script As String = ""
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private CursorPosition As Integer = 0
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private EoFCursor As Integer = 0
            Private ReadOnly Property Lookahead As String
                Get
                    If CursorPosition + 2 <= EoFCursor Then
                        Return GetSlice(CursorPosition + 1, 1)
                    Else
                        Return Nothing
                    End If

                End Get
            End Property
            Private ReadOnly Property CurrentChar As Char
                Get
                    If CursorPosition >= EoFCursor Then
                        Return "@"
                    Else
                        Return _Script(CursorPosition)
                    End If
                End Get
            End Property
            Public ReadOnly Property EndOfFile As Boolean
                Get
                    If CursorPosition >= EoFCursor Then
                        Return True
                    Else
                        Return False
                    End If
                End Get
            End Property
            Public CurrentGrammar As List(Of GrammarDefinintion)
            ''' <summary>
            ''' Advances Cursor position to next Char
            ''' </summary>
            Public Sub New(ByRef iScript As String)
                _Script = iScript
                EoFCursor = _Script.Length
            End Sub
            ''' <summary>
            ''' Next Char
            ''' </summary>
            Public Sub _NextChar()
                CursorPosition += 1
            End Sub
            ''' <summary>
            ''' TOKENIZING BY CHAR (MAINFUNCTION)
            ''' 
            '''-Numerical
            '''-WhiteSpace
            '''-Operators +-*/()
            ''' </summary>
            ''' <returns></returns>
            Public Function _NextToken() As SyntaxToken
                '-Numerical
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""

                'WhiteSpace
                If Char.IsWhiteSpace(CurrentChar) Then
                    Return ReadWhiteSpace()
                End If
                'End of file
                If EndOfFile = True Then
                    ' Return New SyntaxToken(SyntaxType._EndOfFileToken, "EOF", CursorPosition - 1, "EOF", Nothing)
                    Return New SyntaxToken(SyntaxType._EndOfFileToken, "_EndOfFileToken", Nothing, Nothing, CursorPosition, CursorPosition)
                End If

                'Numerical
                If Char.IsDigit(CurrentChar) Then
                    Return ReadNumberToken()
                Else
                End If

                'Operators
                Select Case CurrentChar
                    Case ChrW(10), ChrW(13)
                        Return New SyntaxToken(SyntaxType._EndOfLineToken, SyntaxType._EndOfLineToken.GetSyntaxTypeStr, "EOL", "EOL", _start, CursorPosition)
                    Case ","c
                        Return New SyntaxToken(SyntaxType._LIST_SEPERATOR, SyntaxType._LIST_SEPERATOR.GetSyntaxTypeStr, ",", ",", _start, CursorPosition)
                    Case ";"c
                        Return New SyntaxToken(SyntaxType._STATEMENT_END, SyntaxType._STATEMENT_END.GetSyntaxTypeStr, ";", ";", _start, CursorPosition)
                    Case "{"c
                        _start = CursorPosition
                        _length = 1
                        CursorPosition += _length
                        Return New SyntaxToken(SyntaxType._CODE_BEGIN, SyntaxType._CODE_BEGIN.GetSyntaxTypeStr, "{", "{", _start, CursorPosition)


                    Case "}"c
                        _start = CursorPosition
                        _length = 1
                        CursorPosition += _length
                        Return New SyntaxToken(SyntaxType._CODE_END, SyntaxType._CODE_END.GetSyntaxTypeStr, "}", "}", _start, CursorPosition)


                    Case "+"
                        If Lookahead = "=" Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.Add_Equals_Operator, SyntaxType.Add_Equals_Operator.GetSyntaxTypeStr, "+=", "+=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length

                            Return New SyntaxToken(SyntaxType.Add_Operator, SyntaxType.Add_Operator.GetSyntaxTypeStr, "+", "+", _start, _start + _length)

                        End If
                    Case "-"
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.Minus_Equals_Operator, SyntaxType.Minus_Equals_Operator.GetSyntaxTypeStr, "-=", "-=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length

                            Return New SyntaxToken(SyntaxType.Sub_Operator, SyntaxType.Sub_Operator.GetSyntaxTypeStr, "-", "-", _start, _start + _length)

                        End If
                    Case "*"
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.Multiply_Equals_Operator, SyntaxType.Multiply_Equals_Operator.GetSyntaxTypeStr, "*=", "*=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length

                            Return New SyntaxToken(SyntaxType.Multiply_Operator, SyntaxType.Multiply_Operator.GetSyntaxTypeStr, "*", "*", _start, _start + _length)

                        End If
                    Case "/"
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.Divide_Equals_Operator, SyntaxType.Divide_Equals_Operator.GetSyntaxTypeStr, "/=", "/=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length

                            Return New SyntaxToken(SyntaxType.Divide_Operator, SyntaxType.Divide_Operator.GetSyntaxTypeStr, "/", "/", _start, _start + _length)

                        End If
                    Case ")"
                        _start = CursorPosition
                        _length = 1
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType._RightParenthes, "_RightParenthes", ")", ")", _start, _start + _length)

                    Case "("
                        _start = CursorPosition
                        _length = 1
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType._leftParenthes, "_leftParenthes", "(", "(", _start, _start + _length)
                    Case "="c
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.EquivelentTo, "EquivelentTo", "==", "==", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType._ASSIGN, "_ASSIGN", "=", "=", _start, _start + _length)

                        End If
                    Case "!"c
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.NotEqual, SyntaxType.NotEqual.GetSyntaxTypeStr, "!=", "!=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType._Not, SyntaxType._Not.GetSyntaxTypeStr, "!", "!", _start, _start + _length)

                        End If
                    Case "<"c
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.LessThanEquals, SyntaxType.GreaterThanEquals.GetSyntaxTypeStr, "<=", "<=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.LessThanOperator, SyntaxType.LessThanOperator.GetSyntaxTypeStr, "<", "<", _start, _start + _length)

                        End If
                    Case ">"c
                        If Lookahead = "="c Then
                            _start = CursorPosition
                            _length = 2
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.GreaterThanEquals, SyntaxType.GreaterThanEquals.GetSyntaxTypeStr, ">=", ">=", _start, _start + _length)

                        Else
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType.GreaterThan_Operator, SyntaxType.GreaterThan_Operator.GetSyntaxTypeStr, ">", ">", _start, _start + _length)

                        End If
                End Select

                'Identifers
                If Char.IsLetter(CurrentChar) Then
                    Return ReadIdentifierOrKeyword()
                Else
                End If

                If CurrentChar = ChrW(34) Then
                    Return ReadString()
                End If

                If CurrentChar = "@" Then
                    Return New SyntaxToken(SyntaxType._EndOfFileToken, SyntaxType._EndOfFileToken.GetSyntaxTypeStr, "EOF", "EOF", _start, _start + _length)

                Else
                End If
                Dim itext = CurrentChar
                CursorPosition += 1

                Dim x = New SyntaxToken(SyntaxType._UnknownToken, SyntaxType._UnknownToken.GetSyntaxTypeStr, itext, itext, CursorPosition, CursorPosition)
                _Diagnostics.Add("Unrecognized Character in input: '" & vbNewLine & x.ToJson)
                Return x
            End Function
            Public Function ReadWhiteSpace() As SyntaxToken
                '-Numerical
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""
                'Capture StartPoint ForSlicer
                _start = CursorPosition
                'UseInternal Lexer PER (CHAR)
                While Char.IsWhiteSpace(CurrentChar)
                    'Iterate Forwards until end of digits
                    _NextChar()
                End While
                'get length 
                _length = CursorPosition - _start
                'Get Slice
                _iText = _Script.Substring(_start, _length)
                Return New SyntaxToken(SyntaxType._WhitespaceToken, SyntaxType._WhitespaceToken.GetSyntaxTypeStr, _iText, _iText, _start, _start - _length)
            End Function
            Public Function ReadIdentifierOrKeyword() As SyntaxToken
                '-Numerical
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""
                'Identifers
                If Char.IsLetter(CurrentChar) Then
                    'Capture StartPoint ForSlicer
                    _start = CursorPosition
                    'UseInternal Lexer PER (CHAR)
                    While Char.IsLetter(CurrentChar)
                        'Iterate Forwards until end of Letters
                        _NextChar()
                    End While
                    'get length 
                    _length = CursorPosition - _start
                    'Get Slice
                    _iText = _Script.Substring(_start, _length)

                    _iText.GetKeywordSyntaxType()
                    Return New SyntaxToken(_iText.GetKeywordSyntaxType(), _iText.GetKeywordSyntaxType.GetSyntaxTypeStr, _iText, _iText, _start, _start + _length)

                End If
                Return Nothing
            End Function
            Public Function ReadNumberToken()
                '-Numerical
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""
                If Char.IsDigit(CurrentChar) Then
                    'Capture StartPoint ForSlicer
                    _start = CursorPosition
                    'UseInternal Lexer PER (CHAR)
                    While Char.IsDigit(CurrentChar)
                        'Iterate Forwards until end of digits
                        _NextChar()
                    End While

                    'get length 
                    _length = CursorPosition - _start
                    'Get Slice
                    _iText = _Script.Substring(_start, _length)

                    Dim value As Integer = 0
                    If Integer.TryParse(_iText, value) = False Then
                        _Diagnostics.Add("The number is not recognized as integer:" & _iText)
                    End If
                    Return New SyntaxToken(SyntaxType._Integer, SyntaxType._Integer.GetSyntaxTypeStr, _iText, value, _start, _start + _length)

                End If
                Return Nothing
            End Function
            Private Function ReadString() As SyntaxToken
                '-string
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""
                ' "Test \" dddd"
                ' "Test "" dddd"

                ' skip the current quote
                CursorPosition += 1
                'Capture StartPoint ForSlicer
                _start = CursorPosition
                Dim sb = New StringBuilder
                Dim done = False

                While Not done
                    Select Case CurrentChar
                        Case ChrW(0), ChrW(13), ChrW(10)


                            done = True
                        Case """"c
                            If GetSlice(CursorPosition, 1) = """"c Then
                                '  sb.Append(CurrentChar)
                                _length = CursorPosition - _start
                                CursorPosition += 2
                                done = True
                            Else
                                'get length 
                                _length = CursorPosition - _start
                                CursorPosition += 1

                                done = True
                            End If
                        Case Else
                            sb.Append(CurrentChar)
                            CursorPosition += 1
                    End Select
                End While


                _iText = sb.ToString
                Return New SyntaxToken(SyntaxType._String, SyntaxType._String.GetSyntaxTypeStr, _iText, _iText, _start, _start + _length)

            End Function
            ''' <summary>
            ''' Checks token without moving the cursor
            ''' </summary>
            ''' <returns></returns>
            Public Function ViewNextSlice() As String
                If EndOfFile = False Then
                    Dim slice = GetSlice(Me._Script, Me.CursorPosition)
                    If slice IsNot Nothing Then
                        Return slice
                    Else
                        'Does not change anything
                        Return "EOF"
                    End If
                Else
                    Return "EOF"
                End If
            End Function
            ''' <summary>
            ''' BY REGEX
            ''' 
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function GetIdentifiedToken(ByRef CurrentTok As String) As SyntaxToken


                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.SearchPattern)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New SyntaxToken
                        tok._SyntaxType = item.Identifer
                        tok._Value = matches(0)
                        tok._start = CursorPosition
                        tok._End = CursorPosition + tok._Value.Length
                        CursorPosition = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New SyntaxToken
                btok._SyntaxType = SyntaxType._UnknownToken
                btok._Value = CurrentTok
                btok._start = CursorPosition
                btok._End = CursorPosition + _Script.Length
                CursorPosition = EoFCursor
                _Diagnostics.Add("Unrecognized token in input: '" & CurrentChar & "' at Position : " & CursorPosition - 1)

                Return btok

            End Function
            ''' <summary>
            ''' 
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function CheckIdentifiedToken(ByRef CurrentTok As String) As SyntaxToken

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.SearchPattern)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New SyntaxToken
                        tok._SyntaxType = item.Identifer
                        tok._Value = matches(0)
                        tok._start = CursorPosition
                        tok._End = CursorPosition + tok._Value.Length
                        ' Cursor = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New SyntaxToken
                btok._SyntaxType = SyntaxType._UnknownToken
                btok._Value = CurrentTok
                btok._start = CursorPosition
                btok._End = CursorPosition + _Script.Length
                _Diagnostics.Add("Unrecognized token in input: '" & CurrentChar & "' at Position : " & CursorPosition - 1)

                Return btok

            End Function
            ''' <summary>
            ''' Returns from index to end of file (Universal function)
            ''' </summary>
            ''' <param name="Str">String</param>
            ''' <param name="indx">Index</param>
            ''' <returns></returns>
            Public Shared Function GetSlice(ByRef Str As String, ByRef indx As Integer) As String
                If indx <= Str.Length Then
                    Str.Substring(indx)
                    Return Str.Substring(indx)
                Else
                End If
                Return Nothing
            End Function
            ''' <summary>
            ''' Gets Slice from the script
            ''' </summary>
            ''' <param name="_start"></param>
            ''' <param name="_length"></param>
            ''' <returns></returns>
            Public Function GetSlice(ByRef _start As Integer, ByRef _length As Integer) As String
                Return _Script.Substring(_start, _length)
            End Function
            ''' <summary>
            ''' Main Searcher
            ''' </summary>
            ''' <param name="Text">to be searched </param>
            ''' <param name="Pattern">RegEx Search String</param>
            ''' <returns></returns>
            Public Shared Function RegExSearch(ByRef Text As String, Pattern As String) As List(Of String)
                Dim Searcher As New Regex(Pattern)
                Dim iMatch As Match = Searcher.Match(Text)
                Dim iMatches As New List(Of String)
                Do While iMatch.Success
                    iMatches.Add(iMatch.Value)
                    iMatch = iMatch.NextMatch
                Loop
                Return iMatches
            End Function

        End Class
    End Namespace
End Namespace


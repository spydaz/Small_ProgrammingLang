
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Windows.Forms
Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler
        Public Class Lexer
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
            Private ReadOnly Property CurrentChar As Char
                Get
                    If CursorPosition > EoFCursor Then
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

                If EndOfFile = True Then
                    ' Return New SyntaxToken(SyntaxType._EndOfFileToken, "EOF", CursorPosition - 1, "EOF", Nothing)
                    Return New SyntaxToken(SyntaxType._EndOfFileToken, "_EndOfFileToken", Nothing, Nothing, CursorPosition, CursorPosition)
                End If

                'Numerical
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
                    Return New SyntaxToken(SyntaxType._Integer, "Numerical", _iText, value, _start, CursorPosition)

                End If

                'WhiteSpace
                If Char.IsWhiteSpace(CurrentChar) Then
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
                    Return New SyntaxToken(SyntaxType._WhitespaceToken, "WhiteSpace", "", "", _start, CursorPosition)
                End If

                'Operators
                Select Case CurrentChar
                    Case ChrW(10), ChrW(13)
                        Return New SyntaxToken(SyntaxType._EndOfLineToken, "EOL", "", "", _start, CursorPosition)

                    Case "+"
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType.Add_Operator, "Add_Operator", "+", "ADD", _start, CursorPosition)
                    Case "-"
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType.Sub_Operator, "Sub_Operator", "-", "SUB", _start, CursorPosition)

                    Case "*"
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType.Multiply_Operator, "Multiply_Operator", "*", "MUL", _start, CursorPosition)

                    Case "/"
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType.Divide_Operator, "Divide_Operator", "/", "DIV", _start, CursorPosition)

                    Case ")"
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType._RightParenthes, "_RightParenthes", ")", ")", _start, CursorPosition)

                    Case "("
                        CursorPosition += 1
                        Return New SyntaxToken(SyntaxType._leftParenthes, "_leftParenthes", "(", ")", _start, CursorPosition)

                End Select


                CursorPosition += 1
                _Diagnostics.Add("Unrecognized Character in input: '" & CurrentChar & "' at Position : " & CursorPosition - 1)
                Return New SyntaxToken(SyntaxType._UnknownToken, "_UnknownToken", _iText, _iText, _start, CursorPosition)

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
            Public Shared Function LexTokens(ByRef Line As String) As List(Of SyntaxToken)
                Dim Itok As SyntaxToken
                'Original Tokenizer
                Dim SAL_VB_LEXER As New Lexer(Line)
                Dim lst As New List(Of SyntaxToken)
                While True
                    Itok = SAL_VB_LEXER._NextToken
                    If Itok._SyntaxType = SyntaxType._EndOfFileToken Then

                        Exit While

                    Else
                        ' Console.WriteLine(vbNewLine & "Tokens> " & vbNewLine & "Text: " & Itok._Text & vbNewLine & "Type: " & Itok._SyntaxStr & vbNewLine)
                        lst.Add(Itok)
                    End If
                End While
                'TOKENIZER DIAGNOSTICS
                If SAL_VB_LEXER._Diagnostics.Count > 0 Then
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("Tokenizer Error:" & vbNewLine)
                    For Each item In SAL_VB_LEXER._Diagnostics

                        Console.WriteLine(item & vbNewLine)

                    Next
                    'Enable wait
                    Dim UserInput_LINE = Console.ReadLine()
                End If
                Return lst
            End Function
        End Class
    End Namespace
End Namespace


﻿'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Tokenizer\Lexer.vb
'
' summary:	Lexer class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Tokenizer

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   A lexer. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Friend Class Lexer

#Region "Propertys"
                ''' <summary>   The lexer diagnostics 
                '''             Execptions Generated by the class. </summary>
                Public LexerDiagnostics = New List(Of DiagnosticsException)
                ''' <summary>   The diagnostics. 
                '''             Old version To be replaced</summary>
                Public _Diagnostics As New List(Of String)
                ''' <summary>   The script. 
                '''             The current program script</summary>
                Private _Script As String = ""
                ''' <summary>
                ''' Cursor Position
                ''' </summary>
                Private CursorPosition As Integer = 0
                ''' <summary>
                ''' Cursor Position
                ''' </summary>
                Private EoFCursor As Integer = 0

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the lookahead.Token. (Not using The regEx) </summary>
                '''
                ''' <value> The lookahead. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private ReadOnly Property Lookahead As String
                    Get
                        If CursorPosition + 2 <= EoFCursor Then
                            Return GetSlice(CursorPosition + 1, 1)
                        Else
                            Return Nothing
                        End If

                    End Get
                End Property

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the current character. </summary>
                '''
                ''' <value> The current character. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private ReadOnly Property CurrentChar As Char
                    Get
                        If CursorPosition >= EoFCursor Then
                            Return "@"
                        Else
                            Return _Script(CursorPosition)
                        End If
                    End Get
                End Property

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the end of file. </summary>
                '''
                ''' <value> The end of file. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public ReadOnly Property EndOfFile As Boolean
                    Get
                        If CursorPosition >= EoFCursor Then
                            Return True
                        Else
                            Return False
                        End If
                    End Get
                End Property
                ''' <summary>   The current grammar. 
                '''             The current grammar in use by the RegEx Tokenizer
                '''             It is prudent to load a grammar as well as relying on the internal
                '''             Basic compiler; which attempts to parse all possible tokens to a tree
                '''             Specific words will need to be reidentifed as IDENTIFIERS</summary>
                Public CurrentGrammar As List(Of GrammarDefinintion)
#End Region

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Constructor. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="iScript">  [in,out] Zero-based index of the script. </param>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub New(ByRef iScript As String)
                    _Script = iScript
                    EoFCursor = _Script.Length
                End Sub

#Region "Char Scanner"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Next character. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Sub _NextChar()
                    CursorPosition += 1
                End Sub

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Next token. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   A SyntaxToken. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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
                        Case "{"
                            _start = CursorPosition
                            _length = 1
                            CursorPosition += _length
                            Return New SyntaxToken(SyntaxType._CODE_BEGIN, SyntaxType._CODE_BEGIN.GetSyntaxTypeStr, "{", "{", _start, CursorPosition)


                        Case "}"
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
                        Case "<"c
                            If Lookahead = ">"c Then
                                _start = CursorPosition
                                _length = 2
                                CursorPosition += _length
                                Return New SyntaxToken(SyntaxType.NotEqual, SyntaxType.NotEqual.GetSyntaxTypeStr, "!=", "!=", _start, _start + _length)

                            Else

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

                    'String
                    If CurrentChar = ChrW(34) Then
                        Return ReadString()
                    End If
                    'eofToken
                    If CurrentChar = "@" Then
                        Return New SyntaxToken(SyntaxType._EndOfFileToken, SyntaxType._EndOfFileToken.GetSyntaxTypeStr, "EOF", "EOF", _start, _start + _length)

                    Else
                    End If

                    Dim itext = CurrentChar
                    CursorPosition += 1
                    Dim x = New SyntaxToken(SyntaxType._UnknownToken, SyntaxType._UnknownToken.GetSyntaxTypeStr, itext, itext, CursorPosition, CursorPosition)
                    Dim Err As New DiagnosticsException("Unrecognized Character in input: '", ExceptionType.UnknownTokenError, x.ToJson, SyntaxType._UnknownToken)
                    LexerDiagnostics.add(Err)

                    Return x
                End Function
#End Region
#Region "Mini Tokenizers"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Reads white space. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The white space. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Reads identifiers or keywords. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The identifier or keyword. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Reads number tokens. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The number token. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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
                            Dim x = New SyntaxToken(SyntaxType._UnknownToken, SyntaxType._UnknownToken.GetSyntaxTypeStr, _iText, _iText, _start, _start + _length)
                            Dim Err As New DiagnosticsException("The number is not recognized as integer:", ExceptionType.UnknownTokenError, x.ToJson, SyntaxType._UnknownToken)
                            LexerDiagnostics.add(Err)

                        End If
                        Return New SyntaxToken(SyntaxType._Integer, SyntaxType._Integer.GetSyntaxTypeStr, _iText, value, _start, _start + _length)

                    End If
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Reads Identifed tokens (Strings) . </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The string. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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
#End Region
#Region "ReGeX Tokenizer"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   View next slice. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   A String. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets identified token. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="CurrentTok">   [in,out]. </param>
                '''
                ''' <returns>   The identified token. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Check identified token. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="CurrentTok">   [in,out]. </param>
                '''
                ''' <returns>   A SyntaxToken. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets Slice from the script. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Str">  [in,out] The string. </param>
                ''' <param name="indx"> [in,out] The indx. </param>
                '''
                ''' <returns>   The slice. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Shared Function GetSlice(ByRef Str As String, ByRef indx As Integer) As String
                    If indx <= Str.Length Then
                        Str.Substring(indx)
                        Return Str.Substring(indx)
                    Else
                    End If
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Returns from index to end of file (Universal function) </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="_start">   [in,out] The start. </param>
                ''' <param name="_length">  [in,out] The length. </param>
                '''
                ''' <returns>   The slice. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function GetSlice(ByRef _start As Integer, ByRef _length As Integer) As String
                    Return _Script.Substring(_start, _length)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   RegEx search. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Text">     [in,out] The text. </param>
                ''' <param name="Pattern">  Specifies the pattern. </param>
                '''
                ''' <returns>   A List(Of String) </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

#End Region
#Region "TOSTRING"

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Converts this  to a JSON. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   This  as a String. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function ToJson() As String
                    Return FormatJsonOutput(ToString)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Returns a string that represents the current object. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   A string that represents the current object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Overrides Function ToString() As String
                    Dim Converter As New JavaScriptSerializer
                    Return Converter.Serialize(Me)
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Format JSON output. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <param name="jsonString">   The JSON string. </param>
                '''
                ''' <returns>   The formatted JSON output. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function FormatJsonOutput(ByVal jsonString As String) As String
                    Dim stringBuilder = New StringBuilder()
                    Dim escaping As Boolean = False
                    Dim inQuotes As Boolean = False
                    Dim indentation As Integer = 0

                    For Each character As Char In jsonString

                        If escaping Then
                            escaping = False
                            stringBuilder.Append(character)
                        Else

                            If character = "\"c Then
                                escaping = True
                                stringBuilder.Append(character)
                            ElseIf character = """"c Then
                                inQuotes = Not inQuotes
                                stringBuilder.Append(character)
                            ElseIf Not inQuotes Then

                                If character = ","c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, indentation)
                                ElseIf character = "["c OrElse character = "{"c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                                ElseIf character = "]"c OrElse character = "}"c Then
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                                    stringBuilder.Append(character)
                                ElseIf character = ":"c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbTab)
                                ElseIf Not Char.IsWhiteSpace(character) Then
                                    stringBuilder.Append(character)
                                End If
                            Else
                                stringBuilder.Append(character)
                            End If
                        End If
                    Next

                    Return stringBuilder.ToString()
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets debugger display. </summary>
                '''
                ''' <remarks>   Leroy, 24/05/2021. </remarks>
                '''
                ''' <returns>   The debugger display. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
#End Region
            End Class
        End Namespace

    End Namespace
End Namespace


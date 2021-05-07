Imports System.Text.RegularExpressions
Imports SDK.SmallProgLang.GrammarFactory
Imports SDK.SmallProgLang.GrammarFactory.Grammar

Namespace SmallProgLang
    Namespace Compiler
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Lexer
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private Cursor As Integer = 0
            ''' <summary>
            ''' Cursor Position
            ''' </summary>
            Private EoFCursor As Integer = 0
            ''' <summary>
            ''' Program being Tokenized
            ''' </summary>
            Private CurrentScript As String = ""
            Private iPastTokens As New List(Of Token)
            Private iLastToken As Token
            Public ReadOnly Property PastTokens As List(Of Token)
                Get
                    Return iPastTokens
                End Get
            End Property
            Private Property LastToken As Token
                Get
                    Return iLastToken
                End Get
                Set(value As Token)
                    iLastToken = value
                    iPastTokens.Add(value)
                End Set
            End Property
            Public Function GetLastToken() As Token
                Return LastToken
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
            Public ReadOnly Property EndOfFile As Boolean
                Get
                    If Cursor >= EoFCursor Then
                        Return True
                    Else
                        Return False
                    End If
                End Get

            End Property
            ''' <summary>
            ''' Gets next token moves cursor forwards
            ''' </summary>
            ''' <returns></returns>
            Public Function GetNext() As String
                If EndOfFile = False Then
                    Dim slice = GetSlice(Me.CurrentScript, Me.Cursor)
                    If slice IsNot Nothing Then
                        Cursor += slice.Length
                        Return slice

                    Else
                        'Errors jump straight to enod of file
                        Cursor = EoFCursor
                        Return "EOF"
                    End If
                Else
                    Return "EOF"
                End If
            End Function
            ''' <summary>
            ''' Checks token without moving the cursor
            ''' </summary>
            ''' <returns></returns>
            Public Function ViewNext() As String
                If EndOfFile = False Then
                    Dim slice = GetSlice(Me.CurrentScript, Me.Cursor)
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
            ''' Main Searcher
            ''' </summary>
            ''' <param name="Text">to be searched </param>
            ''' <param name="Pattern">RegEx Search String</param>
            ''' <returns></returns>
            Private Shared Function RegExSearch(ByRef Text As String, Pattern As String) As List(Of String)
                Dim Searcher As New Regex(Pattern)
                Dim iMatch As Match = Searcher.Match(Text)
                Dim iMatches As New List(Of String)
                Do While iMatch.Success
                    iMatches.Add(iMatch.Value)
                    iMatch = iMatch.NextMatch
                Loop
                Return iMatches
            End Function
            ''' <summary>
            ''' Steps the tokenizer backwards
            ''' </summary>
            ''' <param name="TokenStr"></param>
            Public Sub StepBack(ByRef TokenStr As String)
                'If last value was the same then do.
                If TokenStr = LastToken.Value Then
                    Try
                        Cursor = Cursor - TokenStr.Length
                        'Removes last token
                        iPastTokens.RemoveAt(PastTokens.Count - 1)
                        'sets last token to newlast on the list
                        iLastToken = PastTokens(PastTokens.Count - 1)
                    Catch ex As Exception
                        'Error (cant go back)
                    End Try
                Else
                    'Was not the last value in stack
                End If


            End Sub
            Public Function Eat(ByRef TokenType As Type_Id) As Token
                Dim Strt As Integer = Cursor
                If IdentifiyToken(ViewNext()) = TokenType Then

                    'With return "" if nothing is detected
                    Dim str As String = ""
                    'Build token until token is not the type (if token is of type then add it)

                    'Get next advances the cursor
                    str &= GetNext()

                    Dim _end As Integer = Cursor
                    Dim Toke As New Token
                    Toke.ID = TokenType
                    Toke.Value = str
                    Toke._start = Strt
                    Toke._End = _end
                    'Preserve token returned
                    LastToken = Toke

                    Return Toke
                Else
                    'Not match tokentype
                    Return Nothing
                End If

            End Function
            ''' <summary>
            ''' Identifys token but due to some tokens maybe cross identifying 
            ''' CheckIdentifiedToken will return the full token without moving the cursor
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function IdentifiyToken(ByRef CurrentTok As String) As GrammarFactory.Grammar.Type_Id

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then



                        Return item.ID
                    Else
                        'Check next
                    End If
                Next

                Return Type_Id._BAD_TOKEN
            End Function
            ''' <summary>
            ''' Identifys token
            ''' </summary>
            ''' <param name="CurrentTok"></param>
            ''' <returns></returns>
            Public Function GetIdentifiedToken(ByRef CurrentTok As String) As Token

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New Token
                        tok.ID = item.ID
                        tok.Value = matches(0)
                        tok._start = Cursor
                        tok._End = Cursor + tok.Value.Length
                        Cursor = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New Token
                btok.ID = Type_Id._BAD_TOKEN
                btok.Value = CurrentTok
                btok._start = Cursor
                btok._End = Cursor + CurrentScript.Length
                Cursor = EoFCursor
                Return btok

            End Function
            Public Function CheckIdentifiedToken(ByRef CurrentTok As String) As Token

                For Each item In CurrentGrammar
                    Dim matches = RegExSearch(CurrentTok, item.Exp)
                    If matches IsNot Nothing And matches.Count > 0 Then
                        Dim tok As New Token
                        tok.ID = item.ID
                        tok.Value = matches(0)
                        tok._start = Cursor
                        tok._End = Cursor + tok.Value.Length
                        ' Cursor = tok._End
                        Return tok
                    Else
                        'Check next
                    End If
                Next
                'Match not found bad token
                Dim btok As New Token
                btok.ID = Type_Id._BAD_TOKEN
                btok.Value = CurrentTok
                btok._start = Cursor
                btok._End = Cursor + CurrentScript.Length

                Return btok

            End Function
            Public CurrentGrammar As List(Of GrammarFactory.Grammar)
            Public Sub New(ByRef Script As String)
                Me.CurrentScript = Script
                EoFCursor = Script.Length
                CurrentGrammar = GrammarFactory.Grammar.GetExtendedGrammar
            End Sub
            ''' <summary>
            ''' Use for Sal and OtherLangs
            ''' </summary>
            ''' <param name="Script"></param>
            ''' <param name="Grammar"></param>
            Public Sub New(ByRef Script As String, ByRef Grammar As List(Of GrammarFactory.Grammar))
                Me.CurrentScript = Script
                EoFCursor = Script.Length
                CurrentGrammar = Grammar
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace
End Namespace

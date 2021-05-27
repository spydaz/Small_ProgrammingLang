'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\UserControls\ClassSyntaxTextbox.vb
'
' summary:	Class syntax textbox class
'---------------------------------------------------------------------------------------------------

Imports System.Drawing
Imports System.Windows.Forms
Imports System.String
Imports System.Text

'''////////////////////////////////////////////////////////////////////////////////////////////////////
''' <summary>   A syntax text box. </summary>
'''
''' <remarks>   Leroy, 27/05/2021. </remarks>
'''////////////////////////////////////////////////////////////////////////////////////////////////////

Public Class SyntaxTextBox
    Inherits RichTextBox
    ''' <summary>   True to im adding. </summary>
    Private ImAdding As Boolean = False
    ''' <summary>   True to im done. </summary>
    Private ImDone As Boolean = False
    ''' <summary>   True if ive been clicked. </summary>
    Private IveBeenClicked As Boolean = False
    ''' <summary>   The terabytes. </summary>
    Private tb As SyntaxTextBox = Me
    ''' <summary>   The new word. </summary>
    Private newWord As String
    ''' <summary>   The CMS drop down. </summary>
    Private cmsDropDown As New ContextMenuStrip
    ''' <summary>   The suggestions. </summary>
    Dim Suggestions As IEnumerable(Of String)
    ''' <summary>   The last word. </summary>
    Dim lastWord As String = ""
    ''' <summary>   The last index. </summary>
    Dim LastIndex As Integer = 0
    ''' <summary>   Zero-based index of the syntax. </summary>

    Private iSyntax As New List(Of String)

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Gets or sets the syntax. </summary>
    '''
    ''' <value> The syntax. </value>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Property Syntax As List(Of String)
        Get
            Return iSyntax
        End Get
        Set(value As List(Of String))
            iSyntax = value
        End Set
    End Property

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Highlight term. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="Str">      [in,out] The string. </param>
    ''' <param name="Color">    [in,out] The color. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub HighlightTerm(ByRef Str As String, ByRef Color As Drawing.Color)
        Highlighter.ColorSearchTerm(Str, Me, Color)
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Highlight term. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="Str">  [in,out] The string. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub HighlightTerm(ByRef Str As String)
        Highlighter.HighlightWord(Me, Str)
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Highlight terms. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="Str">  [in,out] The string. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub HighlightTerms(ByRef Str As List(Of String))

        For Each item In Str
            Highlighter.HighlightWord(Me, item)
        Next


    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A highlighter. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Class Highlighter

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Proper case. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="TheString">    [in,out] the string. </param>
        '''
        ''' <returns>   A String. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Shared Function ProperCase(ByRef TheString As String) As String
            ProperCase = UCase(Strings.Left(TheString, 1))

            For i = 2 To Len(TheString)

                ProperCase = If(Mid(TheString, i - 1, 1) = " ", ProperCase & UCase(Mid(TheString, i, 1)), ProperCase & LCase(Mid(TheString, i, 1)))
            Next i
        End Function
        ''' <summary>   The syntax terms. </summary>
        Public Shared SyntaxTerms() As String = ({"SPYDAZ", "ABS", "ACCESS", "ADDITEM", "ADDNEW", "ALIAS", "AND", "ANY", "APP", "APPACTIVATE", "APPEND", "APPENDCHUNK", "ARRANGE", "AS", "ASC", "ATN", "BASE", "BEEP", "BEGINTRANS", "BINARY", "BYVAL", "CALL", "CASE", "CCUR", "CDBL", "CHDIR", "CHDRIVE", "CHR", "CHR$", "CINT", "CIRCLE", "CLEAR", "CLIPBOARD", "CLNG", "CLOSE", "CLS", "COMMAND", "
COMMAND$", "COMMITTRANS", "COMPARE", "CONST", "CONTROL", "CONTROLS", "COS", "CREATEDYNASET", "CSNG", "CSTR", "CURDIR$", "CURRENCY", "CVAR", "CVDATE", "DATA", "DATE", "DATE$", "DIM", "DATESERIAL", "DATEVALUE", "DAY", "
DEBUG", "DECLARE", "DEFCUR", "CEFDBL", "DEFINT", "DEFLNG", "DEFSNG", "DEFSTR", "DEFVAR", "DELETE", "DIM", "DIR", "DIR$", "DO", "DOEVENTS", "DOUBLE", "DRAG", "DYNASET", "EDIT", "ELSE", "ELSEIF", "END", "ENDDOC", "ENDIF", "
ENVIRON$", "EOF", "EQV", "ERASE", "ERL", "ERR", "ERROR", "ERROR$", "EXECUTESQL", "EXIT", "EXP", "EXPLICIT", "FALSE", "FIELDSIZE", "FILEATTR", "FILECOPY", "FILEDATETIME", "FILELEN", "FIX", "FOR", "FORM", "FORMAT", "
FORMAT$", "FORMS", "FREEFILE", "FUNCTION", "GET", "GETATTR", "GETCHUNK", "GETDATA", "DETFORMAT", "GETTEXT", "GLOBAL", "GOSUB", "GOTO", "HEX", "HEX$", "HIDE", "HOUR", "IF", "IMP", "INPUT", "INPUT$", "INPUTBOX", "INPUTBOX$", "
INSTR", "INT", "INTEGER", "IS", "ISDATE", "ISEMPTY", "ISNULL", "ISNUMERIC", "KILL", "LBOUND", "LCASE", "LCASE$", "LEFT", "LEFT$", "LEN", "LET", "LIB", "LIKE", "LINE", "LINKEXECUTE", "LINKPOKE", "LINKREQUEST", "
LINKSEND", "LOAD", "LOADPICTURE", "LOC", "LOCAL", "LOCK", "LOF", "LOG", "LONG", "LOOP", "LSET", "LTRIM",
    "LTRIM$", "ME", "MID", "MID$", "MINUTE", "MKDIR", "MOD", "MONTH", "MOVE", "MOVEFIRST", "MOVELAST", "MOVENEXT", "MOVEPREVIOUS",
    "MOVERELATIVE", "MSGBOX", "NAME", "NEW", "NEWPAGE", "NEXT", "NEXTBLOCK", "NOT", "NOTHING",
    "NOW", "NULL", "OCT", "OCT$", "ON", "OPEN", "OPENDATABASE", "OPTION", "OR", "OUTPUT", "POINT", "PRESERVE", "PRINT",
    "PRINTER", "PRINTFORM", "PRIVATE", "PSET", "PUT", "PUBLIC", "QBCOLOR", "RANDOM", "RANDOMIZE", "READ", "REDIM", "REFRESH",
    "REGISTERDATABASE", "REM", "REMOVEITEM", "RESET", "RESTORE", "RESUME", "RETURN", "RGB", "RIGHT", "RIGHT$", "RMDIR", "RND",
    "ROLLBACK", "RSET", "RTRIM", "RTRIM$", "SAVEPICTURE", "SCALE", "SECOND", "SEEK", "SELECT", "SENDKEYS", "SET", "SETATTR",
    "SETDATA", "SETFOCUS", "SETTEXT", "SGN", "SHARED",
    "SHELL", "SHOW", "SIN", "SINGLE", "SPACE", "SPACE$", "SPC", "SQR",
    "STATIC", "STEP", "STOP", "STR", "STR$", "STRCOMP", "STRING", "STRING$", "SUB",
    "SYSTEM", "TAB", "TAN", "TEXT", "TEXTHEIGHT", "TEXTWIDTH", "THEN", "TIME", "TIME$",
    "TIMER", "TIMESERIAL", "TIMEVALUE", "TO", "TRIM",
    "TRIM$", "TRUE", "TYPE", "TYPEOF", "UBOUND", "UCASE", "UCASE$", "UNLOAD",
    "UNLOCK", "UNTIL", "UPDATE", "USING", "VAL", "VARIANT", "VARTYPE", "WEEKDAY", "WEND", "WHILE", "WIDTH",
    "WRITE", "XOR", "YEAR", "ZORDER", "ADDHANDLER", "ADDRESSOF", "ALIAS", "AND", "ANDALSO", "AS", "BYREF",
    "BOOLEAN", "BYTE", "BYVAL", "CALL", "CASE", "CATCH", "CBOOL", "CBYTE", "CCHAR", "CDATE",
    "CDEC", "CDBL", "CHAR", "CINT", "CLASS", "CLNG", "COBJ", "CONST", "CONTINUE", "CSBYTE",
    "CSHORT", "CSNG", "CSTR", "CTYPE", "CUINT", "CULNG", "CUSHORT", "DATE", "DECIMAL", "DECLARE",
    "DEFAULT", "DELEGATE", "DIM", "DIRECTCAST", "DOUBLE", "DO", "EACH", "ELSE", "ELSEIF", "END",
    "ENDIF", "ENUM", "ERASE", "ERROR", "EVENT", "EXIT", "FALSE", "FINALLY", "FOR", "FRIEND",
    "FUNCTION", "GET", "GETTYPE", "GLOBAL", "GOSUB", "GOTO", "HANDLES", "IF", "IMPLEMENTS",
    "IMPORTS", "IN", "INHERITS", "INTEGER", "INTERFACE", "IS", "ISNOT", "LET", "LIB", "LIKE",
    "LONG", "LOOP", "ME", "MOD", "MODULE", "MUSTINHERIT", "MUSTOVERRIDE", "MYBASE", "MYCLASS",
    "NAMESPACE", "NARROWING", "NEW", "NEXT", "NOT", "NOTHING", "NOTINHERITABLE", "NOTOVERRIDABLE",
    "OBJECT", "ON", "OF", "OPERATOR", "OPTION", "OPTIONAL", "OR", "ORELSE", "OVERLOADS",
    "OVERRIDABLE", "OVERRIDES", "PARAMARRAY", "PARTIAL", "PRIVATE", "PROPERTY", "PROTECTED",
    "PUBLIC", "RAISEEVENT", "READONLY", "REDIM", "REM", "REMOVEHANDLER", "RESUME", "RETURN",
    "SBYTE", "SELECT", "SET", "SHADOWS", "SHARED", "SHORT", "SINGLE", "STATIC", "STEP", "STOP",
    "STRING", "STRUCTURE", "SUB", "SYNCLOCK", "THEN", "THROW", "TO", "TRUE", "TRY", "TRYCAST",
    "TYPEOF", "WEND", "VARIANT", "UINTEGER", "ULONG", "USHORT", "USING", "WHEN", "WHILE", "WIDENING",
    "WITH", "WITHEVENTS", "WRITEONLY", "XOR", "#CONST", "#ELSE", "#ELSEIF", "#END", "#IF"})
        ''' <summary>   The index of search text. </summary>

        Private Shared indexOfSearchText As Integer = 0
        ''' <summary>   The start. </summary>

        Private Shared start As Integer = 0
        ''' <summary>   The grammar. </summary>

        Private mGrammar As New List(Of String)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Color search term. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="SearchStr">    [in,out]. </param>
        ''' <param name="Rtb">          . </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub ColorSearchTerm(ByRef SearchStr As String, Rtb As RichTextBox)
            ColorSearchTerm(SearchStr, Rtb, Color.CadetBlue)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Color search term. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="SearchStr">    [in,out]. </param>
        ''' <param name="Rtb">          . </param>
        ''' <param name="MyColor">      [in,out]. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub ColorSearchTerm(ByRef SearchStr As String, Rtb As RichTextBox, ByRef MyColor As Color)
            Dim startindex As Integer = 0
            start = 0
            indexOfSearchText = 0

            If SearchStr <> "" Then

                SearchStr &= " "


                If SearchStr.Length > 0 And startindex = 0 Then
                    startindex = FindText(Rtb, SearchStr, start, Rtb.Text.Length)
                End If
                ' If string was found in the RichTextBox, highlight it
                If startindex >= 0 Then
                    ' Set the highlight color as red
                    Rtb.SelectionColor = MyColor

                    ' Find the end index. End Index = number of characters in textbox
                    Dim endindex As Integer = SearchStr.Length
                    ' Highlight the search string

                    Rtb.Select(startindex, endindex)
                    Rtb.SelectedText.ToUpper()
                    ' mark the start position after the position of last search string
                    start = startindex + endindex

                End If
            Else
            End If
            Rtb.Select(Rtb.TextLength, Rtb.TextLength)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches For Internal Syntax. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Rtb">  [in,out]. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub SearchSyntax(ByRef Rtb As RichTextBox)
            'Searches Basic Syntax
            For Each wrd As String In SyntaxTerms
                start = 0
                indexOfSearchText = 0
                ColorSearchTerm(wrd, Rtb)
            Next
            For Each wrd As String In SyntaxTerms
                start = 0
                indexOfSearchText = 0
                ColorSearchTerm(wrd, Rtb)
            Next
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches for Specific Word to colorize (Blue) </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Rtb">      [in,out]. </param>
        ''' <param name="Terms">    [in,out] The terms. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub SearchSyntax(ByRef Rtb As RichTextBox, ByRef Terms As List(Of String))
            'Searches Basic Syntax
            For Each wrd As String In SyntaxTerms
                start = 0
                indexOfSearchText = 0
                ColorSearchTerm(wrd, Rtb)
            Next
            For Each wrd As String In Terms
                start = 0
                indexOfSearchText = 0
                ColorSearchTerm(UCase(wrd), Rtb)
            Next
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches For Internal Syntax. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Rtb">          [in,out]. </param>
        ''' <param name="SearchStr">    [in,out]. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Shared Sub SearchSyntax(ByRef Rtb As RichTextBox, ByRef SearchStr As String)
            start = 0
            indexOfSearchText = 0
            ColorSearchTerm(SearchStr, Rtb)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches For Internal Syntax. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Rtb">          [in,out]. </param>
        ''' <param name="SearchStr">    [in,out]. </param>
        ''' <param name="MyColor">      my color. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Shared Sub SearchSyntax(ByRef Rtb As RichTextBox, ByRef SearchStr As String, MyColor As Color)

            start = 0
            indexOfSearchText = 0
            ColorSearchTerm(SearchStr, Rtb, MyColor)

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches for the first text. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Rtb">          [in,out]. </param>
        ''' <param name="SearchStr">    . </param>
        ''' <param name="searchStart">  The search start. </param>
        ''' <param name="searchEnd">    The search end. </param>
        '''
        ''' <returns>   The found text. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Function FindText(ByRef Rtb As RichTextBox, SearchStr As String,
                                                        ByVal searchStart As Integer, ByVal searchEnd As Integer) As Integer

            ' Unselect the previously searched string
            If searchStart > 0 AndAlso searchEnd > 0 AndAlso indexOfSearchText >= 0 Then
                Rtb.Undo()
            End If

            ' Set the return value to -1 by default.
            Dim retVal As Integer = -1

            ' A valid starting index should be specified. if indexOfSearchText = -1, the end of search
            If searchStart >= 0 AndAlso indexOfSearchText >= 0 Then

                ' A valid ending index
                If searchEnd > searchStart OrElse searchEnd = -1 Then

                    ' Find the position of search string in RichTextBox
                    indexOfSearchText = Rtb.Find(SearchStr, searchStart, searchEnd, RichTextBoxFinds.WholeWord)

                    ' Determine whether the text was found in richTextBox1.
                    If indexOfSearchText <> -1 Then
                        ' Return the index to the specified search text.
                        retVal = indexOfSearchText
                    End If

                End If
            End If
            Return retVal

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Highlight internal syntax. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   [in,out] The sender. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub HighlightInternalSyntax(ByRef sender As RichTextBox)


            For Each Word As String In SyntaxTerms
                '  HighlightWord(sender, Word)
                HighlightWord(sender, LCase(Word))
                HighlightWord(sender, ProperCase(Word))
            Next

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Highlight word. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   [in,out] The sender. </param>
        ''' <param name="word">     [in,out] The word. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Sub HighlightWord(ByRef sender As RichTextBox, ByRef word As String)

            Dim index As Integer = 0
            While index < sender.Text.LastIndexOf(word)
                'find
                sender.Find(word, index, sender.TextLength, RichTextBoxFinds.WholeWord)
                'select and color
                sender.SelectionColor = Color.Magenta
                ' word = UCase(word)
                index = sender.Text.IndexOf(word, index) + 1
            End While
        End Sub
    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Initializes the component. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'SyntaxTextBox
        '
        Me.AutoWordSelection = True
        Me.EnableAutoDragDrop = True
        Me.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResumeLayout(False)
        AddTerms()
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Syntax text box text changed. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub SyntaxTextBox_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        'Exit conditions: ImAdding prevents recursive event call, ImDeleting allows Backspace to work
        If ImAdding Or ImDone Or IveBeenClicked Or Strings.Trim(sender.text) = "" Then
            cmsDropDown.Close()
            Return
        End If
        'Close old dropdown lists
        cmsDropDown.Close()

        'Get last word entered
        tb = sender
        Dim CursorPosition As Integer = tb.SelectionStart
        lastWord = GetLastWordEntered(tb)
        Suggestions = GetSuggestions(lastWord)
        LastIndex = GetLastIndex(tb)

        ''Display dropdown list above 3 to stop keep popping up
        If Suggestions.Count > 0 Then

            With cmsDropDown
                .Items.Clear()
                For iCount As Integer = 0 To IIf(Suggestions.Count > 100, 65, Suggestions.Count)
                    .Items.Add(Suggestions(iCount))
                    AddHandler .Items(iCount).Click, AddressOf cmsDropDown_Click
                Next
                .AutoSize = True
                .AutoClose = False
                .Width = tb.Width
                .Font = tb.Font
                .ForeColor = Color.Aqua
                .BackColor = Color.Black
                .BackgroundImage = My.Resources.Bar2
                .BackgroundImageLayout = ImageLayout.Stretch
                .MaximumSize = New Size(tb.Width, 300)
                .LayoutStyle = ToolStripLayoutStyle.Flow
                .Show(MousePosition.X + 10, MousePosition.Y + 30)

            End With

            'set focus back to original control
            tb.Focus()
        Else

            cmsDropDown.Close()

        End If
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Clears the terms. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub ClearTerms()
        Me.Syntax.Clear()

    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Adds a term. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="item"> The item. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub AddTerm(item As String)

        Me.Syntax.Add(item)


    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Gets the last index. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   [in,out] Source of the event. </param>
    '''
    ''' <returns>   The last index. </returns>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Function GetLastIndex(ByRef sender As SyntaxTextBox) As Integer
        'Get last word entered
        tb = sender

        Dim CursorPosition As Integer = tb.SelectionStart
        Dim Txt = Strings.Trim(CStr(tb.Text))
        Dim lastIndex As Integer = IIf(Txt.LastIndexOf(" ") = -1, 0, Txt.LastIndexOf(" ") + 1)
        Return lastIndex
    End Function

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Gets the suggestions in this collection. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="LastWord"> The last word. </param>
    '''
    ''' <returns>
    ''' An enumerator that allows foreach to be used to process the suggestions in this collection.
    ''' </returns>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Function GetSuggestions(ByVal LastWord As String) As IEnumerable(Of String)
        Dim QueryStR As String = UCase(LastWord)
        'Get word suggestions based on word start for Append function
        Dim StringList As New List(Of String)
        For Each c In tb.iSyntax
            StringList.Add(UCase(c))
        Next
        Dim iSuggestions As IEnumerable(Of String) = From c In StringList Where c.StartsWith(QueryStR, StringComparison.InvariantCultureIgnoreCase) Select c
        ''Reset suggestion list to 'contains' if list is small - NOW CASE SENSITIVE
        If iSuggestions.Count = 0 Then
            iSuggestions = From c In StringList Where c.IndexOf(LastWord, StringComparison.InvariantCultureIgnoreCase) <> -1 Select c 'c.Contains(lastWord) 
        End If
        Return iSuggestions
    End Function

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Gets the last word entered. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="Sender">   [in,out] The sender. </param>
    '''
    ''' <returns>   The last word entered. </returns>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Function GetLastWordEntered(ByRef Sender As SyntaxTextBox) As String
        'Get last word entered
        Dim tb = Sender
        Try
            Dim CursorPosition As Integer = Sender.SelectionStart
            Dim Txt = Strings.Trim(CStr(tb.Text))
            Dim lastIndex As Integer = IIf(Txt.LastIndexOf(" ") = -1, 0, Txt.LastIndexOf(" ") + 1)
            Dim lastWord As String = Strings.Trim(Txt.Substring(lastIndex))
            Return lastWord

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "GetLastWordEntered Error ")
        End Try
        Return ""
    End Function

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Adds terms. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub AddTerms()
        For Each item In SyntaxTerms
            If Checkterm(item) = False Then
                Me.Syntax.Add(item)
            End If
        Next
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Checkterms the given string. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="str">  [in,out] The string. </param>
    '''
    ''' <returns>   True if it succeeds, false if it fails. </returns>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Function Checkterm(ByRef str As String) As Boolean
        Checkterm = False
        For Each item In Syntax
            If item = str Then Return True

        Next
    End Function
    ''' <summary>   The syntax terms. </summary>

    Public Shared SyntaxTerms() As String = ({"SPYDAZ", "ABS", "ACCESS", "ADDITEM", "ADDNEW", "ALIAS", "AND", "ANY", "APP", "APPACTIVATE", "APPEND", "APPENDCHUNK", "ARRANGE", "AS", "ASC", "ATN", "BASE", "BEEP", "BEGINTRANS", "BINARY", "BYVAL", "CALL", "CASE", "CCUR", "CDBL", "CHDIR", "CHDRIVE", "CHR", "CHR$", "CINT", "CIRCLE", "CLEAR", "CLIPBOARD", "CLNG", "CLOSE", "CLS", "COMMAND", "
COMMAND$", "COMMITTRANS", "COMPARE", "CONST", "CONTROL", "CONTROLS", "COS", "CREATEDYNASET", "CSNG", "CSTR", "CURDIR$", "CURRENCY", "CVAR", "CVDATE", "DATA", "DATE", "DATE$", "DIM", "DATESERIAL", "DATEVALUE", "DAY", "
DEBUG", "DECLARE", "DEFCUR", "CEFDBL", "DEFINT", "DEFLNG", "DEFSNG", "DEFSTR", "DEFVAR", "DELETE", "DIM", "DIR", "DIR$", "DO", "DOEVENTS", "DOUBLE", "DRAG", "DYNASET", "EDIT", "ELSE", "ELSEIF", "END", "ENDDOC", "ENDIF", "
ENVIRON$", "EOF", "EQV", "ERASE", "ERL", "ERR", "ERROR", "ERROR$", "EXECUTESQL", "EXIT", "EXP", "EXPLICIT", "FALSE", "FIELDSIZE", "FILEATTR", "FILECOPY", "FILEDATETIME", "FILELEN", "FIX", "FOR", "FORM", "FORMAT", "
FORMAT$", "FORMS", "FREEFILE", "FUNCTION", "GET", "GETATTR", "GETCHUNK", "GETDATA", "DETFORMAT", "GETTEXT", "GLOBAL", "GOSUB", "GOTO", "HEX", "HEX$", "HIDE", "HOUR", "IF", "IMP", "INPUT", "INPUT$", "INPUTBOX", "INPUTBOX$", "
INSTR", "INT", "INTEGER", "IS", "ISDATE", "ISEMPTY", "ISNULL", "ISNUMERIC", "KILL", "LBOUND", "LCASE", "LCASE$", "LEFT", "LEFT$", "LEN", "LET", "LIB", "LIKE", "LINE", "LINKEXECUTE", "LINKPOKE", "LINKREQUEST", "
LINKSEND", "LOAD", "LOADPICTURE", "LOC", "LOCAL", "LOCK", "LOF", "LOG", "LONG", "LOOP", "LSET", "LTRIM",
    "LTRIM$", "ME", "MID", "MID$", "MINUTE", "MKDIR", "MOD", "MONTH", "MOVE", "MOVEFIRST", "MOVELAST", "MOVENEXT", "MOVEPREVIOUS",
    "MOVERELATIVE", "MSGBOX", "NAME", "NEW", "NEWPAGE", "NEXT", "NEXTBLOCK", "NOT", "NOTHING",
    "NOW", "NULL", "OCT", "OCT$", "ON", "OPEN", "OPENDATABASE", "OPTION", "OR", "OUTPUT", "POINT", "PRESERVE", "PRINT",
    "PRINTER", "PRINTFORM", "PRIVATE", "PSET", "PUT", "PUBLIC", "QBCOLOR", "RANDOM", "RANDOMIZE", "READ", "REDIM", "REFRESH",
    "REGISTERDATABASE", "REM", "REMOVEITEM", "RESET", "RESTORE", "RESUME", "RETURN", "RGB", "RIGHT", "RIGHT$", "RMDIR", "RND",
    "ROLLBACK", "RSET", "RTRIM", "RTRIM$", "SAVEPICTURE", "SCALE", "SECOND", "SEEK", "SELECT", "SENDKEYS", "SET", "SETATTR",
    "SETDATA", "SETFOCUS", "SETTEXT", "SGN", "SHARED",
    "SHELL", "SHOW", "SIN", "SINGLE", "SPACE", "SPACE$", "SPC", "SQR",
    "STATIC", "STEP", "STOP", "STR", "STR$", "STRCOMP", "STRING", "STRING$", "SUB",
    "SYSTEM", "TAB", "TAN", "TEXT", "TEXTHEIGHT", "TEXTWIDTH", "THEN", "TIME", "TIME$",
    "TIMER", "TIMESERIAL", "TIMEVALUE", "TO", "TRIM",
    "TRIM$", "TRUE", "TYPE", "TYPEOF", "UBOUND", "UCASE", "UCASE$", "UNLOAD",
    "UNLOCK", "UNTIL", "UPDATE", "USING", "VAL", "VARIANT", "VARTYPE", "WEEKDAY", "WEND", "WHILE", "WIDTH",
    "WRITE", "XOR", "YEAR", "ZORDER", "ADDHANDLER", "ADDRESSOF", "ALIAS", "AND", "ANDALSO", "AS", "BYREF",
    "BOOLEAN", "BYTE", "BYVAL", "CALL", "CASE", "CATCH", "CBOOL", "CBYTE", "CCHAR", "CDATE",
    "CDEC", "CDBL", "CHAR", "CINT", "CLASS", "CLNG", "COBJ", "CONST", "CONTINUE", "CSBYTE",
    "CSHORT", "CSNG", "CSTR", "CTYPE", "CUINT", "CULNG", "CUSHORT", "DATE", "DECIMAL", "DECLARE",
    "DEFAULT", "DELEGATE", "DIM", "DIRECTCAST", "DOUBLE", "DO", "EACH", "ELSE", "ELSEIF", "END",
    "ENDIF", "ENUM", "ERASE", "ERROR", "EVENT", "EXIT", "FALSE", "FINALLY", "FOR", "FRIEND",
    "FUNCTION", "GET", "GETTYPE", "GLOBAL", "GOSUB", "GOTO", "HANDLES", "IF", "IMPLEMENTS",
    "IMPORTS", "IN", "INHERITS", "INTEGER", "INTERFACE", "IS", "ISNOT", "LET", "LIB", "LIKE",
    "LONG", "LOOP", "ME", "MOD", "MODULE", "MUSTINHERIT", "MUSTOVERRIDE", "MYBASE", "MYCLASS",
    "NAMESPACE", "NARROWING", "NEW", "NEXT", "NOT", "NOTHING", "NOTINHERITABLE", "NOTOVERRIDABLE",
    "OBJECT", "ON", "OF", "OPERATOR", "OPTION", "OPTIONAL", "OR", "ORELSE", "OVERLOADS",
    "OVERRIDABLE", "OVERRIDES", "PARAMARRAY", "PARTIAL", "PRIVATE", "PROPERTY", "PROTECTED",
    "PUBLIC", "RAISEEVENT", "READONLY", "REDIM", "REM", "REMOVEHANDLER", "RESUME", "RETURN",
    "SBYTE", "SELECT", "SET", "SHADOWS", "SHARED", "SHORT", "SINGLE", "STATIC", "STEP", "STOP",
    "STRING", "STRUCTURE", "SUB", "SYNCLOCK", "THEN", "THROW", "TO", "TRUE", "TRY", "TRYCAST",
    "TYPEOF", "WEND", "VARIANT", "UINTEGER", "ULONG", "USHORT", "USING", "WHEN", "WHILE", "WIDENING",
    "WITH", "WITHEVENTS", "WRITEONLY", "XOR", "#CONST", "#ELSE", "#ELSEIF", "#END", "#IF"})

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>
    ''' Initializes a new instance of the <see cref="T:System.Windows.Forms.RichTextBox" /> class.
    ''' </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub New()
        AddTerms()
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Refreshes this.  </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub _Refresh()
        Dim CursorPosition As Integer = Me.SelectionStart
        Highlighter.HighlightInternalSyntax(Me)
        Highlighter.SearchSyntax(Me, iSyntax)
        Me.SelectionStart = CursorPosition
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Event handler. Called by cmsDropDown for click events. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub cmsDropDown_Click(sender As System.Object, e As System.EventArgs)

        cmsDropDown.Close()

        'Get last word of TextBox
        Dim Txt = Strings.Trim(CStr(Me.Text))
        Dim lastIndex As Integer = IIf(Txt.LastIndexOf(" ") = -1, 0, Txt.LastIndexOf(" ") + 1)

        'Set to new value 
        With Me
            .SelectionStart = lastIndex
            .SelectionLength = .TextLength
            .SelectedText = sender.text
        End With

        IveBeenClicked = True

    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Class inteli text box key down. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Key event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub ClassInteliTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown



        Select Case e.KeyCode
            Case Keys.Insert
                Dim CursorPosition As Integer = Me.SelectionStart
                Highlighter.HighlightInternalSyntax(Me)
                Highlighter.SearchSyntax(Me, iSyntax)
                sender.SelectionStart = CursorPosition
            Case Keys.Tab
                If Suggestions IsNot Nothing Then
                    ImAdding = True
                    Dim CursorPosition As Integer = tb.SelectionStart
                    'Append word if available (autoSelect)
                    If Not (Suggestions(0) Is Nothing) Then
                        newWord = Suggestions(0)
                    End If
                    'If tb.SelectionLength = 0 Then
                    Dim Txt = Strings.Trim(CStr(tb.Text))
                    Dim lastIndex As Integer = IIf(Txt.LastIndexOf(" ") = -1, 0, Txt.LastIndexOf(" ") + 1)
                    Dim Pos As Integer = 0
                    'Set to new value
                    If newWord <> "" Then
                        With sender
                            .SelectionStart = lastIndex
                            .SelectionLength = .TextLength
                            .SelectedText = newWord
                            Pos = .TextLength
                        End With
                        newWord = ""
                    End If
                    'End If

                    sender.SelectionStart = Pos

                    ImDone = True
                    IveBeenClicked = True
                    e.SuppressKeyPress = True
                Else
                    ImDone = True
                    IveBeenClicked = False
                    e.SuppressKeyPress = False

                End If
            Case Keys.Back
                ImDone = True
                IveBeenClicked = False
                newWord = ""
                cmsDropDown.Close()
            Case Keys.Space
                ImDone = True
                IveBeenClicked = False
                newWord = ""
                cmsDropDown.Close()



            Case Keys.Enter
                ImDone = True
                IveBeenClicked = False
                cmsDropDown.Close()

            Case Else
                ImDone = False
                IveBeenClicked = False

        End Select


    End Sub


End Class

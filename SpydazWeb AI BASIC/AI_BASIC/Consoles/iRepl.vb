Public MustInherit Class iRepl
    Public Line As String = ""
    Public ShowTree As Boolean = False
    Public ShowSyntax As Boolean = False
    Public ShowCode As Boolean = True


#Region "REPL"
    Public Sub CheckReplCmd()
        Select Case UCase(Line)
            Case "CLS"
                _ClearScreen()
                _GetInput()
            Case "SHOWSYNTAX"
                ShowSyntax = True
                _GetInput()
            Case "SHOWTOKENS"
                ShowTree = True
                _GetInput()
            Case "HIDESYNTAX"
                ShowSyntax = False
                _GetInput()
            Case "HIDETOKENS"
                ShowTree = False
                _GetInput()
            Case "HIDECODE"
                ShowCode = False
                _GetInput()
            Case "SHOWCODE"
                ShowCode = True
                _GetInput()
        End Select
    End Sub
    Public Sub ResetConsole()
        Console.ForegroundColor = ConsoleColor.White
    End Sub
    Public Sub _WriteCodeLine()
        Console.WriteLine(Line)
        ResetConsole()
    End Sub
    Public Sub _DisplayPrompt()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.Write("»")
        ResetConsole()
    End Sub
    Public Sub _ClearScreen()
        Console.Clear()
        ResetConsole()
    End Sub
    Public Sub _GetInput()
        Line = Console.ReadLine().Replace("»", "")
    End Sub
    Public Sub _Show_title()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Welcome to SpydazWeb Basic")
        ResetConsole()
    End Sub
#End Region

End Class

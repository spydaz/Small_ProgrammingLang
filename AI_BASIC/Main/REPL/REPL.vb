Namespace REPL
    Module REPL
        Private ShowTree As Boolean = True
        Private ShowTokens As Boolean = True
        Private History As New List(Of String)

        ''' <summary>
        ''' Console REPL
        ''' </summary>
        Sub Main()
            'Intro
            _Show_title()
            'GetInput (line by Line)
            Dim UserInput_LINE As String = Console.ReadLine()
#Region "REPL_CMDS"
            'ReplCmds
            If UserInput_LINE = "#ShowTree" Then
                ShowTree = True
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#HideTree" Then
                ShowTree = False
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#ShowTokens" Then
                ShowTokens = True
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#HideTokens" Then
                ShowTokens = False
                UserInput_LINE = Console.ReadLine()
            End If
            If UserInput_LINE = "#Cls" Then
                Console.Clear()
                UserInput_LINE = Console.ReadLine()
            End If
#End Region
            'Handle WhiteSpace Early in repl
            If String.IsNullOrWhiteSpace(UserInput_LINE) Then
                Return
            Else
                'Save Statments
                History.Add(UserInput_LINE)
            End If
            'MAIN:
            '
            'Get Expression/Tree
            Dim ExpressionTree = SyntaxTree.Parse(UserInput_LINE)
            'Outputs
            If ShowTree = True Then
                DisplayAST_Tree()
            End If
            If ShowTokens = True Then
                DisplayToken_Tree()
            End If
            SetReplCode()
        End Sub
        Public Sub DisplayAST_Tree()
            Console.ForegroundColor = ConsoleColor.DarkYellow
        End Sub
        Public Sub DisplayToken_Tree()
            Console.ForegroundColor = ConsoleColor.White
        End Sub
        Public Sub _Show_title()
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("Welcome  Basic")
        End Sub
        Public Sub DisplayDiagnostics()
            Console.ForegroundColor = ConsoleColor.Red
        End Sub
        Public Sub SetReplCode()
            Console.ForegroundColor = ConsoleColor.Magenta
        End Sub
    End Module

End Namespace

Namespace Consoles



    Public Module ReplCmds

        Public Enum ReplCmd
            ShowTokenTree
            HideTokenTree
            ShowExpressionTree
            HideExpressionTree
            ClearScreen
            LoadEditor
            LoadTester
            NoComand
            Run
            Compile
        End Enum
        Public Function CheckIfReplCmd(ByRef InputStr As String) As ReplCmd
            Select Case UCase(InputStr)
                Case "SHOWTOKENTREE"
                    Return ReplCmd.ShowTokenTree
                Case "HIDETOKENTREE"
                    Return ReplCmd.HideTokenTree
                Case "SHOWEXPRESSIONTREE"
                    Return ReplCmd.ShowExpressionTree
                Case "HIDEEXPRESSIONTREE"
                    Return ReplCmd.HideExpressionTree
                Case "CLEARSCREEN"
                    Return ReplCmd.ClearScreen
                Case "LOADEDITOR"
                    Return ReplCmd.LoadEditor
                Case "LOADTESTER"
                    Return ReplCmd.LoadTester
                Case "RUN"
                    Return ReplCmd.Run
                Case "COMPILE"
                    Return ReplCmd.Compile
                Case Else
                    Return ReplCmd.NoComand
            End Select
        End Function



    End Module

End Namespace




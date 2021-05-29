'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Compilation\CompilerRepl.vb
'
' summary:	Compiler repl class
'---------------------------------------------------------------------------------------------------
Imports System.IO




Public Module CompilerRepl
                Private CodeFilename As String
                Private Code As List(Of String)
                'Command line arguments are delimited by spaces. You can use double quotation marks (") to include spaces within an argument. The single quotation mark ('), however, does not provide this functionality.
                Public Sub Main()
                    Dim arguments As String() = Environment.GetCommandLineArgs()
                    If arguments IsNot Nothing Then

                        CodeFilename = arguments(0)
                        Code = ReadFileTolines(CodeFilename)
                        Console.WriteLine("FileName : {0}", CodeFilename)
                        Run(Code)
                    Else
                        Run()
                    End If
                End Sub
                Public Sub Run(ByRef iCode As List(Of String))
        Dim Con As New InterpretorRepl
        Con.CompileScript(iCode)
                End Sub
                Public Sub Run()
        Dim Con As New InterpretorRepl
        Con.RunCmdLine()
                End Sub
                Public Function ReadFileTolines(ByRef FilePath As String) As List(Of String)
                    ' Open the file to read from.
                    Dim readText() As String = File.ReadAllLines(FilePath)
                    Return readText.ToList
                End Function
            End Module

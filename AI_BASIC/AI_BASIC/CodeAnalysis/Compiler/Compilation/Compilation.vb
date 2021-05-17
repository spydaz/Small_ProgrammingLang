Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler
        Public Class Compiler
            ''' <summary>
            ''' Errors Produced by the Produce TokenTree 
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property TokenTreeErrors As List(Of String)
                Get
                    Return _LineDiagnostics
                End Get

            End Property
            Private _LineDiagnostics As New List(Of String)
            Private Program As New List(Of String)
            Private CurrentLine As Integer
            Private CompiledWithErrors As Boolean = False
            Private CurrentLineStateReady As Boolean = False
            Private ProgramCompiledReady As Boolean = False
            Private ProgramScript As String = ""
            Public ReadOnly Property CompiledReady As Boolean
                Get
                    Return ProgramCompiledReady
                End Get
            End Property
            Private LineCompiledlineStates As List(Of Boolean)
            Private _CompilerDiagnostics As List(Of String)
            Public ReadOnly Property CompilerErrors As String
                Get
                    Return GetCompilerDiagnostics()
                End Get
            End Property
            Public Sub New()

            End Sub
            Public Sub New(ByRef _Program As String)
                ProgramScript = _Program
            End Sub
            ''' <summary>
            ''' Returns a result
            ''' </summary>
            ''' <returns></returns>
            Public Function ExecuteProgram() As String
                'Check if it compiles
                CompileProgram()

                If ProgramCompiledReady = True Then
                    Return EvaluateExpressionSyntaxTree(ProduceExpressionSyntaxTree(ProduceTokenTree(ProgramScript)))
                Else
                    Return "Compiler still has errors please recompile: " & vbNewLine &
                        "Unable to execute:" & vbNewLine & CompilerErrors
                End If

            End Function
            Public Function ExecuteProgram(ByRef _Program As String) As String
                ProgramScript = _Program
                'Check if it compiles
                CompileProgram()

                If ProgramCompiledReady = True Then
                    Return EvaluateExpressionSyntaxTree(ProduceExpressionSyntaxTree(ProduceTokenTree(ProgramScript)))
                Else
                    Return "Compiler still has errors please recompile: " & vbNewLine &
                        "Unable to execute:" & vbNewLine & CompilerErrors
                End If

            End Function
            ''' <summary>
            ''' Debugs code for errors
            ''' </summary>
            ''' <returns></returns>
            Public Function CompileProgram() As Boolean
                CompiledWithErrors = False
                CurrentLineStateReady = False
                ProgramCompiledReady = False
                LineCompiledlineStates = New List(Of Boolean)
                Dim lines = ProgramScript.Split(vbNewLine)
                For Each item In lines
                    LineCompiledlineStates.Add(CompileForDebugging(item))
                    If CurrentLineStateReady = True Then
                    Else
                        _CompilerDiagnostics.Add("Error at line " & CurrentLine &
                                                 vbNewLine & GetLineDiagnostics())
                    End If
                Next
                'checkState
                Select Case CompiledWithErrors
                    Case True
                        ProgramCompiledReady = True
                        Return ProgramCompiledReady
                    Case False
                        ProgramCompiledReady = False
                        Return ProgramCompiledReady
                End Select
                Return Nothing
            End Function
            ''' <summary>
            ''' Debugs code for errors 
            ''' </summary>
            ''' <param name="_Program"></param>
            ''' <returns></returns>
            Public Function CompileProgram(ByRef _Program As String) As Boolean
                CompiledWithErrors = False
                CurrentLineStateReady = False
                ProgramCompiledReady = False
                LineCompiledlineStates = New List(Of Boolean)
                Dim lines = _Program.Split(vbNewLine)
                For Each item In lines
                    LineCompiledlineStates.Add(CompileForDebugging(item))
                    If CurrentLineStateReady = True Then
                    Else
                        _CompilerDiagnostics.Add("Error at line " & CurrentLine &
                                                 vbNewLine & GetLineDiagnostics())
                    End If
                Next
                'checkState
                Select Case CompiledWithErrors
                    Case True
                        ProgramCompiledReady = True
                        Return ProgramCompiledReady
                    Case False
                        ProgramCompiledReady = False
                        Return ProgramCompiledReady
                End Select
                Return Nothing
            End Function
            ''' <summary>
            ''' Debugs code for errors
            ''' </summary>
            ''' <param name="Line"></param>
            ''' <returns></returns>
            Private Function CompileForDebugging(ByRef Line As String) As Boolean
                Dim ExpressionTree As SyntaxTree
                _LineDiagnostics = New List(Of String)
                CurrentLineStateReady = False
                CurrentLine += 1
                Try
                    ExpressionTree = ProduceExpressionSyntaxTree(ProduceTokenTree(Line))
                    If _LineDiagnostics.Count > 0 Then
                        ''DEBUGGING POINT:
                        '    MessageBox.Show("Compiler Errors :" & vbNewLine & GetLineDiagnostics(),
                        '"Compiler has Found Errors on Current line :" & CurrentLine)

                        CurrentLineStateReady = False
                        CompiledWithErrors = True
                    Else

                        CurrentLineStateReady = True
                    End If


                Catch ex As Exception
                    CurrentLineStateReady = False
                    MessageBox.Show(ex.ToString & vbNewLine & "Compiler Errors :" & vbNewLine & GetLineDiagnostics(),
                    "Compiler has Found a fatal error on Current line :" & CurrentLine)
                    Return False
                End Try

                'Compiled With or Without errors but no fatal errors
                Return True


            End Function
            ''' <summary>
            ''' Returns diagnostics for line
            ''' </summary>
            ''' <returns></returns>
            Private Function GetLineDiagnostics() As String
                Dim Str As String = ""
                Dim Count As Integer = 1
                If _LineDiagnostics.Count > 0 Then
                    For Each item In _LineDiagnostics
                        Str &= "Line Error :" & Count & vbNewLine & item & vbNewLine
                    Next
                End If
                Return Str
            End Function
            ''' <summary>
            ''' Gets compiler diagnostics 
            ''' </summary>
            ''' <returns></returns>
            Private Function GetCompilerDiagnostics() As String
                Dim Str As String = ""
                Dim Count As Integer = 1
                If _CompilerDiagnostics.Count > 0 Then
                    For Each item In _CompilerDiagnostics
                        Str &= "CompilerError :" & Count & vbNewLine & item & vbNewLine
                    Next
                End If
                Return Str
            End Function
            ''' <summary>
            ''' Produces a tokenized tree regardless of errors
            ''' Errors are retrieved in TokenTreeErrors
            ''' </summary>
            ''' <param name="Line"></param>
            ''' <returns></returns>
            Public Function ProduceTokenTree(ByRef Line As String) As List(Of SyntaxToken)
                Dim iLexer As New Lexer(Line)
                Dim _tree As New List(Of SyntaxToken)

                While True
                    Dim Token = iLexer._NextToken
                    If Token._SyntaxType = SyntaxType._EndOfFileToken Then

                        Exit While

                    Else
                        _tree.Add(Token)
                    End If
                End While
                'Add End Of FileToken
                _tree.Add(New SyntaxToken(SyntaxType._EndOfFileToken, SyntaxType._EndOfFileToken.GetSyntaxTypeStr,
                                          "EOF", "EOF", Line.Length, Line.Length))
                _LineDiagnostics.AddRange(iLexer._Diagnostics)


                Return _tree
            End Function
            ''' <summary>
            ''' Produces an abstract syntax tree
            ''' </summary>
            ''' <param name="_SyntaxTokentree"></param>
            ''' <returns></returns>
            Public Function ProduceExpressionSyntaxTree(ByRef _SyntaxTokentree As List(Of SyntaxToken)) As SyntaxTree
                Dim iParser = New Parser(_SyntaxTokentree)
                Dim _tree As SyntaxTree = iParser.ParseSyntaxTree()
                _LineDiagnostics.AddRange(iParser._Diagnostics)
                Return _tree
            End Function
            ''' <summary>
            ''' Produces an abstract syntax tree
            ''' </summary>
            ''' <returns></returns>
            Public Function ProduceExpressionSyntaxTree(ByRef Line As String) As SyntaxTree
                Dim iParser = New Parser(ProduceTokenTree(Line))
                Dim _tree As SyntaxTree = iParser.ParseSyntaxTree()

                Return _tree
            End Function
            ''' <summary>
            ''' Evaluates Syntax tree
            ''' </summary>
            ''' <returns></returns>
            Public Function EvaluateExpressionSyntaxTree(ByRef ExpressionTree As SyntaxTree) As String
                Dim IEvaluator As New Evaluator(ExpressionTree)
                Dim Result = IEvaluator._Evaluate().ToString
                _LineDiagnostics.AddRange(IEvaluator._Diagnostics)
                Return Result
            End Function
        End Class
    End Namespace
End Namespace


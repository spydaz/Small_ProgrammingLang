Imports System.Windows.Forms
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.CodeAnalysis.Compiler.Evaluation
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor
Imports AI_BASIC.CodeAnalysis.Compiler.Tokenizer
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

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
            Private Shared _LineDiagnostics As New List(Of String)
            Private Shared Program As New List(Of String)
            Private Shared CurrentLine As Integer
            Private Shared CompiledWithErrors As Boolean = False
            Private Shared CurrentLineStateReady As Boolean = False
            Private Shared ProgramCompiledReady As Boolean = False
            Private Shared ProgramScript As String = ""
            Private Shared ExpressionTree As SyntaxTree
            Private Shared _tree As New List(Of SyntaxToken)
            Private iTokenTrees As List(Of List(Of SyntaxToken))
            Private iExpressionTrees As List(Of SyntaxTree)
            Private LineCompiledlineStates As List(Of Boolean)
            Private _CompilerDiagnostics As List(Of String)
            Public ReadOnly Property TokenTrees As List(Of List(Of SyntaxToken))
                Get
                    Return iTokenTrees
                End Get
            End Property
            Public ReadOnly Property ExpressionTrees As List(Of SyntaxTree)
                Get
                    Return iExpressionTrees
                End Get
            End Property
            Public ReadOnly Property CurrentTokenTree As List(Of SyntaxToken)
                Get
                    Return _tree
                End Get
            End Property
            Public ReadOnly Property CurrentSyntaxTree As SyntaxTree
                Get
                    Return ExpressionTree
                End Get

            End Property
            Public ReadOnly Property CompiledReady As Boolean
                Get
                    Return ProgramCompiledReady
                End Get
            End Property

            Public ReadOnly Property CompilerErrors As String
                Get
                    Return GetCompilerDiagnostics()
                End Get
            End Property
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
                    Return EvaluateExpressionSyntaxTree(CurrentSyntaxTree)
                Else
                    Return "Compiler still has errors please recompile: " & vbNewLine &
                        "Unable to execute:" & vbNewLine & CompilerErrors
                End If

            End Function
            ''' <summary>
            ''' Debugs code for errors / Produces Both tree also
            ''' </summary>
            ''' <returns></returns>
            Public Function CompileProgram() As Boolean
                iTokenTrees = New List(Of List(Of SyntaxToken))
                iExpressionTrees = New List(Of SyntaxTree)
                CompiledWithErrors = False
                CurrentLineStateReady = False
                ProgramCompiledReady = False
                LineCompiledlineStates = New List(Of Boolean)
                _CompilerDiagnostics = New List(Of String)
                CurrentLine = 0
                Dim lines = ProgramScript.Split(vbNewLine)
                For Each item In lines
                    CurrentLine += 1
                    'Capture LineDiagnostic state
                    LineCompiledlineStates.Add(CompileForDebugging(item))
                    If CurrentLineStateReady = True Then
                    Else
                        _CompilerDiagnostics.Add("Error at line " & CurrentLine &
                                                 vbNewLine & GetLineDiagnostics())
                    End If

                    'capture Accumilated trees
                    iTokenTrees.Add(CurrentTokenTree)
                    iExpressionTrees.Add(CurrentSyntaxTree)
                Next

                'checkState of Compiled lines
                For Each item In LineCompiledlineStates
                    If item = False Then
                        CompiledWithErrors = False
                    End If
                Next
                'Set Ready state
                Select Case CompiledWithErrors
                    Case True
                        ProgramCompiledReady = False
                        Return ProgramCompiledReady
                    Case False
                        ProgramCompiledReady = True
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

                _LineDiagnostics = New List(Of String)
                CurrentLineStateReady = False

                Try
                    ProduceTokenTree(Line)
                    ProduceExpressionSyntaxTree(Line)
                    If _LineDiagnostics.Count > 0 Then
                        ''DEBUGGING POINT:
                        ' MessageBox.Show("Compiler Errors :" & vbNewLine & GetLineDiagnostics(),
                        ' "Compiler has Found Errors on Current line :" & CurrentLine)

                        CurrentLineStateReady = False
                        CompiledWithErrors = True
                    Else
                        CompiledWithErrors = False
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
                        Count += 1
                    Next
                End If
                Return Str
            End Function
            ''' <summary>
            ''' Gets compiler diagnostics 
            ''' </summary>
            ''' <returns></returns>
            Public Function GetCompilerDiagnostics() As String
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
            Public Shared Function ProduceTokenTree(ByRef Line As String) As List(Of SyntaxToken)
                Dim iLexer As New Lexer(Line)
                _tree = New List(Of SyntaxToken)

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
            ''' <returns></returns>
            Public Shared Function ProduceExpressionSyntaxTree(ByRef Line As String) As SyntaxTree
                Dim iParser = New Parser(Line)
                ExpressionTree = iParser.Parse()

                Return ExpressionTree


            End Function
            ''' <summary>
            ''' Evaluates Syntax tree
            ''' </summary>
            ''' <returns></returns>
            Public Shared Function EvaluateExpressionSyntaxTree(ByRef ExpressionTree As SyntaxTree) As String
                Dim IEvaluator As New Evaluator(ExpressionTree)
                Dim Env As New EnvironmentalMemory

                Dim Result = IEvaluator._Evaluate(Env).ToString
                _LineDiagnostics.AddRange(IEvaluator._Diagnostics)
                Return Result
            End Function
        End Class
    End Namespace
End Namespace


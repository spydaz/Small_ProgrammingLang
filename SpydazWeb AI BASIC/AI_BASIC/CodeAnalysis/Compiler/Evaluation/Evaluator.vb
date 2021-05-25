Imports System.Linq.Expressions
Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Evaluation
            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Friend Class Evaluator
                Private Env As New EnvironmentalMemory
                Public _Diagnostics As New List(Of String)
                Public _tree As SyntaxTree
                ''' <summary>
                ''' Check / Report Diagnostics
                ''' </summary>
                Private Evaluateable As Boolean
                Public Sub New(ByRef _itree As SyntaxTree)
                    _tree = _itree
                End Sub
                Public Function _Evaluate(ByRef Env As EnvironmentalMemory) As Object
                    Me.Env = Env
                    For Each item In _tree.Body
                        Dim result = _EvaluateExpresssion(item)
                        If _Diagnostics.Count > 0 Then

                            Return "Evaluation Error" & _tree.ToJson
                        End If
                        Return result
                    Next
                    Return "Unable to Evaluate"
                End Function
                Private Function _EvaluateExpresssion(ByRef iNode As SyntaxNode) As Object
                    If iNode IsNot Nothing Then

                        Select Case iNode._SyntaxType
                            Case SyntaxType._NumericLiteralExpression
                                Return Evaluate_NumericLiteralExpression(iNode)
                            Case SyntaxType._StringExpression
                                Return EvaluateStringExpression(iNode)
                            Case SyntaxType._BooleanLiteralExpression
                                Return EvaluateBooleanLiteralExpression(iNode)
                            Case SyntaxType._UnaryExpression
                                Return EvaluateUnaryExpression(iNode)
                            Case SyntaxType._IdentifierExpression
                                Return EvaluateIdentifierExpression(iNode)
                            Case SyntaxType._VariableDeclaration
                                Return EvaluateVariableDeclaration(iNode)
                            Case SyntaxType._AssignmentExpression
                                Return EvaluateAssignmentExpression(iNode)
                            Case SyntaxType.AddativeExpression
                                Return EvaluateAddativeExpression(iNode)
                            Case SyntaxType.MultiplicativeExpression
                                Return EvaluateAddativeExpression(iNode)
                            Case SyntaxType.ConditionalExpression
                                Return EvaluateConditionalExpression(iNode)
                            Case SyntaxType.IfExpression
                                Return EvaluateIfExpression(iNode)
                        End Select

                    Else
                    End If
                    Dim DiagExe As New DiagnosticsException("Unexpected Expression :", ExceptionType.EvaluationException, iNode.ToJson, iNode._SyntaxType)
                    _Diagnostics.Add(DiagExe.ToJson)
                    Return "Unable to Evaluate"
                End Function
                Public Function EvaluateStringExpression(ByRef Expr As SyntaxNode) As String
                    If Expr._SyntaxType = SyntaxType._StringExpression Then
                        Dim n As SyntaxNodes.StringExpression = Expr
                        Return n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate StringExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function Evaluate_NumericLiteralExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType._NumericLiteralExpression Then
                        Dim n As NumericalExpression = Expr
                        Return n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate NumericalExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateBooleanLiteralExpression(ByRef Expr As SyntaxNode) As Boolean
                    If Expr._SyntaxType = SyntaxType._BooleanLiteralExpression Then
                        Dim n As SyntaxNodes.BooleanLiteralExpression = Expr
                        Return n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate BooleanLiteralExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateUnaryExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType._UnaryExpression Then
                        Dim u As SyntaxNodes.UnaryExpression = Expr
                        Return u.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate UnaryExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateIdentifierExpression(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._IdentifierExpression Then
                        Dim i As SyntaxNodes.IdentifierExpression = Expr
                        Return i.Evaluate(Env)

                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate IdentifierExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateVariableDeclaration(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._VariableDeclaration Then
                        Dim i As SyntaxNodes.VariableDeclarationExpression = Expr
                        Return i.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate VariableDeclaration", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateAssignmentExpression(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._AssignmentExpression Then
                        Dim i As SyntaxNodes.AssignmentExpression = Expr
                        Return i.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate _AssignmentExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        _Diagnostics.Add(x.ToJson)
                        Return Nothing
                    End If
                End Function
                Public Function EvaluateAddativeExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType.AddativeExpression Then
                        Dim b As SyntaxNodes.BinaryExpression = Expr
                        Dim _Left As Integer = b._Left.Evaluate(Env)
                        Dim _Right As Integer = b._Right.Evaluate(Env)
                        Select Case b._Operator._SyntaxType
                            Case SyntaxType.Add_Operator
                                Return _Left + _Right
                            Case SyntaxType.Sub_Operator
                                Return _Left - _Right
                        End Select
                    Else
                    End If
                    Dim x As New DiagnosticsException("Unable to evaluate AddativeExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                    _Diagnostics.Add(x.ToJson)
                    Return Nothing
                End Function
                Public Function EvaluateMultiplicativeExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType.AddativeExpression Then
                        Dim b As SyntaxNodes.BinaryExpression = Expr
                        Dim _Left As Integer = b._Left.Evaluate(Env)
                        Dim _Right As Integer = b._Right.Evaluate(Env)
                        Select Case b._Operator._SyntaxType
                            Case SyntaxType.Multiply_Operator
                                Return _Left * _Right
                            Case SyntaxType.Divide_Operator
                                Return _Left / _Right
                        End Select
                    Else
                    End If
                    Dim x As New DiagnosticsException("Unable to evaluate MultiplicativeExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                    _Diagnostics.Add(x.ToJson)
                    Return Nothing
                End Function
                Public Function EvaluateConditionalExpression(ByRef Expr As SyntaxNode) As Boolean
                    If Expr._SyntaxType = SyntaxType.ConditionalExpression Then
                        Dim b As SyntaxNodes.BinaryExpression = Expr
                        Dim _Left As Boolean = b._Left.Evaluate(Env)
                        Dim _Right As Boolean = b._Right.Evaluate(Env)
                        Select Case b._Operator._SyntaxType
                            Case SyntaxType.GreaterThan_Operator
                                Return _Left > _Right
                            Case SyntaxType.LessThanOperator
                                Return _Left < _Right
                            Case SyntaxType.NotEqual
                                Return _Left <> _Right
                            Case SyntaxType.EquivelentTo
                                Return _Left = _Right
                            Case SyntaxType.LessThanEquals
                                Return _Left <= _Right
                            Case SyntaxType.GreaterThanEquals
                                Return _Left >= _Right
                        End Select
                    Else
                    End If
                    Dim x As New DiagnosticsException("Unable to evaluate ConditionalExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                    _Diagnostics.Add(x.ToJson)
                    Return Nothing
                End Function
                Public Function EvaluateIfExpression(ByRef Expr As SyntaxNode) As Boolean
                    If Expr._SyntaxType = SyntaxType.ifElseExpression Then

                        Select Case Expr._SyntaxType

                            Case SyntaxType.ifElseExpression
                                Dim i As IfElseExpression = Expr

                                Return i.Evaluate(Env)
                            Case SyntaxType.ifThenExpression
                                Dim i As IfThenExpression = Expr
                                Return i.Evaluate(Env)
                        End Select
                    Else
                    End If
                    Dim x As New DiagnosticsException("Unable to evaluate ConditionalExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                    _Diagnostics.Add(x.ToJson)
                    Return Nothing
                End Function
                Private Function DisplayDiagnostics(ByRef UserInput_LINE As String) As Boolean
                    Console.ForegroundColor = ConsoleColor.Red
                    'Catch Errors
                    If _tree.Diagnostics.Count > 0 Then
                        _Diagnostics = _tree.Diagnostics
                        'Tokens
                        Console.ForegroundColor = ConsoleColor.Red
                        'PARSER DIAGNOSTICS
                        Console.WriteLine("Compiler Errors: " & vbNewLine)
                        For Each item In _tree.Diagnostics
                            Console.ForegroundColor = ConsoleColor.DarkRed
                            Console.WriteLine(item & vbNewLine)
                        Next
                        'Tokens


                        Evaluateable = True
                        Return True

                    Else
                        Evaluateable = False
                        Return False

                    End If
                End Function

                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
#Region "TOSTRING"
                ''' <summary>
                ''' Serializes object to json
                ''' </summary>
                ''' <returns> </returns>
                Public Function ToJson() As String
                    Return FormatJsonOutput(ToString)
                End Function
                Public Overrides Function ToString() As String
                    Dim Converter As New JavaScriptSerializer
                    Return Converter.Serialize(Me)
                End Function
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
#End Region
            End Class
        End Namespace
    End Namespace
End Namespace


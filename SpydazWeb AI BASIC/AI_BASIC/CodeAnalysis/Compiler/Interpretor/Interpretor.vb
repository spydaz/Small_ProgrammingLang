'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Evaluation\EvaluatorII.vb
'
' summary:	Evaluator ii class
'---------------------------------------------------------------------------------------------------

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Interpretor
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   An Interpretor . </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Friend Class Interpretor
                ''' <summary>   The environment. </summary>
                Private Env As New EnvironmentalMemory
                ''' <summary>   The evaluator diagnostics. </summary>
                Private EvaluatorDiagnostics As New List(Of DiagnosticsException)
                ''' <summary>   List of results. </summary>
                Private ResultsList As New List(Of Object)



                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate program. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Script">   [in,out] The script. </param>
                ''' <param name="Program">  [in,out] The program. </param>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateProgram(ByRef Script As String, ByRef Program As List(Of SyntaxTree))
                    Env = New EnvironmentalMemory
                    If Program IsNot Nothing Then
                        For Each item In Program
                            ResultsList.AddRange(_EvaluateLine(item, Env))
                        Next
                    End If
                    Return ResultsList
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate line. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="_tree">    [in,out] The tree. </param>
                ''' <param name="Env">      [in,out] The environment. </param>
                '''
                ''' <returns>   An Object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function _EvaluateLine(ByRef _tree As SyntaxTree, ByRef Env As EnvironmentalMemory) As Object
                    Dim results As New List(Of Object)
                    Me.Env = Env
                    For Each item In _tree.Body
                        results.Add(_EvaluateExpresssion(item))

                    Next
                    Return results
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate line. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="_tree">    [in,out] The tree. </param>
                '''
                ''' <returns>   An Object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function _EvaluateLine(ByRef _tree As SyntaxTree) As Object
                    Dim results As New List(Of Object)
                    Me.Env = New EnvironmentalMemory
                    For Each item In _tree.Body
                        results.Add(_EvaluateExpresssion(item))

                    Next
                    Return results
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate expresssion. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="iNode">    [in,out] Zero-based index of the node. </param>
                '''
                ''' <returns>   An Object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    EvaluatorDiagnostics.Add(DiagExe)
                    Return "Unable to Evaluate"
                End Function
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
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
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
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
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
#End Region

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets debugger display. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   The debugger display. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate string expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   A String. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateStringExpression(ByRef Expr As SyntaxNode) As String
                    If Expr._SyntaxType = SyntaxType._StringExpression Then
                        Dim n As SyntaxNodes.StringExpression = Expr
                        Return n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate StringExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate numeric literal expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   An Integer. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function Evaluate_NumericLiteralExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType._NumericLiteralExpression Then
                        Dim n As SyntaxNodes.NumericalExpression = Expr
                        Return New Integer = n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate NumericalExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate boolean literal expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateBooleanLiteralExpression(ByRef Expr As SyntaxNode) As Boolean
                    If Expr._SyntaxType = SyntaxType._BooleanLiteralExpression Then
                        Dim n As SyntaxNodes.BooleanLiteralExpression = Expr
                        Return New Boolean = n.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate BooleanLiteralExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate unary expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   An Integer. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateUnaryExpression(ByRef Expr As SyntaxNode) As Integer
                    If Expr._SyntaxType = SyntaxType._UnaryExpression Then
                        Dim u As SyntaxNodes.UnaryExpression = Expr
                        Return u.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate UnaryExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate identifier expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateIdentifierExpression(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._IdentifierExpression Then
                        Dim i As SyntaxNodes.IdentifierExpression = Expr
                        Return i.Evaluate(Env)

                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate IdentifierExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate variable declaration. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////

                Public Function EvaluateVariableDeclaration(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._VariableDeclaration Then
                        Dim i As SyntaxNodes.VariableDeclarationExpression = Expr
                        Return i.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate VariableDeclaration", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate assignment expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   . </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function EvaluateAssignmentExpression(ByRef Expr As SyntaxNode)
                    If Expr._SyntaxType = SyntaxType._AssignmentExpression Then
                        Dim i As SyntaxNodes.AssignmentExpression = Expr
                        Return i.Evaluate(Env)
                    Else
                        Dim x As New DiagnosticsException("Unable to evaluate _AssignmentExpression", ExceptionType.EvaluationException, Expr.ToJson, Expr._SyntaxType)
                        EvaluatorDiagnostics.Add(x)
                        Return Nothing
                    End If
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate addative expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   An Integer. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    EvaluatorDiagnostics.Add(x)
                    Return Nothing
                End Function

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate multiplicative expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   An Integer. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    EvaluatorDiagnostics.Add(x)
                    Return Nothing
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate conditional expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    EvaluatorDiagnostics.Add(x)
                    Return Nothing
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Evaluate if expression. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Expr"> [in,out] The expression. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                    EvaluatorDiagnostics.Add(x)
                    Return Nothing
                End Function
            End Class
        End Namespace
    End Namespace
End Namespace


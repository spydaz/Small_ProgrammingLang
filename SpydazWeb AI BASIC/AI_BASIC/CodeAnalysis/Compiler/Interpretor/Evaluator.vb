Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Interpretor
            Public Class Evaluator
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
                Public Function _Evaluate() As Object
                    For Each item In _tree.Body
                        Dim result = _EvaluateExpresssion(item)
                        If _Diagnostics.Count > 0 Then

                            Return "Evaluation Error" & _tree.ToJson
                        End If
                        Return result
                    Next
                    Return "Unable to Evaluate"
                End Function
                Public Function _EvaluateExpresssion(ByRef iNode As Object) As Object
                    If iNode IsNot Nothing Then
                        If iNode._SyntaxType = SyntaxType._NumericLiteralExpression Then
                            Dim n As SyntaxNodes.NumericalExpression = iNode
                            Return n.Evaluate(Env)
                        Else

                        End If

                        If iNode._SyntaxType = SyntaxType._StringExpression Then
                            Dim n As SyntaxNodes.StringExpression = iNode
                            Return n.Evaluate(Env)
                        Else

                        End If
                        If iNode._SyntaxType = SyntaxType._BooleanLiteralExpression Then
                            Dim n As SyntaxNodes.BooleanLiteralExpression = iNode
                            Return n.Evaluate(Env)
                        Else

                        End If
                        If iNode._SyntaxType = SyntaxType._UnaryExpression Then
                            Dim u As SyntaxNodes.UnaryExpression = iNode
                            Return u.Evaluate(Env)
                        Else

                        End If
                        If iNode._SyntaxType = SyntaxType._IdentifierExpression Then
                            Dim i As SyntaxNodes.IdentifierExpression = iNode
                            Return i.Evaluate(Env)

                        End If
                        Try


                            If iNode._SyntaxType = SyntaxType._BinaryExpression Then
                                Dim b As SyntaxNodes.BinaryExpression = iNode
                                Dim _Left As Integer = b._Left.Evaluate(Env)
                                Dim _Right As Integer = b._Right.Evaluate(Env)

                                Select Case iNode._Operator._SyntaxType
                                    Case SyntaxType.Add_Operator
                                        Return _Left + _Right
                                    Case SyntaxType.Sub_Operator
                                        Return _Left - _Right
                                    Case SyntaxType.Multiply_Operator
                                        Return _Left * _Right
                                    Case SyntaxType.Divide_Operator
                                        Return _Left / _Right
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
                                    Case Else
                                        Throw New Exception("Unexpected Binary Operator :" & iNode._Operator._SyntaxStr)
                                End Select

                            Else

                            End If
                        Catch ex As Exception
                            _Diagnostics.Add("Unexpected Exception :" & vbNewLine & ex.ToString)
                        End Try

                    End If
                    _Diagnostics.Add("Unexpected Expression :")
                    Return "Unexpected Expression :"
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
            End Class
        End Namespace
    End Namespace
End Namespace


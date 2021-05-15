Imports System.Linq.Expressions
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Compiler

        Friend Class Evaluator
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
                    Return _EvaluateExpresssion(item)
                Next
                Return "Evaluation Error"
            End Function
            Public Function _EvaluateExpresssion(ByRef iNode As Object) As Object

                If iNode._SyntaxType = SyntaxType._Integer Then

                    Return iNode._Literal._value
                Else

                End If
                If iNode.SyntaxType = SyntaxType._Decimal Then
                    Dim i As NumericalExpression = iNode
                    Return i._Literal
                Else

                End If
                If iNode.SyntaxType = SyntaxType._BinaryExpression Then
                    Dim b As SyntaxNodes.BinaryExpression = iNode
                    Dim _Left As Integer = _EvaluateExpresssion(b._Left)
                    Dim _Right As Integer = _EvaluateExpresssion(b._Right)

                    Select Case iNode._Operator._SyntaxType
                        Case SyntaxType.Add_Operator
                            Return _Left + _Right
                        Case SyntaxType.Sub_Operator
                            Return _Left - _Right
                        Case SyntaxType.Multiply_Operator
                            Return _Left * _Right
                        Case SyntaxType.Divide_Operator
                            Return _Left / _Right
                        Case Else
                            Throw New Exception("Unexpected Binary Operator :" & iNode._Operator._SyntaxStr)
                    End Select

                Else

                End If

                Throw New Exception("Unexpected Expression :" & iNode.SyntaxTypeStr)
                Return 0
            End Function
            Public Function RunScript(ByRef UserInput_LINE As String) As String
                ':::_EVALUATE_::: 
                If Evaluateable = True Then
                    If DisplayDiagnostics(UserInput_LINE) = False Then
                        'No Errors then Evaluate
                        Console.ForegroundColor = ConsoleColor.Green
                        Dim Eval As New Evaluator(_tree)
                        Dim Result = Eval._Evaluate
                        Return Result
                    End If

                Else
                    'Already displayed diagnostics
                End If
                Return _Diagnostics.ToString
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



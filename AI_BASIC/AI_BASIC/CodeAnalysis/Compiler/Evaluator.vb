Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler

        Public Class Evaluator
            Public _tree As SyntaxTree
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

                If iNode.SyntaxType = SyntaxType._NumericLiteralExpression Then

                    Return iNode.NumberToken._Value
                Else

                End If
                If iNode.SyntaxType = SyntaxType._BinaryExpression Then

                    Dim _Left As Integer = _EvaluateExpresssion(iNode._Left)
                    Dim _Right As Integer = _EvaluateExpresssion(iNode._Right)

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

        End Class
    End Namespace
End Namespace



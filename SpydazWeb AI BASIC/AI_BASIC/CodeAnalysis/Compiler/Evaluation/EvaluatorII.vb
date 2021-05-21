﻿Imports System.Runtime.InteropServices
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
            Public Class EvaluatorII
                Private Env As New EnvironmentalMemory
                Private EvaluatorDiagnostics As New List(Of DiagnosticsException)
                Private ResultsList As New List(Of Object)
                Public Function EvaluateProgram(ByRef Script As String, ByRef Program As List(Of SyntaxTree))
                    Env = New EnvironmentalMemory
                    For Each item In Program
                        ResultsList.AddRange(_EvaluateLine(item, Env))
                    Next
                    Return ResultsList
                End Function
                Public Function _EvaluateLine(ByRef _tree As SyntaxTree, ByRef Env As EnvironmentalMemory) As Object
                    Dim results As New List(Of Object)
                    Me.Env = Env
                    For Each item In _tree.Body
                        results.Add(_EvaluateExpresssion(item))

                    Next
                    Return results
                End Function
                Public Function _EvaluateLine(ByRef _tree As SyntaxTree) As Object
                    Dim results As New List(Of Object)
                    Me.Env = New EnvironmentalMemory
                    For Each item In _tree.Body
                        results.Add(_EvaluateExpresssion(item))

                    Next
                    Return results
                End Function
                Private Function _EvaluateExpresssion(ByRef iNode As SyntaxNode) As Object
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
                        If iNode._SyntaxType = SyntaxType._VariableDeclaration Then
                            Dim i As SyntaxNodes.VariableDeclarationExpression = iNode
                            Return i.Evaluate(Env)

                        End If
                        If iNode._SyntaxType = SyntaxType._AssignmentExpression Then
                            Dim i As SyntaxNodes.AssignmentExpression = iNode
                            Return i.Evaluate(Env)

                        End If
                        Try


                            If iNode._SyntaxType = SyntaxType._BinaryExpression Then
                                Dim b As SyntaxNodes.BinaryExpression = iNode
                                Dim _Left As Integer = b._Left.Evaluate(Env)
                                Dim _Right As Integer = b._Right.Evaluate(Env)

                                Select Case b._Operator._SyntaxType
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
                                        Dim zexe As New DiagnosticsException("Unexpected Binary Operator :", ExceptionType.EvaluationException, iNode, SyntaxType._BinaryExpression)
                                        EvaluatorDiagnostics.Add(zexe)
                                End Select

                            Else

                            End If
                        Catch ex As Exception
                            Dim exe As New DiagnosticsException("Unexpected Exception :", ExceptionType.EvaluationException, ex.ToString, SyntaxType._String)
                            EvaluatorDiagnostics.Add(exe)
                        End Try

                    End If
                    Dim iexe As New DiagnosticsException("Unexpected Expression :", ExceptionType.EvaluationException, iNode, iNode._SyntaxType)
                    EvaluatorDiagnostics.Add(iexe)
                    Return "Unable to Evaluate"
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
                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
            End Class
        End Namespace
    End Namespace
End Namespace

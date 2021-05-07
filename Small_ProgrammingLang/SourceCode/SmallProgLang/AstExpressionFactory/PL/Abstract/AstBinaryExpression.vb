Imports Microsoft.VisualBasic.CompilerServices
Imports SDK.SAL
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Used for Binary Operations
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class AstBinaryExpression
            Inherits AstExpression
            Public _Left As AstExpression
            Public _Right As AstExpression
            Public _Operator As String
            Public LocalEnvironment As New EnvironmentalMemory
            Public Sub New(ByRef nType As AST_NODE, ByRef nLeft As AstExpression, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(nType)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "BinaryExpression"
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                lst.Add(_Operator)
                lst.AddRange(_Left.ToArraylist)
                lst.AddRange(_Right.ToArraylist)
                Return lst
            End Function

            Public Sub New(ByRef nType As AST_NODE, ByRef nLeft As Ast_VariableDeclarationExpression, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(nType)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "BinaryExpression"
            End Sub
            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Select Case _Operator
'Mathmatical
                    Case "+"
                        Return EvaluateAddative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "-"
                        Return EvaluateAddative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "*"
                        Return EvaluateMultiplicative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
                    Case "/"
                        Return EvaluateMultiplicative(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)
'Relational

                    Case ">="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "<="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case ">"
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "<"
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "="
                        Return EvaluateBoolean(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

'Complex assign
                    Case "+="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "-="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "*="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)

                    Case "/="
                        Return EvaluateComplex(_Left.GetValue(ParentEnv), _Operator, _Right.GetValue(ParentEnv), ParentEnv)


                End Select
                Return ParentEnv
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Me.LocalEnvironment = ParentEnv

                Return GetValue(ParentEnv)


            End Function
            ''' <summary>
            ''' Allows for evaluation of the node : Imeadialty invoked expression
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="iOperator"></param>
            ''' <param name="Right"></param>
            ''' <returns></returns>
            Private Function EvaluateMultiplicative(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer

                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "*"
                            Return (Left.GetValue(ParentEnv) * Right.GetValue(ParentEnv))
                        Case "/"
                            Return (Left.GetValue(ParentEnv) / Right.GetValue(ParentEnv))
                    End Select


                End If
                Return (Left.GetValue(ParentEnv))
            End Function
            Private Function EvaluateAddative(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer

                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "+"
                            Return (Left.GetValue(ParentEnv) + Right.GetValue(ParentEnv))
                        Case "-"
                            Return (Left.GetValue(ParentEnv) - Right.GetValue(ParentEnv))
                    End Select


                End If
                Return (Left.GetValue(ParentEnv))
            End Function
            ''' <summary>
            ''' Evaluate node values ( imeadiatly invoked expression )
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="iOperator"></param>
            ''' <param name="Right"></param>
            ''' <returns></returns>
            Private Function EvaluateBoolean(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Boolean


                Select Case iOperator
                        Case ">="
                            Return (Left.GetValue(ParentEnv) >= Right.GetValue(ParentEnv))
                        Case "<="
                            Return (Left.GetValue(ParentEnv) <= Right.GetValue(ParentEnv))
                        Case ">"
                            Return (Left.GetValue(ParentEnv) > Right.GetValue(ParentEnv))
                        Case "<"
                            Return (Left.GetValue(ParentEnv) < Right.GetValue(ParentEnv))
                        Case "="
                            Return (Left.GetValue(ParentEnv) = Right.GetValue(ParentEnv))

                    End Select



                Return (Left.GetValue(ParentEnv))
            End Function
            Private Function EvaluateComplex(ByRef Left As Ast_Literal, ByRef iOperator As String, ByRef Right As Ast_Literal, ByRef ParentEnv As EnvironmentalMemory) As Integer
                If Left._Type = AST_NODE._integer And Right._Type = AST_NODE._integer Then
                    Select Case iOperator
                        Case "+="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf += rt
                            Return lf
                        Case "-="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf -= rt
                            Return lf
                        Case "*="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf *= rt
                            Return lf
                        Case "/="
                            Dim lf = Integer.Parse(((Left.GetValue(ParentEnv))))
                            Dim rt = Integer.Parse(((Right.GetValue(ParentEnv))))
                            lf /= rt
                            Return lf


                    End Select


                End If
                Return Integer.Parse(((Left.GetValue(ParentEnv))))
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace

End Namespace
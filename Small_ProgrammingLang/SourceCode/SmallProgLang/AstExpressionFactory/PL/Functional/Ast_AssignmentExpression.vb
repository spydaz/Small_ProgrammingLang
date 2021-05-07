Imports SDK.SAL
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_AssignmentExpression
            Inherits AstExpression
            ''' <summary>
            ''' Must be var literal type
            ''' </summary>
            Public _Left As Ast_Identifier
            Public _Right As AstExpression
            Public _Operator As String
            Public Sub New(ByRef nLeft As Ast_Identifier, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(AST_NODE._assignExpression)
                Me._Left = nLeft
                Me._Right = nRight
                Me._Operator = nOperator
                Me._Raw = nLeft._Raw & nOperator & nRight._Raw
                Me._Start = nLeft._Start
                Me._End = nRight._End
                Me._TypeStr = "_assignExpression"
            End Sub

            Public Overrides Function ToArrayList() As List(Of String)

                Dim lst As List(Of String) = MyBase.ToArraylist
                lst.Add(_Operator)
                lst.Add(_Left._Name.ToString)
                lst.AddRange(_Right.ToArraylist)
                Return lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Function GetVar(ByRef ParentEnv As EnvironmentalMemory) As EnvironmentalMemory.Variable
                Dim nvar As New EnvironmentalMemory.Variable
                nvar.Value = _Right.Evaluate(ParentEnv)
                nvar.Name = _Left._Name
                nvar.Type = Me._Type
                Return nvar
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Dim nvar As New EnvironmentalMemory.Variable
                nvar.Value = _Right.Evaluate(ParentEnv)
                nvar.Name = _Left._Name
                nvar.Type = Me._Type
                If ParentEnv.CheckVar(nvar.Name) = True Then
                    Return _Right.Evaluate(ParentEnv)
                End If
                Return Nothing
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace

End Namespace
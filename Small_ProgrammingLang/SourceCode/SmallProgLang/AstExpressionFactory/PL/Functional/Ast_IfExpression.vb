
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang
    'if (x>7) then { x = 1; } else { x = 2; }
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_IfExpression
            Inherits AstExpression
            Public Test As AstExpression
            Public Consequent As Ast_BlockExpression
            Public Alternate As Ast_BlockExpression

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace



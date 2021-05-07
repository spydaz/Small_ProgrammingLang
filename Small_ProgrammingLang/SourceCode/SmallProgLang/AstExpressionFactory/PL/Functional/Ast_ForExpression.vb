
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory

        'for (dim i = 0); (i < 10); (i += 1) {      x += i;    }
        'for (Init); (Test); (Increment) { <BODY >    x += i;    }
        ''' <summary>
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_ForExpression
            Inherits AstExpression


            Public Init As Ast_VariableDeclarationExpression
            Public Test As AstBinaryExpression
            Public Increment As AstExpression
            Public Body As Ast_BlockExpression

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
                Return ToString()
            End Function
        End Class
    End Namespace
End Namespace


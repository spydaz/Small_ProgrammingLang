Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        'DO_
        '   while (x > 10) {      x -= 1;    } 
        '   Until (x > 10) {      x -= 1;    } 
        ''' <summary>
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_DoExpression
            Inherits AstExpression
            Public Body As Ast_BlockExpression
            Public Test As AstBinaryExpression

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As SmallProgLang.Evaluator.EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As SmallProgLang.Evaluator.EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace


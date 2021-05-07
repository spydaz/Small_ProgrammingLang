Imports SDK.SmallProgLang.Ast_ExpressionFactory


Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_SAL_Literal
            Inherits Ast_Literal

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Sub New(ByRef ntype As AST_NODE, ByRef nValue As Object)
                MyBase.New(ntype, nValue)
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToJson
            End Function
        End Class
    End Namespace
End Namespace

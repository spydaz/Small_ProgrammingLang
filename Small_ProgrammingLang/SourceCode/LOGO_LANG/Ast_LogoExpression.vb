Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang
    Namespace Ast_ExpressionFactory
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_Logo_Value
            Inherits Ast_Literal

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub

            Public Sub New(ByRef ntype As AST_NODE, ByRef nValue As Object)
                MyBase.New(ntype, nValue)
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToJson()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_LogoIdentifer
            Inherits Ast_Identifier

            Public Sub New(ByRef nName As String)
                MyBase.New(nName)
                Me._TypeStr = "_LogoIdentifer"
            End Sub

            Private Function GetDebuggerDisplay() As String
                Return ToJson()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_LogoCmdExpression
            Inherits AstExpression
            Public _Left_Cmd As Ast_Identifier
            Public _Right_Value As Ast_Literal

            Public Sub New(ByRef ntype As AST_NODE, ByRef _left As Ast_Identifier, _Right As Ast_Literal)
                MyBase.New(ntype)
                Me._TypeStr = "_LogoCmdExpression"
                Me._Left_Cmd = _left
                Me._Right_Value = _Right
            End Sub

            ''' <summary>
            ''' Evaluates the expression and returns the value
            ''' 
            ''' </summary>
            ''' <param name="ParentEnv"></param>
            ''' <returns></returns>
            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Left_Cmd._Name & " " & _Right_Value.GetValue(ParentEnv)
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_logoEvaluation
            Inherits AstBinaryExpression

            Public Sub New(ByRef nType As AST_NODE, ByRef nLeft As AstExpression, ByRef nOperator As String, ByRef nRight As AstExpression)
                MyBase.New(nType, nLeft, nOperator, nRight)
                Me._TypeStr = "_logoEvaluation"
            End Sub


        End Class
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_Logo_Expression
            Inherits AstExpression
            Public _value As Ast_Literal
            Public Sub New(ByRef ivalue As AstNode)
                MyBase.New(AST_NODE.Logo_Expression)
                Me._TypeStr = "_Logo_Expression"
            End Sub

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Overrides Function GenerateSalCode(ByRef ParentEnv As EnvironmentalMemory) As String
                Throw New NotImplementedException()
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _value.iLiteral
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace
End Namespace





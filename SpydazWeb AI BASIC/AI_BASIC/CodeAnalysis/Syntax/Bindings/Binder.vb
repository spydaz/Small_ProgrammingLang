
Imports System.Linq.Expressions
Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.Bindings.BoundNodes
Imports AI_BASIC.Syntax.SyntaxNodes

Namespace CodeAnalysis
    Namespace Syntax
        Namespace Bindings
            Friend Class Binder
                Public BoundTree As BoundSyntaxTree
                Private ExpressionSyntaxTree As SyntaxTree
                Private BoundStatements As List(Of ExpressionSyntaxNode)
                Public _Diagnostics As List(Of String)
                Public Sub New(ByRef _SyntaxTree As SyntaxTree)
                    _Diagnostics = New List(Of String)
                    BoundTree = New BoundSyntaxTree
                    BoundStatements = New List(Of ExpressionSyntaxNode)
                    ExpressionSyntaxTree = _SyntaxTree
                    BoundTree.ExpressionSyntaxStatments = _SyntaxTree.Body
                    BoundTree.SyntaxTokens = _SyntaxTree.Tokens
                    Bind()
                End Sub


                Public Sub Bind()
                    For Each item In ExpressionSyntaxTree.Body
                        Select Case item._SyntaxType
                            Case SyntaxType._NumericLiteralExpression
                                BindLiteralExpression(item)
                            Case SyntaxType._StringExpression
                                BindLiteralExpression(item)
                            Case SyntaxType._BooleanLiteralExpression
                                BindLiteralExpression(item)
                            Case SyntaxType._null
                                BindLiteralExpression(item)
                            Case SyntaxType._IdentifierExpression
                                BindLiteralExpression(item)
                            Case SyntaxType._IdentifierExpression
                                BindLiteralExpression(item)
                            Case SyntaxType._VariableDeclaration
                                BindLiteralExpression(item)
                        End Select
                    Next
                End Sub
                Public Sub BindLiteralExpression(ByRef Expression As ExpressionSyntaxNode)
                    Select Case Expression._SyntaxType
                        Case SyntaxType._NumericLiteralExpression
                            BoundStatements.Add(New Bound_LiteralExpression(Expression))
                        Case SyntaxType._StringExpression
                            BoundStatements.Add(New Bound_LiteralExpression(Expression))
                        Case SyntaxType._BooleanLiteralExpression
                            BoundStatements.Add(New Bound_LiteralExpression(Expression))
                        Case SyntaxType._null
                            BoundStatements.Add(New Bound_LiteralExpression(Expression))
                        Case SyntaxType._IdentifierExpression
                            BoundStatements.Add(New Bound_IdentifierExpression(Expression))
                        Case SyntaxType._VariableDeclaration
                            BoundStatements.Add(New Bound_IdentifierExpression(Expression))

                    End Select

                End Sub
            End Class
            Public Class BoundSyntaxTree
                Public BoundStatements As List(Of ExpressionSyntaxNode)
                Public ExpressionSyntaxStatments As List(Of SyntaxNode)
                Public SyntaxTokens As List(Of SyntaxToken)
            End Class

        End Namespace
    End Namespace
End Namespace
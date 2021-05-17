﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

  Partial Public NotInheritable Class ParenExpressionSyntax
    Inherits ExpressionSyntax

    Sub New(tree As SyntaxTree, openParenToken As SyntaxToken, expression As ExpressionSyntax, closeParenToken As SyntaxToken)
      MyBase.New(tree)
      Me.OpenParenToken = openParenToken
      Me.Expression = expression
      Me.CloseParenToken = closeParenToken
    End Sub

    Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.ParenExpression
    Public ReadOnly Property OpenParenToken As SyntaxToken
    Public ReadOnly Property Expression As ExpressionSyntax
    Public ReadOnly Property CloseParenToken As SyntaxToken

  End Class

End Namespace
﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

  Partial Public NotInheritable Class CallExpressionSyntax
    Inherits ExpressionSyntax

    Sub New(tree As SyntaxTree, identifier As SyntaxToken, openParen As SyntaxToken, arguments As SeparatedSyntaxList(Of ExpressionSyntax), closeParen As SyntaxToken)
      MyBase.New(tree)
      Me.Identifier = identifier
      Me.OpenParen = openParen
      Me.Arguments = arguments
      Me.CloseParen = closeParen
    End Sub

    Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.CallExpression
    Public ReadOnly Property Identifier As SyntaxToken
    Public ReadOnly Property OpenParen As SyntaxToken
    Public ReadOnly Property Arguments As SeparatedSyntaxList(Of ExpressionSyntax)
    Public ReadOnly Property CloseParen As SyntaxToken

  End Class

End Namespace
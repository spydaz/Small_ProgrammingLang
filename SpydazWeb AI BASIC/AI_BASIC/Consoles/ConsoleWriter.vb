Imports System.Drawing
Imports System.IO
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes


Namespace Consoles
    Public Class ConsoleWriter
        Friend Shared Sub WriteSyntaxToken(ByRef Token As SyntaxToken)
            Select Case Token._SyntaxType
                Case SyntaxType._UnknownToken
                    Console.ForegroundColor = ConsoleColor.Magenta
                    Console.WriteLine(Token.ToJson)
                Case Else

                    If IsKeyWord(Token._SyntaxType) = True Then
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.WriteLine(Token.ToJson)
                    End If
                    If IsNumber(Token._SyntaxType) = True Then
                        Console.ForegroundColor = ConsoleColor.Yellow
                        Console.WriteLine(Token.ToJson)
                    End If
                    If Token._SyntaxType = SyntaxType._Identifier Then
                        Console.ForegroundColor = ConsoleColor.Yellow
                        Console.WriteLine(Token.ToJson)
                    End If
                    If Token._SyntaxType = SyntaxType._String Then
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.WriteLine(Token.ToJson)
                    End If

                    Console.ForegroundColor = ConsoleColor.White
                    Console.WriteLine(Token.ToJson)
            End Select
        End Sub
        Friend Shared Sub WriteTokenList(ByRef Lst As List(Of SyntaxToken))
            For Each item In Lst
                WriteSyntaxToken(item)
            Next
        End Sub
        Public Shared Sub WriteExpressionList(ByRef Lst As List(Of SyntaxNode))
            For Each item In Lst
                WriteExpressionSyntaxToken(item)
            Next
        End Sub
        Private Shared Sub WriteExpressionSyntaxToken(ByRef Expression As ExpressionSyntaxNode)
            Select Case Expression._SyntaxType
                Case SyntaxType._NumericLiteralExpression
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._StringExpression
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._BooleanLiteralExpression
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._IdentifierExpression
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._BinaryExpression
                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._VariableDeclaration
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine(Expression.ToJson)
                Case SyntaxType._AssignmentExpression
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine(Expression.ToJson)
                Case Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(Expression.ToJson)
            End Select
        End Sub
        Friend Shared Sub WriteKeyword(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.Blue
            Console.Write(text)
            Console.ResetColor()
        End Sub
        Friend Shared Sub WriteIdentifier(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.Write(text)
            Console.ResetColor()
        End Sub
        Friend Shared Sub WriteNumber(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.Write(text)
            Console.ResetColor()
        End Sub
        Public Shared Sub WriteDiagnostics(ByRef text As String)
            If text = "NO ERRORS" Then
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.Write("Compiled without Errors")
                Console.ResetColor()
            Else
                Console.ForegroundColor = ConsoleColor.Magenta
                Console.Write(text)
                Console.ResetColor()
            End If
        End Sub
    End Class
End Namespace

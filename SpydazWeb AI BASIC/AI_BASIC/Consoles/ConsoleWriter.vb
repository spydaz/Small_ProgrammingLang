'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\ConsoleWriter.vb
'
' summary:	Console writer class
'---------------------------------------------------------------------------------------------------

Imports System.Drawing
Imports System.IO
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports AI_BASIC.Syntax
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.Typing

Namespace Consoles

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A console writer. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend Class ConsoleWriter

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a syntax token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Token">    [in,out] The token. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a token list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Lst">  [in,out] The list. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Shared Sub WriteTokenList(ByRef Lst As List(Of SyntaxToken))
            For Each item In Lst
                WriteSyntaxToken(item)
            Next
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an expression list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Lst">  [in,out] The list. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Shared Sub WriteExpressionList(ByRef Lst As List(Of SyntaxNode))
            For Each item In Lst
                WriteExpressionSyntaxToken(item)
            Next
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an expression syntax token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="Expression">   [in,out] The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a keyword. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> [in,out] The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Shared Sub WriteKeyword(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.Blue
            Console.Write(text)
            Console.ResetColor()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an identifier. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> [in,out] The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Shared Sub WriteIdentifier(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.Write(text)
            Console.ResetColor()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a number. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> [in,out] The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Shared Sub WriteNumber(ByRef text As String)
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.Write(text)
            Console.ResetColor()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes the diagnostics. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> [in,out] The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
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

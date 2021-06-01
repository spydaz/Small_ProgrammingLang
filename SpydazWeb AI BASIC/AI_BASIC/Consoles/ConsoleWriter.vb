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
Imports BinaryExpression = AI_BASIC.Syntax.SyntaxNodes.BinaryExpression

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



            If IsKeyWord(Token._SyntaxType) = True Then
                Console.ForegroundColor = ConsoleColor.Blue
                Console.WriteLine(Token.ToJson)
            ElseIf IsNumber(Token._SyntaxType) = True Then
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(Token.ToJson)
            ElseIf Token._SyntaxType = SyntaxType._Identifier Then
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(Token.ToJson)
            ElseIf Token._SyntaxType = SyntaxType._String Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(Token.ToJson)
            ElseIf Token._SyntaxType = SyntaxType._UnknownToken Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(Token.ToJson)
            Else
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(Token.ToJson)

            End If


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
            If Expression IsNot Nothing Then
                Try
                    Select Case Expression._SyntaxType
                        Case SyntaxType._NumericLiteralExpression
                            WriteNumber(Expression.ToJson)
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
                            WriteBinaryExpression(Expression)
                        Case SyntaxType.AddativeExpression
                            WriteBinaryExpression(Expression)
                        Case SyntaxType.MultiplicativeExpression
                            WriteBinaryExpression(Expression)
                        Case SyntaxType.ConditionalExpression
                            WriteBinaryExpression(Expression)
                        Case SyntaxType._VariableDeclaration
                            Console.ForegroundColor = ConsoleColor.Green
                            Console.WriteLine(Expression.ToJson)
                        Case SyntaxType._AssignmentExpression
                            Console.ForegroundColor = ConsoleColor.Yellow
                            Console.WriteLine(Expression.ToJson)
                        Case SyntaxType.ifThenExpression
                            WriteIfCondition(Expression)
                        Case SyntaxType.ifElseExpression
                            WriteIfCondition(Expression)
                        Case SyntaxType._CodeBlock
                            WriteCodeBlockExpression(Expression)
                        Case SyntaxType.ForExpression
                            Console.WriteLine(Expression.ToJson)
                        Case SyntaxType.DO_UntilExpression
                            WriteDoExpression(Expression)
                        Case SyntaxType.DO_WhileExpression
                            WriteDoExpression(Expression)
                        Case Else
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.WriteLine(Expression.ToJson)
                    End Select
                Catch ex As Exception

                End Try
            End If
            Console.ResetColor()
        End Sub
        Private Shared Sub WriteDoExpression(ByRef Expression As ExpressionSyntaxNode)
            Console.ForegroundColor = ConsoleColor.Blue
            Console.WriteLine(" Syntax TypeStr : " & Expression._SyntaxTypeStr)
            Select Case Expression._SyntaxType

                Case SyntaxType.DO_UntilExpression
                    Dim d1_ As UntilExpression = Expression
                    WriteExpressionSyntaxToken(d1_.Condition)
                    WriteExpressionSyntaxToken(d1_.Body)

                Case SyntaxType.DO_WhileExpression
                    Dim d2_ As UntilExpression = Expression
                    WriteExpressionSyntaxToken(d2_.Condition)
                    WriteExpressionSyntaxToken(d2_.Body)

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
            Console.ForegroundColor = ConsoleColor.DarkGreen
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
                Console.ForegroundColor = ConsoleColor.DarkGray
                Console.Write("Compiled without Errors")
                Console.ResetColor()
            Else
                Console.ForegroundColor = ConsoleColor.Magenta
                Console.Write(text)
                Console.ResetColor()
            End If
        End Sub
        Private Shared Sub WriteIfCondition(ByRef Expression As ExpressionSyntaxNode)

            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine(" Syntax TypeStr : " & Expression._SyntaxTypeStr)

            If Expression._SyntaxType = SyntaxType.ifThenExpression Then
                Console.ForegroundColor = ConsoleColor.Yellow
                Dim i As IfThenExpression = Expression
                WriteExpressionSyntaxToken(i.IfCondition)
                Console.ForegroundColor = ConsoleColor.Cyan
                WriteExpressionSyntaxToken(i.ThenCondition)

            Else
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Dim i As IfElseExpression = Expression
                WriteExpressionSyntaxToken(i.IfCondition)
                Console.ForegroundColor = ConsoleColor.Cyan
                WriteExpressionSyntaxToken(i.ThenCondition)
                Console.ForegroundColor = ConsoleColor.White
                WriteExpressionSyntaxToken(i.ElseCondition)

            End If

            Console.ResetColor()
        End Sub
        Private Shared Sub WriteCodeBlockExpression(ByRef Expression As ExpressionSyntaxNode)
            Dim c As CodeBlockExpression = Expression
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine()
            Console.WriteLine(" Syntax TypeStr : " & Expression._SyntaxTypeStr)
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("     LocalMemory : ")
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine(c.LocalMemory.ToJson & vbNewLine)

            For Each item In c.Body
                WriteExpressionSyntaxToken(item)
            Next
            Console.ResetColor()
        End Sub
        Private Shared Sub WriteBinaryExpression(ByRef Expression As ExpressionSyntaxNode)
            Dim b As BinaryExpression = Expression
            Dim _Left As ExpressionSyntaxNode = b._Left
            Dim _Right As ExpressionSyntaxNode = b._Right
            Dim Operand As SyntaxToken = b._Operator

            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine(" Syntax TypeStr : " & Expression._SyntaxTypeStr)
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("     Left Expression : ")
            Console.ForegroundColor = ConsoleColor.Green
            WriteExpressionSyntaxToken(_Left)
            Console.ResetColor()
            Console.WriteLine(Operand.ToJson)
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("     Right Expression : ")
            Console.ForegroundColor = ConsoleColor.Green
            WriteExpressionSyntaxToken(_Right)
        End Sub
    End Class
End Namespace

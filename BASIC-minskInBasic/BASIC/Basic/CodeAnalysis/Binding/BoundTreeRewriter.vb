'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundTreeRewriter.vb
'
' summary:	Bound tree rewriter class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A bound tree rewriter. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend MustInherit Class BoundTreeRewriter

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overridable Function RewriteStatement(node As BoundStatement) As BoundStatement
            Select Case node.Kind
                Case BoundNodeKind.BlockStatement : Return RewriteBlockStatement(DirectCast(node, BoundBlockStatement))
                Case BoundNodeKind.NopStatement : Return RewriteNopStatement(DirectCast(node, BoundNopStatement))
                Case BoundNodeKind.VariableDeclaration : Return RewriteVariableDeclaration(DirectCast(node, BoundVariableDeclaration))
                Case BoundNodeKind.IfStatement : Return RewriteIfStatement(DirectCast(node, BoundIfStatement))
                Case BoundNodeKind.WhileStatement : Return RewriteWhileStatement(DirectCast(node, BoundWhileStatement))
                Case BoundNodeKind.DoWhileStatement : Return RewriteDoWhileStatement(DirectCast(node, BoundDoWhileStatement))
                Case BoundNodeKind.ForStatement : Return RewriteForStatement(DirectCast(node, BoundForStatement))
                Case BoundNodeKind.LabelStatement : Return RewriteLabeltatement(DirectCast(node, BoundLabelStatement))
                Case BoundNodeKind.GotoStatement : Return RewriteGotoStatement(DirectCast(node, BoundGotoStatement))
                Case BoundNodeKind.ConditionalGotoStatement : Return RewriteConditionalGotoStatement(DirectCast(node, BoundConditionalGotoStatement))
                Case BoundNodeKind.ReturnStatement : Return RewriteReturnStatement(DirectCast(node, BoundReturnStatement))
                Case BoundNodeKind.ExpressionStatement : Return RewriteExpressionStatement(DirectCast(node, BoundExpressionStatement))
                Case Else
                    Throw New Exception($"Unexpected node: {node.Kind}")
            End Select
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite block statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteBlockStatement(node As BoundBlockStatement) As BoundStatement
            Dim builder As ImmutableArray(Of BoundStatement).Builder = Nothing
            For i = 0 To node.Statements.Length - 1
                Dim oldStatement = node.Statements(i)
                Dim newStatement = RewriteStatement(oldStatement)
                If newStatement IsNot oldStatement Then
                    If builder Is Nothing Then
                        builder = ImmutableArray.CreateBuilder(Of BoundStatement)(node.Statements.Length)
                        For j = 0 To i - 1
                            builder.Add(node.Statements(j))
                        Next
                    End If
                End If
                If builder IsNot Nothing Then
                    builder.Add(newStatement)
                End If
            Next
            If builder Is Nothing Then
                Return node
            End If
            Return New BoundBlockStatement(builder.MoveToImmutable)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite nop statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteNopStatement(node As BoundNopStatement) As BoundStatement
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite variable declaration. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteVariableDeclaration(node As BoundVariableDeclaration) As BoundStatement
            Dim initializer = RewriteExpression(node.Initializer)
            If initializer Is node.Initializer Then
                Return node
            End If
            Return New BoundVariableDeclaration(node.Variable, initializer)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite if statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteIfStatement(node As BoundIfStatement) As BoundStatement
            Dim condition = RewriteExpression(node.Condition)
            Dim thenStatement = RewriteStatement(node.ThenStatement)
            Dim elseStatement = If(node.ElseStatement Is Nothing, Nothing, RewriteStatement(node.ElseStatement))
            If condition Is node.Condition AndAlso
               thenStatement Is node.ThenStatement AndAlso
               elseStatement Is node.ElseStatement Then
                Return node
            End If
            Return New BoundIfStatement(condition, thenStatement, elseStatement)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite while statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteWhileStatement(node As BoundWhileStatement) As BoundStatement
            Dim condition = RewriteExpression(node.Condition)
            Dim body = RewriteStatement(node.Body)
            If condition Is node.Condition AndAlso body Is node.Body Then
                Return node
            End If
            Return New BoundWhileStatement(condition, body, node.BreakLabel, node.ContinueLabel)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite do while statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteDoWhileStatement(node As BoundDoWhileStatement) As BoundStatement
            Dim body = RewriteStatement(node.Body)
            Dim condition = RewriteExpression(node.Condition)
            If body Is node.Body AndAlso condition Is node.Condition Then
                Return node
            End If
            Return New BoundDoWhileStatement(body, condition, node.BreakLabel, node.ContinueLabel)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite for statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteForStatement(node As BoundForStatement) As BoundStatement
            Dim lowerBound = RewriteExpression(node.LowerBound)
            Dim upperBound = RewriteExpression(node.UpperBound)
            Dim body = RewriteStatement(node.Body)
            If lowerBound Is node.LowerBound AndAlso
                     upperBound Is node.UpperBound AndAlso
                     body Is node.Body Then
                Return node
            End If
            Return New BoundForStatement(node.Variable, lowerBound, upperBound, body, node.BreakLabel, node.ContinueLabel)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite labeltatement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteLabeltatement(node As BoundLabelStatement) As BoundStatement
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite goto statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteGotoStatement(node As BoundGotoStatement) As BoundStatement
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite conditional goto statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteConditionalGotoStatement(node As BoundConditionalGotoStatement) As BoundStatement
            Dim condition = RewriteExpression(node.Condition)
            If condition Is node.Condition Then
                Return node
            End If
            Return New BoundConditionalGotoStatement(node.Label, condition, node.JumpIfTrue)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite return statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteReturnStatement(node As BoundReturnStatement) As BoundStatement
            Dim expression = If(node.Expression Is Nothing, Nothing, RewriteExpression(node.Expression))
            If expression Is node.Expression Then
                Return node
            End If
            Return New BoundReturnStatement(expression)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite expression statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundStatement. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteExpressionStatement(node As BoundExpressionStatement) As BoundStatement
            Dim expression = RewriteExpression(node.Expression)
            If expression Is node.Expression Then
                Return node
            Else
                Return New BoundExpressionStatement(expression)
            End If
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overridable Function RewriteExpression(node As BoundExpression) As BoundExpression
            Select Case node.Kind
                Case BoundNodeKind.ErrorExpression : Return RewriteErrorExpression(DirectCast(node, BoundErrorExpression))
                Case BoundNodeKind.LiteralExpression : Return RewriteLiteralExpression(DirectCast(node, BoundLiteralExpression))
                Case BoundNodeKind.VariableExpression : Return RewriteVariableExpression(DirectCast(node, BoundVariableExpression))
                Case BoundNodeKind.AssignmentExpression : Return RewriteAssignmentExpression(DirectCast(node, BoundAssignmentExpression))
                Case BoundNodeKind.UnaryExpression : Return RewriteUnaryExpression(DirectCast(node, BoundUnaryExpression))
                Case BoundNodeKind.BinaryExpression : Return RewriteBinaryExpression(DirectCast(node, BoundBinaryExpression))
                Case BoundNodeKind.CallExpression : Return RewriteCallExpression(DirectCast(node, BoundCallExpression))
                Case BoundNodeKind.ConversionExpression : Return RewriteConversionExpression(DirectCast(node, BoundConversionExpression))
                Case Else
                    Throw New Exception($"Unexpected node: {node.Kind}")
            End Select
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite error expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function RewriteErrorExpression(node As BoundErrorExpression) As BoundExpression
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteLiteralExpression(node As BoundLiteralExpression) As BoundExpression
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite variable expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteVariableExpression(node As BoundVariableExpression) As BoundExpression
            Return node
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite assignment expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteAssignmentExpression(node As BoundAssignmentExpression) As BoundExpression
            Dim expression = RewriteExpression(node.Expression)
            If expression Is node.Expression Then
                Return node
            Else
                Return New BoundAssignmentExpression(node.Variable, expression)
            End If
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite unary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteUnaryExpression(node As BoundUnaryExpression) As BoundExpression
            Dim operand = RewriteExpression(node.Operand)
            If operand Is node.Operand Then
                Return node
            Else
                Return New BoundUnaryExpression(node.Op, operand)
            End If
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite binary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteBinaryExpression(node As BoundBinaryExpression) As BoundExpression
            Dim left = RewriteExpression(node.Left)
            Dim right = RewriteExpression(node.Right)
            If left Is node.Left AndAlso
                     right Is node.Right Then
                Return node
            Else
                Return New BoundBinaryExpression(left, node.Op, right)
            End If
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite call expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteCallExpression(node As BoundCallExpression) As BoundExpression
            Dim builder As ImmutableArray(Of BoundExpression).Builder = Nothing
            For i = 0 To node.Arguments.Length - 1
                Dim oldArgument = node.Arguments(i)
                Dim newArgument = RewriteExpression(oldArgument)
                If newArgument IsNot oldArgument Then
                    If builder Is Nothing Then
                        builder = ImmutableArray.CreateBuilder(Of BoundExpression)(node.Arguments.Length)
                        For j = 0 To i - 1
                            builder.Add(node.Arguments(j))
                        Next
                    End If
                End If
                If builder IsNot Nothing Then
                    builder.Add(newArgument)
                End If
            Next
            If builder Is Nothing Then
                Return node
            End If
            Return New BoundCallExpression(node.Function, builder.MoveToImmutable)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Rewrite conversion expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node"> The node. </param>
        '''
        ''' <returns>   A BoundExpression. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overridable Function RewriteConversionExpression(node As BoundConversionExpression) As BoundExpression
      Dim expression = RewriteExpression(node.Expression)
      If expression Is node.Expression Then
        Return node
      Else
        Return New BoundConversionExpression(node.Type, expression)
      End If
    End Function

  End Class

End Namespace
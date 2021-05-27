'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundNodePrinter.vb
'
' summary:	Bound node printer class
'---------------------------------------------------------------------------------------------------

Option Infer On

Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Syntax
Imports Basic.IO

Namespace Global.Basic.CodeAnalysis.Binding

    Friend Module BoundNodePrinter

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes to. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Sub WriteTo(node As BoundNode, writer As TextWriter)
      If TypeOf writer Is IndentedTextWriter Then
        WriteTo(node, DirectCast(writer, IndentedTextWriter))
      Else
        WriteTo(node, New IndentedTextWriter(writer))
      End If
    End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes to. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Public Sub WriteTo(node As BoundNode, writer As IndentedTextWriter)
      Select Case node.Kind
        Case BoundNodeKind.BlockStatement
          WriteBlockStatement(CType(node, BoundBlockStatement), writer)
        Case BoundNodeKind.NopStatement
          WriteNopStatement(CType(node, BoundNopStatement), writer)
        Case BoundNodeKind.VariableDeclaration
          WriteVariableDeclaration(CType(node, BoundVariableDeclaration), writer)
        Case BoundNodeKind.IfStatement
          WriteIfStatement(CType(node, BoundIfStatement), writer)
        Case BoundNodeKind.WhileStatement
          WriteWhileStatement(CType(node, BoundWhileStatement), writer)
        Case BoundNodeKind.DoWhileStatement
          WriteDoWhileStatement(CType(node, BoundDoWhileStatement), writer)
        Case BoundNodeKind.ForStatement
          WriteForStatement(CType(node, BoundForStatement), writer)
        Case BoundNodeKind.LabelStatement
          WriteLabelStatement(CType(node, BoundLabelStatement), writer)
        Case BoundNodeKind.GotoStatement
          WriteGotoStatement(CType(node, BoundGotoStatement), writer)
        Case BoundNodeKind.ConditionalGotoStatement
          WriteConditionalGotoStatement(CType(node, BoundConditionalGotoStatement), writer)
        Case BoundNodeKind.ReturnStatement
          WriteReturnStatement(CType(node, BoundReturnStatement), writer)
        Case BoundNodeKind.ExpressionStatement
          WriteExpressionStatement(CType(node, BoundExpressionStatement), writer)
        Case BoundNodeKind.ErrorExpression
          WriteErrorExpression(CType(node, BoundErrorExpression), writer)
        Case BoundNodeKind.LiteralExpression
          WriteLiteralExpression(CType(node, BoundLiteralExpression), writer)
        Case BoundNodeKind.VariableExpression
          WriteVariableExpression(CType(node, BoundVariableExpression), writer)
        Case BoundNodeKind.AssignmentExpression
          WriteAssignmentExpression(CType(node, BoundAssignmentExpression), writer)
        Case BoundNodeKind.UnaryExpression
          WriteUnaryExpression(CType(node, BoundUnaryExpression), writer)
        Case BoundNodeKind.BinaryExpression
          WriteBinaryExpression(CType(node, BoundBinaryExpression), writer)
        Case BoundNodeKind.CallExpression
          WriteCallExpression(CType(node, BoundCallExpression), writer)
        Case BoundNodeKind.ConversionExpression
          WriteConversionExpression(CType(node, BoundConversionExpression), writer)
        Case Else
          Throw New Exception($"Unexpected node {node.Kind}")
      End Select
    End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a nested statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="writer">   The writer. </param>
        ''' <param name="node">     The node. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Private Sub WriteNestedStatement(writer As IndentedTextWriter, node As BoundStatement)

      Dim needsIndentation = TypeOf node IsNot BoundBlockStatement

      If needsIndentation Then
        writer.Indent += 1
      End If

      node.WriteTo(writer)

      If needsIndentation Then
        writer.Indent -= 1
      End If

    End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a nested expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="writer">           The writer. </param>
        ''' <param name="parentPrecedence"> The parent precedence. </param>
        ''' <param name="expression">       The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Private Sub WriteNestedExpression(writer As IndentedTextWriter, parentPrecedence As Integer, expression As BoundExpression)
      If TypeOf expression Is BoundUnaryExpression Then
        Dim unary = CType(expression, BoundUnaryExpression)
        writer.WriteNestedExpression(parentPrecedence, SyntaxFacts.GetUnaryOperatorPrecedence(unary.Op.SyntaxKind), unary)
      ElseIf TypeOf expression Is BoundBinaryExpression Then
        Dim binary = CType(expression, BoundBinaryExpression)
        writer.WriteNestedExpression(parentPrecedence, SyntaxFacts.GetBinaryOperatorPrecedence(binary.Op.SyntaxKind), binary)
      Else
        expression.WriteTo(writer)
      End If
    End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a nested expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="writer">               The writer. </param>
        ''' <param name="parentPrecedence">     The parent precedence. </param>
        ''' <param name="currentPrecedence">    The current precedence. </param>
        ''' <param name="expression">           The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Extension>
    Private Sub WriteNestedExpression(writer As IndentedTextWriter, parentPrecedence As Integer, currentPrecedence As Integer, expression As BoundExpression)

      Dim needsParenthesis = parentPrecedence >= currentPrecedence

      If needsParenthesis Then
        writer.WritePunctuation(SyntaxKind.OpenParenToken)
      End If

      expression.WriteTo(writer)

      If needsParenthesis Then
        writer.WritePunctuation(SyntaxKind.CloseParenToken)
      End If

    End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a block statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteBlockStatement(node As BoundBlockStatement, writer As IndentedTextWriter)

            writer.WritePunctuation(SyntaxKind.OpenBraceToken)
            writer.WriteLine()
            writer.Indent += 1

            For Each s In node.Statements
                s.WriteTo(writer)
            Next

            writer.Indent -= 1
            writer.WritePunctuation(SyntaxKind.CloseBraceToken)
            writer.WriteLine()

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a nop statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteNopStatement(node As BoundNopStatement, writer As IndentedTextWriter)
            If node Is Nothing Then
            End If
            writer.WriteKeyword("nop")
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a variable declaration. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteVariableDeclaration(node As BoundVariableDeclaration, writer As IndentedTextWriter)
            writer.WriteKeyword(If(node.Variable.IsReadOnly, SyntaxKind.LetKeyword, SyntaxKind.VarKeyword))
            writer.WriteSpace
            writer.WriteIdentifier(node.Variable.Name)
            writer.WriteSpace
            writer.WritePunctuation(SyntaxKind.EqualsToken)
            writer.WriteSpace
            node.Initializer.WriteTo(writer)
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes if statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteIfStatement(node As BoundIfStatement, writer As IndentedTextWriter)
            writer.WriteKeyword(SyntaxKind.IfKeyword)
            writer.WriteSpace
            node.Condition.WriteTo(writer)
            writer.WriteLine()
            writer.WriteNestedStatement(node.ThenStatement)
            If node.ElseStatement IsNot Nothing Then
                writer.WriteKeyword(SyntaxKind.ElseKeyword)
                writer.WriteLine()
                writer.WriteNestedStatement(node.ElseStatement)
            End If
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a while statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteWhileStatement(node As BoundWhileStatement, writer As IndentedTextWriter)
            writer.WriteKeyword(SyntaxKind.WhileKeyword)
            writer.WriteSpace
            node.Condition.WriteTo(writer)
            writer.WriteLine()
            writer.WriteNestedStatement(node.Body)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a do while statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteDoWhileStatement(node As BoundDoWhileStatement, writer As IndentedTextWriter)
            writer.WriteKeyword(SyntaxKind.DoKeyword)
            writer.WriteLine()
            writer.WriteNestedStatement(node.Body)
            writer.WriteKeyword(SyntaxKind.WhileKeyword)
            writer.WriteSpace
            node.Condition.WriteTo(writer)
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes for statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteForStatement(node As BoundForStatement, writer As IndentedTextWriter)
            writer.WriteKeyword(SyntaxKind.ForKeyword)
            writer.WriteSpace
            writer.WriteIdentifier(node.Variable.Name)
            writer.WriteSpace
            writer.WritePunctuation(SyntaxKind.EqualsToken)
            writer.WriteSpace
            node.LowerBound.WriteTo(writer)
            writer.WriteSpace
            writer.WriteKeyword(SyntaxKind.ToKeyword)
            writer.WriteSpace
            node.UpperBound.WriteTo(writer)
            writer.WriteLine()
            writer.WriteNestedStatement(node.Body)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a label statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteLabelStatement(node As BoundLabelStatement, writer As IndentedTextWriter)

            Dim unindent = writer.Indent > 0

            If unindent Then
                writer.Indent -= 1
            End If

            writer.WritePunctuation(node.Label.Name)
            writer.WritePunctuation(SyntaxKind.ColonToken)
            writer.WriteLine()

            If unindent Then
                writer.Indent += 1
            End If

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a goto statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteGotoStatement(node As BoundGotoStatement, writer As IndentedTextWriter)
            writer.WriteKeyword("goto") ' There is no SyntaxKind for goto
            writer.WriteSpace()
            writer.WriteIdentifier(node.Label.Name)
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a conditional goto statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteConditionalGotoStatement(node As BoundConditionalGotoStatement, writer As IndentedTextWriter)
            writer.WriteKeyword("goto") ' There is no SyntaxKind for goto
            writer.WriteSpace()
            writer.WriteIdentifier(node.Label.Name)
            writer.WriteSpace()
            writer.WriteKeyword(If(node.JumpIfTrue, "if", "unless"))
            writer.WriteSpace()
            node.Condition.WriteTo(writer)
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a return statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteReturnStatement(node As BoundReturnStatement, writer As IndentedTextWriter)
            writer.WriteKeyword(SyntaxKind.ReturnKeyword)
            If node.Expression IsNot Nothing Then
                writer.WriteSpace()
                node.Expression.WriteTo(writer)
            End If
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an expression statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteExpressionStatement(node As BoundExpressionStatement, writer As IndentedTextWriter)
            node.Expression.WriteTo(writer)
            writer.WriteLine()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an error expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteErrorExpression(node As BoundErrorExpression, writer As IndentedTextWriter)
            If node Is Nothing Then
            End If
            writer.WriteKeyword("?")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteLiteralExpression(node As BoundLiteralExpression, writer As IndentedTextWriter)
            Dim value = node.Value.ToString()
            If node.Type Is TypeSymbol.Bool Then
                writer.WriteKeyword(If(CBool(value), SyntaxKind.TrueKeyword, SyntaxKind.FalseKeyword))
            ElseIf node.Type Is TypeSymbol.Int Then
                writer.WriteNumber(value)
            ElseIf node.Type Is TypeSymbol.String Then
                value = """" & value.Replace("""", """""") & """"
                writer.WriteString(value)
            Else
                Throw New Exception($"Unexpected type {node.Type}")
            End If
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a variable expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteVariableExpression(node As BoundVariableExpression, writer As IndentedTextWriter)
            writer.WriteIdentifier(node.Variable.Name)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an assignment expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteAssignmentExpression(node As BoundAssignmentExpression, writer As IndentedTextWriter)
            writer.WriteIdentifier(node.Variable.Name)
            writer.WriteSpace
            writer.WritePunctuation(SyntaxKind.EqualsToken)
            writer.WriteSpace
            node.Expression.WriteTo(writer)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes an unary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteUnaryExpression(node As BoundUnaryExpression, writer As IndentedTextWriter)
            Dim precedence = SyntaxFacts.GetUnaryOperatorPrecedence(node.Op.SyntaxKind)
            writer.WritePunctuation(node.Op.SyntaxKind)
            writer.WriteNestedExpression(precedence, node.Operand)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a binary expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteBinaryExpression(node As BoundBinaryExpression, writer As IndentedTextWriter)
            Dim precedence = SyntaxFacts.GetBinaryOperatorPrecedence(node.Op.SyntaxKind)
            writer.WriteNestedExpression(precedence, node.Left)
            writer.WriteSpace
            writer.WritePunctuation(node.Op.SyntaxKind)
            writer.WriteSpace
            writer.WriteNestedExpression(precedence, node.Right)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a call expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteCallExpression(node As BoundCallExpression, writer As IndentedTextWriter)

            writer.WriteIdentifier(node.Function.Name)
            writer.WritePunctuation(SyntaxKind.OpenParenToken)

            Dim isFirst = True

            For Each argument In node.Arguments

                If isFirst Then
                    isFirst = False
                Else
                    writer.WritePunctuation(SyntaxKind.CommaToken)
                    writer.WriteSpace
                End If

                argument.WriteTo(writer)

            Next

            writer.WritePunctuation(SyntaxKind.CloseParenToken)

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes a conversion expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="node">     The node. </param>
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub WriteConversionExpression(node As BoundConversionExpression, writer As IndentedTextWriter)
      writer.WriteIdentifier(node.Type.Name)
      writer.WritePunctuation(SyntaxKind.OpenParenToken)
      node.Expression.WriteTo(writer)
      writer.WritePunctuation(SyntaxKind.CloseParenToken)
    End Sub

  End Module

End Namespace
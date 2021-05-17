﻿Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports Basic.CodeAnalysis.Binding
Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Syntax

Namespace Global.Basic.CodeAnalysis.Lowering

  ' TODO: Consider creating a BoundNodeFactory to construct nodes to make lowering easier to read.
  Friend NotInheritable Class Lowerer
    Inherits BoundTreeRewriter

    Private m_labelCount As Integer = 0

    Private Sub New()
    End Sub

    Private Function GenerateLabel() As BoundLabel
      m_labelCount += 1
      Dim name = $"Label{m_labelCount}"
      Return New BoundLabel(name)
    End Function

    Public Shared Function Lower(func As FunctionSymbol, statement As BoundStatement) As BoundBlockStatement
      Dim lowerer = New Lowerer
      Dim result = lowerer.RewriteStatement(statement)
      Return RemoveDeadCode(Flatten(func, result))
    End Function

    Private Shared Function Flatten(func As FunctionSymbol, statement As BoundStatement) As BoundBlockStatement
      Dim builder = ImmutableArray.CreateBuilder(Of BoundStatement)
      Dim stack = New Stack(Of BoundStatement)
      stack.Push(statement)
      While stack.Count > 0
        Dim current = stack.Pop
        If TypeOf current Is BoundBlockStatement Then
          For Each s In DirectCast(current, BoundBlockStatement).Statements.Reverse()
            stack.Push(s)
          Next
        Else
          builder.Add(current)
        End If
      End While
      If func.Type Is TypeSymbol.Void Then
        If builder.Count = 0 OrElse CanFallThrough(builder.Last) Then
          builder.Add(New BoundReturnStatement(Nothing))
        End If
      End If
      Return New BoundBlockStatement(builder.ToImmutable)
    End Function

    Private Shared Function CanFallThrough(boundStatement As BoundStatement) As Boolean
      Return boundStatement.Kind <> BoundNodeKind.ReturnStatement AndAlso
             boundStatement.Kind <> BoundNodeKind.GotoStatement
    End Function

    Private Shared Function RemoveDeadCode(node As BoundBlockStatement) As BoundBlockStatement

      Dim controlFlow = ControlFlowGraph.Create(node)
      Dim reachableStatements = New HashSet(Of BoundStatement)(controlFlow.Blocks.SelectMany(Function(b) b.Statements))

      Dim builder = node.Statements.ToBuilder
      For i = builder.Count - 1 To 0 Step -1
        If Not reachableStatements.Contains(builder(i)) Then
          builder.RemoveAt(i)
        End If
      Next

      Return New BoundBlockStatement(builder.ToImmutable)

    End Function

    Protected Overrides Function RewriteIfStatement(node As BoundIfStatement) As BoundStatement

      If node.ElseStatement Is Nothing Then

        ' if <condition>
        '      <then>
        '
        ' ------>
        '
        ' gotoFalse <condition> end
        ' <then>
        ' end:

        Dim endLabel = GenerateLabel()
        Dim gotoFalse = New BoundConditionalGotoStatement(endLabel, node.Condition, False)
        Dim endLabelStatement = New BoundLabelStatement(endLabel)
        Dim result = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(gotoFalse, node.ThenStatement, endLabelStatement))
        Return RewriteStatement(result)

      Else

        ' if <condition>
        '      <then>
        '
        ' else
        '       <else>
        '
        ' ------>
        '
        ' gotoFalse <condition> else
        ' <then>
        ' goto end
        ' else:
        ' <else>
        ' end:

        Dim elseLabel = GenerateLabel()
        Dim endLabel = GenerateLabel()

        Dim gotoFalse = New BoundConditionalGotoStatement(elseLabel, node.Condition, False)
        Dim gotoEndStatement = New BoundGotoStatement(endLabel)

        Dim elseLabelStatement = New BoundLabelStatement(elseLabel)
        Dim endLabelStatement = New BoundLabelStatement(endLabel)

        Dim result = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(gotoFalse,
                                                                                      node.ThenStatement,
                                                                                      gotoEndStatement,
                                                                                      elseLabelStatement,
                                                                                      node.ElseStatement,
                                                                                      endLabelStatement))
        Return RewriteStatement(result)

      End If

    End Function

    Protected Overrides Function RewriteWhileStatement(node As BoundWhileStatement) As BoundStatement

      ' while <condition>
      '   <body>
      '
      ' ------->
      '
      ' goto continue
      ' body:
      ' <body>
      ' continue:
      ' gotoTrue <condition> body
      ' break:

      Dim bodyLabel = GenerateLabel()

      Dim gotoContinue = New BoundGotoStatement(node.ContinueLabel)
      Dim bodyLabelStatement = New BoundLabelStatement(bodyLabel)
      Dim continueLabelStatement = New BoundLabelStatement(node.ContinueLabel)
      Dim gotoTrue = New BoundConditionalGotoStatement(bodyLabel, node.Condition)
      Dim breakLabelStatement = New BoundLabelStatement(node.BreakLabel)

      Dim result = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(gotoContinue,
                                                                                    bodyLabelStatement,
                                                                                    node.Body,
                                                                                    continueLabelStatement,
                                                                                    gotoTrue,
                                                                                    breakLabelStatement))

      Return RewriteStatement(result)

    End Function

    Protected Overrides Function RewriteDoWhileStatement(node As BoundDoWhileStatement) As BoundStatement

      ' do 
      '   <body>
      ' while <condition>
      '
      ' ------->
      '
      ' body:
      ' <body>
      ' check:
      ' continue:
      ' gotoTrue <condition> body
      ' break:

      Dim bodyLabel = GenerateLabel()

      Dim bodyLabelStatement = New BoundLabelStatement(bodyLabel)
      Dim continueLabelStatement = New BoundLabelStatement(node.ContinueLabel)
      Dim gotoTrue = New BoundConditionalGotoStatement(bodyLabel, node.Condition)
      Dim breakLabelStatement = New BoundLabelStatement(node.BreakLabel)

      Dim result = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(bodyLabelStatement,
                                                                                    node.Body,
                                                                                    continueLabelStatement,
                                                                                    gotoTrue,
                                                                                    breakLabelStatement))

      Return RewriteStatement(result)

    End Function


    Protected Overrides Function RewriteForStatement(node As BoundForStatement) As BoundStatement

      '
      ' for i = <lower> to <upper>
      '     <boby>
      '
      '  ------>
      '
      ' {
      '   var <var> = <lower>
      '   while (<var> <= <upper>)
      '   let upperBound = <upper>
      '   while (<var> <= upperBound)
      '   {
      '     <body>
      '     continue:
      '     <var> = <var> + 1
      '   }
      ' }

      Dim variableDeclaration = New BoundVariableDeclaration(node.Variable, node.LowerBound)
      Dim variableExpression = New BoundVariableExpression(node.Variable)
      Dim upperBoundSymbol = New LocalVariableSymbol("upperBound", True, TypeSymbol.Int, node.UpperBound.ConstantValue)
      Dim upperBoundDeclaration = New BoundVariableDeclaration(upperBoundSymbol, node.UpperBound)
      Dim condition = New BoundBinaryExpression(
              variableExpression,
              BoundBinaryOperator.Bind(SyntaxKind.LessThanEqualsToken, TypeSymbol.Int, TypeSymbol.Int),
              New BoundVariableExpression(upperBoundSymbol))
      Dim continueLabelStatement = New BoundLabelStatement(node.ContinueLabel)
      Dim increment = New BoundExpressionStatement(
              New BoundAssignmentExpression(
                node.Variable,
                New BoundBinaryExpression(
                  variableExpression,
                  BoundBinaryOperator.Bind(SyntaxKind.PlusToken, TypeSymbol.Int, TypeSymbol.Int),
                  New BoundLiteralExpression(1))))
      Dim whileBody = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(node.Body,
                                                                                       continueLabelStatement,
                                                                                       increment))
      Dim whileStatement = New BoundWhileStatement(condition, whileBody, node.BreakLabel, GenerateLabel)
      Dim result = New BoundBlockStatement(ImmutableArray.Create(Of BoundStatement)(
              variableDeclaration,
              upperBoundDeclaration,
              whileStatement))

      Return RewriteStatement(result)

    End Function

    Protected Overrides Function RewriteConditionalGotoStatement(node As BoundConditionalGotoStatement) As BoundStatement

      If node.Condition.ConstantValue IsNot Nothing Then
        Dim condition = CBool(node.Condition.ConstantValue.Value)
        condition = If(node.JumpIfTrue, condition, Not condition)
        If condition Then
          Return RewriteStatement(New BoundGotoStatement(node.Label))
        Else
          Return RewriteStatement(New BoundNopStatement())
        End If
      End If

      Return MyBase.RewriteConditionalGotoStatement(node)

    End Function

  End Class

End Namespace

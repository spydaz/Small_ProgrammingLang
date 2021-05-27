﻿'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\ConstantFolding.vb
'
' summary:	Constant folding class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    Friend Module ConstantFolding

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Calculates the constant. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="op">       The operation. </param>
        ''' <param name="operand">  The operand. </param>
        '''
        ''' <returns>   The calculated constant. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function ComputeConstant(op As BoundUnaryOperator, operand As BoundExpression) As BoundConstant
            If operand.ConstantValue IsNot Nothing AndAlso TypeOf operand.ConstantValue.Value Is Integer Then
                Select Case op.Kind
                    Case BoundUnaryOperatorKind.Identity
                        Return New BoundConstant(CInt(operand.ConstantValue.Value))
                    Case BoundUnaryOperatorKind.Negation
                        Return New BoundConstant(-CInt(operand.ConstantValue.Value))
                    Case BoundUnaryOperatorKind.LogicalNegation
                        Return New BoundConstant(Not CBool(operand.ConstantValue.Value))
                    Case BoundUnaryOperatorKind.Onescomplement
                        Return New BoundConstant(Not CInt(operand.ConstantValue.Value))
                    Case Else
                        Throw New Exception($"Unexpected unary operator {op.Kind}")
                End Select
            End If
            Return Nothing
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Calculates the constant. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="left">     The left. </param>
        ''' <param name="op">       The operation. </param>
        ''' <param name="right">    The right. </param>
        '''
        ''' <returns>   The calculated constant. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function ComputeConstant(left As BoundExpression, op As BoundBinaryOperator, right As BoundExpression) As BoundConstant

      Dim leftConstant = left.ConstantValue
      Dim rightConstant = right.ConstantValue

      ' Special case && and || because there are cases where only need one side needs to be known.

      If op.Kind = BoundBinaryOperatorKind.LogicalAnd Then
        If (leftConstant IsNot Nothing AndAlso Not CBool(leftConstant.Value)) OrElse
           (rightConstant IsNot Nothing AndAlso Not CBool(rightConstant.Value)) Then
          Return New BoundConstant(False)
        End If
      End If

      If op.Kind = BoundBinaryOperatorKind.LogicalOr Then
        If (leftConstant IsNot Nothing AndAlso CBool(leftConstant.Value)) OrElse
           (rightConstant IsNot Nothing AndAlso CBool(rightConstant.Value)) Then
          Return New BoundConstant(True)
        End If
      End If

      If leftConstant Is Nothing OrElse rightConstant Is Nothing Then Return Nothing

      ' compute

      Dim l = leftConstant.Value
      Dim r = rightConstant.Value

      Select Case op.Kind
        Case BoundBinaryOperatorKind.Addition
          If left.Type Is TypeSymbol.Int Then
            Return New BoundConstant(CInt(l) + CInt(r))
          Else
            Return New BoundConstant(CStr(l) & CStr(r))
          End If
        Case BoundBinaryOperatorKind.Subtraction : Return New BoundConstant(CInt(l) - CInt(r))
        Case BoundBinaryOperatorKind.Multiplication : Return New BoundConstant(CInt(l) * CInt(r))
        Case BoundBinaryOperatorKind.Division : Return New BoundConstant(CInt(l) \ CInt(r))
        Case BoundBinaryOperatorKind.BitwiseAnd
          If left.Type Is TypeSymbol.Int Then
            Return New BoundConstant(CInt(l) And CInt(r))
          Else
            Return New BoundConstant(CBool(l) And CBool(r))
          End If
        Case BoundBinaryOperatorKind.BitwiseOr
          If left.Type Is TypeSymbol.Int Then
            Return New BoundConstant(CInt(l) Or CInt(r))
          Else
            Return New BoundConstant(CBool(l) Or CBool(r))
          End If
        Case BoundBinaryOperatorKind.BitwiseXor
          If left.Type Is TypeSymbol.Int Then
            Return New BoundConstant(CInt(l) Xor CInt(r))
          Else
            Return New BoundConstant(CBool(l) Xor CBool(r))
          End If
        Case BoundBinaryOperatorKind.LogicalAnd : Return New BoundConstant(CBool(l) And CBool(r))
        Case BoundBinaryOperatorKind.LogicalOr : Return New BoundConstant(CBool(l) Or CBool(r))
        Case BoundBinaryOperatorKind.Equals : Return New BoundConstant(Equals(l, r))
        Case BoundBinaryOperatorKind.NotEquals : Return New BoundConstant(Not Equals(l, r))
        Case BoundBinaryOperatorKind.Less : Return New BoundConstant(CInt(l) < CInt(r))
        Case BoundBinaryOperatorKind.Greater : Return New BoundConstant(CInt(l) > CInt(r))
        Case BoundBinaryOperatorKind.LessOrEquals : Return New BoundConstant(CInt(l) <= CInt(r))
        Case BoundBinaryOperatorKind.GreaterOrEquals : Return New BoundConstant(CInt(l) >= CInt(r))
        Case Else
          Throw New Exception($"Unexpected binary operator {op.Kind}")
      End Select


    End Function

  End Module

End Namespace
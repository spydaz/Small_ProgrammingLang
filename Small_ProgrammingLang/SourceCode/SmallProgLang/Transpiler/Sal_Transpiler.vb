Imports SDK.SmallProgLang.Ast_ExpressionFactory

Namespace SmallProgLang
    Namespace Transpiler
        ''' <summary>
        ''' Transpiles to SAL Code; 
        ''' Can be run on SAL Assembler
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Sal_Transpiler
            ''' <summary>
            ''' Line number counter
            ''' </summary>
            Private LineNumber As Integer
            ''' <summary>
            ''' Increases the current line number to track the line number in the program
            ''' </summary>
            Private Sub IncrLineNumber()
                LineNumber += 1
            End Sub
            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
            ''' <summary>
            ''' As Each Expression is consumed an program will be returned 
            ''' Each Expression Should be an array 
            ''' Provided by the AST toArray Function 
            ''' Which only Creates the Desired nodes in the order Expected by the Transpiler(RPL)
            ''' </summary>
            ''' <param name="Ast"></param>
            ''' <returns></returns>
            Public Function Transpile(ByRef Ast As AstExpression) As List(Of String)
                Dim Expr = Ast.ToArraylist
                'Created for tracking 
                IncrLineNumber()

                'Detect Binary Expressions
                Select Case Expr.Count
                    Case 2
                        Return (_print(Expr(1)))
                    Case 3
                        'Function : to get literal will be required 
                        Return _Binary_op(Expr(1), Expr(2), Expr(0))
                End Select

                'If it is not a binary op
                ''then it can be identified by its type
                Select Case Ast._Type

                End Select

                Dim str As New List(Of String)
                str.Add("Not Implemented Bad Expression Syntax =" & FormatJsonOutput(Expr.ToJson) & vbNewLine & "LineNumber =" & LineNumber)
                Return str
            End Function

#Region "Generators"

            ''' <summary>
            ''' Generates SalCode For Binary ops
            ''' -Simple Assign
            ''' -Conditional
            ''' -Addative
            ''' -Multiplicative
            ''' 
            ''' </summary>
            ''' <param name="Left"></param>
            ''' <param name="Right"></param>
            ''' <param name="iOperator"></param>
            ''' <returns></returns>
            Private Function _Binary_op(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As List(Of String)

                Dim PROGRAM As New List(Of String)
                Select Case iOperator
                    Case "-"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("SUB")
                        PROGRAM.Add("PRINT_M")

                    Case "+"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("ADD")
                        PROGRAM.Add("PRINT_M")

                    Case "/"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("DIV")
                        PROGRAM.Add("PRINT_M")

                    Case "*"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("MUL")
                        PROGRAM.Add("PRINT_M")
                    Case ">"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_GT")
                        PROGRAM.Add("PRINT_M")

                    Case "<"
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_LT")
                        PROGRAM.Add("PRINT_M")

                    Case ">="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_GTE")
                        PROGRAM.Add("PRINT_M")

                    Case "<="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_LTE")
                        PROGRAM.Add("PRINT_M")

                    Case "="
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Left.ToString)
                        PROGRAM.Add("PUSH")
                        PROGRAM.Add(Right.ToString)
                        PROGRAM.Add("IS_EQ")
                        PROGRAM.Add("PRINT_M")

                End Select
                Return PROGRAM
            End Function
            Private Function _print(ByRef Str As String) As List(Of String)

                Dim PROGRAM As New List(Of String)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Str.Replace("'", ""))
                PROGRAM.Add("PRINT_M")
                Return PROGRAM

            End Function
#End Region

        End Class
    End Namespace
End Namespace

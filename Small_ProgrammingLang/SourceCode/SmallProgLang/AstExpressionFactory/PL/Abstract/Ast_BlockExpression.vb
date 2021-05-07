Imports SDK.SAL
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' Used for CodeBlocks
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class Ast_BlockExpression
            Inherits AstExpression
            Public Body As List(Of AstExpression)
            Public _ReturnValues As New List(Of AstExpression)
            Public _hasReturn As Boolean = False
            Public Sub New(ByRef iBody As List(Of AstExpression))
                MyBase.New(AST_NODE._blockCode)
                Me._TypeStr = "_blockCode"
                Body = iBody
                Me._Start = iBody(0)._Start
                For Each item In iBody
                    Me._Raw &= item._Raw
                Next
                Me._End = iBody(iBody.Count - 1)._End
                _ReturnValues = New List(Of AstExpression)
                _hasReturn = False
            End Sub
            Public Sub New(ByRef iBody As List(Of AstExpression), ByRef iReturnValues As List(Of AstExpression))
                MyBase.New(AST_NODE._blockCode)
                Me._TypeStr = "_blockCode"
                Body = iBody
                Me._Start = iBody(0)._Start
                For Each item In iBody
                    Me._Raw &= item._Raw
                Next
                Me._End = iBody(iBody.Count - 1)._End
                _ReturnValues = iReturnValues
                _hasReturn = True
            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                For Each item In Body
                    lst.AddRange(item.ToArraylist)
                Next
                lst.Add(_hasReturn.ToString)
                For Each item In _ReturnValues
                    lst.AddRange(item.ToArraylist)
                Next
                Return lst
            End Function

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return GetValue(ParentEnv)
            End Function

            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                If _hasReturn = False Then
                    For Each item In Body
                        item.Evaluate(ParentEnv)
                    Next
                    Return ParentEnv
                Else
                    Dim Values As New List(Of Object)

                    For Each item In _ReturnValues
                        Values.Add(item.Evaluate(ParentEnv))
                    Next
                    Return _ReturnValues
                End If

            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace

End Namespace
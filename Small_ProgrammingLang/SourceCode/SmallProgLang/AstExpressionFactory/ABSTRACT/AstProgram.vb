Imports SDK.SAL
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory
        ''' <summary>
        ''' 
        ''' 
        ''' Syntax : 
        ''' Based on AST Explorer output
        '''{
        '''  "type": "Program",
        '''  "start": 0,
        '''  "end": 2,
        '''  "body": [
        '''    {
        '''      "type": "ExpressionStatement",
        '''      "start": 0,
        '''      "end": 2,
        '''      "expression": {
        '''        "type": "Literal",
        '''        "start": 0,
        '''        "end": 2,
        '''        "value": 42,
        '''        "raw": "42"
        '''      }
        '''    }
        '''  ],
        '''}
        ''' 
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public Class AstProgram
            Inherits AstNode
            ''' <summary>
            ''' Expression List
            ''' </summary>
            Public Body As List(Of AstExpression)

            ''' <summary>
            ''' Instanciate Expression
            ''' </summary>
            ''' <param name="nBody">Expressions</param>
            Public Sub New(ByRef nBody As List(Of AstExpression))
                MyBase.New(AST_NODE._Program)
                Me.Body = nBody
                Me._TypeStr = "_Program"

            End Sub
            Public Overrides Function ToArraylist() As List(Of String)
                Dim lst = MyBase.ToArraylist()
                For Each item In Body
                    lst.AddRange(item.ToArraylist)
                Next
                Return lst
            End Function


            Public Overrides Function GetValue(ByRef ParentEnv As EnvironmentalMemory) As Object
                Throw New NotImplementedException()
            End Function

            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace

End Namespace
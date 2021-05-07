Imports System.Text
Imports System.Text.Json
Imports System.Web.Script.Serialization
Imports SDK.SAL
Imports SDK.SmallProgLang.Evaluator

Namespace SmallProgLang

    Namespace Ast_ExpressionFactory

        ''' <summary>
        ''' Expression Model Used To Group Expressions
        ''' </summary>
        <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
        Public MustInherit Class AstExpression
            Inherits AstNode

            Public Sub New(ByRef ntype As AST_NODE)
                MyBase.New(ntype)
            End Sub
            ''' <summary>
            ''' We shall attempt to evaluate every expression inside of itself to return the values within.
            ''' The expression uses the Environment delivered as its own global record;
            ''' the environment is returned to the sender 
            ''' with any values updated;
            ''' This function must be overridden
            ''' </summary>
            ''' <param name="ParentEnv">sets the environment for the expression; 
            ''' the environment contains the current record of variables in use by the global script </param>
            ''' <returns></returns>
            Public MustOverride Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object


            Private Function GetDebuggerDisplay() As String
                Return ToString()
            End Function
        End Class
    End Namespace

End Namespace
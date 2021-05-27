'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundExpressionStatement.vb
'
' summary:	Bound expression statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundExpressionStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="expression">   The expression. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(expression As BoundExpression)
            Me.Expression = expression
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.ExpressionStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the expression. </summary>
        '''
        ''' <value> The expression. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Expression As BoundExpression

    End Class

End Namespace
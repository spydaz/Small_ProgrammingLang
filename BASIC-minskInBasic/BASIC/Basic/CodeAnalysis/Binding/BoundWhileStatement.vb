'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundWhileStatement.vb
'
' summary:	Bound while statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundDoWhileStatement
        Inherits BoundLoopStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="body">             The body. </param>
        ''' <param name="condition">        The condition. </param>
        ''' <param name="breakLabel">       The break label. </param>
        ''' <param name="continuelabel">    The continuelabel. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(body As BoundStatement, condition As BoundExpression, breakLabel As BoundLabel, continuelabel As BoundLabel)
            MyBase.New(breakLabel, continuelabel)
            Me.Body = body
            Me.Condition = condition
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.DoWhileStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As BoundExpression

    End Class
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundWhileStatement
        Inherits BoundLoopStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="condition">        The condition. </param>
        ''' <param name="body">             The body. </param>
        ''' <param name="breakLabel">       The break label. </param>
        ''' <param name="continueLabel">    The continue label. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(condition As BoundExpression, body As BoundStatement, breakLabel As BoundLabel, continueLabel As BoundLabel)
            MyBase.New(breakLabel, continueLabel)
            Me.Condition = condition
            Me.Body = body
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.WhileStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As BoundStatement

    End Class

End Namespace
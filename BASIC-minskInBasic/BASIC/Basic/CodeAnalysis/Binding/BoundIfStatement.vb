'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundIfStatement.vb
'
' summary:	Bound if statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundIfStatement
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="condition">        The condition. </param>
        ''' <param name="thenStatement">    The then statement. </param>
        ''' <param name="elseStatement">    The else statement. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(condition As BoundExpression, thenStatement As BoundStatement, elseStatement As BoundStatement)
            Me.Condition = condition
            Me.ThenStatement = thenStatement
            Me.ElseStatement = elseStatement
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.IfStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the condition. </summary>
        '''
        ''' <value> The condition. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Condition As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the then statement. </summary>
        '''
        ''' <value> The then statement. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ThenStatement As BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the else statement. </summary>
        '''
        ''' <value> The else statement. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ElseStatement As BoundStatement

    End Class

End Namespace
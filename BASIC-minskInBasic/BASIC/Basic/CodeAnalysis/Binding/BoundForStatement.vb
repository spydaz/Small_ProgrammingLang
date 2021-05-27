'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundForStatement.vb
'
' summary:	Bound for statement class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundForStatement
        Inherits BoundLoopStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="variable">         The variable. </param>
        ''' <param name="lowerBound">       The lower bound. </param>
        ''' <param name="upperBound">       The upper bound. </param>
        ''' <param name="body">             The body. </param>
        ''' <param name="breakLabel">       The break label. </param>
        ''' <param name="continueLabel">    The continue label. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(variable As VariableSymbol, lowerBound As BoundExpression, upperBound As BoundExpression, body As BoundStatement, breakLabel As BoundLabel, continueLabel As BoundLabel)
            MyBase.New(breakLabel, continueLabel)
            Me.Variable = variable
            Me.LowerBound = lowerBound
            Me.UpperBound = upperBound
            Me.Body = body
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.ForStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the variable. </summary>
        '''
        ''' <value> The variable. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Variable As VariableSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the lower bound. </summary>
        '''
        ''' <value> The lower bound. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property LowerBound As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the upper bound. </summary>
        '''
        ''' <value> The upper bound. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property UpperBound As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As BoundStatement

    End Class

End Namespace
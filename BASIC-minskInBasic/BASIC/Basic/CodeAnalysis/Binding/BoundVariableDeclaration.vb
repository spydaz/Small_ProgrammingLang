'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundVariableDeclaration.vb
'
' summary:	Bound variable declaration class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundVariableDeclaration
        Inherits BoundStatement

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="variable">     The variable. </param>
        ''' <param name="initializer">  The initializer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(variable As VariableSymbol, initializer As BoundExpression)
            Me.Variable = variable
            Me.Initializer = initializer
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.VariableDeclaration

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the variable. </summary>
        '''
        ''' <value> The variable. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Variable As VariableSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the initializer. </summary>
        '''
        ''' <value> The initializer. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Initializer As BoundExpression

    End Class

End Namespace
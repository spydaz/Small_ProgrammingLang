'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundVariableExpression.vb
'
' summary:	Bound variable expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundVariableExpression
        Inherits BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="variable"> The variable. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(variable As VariableSymbol)
            Me.Variable = variable
            Type = variable.Type
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.VariableExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Type As TypeSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the variable. </summary>
        '''
        ''' <value> The variable. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Variable As VariableSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the constant value. </summary>
        '''
        ''' <value> The constant value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property ConstantValue As BoundConstant
            Get
                Return Variable.Constant
            End Get
        End Property

    End Class

End Namespace
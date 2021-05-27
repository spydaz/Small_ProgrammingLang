'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundUnaryExpression.vb
'
' summary:	Bound unary expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundUnaryExpression
        Inherits BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="op">       The operation. </param>
        ''' <param name="operand">  The operand. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(op As BoundUnaryOperator, operand As BoundExpression)
            Me.Op = op
            Me.Operand = operand
            ConstantValue = ConstantFolding.ComputeConstant(op, operand)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.UnaryExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Type As TypeSymbol
            Get
                Return Op.Type
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the operation. </summary>
        '''
        ''' <value> The operation. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Op As BoundUnaryOperator

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the operand. </summary>
        '''
        ''' <value> The operand. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Operand As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the constant value. </summary>
        '''
        ''' <value> The constant value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property ConstantValue As BoundConstant
        '  Get
        '    Return MyBase.ConstantValue
        '  End Get
        '  Set(value As BoundConstant)
        '    MyBase.ConstantValue = value
        '  End Set
        'End Property

    End Class

End Namespace
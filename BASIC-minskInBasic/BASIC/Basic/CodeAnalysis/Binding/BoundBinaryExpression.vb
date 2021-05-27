'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundBinaryExpression.vb
'
' summary:	Bound binary expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundBinaryExpression
        Inherits BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="left">     The left. </param>
        ''' <param name="op">       The operation. </param>
        ''' <param name="right">    The right. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(left As BoundExpression, op As BoundBinaryOperator, right As BoundExpression)
            Me.Left = left
            Me.Op = op
            Me.Right = right
            ConstantValue = ConstantFolding.ComputeConstant(left, op, right)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.BinaryExpression

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
        ''' <summary>   Gets the left. </summary>
        '''
        ''' <value> The left. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Left As BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the operation. </summary>
        '''
        ''' <value> The operation. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Op As BoundBinaryOperator

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the right. </summary>
        '''
        ''' <value> The right. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Right As BoundExpression

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
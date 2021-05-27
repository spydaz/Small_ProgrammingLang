'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundExpression.vb
'
' summary:	Bound expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A bound expression. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend MustInherit Class BoundExpression
        Inherits BoundNode

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public MustOverride ReadOnly Property Type As TypeSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the constant value. </summary>
        '''
        ''' <value> The constant value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overridable ReadOnly Property ConstantValue As BoundConstant

  End Class

End Namespace
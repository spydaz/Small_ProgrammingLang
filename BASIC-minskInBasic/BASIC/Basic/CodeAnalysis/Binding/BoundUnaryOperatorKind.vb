'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundUnaryOperatorKind.vb
'
' summary:	Bound unary operator kind class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Values that represent bound unary operator kinds. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend Enum BoundUnaryOperatorKind
        ''' <summary>   An enum constant representing the identity option. </summary>
        Identity
        ''' <summary>   An enum constant representing the negation option. </summary>
        Negation
        ''' <summary>   An enum constant representing the logical negation option. </summary>
        LogicalNegation
        ''' <summary>   An enum constant representing the onescomplement option. </summary>
        Onescomplement
    End Enum

End Namespace
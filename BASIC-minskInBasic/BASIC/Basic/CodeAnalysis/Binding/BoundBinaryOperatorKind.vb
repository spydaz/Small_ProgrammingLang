'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundBinaryOperatorKind.vb
'
' summary:	Bound binary operator kind class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Values that represent bound binary operator kinds. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend Enum BoundBinaryOperatorKind
        ''' <summary>   An enum constant representing the addition option. </summary>
        Addition
        Subtraction
        ''' <summary>   An enum constant representing the multiplication option. </summary>
        Multiplication
        ''' <summary>   An enum constant representing the division option. </summary>
        Division
        ''' <summary>   An enum constant representing the logical and option. </summary>
        LogicalAnd
        ''' <summary>   An enum constant representing the logical or option. </summary>
        LogicalOr
        ''' <summary>   An enum constant representing the bitwise and option. </summary>
        BitwiseAnd
        ''' <summary>   An enum constant representing the bitwise or option. </summary>
        BitwiseOr
        ''' <summary>   An enum constant representing the bitwise Exclusive-or option. </summary>
        BitwiseXor
        ''' <summary>   An enum constant representing the equals option. </summary>
        Equals
        ''' <summary>   An enum constant representing the not equals option. </summary>
        NotEquals
        ''' <summary>   An enum constant representing the less option. </summary>
        Less
        ''' <summary>   An enum constant representing the less or equals option. </summary>
        LessOrEquals
        ''' <summary>   An enum constant representing the greater or equals option. </summary>
        GreaterOrEquals
        ''' <summary>   An enum constant representing the greater option. </summary>
        Greater
    End Enum

End Namespace
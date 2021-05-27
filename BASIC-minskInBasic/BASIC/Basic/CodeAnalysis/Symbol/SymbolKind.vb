'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\SymbolKind.vb
'
' summary:	Symbol kind class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Values that represent symbol kinds. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum SymbolKind
        ''' <summary>   An enum constant representing the function] option. </summary>
        [Function]
        ''' <summary>   An enum constant representing the global variable option. </summary>
        GlobalVariable
        ''' <summary>   An enum constant representing the local variable option. </summary>
        LocalVariable
        ''' <summary>   An enum constant representing the parameter option. </summary>
        Parameter
        ''' <summary>   An enum constant representing the type option. </summary>
        Type
    End Enum

End Namespace
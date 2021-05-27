'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\LocalVariableSymbol.vb
'
' summary:	Local variable symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Binding

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A local variable symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Class LocalVariableSymbol
        Inherits VariableSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name">         The name. </param>
        ''' <param name="isReadOnly">   True if is read only, false if not. </param>
        ''' <param name="type">         The type. </param>
        ''' <param name="constant">     The constant. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Sub New(name As String, isReadOnly As Boolean, type As TypeSymbol, constant As BoundConstant)
            MyBase.New(name, isReadOnly, type, constant)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SymbolKind = SymbolKind.LocalVariable

  End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\ParameterSymbol.vb
'
' summary:	Parameter symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A parameter symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class ParameterSymbol
        Inherits LocalVariableSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name">     The name. </param>
        ''' <param name="type">     The type. </param>
        ''' <param name="ordinal">  The ordinal. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(name As String, type As TypeSymbol, ordinal As Integer)
            MyBase.New(name, True, type, Nothing)
            Me.Ordinal = ordinal
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SymbolKind = SymbolKind.Parameter

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the ordinal. </summary>
        '''
        ''' <value> The ordinal. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Ordinal As Integer

    End Class

End Namespace
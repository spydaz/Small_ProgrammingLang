'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\TypeSymbol.vb
'
' summary:	Type symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A type symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class TypeSymbol
        Inherits Symbol
        ''' <summary>   The error]. </summary>

        Public Shared ReadOnly [Error] As New TypeSymbol("?")
        ''' <summary>   any. </summary>
        Public Shared ReadOnly Any As New TypeSymbol("any")
        ''' <summary>   The bool. </summary>
        Public Shared ReadOnly Bool As New TypeSymbol("bool")
        ''' <summary>   The int. </summary>
        Public Shared ReadOnly Int As New TypeSymbol("int")
        ''' <summary>   The string]. </summary>
        Public Shared ReadOnly [String] As New TypeSymbol("string")
        ''' <summary>   The void. </summary>
        Public Shared ReadOnly Void As New TypeSymbol("void")

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Specialized constructor for use only by derived class. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name"> The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub New(name As String)
            MyBase.New(name)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SymbolKind = SymbolKind.Type

  End Class

End Namespace
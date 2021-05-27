'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\VariableSymbol.vb
'
' summary:	Variable symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On
Imports Basic.CodeAnalysis.Binding

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A variable symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class VariableSymbol
        Inherits Symbol

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
            MyBase.New(name)
            Me.IsReadOnly = isReadOnly
            Me.Type = type
            Me.Constant = If(isReadOnly, constant, Nothing)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the is read only. </summary>
        '''
        ''' <value> The is read only. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IsReadOnly As Boolean

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Type As TypeSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the constant. </summary>
        '''
        ''' <value> The constant. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend ReadOnly Property Constant As BoundConstant

    End Class

End Namespace
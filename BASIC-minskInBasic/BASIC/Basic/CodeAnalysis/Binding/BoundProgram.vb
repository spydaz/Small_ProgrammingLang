'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundProgram.vb
'
' summary:	Bound program class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundProgram

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="previous">         The previous. </param>
        ''' <param name="diagnostics">      The diagnostics. </param>
        ''' <param name="mainFunction">     The main function. </param>
        ''' <param name="scriptFunction">   The script function. </param>
        ''' <param name="Functions">        The functions. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(previous As BoundProgram,
                   diagnostics As ImmutableArray(Of Diagnostic),
                   mainFunction As FunctionSymbol,
                   scriptFunction As FunctionSymbol,
                   Functions As ImmutableDictionary(Of FunctionSymbol, BoundBlockStatement))
            Me.Previous = previous
            Me.Diagnostics = diagnostics
            Me.MainFunction = mainFunction
            Me.ScriptFunction = scriptFunction
            Me.Functions = Functions
            'Me.Statement = Statement
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the previous. </summary>
        '''
        ''' <value> The previous. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Previous As BoundProgram

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the diagnostics. </summary>
        '''
        ''' <value> The diagnostics. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Diagnostics As ImmutableArray(Of Diagnostic)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the main function. </summary>
        '''
        ''' <value> The main function. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property MainFunction As FunctionSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the script function. </summary>
        '''
        ''' <value> The script function. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property ScriptFunction As FunctionSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the functions. </summary>
        '''
        ''' <value> The functions. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Functions As ImmutableDictionary(Of FunctionSymbol, BoundBlockStatement)
        'Public ReadOnly Property Statement As BoundBlockStatement
    End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundGlobalScope.vb
'
' summary:	Bound global scope class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundGlobalScope

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="previous">         The previous. </param>
        ''' <param name="diagnostics">      The diagnostics. </param>
        ''' <param name="mainFunction">     The main function. </param>
        ''' <param name="scriptFunction">   The script function. </param>
        ''' <param name="functions">        The functions. </param>
        ''' <param name="variables">        The variables. </param>
        ''' <param name="statements">       The statements. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(previous As BoundGlobalScope,
                   diagnostics As ImmutableArray(Of Diagnostic),
                   mainFunction As FunctionSymbol,
                   scriptFunction As FunctionSymbol,
                   functions As ImmutableArray(Of FunctionSymbol),
                   variables As ImmutableArray(Of VariableSymbol),
                   statements As ImmutableArray(Of BoundStatement))
            Me.Previous = previous
            Me.Diagnostics = diagnostics
            Me.MainFunction = mainFunction
            Me.ScriptFunction = scriptFunction
            Me.Functions = functions
            Me.Variables = variables
            Me.Statements = statements
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the previous. </summary>
        '''
        ''' <value> The previous. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Previous As BoundGlobalScope

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

        Public ReadOnly Property Functions As ImmutableArray(Of FunctionSymbol)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the variables. </summary>
        '''
        ''' <value> The variables. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Variables As ImmutableArray(Of VariableSymbol)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the statements. </summary>
        '''
        ''' <value> The statements. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Statements As ImmutableArray(Of BoundStatement)

    End Class

End Namespace
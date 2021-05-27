'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundCallExpression.vb
'
' summary:	Bound call expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundCallExpression
        Inherits BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="![function]">          The [function]. </param>
        ''' <param name="As FunctionSymbol">    as function symbol. </param>
        ''' <param name="arguments">            The arguments. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New([function] As FunctionSymbol, arguments As ImmutableArray(Of BoundExpression))
            Me.Function = [function]
            Me.Arguments = arguments
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.CallExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Type As TypeSymbol
            Get
                Return [Function].Type
            End Get
        End Property
        Public ReadOnly Property [Function] As FunctionSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the arguments. </summary>
        '''
        ''' <value> The arguments. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Arguments As ImmutableArray(Of BoundExpression)

    End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundLiteralExpression.vb
'
' summary:	Bound literal expression class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundLiteralExpression
        Inherits BoundExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        '''
        ''' <param name="value">    The value. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(value As Object)
            'Me.Value = value
            If TypeOf value Is Boolean Then
                Type = TypeSymbol.Bool
            ElseIf TypeOf value Is Integer Then
                Type = TypeSymbol.Int
            ElseIf TypeOf value Is String Then
                Type = TypeSymbol.String
            Else
                Throw New Exception($"Unexpected literal '{value}' of type {value.GetType}.")
            End If
            ConstantValue = New BoundConstant(value)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As BoundNodeKind = BoundNodeKind.LiteralExpression

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Type As TypeSymbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the value. </summary>
        '''
        ''' <value> The value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Value As Object
            Get
                Return ConstantValue.Value
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the constant value. </summary>
        '''
        ''' <value> The constant value. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property ConstantValue As BoundConstant

    End Class

End Namespace
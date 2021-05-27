'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundScope.vb
'
' summary:	Bound scope class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports System.Runtime.InteropServices
Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class BoundScope
        ''' <summary>   The symbols. </summary>

        Private m_symbols As New Dictionary(Of String, Symbol)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="parent">   The parent. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(parent As BoundScope)
            Me.Parent = parent
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the parent. </summary>
        '''
        ''' <value> The parent. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Parent As BoundScope

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Attempts to declare variable. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="variable"> The variable. </param>
        '''
        ''' <returns>   True if it succeeds, false if it fails. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function TryDeclareVariable(variable As VariableSymbol) As Boolean
            Return TryDeclareSymbol(variable)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Attempts to declare symbol. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="symbol">   The symbol. </param>
        '''
        ''' <returns>   True if it succeeds, false if it fails. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function TryDeclareSymbol(Of TSymbol As Symbol)(symbol As TSymbol) As Boolean
            If m_symbols Is Nothing Then
                m_symbols = New Dictionary(Of String, Symbol)()
            ElseIf m_symbols.ContainsKey(symbol.Name) Then
                Return False
            End If
            m_symbols.Add(symbol.Name, symbol)
            Return True
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets declared variables. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The declared variables. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetDeclaredVariables() As ImmutableArray(Of VariableSymbol)
            Return GetDeclaredSymbols(Of VariableSymbol)()
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Attempts to declare function a  from the given.  </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="![function]">          The [function]. </param>
        ''' <param name="As FunctionSymbol">    as function symbol. </param>
        '''
        ''' <returns>   True if it succeeds, false if it fails. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function TryDeclareFunction([function] As FunctionSymbol) As Boolean
            Return TryDeclareSymbol([function])
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Try lookup symbol. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name"> The name. </param>
        '''
        ''' <returns>   A Symbol. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        'Public Function TryLookupVariable(name As String, <Out()> ByRef variable As VariableSymbol) As Boolean
        '  Return TryLookupSymbol(name, variable)
        'End Function

        'Public Function TryLookupFunction(name As String, <Out()> ByRef [function] As FunctionSymbol) As Boolean
        '  Return TryLookupSymbol(name, [function])
        'End Function

        'Private Function TryLookupSymbol(Of TSymbol As Symbol)(name As String, ByRef symbol As TSymbol) As Boolean

        Public Function TryLookupSymbol(name As String) As Symbol

            'symbol = Nothing

            'Dim declaredSymbol As Symbol
            'If m_symbols IsNot Nothing AndAlso m_symbols.TryGetValue(name.ToLower, declaredSymbol) Then
            '  If TypeOf declaredSymbol Is TSymbol Then
            '    symbol = DirectCast(declaredSymbol, TSymbol)
            '    Return True
            '  End If
            '  Return False
            'End If

            'If Parent Is Nothing Then
            '  Return False
            'End If

            Dim symbol As Symbol = Nothing
            If m_symbols IsNot Nothing AndAlso m_symbols.TryGetValue(name, symbol) Then
                Return symbol
            End If

            Return Parent?.TryLookupSymbol(name) ', symbol)

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets declared functions. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The declared functions. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetDeclaredFunctions() As ImmutableArray(Of FunctionSymbol)
            Return GetDeclaredSymbols(Of FunctionSymbol)()
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets declared symbols. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The declared symbols. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function GetDeclaredSymbols(Of TSymbol As Symbol)() As ImmutableArray(Of TSymbol)
      If m_symbols Is Nothing Then
        Return ImmutableArray(Of TSymbol).Empty
      End If
      Return m_symbols.Values.OfType(Of TSymbol).ToImmutableArray
    End Function

  End Class

End Namespace
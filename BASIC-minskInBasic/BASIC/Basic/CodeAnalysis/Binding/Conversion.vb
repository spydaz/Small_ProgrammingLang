'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\Conversion.vb
'
' summary:	Conversion class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class Conversion
        ''' <summary>   The none. </summary>

        Public Shared ReadOnly None As New Conversion(False, False, False)
        ''' <summary>   The identity. </summary>
        Public Shared ReadOnly Identity As New Conversion(True, True, True)
        ''' <summary>   The implicit. </summary>
        Public Shared ReadOnly Implicit As New Conversion(True, False, True)
        ''' <summary>   The explicit. </summary>
        Public Shared ReadOnly Explicit As New Conversion(True, False, False)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="exists">       True to exists. </param>
        ''' <param name="isIdentity">   True if is identity, false if not. </param>
        ''' <param name="isImplicit">   True if is implicit, false if not. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(exists As Boolean, isIdentity As Boolean, isImplicit As Boolean)
            Me.Exists = exists
            Me.IsIdentity = isIdentity
            Me.IsImplicit = isImplicit
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the exists. </summary>
        '''
        ''' <value> The exists. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Exists As Boolean

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the is identity. </summary>
        '''
        ''' <value> The is identity. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IsIdentity As Boolean

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the is implicit. </summary>
        '''
        ''' <value> The is implicit. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IsImplicit As Boolean

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the is explicit. </summary>
        '''
        ''' <value> The is explicit. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property IsExplicit As Boolean
            Get
                Return Exists AndAlso Not IsImplicit
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Classifies. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="![from]">          The [from]. </param>
        ''' <param name="As TypeSymbol">    as type symbol. </param>
        ''' <param name="![to]">            The [to]. </param>
        ''' <param name="As TypeSymbol">    as type symbol. </param>
        '''
        ''' <returns>   A Conversion. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Classify([from] As TypeSymbol, [to] As TypeSymbol) As Conversion

      If [from] Is [to] Then
        Return Conversion.Identity
      End If

      If [from] IsNot TypeSymbol.Void AndAlso [to] Is TypeSymbol.Any Then
        Return Conversion.Implicit
      End If

      If [from] Is TypeSymbol.Any AndAlso [to] IsNot TypeSymbol.Void Then
        Return Conversion.Explicit
      End If

      If [from] Is TypeSymbol.Bool OrElse [from] Is TypeSymbol.Int Then
        If [to] Is TypeSymbol.String Then
          Return Conversion.Explicit
        End If
      End If

      If [from] Is TypeSymbol.String Then
        If [to] Is TypeSymbol.Bool OrElse [to] Is TypeSymbol.Int Then
          Return Conversion.Explicit
        End If
      End If

      Return Conversion.None

    End Function

  End Class

End Namespace
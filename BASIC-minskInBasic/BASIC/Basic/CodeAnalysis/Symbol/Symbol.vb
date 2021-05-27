'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\Symbol.vb
'
' summary:	Symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.IO

Namespace Global.Basic.CodeAnalysis.Symbols

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class Symbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Specialized constructor for use only by derived class. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name"> The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Friend Sub New(name As String)
            Me.Name = name
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public MustOverride ReadOnly Property Kind As SymbolKind

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the name. </summary>
        '''
        ''' <value> The name. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Name As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes to. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub WriteTo(writer As TextWriter)
            SymbolPrinter.WriteTo(Me, writer)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
      Using writer = New StringWriter()
        WriteTo(writer)
        Return writer.ToString()
      End Using
    End Function

  End Class

End Namespace
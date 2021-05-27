'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SeparatedSyntaxList.vb
'
' summary:	Separated syntax list class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> List of separated syntaxes. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public MustInherit Class SeparatedSyntaxList

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets with separators. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        '''                                                 the required range. </exception>
        '''
        ''' <returns>   The with separators. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public MustOverride Function GetWithSeparators() As ImmutableArray(Of SyntaxNode)

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A syntax node. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class SeparatedSyntaxList(Of T As SyntaxNode)
        Inherits SeparatedSyntaxList
        Implements IEnumerable(Of T)
        ''' <summary>   The nodes and separators. </summary>

        Private ReadOnly m_nodesAndSeparators As ImmutableArray(Of SyntaxNode)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="nodesAndSeparators">   The nodes and separators. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(nodesAndSeparators As ImmutableArray(Of SyntaxNode))
            m_nodesAndSeparators = nodesAndSeparators
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the number of.  </summary>
        '''
        ''' <value> The count. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Count As Integer
            Get
                Return (m_nodesAndSeparators.Length + 1) \ 2
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the item. </summary>
        '''
        ''' <value> The item. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Default Public ReadOnly Property Item(index As Integer) As T
            Get
                Return CType(m_nodesAndSeparators(index * 2), T)
            End Get
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets a separator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        '''                                                 the required range. </exception>
        '''
        ''' <param name="index">    Zero-based index of the. </param>
        '''
        ''' <returns>   The separator. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetSeparator(index As Integer) As SyntaxToken
            If index < 0 OrElse index >= Count - 1 Then Throw New ArgumentOutOfRangeException(NameOf(index))
            Return CType(m_nodesAndSeparators(index * 2 + 1), SyntaxToken)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets with separators. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The with separators. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function GetWithSeparators() As ImmutableArray(Of SyntaxNode)
            Return m_nodesAndSeparators
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns an enumerator that iterates through the collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   An enumerator that can be used to iterate through the collection. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            For i = 0 To Count - 1
                Yield Me(i)
            Next
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Enumerable get enumerator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   An IEnumerator. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return GetEnumerator()
    End Function

  End Class

End Namespace
'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxTree.vb
'
' summary:	Syntax tree class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports System.Data
Imports Basic.CodeAnalysis.Text

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A syntax tree. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class SyntaxTree

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Handler, called when the parse. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="root">         [in,out] The root. </param>
        ''' <param name="diagnostics">  [in,out] The diagnostics. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Delegate Sub ParseHandler(tree As SyntaxTree,
                                          ByRef root As CompilationUnitSyntax,
                                          ByRef diagnostics As ImmutableArray(Of Diagnostic))

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">     The text. </param>
        ''' <param name="handler">  The handler. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub New(text As SourceText, handler As ParseHandler)

            Me.Text = text

            'Dim parser = New Parser(Me)
            Dim root As CompilationUnitSyntax = Nothing ' = parser.ParseCompilationUnit()
            Dim d As ImmutableArray(Of Diagnostic) = Nothing
            handler(Me, root, d)

            Diagnostics = d 'parser.Diagnostics.ToImmutableArray
            Me.Root = root

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the text. </summary>
        '''
        ''' <value> The text. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Text As SourceText

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the diagnostics. </summary>
        '''
        ''' <value> The diagnostics. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Diagnostics As ImmutableArray(Of Diagnostic)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the root. </summary>
        '''
        ''' <value> The root. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Root As CompilationUnitSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Loads the given file. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="fileName"> The file name to load. </param>
        '''
        ''' <returns>   A SyntaxTree. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Load(fileName As String) As SyntaxTree
            Dim text = System.IO.File.ReadAllText(fileName)
            Dim source = SourceText.From(text, fileName)
            Return Parse(source)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parses. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">         The tree. </param>
        ''' <param name="root">         [in,out] The root. </param>
        ''' <param name="diagnostics">  [in,out] The diagnostics. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Sub Parse(tree As SyntaxTree, ByRef root As CompilationUnitSyntax, ByRef diagnostics As ImmutableArray(Of Diagnostic))
            Dim parser = New Parser(tree)
            root = parser.ParseCompilationUnit
            diagnostics = parser.Diagnostics.ToImmutableArray
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parses. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        '''
        ''' <returns>   A SyntaxTree. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Parse(text As String) As SyntaxTree
            Dim source = SourceText.From(text)
            Return Parse(source)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parses the given text. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        '''
        ''' <returns>   A SyntaxTree. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Parse(text As SourceText) As SyntaxTree
            Return New SyntaxTree(text, AddressOf Parse)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse tokens. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">             The text. </param>
        ''' <param name="includeEndOfFile"> (Optional) True to include, false to exclude the end of file. </param>
        '''
        ''' <returns>   An ImmutableArray(Of SyntaxToken) </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function ParseTokens(text As String, Optional includeEndOfFile As Boolean = False) As ImmutableArray(Of SyntaxToken)
            Dim source = SourceText.From(text)
            Return ParseTokens(source, includeEndOfFile)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse tokens. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">             The text. </param>
        ''' <param name="diagnostics">      [in,out] The diagnostics. </param>
        ''' <param name="includeEndOfFile"> (Optional) True to include, false to exclude the end of file. </param>
        '''
        ''' <returns>   An ImmutableArray(Of SyntaxToken) </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function ParseTokens(text As String, ByRef diagnostics As ImmutableArray(Of Diagnostic), Optional includeEndOfFile As Boolean = False) As ImmutableArray(Of SyntaxToken)
            Dim source = SourceText.From(text)
            Return ParseTokens(source, diagnostics, includeEndOfFile)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse tokens. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">             The text. </param>
        ''' <param name="includeEndOfFile"> (Optional) True to include, false to exclude the end of file. </param>
        '''
        ''' <returns>   An ImmutableArray(Of SyntaxToken) </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function ParseTokens(text As SourceText, Optional includeEndOfFile As Boolean = False) As ImmutableArray(Of SyntaxToken)
            Return ParseTokens(text, Nothing, includeEndOfFile)
        End Function
        ''' <summary>   The parsed tokens. </summary>

        ' I think this will work; Minsk handled this by leveraging C#'s local function capability.
        Private Shared ReadOnly m_parsedTokens As New List(Of SyntaxToken)
        ''' <summary>   True to include, false to exclude the end of file. </summary>
        Private Shared m_includeEndOfFile As Boolean

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse tokens. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text">             The text. </param>
        ''' <param name="diagnostics">      [in,out] The diagnostics. </param>
        ''' <param name="includeEndOfFile"> (Optional) True to include, false to exclude the end of file. </param>
        '''
        ''' <returns>   An ImmutableArray(Of SyntaxToken) </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function ParseTokens(text As SourceText, ByRef diagnostics As ImmutableArray(Of Diagnostic), Optional includeEndOfFile As Boolean = False) As ImmutableArray(Of SyntaxToken)
            m_includeEndOfFile = includeEndOfFile
            m_parsedTokens.Clear()
            ' ParseTokens local function was here....
            Dim st = New SyntaxTree(text, AddressOf ParseTokens_ParseTokens)
            diagnostics = st.Diagnostics.ToImmutableArray
            Return m_parsedTokens.ToImmutableArray
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Parse tokens parse tokens. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="st">   The st. </param>
        ''' <param name="root"> [in,out] The root. </param>
        ''' <param name="d">    [in,out] an ImmutableArray(OfDiagnostic) to process. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Shared Sub ParseTokens_ParseTokens(st As SyntaxTree, ByRef root As CompilationUnitSyntax, ByRef d As ImmutableArray(Of Diagnostic))
      'root = Nothing
      Dim l = New Lexer(st)
      Do
        Dim token = l.Lex
        If token.Kind = SyntaxKind.EndOfFileToken Then
          root = New CompilationUnitSyntax(st, ImmutableArray(Of MemberSyntax).Empty, token)
        End If
        If token.Kind <> SyntaxKind.EndOfFileToken OrElse m_includeEndOfFile Then
          m_parsedTokens.Add(token)
        End If
        If token.Kind = SyntaxKind.EndOfFileToken Then Exit Do
      Loop
      d = l.Diagnostics.ToImmutableArray
    End Sub

  End Class

End Namespace
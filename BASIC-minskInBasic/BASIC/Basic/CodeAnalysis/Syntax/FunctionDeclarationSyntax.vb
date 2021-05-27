'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\FunctionDeclarationSyntax.vb
'
' summary:	Function declaration syntax class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A function declaration syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Public NotInheritable Class FunctionDeclarationSyntax
        Inherits MemberSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="tree">             The tree. </param>
        ''' <param name="functionKeyword">  The function keyword. </param>
        ''' <param name="identifier">       The identifier. </param>
        ''' <param name="openParen">        The open paren. </param>
        ''' <param name="parameters">       Options for controlling the operation. </param>
        ''' <param name="closeParen">       The close paren. </param>
        ''' <param name="type">             The type. </param>
        ''' <param name="body">             The body. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(tree As SyntaxTree, functionKeyword As SyntaxToken,
                identifier As SyntaxToken,
                openParen As SyntaxToken,
                parameters As SeparatedSyntaxList(Of ParameterSyntax),
                closeParen As SyntaxToken,
                type As TypeClauseSyntax,
                body As StatementSyntax)
            MyBase.New(tree)
            Me.FunctionKeyword = functionKeyword
            Me.Identifier = identifier
            Me.OpenParen = openParen
            Me.Parameters = parameters
            Me.CloseParen = closeParen
            Me.Type = type
            Me.Body = body
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.FunctionDeclaration

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the function keyword. </summary>
        '''
        ''' <value> The function keyword. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property FunctionKeyword As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the identifier. </summary>
        '''
        ''' <value> The identifier. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Identifier As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the open paren. </summary>
        '''
        ''' <value> The open paren. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property OpenParen As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets options for controlling the operation. </summary>
        '''
        ''' <value> The parameters. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Parameters As SeparatedSyntaxList(Of ParameterSyntax)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the close paren. </summary>
        '''
        ''' <value> The close paren. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property CloseParen As SyntaxToken

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Type As TypeClauseSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the body. </summary>
        '''
        ''' <value> The body. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Body As StatementSyntax
    End Class

End Namespace
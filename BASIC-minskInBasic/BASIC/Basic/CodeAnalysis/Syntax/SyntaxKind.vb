'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxKind.vb
'
' summary:	Syntax kind class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Values that represent syntax kinds. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum SyntaxKind
        ''' <summary>   An enum constant representing the bad token option. </summary>

        BadToken
        ''' <summary>   An enum constant representing the skipped text trivia option. </summary>

        ' Trivia
        SkippedTextTrivia
        ''' <summary>   An enum constant representing the line break trivia option. </summary>
        LineBreakTrivia
        ''' <summary>   An enum constant representing the whitespace trivia option. </summary>
        WhitespaceTrivia
        ''' <summary>   An enum constant representing the single line comment trivia option. </summary>
        SingleLineCommentTrivia
        ''' <summary>   An enum constant representing the multi line comment trivia option. </summary>
        MultiLineCommentTrivia
        ''' <summary>   An enum constant representing the end of file token option. </summary>

        ' Tokens
        EndOfFileToken
        ''' <summary>   An enum constant representing the number token option. </summary>
        NumberToken
        ''' <summary>   An enum constant representing the string token option. </summary>
        StringToken
        ''' <summary>   An enum constant representing the plus token option. </summary>
        PlusToken
        ''' <summary>   An enum constant representing the minus token option. </summary>
        MinusToken
        ''' <summary>   An enum constant representing the star token option. </summary>
        StarToken
        ''' <summary>   An enum constant representing the slash token option. </summary>
        SlashToken
        ''' <summary>   An enum constant representing the bang token option. </summary>
        BangToken
        ''' <summary>   An enum constant representing the equals token option. </summary>
        EqualsToken
        ''' <summary>   An enum constant representing the tilde token option. </summary>
        TildeToken
        ''' <summary>   An enum constant representing the hat token option. </summary>
        HatToken
        ''' <summary>   An enum constant representing the ampersand token option. </summary>
        AmpersandToken
        ''' <summary>   An enum constant representing the ampersand token option. </summary>
        AmpersandAmpersandToken
        ''' <summary>   An enum constant representing the equals token option. </summary>
        EqualsEqualsToken
        ''' <summary>   An enum constant representing the bang equals token option. </summary>
        BangEqualsToken
        ''' <summary>   An enum constant representing the less than token option. </summary>
        LessThanToken
        ''' <summary>   An enum constant representing the less than equals token option. </summary>
        LessThanEqualsToken
        ''' <summary>   An enum constant representing the greater than equals token option. </summary>
        'LessThanGreaterThanToken
        GreaterThanEqualsToken
        ''' <summary>   An enum constant representing the greater than token option. </summary>
        GreaterThanToken
        ''' <summary>   An enum constant representing the pipe token option. </summary>
        PipeToken
        ''' <summary>   An enum constant representing the pipe token option. </summary>
        PipePipeToken
        ''' <summary>   An enum constant representing the open paren token option. </summary>
        OpenParenToken
        ''' <summary>   An enum constant representing the close paren token option. </summary>
        CloseParenToken
        ''' <summary>   An enum constant representing the open brace token option. </summary>
        OpenBraceToken
        ''' <summary>   An enum constant representing the close brace token option. </summary>
        CloseBraceToken
        ''' <summary>   An enum constant representing the colon token option. </summary>
        ColonToken
        ''' <summary>   An enum constant representing the comma token option. </summary>
        CommaToken
        ''' <summary>   An enum constant representing the identifier token option. </summary>
        IdentifierToken
        ''' <summary>   An enum constant representing the false keyword option. </summary>

        ' Keywords

        FalseKeyword
        ''' <summary>   An enum constant representing the true keyword option. </summary>
        TrueKeyword
        ''' <summary>   An enum constant representing the let keyword option. </summary>

        'NotKeyword
        'AndKeyword
        'AndAlsoKeyword
        'OrKeyword
        'OrElseKeyword

        LetKeyword 'TODO: LET has a different behavior in BASIC.
        ''' <summary>   An enum constant representing the return keyword option. </summary>
        ReturnKeyword
        ''' <summary>   An enum constant representing the Variable keyword option. </summary>
        VarKeyword
        'DimKeyword

        FunctionKeyword
        ''' <summary>   An enum constant representing if keyword option. </summary>

        IfKeyword
        ''' <summary>   An enum constant representing the break keyword option. </summary>
        'ThenKeyword
        BreakKeyword
        ''' <summary>   An enum constant representing the continue keyword option. </summary>
        ContinueKeyword
        ''' <summary>   An enum constant representing the else keyword option. </summary>
        ElseKeyword
        ''' <summary>   An enum constant representing the while keyword option. </summary>
        'ElseIfKeyword
        'EndIfKeyword

        WhileKeyword
        ''' <summary>   An enum constant representing the do keyword option. </summary>
        DoKeyword
        ''' <summary>   An enum constant representing for keyword option. </summary>

        ForKeyword
        ''' <summary>   An enum constant representing to keyword option. </summary>
        ToKeyword
        ''' <summary>   An enum constant representing the compilation unit option. </summary>

        ' Nodes

        CompilationUnit
        ''' <summary>   An enum constant representing the global statement option. </summary>
        GlobalStatement
        FunctionDeclaration
        ''' <summary>   An enum constant representing the parameter option. </summary>
        Parameter
        ''' <summary>   An enum constant representing the else clause option. </summary>
        ElseClause
        ''' <summary>   An enum constant representing the type clause option. </summary>
        TypeClause
        ''' <summary>   An enum constant representing the block statement option. </summary>

        ' Statements

        BlockStatement
        ''' <summary>   An enum constant representing the variable declaration option. </summary>
        VariableDeclaration
        ''' <summary>   An enum constant representing if statement option. </summary>
        IfStatement
        ''' <summary>   An enum constant representing the while statement option. </summary>
        WhileStatement
        ''' <summary>   An enum constant representing the do while statement option. </summary>
        DoWhileStatement
        ''' <summary>   An enum constant representing for statement option. </summary>
        ForStatement
        ''' <summary>   An enum constant representing the break statement option. </summary>
        BreakStatement
        ''' <summary>   An enum constant representing the continue statement option. </summary>
        ContinueStatement
        ''' <summary>   An enum constant representing the return statement option. </summary>
        ReturnStatement
        ''' <summary>   An enum constant representing the expression statement option. </summary>
        ExpressionStatement
        ''' <summary>   An enum constant representing the literal expression option. </summary>

        ' Expressions

        LiteralExpression
        ''' <summary>   An enum constant representing the name expression option. </summary>
        NameExpression
        ''' <summary>   An enum constant representing the unary expression option. </summary>
        UnaryExpression
        ''' <summary>   An enum constant representing the binary expression option. </summary>
        BinaryExpression
        ''' <summary>   An enum constant representing the paren expression option. </summary>
        ParenExpression
        ''' <summary>   An enum constant representing the assignment expression option. </summary>
        AssignmentExpression
        ''' <summary>   An enum constant representing the call expression option. </summary>
        CallExpression

    End Enum

End Namespace
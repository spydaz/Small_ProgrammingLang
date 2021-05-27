'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Syntax\SyntaxNode_GetChildren.g.vb
'
' summary:	Syntax node get children.g class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Reflection
Imports Basic.CodeAnalysis.Syntax

Namespace Global.Basic.CodeAnalysis.Syntax

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An assignment expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class AssignmentExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield IdentifierToken
            Yield EqualsToken
            Yield Expression
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A binary expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class BinaryExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Left
            Yield OperatorToken
            Yield Right
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A block statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class BlockStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield OpenBraceToken
            For Each child In Statements
                Yield child
            Next
            Yield CloseBraceToken
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A break statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class BreakStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Keyword
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A call expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class CallExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Identifier
            Yield OpenParen
            For Each child In Arguments.GetWithSeparators
                Yield child
            Next
            Yield CloseParen
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A compilation unit syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class CompilationUnitSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            For Each child In Members
                Yield child
            Next
            Yield EndOfFileToken
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A continue statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ContinueStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Keyword
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A do while statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class DoWhileStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield DoKeyword
            Yield Body
            Yield WhileKeyword
            Yield Condition
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An else clause syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ElseClauseSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield ElseKeyword
            Yield ElseStatement
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An expression statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ExpressionStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Expression
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> for statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ForStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Keyword
            Yield Identifier
            Yield EqualsToken
            Yield LowerBound
            Yield ToKeyword
            Yield UpperBound
            Yield Body
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A function declaration syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class FunctionDeclarationSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield FunctionKeyword
            Yield Identifier
            Yield OpenParen
            For Each child In Parameters.GetWithSeparators
                Yield child
            Next
            Yield CloseParen
            Yield Type
            Yield Body
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A global statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class GlobalStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Statement
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> if statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class IfStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield IfKeyword
            Yield Condition
            Yield ThenStatement
            Yield ElseClause
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A literal expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class LiteralExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield LiteralToken
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A name expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class NameExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield IdentifierToken
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A parameter syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ParameterSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Identifier
            Yield Type
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A paren expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ParenExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield OpenParenToken
            Yield Expression
            Yield CloseParenToken
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A return statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class ReturnStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield ReturnKeyword
            Yield Expression
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A type clause syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class TypeClauseSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield ColonToken
            Yield Identifier
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> An unary expression syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class UnaryExpressionSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield OperatorToken
            Yield Operand
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A variable declaration syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class VariableDeclarationSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
            Yield Keyword
            Yield Identifier
            Yield TypeClause
            Yield EqualsToken
            Yield Initializer
        End Function

    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A while statement syntax. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Partial Class WhileStatementSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets the childrens in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process the childrens in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Iterator Function GetChildren() As IEnumerable(Of SyntaxNode)
      Yield WhileKeyword
      Yield Condition
      Yield Body
    End Function

  End Class

End Namespace
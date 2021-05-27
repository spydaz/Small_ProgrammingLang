'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\BoundNodeKind.vb
'
' summary:	Bound node kind class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Binding

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> Values that represent bound node kinds. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Friend Enum BoundNodeKind
        ''' <summary>   An enum constant representing the nop statement option. </summary>

        ' Statements
        NopStatement
        ''' <summary>   An enum constant representing the block statement option. </summary>
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
        ''' <summary>   An enum constant representing the label statement option. </summary>
        LabelStatement
        ''' <summary>   An enum constant representing the goto statement option. </summary>
        GotoStatement
        ''' <summary>   An enum constant representing the conditional goto statement option. </summary>
        ConditionalGotoStatement
        ''' <summary>   An enum constant representing the return statement option. </summary>
        ReturnStatement
        ''' <summary>   An enum constant representing the expression statement option. </summary>
        ExpressionStatement
        ''' <summary>   An enum constant representing the error expression option. </summary>

        ' Expressions
        ErrorExpression
        ''' <summary>   An enum constant representing the literal expression option. </summary>
        LiteralExpression
        ''' <summary>   An enum constant representing the variable expression option. </summary>
        VariableExpression
        ''' <summary>   An enum constant representing the assignment expression option. </summary>
        AssignmentExpression
        ''' <summary>   An enum constant representing the unary expression option. </summary>
        UnaryExpression
        ''' <summary>   An enum constant representing the binary expression option. </summary>
        BinaryExpression
        ''' <summary>   An enum constant representing the call expression option. </summary>
        CallExpression
        ''' <summary>   An enum constant representing the conversion expression option. </summary>
        ConversionExpression

    End Enum

End Namespace
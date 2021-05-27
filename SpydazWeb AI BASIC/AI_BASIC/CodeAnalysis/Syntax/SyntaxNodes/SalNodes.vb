'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Syntax\SyntaxNodes\SalNodes.vb
'
' summary:	Sal nodes class
'---------------------------------------------------------------------------------------------------

Imports AI_BASIC.CodeAnalysis.Compiler.Environment
Imports AI_BASIC.Typing

Namespace Syntax
    Namespace SyntaxNodes

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A sal expression syntax. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend MustInherit Class SalExpressionSyntax
            Inherits SyntaxNode

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Initializes the Type for the Syntax Node to identify the node. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="syntaxType">       SyntaxType. </param>
            ''' <param name="syntaxTypeStr">    String version of the Type. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(syntaxType As SyntaxType, syntaxTypeStr As String)
                MyBase.New(syntaxType, syntaxTypeStr)
            End Sub

        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A sal command expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Class SalCommandExpression
            Inherits SalExpressionSyntax
            ''' <summary>   The command. </summary>
            Public Cmd As SyntaxToken

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="cmd">  The command. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(cmd As SyntaxToken)
                MyBase.New(SyntaxType.SalCommand, "SalCommand")
                Me.Cmd = cmd
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return Cmd._Value
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A sal function expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Class SalFunctionExpression
            Inherits SalExpressionSyntax
            ''' <summary>   The command. </summary>
            Public Cmd As SalCommandExpression
            ''' <summary>   The parameter. </summary>
            Public Param As SalLiteralExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="_syntaxType">  Type of the syntax. </param>
            ''' <param name="cmd">          The command. </param>
            ''' <param name="param">        The parameter. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(_syntaxType As SyntaxType, cmd As SalCommandExpression, param As SalLiteralExpression)
                MyBase.New(_syntaxType, _syntaxType.GetSyntaxTypeStr)


                Me.Cmd = cmd
                Me.Param = param
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return Cmd.Evaluate(ParentEnv) & " " & Param.Evaluate(ParentEnv)
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A sal literal expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Class SalLiteralExpression
            Inherits SalExpressionSyntax
            ''' <summary>   The literal. </summary>
            Public _Literal As SyntaxNode

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="syntaxType">   Type of the syntax. </param>
            ''' <param name="literal">      The literal. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(syntaxType As SyntaxType, literal As SyntaxNode)
                MyBase.New(syntaxType, syntaxType.GetSyntaxTypeStr)
                _Literal = literal
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>
            ''' Evaluates node in the interpretor;
            ''' To evaluate a node ;
            ''' (1) It will require an Memeory Environment from its parent caller
            '''     The Environment Will Contain the variables and functions, which the expression will have
            '''     access to to evalute correctly.
            ''' (2) To get the values use Get Children ,
            '''        Evaluating the Correct values returned.
            ''' </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="ParentEnv">    [in,out] The parent environment. </param>
            '''
            ''' <returns>   An Object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function Evaluate(ByRef ParentEnv As EnvironmentalMemory) As Object
                Return _Literal.Evaluate(ParentEnv)
            End Function
        End Class
    End Namespace
End Namespace


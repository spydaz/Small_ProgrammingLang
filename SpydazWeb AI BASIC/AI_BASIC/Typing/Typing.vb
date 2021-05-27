'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Syntax\Typing.vb
'
' summary:	Typing class
'---------------------------------------------------------------------------------------------------

Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Web.Script.Serialization

Namespace Typing

    Public Module ReplCmds

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Values that represent repl Commands. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Enum ReplCmd
            ''' <summary>   An enum constant representing the show token tree option. </summary>
            ShowTokenTree
            ''' <summary>   An enum constant representing the hide token tree option. </summary>
            HideTokenTree
            ''' <summary>   An enum constant representing the show expression tree option. </summary>
            ShowExpressionTree
            ''' <summary>   An enum constant representing the hide expression tree option. </summary>
            HideExpressionTree
            ''' <summary>   An enum constant representing the clear screen option. </summary>
            ClearScreen
            ''' <summary>   An enum constant representing the load editor option. </summary>
            LoadEditor
            ''' <summary>   An enum constant representing the load tester option. </summary>
            LoadTester
            ''' <summary>   An enum constant representing the no comand option. </summary>
            NoComand
            ''' <summary>   An enum constant representing the run option. </summary>
            Run
            ''' <summary>   An enum constant representing the compile option. </summary>
            Compile
        End Enum

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Check if repl command. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="InputStr"> [in,out] The input string. </param>
        '''
        ''' <returns>   A ReplCmd. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Function CheckIfReplCmd(ByRef InputStr As String) As ReplCmd
            Select Case UCase(InputStr)
                Case "SHOWTOKENTREE"
                    Return ReplCmd.ShowTokenTree
                Case "HIDETOKENTREE"
                    Return ReplCmd.HideTokenTree
                Case "SHOWEXPRESSIONTREE"
                    Return ReplCmd.ShowExpressionTree
                Case "HIDEEXPRESSIONTREE"
                    Return ReplCmd.HideExpressionTree
                Case "CLEARSCREEN"
                    Return ReplCmd.ClearScreen
                Case "LOADEDITOR"
                    Return ReplCmd.LoadEditor
                Case "LOADTESTER"
                    Return ReplCmd.LoadTester
                Case "RUN"
                    Return ReplCmd.Run
                Case "COMPILE"
                    Return ReplCmd.Compile
                Case Else
                    Return ReplCmd.NoComand
            End Select
        End Function



    End Module

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A location. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
    Public Class Location
        ''' <summary>   The start. </summary>
        Public _Start As Integer
        ''' <summary>   The end. </summary>
        Public _End As Integer
        ''' <summary>   The length. </summary>
        Public _Length As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="iStart">   Zero-based index of the start. </param>
        ''' <param name="iEnd">     Zero-based index of the end. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(iStart As Integer, iEnd As Integer)
            _Start = iStart
            _End = iEnd
            _Length = iEnd - iStart
        End Sub
#Region "TOSTRING"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Converts this  to a JSON. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   This  as a String. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Function ToJson() As String
            Return FormatJsonOutput(ToString)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns a string that represents the current object. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   A string that represents the current object. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Function ToString() As String
            Dim Converter As New JavaScriptSerializer
            Return Converter.Serialize(Me)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Format JSON output. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="jsonString">   The JSON string. </param>
        '''
        ''' <returns>   The formatted JSON output. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function FormatJsonOutput(ByVal jsonString As String) As String
            Dim stringBuilder = New StringBuilder()
            Dim escaping As Boolean = False
            Dim inQuotes As Boolean = False
            Dim indentation As Integer = 0

            For Each character As Char In jsonString

                If escaping Then
                    escaping = False
                    stringBuilder.Append(character)
                Else

                    If character = "\"c Then
                        escaping = True
                        stringBuilder.Append(character)
                    ElseIf character = """"c Then
                        inQuotes = Not inQuotes
                        stringBuilder.Append(character)
                    ElseIf Not inQuotes Then

                        If character = ","c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, indentation)
                        ElseIf character = "["c OrElse character = "{"c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                        ElseIf character = "]"c OrElse character = "}"c Then
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                            stringBuilder.Append(character)
                        ElseIf character = ":"c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbTab)
                        ElseIf Not Char.IsWhiteSpace(character) Then
                            stringBuilder.Append(character)
                        End If
                    Else
                        stringBuilder.Append(character)
                    End If
                End If
            Next

            Return stringBuilder.ToString()
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets debugger display. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The debugger display. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function GetDebuggerDisplay() As String
            Return ToString()
        End Function
#End Region
    End Class

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent diagnostic types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Enum DiagnosticType
        ''' <summary>   An enum constant representing the tokenizer diagnostics option. </summary>
        TokenizerDiagnostics
        ''' <summary>   An enum constant representing the expression syntax diagnostics option. </summary>
        ExpressionSyntaxDiagnostics
        ''' <summary>   An enum constant representing the evaluation diagnostics option. </summary>
        EvaluationDiagnostics
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent exception types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Enum ExceptionType
        ''' <summary>   An enum constant representing the general error option. </summary>
        GeneralError
        ''' <summary>   An enum constant representing the variable declaration error option. </summary>
        VariableDeclarationError
        ''' <summary>   An enum constant representing the memory access error option. </summary>
        MemoryAccessError
        ''' <summary>   An enum constant representing the unknown literal error option. </summary>
        UnknownLiteralError
        ''' <summary>   An enum constant representing the unknown token error option. </summary>
        UnknownTokenError
        ''' <summary>   An enum constant representing the unknown return type option. </summary>
        UnknownReturnType
        ''' <summary>   An enum constant representing the unknown expression error option. </summary>
        UnknownExpressionError
        ''' <summary>   An enum constant representing the tokenizer general exception option. </summary>
        TokenizerGeneralException
        ''' <summary>   An enum constant representing the parser general exception option. </summary>
        ParserGeneralException
        ''' <summary>   An enum constant representing the evaluation exception option. </summary>
        EvaluationException
        ''' <summary>   An enum constant representing the invalid literal type option. </summary>
        InvalidLiteralType
        ''' <summary>   An enum constant representing the invalid parameter option. </summary>
        InvalidParameter
        ''' <summary>   An enum constant representing the unableto compile error option. </summary>
        UnabletoCompileError
        ''' <summary>   An enum constant representing the unableto parse error option. </summary>
        UnabletoParseError
        ''' <summary>   An enum constant representing the unableto tokenize error option. </summary>
        UnabletoTokenizeError
        NullRefferenceError
    End Enum
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent operator types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Enum OperatorType
        ''' <summary>   An enum constant representing the add option. </summary>
        Add
        ''' <summary>   An enum constant representing the minus option. </summary>
        Minus
        ''' <summary>   An enum constant representing the divide option. </summary>
        Divide
        ''' <summary>   An enum constant representing the multiply option. </summary>
        Multiply
        ''' <summary>   An enum constant representing the add equals option. </summary>
        AddEquals
        ''' <summary>   An enum constant representing the minus equals option. </summary>
        MinusEquals
        ''' <summary>   An enum constant representing the divide equals option. </summary>
        DivideEquals
        ''' <summary>   An enum constant representing the multiply equals option. </summary>
        MultiplyEquals
        ''' <summary>   An enum constant representing the greater than option. </summary>
        GreaterThan
        ''' <summary>   An enum constant representing the less than option. </summary>
        LessThan
        ''' <summary>   An enum constant representing the greater than equals option. </summary>
        GreaterThanEquals
        ''' <summary>   An enum constant representing the less than equals option. </summary>
        LessThanEquals
        ''' <summary>   An enum constant representing the not equals option. </summary>
        NotEquals
        ''' <summary>   An enum constant representing the equivilent option. </summary>
        Equivilent
        ''' <summary>   An enum constant representing the equals option. </summary>
        Equals
        ''' <summary>   An enum constant representing the logical or option. </summary>
        LogicalOr
        ''' <summary>   An enum constant representing the logical and option. </summary>
        LogicalAnd
        ''' <summary>   An enum constant representing the logical not option. </summary>
        LogicalNot
        ''' <summary>   An enum constant representing the logicalx or option. </summary>
        LogicalxOr
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent Language types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Enum LangTypes
        ''' <summary>   An enum constant representing the sal option. </summary>
        SAL
        ''' <summary>   An enum constant representing the basic option. </summary>
        BASIC
        ''' <summary>   An enum constant representing the logo option. </summary>
        LOGO
        ''' <summary>   An enum constant representing the unknown option. </summary>
        Unknown
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent literal types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum LiteralType
        ''' <summary>   An enum constant representing the boolean option. </summary>
        _Boolean
        ''' <summary>   An enum constant representing the string option. </summary>
        _String
        ''' <summary>   An enum constant representing the array option. </summary>
        _Array
        ''' <summary>   An enum constant representing the integer option. </summary>
        _Integer
        ''' <summary>   An enum constant representing the decimal option. </summary>
        _Decimal
        ''' <summary>   An enum constant representing the date option. </summary>
        _Date
        ''' <summary>   An enum constant representing the null option. </summary>
        _NULL
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent binary operations. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum BinaryOperation
        ''' <summary>   An enum constant representing the comparison option. </summary>
        _Comparison
        ''' <summary>   An enum constant representing the multiplcative option. </summary>
        _Multiplcative
        ''' <summary>   An enum constant representing the addative option. </summary>
        _Addative
        ''' <summary>   An enum constant representing the assignment option. </summary>
        _Assignment
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent expression types. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum ExpressionType
        ''' <summary>   An enum constant representing the literal option. </summary>
        Literal
        ''' <summary>   An enum constant representing the binary expression option. </summary>
        BinaryExpression
        ''' <summary>   An enum constant representing the variable decleration option. </summary>
        VariableDecleration
        ''' <summary>   An enum constant representing the variable option. </summary>
        Variable
        ''' <summary>   An enum constant representing the conditional expression option. </summary>
        ConditionalExpression
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent functions. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum Functions
        ''' <summary>   An enum constant representing if then function option. </summary>
        If_Then_Function
        ''' <summary>   An enum constant representing if then else function option. </summary>
        If_Then_Else_Function
        ''' <summary>   An enum constant representing for next function option. </summary>
        For_Next_Function
        ''' <summary>   An enum constant representing the do while function option. </summary>
        Do_While_Function
        ''' <summary>   An enum constant representing the do until function option. </summary>
        Do_Until_Function
        ''' <summary>   An enum constant representing the variable declare function option. </summary>
        Variable_Declare_Function
        ''' <summary>   An enum constant representing the binary operation option. </summary>
        Binary_Operation
    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent classifications. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Enum Classification
        ''' <summary>   An enum constant representing the text option. </summary>
        Text
        ''' <summary>   An enum constant representing the identifier option. </summary>
        Identifier
        ''' <summary>   An enum constant representing the keyword option. </summary>
        Keyword
        ''' <summary>   An enum constant representing the literal option. </summary>
        Literal
        ''' <summary>   An enum constant representing the type option. </summary>
        Type
        ''' <summary>   An enum constant representing the comment option. </summary>
        Comment
        ''' <summary>   An enum constant representing the number option. </summary>
        number
        ''' <summary>   An enum constant representing the string option. </summary>
        _string
        ''' <summary>   An enum constant representing the unknown option. </summary>
        unknown
    End Enum
    Public Module SyntaxFacts

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is key word. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if key word, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function IsKeyWord(kind As SyntaxType) As Boolean
            Select Case kind
                Case SyntaxType.IfKeyword
                    Return True
                Case SyntaxType.ThenKeyword
                    Return True
                Case SyntaxType.ElseIfKeyword
                    Return True
                Case SyntaxType.EndIfKeyword
                    Return True
                Case SyntaxType.ForKeyword
                    Return True
                Case SyntaxType.NextKeyWord
                    Return True
                Case SyntaxType.EachKeyWord
                    Return True
                Case SyntaxType.InKeyWord
                    Return True
                Case SyntaxType.ToKeyword
                    Return True

                Case SyntaxType.VarKeyword
                    Return True
                Case SyntaxType.DimKeyword
                    Return True
                Case SyntaxType.LetKeyword
                    Return True

                Case SyntaxType.DoKeyword
                    Return True
                Case SyntaxType.WhileKeyword
                    Return True
                Case SyntaxType.UntilKeyword
                    Return True
                Case SyntaxType.LoopKeyWord
                    Return True
            End Select
            Return False
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Query if 'kind' is number. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   True if number, false if not. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function IsNumber(kind As SyntaxType) As Boolean
            Select Case kind
                Case SyntaxType._Decimal
                    Return True

                Case SyntaxType._Integer
                    Return True

            End Select

            Return False
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets a classification. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The classification. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetClassification(kind As SyntaxType) As Classification

            Dim isKeyword = kind.IsKeyWord
            Dim isNumber = kind.IsNumber
            Dim isIdentifier = kind = SyntaxType._Identifier
            Dim isString = kind = SyntaxType._String
            If isKeyword Then
                Return Classification.Keyword
            ElseIf isIdentifier Then
                Return Classification.Identifier
            ElseIf isNumber Then
                Return Classification.number
            ElseIf isString Then
                Return Classification._string
            Else
                Return Classification.unknown
            End If

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets literal type string. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="_LiteralType"> [in,out] Type of the literal. </param>
        '''
        ''' <returns>   The literal type string. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetLiteralTypeStr(ByRef _LiteralType As LiteralType) As String
            Select Case _LiteralType
                Case LiteralType._Boolean
                    Return "Boolean"
                Case LiteralType._String
                    Return "String"
                Case LiteralType._Array
                    Return "Array"
                Case LiteralType._Integer
                    Return "Integer"
                Case LiteralType._Decimal
                    Return "Decimal"
                Case LiteralType._Date
                    Return "Date"
                Case LiteralType._NULL
                    Return "Null"
            End Select
            Return "Unkown"
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets keyword syntax type. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        '''
        ''' <returns>   The keyword syntax type. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Function GetKeywordSyntaxType(text As String) As SyntaxType

            Select Case text
#Region "Psudoe maths Coding"
                Case "greater_than"
                    Return SyntaxType.GreaterThan_Operator
                Case "less_than"
                    Return SyntaxType.LessThanOperator
                Case "add_to"
                    Return SyntaxType.Add_Operator
                Case "multiply_by"
                    Return SyntaxType.Multiply_Operator
                Case "subtract_from"
                    Return SyntaxType.Sub_Operator
                Case "divide_by"
                    Return SyntaxType.Divide_Operator
                Case "greater_than_or_equals"
                    Return SyntaxType.GreaterThanEquals
                Case "less_than_or_equals"
                    Return SyntaxType.LessThanEquals
                Case "not_equal_to"
                    Return SyntaxType.NotEqual
                Case "equal_to"
                    Return SyntaxType.EquivelentTo
                Case "equals"
                    Return SyntaxType._ASSIGN
                Case "add_equals"
                    Return SyntaxType.Add_Equals_Operator
                Case "minus_equals"
                    Return SyntaxType.Minus_Equals_Operator
                Case "divide_equals"
                    Return SyntaxType.Divide_Equals_Operator
                Case "multiply_equals"
                    Return SyntaxType.Multiply_Equals_Operator
#End Region
#Region "Logical"
                Case "or"
                    Return SyntaxType.OrKeyword
                Case "and"
                    Return SyntaxType.AndKeyWord
                Case "not"
                    Return SyntaxType.NotKeyWord
#End Region
#Region "Literaltpyes"
                Case "string"
                    Return SyntaxType._StringType
                Case "boolean"
                    Return SyntaxType._BooleanType
                Case "bool"
                    Return SyntaxType._BooleanType
                Case "array"
                    Return SyntaxType._ArrayType
                Case "integer"
                    Return SyntaxType._IntegerType
                Case "decimal"
                    Return SyntaxType._DecimalType
                Case "true"
                    Return SyntaxType.TrueKeyword
                Case "false"
                    Return SyntaxType.FalseKeyword
                Case "date"
                    Return SyntaxType._DateType
#End Region
#Region "basic Functions"
#Region "Variable Declerations"
                Case "var"
                    Return SyntaxType.VarKeyword
                Case "dim"
                    Return SyntaxType.DimKeyword
                Case "as"
                    Return SyntaxType.AsKeyWord
#End Region
#Region "Assignment"
                Case "let"
                    Return SyntaxType.LetKeyword
                Case "equals"
                    Return SyntaxType._ASSIGN
#End Region
#Region "If/Then/Else"
                Case "if"
                    Return SyntaxType.IfKeyword
                Case "else"
                    Return SyntaxType.ElseKeyword
                Case "then"
                    Return SyntaxType.ThenKeyword
                Case "elseif"
                    Return SyntaxType.ElseIfKeyword
                Case "endif"
                    Return SyntaxType.EndIfKeyword
#End Region
#Region "Do/While/Until"


                Case "while"
                    Return SyntaxType.WhileKeyword
                Case "do"
                    Return SyntaxType.DoKeyword
                Case "until"
                    Return SyntaxType.UntilKeyword
                Case "loop"
                    Return SyntaxType.LoopKeyWord
#End Region
#Region "For/Each/In/To Next"

                Case "for"
                    Return SyntaxType.ForKeyword
                Case "to"
                    Return SyntaxType.ToKeyword

                Case "each"
                    Return SyntaxType.EachKeyWord
                Case "in"
                    Return SyntaxType.InKeyWord
                Case "next"
                    Return SyntaxType.NextKeyWord
#End Region
#Region "Function"
                Case "return"
                    Return SyntaxType.ReturnKeyword
                Case "function"
                    Return SyntaxType.FunctionKeyword
                Case "break"
                    Return SyntaxType.BreakKeyword
                Case "continue"
                    Return SyntaxType.ContinueKeyword
#End Region
#Region "Sal Commands"
                Case "push"
                    Return SyntaxType.SAL_PUSH
                Case "pop"
                    Return SyntaxType.SAL_POP
                Case "pull"
                    Return SyntaxType.SAL_PULL
                Case "peek"
                    Return SyntaxType.SAL_PEEK
                Case "wait"
                    Return SyntaxType.SAL_WAIT
                Case "pause"
                    Return SyntaxType.SAL_PAUSE
                Case "halt"
                    Return SyntaxType.SAL_HALT
                Case "resume"
                    Return SyntaxType.SAL_RESUME
                Case "dup"
                    Return SyntaxType.SAL_DUP
                Case "jmp"
                    Return SyntaxType.SAL_JMP
                Case "jif_t"
                    Return SyntaxType.SAL_JIF_T
                Case "jif_f"
                    Return SyntaxType.SAL_JIF_F
                Case "jif_eq"
                    Return SyntaxType.SAL_JIF_EQ
                Case "jif_gt"
                    Return SyntaxType.SAL_JIF_GT
                Case "jif_lt"
                    Return SyntaxType.SAL_JIF_LT
                Case "load"
                    Return SyntaxType.SAL_LOAD
                Case "store"
                    Return SyntaxType.SAL_STORE
                Case "remove"
                    Return SyntaxType.SAL_REMOVE
                Case "call"
                    Return SyntaxType.SAL_CALL
                Case "print_c"
                    Return SyntaxType.SAL_PRINT_C
                Case "print_m"
                    Return SyntaxType.SAL_PRINT_M
                Case "add"
                    Return SyntaxType.SAL_ADD
                Case "sub"
                    Return SyntaxType.SAL_SUB
                Case "mul"
                    Return SyntaxType.SAL_MUL
                Case "div"
                    Return SyntaxType.SAL_DIV

                Case "is_eq"
                    Return SyntaxType.SAL_IS_EQ
                Case "is_gt"
                    Return SyntaxType.SAL_IS_GT
                Case "is_lt"
                    Return SyntaxType.SAL_IS_LT
                Case "is_gte"
                    Return SyntaxType.SAL_IS_GTE
                Case "is_lte"
                    Return SyntaxType.SAL_IS_LTE
                Case "to_pos"
                    Return SyntaxType.SAL_TO_POS
                Case "is_neg"
                    Return SyntaxType.SAL_TO_NEG
                Case "incr"
                    Return SyntaxType.SAL_INCR
                Case "decr"
                    Return SyntaxType.SAL_DECR
#End Region
#Region "LOGO"
                Case "ht"
                    Return SyntaxType.LOGO_ht
                Case "hideturtle"
                    Return SyntaxType.LOGO_ht
                Case "fd"
                    Return SyntaxType.LOGO_fd
                Case "forward"
                    Return SyntaxType.LOGO_fd
                Case "bk"
                    Return SyntaxType.LOGO_bk
                Case "backward"
                    Return SyntaxType.LOGO_bk
                Case "rt"
                    Return SyntaxType.LOGO_rt
                Case "right"
                    Return SyntaxType.LOGO_rt
                Case "lt"
                    Return SyntaxType.LOGO_lt
                Case "left"
                    Return SyntaxType.LOGO_lt
                Case "label"
                    Return SyntaxType.LOGO_label
                Case "if"
                    Return SyntaxType.IfKeyword
                Case "for"
                    Return SyntaxType.ForKeyword
                Case "deref"
                    Return SyntaxType.LOGO_deref
                Case "setxy"
                    Return SyntaxType.LOGO_setxy
                Case "st"
                    Return SyntaxType.LOGO_st
                Case "stop"
                    Return SyntaxType.LOGO_Stop
                Case "pu"
                    Return SyntaxType.LOGO_pu
                Case "pd"
                    Return SyntaxType.LOGO_pd
                Case "make"
                    Return SyntaxType.LOGO_make
#End Region
#End Region


                Case Else
                    Return SyntaxType._Identifier
            End Select

        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets unary operator precedence. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The unary operator precedence. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetUnaryOperatorPrecedence(kind As SyntaxType) As Integer
            Select Case kind
                Case SyntaxType.Add_Operator, SyntaxType.Sub_Operator, SyntaxType.NotKeyWord
                    ' SyntaxKind.BangToken,
                    '   SyntaxKind.TildeToken
                    Return 6

                Case Else
                    Return 0
            End Select
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets binary operator precedence. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="kind"> The kind. </param>
        '''
        ''' <returns>   The binary operator precedence. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetBinaryOperatorPrecedence(kind As SyntaxType) As Integer
            Select Case kind

                Case SyntaxType.Multiply_Operator, SyntaxType.Divide_Operator
                    Return 5
                Case SyntaxType.Add_Operator, SyntaxType.Sub_Operator
                    Return 4

                Case SyntaxType.EquivelentTo,
                     SyntaxType.NotEqual,
                     SyntaxType.LessThanOperator,
                     SyntaxType.LessThanEquals,
                     SyntaxType.GreaterThanEquals,
                     SyntaxType.GreaterThan_Operator
                    Return 3

                Case SyntaxType.AndKeyWord
                    'SyntaxType.AmpersandAmpersandToken
                    Return 2

                Case SyntaxType.OrKeyword
                    ' SyntaxType.PipePipeToken,
                    ' SyntaxType.HatToken
                    Return 1

                Case Else
                    Return 0
            End Select
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets syntax type string. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="_Syntaxtype">  [in,out] The syntaxtype. </param>
        '''
        ''' <returns>   The syntax type string. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetSyntaxTypeStr(ByRef _Syntaxtype As SyntaxType) As String
            Select Case _Syntaxtype
'literals
                Case SyntaxType._arrayList
                    Return "_arrayList"
                Case SyntaxType._Date
                    Return "_Date"
                Case SyntaxType._String
                    Return "_String"
                Case SyntaxType._Integer
                    Return "_Integer"
                Case SyntaxType._Identifier
                    Return "_Identifier"
                Case SyntaxType._Boolean
                    Return "_Boolean"
                Case SyntaxType._Decimal
                    Return "_Decimal"

                Case SyntaxType.TrueKeyword
                    Return "TrueKeyword"
                Case SyntaxType.FalseKeyword
                    Return "FalseKeyword"
                Case SyntaxType._DecimalType
                    Return "_DecimalType"
                Case SyntaxType._IntegerType
                    Return "_IntegerType"
                Case SyntaxType._StringType
                    Return "_StringType"
                Case SyntaxType._ArrayType
                    Return "_ArrayType"
                Case SyntaxType._IntegerType
                    Return "_IntegerType"
                Case SyntaxType._DateType
                    Return "_DateType"
                Case SyntaxType._NullType
                    Return "_NullType"
                Case SyntaxType._BooleanType
                    Return "_BooleanType"
'keywords
                Case SyntaxType.IfKeyword
                    Return "IfKeyword"
                Case SyntaxType.ForKeyword
                    Return "ForKeyword"
                Case SyntaxType.ToKeyword
                    Return "ToKeyword"
                Case SyntaxType.ReturnKeyword
                    Return "ReturnKeyword"
                Case SyntaxType.WhileKeyword
                    Return "WhileKeyword"
                Case SyntaxType.DoKeyword
                    Return "DoKeyword"
                Case SyntaxType.UntilKeyword
                    Return "UntilKeword"
                Case SyntaxType.ThenKeyword
                    Return "ThenKeyword"
                Case SyntaxType.FunctionKeyword
                    Return "FunctionKeyword"
                Case SyntaxType.ElseKeyword
                    Return "ElseKeyword"
                Case SyntaxType.EndIfKeyword
                    Return "EndIfKeyword"
                Case SyntaxType.AsKeyWord
                    Return "AsKeyWord"
                Case SyntaxType.InKeyWord
                    Return "InKeyWord"
                Case SyntaxType.EachKeyWord
                    Return "EachKeyWord"
                Case SyntaxType.ToKeyword
                    Return "ToKeyword"
                Case SyntaxType.ElseIfKeyword
                    Return "ElseIfKeyword"
                Case SyntaxType._null
                    Return "_null"
                Case SyntaxType.ContinueKeyword
                    Return "ContinueKeyword"
                Case SyntaxType.BreakKeyword
                    Return "BreakKeyword"
'maths
                Case SyntaxType.Add_Operator
                    Return "Add_Operator"
                Case SyntaxType.Sub_Operator
                    Return "Sub_Operator"
                Case SyntaxType.Multiply_Operator
                    Return "Multiply_Operator"
                Case SyntaxType.Divide_Operator
                    Return "Divide_Operator"
'complex assign
                Case SyntaxType.GreaterThan_Operator
                    Return "GreaterThan_Operator"
                Case SyntaxType.LessThanOperator
                    Return "LessThanOperator"
                Case SyntaxType.GreaterThanEquals
                    Return "GreaterThanEquals"
                Case SyntaxType.LessThanEquals
                    Return "LessThanEquals"
                Case SyntaxType.Add_Equals_Operator
                    Return "Add_Equals_Operator"
                Case SyntaxType.Minus_Equals_Operator
                    Return "Minus_Equals_Operator"
                Case SyntaxType.Divide_Equals_Operator
                    Return "Divide_Equals_Operator"
                Case SyntaxType.Multiply_Equals_Operator
                    Return "Multiply_Equals_Operator"
'expressions

                Case SyntaxType._BinaryExpression
                    Return "_BinaryExpression"
                Case SyntaxType._UnaryExpression
                    Return "_UnaryExpression"
                Case SyntaxType._NumericLiteralExpression
                    Return "_NumericLiteralExpression"
                Case SyntaxType._AssignmentExpression
                    Return "_AssignmentExpression"
                Case SyntaxType._VariableDeclaration
                    Return "_VariableDeclaration"
                Case SyntaxType._ParenthesizedExpresion
                    Return "_ParenthesizedExpresion"
                Case SyntaxType._BooleanLiteralExpression
                    Return "_BooleanLiteralExpression"
                Case SyntaxType._StringExpression
                    Return "_StringExpression"
                Case SyntaxType._CodeBlock
                    Return "_CodeBlock"
                Case SyntaxType._IdentifierExpression
                    Return "_IdentifierExpression"
                Case SyntaxType.ifThenExpression
                    Return "ifThenExpression"
                Case SyntaxType.ifElseExpression
                    Return "ifElseExpression"
                Case SyntaxType.IfExpression
                    Return "IfExpression"
#Region "Expressions"
                Case SyntaxType.AddativeExpression
                    Return "AddativeExpression"
                Case SyntaxType.MultiplicativeExpression
                    Return "MultiplicativeExpression"
                Case SyntaxType.ConditionalExpression
                    Return "ConditionalExpression"
#End Region

                Case SyntaxType._CODE_BEGIN
                    Return "_CODE_BEGIN"
                Case SyntaxType._CODE_END
                    Return "_CODE_END"

'sys
                Case SyntaxType._UnknownToken
                    Return "_UnknownToken"
                Case SyntaxType._WhitespaceToken
                    Return "_WhitespaceToken"
                Case SyntaxType._EndOfFileToken
                    Return "_EndOfFileToken"

'assign
                Case SyntaxType.DimKeyword
                    Return "DimKeyword"
                Case SyntaxType.LetKeyword
                    Return "LetKeyword"
                Case SyntaxType._ASSIGN
                    Return "_Assign"
                Case SyntaxType.VarKeyword
                    Return "VarKeyword"


            End Select

            Return "_UnknownToken"
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets an operator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="_Operatortype">    [in,out] The operatortype. </param>
        '''
        ''' <returns>   The operator. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <Runtime.CompilerServices.Extension()>
        Public Function GetOperator(ByRef _Operatortype As OperatorType) As String
            Select Case _Operatortype
                Case OperatorType.Add
                    Return "+"
                Case OperatorType.Minus
                    Return "-"
                Case OperatorType.Divide
                    Return "/"
                Case OperatorType.Multiply
                    Return "*"
                Case OperatorType.AddEquals
                    Return "+="
                Case OperatorType.MinusEquals
                    Return "-="
                Case OperatorType.DivideEquals
                    Return "/="
                Case OperatorType.MultiplyEquals
                    Return "*="
                Case OperatorType.GreaterThan
                    Return ">"
                Case OperatorType.LessThan
                    Return "<"
                Case OperatorType.GreaterThanEquals
                    Return ">="
                Case OperatorType.LessThanEquals
                    Return "<="
                Case OperatorType.NotEquals
                    Return "!="
                Case OperatorType.Equivilent
                    Return "=="
                Case OperatorType.Equals
                    Return "="
                Case OperatorType.LogicalOr
                    Return "OR"
                Case OperatorType.LogicalAnd
                    Return "AND"
                Case OperatorType.LogicalNot
                    Return "NOT"
                Case OperatorType.LogicalxOr
                    Return "XOR"
                Case Else
                    Return "ERROR NOT DEFINED"
            End Select
        End Function
    End Module

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A syntax token. </summary>
    ''' Token to be returned contain as much information required,
    ''' for the Parser to make a decision on how to handle the token.
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Structure SyntaxToken
        ''' <summary>   Type of the syntax. </summary>
        Public _SyntaxType As SyntaxType
        ''' <summary>   The syntax type string. </summary>
        Public _SyntaxTypeStr As String
        ''' <summary>
        ''' Held Data
        ''' </summary>
        Public _Raw As String
        ''' <summary>
        ''' Used to hold the value as a type
        ''' </summary>
        Public _Value As Object
        ''' <summary>
        ''' Start of token(Start position)
        ''' </summary>
        Public _start As Integer
        ''' <summary>
        ''' End of token (end Position)
        ''' </summary>
        Public _End As Integer

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Converts this  to a JSON. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   This  as a String. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Function ToJson() As String
            On Error Resume Next
            If Me._Value IsNot Nothing Then
                Return FormatJsonOutput(ToString)
            End If
            Return Nothing
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns the fully qualified type name of this instance. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The fully qualified type name. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Overrides Function ToString() As String
            On Error Resume Next
            Dim Converter As New JavaScriptSerializer
            If Me._Value IsNot Nothing Then


                Return Converter.Serialize(Me)

            End If
            Return Nothing
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Format JSON output. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="jsonString">   The JSON string. </param>
        '''
        ''' <returns>   The formatted JSON output. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Function FormatJsonOutput(ByVal jsonString As String) As String
            Dim stringBuilder = New StringBuilder()
            Dim escaping As Boolean = False
            Dim inQuotes As Boolean = False
            Dim indentation As Integer = 0

            For Each character As Char In jsonString

                If escaping Then
                    escaping = False
                    stringBuilder.Append(character)
                Else

                    If character = "\"c Then
                        escaping = True
                        stringBuilder.Append(character)
                    ElseIf character = """"c Then
                        inQuotes = Not inQuotes
                        stringBuilder.Append(character)
                    ElseIf Not inQuotes Then

                        If character = ","c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, indentation)
                        ElseIf character = "["c OrElse character = "{"c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                        ElseIf character = "]"c OrElse character = "}"c Then
                            stringBuilder.Append(vbCrLf)
                            stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                            stringBuilder.Append(character)
                        ElseIf character = ":"c Then
                            stringBuilder.Append(character)
                            stringBuilder.Append(vbTab)
                        ElseIf Not Char.IsWhiteSpace(character) Then
                            stringBuilder.Append(character)
                        End If
                    Else
                        stringBuilder.Append(character)
                    End If
                End If
            Next

            Return stringBuilder.ToString()
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        '''
        ''' <param name="_sType">           [in,out] The type. </param>
        ''' <param name="syntaxTypeStr">    The syntax type string. </param>
        ''' <param name="raw">              Held Data. </param>
        ''' <param name="value">            Used to hold the value as a type. </param>
        ''' <param name="start">            Start of token(Start position) </param>
        ''' <param name="iend">             The iend. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New(ByRef _sType As SyntaxType, syntaxTypeStr As String, raw As String, value As Object, start As Integer, iend As Integer)
            If syntaxTypeStr Is Nothing Then
                Throw New ArgumentNullException(NameOf(syntaxTypeStr))
            End If

            'Intialize Token
            _SyntaxType = _sType
            _SyntaxTypeStr = syntaxTypeStr
            _Raw = raw
            _Value = value
            _start = start
            _End = iend
        End Sub
    End Structure

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Values that represent syntax types. </summary>
    ''' Descriptive Element for describing Tokens and SyntaxNodes/Expressions
    ''' Important to Keep Everything Centralized, 
    ''' This role is often Doubled up creatingtwo Different Classes of Descriptive Tokens; 
    ''' Hence Combining the TokenTypes 
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Enum SyntaxType
#Region "SYSTEM"
        ''' <summary>   An enum constant representing the unknown token option. </summary>
        _UnknownToken = 0
        ''' <summary>   An enum constant representing the whitespace token option. </summary>
        _WhitespaceToken = 1
        ''' <summary>   An enum constant representing the new line token option. </summary>
        _NewLineToken = 2
        ''' <summary>   An enum constant representing the end of file token option. </summary>
        _EndOfFileToken = 3
        ''' <summary>   An enum constant representing the end of line token option. </summary>
        _EndOfLineToken = 4
#End Region
#Region "Operartors"
        ''' <summary>   An enum constant representing the add operator option. </summary>
        Add_Operator = 11
        Sub_Operator = 12
        ''' <summary>   An enum constant representing the multiply operator option. </summary>
        Multiply_Operator = 13
        ''' <summary>   An enum constant representing the divide operator option. </summary>
        Divide_Operator = 14
        ''' <summary>   An enum constant representing the greater than operator option. </summary>
        GreaterThan_Operator = 15
        ''' <summary>   An enum constant representing the less than operator option. </summary>
        LessThanOperator = 17
        ''' <summary>   An enum constant representing the greater than equals option. </summary>
        GreaterThanEquals = 18
        ''' <summary>   An enum constant representing the less than equals option. </summary>
        LessThanEquals = 19
        ''' <summary>   An enum constant representing the add equals operator option. </summary>
        Add_Equals_Operator = 20
        ''' <summary>   An enum constant representing the minus equals operator option. </summary>
        Minus_Equals_Operator = 21
        ''' <summary>   An enum constant representing the multiply equals operator option. </summary>
        Multiply_Equals_Operator = 22
        ''' <summary>   An enum constant representing the divide equals operator option. </summary>
        Divide_Equals_Operator = 23
        ''' <summary>   An enum constant representing the equals option. </summary>
        Equals = 24
        ''' <summary>   An enum constant representing the not equal option. </summary>
        NotEqual = 25
        ''' <summary>   An enum constant representing the equivelent to option. </summary>
        EquivelentTo = 27
        ''' <summary>   An enum constant representing the not option. </summary>
        _Not = 25
        'Logical Operators
        '


#End Region
        ''' <summary>   An enum constant representing the left parenthes option. </summary>
        _leftParenthes = 30
        ''' <summary>   An enum constant representing the right parenthes option. </summary>
        _RightParenthes = 31
        ''' <summary>   An enum constant representing the simple assign option. </summary>
        _SIMPLE_ASSIGN = 32
        ''' <summary>   An enum constant representing the code begin option. </summary>
        _CODE_BEGIN = 33
        ''' <summary>   An enum constant representing the code end option. </summary>
        _CODE_END = 34
        ''' <summary>   An enum constant representing the list seperator option. </summary>
        _LIST_SEPERATOR = 35
        ''' <summary>   An enum constant representing the list end option. </summary>
        _LIST_END = 36
        ''' <summary>   An enum constant representing the list begin option. </summary>
        _LIST_BEGIN = 37
        ''' <summary>   An enum constant representing the statement end option. </summary>
        _STATEMENT_END = 38
        ''' <summary>   An enum constant representing the null option. </summary>
        _null = 40
        ''' <summary>   An enum constant representing the integer option. </summary>
        _Integer = 41
        ''' <summary>   An enum constant representing the array list option. </summary>
        _arrayList = 42
        ''' <summary>   An enum constant representing the identifier option. </summary>
        _Identifier = 43
        ''' <summary>   An enum constant representing the string option. </summary>
        _String = 44
        ''' <summary>   An enum constant representing the boolean option. </summary>
        _Boolean = 45
        ''' <summary>   An enum constant representing the decimal option. </summary>
        _Decimal = 46
        ''' <summary>   An enum constant representing the date option. </summary>
        _Date = 47
        ''' <summary>   An enum constant representing the assign option. </summary>


        _ASSIGN = 50
#Region "Universal Expressions"
        ''' <summary>   An enum constant representing the numeric literal expression option. </summary>
        _NumericLiteralExpression = 100
        ''' <summary>   An enum constant representing the identifier expression option. </summary>
        _IdentifierExpression = 101
        ''' <summary>   An enum constant representing the string expression option. </summary>
        _StringExpression = 102
        ''' <summary>   An enum constant representing the boolean literal expression option. </summary>
        _BooleanLiteralExpression = 103
        ''' <summary>   An enum constant representing the unary expression option. </summary>
        _UnaryExpression = 105
        ''' <summary>   An enum constant representing the binary expression option. </summary>
        _BinaryExpression = 110
        ''' <summary>   An enum constant representing the assignment expression option. </summary>
        _AssignmentExpression = 120
        ''' <summary>   An enum constant representing the parenthesized expresion option. </summary>
        _ParenthesizedExpresion = 130
        ''' <summary>   An enum constant representing the code block option. </summary>
        _CodeBlock = 140
        ''' <summary>   An enum constant representing the variable declaration option. </summary>
        _VariableDeclaration = 141
        ''' <summary>   An enum constant representing the multiplicative expression option. </summary>
        MultiplicativeExpression = 142
        ''' <summary>   An enum constant representing the addative expression option. </summary>
        AddativeExpression = 143
        ''' <summary>   An enum constant representing the conditional expression option. </summary>
        ConditionalExpression = 144
        ''' <summary>   An enum constant representing if then expression option. </summary>
        ifThenExpression = 145
        ''' <summary>   An enum constant representing if else expression option. </summary>
        ifElseExpression = 146
#End Region
#Region "MainLanguage"
#Region "Functions - Used In Universal RegexSearches"
        ''' <summary>   An enum constant representing the print option. </summary>


        _PRINT = 90
        ''' <summary>   An enum constant representing the function declare option. </summary>
        _FUNCTION_DECLARE = 95

#End Region

#Region "Logo"
        ''' <summary>   An enum constant representing the logo repeat option. </summary>
        LOGO_repeat = 501
        ''' <summary>   An enum constant representing the logo fd option. </summary>
        LOGO_fd = 502
        ''' <summary>   An enum constant representing the logo Block option. </summary>
        LOGO_bk = 503
        ''' <summary>   An enum constant representing the logo Right option. </summary>
        LOGO_rt = 504
        ''' <summary>   An enum constant representing the logo lt option. </summary>
        LOGO_lt = 505
        ''' <summary>   An enum constant representing the logo Create struct option. </summary>
        LOGO_cs = 506
        ''' <summary>   An enum constant representing the logo pu option. </summary>
        LOGO_pu = 507
        ''' <summary>   An enum constant representing the logo pd option. </summary>
        LOGO_pd = 508
        ''' <summary>   An enum constant representing the logo Height option. </summary>
        LOGO_ht = 509
        ''' <summary>   An enum constant representing the logo st option. </summary>
        LOGO_st = 510
        ''' <summary>   An enum constant representing the logo Dereference option. </summary>
        LOGO_deref = 511
        ''' <summary>   An enum constant representing the logo home option. </summary>
        LOGO_home = 512
        ''' <summary>   An enum constant representing the logo label option. </summary>
        LOGO_label = 513
        ''' <summary>   An enum constant representing the logo setxy option. </summary>
        LOGO_setxy = 514
        ''' <summary>   An enum constant representing the logo make option. </summary>
        LOGO_make = 515
        ''' <summary>   An enum constant representing the logo procedure invocation option. </summary>
        LOGO_procedureInvocation = 516
        ''' <summary>   An enum constant representing the logo procedure declaration option. </summary>
        LOGO_procedureDeclaration = 517
        ''' <summary>   An enum constant representing the logo parameter declarations option. </summary>
        LOGO_parameterDeclarations = 518
        ''' <summary>   An enum constant representing the logo comparison option. </summary>
        LOGO_comparison = 519
        ''' <summary>   An enum constant representing the logo comparison operator option. </summary>
        LOGO_comparisonOperator = 520
        ''' <summary>   An enum constant representing the logo stop option. </summary>
        LOGO_Stop = 522
        ''' <summary>   An enum constant representing the logo Language option. </summary>
        LOGO_LANG = 524
        ''' <summary>   An enum constant representing the logo EOL option. </summary>
        LOGO_EOL = 525
        ''' <summary>   An enum constant representing the logo sign expression option. </summary>
        LOGO_signExpression = 528
        ''' <summary>   An enum constant representing the logo multiplying expression option. </summary>
        LOGO_multiplyingExpression = 529
        ''' <summary>   An enum constant representing the logo expression option. </summary>
        LOGO_expression = 530
        ''' <summary>   An enum constant representing the logo comments option. </summary>
        LOGO_comments = 531
#End Region
#Region "SAL"
        ''' <summary>   An enum constant representing the sal null option. </summary>
        SAL_NULL = 700
        ''' <summary>   An enum constant representing the sal remove option. </summary>
        SAL_REMOVE = 701
        ''' <summary>   An enum constant representing the sal resume option. </summary>
        SAL_RESUME = 702
        ''' <summary>   An enum constant representing the sal push option. </summary>
        SAL_PUSH = 703
        ''' <summary>   An enum constant representing the sal pull option. </summary>
        SAL_PULL = 704
        ''' <summary>   An enum constant representing the sal peek option. </summary>
        SAL_PEEK = 705
        ''' <summary>   An enum constant representing the sal wait option. </summary>
        SAL_WAIT = 706
        ''' <summary>   An enum constant representing the sal pause option. </summary>
        SAL_PAUSE = 707
        ''' <summary>   An enum constant representing the sal halt option. </summary>
        SAL_HALT = 708
        ''' <summary>   An enum constant representing the sal Duplicate option. </summary>
        SAL_DUP = 709
        ''' <summary>   An enum constant representing the sal Jump option. </summary>
        SAL_JMP = 710
        ''' <summary>   An enum constant representing the sal jif t option. </summary>
        SAL_JIF_T = 711
        ''' <summary>   An enum constant representing the sal jif option. </summary>
        SAL_JIF_F = 712
        ''' <summary>   An enum constant representing the sal jif eq option. </summary>
        SAL_JIF_EQ = 713
        ''' <summary>   An enum constant representing the sal jif gt option. </summary>
        SAL_JIF_GT = 714
        ''' <summary>   An enum constant representing the sal jif lt option. </summary>
        SAL_JIF_LT = 715
        ''' <summary>   An enum constant representing the sal load option. </summary>
        SAL_LOAD = 716
        ''' <summary>   An enum constant representing the sal store option. </summary>
        SAL_STORE = 717
        ''' <summary>   An enum constant representing the sal call option. </summary>
        SAL_CALL = 718
        ''' <summary>   An enum constant representing the sal ret option. </summary>
        SAL_RET = 719
        ''' <summary>   An enum constant representing the sal print m option. </summary>
        SAL_PRINT_M = 720
        ''' <summary>   An enum constant representing the sal print c option. </summary>
        SAL_PRINT_C = 721
        ''' <summary>   An enum constant representing the sal add option. </summary>
        SAL_ADD = 722
        ''' <summary>   An enum constant representing the sal sub option. </summary>
        SAL_SUB = 723
        ''' <summary>   An enum constant representing the sal mul option. </summary>
        SAL_MUL = 724
        ''' <summary>   An enum constant representing the sal div option. </summary>
        SAL_DIV = 725
        ''' <summary>   An enum constant representing the sal and option. </summary>
        SAL_AND = 726
        ''' <summary>   An enum constant representing the sal or option. </summary>
        SAL_OR = 727
        ''' <summary>   An enum constant representing the sal not option. </summary>
        SAL_NOT = 728
        ''' <summary>   An enum constant representing the sal is eq option. </summary>
        SAL_IS_EQ = 729
        ''' <summary>   An enum constant representing the sal is gt option. </summary>
        SAL_IS_GT = 730
        ''' <summary>   An enum constant representing the sal is gte option. </summary>
        SAL_IS_GTE = 731
        ''' <summary>   An enum constant representing the sal is lt option. </summary>
        SAL_IS_LT = 732
        ''' <summary>   An enum constant representing the sal is lte option. </summary>
        SAL_IS_LTE = 733
        ''' <summary>   An enum constant representing the sal to Position option. </summary>
        SAL_TO_POS = 734
        ''' <summary>   An enum constant representing the sal to Negative option. </summary>
        SAL_TO_NEG = 735
        ''' <summary>   An enum constant representing the sal Increment option. </summary>
        SAL_INCR = 736
        ''' <summary>   An enum constant representing the sal Decrement option. </summary>
        SAL_DECR = 737
        ''' <summary>   An enum constant representing the sal expression option. </summary>
        _SAL_Expression = 750
        ''' <summary>   An enum constant representing the sal program title option. </summary>
        _Sal_Program_title = 760
        ''' <summary>   An enum constant representing the sal begin statement option. </summary>
        _Sal_BeginStatement = 770
        ''' <summary>   An enum constant representing the sal pop option. </summary>

        SAL_POP



#End Region
#Region "Keywords"
        ''' <summary>   An enum constant representing the break keyword option. </summary>
        BreakKeyword = 201
        ''' <summary>   An enum constant representing to keyword option. </summary>
        ToKeyword = 202
        ''' <summary>   An enum constant representing for keyword option. </summary>
        ForKeyword = 203
        ''' <summary>   An enum constant representing the true keyword option. </summary>
        TrueKeyword = 204
        ''' <summary>   An enum constant representing the false keyword option. </summary>
        FalseKeyword = 205
        ''' <summary>   An enum constant representing the let keyword option. </summary>
        LetKeyword = 206
        ''' <summary>   An enum constant representing the return keyword option. </summary>
        ReturnKeyword = 207
        ''' <summary>   An enum constant representing the Variable keyword option. </summary>
        VarKeyword = 208
        FunctionKeyword = 209
        ''' <summary>   An enum constant representing if keyword option. </summary>
        IfKeyword = 210
        ''' <summary>   An enum constant representing the continue keyword option. </summary>
        ContinueKeyword = 211
        ''' <summary>   An enum constant representing the else keyword option. </summary>
        ElseKeyword = 212
        ''' <summary>   An enum constant representing the while keyword option. </summary>
        WhileKeyword = 213
        ''' <summary>   An enum constant representing the do keyword option. </summary>
        DoKeyword = 214
        ''' <summary>   An enum constant representing the until keyword option. </summary>
        UntilKeyword = 215
        ''' <summary>   An enum constant representing the then keyword option. </summary>
        ThenKeyword = 216
        ''' <summary>   An enum constant representing the dim keyword option. </summary>
        DimKeyword = 217
        ''' <summary>   An enum constant representing the string type option. </summary>
        _StringType = 218
        ''' <summary>   An enum constant representing the array type option. </summary>
        _ArrayType = 219
        ''' <summary>   An enum constant representing the integer type option. </summary>
        _IntegerType = 220
        ''' <summary>   An enum constant representing the decimal type option. </summary>
        _DecimalType = 221
        ''' <summary>   An enum constant representing the boolean type option. </summary>
        _BooleanType = 222
        ''' <summary>   An enum constant representing the date type option. </summary>
        _DateType = 223
        ''' <summary>   An enum constant representing the null type option. </summary>
        _NullType = 224
        ''' <summary>   An enum constant representing as key word option. </summary>
        AsKeyWord = 225
        ''' <summary>   An enum constant representing the each key word option. </summary>
        EachKeyWord = 225
        ''' <summary>   An enum constant representing the next key word option. </summary>
        NextKeyWord
        ''' <summary>   An enum constant representing the loop key word option. </summary>
        LoopKeyWord
        ''' <summary>   An enum constant representing the in key word option. </summary>
        InKeyWord
        ''' <summary>   An enum constant representing the else if keyword option. </summary>
        ElseIfKeyword
        ''' <summary>   An enum constant representing the end if keyword option. </summary>
        EndIfKeyword
        ''' <summary>   An enum constant representing the or keyword option. </summary>
        OrKeyword
        ''' <summary>   An enum constant representing the and key word option. </summary>
        AndKeyWord
        ''' <summary>   An enum constant representing the not key word option. </summary>
        NotKeyWord
#End Region
#Region "BoundNodes"
        ''' <summary>   An enum constant representing the unbound literal option. </summary>
        UnboundLiteral
        ''' <summary>   An enum constant representing the bound literal option. </summary>
        BoundLiteral



#End Region
        ''' <summary>   An enum constant representing the sal program begin option. </summary>
        _SAL_PROGRAM_BEGIN = 771
        ''' <summary>   An enum constant representing the basic Language option. </summary>
        BASIC_LANG = 772
        ''' <summary>   An enum constant representing the unknown variable option. </summary>
        UnknownVariable = 773
        ''' <summary>   An enum constant representing if expression option. </summary>
        IfExpression = 774
        ''' <summary>   An enum constant representing the sal function expression option. </summary>
        _SAL_FunctionExpression = 775
        ''' <summary>   An enum constant representing the sal command option. </summary>
        SalCommand = 776

#End Region

    End Enum

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A grammar definintion. </summary>
    ''' Defines Grammar items requireing a RegEx pattern to detect
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Structure GrammarDefinintion
        ''' <summary>   The identifer. </summary>
        Public Identifer As SyntaxType
        ''' <summary>   A pattern specifying the search. </summary>
        Public SearchPattern As String

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets basic pattern list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The basic pattern list. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Shared Function GetBasicPatternList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion

            Spec.AddRange(GetMathOperatorList)
            Spec.AddRange(GetLiteralPatternsList)
            Spec.AddRange(GetBasicFunctionsPatternList)
            Spec.AddRange(GetBasicLANGS)
            Return Spec
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets basic langs. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The basic langs. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function GetBasicLANGS() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion

            Spec.AddRange(GetSALGrammar)
            Spec.AddRange(GetLOGO_Grammar)
            Spec.AddRange(GetBasicFunctionsPatternList)
            Return Spec
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets literal patterns list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The literal patterns list. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function GetLiteralPatternsList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion
            'DATE
            ' /(?<year>[0-9]{4})-(?<month>[0-9]{2})-(?<day>[0-9]{2})/

            'Date Literal
            'Day/Month/Year
            'Month/Day/Year
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Date
            NewGram.SearchPattern = "^(?[0-9]{2})-(?[0-9]{2})-(?[0-9]{4})"
            Spec.Add(NewGram)

            '_Identifier
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Identifier
            NewGram.SearchPattern = "^\b[a-z][a-z0-9]+\b"
            Spec.Add(NewGram)

            '_INTEGER
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Integer
            NewGram.SearchPattern = "^\d+"
            Spec.Add(NewGram)

            '_Decimal
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Decimal
            NewGram.SearchPattern = "^\d+\.?\d+"
            Spec.Add(NewGram)


            '_String
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._String
            NewGram.SearchPattern = "^" & Chr(34) & "[^" & Chr(34) & "]*" & Chr(34)
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._String
            NewGram.SearchPattern = "^'[^']*'"
            Spec.Add(NewGram)

            'logical(boolean) - Literal
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.TrueKeyword
            NewGram.SearchPattern = "^\btrue\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.FalseKeyword
            NewGram.SearchPattern = "^\bfalse\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._null
            NewGram.SearchPattern = "^\bnull\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LIST_SEPERATOR
            NewGram.SearchPattern = "^\,"
            Spec.Add(NewGram)
            'ARGS LIST : LEFT BOUNDRY
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LIST_BEGIN
            NewGram.SearchPattern = "^\["
            Spec.Add(NewGram)
            'ARGS LIST: RIGHT BOUNDRY
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LIST_END
            NewGram.SearchPattern = "^\]"
            Spec.Add(NewGram)
            Return Spec
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets mathematics operator list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The mathematics operator list. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Public Shared Function GetMathOperatorList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion

            'Maths Operators
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Add_Operator
            NewGram.SearchPattern = "^\+"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Sub_Operator
            NewGram.SearchPattern = "^\-"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Divide_Operator
            NewGram.SearchPattern = "^\/"
            Spec.Add(NewGram)
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Multiply_Operator
            NewGram.SearchPattern = "^\*"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_DIV
            NewGram.SearchPattern = "^\bDIV\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_ADD
            NewGram.SearchPattern = "^\bADD\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_MUL
            NewGram.SearchPattern = "^\bMUL\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_SUB
            NewGram.SearchPattern = "^\bSUB\b"
            Spec.Add(NewGram)
            ' -=,
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Minus_Equals_Operator
            NewGram.SearchPattern = "^(\-)="
            Spec.Add(NewGram)
            '*=, 
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Multiply_Equals_Operator
            NewGram.SearchPattern = "^(\*)="
            Spec.Add(NewGram)
            '+=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Add_Equals_Operator
            NewGram.SearchPattern = "^(\+)="
            Spec.Add(NewGram)
            '/=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.Divide_Equals_Operator
            NewGram.SearchPattern = "^(\/)="
            Spec.Add(NewGram)

            ''=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._ASSIGN
            NewGram.SearchPattern = "^\="
            Spec.Add(NewGram)

            'Logical Operators
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.AndKeyWord
            NewGram.SearchPattern = "^\bAND\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_OR
            NewGram.SearchPattern = "^\bOR\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.NotKeyWord
            NewGram.SearchPattern = "^\bNOT\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.AndKeyWord
            NewGram.SearchPattern = "^\band\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.OrKeyword
            NewGram.SearchPattern = "^\bor\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.NotKeyWord
            NewGram.SearchPattern = "^\bnot\b"
            Spec.Add(NewGram)

            'Equality operators: !=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.EquivelentTo
            NewGram.SearchPattern = "^(=\)=\="
            'Equality operators: ==
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.NotEqual
            NewGram.SearchPattern = "^(!)=\="

            'Operations (Groups)
            '_ParenthesizedExpresion
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._leftParenthes
            NewGram.SearchPattern = "^\("
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._RightParenthes
            NewGram.SearchPattern = "^\)"
            Spec.Add(NewGram)
            'BLOCK CODE: LEFT BOUNDRY
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._CODE_BEGIN
            NewGram.SearchPattern = "^\{"
            Spec.Add(NewGram)
            'BLOCK CODE: RIGHT BOUNDRY
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._CODE_END
            NewGram.SearchPattern = "^\}"

            Return Spec
        End Function

        ''' <summary>
        ''' 'https://www.tutorialspoint.com/logo/logo_introduction.htm
        '''      'https://www.transum.org/software/Logo/ (Online logo)
        '''      'https://www.calormen.com/jslogo/
        '''      'FORWARD	    fd	            FORWARD(space)(number Of steps To move forward)	Moves turtle forward For number Of times specified	"forward 100" Or "fd 100"
        '''      'BACK	        bk	            BACK(space) (number Of steps To move backward)	Moves turtle back For number Of times specified	"back 100" Or "bk 100"
        '''      'RIGHT	        rt	            RIGHT(space) (degrees To rotate toward right	Turns turtle right For number Of degrees specified	"right 228" Or "rt 228"
        '''      'LEFT	        lt	            LEFT(space) (degrees To rotate toward left )	Turns turtle left For number Of degrees specified	"left 228" Or "lt 228"
        '''      'HOME	        home	        Home	Comes To screen center but does Not clear the screen	"home"
        '''      'CLEAN	        ct cs	        Clean	Clears the screen Of trails but the turtle remains where it Is without moving	"clean"
        '''      'CLEARSCREEN	CS	            Clearscreen	Clears the screen Of trails And comes To screen center	"cs"
        '''      'HIDETURTLE	    HT	            Hide turtle	Hides the turtle And aids viewing a clear drawing On the screen	"ht"
        '''      'SHOWTURTLE	    ST	            Show turtle	Shows the turtle after it Is hidden from the screen	"st"
        '''      'PENUP	        PU(set)         Pen up	Sets the turtle To move without drawing	"pu"
        '''      'PENDOWN	    PD(resets)      Pen	Resets To a drawing pen When ordered To move	"pd"
        '''      'CLEARTEXT	    CT	Clear text	Clears all text In the command screen	"ct"
        '''      '
        '''      'signExpression
        '''      '   (('+' | '-'))* (number | deref | func)
        '''      '
        '''      'multiplyingExpression
        '''      '    : signExpression (('*' | '/') signExpression)*
        '''      '
        '''      'expression
        '''      '    : multiplyingExpression (('+' | '-') multiplyingExpression)*
        '''      '
        '''      'parameterDeclarations
        '''      '       : ':' name (',' parameterDeclarations)*
        '''      '
        '''      'procedureDeclaration
        '''      '       : 'to' name parameterDeclarations* EOL? (line? EOL) + 'end'
        '''      '
        '''      'procedureInvocation
        '''      '       : name expression*
        '''      '
        '''      'deref
        '''      '   ':' name
        '''      '
        '''      'fd
        '''      '   : ('fd' | 'forward') expression
        '''      '
        '''      'bk
        '''      '   : ('bk' | 'backward') expression
        '''      '
        '''      'rt
        '''      '   : ('rt' | 'right') expression
        '''      '
        '''      'lt 
        '''      '    : ('lt' | 'left') expression
        '''      '
        '''      'cs
        '''      '    : cs
        '''      '    : clearscreen
        '''      '
        '''      'pu
        '''      '    : pu
        '''      '    : penup
        '''      '
        '''      'pd
        '''      '
        '''      '    : pd
        '''      '    : pendown
        '''      '
        '''      'ht
        '''      '
        '''      '    : ht
        '''      '    : hideturtle
        '''      '
        '''      'st'
        '''      '
        '''      '    : st
        '''      '    : showturtle
        '''      '
        '''      '    : Home
        '''      '
        '''      '    : Stop
        '''      '
        '''      '    : label
        '''      '
        '''      'setxy
        '''      '    : setxy expression expression
        '''      '
        '''      'random
        '''      '
        '''      '    : random expression
        '''      '
        '''      'for
        '''      '    : 'for' '[' name expression expression expression ']' block
        '''      '
        '''      'value
        '''      ' String / Expression / deref
        '''      '
        '''      'name
        '''      '   String
        '''      '
        '''      'print
        '''      '    : 'print' (value | quotedstring)
        '''      '
        '''      'make
        '''      '    : 'make' STRINGLITERAL value
        '''      '
        '''      'comparison
        '''      ' : expression comparisonOperator expression
        '''      '
        '''      'comparisonOperator
        '''      '
        '''      'if
        '''      '       'if' comparison block
        '''      ' block
        '''      '       '[' cmd + ']'
        '''      '
        '''      'repeat
        '''      '       : 'repeat' number block
        '''      '
        '''      'func
        '''      '   : random
        '''      '
        '''      'line
        '''      '       : cmd + comment?
        '''      '       : comment
        '''      '       : print comment?
        '''      '       : procedureDeclaration
        '''      '
        '''      'prog
        '''      '       :(line? EOL) + line?
        '''      '
        '''      'comment
        '''      '       : COMMENT
        '''      '       :  ~ [\r\n]*';'
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetLOGO_Grammar() As List(Of GrammarDefinintion)
            Dim iSpec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_comments
            NewGram.SearchPattern = "^\~"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_LANG
            NewGram.SearchPattern = "\blogo_lang\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_EOL
            NewGram.SearchPattern = "^\;"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_deref
            NewGram.SearchPattern = "^\:"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_fd
            NewGram.SearchPattern = "\bfd\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_fd
            NewGram.SearchPattern = "\bforward\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_bk
            NewGram.SearchPattern = "\bbackward\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_bk
            NewGram.SearchPattern = "\bbk\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_rt
            NewGram.SearchPattern = "\brt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_rt
            NewGram.SearchPattern = "\bright\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_lt
            NewGram.SearchPattern = "\blt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_lt
            NewGram.SearchPattern = "\bleft\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_cs
            NewGram.SearchPattern = "\bcs\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_cs
            NewGram.SearchPattern = "\bclearscreen\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_pu
            NewGram.SearchPattern = "\bpu\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_pu
            NewGram.SearchPattern = "\bpenup\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_pd
            NewGram.SearchPattern = "\bpd\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_pd
            NewGram.SearchPattern = "\bpendown\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_ht
            NewGram.SearchPattern = "\bht\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_ht
            NewGram.SearchPattern = "\bhideturtle\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_st
            NewGram.SearchPattern = "\bst\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_st
            NewGram.SearchPattern = "\bshowturtle\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_label
            NewGram.SearchPattern = "\blabel\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_setxy
            NewGram.SearchPattern = "\bsetxy\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_make
            NewGram.SearchPattern = "\bmake\b"
            iSpec.Add(NewGram)


            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._String
            NewGram.SearchPattern = "^" & Chr(34) & "[^" & Chr(34) & "]*" & Chr(34)
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._String
            NewGram.SearchPattern = "^'[^']*'"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._WhitespaceToken
            NewGram.SearchPattern = "^\s"
            iSpec.Add(NewGram)

            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._null
            NewGram.SearchPattern = "^\bnull\b"
            iSpec.Add(NewGram)
            'Variable
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Integer
            NewGram.SearchPattern = "^\b[a-z][a-z0-9]+\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._Integer
            NewGram.SearchPattern = "^\d+"
            iSpec.Add(NewGram)








            Return iSpec
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets basic functions pattern list. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The basic functions pattern list. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function GetBasicFunctionsPatternList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion
#Region "IF/THEN"
            'IF/THEN
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.IfKeyword
            NewGram.SearchPattern = "^\bif\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.ElseKeyword
            NewGram.SearchPattern = "^\belse\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.ThenKeyword
            NewGram.SearchPattern = "^\bthen\b"
            Spec.Add(NewGram)


#End Region
#Region "DO WHILE/UNTIL"
            'DO WHILE/UNTIL
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.WhileKeyword
            NewGram.SearchPattern = "^\bwhile\b"
            Spec.Add(NewGram)


            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.UntilKeyword
            NewGram.SearchPattern = "^\buntil\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LoopKeyWord
            NewGram.SearchPattern = "^\bloop\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.DoKeyword
            NewGram.SearchPattern = "^\bdo\b"
            Spec.Add(NewGram)
#End Region
#Region "For/Next"

            'For/To  For/Each/in /Next
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.ForKeyword
            NewGram.SearchPattern = "^\bfor\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.EachKeyWord
            NewGram.SearchPattern = "^\beach\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.ToKeyword
            NewGram.SearchPattern = "^\bto\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.NextKeyWord
            NewGram.SearchPattern = "^\bnext\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.InKeyWord
            NewGram.SearchPattern = "^\bin\b"
            Spec.Add(NewGram)

#End Region
#Region "VariableDeclaration"
            'ASSIGNMENT : Syntax  _Variable _AS 
            'Reconsidered Using Dim (Could Still Implement by changing Assignment handler/Generator)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.DimKeyword
            NewGram.SearchPattern = "^\bdim\b\s"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LetKeyword
            NewGram.SearchPattern = "^\blet\b\s"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.VarKeyword
            NewGram.SearchPattern = "^\bvar\b\s"
            Spec.Add(NewGram)
            'Assignment operators: xLeft assigns output of right (9+4) (+= 9) (-=2) (3) (true)
#End Region
#Region "Print"
            'Prints
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._PRINT
            NewGram.SearchPattern = "^\bprint\b"
            Spec.Add(NewGram)
#End Region
#Region "Return Value"
            'Functions/Classes
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.ReturnKeyword
            NewGram.SearchPattern = "^\breturn\b"
            Spec.Add(NewGram)
#End Region
#Region "Declare Function"
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FUNCTION_DECLARE
            NewGram.SearchPattern = "\bdef\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FUNCTION_DECLARE
            NewGram.SearchPattern = "^\bfunction\b"
            Spec.Add(NewGram)
#End Region
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.BASIC_LANG
            NewGram.SearchPattern = "\bbasic_lang\b"
            Spec.Add(NewGram)
            Return Spec
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets sal grammar. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   The sal grammar. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function GetSALGrammar() As List(Of GrammarDefinintion)
            Dim iSpec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._SAL_PROGRAM_BEGIN
            NewGram.SearchPattern = "^\bsal_lang\b"
            iSpec.Add(NewGram)
#Region "SAL"
            'Sal_Cmds

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_REMOVE
            NewGram.SearchPattern = "^\bremove\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_RESUME
            NewGram.SearchPattern = "^\bresume\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PUSH
            NewGram.SearchPattern = "^\bpush\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_POP
            NewGram.SearchPattern = "^\bpop\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PULL
            NewGram.SearchPattern = "^\bpull\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PEEK
            NewGram.SearchPattern = "^\bpeek\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_WAIT
            NewGram.SearchPattern = "^\bwait\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PAUSE
            NewGram.SearchPattern = "^\bpause\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_HALT
            NewGram.SearchPattern = "^\bhalt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_DUP
            NewGram.SearchPattern = "^\bdup\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JMP
            NewGram.SearchPattern = "^\bjmp\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_T
            NewGram.SearchPattern = "^\bjif_t\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_F
            NewGram.SearchPattern = "^\bjif_f\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_EQ
            NewGram.SearchPattern = "^\bjif_eq\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_GT
            NewGram.SearchPattern = "^\bjif_gt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_LT
            NewGram.SearchPattern = "^\bjif_lt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_LOAD
            NewGram.SearchPattern = "^\bload\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_STORE
            NewGram.SearchPattern = "^\bstore\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_CALL
            NewGram.SearchPattern = "^\bcall\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_RET
            NewGram.SearchPattern = "^\bret\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PRINT_M
            NewGram.SearchPattern = "^\bprint_m\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_PRINT_C
            NewGram.SearchPattern = "^\bprint_c\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_ADD
            NewGram.SearchPattern = "^\badd\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_SUB
            NewGram.SearchPattern = "^\bsub\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_MUL
            NewGram.SearchPattern = "^\bmul\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_DIV
            NewGram.SearchPattern = "^\bdiv\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_ADD
            NewGram.SearchPattern = "^\band\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_OR
            NewGram.SearchPattern = "^\bor\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_NOT
            NewGram.SearchPattern = "^\bnot\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_JIF_LT
            NewGram.SearchPattern = "^\bis_eq\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_IS_GT
            NewGram.SearchPattern = "^\bis_gt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_IS_GTE
            NewGram.SearchPattern = "^\bis_gte\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_IS_LT
            NewGram.SearchPattern = "^\bis_lt\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_IS_LT
            NewGram.SearchPattern = "^\bis_lte\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_TO_POS
            NewGram.SearchPattern = "^\bto_pos\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_TO_NEG
            NewGram.SearchPattern = "^\bto_neg\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_INCR
            NewGram.SearchPattern = "^\bincr\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.SAL_DECR
            NewGram.SearchPattern = "^\bdecr\b"
            iSpec.Add(NewGram)



#End Region
            Return iSpec
        End Function

    End Structure
End Namespace


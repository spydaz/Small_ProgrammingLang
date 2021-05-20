Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Web.Script.Serialization

Namespace Syntax
    Public Enum DiagnosticType
        TokenizerDiagnostics
        ExpressionSyntaxDiagnostics
        EvaluationDiagnostics
    End Enum
    Public Enum ExceptionType
        GeneralError
        VariableDeclarationError
        MemoryAccessError
        UnknownLiteralError
        UnknownTokenError
        UnknownReturnType
        UnknownExpressionError
        TokenizerGeneralException
        ParserGeneralException
        EvaluationException
        InvalidLiteralType
        InvalidParameter
        UnabletoCompileError
        UnabletoParseError
        UnabletoTokenizeError
    End Enum
    Public Enum OperatorType
        Add
        Minus
        Divide
        Multiply
        AddEquals
        MinusEquals
        DivideEquals
        MultiplyEquals
        GreaterThan
        LessThan
        GreaterThanEquals
        LessThanEquals
        NotEquals
        Equivilent
        Equals
        LogicalOr
        LogicalAnd
        LogicalNot
        LogicalxOr
    End Enum
    Public Enum LangTypes
        SAL
        BASIC
        LOGO
        Unknown
    End Enum
    Public Enum LiteralType
        _Boolean
        _String
        _Array
        _Integer
        _Decimal
        _Date
        _NULL
    End Enum
    Public Enum BinaryOperation
        _Comparison
        _Multiplcative
        _Addative
        _Assignment
    End Enum
    Public Enum ExpressionType
        Literal
        BinaryExpression
        VariableDecleration
        Variable
    End Enum
    Public Enum Functions
        If_Then_Function
        If_Then_Else_Function
        For_Next_Function
        Do_While_Function
        Do_Until_Function
        Variable_Declare_Function
        Binary_Operation
    End Enum
    Public Module SyntaxFacts
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
                    Return SyntaxType.UntilKeword
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

#End Region

                Case Else
                    Return SyntaxType._Identifier
            End Select

        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function GetUnaryOperatorPrecedence(kind As SyntaxType) As Integer
            Select Case kind
                Case SyntaxType.Add_Operator, SyntaxType.Sub_Operator
                    ' SyntaxKind.BangToken,
                    '   SyntaxKind.TildeToken
                    Return 6

                Case Else
                    Return 0
            End Select
        End Function
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
        <Runtime.CompilerServices.Extension()>
        Public Function GetSyntaxTypeStr(ByRef _Syntaxtype As SyntaxType) As String
            Select Case _Syntaxtype
'literals
                Case SyntaxType._String
                    Return "_String"
                Case SyntaxType._Integer
                    Return "_Integer"
                Case SyntaxType._Identifier
                    Return "_Identifier"
                Case SyntaxType.IfKeyword
                    Return "IfKeyword"
'keywords
                Case SyntaxType.ForKeyword
                    Return "ForKeyword"
                Case SyntaxType.ToKeyword
                    Return "ToKeyword"
                Case SyntaxType.ReturnKeyword
                    Return "ReturnKeyword"
                Case SyntaxType.VarKeyword
                    Return "VarKeyword"
                Case SyntaxType.ContinueKeyword
                    Return "ContinueKeyword"
                Case SyntaxType.WhileKeyword
                    Return "WhileKeyword"
                Case SyntaxType.DoKeyword
                    Return "DoKeyword"
                Case SyntaxType.UntilKeword
                    Return "UntilKeword"
                Case SyntaxType.ThenKeyword
                    Return "ThenKeyword"
                Case SyntaxType.FunctionKeyword
                    Return "FunctionKeyword"
                Case SyntaxType.TrueKeyword
                    Return "TrueKeyword"
                Case SyntaxType.FalseKeyword
                    Return "FalseKeyword"
                Case SyntaxType.ElseKeyword
                    Return "ElseKeyword"
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
                Case SyntaxType._CodeBlock
                    Return "_CodeBlock"
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
            End Select

            Return "_UnknownToken"
        End Function
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
    ''' <summary>
    ''' Token to be returned contain as much information required,
    ''' for the Parser to make a decision on how to handle the token.
    ''' </summary>
    Public Structure SyntaxToken
        Public _SyntaxType As SyntaxType
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
        ''' <summary>
        ''' Formatted json
        ''' </summary>
        ''' <returns> </returns>
        Public Function ToJson() As String
            On Error Resume Next
            If Me._Value IsNot Nothing Then
                Return FormatJsonOutput(ToString)
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Inline json
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            On Error Resume Next
            Dim Converter As New JavaScriptSerializer
            If Me._Value IsNot Nothing Then


                Return Converter.Serialize(Me)

            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Formats the output of the json parsed
        ''' </summary>
        ''' <param name="jsonString"></param>
        ''' <returns></returns>
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
    ''' <summary>
    ''' Descriptive Element for describing Tokens and SyntaxNodes/Expressions
    ''' Important to Keep Everything Centralized, 
    ''' This role is often Doubled up creatingtwo Different Classes of Descriptive Tokens; 
    ''' Hence Combining the TokenTypes 
    ''' </summary>
    Public Enum SyntaxType
#Region "SYSTEM"
        'SystemTokens
        '
        _UnknownToken = 0
        _WhitespaceToken = 1
        _NewLineToken = 2
        _EndOfFileToken = 3
        _EndOfLineToken = 4
#End Region
#Region "Operartors"
        'Maths Term Operators
        '
        Add_Operator = 11
        Sub_Operator = 12
        'Maths Factor Operators
        '
        Multiply_Operator = 13
        Divide_Operator = 14
        'Comparision Operators
        '
        GreaterThan_Operator = 15
        LessThanOperator = 17
        GreaterThanEquals = 18
        LessThanEquals = 19
        'Assignment Operators
        '
        Add_Equals_Operator = 20
        Minus_Equals_Operator = 21
        Multiply_Equals_Operator = 22
        Divide_Equals_Operator = 23
        Equals = 24
        NotEqual = 25
        EquivelentTo = 27
        _Not = 25
        'Logical Operators
        '


#End Region

        'Symbols
        '
        _leftParenthes = 30
        _RightParenthes = 31
        _SIMPLE_ASSIGN = 32
        _CODE_BEGIN = 33
        _CODE_END = 34
        _LIST_SEPERATOR = 35
        _LIST_END = 36
        _LIST_BEGIN = 37
        _STATEMENT_END = 38

        'Literals
        '
        _null = 40
        _Integer = 41
        _arrayList = 42
        _Identifier = 43
        _String = 44
        _Boolean = 45
        _Decimal = 46
        _Date = 47


        _ASSIGN = 50
#Region "Universal Expressions"

        'ExpressionSyntax
        'Single numeric
        _NumericLiteralExpression = 100
        _IdentifierExpression = 101
        _StringExpression = 102
        _BooleanLiteralExpression = 103
        ' -3 +2
        _UnaryExpression = 105
        'Left +/-*><= _Right
        _BinaryExpression = 110
        'Left=Right
        _AssignmentExpression = 120
        '(Expr)
        _ParenthesizedExpresion = 130
        'List pf (Expr)
        _CodeBlock = 140
        _VariableDeclaration = 141
#End Region
#Region "MainLanguage"
#Region "Functions - Used In Universal RegexSearches"


        _PRINT = 90
        _FUNCTION_DECLARE = 95

#End Region

#Region "Logo"
        'LOGO LANG
        '
        LOGO_repeat = 501
        LOGO_fd = 502
        LOGO_bk = 503
        LOGO_rt = 504
        LOGO_lt = 505
        LOGO_cs = 506
        LOGO_pu = 507
        LOGO_pd = 508
        LOGO_ht = 509
        LOGO_st = 510
        LOGO_deref = 511
        LOGO_home = 512
        LOGO_label = 513
        LOGO_setxy = 514
        LOGO_make = 515
        LOGO_procedureInvocation = 516
        LOGO_procedureDeclaration = 517
        LOGO_parameterDeclarations = 518
        LOGO_comparison = 519
        LOGO_comparisonOperator = 520
        '   LOGO_ife = 521
        LOGO_Stop = 522
        ' LOGO_fore = 523
        LOGO_LANG = 524
        LOGO_EOL = 525
        ' LOGO_number = 526
        '  LOGO_name = 527
        LOGO_signExpression = 528
        LOGO_multiplyingExpression = 529
        LOGO_expression = 530
        LOGO_comments = 531
#End Region
#Region "SAL"
        '        'SAL_ASSEMBLY LANG
        '        'Token_IDs
        SAL_NULL = 700
        SAL_REMOVE = 701
        SAL_RESUME = 702
        SAL_PUSH = 703
        SAL_PULL = 704
        SAL_PEEK = 705
        SAL_WAIT = 706
        SAL_PAUSE = 707
        SAL_HALT = 708
        SAL_DUP = 709
        SAL_JMP = 710
        SAL_JIF_T = 711
        SAL_JIF_F = 712
        SAL_JIF_EQ = 713
        SAL_JIF_GT = 714
        SAL_JIF_LT = 715
        SAL_LOAD = 716
        SAL_STORE = 717
        SAL_CALL = 718
        SAL_RET = 719
        SAL_PRINT_M = 720
        SAL_PRINT_C = 721
        SAL_ADD = 722
        SAL_SUB = 723
        SAL_MUL = 724
        SAL_DIV = 725
        SAL_AND = 726
        SAL_OR = 727
        SAL_NOT = 728
        SAL_IS_EQ = 729
        SAL_IS_GT = 730
        SAL_IS_GTE = 731
        SAL_IS_LT = 732
        SAL_IS_LTE = 733
        SAL_TO_POS = 734
        SAL_TO_NEG = 735
        SAL_INCR = 736
        SAL_DECR = 737
        _SAL_Expression = 750
        _Sal_Program_title = 760
        _Sal_BeginStatement = 770

        SAL_POP



#End Region
#Region "Keywords"
        BreakKeyword = 201
        ToKeyword = 202
        ForKeyword = 203
        TrueKeyword = 204
        FalseKeyword = 205
        LetKeyword = 206
        ReturnKeyword = 207
        VarKeyword = 208
        FunctionKeyword = 209
        IfKeyword = 210
        ContinueKeyword = 211
        ElseKeyword = 212
        WhileKeyword = 213
        DoKeyword = 214
        UntilKeword = 215
        ThenKeyword = 216
        DimKeyword = 217
        _StringType = 218
        _ArrayType = 219
        _IntegerType = 220
        _DecimalType = 221
        _BooleanType = 222
        _DateType = 223
        _NullType = 224
        AsKeyWord = 225
        EachKeyWord = 225
        NextKeyWord
        LoopKeyWord
        InKeyWord
        ElseIfKeyword
        EndIfKeyword
        OrKeyword
        AndKeyWord
        NotKeyWord
#End Region
#Region "BoundNodes"
        UnboundLiteral
        BoundLiteral



#End Region
        _SAL_PROGRAM_BEGIN = 771
        BASIC_LANG = 772

#End Region

    End Enum
    ''' <summary>
    ''' Defines Grammar items requireing a RegEx pattern to detect
    ''' </summary>
    Public Structure GrammarDefinintion
        Public Identifer As SyntaxType
        Public SearchPattern As String

        ''' <summary>
        ''' Known Search Pattrns
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetBasicPatternList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion

            Spec.AddRange(GetMathOperatorList)
            Spec.AddRange(GetLiteralPatternsList)
            Spec.AddRange(GetBasicFunctionsPatternList)
            Spec.AddRange(GetBasicLANGS)
            Return Spec
        End Function
        Public Shared Function GetBasicLANGS() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion

            Spec.AddRange(GetSALGrammar)
            Spec.AddRange(GetLOGO_Grammar)
            Spec.AddRange(GetBasicFunctionsPatternList)
            Return Spec
        End Function
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
        ''' <summary>
        ''' Known Search Patterns For maths Operations
        ''' </summary>
        ''' <returns></returns>
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
            NewGram.Identifer = SyntaxType.UntilKeword
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


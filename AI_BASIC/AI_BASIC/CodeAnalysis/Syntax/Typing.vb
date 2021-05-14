Imports System.Text
Imports System.Web.Script.Serialization

Namespace Syntax
    Public Enum LangTypes
        SAL
        BASIC
        LOGO
        Unknown
    End Enum
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
            Return FormatJsonOutput(ToString)
        End Function
        ''' <summary>
        ''' Inline json
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Dim Converter As New JavaScriptSerializer
            Return Converter.Serialize(Me)
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

        Public Sub New(ByRef _sType As SyntaxType, syntaxTypeStr As String, raw As String, value As Object, start As Integer, _end As Integer)
            If syntaxTypeStr Is Nothing Then
                Throw New ArgumentNullException(NameOf(syntaxTypeStr))
            End If
            If raw Is Nothing Then
                Throw New ArgumentNullException(NameOf(raw))
            End If
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            'Intialize Token
            _SyntaxType = _sType
            _SyntaxTypeStr = syntaxTypeStr
            _Raw = raw
            _Value = value
            _start = start
            _end = _end
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
        AddEquals = 20
        MinusEqual = 21
        MultiplyEquals = 22
        DivideEquals = 23
        Equals = 24
        NotEqual = 25
        'Logical Operators
        '
        _AND = 26
        _OR = 27
        _NOT = 28
#End Region


        'Symbols
        '
        _leftParenthes = 30
        _RightParenthes = 31

        'Literals
        '
        _null = 40
        _Integer = 41
        _arrayList = 42
        _Identifier = 43
        _String = 44
        _Boolean = 45

#Region "Universal Expressions"

        'ExpressionSyntax
        'Single numeric
        _NumericLiteralExpression = 100
        'Left +/-*><= _Right
        _BinaryExpression = 110
        'Left=Right
        _AssignmentExpression = 120
        '(Expr)
        _ParenthesizedExpresion = 130
        'List pf (Expr)
        _CodeBlock = 140
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
        LOGO_ife = 521
        LOGO_Stop = 522
        LOGO_fore = 523
        LOGO_LANG = 524
        LOGO_EOL = 525
        LOGO_number = 526
        LOGO_name = 527
        LOGO_signExpression = 528
        LOGO_multiplyingExpression = 529
        LOGO_expression = 530
#End Region
#Region "SAL"
        'SAL_ASSEMBLY LANG
        'Token_IDs
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



            'Logical Operators





            'Operations
            '_ParenthesizedExpresion
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._leftParenthes
            NewGram.SearchPattern = "^\("
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._RightParenthes
            NewGram.SearchPattern = "^\)"
            Spec.Add(NewGram)






            Return Spec
        End Function
    End Structure

End Namespace


﻿Imports System.Text
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
        Add_Equals_Operator = 20
        Minus_Equals_Operator = 21
        Multiply_Equals_Operator = 22
        Divide_Equals_Operator = 23
        Equals = 24
        NotEqual = 25
        NotEquivelentTo = 26
        EquivelentTo = 27

        'Logical Operators
        '
        _LOGICAL_AND = 26
        _LOGICAL_OR = 27
        _LOGICAL_NOT = 28
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
        _TRUE = 48
        _FALSE = 49

        _ASSIGN = 50
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
        _VariableDeclaration
#End Region
#Region "MainLanguage"
#Region "Functions"
        _IF = 60
        _ELSE = 61
        _THEN = 62
        _DO = 65
        _WHILE = 70
        _UNTIL = 71
        _LOOP = 72
        _FOR = 80
        _EACH = 81
        _TO = 82
        _NEXT = 83
        _IN = 84
        _PRINT = 90
        _FUNCTION_DECLARE = 95
        _RETURN = 96
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
        _SAL_PROGRAM_BEGIN = 771
        BASIC_LANG = 772


#End Region
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
            NewGram.Identifer = SyntaxType._TRUE
            NewGram.SearchPattern = "^\btrue\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FALSE
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
            NewGram.Identifer = SyntaxType._LOGICAL_AND
            NewGram.SearchPattern = "^\band\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LOGICAL_OR
            NewGram.SearchPattern = "^\bor\b"
            Spec.Add(NewGram)

            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LOGICAL_NOT
            NewGram.SearchPattern = "^\bnot\b"
            Spec.Add(NewGram)

            'Equality operators: !=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.NotEquivelentTo
            NewGram.SearchPattern = "^(=\)=\="
            'Equality operators: ==
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.EquivelentTo
            NewGram.SearchPattern = "^(=|!)=\="

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
            NewGram.Identifer = SyntaxType.LOGO_ife
            NewGram.SearchPattern = "\bif\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_fore
            NewGram.SearchPattern = "\bfor\b"
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
            'logical(boolean) - Literal
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._TRUE
            NewGram.SearchPattern = "^\btrue\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FALSE
            NewGram.SearchPattern = "^\bfalse\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._null
            NewGram.SearchPattern = "^\bnull\b"
            iSpec.Add(NewGram)
            'Variable
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_name
            NewGram.SearchPattern = "^\b[a-z][a-z0-9]+\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_number
            NewGram.SearchPattern = "^\d+"
            iSpec.Add(NewGram)


            '*=, /=, +=, -=,
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_signExpression
            NewGram.SearchPattern = "^(\*|\/|\+|\-)="
            iSpec.Add(NewGram)

            'Equality operators: ==, !=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_comparisonOperator
            NewGram.SearchPattern = "^(=|!)=\="
            iSpec.Add(NewGram)
            'Relational operators: >, >=, <, <=
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_comparisonOperator
            NewGram.SearchPattern = "^[><]\=?"
            iSpec.Add(NewGram)
            'Math operators: +, -, *, /
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_signExpression
            NewGram.SearchPattern = "^[+\-]"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType.LOGO_multiplyingExpression
            NewGram.SearchPattern = "^[*/]"
            iSpec.Add(NewGram)





            Return iSpec
        End Function
        Public Shared Function GetBasicFunctionsPatternList() As List(Of GrammarDefinintion)
            Dim Spec As New List(Of GrammarDefinintion)
            Dim NewGram As New GrammarDefinintion
#Region "IF/THEN"
            'IF/THEN
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._IF
            NewGram.SearchPattern = "^\bif\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._ELSE
            NewGram.SearchPattern = "^\belse\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._THEN
            NewGram.SearchPattern = "^\bthen\b"
            Spec.Add(NewGram)


#End Region
#Region "DO WHILE/UNTIL"
            'DO WHILE/UNTIL
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._WHILE
            NewGram.SearchPattern = "^\bwhile\b"
            Spec.Add(NewGram)


            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._UNTIL
            NewGram.SearchPattern = "^\buntil\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._LOOP
            NewGram.SearchPattern = "^\bloop\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._DO
            NewGram.SearchPattern = "^\bdo\b"
            Spec.Add(NewGram)
#End Region
#Region "For/Next"

            'For/To  For/Each/in /Next
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FOR
            NewGram.SearchPattern = "^\bfor\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._EACH
            NewGram.SearchPattern = "^\beach\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._TO
            NewGram.SearchPattern = "^\bto\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._NEXT
            NewGram.SearchPattern = "^\bnext\b"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._IN
            NewGram.SearchPattern = "^\bin\b"
            Spec.Add(NewGram)

#End Region
#Region "VariableDeclaration"
            'ASSIGNMENT : Syntax  _Variable _AS 
            'Reconsidered Using Dim (Could Still Implement by changing Assignment handler/Generator)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._VariableDeclaration
            NewGram.SearchPattern = "^\bdim\b\s"
            Spec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._VariableDeclaration
            NewGram.SearchPattern = "^\blet\b\s"
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
            NewGram.Identifer = SyntaxType._RETURN
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
            'logical(boolean) - Literal
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._TRUE
            NewGram.SearchPattern = "^\btrue\b"
            iSpec.Add(NewGram)
            NewGram = New GrammarDefinintion
            NewGram.Identifer = SyntaxType._FALSE
            NewGram.SearchPattern = "^\bfalse\b"
            iSpec.Add(NewGram)


#End Region
            Return iSpec
        End Function

    End Structure
End Namespace


Imports System.Text.Json

Namespace SmallProgLang
    Namespace GrammarFactory


        ''' <summary>
        ''' Simple Gramar object (Expected token Shape or from)
        ''' </summary>
        Public Class Grammar
            ''' <summary>
            ''' GRAMMAR OBJECT ID
            ''' </summary>
            Public Enum Type_Id
                ''' <summary>
                ''' Literal
                ''' </summary>
                _INTEGER
                ''' <summary>
                ''' Literal
                ''' </summary>
                _STRING
                ''' <summary>
                ''' Print Literal/Value/String
                ''' </summary>
                _PRINT
                ''' <summary>
                ''' Declare Var
                ''' </summary>
                _DIM
                ''' <summary>
                ''' Begin Iteration of list
                ''' For (Iterator = (Increment to Completion)
                ''' </summary>
                _FOR
                ''' <summary>
                ''' Additional = Step +1
                ''' </summary>
                _EACH
                ''' <summary>
                ''' From item in list
                ''' </summary>
                _IN
                ''' <summary>
                ''' End of iteration marker
                ''' </summary>
                _TO
                ''' <summary>
                ''' Increment Iterator
                ''' </summary>
                _NEXT
                ''' <summary>
                ''' If Condition = Outcome Then (code) Else (code)
                ''' </summary>
                _IF
                ''' <summary>
                ''' Then (block)
                ''' </summary>
                _THEN
                ''' <summary>
                ''' Else (Block)
                ''' </summary>
                _ELSE
                ''' <summary>
                ''' Until Condition = true
                ''' </summary>
                _UNTIL
                ''' <summary>
                ''' While Condition = true
                ''' </summary>
                _WHILE
                ''' <summary>
                ''' Signify begining of Do...While/Until
                ''' </summary>
                _DO
                _RETURN
                _FUNCTION
                _SUB
                _CLASS
                _NEW
                ''' <summary>
                ''' Used in Declaration Assignment
                ''' Left (var) assign as (LiteralType)
                ''' </summary>
                _AS
                ''' <summary>
                ''' End of While loop (marker)(check expression Condition)
                ''' </summary>
                _LOOP
                ''' <summary>
                ''' xLeft = output of right (9+4) (+= 9) (-=2) (3) (true)
                ''' </summary>
                _SIMPLE_ASSIGN
                ''' <summary>
                ''' xLeft assigns output of right -=(9+4) (+= 9) (-=2) (3) (true)
                ''' Complex assign ... x=x+(ouput)x=x-(ouput) etc
                ''' </summary>
                _COMPLEX_ASSIGN
                ''' <summary>
                ''' Boolean Literal Env Variable
                ''' </summary>
                _TRUE
                ''' <summary>
                ''' Boolean Literal - Env Variable
                ''' </summary>
                _FALSE
                ''' <summary>
                ''' Boolean literal -Env Variable
                ''' </summary>
                _NULL
                ''' <summary>
                ''' Used for Args List (Lists) = Arrays
                ''' Args are lists of Vars (function Environment Vars)
                ''' </summary>
                _LIST_BEGIN
                ''' <summary>
                ''' End of List
                ''' </summary>
                _LIST_END
                ''' <summary>
                ''' Used for Blocks of code
                ''' </summary>
                _CODE_BEGIN
                ''' <summary>
                ''' End of Code block
                ''' </summary>
                _CODE_END
                ''' <summary>
                ''' Used for operation blocks as well as 
                ''' ordering prioritizing evals
                ''' Begin
                ''' </summary>
                _CONDITIONAL_BEGIN
                ''' <summary>
                ''' End of Condition
                ''' </summary>
                _CONDITIONAL_END
                ''' <summary>
                '''  - AND
                ''' </summary>
                _LOGICAL_AND
                ''' <summary>
                '''  | OR
                ''' </summary>
                _LOGICAL_OR
                ''' <summary>
                ''' ! NOT
                ''' </summary>
                _LOGICAL_NOT
                ''' <summary>
                ''' Greater than / Less Than
                ''' </summary>
                _RELATIONAL_OPERATOR
                ''' <summary>
                ''' +-
                ''' </summary>
                _ADDITIVE_OPERATOR
                ''' <summary>
                ''' */
                ''' </summary>
                _MULTIPLICATIVE_OPERATOR
                ''' <summary>
                ''' ;
                ''' </summary>
                _STATEMENT_END
                ''' <summary>
                ''' end of file
                ''' </summary>
                _EOF
                ''' <summary>
                ''' Bad / Unrecognized token
                ''' </summary>
                _BAD_TOKEN
                ''' <summary>
                ''' Seperates items in list
                ''' </summary>
                _LIST_SEPERATOR
                ''' <summary>
                ''' !=
                ''' </summary>
                _NOT_EQUALS
                ''' <summary>
                ''' DECLARE VAR 
                ''' </summary>
                _VARIABLE_DECLARE
                'Sal token_IDs
                SAL_NULL
                SAL_REMOVE
                SAL_RESUME
                SAL_PUSH
                SAL_PULL
                SAL_PEEK
                SAL_WAIT
                SAL_PAUSE
                SAL_HALT
                SAL_DUP
                SAL_JMP
                SAL_JIF_T
                SAL_JIF_F
                SAL_JIF_EQ
                SAL_JIF_GT
                SAL_JIF_LT
                SAL_LOAD
                SAL_STORE
                SAL_CALL
                SAL_RET
                SAL_PRINT_M
                SAL_PRINT_C
                SAL_ADD
                SAL_SUB
                SAL_MUL
                SAL_DIV
                SAL_AND
                SAL_OR
                SAL_NOT
                SAL_IS_EQ
                SAL_IS_GT
                SAL_IS_GTE
                SAL_IS_LT
                SAL_IS_LTE
                SAL_TO_POS
                SAL_TO_NEG
                SAL_INCR
                SAL_DECR
                _SAL_PROGRAM_BEGIN
                _SAL_EXPRESSION_BEGIN
                _PL_PROGRAM_BEGIN
                _Def
                _EQUALITY
                _JSON_digit
                _JSON_esc
                _JSON_int
                _JSON_exp
                _JSON_frac
                _FUNCTION_DECLARE
                _DOT
                _OBJ_string
                _OBJ_integer
                _OBJ_boolean
                _OBJ_array
                _OBJ_null
                ''' <summary>
                ''' Literal
                ''' </summary>
                _VARIABLE
                _WHITESPACE
                _COMMENTS
            End Enum
            ''' <summary>
            ''' Identifier
            ''' </summary>
            Public ID As Type_Id
            ''' <summary>
            ''' RegEx Expression to search
            ''' </summary>
            Public Exp As String
            ''' <summary>
            ''' Set OF KeyWords for Language with RegEx Search Expressions
            ''' Based on basic programming languge keywords and symbols /Literals
            ''' This is a preloaded Grammar (list of Grammar objects)
            ''' </summary>
            ''' <returns></returns>
            Public Shared Function GetPLGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar

                NewGram = New Grammar
                NewGram.ID = Type_Id._PL_PROGRAM_BEGIN
                NewGram.Exp = "\bspl_lang\b"
                iSpec.Add(NewGram)

#Region "Print"
                'Prints
                NewGram = New Grammar
                NewGram.ID = Type_Id._PRINT
                NewGram.Exp = "^\bprint\b"
                iSpec.Add(NewGram)
#End Region
#Region "Functions/Classes"
#Region "Return Value"
                'Functions/Classes
                NewGram = New Grammar
                NewGram.ID = Type_Id._RETURN
                NewGram.Exp = "^\breturn\b"
                iSpec.Add(NewGram)
#End Region

#Region "Declare Function"
                NewGram = New Grammar
                NewGram.ID = Type_Id._FUNCTION_DECLARE
                NewGram.Exp = "\bdef\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FUNCTION_DECLARE
                NewGram.Exp = "^\bfunction\b"
                iSpec.Add(NewGram)
#End Region



                NewGram = New Grammar
                NewGram.ID = Type_Id._CLASS
                NewGram.Exp = "^\bclass\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NEW
                NewGram.Exp = "^\bnew\b"
                iSpec.Add(NewGram)
#End Region
#Region "ASSIGNMENT"
                'ASSIGNMENT : Syntax  _Variable _AS 
                'Reconsidered Using Dim (Could Still Implement by changing Assignment handler/Generator)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\bdim\b\s"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\blet\s\b"
                iSpec.Add(NewGram)
                'Assignment operators: xLeft assigns output of right (9+4) (+= 9) (-=2) (3) (true)
#End Region
#Region "IF/THEN"
                'IF/THEN
                NewGram = New Grammar
                NewGram.ID = Type_Id._IF
                NewGram.Exp = "^\bif\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._ELSE
                NewGram.Exp = "^\belse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._THEN
                NewGram.Exp = "^\bthen\b"
                iSpec.Add(NewGram)


#End Region
#Region "DO WHILE/UNTIL"
                'DO WHILE/UNTIL
                NewGram = New Grammar
                NewGram.ID = Type_Id._WHILE
                NewGram.Exp = "^\bwhile\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE_DECLARE
                NewGram.Exp = "^\bas\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._UNTIL
                NewGram.Exp = "^\buntil\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._LOOP
                NewGram.Exp = "^\bloop\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._DO
                NewGram.Exp = "^\bdo\b"
                iSpec.Add(NewGram)
#End Region
#Region "For/Next"

                'For/To  For/Each/in /Next
                NewGram = New Grammar
                NewGram.ID = Type_Id._FOR
                NewGram.Exp = "^\bfor\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._EACH
                NewGram.Exp = "^\beach\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._TO
                NewGram.Exp = "^\bto\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NEXT
                NewGram.Exp = "^\bnext\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._IN
                NewGram.Exp = "^\bin\b"
                iSpec.Add(NewGram)

#End Region




                iSpec.AddRange(GetLogicGrammar)
                iSpec.AddRange(GetSymbolsGrammar)
                iSpec.AddRange(GetLiteralsGrammar)
                'ARGS LIST: RIGHT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._EOF
                NewGram.Exp = "EOF"
                iSpec.Add(NewGram)

                Return iSpec
            End Function
            Public Shared Function GetSymbolsGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar
#Region "Seperators"
                'BLOCK CODE: LEFT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._CODE_BEGIN
                NewGram.Exp = "^\{"
                iSpec.Add(NewGram)
                'BLOCK CODE: RIGHT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._CODE_END
                NewGram.Exp = "^\}"
                iSpec.Add(NewGram)
                'END STATEMENT or EMPTY STATEMENT
                'EMPTY CODE BLOCKS CONTAIN (1 EMPTY STATEMENT)
                NewGram = New Grammar
                NewGram.ID = Type_Id._STATEMENT_END
                NewGram.Exp = "^\;"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._LIST_SEPERATOR
                NewGram.Exp = "^\,"
                iSpec.Add(NewGram)
                'ARGS LIST : LEFT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._LIST_BEGIN
                NewGram.Exp = "^\["
                iSpec.Add(NewGram)
                'ARGS LIST: RIGHT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._LIST_END
                NewGram.Exp = "^\]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._DOT
                NewGram.Exp = "^\\."
                iSpec.Add(NewGram)

#End Region
                Return iSpec
            End Function
            Public Shared Function GetLogicGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar


                'logical(boolean) - Literal
                NewGram = New Grammar
                NewGram.ID = Type_Id._TRUE
                NewGram.Exp = "^\btrue\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FALSE
                NewGram.Exp = "^\bfalse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)

                ''=
                NewGram = New Grammar
                NewGram.ID = Type_Id._SIMPLE_ASSIGN
                NewGram.Exp = "^\="
                iSpec.Add(NewGram)

                '*=, /=, +=, -=,
                NewGram = New Grammar
                NewGram.ID = Type_Id._COMPLEX_ASSIGN
                NewGram.Exp = "^(\*|\/|\+|\-)="
                iSpec.Add(NewGram)


                'Conditional BLOCK CODE: LEFT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._CONDITIONAL_BEGIN
                NewGram.Exp = "^\("
                iSpec.Add(NewGram)
                'Conditional BLOCK CODE: RIGHT BOUNDRY
                NewGram = New Grammar
                NewGram.ID = Type_Id._CONDITIONAL_END
                NewGram.Exp = "^\)"
                iSpec.Add(NewGram)

                'Logical Operators:  &&, ||
                NewGram = New Grammar
                NewGram.ID = Type_Id._LOGICAL_AND
                NewGram.Exp = "^\band\b"
                iSpec.Add(NewGram)

                NewGram = New Grammar
                NewGram.ID = Type_Id._LOGICAL_OR
                NewGram.Exp = "^\bor\b"
                iSpec.Add(NewGram)

                NewGram = New Grammar
                NewGram.ID = Type_Id._LOGICAL_NOT
                NewGram.Exp = "^\bnot\b"
                iSpec.Add(NewGram)

                'Equality operators: ==, !=
                NewGram = New Grammar
                NewGram.ID = Type_Id._EQUALITY
                NewGram.Exp = "^(=|!)=\="

                iSpec.Add(NewGram)

                'Relational operators: >, >=, <, <=
                NewGram = New Grammar
                NewGram.ID = Type_Id._RELATIONAL_OPERATOR
                NewGram.Exp = "^[><]\=?"
                iSpec.Add(NewGram)
                'Math operators: +, -, *, /
                NewGram = New Grammar
                NewGram.ID = Type_Id._ADDITIVE_OPERATOR
                NewGram.Exp = "^[+\-]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._MULTIPLICATIVE_OPERATOR
                NewGram.Exp = "^[*/]"
                iSpec.Add(NewGram)

                Return iSpec
            End Function
            Public Shared Function GetLiteralsGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)

                Dim NewGram As New Grammar
                'Literals
                NewGram.ID = Type_Id._INTEGER
                NewGram.Exp = "^\d+"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._STRING
                NewGram.Exp = "^" & Chr(34) & "[^" & Chr(34) & "]*" & Chr(34)
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._STRING
                NewGram.Exp = "^'[^']*'"
                iSpec.Add(NewGram)

                NewGram = New Grammar
                NewGram.ID = Type_Id._WHITESPACE
                NewGram.Exp = "^\s"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._COMMENTS
                NewGram.Exp = "^\/\/.*"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._COMMENTS
                NewGram.Exp = "^\/\*[\s\S]*?\*\/"
                iSpec.Add(NewGram)

                'Variable
                NewGram = New Grammar
                NewGram.ID = Type_Id._VARIABLE
                NewGram.Exp = "^\b[a-z][a-z0-9]+\b"
                iSpec.Add(NewGram)
#Region "literal Object types"
#Region "ARRAY"
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_array
                NewGram.Exp = "\blist\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_array
                NewGram.Exp = "\barray\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_array
                NewGram.Exp = "\barraylist\b"
                iSpec.Add(NewGram)
#End Region
#Region "boolean"
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_boolean
                NewGram.Exp = "\bbool\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_boolean
                NewGram.Exp = "\bboolean\b"
                iSpec.Add(NewGram)
#End Region
#Region "NULL"
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_null
                NewGram.Exp = "\bnothing\b"
                iSpec.Add(NewGram)
#End Region
#Region "integer"
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_integer
                NewGram.Exp = "\bint\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_integer
                NewGram.Exp = "\binteger\b"
                iSpec.Add(NewGram)
#End Region
#Region "string"
                NewGram = New Grammar
                NewGram.ID = Type_Id._OBJ_string
                NewGram.Exp = "\bstring\b"
                iSpec.Add(NewGram)

#End Region
#End Region
                Return iSpec
            End Function
            Public Shared Function GetSALGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar
                NewGram = New Grammar
                NewGram.ID = Type_Id._SAL_PROGRAM_BEGIN
                NewGram.Exp = "^\bsal_lang\b"
                iSpec.Add(NewGram)

                NewGram = New Grammar
                NewGram.ID = Type_Id._SAL_EXPRESSION_BEGIN
                NewGram.Exp = "^\bsal\b"
                iSpec.Add(NewGram)


                NewGram = New Grammar
                NewGram.ID = Type_Id._WHITESPACE
                NewGram.Exp = "^\s"
                iSpec.Add(NewGram)

                NewGram.ID = Type_Id._SIMPLE_ASSIGN
                NewGram.Exp = "^\bassigns\b"
                iSpec.Add(NewGram)


#Region "SAL"
                'Sal_Cmds
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_REMOVE
                NewGram.Exp = "^\bremove\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_RESUME
                NewGram.Exp = "^\bresume\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PUSH
                NewGram.Exp = "^\bpush\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PULL
                NewGram.Exp = "^\bpull\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PEEK
                NewGram.Exp = "^\bpeek\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_WAIT
                NewGram.Exp = "^\bwait\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PAUSE
                NewGram.Exp = "^\bpause\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_HALT
                NewGram.Exp = "^\bhalt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DUP
                NewGram.Exp = "^\bdup\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JMP
                NewGram.Exp = "^\bjmp\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_T
                NewGram.Exp = "^\bjif_t\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_F
                NewGram.Exp = "^\bjif_f\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_EQ
                NewGram.Exp = "^\bjif_eq\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_GT
                NewGram.Exp = "^\bjif_gt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_LT
                NewGram.Exp = "^\bjif_lt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_LOAD
                NewGram.Exp = "^\bload\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_STORE
                NewGram.Exp = "^\bstore\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_CALL
                NewGram.Exp = "^\bcall\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_RET
                NewGram.Exp = "^\bret\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PRINT_M
                NewGram.Exp = "^\bprint_m\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_PRINT_C
                NewGram.Exp = "^\bprint_c\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_ADD
                NewGram.Exp = "^\badd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_SUB
                NewGram.Exp = "^\bsub\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_MUL
                NewGram.Exp = "^\bmul\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DIV
                NewGram.Exp = "^\bdiv\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_ADD
                NewGram.Exp = "^\band\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_OR
                NewGram.Exp = "^\bor\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_NOT
                NewGram.Exp = "^\bnot\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_JIF_LT
                NewGram.Exp = "^\bis_eq\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_GT
                NewGram.Exp = "^\bis_gt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_GTE
                NewGram.Exp = "^\bis_gte\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_LT
                NewGram.Exp = "^\bis_lt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_IS_LT
                NewGram.Exp = "^\bis_lte\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_TO_POS
                NewGram.Exp = "^\bto_pos\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_TO_NEG
                NewGram.Exp = "^\bto_neg\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_INCR
                NewGram.Exp = "^\bincr\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.SAL_DECR
                NewGram.Exp = "^\bdecr\b"
                iSpec.Add(NewGram)
                'logical(boolean) - Literal
                NewGram = New Grammar
                NewGram.ID = Type_Id._TRUE
                NewGram.Exp = "^\btrue\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._FALSE
                NewGram.Exp = "^\bfalse\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._NULL
                NewGram.Exp = "^\bnull\b"
                iSpec.Add(NewGram)

#End Region

                iSpec.AddRange(GetLogicGrammar)
                iSpec.AddRange(GetSymbolsGrammar)
                iSpec.AddRange(GetLiteralsGrammar)

                NewGram = New Grammar
                NewGram.ID = Type_Id._EOF
                NewGram.Exp = "EOF"
                iSpec.Add(NewGram)
                Return iSpec
            End Function
            Public Shared Function GetExtendedGrammar() As List(Of Grammar)
                Dim lst As New List(Of Grammar)
                lst.AddRange(GetSALGrammar)
                lst.AddRange(GetPLGrammar)
                lst.AddRange(GetLogicGrammar)
                lst.AddRange(GetSymbolsGrammar)
                lst.AddRange(GetLiteralsGrammar)
                Return lst
            End Function
            ''' <summary>
            ''' Still Developing grammar:
            ''' </summary>
            ''' <returns></returns>
            Public Shared Function GetJsonGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar

                '"tokens" "STRING NUMBER { } [ ] , : TRUE FALSE NULL",
                '"start": "JSONText",

                ''"JSONString" [[ "STRING", "$$ = yytext;" ]],

                ''"JSONNumber" [[ "NUMBER", "$$ = Number(yytext);" ]],

                ''"JSONNullLiteral" [[ "NULL", "$$ = null;" ]],

                ''"JSONBooleanLiteral" [[ "TRUE", "$$ = true;" ],
                ''                       [ "FALSE", "$$ = false;" ]],


                ''"JSONText" [[ "JSONValue", "return $$ = $1;" ]],

                ''"JSONValue" [[ "JSONNullLiteral",    "$$ = $1;" ],
                ''              [ "JSONBooleanLiteral", "$$ = $1;" ],
                ''              [ "JSONString",         "$$ = $1;" ],
                ''              [ "JSONNumber",         "$$ = $1;" ],
                ''              [ "JSONObject",         "$$ = $1;" ],
                ''              [ "JSONArray",          "$$ = $1;" ]],

                ''"JSONObject" [[ "{ }", "$$ = {};" ],
                ''               [ "{ JSONMemberList }", "$$ = $2;" ]],

                ''"JSONMember" [[ "JSONString : JSONValue", "$$ = [$1, $3];" ]],

                ''"JSONMemberList" [[ "JSONMember", "$$ = {}; $$[$1[0]] = $1[1];" ],
                ''                   [ "JSONMemberList , JSONMember", "$$ = $1; $1[$3[0]] = $3[1];" ]],

                ''"JSONArray" [[ "[ ]", "$$ = [];" ],
                ''              [ "[ JSONElementList ]", "$$ = $2;" ]],

                ''"JSONElementList" [[ "JSONValue", "$$ = [$1];" ],
                ''                    [ "JSONElementList , JSONValue", "$$ = $1; $1.push($3);" ]]


                '["\\s+", "/* skip whitespace */"],
                '["{int}{frac}?{exp}?\\b", "return 'NUMBER';"],
                '["\"(?:{esc}[\"bfnrt/{esc}]|{esc}u[a-fA-F0-9]{4}|[^\"{esc}])*\"", "yytext = yytext.substr(1,yyleng-2); return 'STRING';"],
                '["\\{", "return '{'"],
                '["\\}", "return '}'"],
                '["\\[", "return '['"],
                '["\\]", "return ']'"],
                '[",", "return ','"],
                '[":", "return ':'"],
                '["true\\b", "return 'TRUE'"],
                '["false\\b", "return 'FALSE'"],
                '["null\\b", "return 'NULL'"]


                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_digit
                NewGram.Exp = "^[0-9]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_esc
                NewGram.Exp = "^\\\\"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_int
                NewGram.Exp = "^-?(?:[0-9]|[1-9][0-9]+)"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_exp
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._JSON_frac
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)"
                iSpec.Add(NewGram)


                Return iSpec
            End Function

        End Class
    End Namespace
End Namespace

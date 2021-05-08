
Imports SDK.SmallProgLang.GrammarFactory.Grammar

Namespace SmallProgLang
    'GRAMMAR
    '
    Namespace GrammarFactory
        'https://www.tutorialspoint.com/logo/logo_introduction.htm
        'https://www.transum.org/software/Logo/ (Online logo)
        'https://www.calormen.com/jslogo/

        'FORWARD	    fd	            FORWARD(space)<number Of steps To move forward>	Moves turtle forward For number Of times specified	"forward 100" Or "fd 100"
        'BACK	        bk	            BACK(space) <number Of steps To move backward>	Moves turtle back For number Of times specified	"back 100" Or "bk 100"
        'RIGHT	        rt	            RIGHT(space) <degrees To rotate toward right	Turns turtle right For number Of degrees specified	"right 228" Or "rt 228"
        'LEFT	        lt	            LEFT(space) <degrees To rotate toward left >	Turns turtle left For number Of degrees specified	"left 228" Or "lt 228"
        'HOME	        home	        Home	Comes To screen center but does Not clear the screen	"home"
        'CLEAN	        ct cs	        Clean	Clears the screen Of trails but the turtle remains where it Is without moving	"clean"
        'CLEARSCREEN	CS	            Clearscreen	Clears the screen Of trails And comes To screen center	"cs"
        'HIDETURTLE	    HT	            Hide turtle	Hides the turtle And aids viewing a clear drawing On the screen	"ht"
        'SHOWTURTLE	    ST	            Show turtle	Shows the turtle after it Is hidden from the screen	"st"
        'PENUP	        PU(set)         Pen up	Sets the turtle To move without drawing	"pu"
        'PENDOWN	    PD(resets)      Pen	Resets To a drawing pen When ordered To move	"pd"
        'CLEARTEXT	    CT	Clear text	Clears all text In the command screen	"ct"



        '
        'signExpression
        '   (('+' | '-'))* (number | deref | func)
        '
        'multiplyingExpression
        '    : signExpression (('*' | '/') signExpression)*
        '
        'expression
        '    : multiplyingExpression (('+' | '-') multiplyingExpression)*
        '
        'parameterDeclarations
        '       : ':' name (',' parameterDeclarations)*
        '
        'procedureDeclaration
        '       : 'to' name parameterDeclarations* EOL? (line? EOL) + 'end'
        '
        'procedureInvocation
        '       : name expression*
        '
        'deref
        '   ':' name
        '
        'fd
        '   : ('fd' | 'forward') expression
        '
        'bk
        '   : ('bk' | 'backward') expression
        '
        'rt
        '   : ('rt' | 'right') expression
        '
        'lt 
        '    : ('lt' | 'left') expression
        '
        'cs
        '    : cs
        '    : clearscreen
        '
        'pu
        '    : pu
        '    : penup
        '
        'pd
        '
        '    : pd
        '    : pendown
        '
        'ht
        '
        '    : ht
        '    : hideturtle
        '
        'st'
        '
        '    : st
        '    : showturtle
        '
        '    : Home
        '
        '    : Stop
        '
        '    : label
        '
        'setxy
        '    : setxy expression expression
        '
        'random
        '
        '    : random expression
        '
        'for
        '    : 'for' '[' name expression expression expression ']' block
        '
        'value
        ' String / Expression / deref
        '
        'name
        '   String
        '
        'print
        '    : 'print' (value | quotedstring)
        '
        'make
        '    : 'make' STRINGLITERAL value
        '
        'comparison
        ' : expression comparisonOperator expression
        '
        'comparisonOperator
        '       '<'
        '       '>'
        '       '='
        '
        'if
        '       'if' comparison block
        ' block
        '       '[' cmd + ']'
        '
        'repeat
        '       : 'repeat' number block
        '
        'func
        '   : random
        '
        'line
        '       : cmd + comment?
        '       : comment
        '       : print comment?
        '       : procedureDeclaration
        '
        'prog
        '       :(line? EOL) + line?
        '
        'comment
        '       : COMMENT
        '       :  ~ [\r\n]*';'




        Public Class LogoGrammar
            Public Shared Function GetLOGOGrammar() As List(Of Grammar)
                Dim iSpec As New List(Of Grammar)
                Dim NewGram As New Grammar
                NewGram = New Grammar
                NewGram.ID = Type_Id._COMMENT
                NewGram.Exp = "^\~"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._LOGO_LANG
                NewGram.Exp = "\blogo_lang\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id.EOL
                NewGram.Exp = "^\;"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._deref
                NewGram.Exp = "^\:"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._fd
                NewGram.Exp = "\bfd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._fd
                NewGram.Exp = "\bforward\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._bk
                NewGram.Exp = "\bbackward\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._bk
                NewGram.Exp = "\bbk\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._rt
                NewGram.Exp = "\brt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._rt
                NewGram.Exp = "\bright\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._lt
                NewGram.Exp = "\blt\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._lt
                NewGram.Exp = "\bleft\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._cs
                NewGram.Exp = "\bcs\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._cs
                NewGram.Exp = "\bclearscreen\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._pu
                NewGram.Exp = "\bpu\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._pu
                NewGram.Exp = "\bpenup\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._pd
                NewGram.Exp = "\bpd\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._pd
                NewGram.Exp = "\bpendown\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._ht
                NewGram.Exp = "\bht\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._ht
                NewGram.Exp = "\bhideturtle\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._st
                NewGram.Exp = "\bst\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._st
                NewGram.Exp = "\bshowturtle\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._label
                NewGram.Exp = "\blabel\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._setxy
                NewGram.Exp = "\bsetxy\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._make
                NewGram.Exp = "\bmake\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._ife
                NewGram.Exp = "\bife\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._fore
                NewGram.Exp = "\bfore\b"
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
                'Variable
                NewGram = New Grammar
                NewGram.ID = Type_Id._name
                NewGram.Exp = "^\b[a-z][a-z0-9]+\b"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._number
                NewGram.Exp = "^\d+"
                iSpec.Add(NewGram)
                ''=
                NewGram = New Grammar
                NewGram.ID = Type_Id._SIMPLE_ASSIGN
                NewGram.Exp = "^\="
                iSpec.Add(NewGram)

                '*=, /=, +=, -=,
                NewGram = New Grammar
                NewGram.ID = Type_Id._signExpression
                NewGram.Exp = "^(\*|\/|\+|\-)="
                iSpec.Add(NewGram)

                'Equality operators: ==, !=
                NewGram = New Grammar
                NewGram.ID = Type_Id._comparisonOperator
                NewGram.Exp = "^(=|!)=\="
                iSpec.Add(NewGram)
                'Relational operators: >, >=, <, <=
                NewGram = New Grammar
                NewGram.ID = Type_Id._comparisonOperator
                NewGram.Exp = "^[><]\=?"
                iSpec.Add(NewGram)
                'Math operators: +, -, *, /
                NewGram = New Grammar
                NewGram.ID = Type_Id._signExpression
                NewGram.Exp = "^[+\-]"
                iSpec.Add(NewGram)
                NewGram = New Grammar
                NewGram.ID = Type_Id._multiplyingExpression
                NewGram.Exp = "^[*/]"
                iSpec.Add(NewGram)





                Return iSpec
            End Function

            Public Function GetLogoRef()
                Dim str As String = ""
                str = My.Resources.LOGO_QUICK_REF
                Return str
            End Function
        End Class


        '

    End Namespace
End Namespace


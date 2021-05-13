 # LOGO PROGRAMMING LANGUAGE
 
 'Used to be one of my first programmiong languges as a child in the 1070's
 'It will parse and eval. And the virtual machine can use the surface to draw the images. 
 This may for the part of the graphic system for the Small Programming language. 
 
 I had previously begun a project simular to this ; 
 
  
  
    
 # ## Useful Links   
        'https://www.tutorialspoint.com/logo/logo_introduction.htm
        'https://www.transum.org/software/Logo/ (Online logo)
        'https://www.calormen.com/jslogo/


 ### Commands
 
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
        
 
 ### Grammar
 
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


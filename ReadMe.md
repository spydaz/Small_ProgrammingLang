## Small Programming Language

My Personal choice is BASIC as it has always been a favorite of mine ; as the pseudo code is basically the same as the real code which makes it very readable without the flourishing of multiple brackets. Although there will be brackets for code boundary as well as semi colons as end markers when required. Currently My older versions seem to be a bit more advanced (still primitive) but the newest version (lexParseEval) will be a more correct model ; again only having the basics of the basic language; (not highly useful)- but still a learning tool(journey/Experience).

https://spydaz.github.io/SpydazWeb-AI-_Emulators/

This basic programming language is a project to learn how to build and design a programming language from scratch.

The Focus is on a type of Basic programming language format;

`My Personal choice is BASIC as it has always been a favorite of mine ; as the pseudo code is basically the same as the real code which makes it very readable without the flourishing of multiple brackets. Although there will be brackets for code boundary as well as semi colons as end markers when required. Currently My older versions seem to be a bit more advanced (still primitive) but the newest version (lexParseEval) will be a more correct model ; again only having the basics of the basic language; (not highly useful)- but still a learning tool(journey/Experience). ``

Again as they are developed the function syntax will be displayed below; Currently the SPL will accept direct machine code instructions by flourishing the statement with a SAL: Tag;

### DESCRIPTION:
The program has been designed in mini stages

`
#The Lexer

This is a tokenizer which basically takes each token by using a grammar model to be used by the RegEx Searcher; These token produced give the position and the string that they have defined. A lookAhead Function has been added to preview the token to be consumed next. A record of tokens collected is also stored enabling for look back as well as Stepping backwards if required.
The Parser

This Parser consumes the tokens produced by the tokenizer and attempts to construct Abstract Syntax Tokens Which enable for the Interpreter to Evaluate or Transpile the code to another Programming language such as Assembly language to be run on a virtual CPU.
The Interpreter

This component Takes the Abstract Syntax tokens produced and evaluates(Executes) the Code. Here the decision is made to execute on the virtual hardware or evaluate in memory to produce a result.


### Small Programming Language - SYNTAX

This is the Current Syntax (Valid)
The parser may accept other complexed combinations Which may not be valid evaluation arguments.(yet) SemiColons are used to denote the end of a Expression Although they are not always needed by the compiler

### Basic Expressions - Constantly updated

	DIM <Identifier> = String Integer Array boolean

	<Identifier> =	Value 
					"Value" 
					true/false 
					[Value,Value,Value] 
					["Value","Value","Value"] 
					[true,False,true] 
					[]
	<Identifier>	+= Value
					*= Value
					-= Value
					/= Value
	<Identifier>	<  Value
					>  Value
					<= Value
					>= Value

	<Identifier> + Value * Value + <Identifier> - (Value + Value);
	<Identifier> = Value + <Identifier> * Value + Value - (<Identifier> + Value);
	<Identifier> = ( <Identifier> = value );

	'
	Used For Functions/Lambdas

	<Identifier>( <Identifier> = value );
	<Identifier> = ( <Identifier> = value );
	
### Spydaz Virtual Machine Code Language ;
## Description :

This assembly language is specific to this virtual processor 
Enableing for the code to be executed on the cpu: 
This is a Micro based instruction set (Misc) Uses Reverse Polish Notation

	SAL:

	_PUSH				: Pushes items on to the stack (top)
	_POP				: Pops items off the stack (top)
	_PEEK				: Views items on the stack (top)
	_WAIT				: Pauses execution of code
	_PAUSE				: Pauses execution of code
	_HALT				: HALT execution of code
	_RESUME				: Resumes execution of code
	_DUP				: Duplicates item on the stack (top)
	_JMP				: Jumps to location
	_JIF_T				: Jump if true
	_JIF_F				: Jump if False
	_JIF_EQ				: Jump if Equals
	_JIF_GT				: Jump if Greater than
	_JIF_LT				: Jump if Less than
	_LOAD				: Load Memory Address
	_STORE				: Store at memeory Address
	_REMOVE				: Removes item at memeory address so location can be free for replacement or updated item
	_CALL				: call location in memory
	_RET				: Return to location called
	_PRINT_M			: Prints to TextConsole Display
	_ADD				: Adds last two items on the stack
	_SUB				: subracts last two items on the stack
	_MUL				: multiplys last two items on the stack
	_DIV				: divides last two items on the stack
	_AND				: if both last two items on the stack are true
	_OR				: if either last two items on the stack are true
	_NOT				: if both last two items on the stack are not true
	_IS_EQ				: if both last two items on the stack are Equals
	_IS_GT				: both items are compared 
	_IS_GTE				: both items are compared 
	_IS_LT				: both items are compared 
	_IS_LTE				: both items are compared 
	_TO_POS				: number is sent to negative
	_TO_NEG				: number is sent to positive
	_INCR				: number is incremented by 1
	_DECR 				: number is Decremented by 2

The Current instruction Set Will Later include a memory register location system ; (after researching different models and implementations)( a vitual machine model based on this was created using a stack machine style. this will form the basis of my (Virtual Machine) - to be developed over time (maybe even format a virtual disk in the future)


# LOGO PROGRAMMING LANGUAGE

'Used to be one of my first programmiong languges as a child in the 1070's 'It will parse and eval. And the virtual machine can use the surface to draw the images. This may for the part of the graphic system for the Small Programming language.

I had previously begun a project simular to this ;

## Useful Links

    'https://www.tutorialspoint.com/logo/logo_introduction.htm
    'https://www.transum.org/software/Logo/ (Online logo)
    'https://www.calormen.com/jslogo/

# Commands

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

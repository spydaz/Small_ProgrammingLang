# LexParseEval
SpydazWeb Program languges and virtual machines, Parser/Compiler/Interpretor. AST Creator... Experiments in developing compilers and programing langugages . Using Mini Basic language to Tokenizer to AST to Assembly code 

https://spydaz.github.io/SpydazWeb-AI-_Emulators/

##Insight
	This Present source code has been changed due to leaning new ways in which to create the hierachy for the AST Tree as well as transpile to S-Expression form for Quick evaluation, prompting me to change my model to be more universal. 


## DESCRIPTION:
	A basic programming language designed in stages to be compiled and executed on a virtual machine :
	the language is translated to an assembly code to be executed on a Virtual Stack based CPU
	The process of tokeninzing the input and susequentially into an executable abstract syntax tree
	the tree generates the required assembly code and executes the code in a virtual environment.
	
## Features:
	Currently Creating a REPL to allow for the compiling and executing of the SAL Assembly language; 
	The Basic programming language will also be able to be compiled and executed in the REPL. 
	The Agnostic parser allows for chooseing which language to devlop with by declaring; either lang as the header of the code
	
### BONUS ABILITYS:

This was not only a to devlop a programming language as this has never been my goal, in fact to parse a syntax tree for gramatical syntax trees is my interest for my artificial inteligence languge components; Initialy i did have this half implemented by obviously my understanding of how to deal with the gramatical trees was not working so AST.... Yep ... its universal ;

   - tokenizer/parser/interpretor ;
   - (lex/ParseEval) ;
   - interpretor Design Pattern ;

They are all generally the same in the end; (it is the techniques) (generic is never the way to go!)... 

Second Bonus (the parser will begin to accept other syntaxes) enebaling for anystyle of programming language in the same program (ie C# with VB) ? Em why not as they are both compiling on the same compiler to the same expressions to the same framworks ? why did microsoft not just do this automatically too?.... Lets see how it goes!


#AST Development / Visualization
	This plays a key part in the devlopment of programming languages the ablity to veiw the tree as well as transform the tree ; collecting as much information as possible enabling for error handleing(nightmares) ... and messaging for the UI... 
	
	
AST => TreeView as well as AST => JSON View
	
# sal_lang 	
	sal_lang 
	sal push 45 halt
	
# spl_lang	
	spl_lang
	45+5(+45);
	
# Spydaz Virtual Machine Code Language ;

## Description :
	This assembly language is specific to this virtual processor 
	Enableing for the code to be executed on the cpu: 
	This is a Micro based instruction set (Misc) Uses Reverse Polish Notation
# SAL:
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



# Small Programming Language
My Personal choice is BASIC as it has always been a favorite of mine ; as the psudeo code is basically the same as the real code which makes it very readable without the flurishing of mulitple brackets. Although there will be brackets for code boundrys as well as semi colons as end markers when required. 
Currently My older versions seem to be a bit more advanced (still primative) but the newest version (lexParseEval) will be a more correct model ; again only having the basics of the basic language; (not highly useful)- but still a learning tool(journey/Experience). 

https://spydaz.github.io/SpydazWeb-AI-_Emulators/


This basic programming language is a project to learn how to build and design a programming  language from scratch.

**The Focus is on a type of Basic programming language format;**
### Small Programming Language

My Personal choice is BASIC as it has always been a favorite of mine ; as the psudeo code is basically the same as the real code which makes it very readable without the flourishing of multiple brackets. Although there will be brackets for code boundary as well as semi colons as end markers when required. 
Currently My older versions seem to be a bit more advanced (still primitive) but the newest version (lexParseEval) will be a more correct model ; again only having the basics of the basic language; (not highly useful)- but still a learning tool(journey/Experience). 

Again as they are developed the function syntax will be displayed below; Currently the SPL will accept direct machine code instructions by flourishing the statement with a SAL: Tag; 

## DESCRIPTION:
The program has been designed in stages ;
`
### The Lexer 
    This is a tokenizer which basically takes each token by using a grammar model to be used by the RegEx Searcher; These token produced give the position and the string that they have defined. 
    A lookAhead Function has been added to preview the token to be consumed next.  A record of tokens collected is also stored enabling for look back as well as Stepping backwards if required.
 `   
### The Parser
    This Parser consumes the tokens produced by the tokenizer and attempts to construct Abstract Syntax Tokens Which enable for the Interpreter to Evaluate or Transpile the code to another Programming language such as Assembly language to be run on a virtual CPU. 
 ` `  
### The Interpreter
    This component Takes the Abstract Syntax tokens produced and evaluates(Executes) the Code. Here the decision is made to execute on the virtual hardware or evaluate in memory to produce a result.


`
### Small Programming Language
This is the Current Syntax (Valid)  
The parser may accept other complexed combinations Which may not be valid evaluation arguments.(yet)
SemiColons are used to denote the end of a Expression 
Although they are not always needed by the compiler 

# Basic Expressions - Constantly updated

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
# Used For Functions/Lambdas
	<Identifier>( <Identifier> = value );
	<Identifier> = ( <Identifier> = value );
'
# Combination Expressions : 
	(i have not gone inot depth explaining here yet as there are too many to document due to the nesting)

	Expression + - / * Expression;
	Expression > < <= >= Expression;
	''CodeBlock With Return Values
	{Expression; <RETURN>[Value,Value,Value];}

	{Expression;
	Expression;
	Expression;}


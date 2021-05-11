# Lex Parse Eval
SpydazWeb Program languages and virtual machines, Parser/Compiler/ Interpreter. AST Creator... Experiments in developing compilers and programming languages . Using Mini Basic language to Tokenizer to AST to Assembly code 

https://spydaz.github.io/SpydazWeb-AI-_Emulators/

##Insight
	This Present source code has been changed due to leaning new ways in which to create the hierarchy for the AST Tree as well as Transpile to S-Expression form for Quick evaluation, prompting me to change my model to be more universal. 


## DESCRIPTION:
A basic programming language designed in stages to be compiled and executed on a virtual machine :
the language is translated to an assembly code to be executed on a Virtual Stack based CPU
The process of tokenizer the input and sequentially into an executable abstract syntax tree
the tree generates the required assembly code and executes the code in a virtual environment.
	
## Features:
Currently Creating a REPL to allow for the compiling and executing of the SAL Assembly language; 
The Basic programming language will also be able to be compiled and executed in the REPL. 
The Agnostic parser allows for choosing which language to develop with by declaring; either lang as the header of the code
	
### BONUS ABILITYS:

This was not only a to develop a programming language as this has never been my goal, in fact to parse a syntax tree for grammatical syntax trees is my interest for my artificial intelligence language components; Initially i did have this half implemented by obviously my understanding of how to deal with the grammatical trees was not working so AST.... Yep ... its universal ;

   - tokenizer/parser/interpreter ;
   - (lex/ParseEval) ;
   - interpreter Design Pattern ;

They are all generally the same in the end; (it is the techniques) (generic is never the way to go!)... 

Second Bonus (the parser will begin to accept other syntax's) enebaling for any style of programming language in the same program (ie C# with VB) ? Em why not as they are both compiling on the same compiler to the same expressions to the same frameworks ? why did Microsoft not just do this automatically too?.... Lets see how it goes!


#AST Development / Visualization
	This plays a key part in the development of programming languages the ability to view the tree as well as transform the tree ; collecting as much information as possible enabling for error handling(nightmares) ... and messaging for the UI... 
	
	
AST => TreeView as well as AST => JSON View
	
# sal_lang 	
	sal_lang 
	sal push 45 halt
	
# spl_lang	
	spl_lang
	45+5(+45);
	

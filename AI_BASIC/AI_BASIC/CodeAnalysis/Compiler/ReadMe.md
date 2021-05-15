# Compilers 

Here are the Compilers and Transpilers Lexers and Parsers 

The lexer patern and parser pattern need to be universal;
Exchanging tokens instead of values as the values will be contian within . 

## The Tokenizer
Tokens Captured Should contain thie atomic data as well as thier expected values,
as well as the verbose information such as position, and original raw value.
The Tokenizers only job is to recognize tokens and produce a token tree with diagnostics
Basically the more keywords and characters that can be recogonized the better the lexer
Although i have some Implementation for the regEx tokenizer . 
I may hardcode as many command and keywords from as many languages as possible.
this way i can implement languages if the syntax is known .. (parsers job)


## The Parser 
Assembles the tokens captured into Valid Expression Syntax nodes(statments),
Here we can produce the general expressions expected and which we can evaluate. (this is the key factor) = Can it be evaluated;
if we have the tokens we can accept as many syntaxes as possible as different assignment statments in different languages produce the same result... an assigned idenfifer.
This will allow for multiple diffent syntaxes to be defined. 

# Essentially if the parser recognizes the statement then it is Evaluatable(and there are no compiler errors)

## The Evaluator ; 
    Evaluates each node returning a value; Utilizing the memory environment as neccasary to handle variables and function calls etc. 
    If it is in the parse tree then we should define an evaluation in the Syntaxnode . 
    Here in the evaluation stage it simply calls the evaluation method... 
    allowing for the node to take care of the evaluation and return its result.
    if it has no result ie then it returns true as the statment block has completed. 
    Variables are stored in the global memeory! local memory is managed in the syntaxnodes which require access to a local environment, 
    the global memeory is always passed to the syntaxnode. updating it as required as some variables are only local!.

### Ahh it starts to come together in mind! 
    the final result will be the last statement in the tree! basicaly a program is a long calcluation!

## The Transpiler (EMITTER)
    This enables for code to be generated for various language model outputs 
    Again handled in the syntax node as only it knows how to handle its own syntax and convert to other langs.
    the transplier will simply call its emit method and build a final program string ; 
    to be returned as the transpiled language;


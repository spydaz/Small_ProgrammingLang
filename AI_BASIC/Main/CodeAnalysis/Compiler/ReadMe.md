# Compilers 

Here are the Compilers and Transpilers Lexers and Parsers 

The lexer patern and parser pattern need to be universal;
Exchanging tokens instead of values as the values will be contian within . 

## The Tokenizer
Tokens Captured Should contain thie atomic data as well as thier expected values,
as well as the verbose information such as position, and original raw value.
## The Parser 
Assembles the tokens captured into Valid Expression Syntax nodes(statments)

## The Evaluator ; 
Evaluates each node returning a value; Utilizing the memory environment as necasary to handle variables and function calls etc. 

## The Transpiler
This enables for code to be generated for various language model outputs 
 

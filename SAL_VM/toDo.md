### to do....

### LOGO
	still not drawing on screen some minor problem needs debugging;
	Until its working cant progress to far on logo Eval statments need to also be created to execute the command on the new GPU for the SAL_VM
	commands are recognizing in parser ; integrated into parsing cycle for main (means languages are intergrated)
	Currently ; 

### SAL
	The sal uses a key word to initiate the string capture of code; 
	but it could be prudent to instigate the same variable recognition cycle as the logo cycle
	Building a separate parser and only leaving behind what is needed for the multi parser; 
	by correctly recognizing the tokens and incorperating them as a program statment. 
	as well as creating the evaluate correctly to evaluate / emit the code for the SAL_VM System


### SMALL_BASIC
	undertanding how to intergrate the other languge models has let me understand,
	how to recognize specific tokens whcih seem to always recognize as identifiers...Fine...
	Now it should be possible to recognize 
`
### =function/Sub declerations 

## -Syntax Function
-Declare;
	def function_identifier [Param_identifier,Param_identifer]{codeblock{return Identifer]};
	-Call;
	identifier = function_identifier [PARAM_LITERAL,PARAM_LITERAL];
## -Syntax sub
-Declare;
		def sub_identifier [Param_identifier,Param_identifer]{codeblock{return true]};
		
-Call;
		call sub_identifier [PARAM_LITERAL,PARAM_LITERAL];
`



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

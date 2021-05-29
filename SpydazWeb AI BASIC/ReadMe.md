# The Spydaz Web Basic Programming Language

This is the model for the Spydazweb Small basic programming language.
It is design to be intergrated in to the SpydazWeb artificial intelligence development environment;

As a Goal it will be the prime programming languge for NLP/Machine learning and other logic problem solving;

This model has been inspired by the Build a Compiler series by (Immo Landwerth) https://github.com/terrajobst on the microsoft Language devlopment team;
And Also By Dmitry Soshnikov Building a parser from Scratch https://github.com/DmitrySoshnikov;
Well done to both;

The Aim is to also have Virtual machine also to execute code on; as well as be compilable and recognized by the visual studio editors;
Hence so many model changes to conform to the various models;

The syntax should be Relative to Basic and C#; but will also contain the extra functions provided by the SpydazWeb AI SDK.
 
# This is the MAIN MODEL
So Previous models will be intergrated into this single language version; 
Consisting of the low level assembly langauge for the VM and the Logo style language for the VM graphics; THe higher level languge will contain the basic commands that are normally incorperated into languages;
 
# Current Progress
Basic Framwork in place; 
Parser expression syntaxes to be implemented ; 
Main Model Designed ;
Importing old nodes (Converting) to reach the same stage as before ; 
This project has basically taken a single day to implement; From memory.... 

    1) I will probably implement the syntaxmodules tomorrow and test... 
        the basic eval 
        3+3*(45+5)

    2) Then the Assignments 
        x = 2+3-4(34+9)
        x = "cat"
        X = [2,3,4]
        x = true/false
        x +=3
        x > 6
        x >= 5

    3) Variable Declarations 
        Dim X as string / boolean / integer / double / list / date
        Dim X as (string / boolean / integer / double) = true / "CAT" / 98 / [45,45.0] / [true,true] / [X,Y]
        
    4) IF/THEN/ELSE
       if 4>foo then {45+3} else {45+89}
       if 4>foo then {45+3} 
      
      
       



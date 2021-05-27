'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\VisualRepl.vb
'
' summary:	Visual repl class
'---------------------------------------------------------------------------------------------------

Imports System.Windows.Forms

Namespace Consoles

    Module VisualRepl

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Executes the visual repl operation. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub runVisualRepl()


                Call Application.Run(New Editor_IDE())
            End Sub
        End Module

End Namespace


'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\MAIN\Program.vb
'
' summary:	Program class
'---------------------------------------------------------------------------------------------------

Imports System.Windows.Forms
Imports System.Console
Imports AI_BASIC.CodeAnalysis.Compiler.Interpretor

Module Program

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Main entry-point for this application. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Sub Main()
        Call Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        '   iRepl.RunInterpretorRepl()
        Dim MyRepl As New InterpretorRepl

        MyRepl.RunCmdLine()


    End Sub


End Module

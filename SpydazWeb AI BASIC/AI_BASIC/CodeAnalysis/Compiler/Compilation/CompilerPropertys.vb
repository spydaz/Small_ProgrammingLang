'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Compilation\CompilerPropertys.vb
'
' summary:	Compiler propertys class
'---------------------------------------------------------------------------------------------------

Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports AI_BASIC.CodeAnalysis.Compiler

'''////////////////////////////////////////////////////////////////////////////////////////////////////
''' <summary>   A compiler propertys. </summary>
'''
''' <remarks>   Leroy, 27/05/2021. </remarks>
'''////////////////////////////////////////////////////////////////////////////////////////////////////

Public Class CompilerPropertys
    ''' <summary>   The refference files. </summary>
    Public RefferenceFiles As New List(Of String)
    ''' <summary>   The embedded files. </summary>
    Public EmbeddedFiles As New List(Of String)
    ''' <summary>   The code text. </summary>
    Public CodeText As String = ""
    ''' <summary>   The folder location. </summary>
    Public FolderLocation As String = ""
    ''' <summary>   The errors. </summary>
    Public Errors As New List(Of String)
    ''' <summary>   True to code ready. </summary>
    Public CodeReady As Boolean = False

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Button compile DLL click. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub ButtonCompileDLL_Click(sender As Object, e As EventArgs) Handles ButtonCompileDLL.Click
        If CodeReady = True Then
            VBC.VB_Tab_CodeCompiler(CodeText, "DLL", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
        Else
            Speaker.Speak("Code Not Loaded - Unable to Proceed")
            Me.Close()
        End If

    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Button compile executable click. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub ButtonCompileEXE_Click(sender As Object, e As EventArgs) Handles ButtonCompileEXE.Click
        If CodeReady = True Then
            VBC.VB_Tab_CodeCompiler(CodeText, "EXE", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
        Else
            Speaker.Speak("Code Not Loaded - Unable to Proceed")
            Me.Close()
        End If

    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Button browse click. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TextBoxFolderName.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Adds an embedded click to 'e'. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub AddEmbedded_Click(sender As Object, e As EventArgs) Handles AddEmbedded.Click
        Dim myOpenFileDialog As New OpenFileDialog()


        myOpenFileDialog.CheckFileExists = True
        myOpenFileDialog.Filter = "Embedded files (*.*)|*.*"
        myOpenFileDialog.InitialDirectory = Application.StartupPath
        myOpenFileDialog.Multiselect = False

        If myOpenFileDialog.ShowDialog = DialogResult.OK Then
            Dim str = myOpenFileDialog.FileName
            EmbeddedFilePaths.Items.Add(str)
            EmbeddedFiles.Add(str)
        End If
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Adds the refferences click to 'e'. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub AddRefferences_Click(sender As Object, e As EventArgs) Handles AddRefferences.Click
        Dim myOpenFileDialog As New OpenFileDialog()


        myOpenFileDialog.CheckFileExists = True
        myOpenFileDialog.Filter = "Refference files (*.*)|*.*"
        myOpenFileDialog.InitialDirectory = Application.StartupPath
        myOpenFileDialog.Multiselect = False

        If myOpenFileDialog.ShowDialog = DialogResult.OK Then
            Dim str = myOpenFileDialog.FileName
            RefferenceFilePaths.Items.Add(str)
            RefferenceFiles.Add(str)
        End If
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Removes the embedded click. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub RemoveEmbedded_Click(sender As Object, e As EventArgs) Handles RemoveEmbedded.Click
        EmbeddedFiles.Remove(EmbeddedFilePaths.SelectedItem)
        EmbeddedFilePaths.Items.Remove(EmbeddedFilePaths.SelectedItem)
    End Sub

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Removes the refference click. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub RemoveRefference_Click(sender As Object, e As EventArgs) Handles RemoveRefference.Click
        RefferenceFiles.Remove(EmbeddedFilePaths.SelectedItem)
        RefferenceFilePaths.Items.Remove(RefferenceFilePaths.SelectedItem)
    End Sub
    ''' <summary>   The speaker. </summary>

    Private Shared Speaker As New Speech.Synthesis.SpeechSynthesizer

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   Compiler propertys load. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''
    ''' <param name="sender">   Source of the event. </param>
    ''' <param name="e">        Event information. </param>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub CompilerPropertys_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxFolderName.Text = Application.StartupPath
        If CodeText.Length < 1 Then CodeReady = False
        If CodeReady = True Then
            Speaker.SpeakAsync("Code Ready! for Compile!")
        Else
            Speaker.SpeakAsync("Compiler Not Ready for Use, No Code!? is Loaded!.")

        End If


    End Sub
End Class
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports AI_BASIC.CodeAnalysis.Compiler

Public Class CompilerPropertys
    Public RefferenceFiles As New List(Of String)
    Public EmbeddedFiles As New List(Of String)
    Public CodeText As String = ""
    Public FolderLocation As String = ""
    Public Errors As New List(Of String)
    Public CodeReady As Boolean = False
    Private Sub ButtonCompileDLL_Click(sender As Object, e As EventArgs) Handles ButtonCompileDLL.Click
        If CodeReady = True Then
            VBC.VB_Tab_CodeCompiler(CodeText, "DLL", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
        Else
            Speaker.Speak("Code Not Loaded - Unable to Proceed")
            Me.Close()
        End If

    End Sub

    Private Sub ButtonCompileEXE_Click(sender As Object, e As EventArgs) Handles ButtonCompileEXE.Click
        If CodeReady = True Then
            VBC.VB_Tab_CodeCompiler(CodeText, "EXE", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
        Else
            Speaker.Speak("Code Not Loaded - Unable to Proceed")
            Me.Close()
        End If

    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TextBoxFolderName.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

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

    Private Sub RemoveEmbedded_Click(sender As Object, e As EventArgs) Handles RemoveEmbedded.Click
        EmbeddedFiles.Remove(EmbeddedFilePaths.SelectedItem)
        EmbeddedFilePaths.Items.Remove(EmbeddedFilePaths.SelectedItem)
    End Sub

    Private Sub RemoveRefference_Click(sender As Object, e As EventArgs) Handles RemoveRefference.Click
        RefferenceFiles.Remove(EmbeddedFilePaths.SelectedItem)
        RefferenceFilePaths.Items.Remove(RefferenceFilePaths.SelectedItem)
    End Sub

    Private Shared Speaker As New Speech.Synthesis.SpeechSynthesizer



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
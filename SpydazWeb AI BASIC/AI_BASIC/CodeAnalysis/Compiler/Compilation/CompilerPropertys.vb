﻿Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class CompilerPropertys
    Public RefferenceFiles As New List(Of String)
    Public EmbeddedFiles As New List(Of String)
    Public CodeText As String = ""
    Public FolderLocation As String = ""
    Public Errors As New List(Of String)
    Public CodeReady As Boolean = False
    Private Sub ButtonCompileDLL_Click(sender As Object, e As EventArgs) Handles ButtonCompileDLL.Click
        If CodeReady = True Then
            VB_Tab_CodeCompiler(CodeText, "DLL", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
        Else
            Speaker.Speak("Code Not Loaded - Unable to Proceed")
            Me.Close()
        End If

    End Sub

    Private Sub ButtonCompileEXE_Click(sender As Object, e As EventArgs) Handles ButtonCompileEXE.Click
        If CodeReady = True Then
            VB_Tab_CodeCompiler(CodeText, "EXE", TextBoxMainClassName.Text, TextBoxStartFunctionName.Text, RefferenceFiles, EmbeddedFiles)
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


    Public Sub VB_Tab_CodeCompiler(CodeBlock As String,
                                          Optional ByRef CompileType As String = "DLL",
                                          Optional ByRef iClassName As String = "MainClass",
                                          Optional iMethodName As String = "Execute",
                                          Optional Assemblies As List(Of String) = Nothing,
                                          Optional ByRef EmbededFiles As List(Of String) = Nothing)

        Const Lang As String = "VisualBasic"

        'The CreateProvider method returns a CodeDomProvider instance for the specificed language name.
        'This instance is later used when we have finished specifying the parmameter values.
        Dim Compiler As CodeDom.Compiler.CodeDomProvider = CodeDom.Compiler.CodeDomProvider.CreateProvider(Lang)
        'Optionally, this could be called Parameters.
        'Think of parameters as settings or specific values passed to a method (later passsed to CompileAssemblyFromSource method).
        Dim Settings As New CodeDom.Compiler.CompilerParameters
        'The CompileAssemblyFromSource method returns a CompilerResult that will be stored in this variable.
        Dim Result As CodeDom.Compiler.CompilerResults = Nothing
        Dim ExecuteableName As String = ""
        If EmbededFiles Is Nothing Then EmbededFiles = New List(Of String)


        Try
            Speaker.Speak("Adding Embedded resources")
            'handle Embedded Resources
            If EmbededFiles IsNot Nothing And EmbededFiles.Count > 0 Then
                For Each str As String In EmbededFiles
                    Settings.EmbeddedResources.Add(str)
                Next
            End If
        Catch ex As Exception
            Speaker.Speak("There is a problem with a resource")
        End Try

        Try
            Speaker.Speak("Adding Assemblys")
            If Assemblies IsNot Nothing And Assemblies.Count > 0 Then
                For Each str As String In Assemblies
                    Settings.ReferencedAssemblies.Add(str)
                Next
            End If
        Catch ex As Exception
            Speaker.Speak("There is a problem with an refference assembly")
        End Try
        'Must Always be added
        Settings.ReferencedAssemblies.Add("System.Windows.Forms.dll")
        Settings.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
        Settings.ReferencedAssemblies.Add("System.dll")
#Region "COMPILER"

        Select Case CompileType
            Case "EXE"
                Speaker.Speak("Compile EXE")
                Dim S As New SaveFileDialog
                With S

                    .Filter = "Executable (*.exe)|*.exe"
                    If (.ShowDialog() = DialogResult.OK) Then
                        ExecuteableName = .FileName
                    End If
                End With
                'Library type options : /target:library, /target:win, /target:winexe
                'Generates an executable instead of a class library.
                'Compiles in memory.
                Settings.GenerateInMemory = True
                Settings.GenerateExecutable = True
                Settings.CompilerOptions = " /target:winexe"
                'Set the assembly file name / path
                Settings.OutputAssembly = ExecuteableName
                'Read the documentation for the result again variable.
                'Calls the CompileAssemblyFromSource that will compile the specified source code using the parameters specified in the settings variable.
                Result = Compiler.CompileAssemblyFromSource(Settings, CodeBlock)
            Case "MEM"
                Speaker.Speak("Executing")
                'Compiles in memory.
                Settings.GenerateInMemory = True
                'Read the documentation for the result again variable.
                'Calls the CompileAssemblyFromSource that will compile the specified source code using the parameters specified in the settings variable.
                Result = Compiler.CompileAssemblyFromSource(Settings, CodeBlock)

                'If Assembly error = true then
                'Return Errors
                'Else
                If Result.Errors.HasErrors = True Then

                    Speaker.Speak("There are some issues to be handled")

                    For Each ITEM In Result.Errors
                        Errors.Add(ITEM.ERRORTEXT)
                    Next
                Else
                    'Creates assembly
                    Dim objAssembly As System.Reflection.Assembly = Result.CompiledAssembly

                    Dim objTheClass As Object = objAssembly.CreateInstance(iClassName)
                    'CheckInstance
                    If objTheClass Is Nothing Then
                        '     MsgBox("Can't load class...MainClass")
                        Speaker.Speak("Error i Can't load class, " & iClassName)
                        Errors.Add("Can't load class..." & iClassName)
                        Exit Sub
                    End If
                    'Trys to execute
                    Try
                        objTheClass.GetType.InvokeMember(iMethodName,
                            System.Reflection.BindingFlags.InvokeMethod, Nothing, objTheClass, Nothing)
                    Catch ex As Exception
                        '  MsgBox("Error:" & ex.Message)
                        Speaker.Speak("Error i Can't load Main Function, " & iMethodName)
                        Errors.Add("Error i Can't load Main Function, " & iMethodName)
                        Speaker.Speak("Woops")
                    End Try
                End If

            Case "DLL"
                Speaker.Speak("Create Library")
                Dim S As New SaveFileDialog
                With S
                    .Filter = "Executable (*.Dll)|*.Dll"
                    If (.ShowDialog() = DialogResult.OK) Then
                        ExecuteableName = .FileName
                    End If
                End With
                'Library type options : /target:library, /target:win, /target:winexe
                'Generates an executable instead of a class library.
                'Compiles in memory.
                Settings.GenerateInMemory = False
                Settings.GenerateExecutable = False
                Settings.CompilerOptions = " /target:library"
                'Set the assembly file name / path
                Settings.OutputAssembly = ExecuteableName
                'Read the documentation for the result again variable.
                'Calls the CompileAssemblyFromSource that will compile the specified source code using the parameters specified in the settings variable.
                Result = Compiler.CompileAssemblyFromSource(Settings, CodeBlock)
        End Select
#End Region
#Region "Errors"
        'Determines if we have any errors when compiling if so loops through all of the CompileErrors in the Reults variable and displays their ErrorText property.
        If (Result.Errors.Count <> 0) Then
            Speaker.Speak("Error Exception")
            Errors.Add("Exception Occured!")

            '  MessageBox.Show("Exception Occured!", "Whoops!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each E As CodeDom.Compiler.CompilerError In Result.Errors

                Errors.Add(E.ErrorText)

            Next
        ElseIf (Result.Errors.Count = 0) And CompileType = "EXE" Or CompileType = "DLL" Then
            Speaker.Speak("file saved " & ExecuteableName)
            Errors.Add("file saved at " & ExecuteableName)

        End If
#End Region

    End Sub

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
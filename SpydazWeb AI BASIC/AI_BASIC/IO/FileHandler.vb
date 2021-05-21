Imports System.IO
Imports System.Windows.Forms

Namespace IO
    Namespace Files
        Public Class FileHandler
            Public Sub SaveFile(ByRef Str As String)
                Dim ofd As New SaveFileDialog
                ofd.Filter = "CodeText files (*.*)|*.*"
                ofd.InitialDirectory = Application.StartupPath
                If ofd.ShowDialog = DialogResult.OK Then
                    Dim fstr = ofd.FileName
                    WriteFile(fstr, Str)
                Else
                End If
            End Sub
            Public Function OpenFile(ByRef Str As String) As String
                Dim ofd As New OpenFileDialog
                ofd.Filter = "CodeText files (*.*)|*.*"
                ofd.InitialDirectory = Application.StartupPath
                If ofd.ShowDialog = DialogResult.OK Then
                    Dim fstr = ofd.FileName
                    ReadFiletoText(fstr)
                Else
                End If
            End Function
            Public Function ReadFileTolines(ByRef FilePath As String) As List(Of String)
                ' Open the file to read from.
                Dim readText() As String = File.ReadAllLines(FilePath)
                Return readText.ToList
            End Function
            Public Function ReadFiletoText(ByRef FilePath As String) As String
                ' Open the file to read from.
                Dim readText As String = File.ReadAllText(FilePath)
                Return readText
            End Function
            Public Function ReadList(ByRef List As String) As List(Of String)
                ' Open the file to read from.
                Dim readText() As String = List.Split(",")
                Return readText.ToList
            End Function
            Public Function WriteFile(ByRef FilePath As String, ByRef Str As String)
                ' Open the file to read from.
                Try
                    File.WriteAllText(FilePath, Str)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Function
        End Class
    End Namespace
End Namespace





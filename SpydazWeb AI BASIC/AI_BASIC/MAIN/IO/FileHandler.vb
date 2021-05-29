'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\IO\FileHandler.vb
'
' summary:	File handler class
'---------------------------------------------------------------------------------------------------

Imports System.IO
Imports System.Windows.Forms

Namespace IO
    Namespace Files
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A file handler. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Friend Class FileHandler

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Saves a file. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="Str">  [in,out] The string. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Opens a file. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            '''
            ''' <returns>   A String. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function OpenFile() As String
                Dim ofd As New OpenFileDialog
                ofd.Filter = "CodeText files (*.*)|*.*"
                ofd.InitialDirectory = Application.StartupPath
                If ofd.ShowDialog = DialogResult.OK Then
                    Dim fstr = ofd.FileName
                    Return ReadFiletoText(fstr)
                Else
                End If
                Return Nothing
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Reads file tolines. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="FilePath"> [in,out] Full pathname of the file. </param>
            '''
            ''' <returns>   The file tolines. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function ReadFileTolines(ByRef FilePath As String) As List(Of String)
                ' Open the file to read from.
                Dim readText() As String = File.ReadAllLines(FilePath)
                Return readText.ToList
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Reads fileto text. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="FilePath"> [in,out] Full pathname of the file. </param>
            '''
            ''' <returns>   The fileto text. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function ReadFiletoText(ByRef FilePath As String) As String
                ' Open the file to read from.
                Dim readText As String = File.ReadAllText(FilePath)
                Return readText
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Reads a list. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="List"> [in,out] The list. </param>
            '''
            ''' <returns>   The list. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function ReadList(ByRef List As String) As List(Of String)
                ' Open the file to read from.
                Dim readText() As String = List.Split(",")
                Return readText.ToList
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Writes a file. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="FilePath"> [in,out] Full pathname of the file. </param>
            ''' <param name="Str">      [in,out] The string. </param>
            '''
            ''' <returns>   . </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

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





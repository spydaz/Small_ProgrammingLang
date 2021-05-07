
Imports System.Web.Script.Serialization
Namespace SAL
    Public Module Ext
        <System.Runtime.CompilerServices.Extension()>
        Public Function SplitAtNewLine(input As String) As IEnumerable(Of String)
            Return input.Split({Environment.NewLine}, StringSplitOptions.None)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractLastChar(ByRef InputStr As String) As String
            ExtractLastChar = Right(InputStr, 1)
        End Function
        <System.Runtime.CompilerServices.Extension()>
        Public Function ExtractFirstChar(ByRef InputStr As String) As String
            ExtractFirstChar = Left(InputStr, 1)
        End Function

    End Module
End Namespace


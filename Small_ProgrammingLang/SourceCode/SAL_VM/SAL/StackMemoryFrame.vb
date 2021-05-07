Namespace SAL
    ''' <summary>
    ''' Memory frame for Variables 
    ''' </summary>
    Public Class StackMemoryFrame
        Public Structure Var
            Public Value As Integer
            Public VarNumber As String
        End Structure
        Public ReturnAddress As Integer
        Public Variables As List(Of Var)

        Public Sub New(ByRef ReturnAddress As Integer)
            ReturnAddress = ReturnAddress
            Variables = New List(Of Var)
        End Sub
        Public Function GetReturnAddress() As Integer

            Return ReturnAddress
        End Function
        Public Function GetVar(ByRef VarName As String) As Integer
            For Each item In Variables
                If item.VarNumber = VarName Then
                    Return item.Value

                End If
            Next
            Return 0
        End Function
        Public Sub SetVar(ByRef VarName As String, ByRef value As Object)
            Dim item As New Var
            item.VarNumber = VarName
            item.Value = value

            Variables.Add(item)
        End Sub
    End Class
End Namespace


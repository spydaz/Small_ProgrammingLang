Public Class Lowering


    Public Sub _for(ByRef Ident As String, ByRef _start As Integer, ByRef _End As Integer, ByRef Body As String)
        Dim C_Syntax = "For (Int() " & Ident & " = " & _start & "; " & Ident & " < " & _End & "; ++" & Ident & ") {   " & Body & "}"
    End Sub

End Class



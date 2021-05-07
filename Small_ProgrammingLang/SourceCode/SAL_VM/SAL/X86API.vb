Imports SDK.SAL

Namespace SAL
    Public Class X86API
        Public Shared Function RunMachineCode(ByRef Code As String) As String
            Code = UCase(Code)
            Dim PROG() As String = Split(Code.Replace(vbCrLf, " "), " ")
            Dim InstructionLst As New List(Of String)
            Dim ROOT As New TreeNode
            ROOT.Text = "ASSEMBLY_CODE"
            Dim Count As Integer = 0
            For Each item In PROG
                Count += 1
                If item <> "" Then
                    Dim NDE As New TreeNode
                    NDE.Text = Count & ": " & item
                    ROOT.Nodes.Add(NDE)
                    InstructionLst.Add(item)
                End If
            Next
            'cpu - START



            Dim CPU As ZX81_CPU = New ZX81_CPU("Test", InstructionLst)
            CPU.RUN()

            Tree = ROOT
            Return "CURRENT POINTER = " & CPU.Get_Instruction_Pointer_Position & vbCr & "CONTAINED DATA = " & CPU.Peek


        End Function
        Public Shared Tree As New TreeNode
    End Class
End Namespace

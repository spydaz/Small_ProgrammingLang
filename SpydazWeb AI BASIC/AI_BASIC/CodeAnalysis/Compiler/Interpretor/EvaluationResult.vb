Imports AI_BASIC.Diagnostics

Public NotInheritable Class EvaluationResult
    Public ReadOnly Property Diagnostics As CompilerDiagnosticResults
    Public ReadOnly Property Result As Object

    Public Sub New(diagnostics As CompilerDiagnosticResults, result As Object)
        If diagnostics Is Nothing Then
            Throw New ArgumentNullException(NameOf(diagnostics))
        End If

        If result Is Nothing Then
            Throw New ArgumentNullException(NameOf(result))
        End If

        Me.Diagnostics = diagnostics
        Me.Result = result
    End Sub
End Class

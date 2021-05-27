Imports AI_BASIC.CodeAnalysis.Diagnostics

Friend Module Globals
    Private iGeneralException As List(Of DiagnosticsException)
    Public Property GeneralException As List(Of DiagnosticsException)
        Get
            Return iGeneralException
        End Get
        Set(value As List(Of DiagnosticsException))
            iGeneralException = value
        End Set
    End Property
End Module

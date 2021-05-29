Imports System.Runtime.InteropServices
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
    Public Class Win32
        <DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean

        End Function
        <DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean

        End Function

    End Class
End Module

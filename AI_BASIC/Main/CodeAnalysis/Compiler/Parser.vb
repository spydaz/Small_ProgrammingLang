Namespace CodeAnalysis
    Namespace Compiler
        Public Class Parser
            Private _Script As String

            Public Sub New(ByRef Script As String)

                If Script Is Nothing Then
                    Throw New ArgumentNullException(NameOf(Script))
                End If
                Me._Script = Script
            End Sub

            Public Function Parse() As SyntaxTree
                Return Nothing
            End Function
        End Class
    End Namespace
End Namespace


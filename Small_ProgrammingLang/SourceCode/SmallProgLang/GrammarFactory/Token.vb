Imports System.Runtime.InteropServices
Imports System.Web.Script.Serialization
Imports SDK.SmallProgLang.GrammarFactory.Grammar

Namespace SmallProgLang
    Namespace GrammarFactory


        ''' <summary>
        ''' Token to be returned 
        ''' </summary>
        Public Structure Token
            ''' <summary>
            ''' Simple identifier
            ''' </summary>
            Public ID As Type_Id
            ''' <summary>
            ''' Held Data
            ''' </summary>
            Public Value As String
            ''' <summary>
            ''' Start of token(Start position)
            ''' </summary>
            Public _start As Integer
            ''' <summary>
            ''' End of token (end Position)
            ''' </summary>
            Public _End As Integer

            Public Function ToJson() As String
                Dim Converter As New JavaScriptSerializer
                Return Converter.Serialize(Me)

            End Function
        End Structure
    End Namespace
End Namespace
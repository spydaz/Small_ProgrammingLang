Imports System.Text
Imports System.Web.Script.Serialization

Imports AI_BASIC.Syntax

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Environment
            Public Class EnvironmentalMemory
                Public LocalMemory As List(Of Variable)
                Public Structure Variable
                    Public name As String
                    Public Type As LiteralType
                    Private ivalue As Object

                    Public ReadOnly Property Value As Object
                        Get
                            Return ivalue
                        End Get
                    End Property
                    Public Sub New(name As String, value As Object, type As LiteralType)
                        If name Is Nothing Then
                            Throw New ArgumentNullException(NameOf(name))
                        End If

                        If value Is Nothing Then
                            Throw New ArgumentNullException(NameOf(value))
                        End If

                        Me.name = name
                        Me.ivalue = value
                        Me.Type = type
                    End Sub
                    Public Sub Update(ByRef nValue As Object)
                        Select Case Me.Type
                            Case LiteralType._Boolean
                                Dim x As Boolean = nValue
                                UpdateValue(x)
                            Case LiteralType._String
                                Dim x As String = nValue
                                UpdateValue(x)
                            Case LiteralType._Integer
                                Dim x As Integer = nValue
                                UpdateValue(x)
                            Case LiteralType._Decimal
                                Dim x As Decimal = nValue
                                UpdateValue(x)
                            Case LiteralType._Date
                                Dim x As Date = nValue
                                UpdateValue(x)
                        End Select
                    End Sub
                    Private Sub UpdateValue(ByRef NewBoolean As Boolean)
                        Me.ivalue = New Boolean = NewBoolean
                    End Sub
                    Private Sub UpdateValue(ByRef NewInteger As Integer)
                        Me.ivalue = New Integer = NewInteger
                    End Sub
                    Private Sub UpdateValue(ByRef NewDate As Date)
                        Me.ivalue = New Date = NewDate
                    End Sub
                    Private Sub UpdateValue(ByRef NewDecimal As Decimal)
                        Me.ivalue = New Decimal = NewDecimal
                    End Sub
                    Private Sub UpdateValue(ByRef NewString As String)
                        Me.ivalue = NewString
                    End Sub
                    ''' <summary>
                    ''' Formatted json
                    ''' </summary>
                    ''' <returns> </returns>
                    Public Function ToJson() As String
                        Return FormatJsonOutput(ToString)
                    End Function
                    ''' <summary>
                    ''' Inline json
                    ''' </summary>
                    ''' <returns></returns>
                    Public Overrides Function ToString() As String
                        Dim Converter As New JavaScriptSerializer
                        Return Converter.Serialize(Me)
                    End Function
                    ''' <summary>
                    ''' Formats the output of the json parsed
                    ''' </summary>
                    ''' <param name="jsonString"></param>
                    ''' <returns></returns>
                    Private Function FormatJsonOutput(ByVal jsonString As String) As String
                        Dim stringBuilder = New StringBuilder()
                        Dim escaping As Boolean = False
                        Dim inQuotes As Boolean = False
                        Dim indentation As Integer = 0

                        For Each character As Char In jsonString

                            If escaping Then
                                escaping = False
                                stringBuilder.Append(character)
                            Else

                                If character = "\"c Then
                                    escaping = True
                                    stringBuilder.Append(character)
                                ElseIf character = """"c Then
                                    inQuotes = Not inQuotes
                                    stringBuilder.Append(character)
                                ElseIf Not inQuotes Then

                                    If character = ","c Then
                                        stringBuilder.Append(character)
                                        stringBuilder.Append(vbCrLf)
                                        stringBuilder.Append(vbTab, indentation)
                                    ElseIf character = "["c OrElse character = "{"c Then
                                        stringBuilder.Append(character)
                                        stringBuilder.Append(vbCrLf)
                                        stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                                    ElseIf character = "]"c OrElse character = "}"c Then
                                        stringBuilder.Append(vbCrLf)
                                        stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                                        stringBuilder.Append(character)
                                    ElseIf character = ":"c Then
                                        stringBuilder.Append(character)
                                        stringBuilder.Append(vbTab)
                                    ElseIf Not Char.IsWhiteSpace(character) Then
                                        stringBuilder.Append(character)
                                    End If
                                Else
                                    stringBuilder.Append(character)
                                End If
                            End If
                        Next

                        Return stringBuilder.ToString()
                    End Function

                End Structure
                Private mGlobalMemory As EnvironmentalMemory
                ''' <summary>
                ''' Global memeory passed in from parent environment
                ''' </summary>
                ''' <returns></returns>
                Public ReadOnly Property GlobalMemory As EnvironmentalMemory
                    Get
                        Return mGlobalMemory
                    End Get
                End Property
                ''' <summary>
                ''' Has no Global Memory
                ''' </summary>
                Public Sub New()
                    LocalMemory = New List(Of Variable)
                End Sub
                Public Sub New(ByRef GlobalMemory As EnvironmentalMemory)
                    LocalMemory = New List(Of Variable)
                    Me.mGlobalMemory = GlobalMemory

                End Sub
                Public Function AssignValue(ByRef Name As String, ByRef Val As Object) As Boolean
                    Dim x As Variable
                    If CheckIfExists(Name) = True Then
                        x = GetVar(Name)
                        LocalMemory.Remove(x)
                        DefineValue(x.name, x.Type, Val)
                        Return True
                    End If
                    Return False
                End Function
                Public Function GetVar(ByRef Name As String) As Variable
                    For Each item In LocalMemory
                        If item.name = Name Then

                            Return item
                        End If
                    Next
                    Return Nothing
                End Function
                Public Function CheckIfExists(ByRef Name As String) As Boolean
                    For Each item In LocalMemory
                        If item.name = Name Then
                            Return True
                        End If
                    Next
                    Return False
                End Function
                Public Function DefineValue(ByRef Name As String, ByRef Type As LiteralType, ByRef Val As Object) As Boolean
                    If CheckIfExists(Name) = False Then
                        LocalMemory.Add(New Variable(Name, Val, Type))
                        Return True
                    End If
                    Return False
                End Function

                Public Function DefineValue(ByRef Name As String, ByRef Type As LiteralType) As Boolean
                    Dim val As Object = Nothing
                    If CheckIfExists(Name) = False Then
                        Select Case Type
                            Case LiteralType._Boolean
                                val = False
                            Case LiteralType._String
                                val = " "
                            Case LiteralType._Array
                                Exit Select
                            Case LiteralType._Integer
                                val = New Integer = 0
                            Case LiteralType._Decimal
                                val = New Decimal = 0
                            Case LiteralType._Date
                                val = New Date = Date.Now
                            Case LiteralType._NULL
                                val = Nothing
                        End Select


                        LocalMemory.Add(New Variable(Name, Val, Type))
                        Return True
                    End If
                    Return False
                End Function



                Public Function GetVarType(ByRef Name As String) As LiteralType
                    If CheckIfExists(Name) = True Then
                        Dim ivar = GetVar(Name)
                        Return ivar.Type
                    End If
                    If GlobalMemory IsNot Nothing Then
                        If GlobalMemory.CheckIfExists(Name) = True Then
                            Dim ivar = GetVar(Name)
                            Return ivar.Type
                        End If
                    Else
                        'THIS IS THE GLOBAL MEMORY
                    End If
                    Return LiteralType._NULL
                End Function
                Public Function GetVarValue(ByRef Name As String) As Object
                    If CheckIfExists(Name) = True Then
                        Dim ivar = GetVar(Name)
                        Return ivar.Value
                    End If
                    If GlobalMemory IsNot Nothing Then
                        If GlobalMemory.CheckIfExists(Name) = True Then
                            Dim ivar = GetVar(Name)
                            Return ivar.Value
                        End If
                    Else
                        'THIS IS THE GLOBAL MEMORY
                    End If
                    Return "Unknown Variable? :"
                End Function
                ''' <summary>
                ''' Formatted json
                ''' </summary>
                ''' <returns> </returns>
                Public Function ToJson() As String
                    Return FormatJsonOutput(ToString)
                End Function
                ''' <summary>
                ''' Inline json
                ''' </summary>
                ''' <returns></returns>
                Public Overrides Function ToString() As String
                    Dim Converter As New JavaScriptSerializer
                    Return Converter.Serialize(Me)
                End Function
                ''' <summary>
                ''' Formats the output of the json parsed
                ''' </summary>
                ''' <param name="jsonString"></param>
                ''' <returns></returns>
                Private Function FormatJsonOutput(ByVal jsonString As String) As String
                    Dim stringBuilder = New StringBuilder()
                    Dim escaping As Boolean = False
                    Dim inQuotes As Boolean = False
                    Dim indentation As Integer = 0

                    For Each character As Char In jsonString

                        If escaping Then
                            escaping = False
                            stringBuilder.Append(character)
                        Else

                            If character = "\"c Then
                                escaping = True
                                stringBuilder.Append(character)
                            ElseIf character = """"c Then
                                inQuotes = Not inQuotes
                                stringBuilder.Append(character)
                            ElseIf Not inQuotes Then

                                If character = ","c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, indentation)
                                ElseIf character = "["c OrElse character = "{"c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, System.Threading.Interlocked.Increment(indentation))
                                ElseIf character = "]"c OrElse character = "}"c Then
                                    stringBuilder.Append(vbCrLf)
                                    stringBuilder.Append(vbTab, System.Threading.Interlocked.Decrement(indentation))
                                    stringBuilder.Append(character)
                                ElseIf character = ":"c Then
                                    stringBuilder.Append(character)
                                    stringBuilder.Append(vbTab)
                                ElseIf Not Char.IsWhiteSpace(character) Then
                                    stringBuilder.Append(character)
                                End If
                            Else
                                stringBuilder.Append(character)
                            End If
                        End If
                    Next

                    Return stringBuilder.ToString()
                End Function

            End Class
        End Namespace
    End Namespace
End Namespace

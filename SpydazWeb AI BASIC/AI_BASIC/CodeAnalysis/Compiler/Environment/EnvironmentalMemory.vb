Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.Syntax
Namespace CodeAnalysis
    Namespace Compiler
        Namespace Environment
            <DebuggerDisplay("{GetDebuggerDisplay(),nq}")>
            Public Class EnvironmentalMemory
                ''' <summary>
                ''' Structure for variable
                ''' </summary>
                Public Structure Variable
                    ''' <summary>
                    ''' Variabel name
                    ''' </summary>
                    Public Name As String
                    ''' <summary>
                    ''' Value of variable
                    ''' </summary>
                    Public Value As Object
                    ''' <summary>
                    ''' Type ass string identifier
                    ''' </summary>
                    Public Type As LiteralType
                End Structure
                ''' <summary>
                ''' Memory for variables
                ''' </summary>
                Private LocalMemory As List(Of Variable)
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
                ''' A global memeory is contained within environmemt for referencing
                ''' </summary>
                ''' <param name="GlobalMemory"></param>
                Public Sub New(ByRef GlobalMemory As EnvironmentalMemory)
                    LocalMemory = New List(Of Variable)
                    Me.mGlobalMemory = GlobalMemory

                End Sub
                Private Function _AddInternalLiterals() As List(Of Variable)
                    Dim Lst As New List(Of Variable)
                    Dim BTrue As New Variable
                    BTrue.Name = "TRUE"
                    BTrue.Value = True
                    BTrue.Type = LiteralType._Boolean
                    Lst.Add(BTrue)
                    Dim BFalse As New Variable
                    BFalse.Name = "FALSE"
                    BFalse.Value = False
                    BFalse.Type = LiteralType._Boolean
                    Lst.Add(BFalse)
                    Dim BNull As New Variable
                    BNull.Name = "NULL"
                    BNull.Value = Nothing
                    BNull.Type = LiteralType._NULL
                    Lst.Add(BNull)
                    Return Lst
                End Function
                ''' <summary>
                ''' Has no Global Memory
                ''' </summary>
                Public Sub New()
                    LocalMemory = _AddInternalLiterals()
                    '  mGlobalMemory = New EnvironmentalMemory


                    '  Me.GlobalMemory = GlobalMemory
                End Sub
                ''' <summary>
                ''' Defines variable in environemnt if not previoulsy exisiting
                ''' </summary>
                ''' <param name="Name">Variable name</param>
                ''' <param name="nType">stype such as string/ integer etc</param>
                ''' <returns></returns>
                Public Function Define(ByRef Name As String, nType As LiteralType) As Object
                    Dim detected As Boolean = False
                    Dim var As New Variable
                    var.Name = Name
                    var.Type = nType
                    If Me.GetVar(Name) = "NULL" Then
                        LocalMemory.Add(var)
                        Return True
                    End If
                    Return "ERROR : Unable to define Variable Name: -: -" & Name & " -: -:Exists Previously in memory"
                End Function
                ''' <summary>
                ''' Assigns a value to the variable
                ''' </summary>
                ''' <param name="Name">Variable name(Previoulsy Exisiting)</param>
                ''' <param name="Value">Value to be stored</param>
                ''' <returns></returns>
                Public Function AssignValue(ByRef Name As String, ByRef Value As Object) As Object
                    For Each item In LocalMemory
                        If item.Name = Name Then
                            'IN LOCAL
                            item.Value = Value
                            Return True
                        End If
                    Next
                    If GlobalMemory IsNot Nothing Then
                        For Each item In GlobalMemory.LocalMemory
                            If item.Name = Name Then
                                '                    'Found in Global
                                item.Value = Value
                                Return True
                            End If
                        Next
                    Else
                        'THIS IS THE GLOBAL MEMORY
                    End If
                    Return "ERROR : Unable to Assign Value  :=" & Value & " -: Not in Memory :- Variable Unknown :=" & Name
                End Function
                ''' <summary>
                ''' Returns the value from the stored variable
                ''' </summary>
                ''' <param name="Name"></param>
                ''' <returns></returns>
                Public Function GetVar(ByRef Name As String) As Object
                    For Each item In LocalMemory
                        If item.Name = Name Then
                            'Found in Local Memeory
                            Return item.Value
                        End If
                    Next
                    If GlobalMemory IsNot Nothing Then

                        If GlobalMemory.GetVar(Name) <> "NULL" Then
                            Return GlobalMemory.GetVar(Name)
                        End If

                    Else
                        'THIS IS THE GLOBAL MEMORY
                    End If
                    Return "NULL"

                End Function
                Public Function GetVarType(ByRef Name As String) As LiteralType
                    For Each item In LocalMemory
                        If item.Name = Name Then
                            'Found in Local Memeory
                            Return item.Type
                        End If
                    Next
                    If GlobalMemory IsNot Nothing Then

                        For Each item In GlobalMemory.LocalMemory
                            If item.Name = Name Then
                                'Found in GlobalMemory
                                Return item.Type
                            End If
                        Next



                    Else
                        'THIS IS THE GLOBAL MEMORY
                    End If
                    Return LiteralType._NULL

                End Function
                Public Function CheckVar(ByRef VarName As String) As Boolean
                    For Each item In LocalMemory
                        If item.Name = VarName Then
                            Return True
                        End If
                    Next
                    CheckVar = False
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
                Private Function GetDebuggerDisplay() As String
                    Return ToString()
                End Function
            End Class
        End Namespace
    End Namespace
End Namespace

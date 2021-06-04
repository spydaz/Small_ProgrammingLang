'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\CodeAnalysis\Compiler\Environment\EnvironmentalMemory.vb
'
' summary:	Environmental memory class
'---------------------------------------------------------------------------------------------------

Imports System.Text
Imports System.Web.Script.Serialization
Imports AI_BASIC.CodeAnalysis.Diagnostics
Imports AI_BASIC.Syntax
Imports AI_BASIC.Typing

Namespace CodeAnalysis
    Namespace Compiler
        Namespace Environment

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   An environmental memory. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Class EnvironmentalMemory

#Region "Symbol Object Types"
                Public Interface MemorySymbol
                    ''' <summary>   The name. </summary>
                    Property name As String
                    ''' <summary>   The type. </summary>
                    Property Type As LiteralType
                    Property TypeStr As String
                End Interface
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   A variable. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Class Variable
                    Implements MemorySymbol

                    ''' <summary>   The ivalue. </summary>
                    Private ivalue As Object

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Gets the value. </summary>
                    '''
                    ''' <value> The value. </value>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Public ReadOnly Property Value As Object
                        Get
                            Return ivalue
                        End Get
                    End Property
                    Private iname As String
                    Public Property name As String Implements MemorySymbol.name
                        Get
                            Return iname
                        End Get
                        Set(value As String)
                            iname = value
                        End Set
                    End Property
                    Private itype As LiteralType
                    Public Property Type As LiteralType Implements MemorySymbol.Type
                        Get
                            Return itype
                        End Get
                        Set(value As LiteralType)
                            itype = value
                        End Set
                    End Property
                    Private iTypeStr As String = ""
                    Public Property TypeStr As String Implements MemorySymbol.TypeStr
                        Get
                            Return iTypeStr
                        End Get
                        Set(value As String)
                            iTypeStr = value
                        End Set
                    End Property

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Constructor. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
                    '''                                             null. </exception>
                    '''
                    ''' <param name="name">     The name. </param>
                    ''' <param name="value">    The value. </param>
                    ''' <param name="type">     The type. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Public Sub New(name As String, value As Object, type As LiteralType)
                        If name Is Nothing Then
                            GeneralException.Add(New DiagnosticsException("Unable to register memory " & NameOf(name), ExceptionType.NullRefferenceError, NameOf(name), SyntaxType._String))

                        End If

                        If value Is Nothing Then
                            GeneralException.Add(New DiagnosticsException("Unable to register memory " & NameOf(name), ExceptionType.NullRefferenceError, NameOf(name), SyntaxType._String))

                        End If

                        Me.name = name
                        Me.ivalue = value
                        Me.Type = type
                        Me.TypeStr = type.GetLiteralTypeStr
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the given nValue. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="nValue">   [in,out] The value. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

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

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the value described by NewString. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="NewBoolean">   [in,out] True to new boolean. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Private Sub UpdateValue(ByRef NewBoolean As Boolean)
                        Me.ivalue = New Boolean = NewBoolean
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the value described by NewString. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="NewInteger">   [in,out] The new integer. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Private Sub UpdateValue(ByRef NewInteger As Integer)
                        Me.ivalue = New Integer = NewInteger
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the value described by NewString. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="NewDate">  [in,out] The new date. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Private Sub UpdateValue(ByRef NewDate As Date)
                        Me.ivalue = New Date = NewDate
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the value described by NewString. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="NewDecimal">   [in,out] The new decimal. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Private Sub UpdateValue(ByRef NewDecimal As Decimal)
                        Me.ivalue = New Decimal = NewDecimal
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Updates the value described by NewString. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="NewString">    [in,out] The new string. </param>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Private Sub UpdateValue(ByRef NewString As String)
                        Me.ivalue = NewString
                    End Sub

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Converts this  to a JSON. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <returns>   This  as a String. </returns>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Public Function ToJson() As String
                        Return FormatJsonOutput(ToString)
                    End Function

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Returns the fully qualified type name of this instance. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <returns>   The fully qualified type name. </returns>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

                    Public Overrides Function ToString() As String
                        Dim Converter As New JavaScriptSerializer
                        Return Converter.Serialize(Me)
                    End Function

                    '''////////////////////////////////////////////////////////////////////////////////////////////////////
                    ''' <summary>   Format JSON output. </summary>
                    '''
                    ''' <remarks>   Leroy, 27/05/2021. </remarks>
                    '''
                    ''' <param name="jsonString">   The JSON string. </param>
                    '''
                    ''' <returns>   The formatted JSON output. </returns>
                    '''////////////////////////////////////////////////////////////////////////////////////////////////////

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
#End Region
#Region "Propertys"

                ''' <summary>   The local memory. </summary>
                Public LocalMemory As List(Of MemorySymbol)
                ''' <summary>   The global memory. </summary>
                Private mGlobalMemory As EnvironmentalMemory

                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets the global memory. </summary>
                '''
                ''' <value> The global memory. </value>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public ReadOnly Property GlobalMemory As EnvironmentalMemory
                    Get
                        Return mGlobalMemory
                    End Get
                End Property
#End Region
#Region "Constructor"
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Initializes a new instance of the <see cref="T:System.Object" /> class. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Sub New()
                    LocalMemory = New List(Of MemorySymbol)
                End Sub
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Has no Global Memory. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="GlobalMemory"> [in,out] Global memeory passed in from parent environment. </param>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Sub New(ByRef GlobalMemory As EnvironmentalMemory)
                    LocalMemory = New List(Of MemorySymbol)
                    Me.mGlobalMemory = GlobalMemory

                End Sub
#End Region
#Region "VARS"
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Assign value. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                ''' <param name="Val">  [in,out] The value. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets a variable. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                '''
                ''' <returns>   The variable. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function GetVar(ByRef Name As String) As Variable
                    For Each item In LocalMemory
                        If item.name = Name Then

                            Return item
                        End If
                    Next
                    Return Nothing
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Determine if exists. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function CheckIfExists(ByRef Name As String) As Boolean
                    For Each item In LocalMemory
                        If item.name = Name Then
                            Return True
                        End If
                    Next
                    Return False
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Define value. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                ''' <param name="Type"> [in,out] The type. </param>
                ''' <param name="Val">  [in,out] The value. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function DefineValue(ByRef Name As String, ByRef Type As LiteralType, ByRef Val As Object) As Boolean
                    If CheckIfExists(Name) = False Then
                        LocalMemory.Add(New Variable(Name, Val, Type))
                        Return True
                    End If
                    Return False
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Define value. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                ''' <param name="Type"> [in,out] The type. </param>
                '''
                ''' <returns>   True if it succeeds, false if it fails. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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


                        LocalMemory.Add(New Variable(Name, val, Type))
                        Return True
                    End If
                    Return False
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets variable type. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                '''
                ''' <returns>   The variable type. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Gets variable value. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="Name"> [in,out] The name. </param>
                '''
                ''' <returns>   The variable value. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
#End Region
#Region "TOSTRING"
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Converts this  to a JSON. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   This  as a String. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Function ToJson() As String
                    Return FormatJsonOutput(ToString)
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Returns a string that represents the current object. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <returns>   A string that represents the current object. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                Public Overrides Function ToString() As String
                    Dim Converter As New JavaScriptSerializer
                    Return Converter.Serialize(Me)
                End Function
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
                ''' <summary>   Format JSON output. </summary>
                '''
                ''' <remarks>   Leroy, 27/05/2021. </remarks>
                '''
                ''' <param name="jsonString">   The JSON string. </param>
                '''
                ''' <returns>   The formatted JSON output. </returns>
                '''////////////////////////////////////////////////////////////////////////////////////////////////////
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
#End Region
            End Class
        End Namespace
    End Namespace
End Namespace

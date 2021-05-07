Namespace SmallProgLang
    Namespace Evaluator
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
                Public Type As Ast_ExpressionFactory.AST_NODE
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
                BTrue.Type = Ast_ExpressionFactory.AST_NODE._boolean
                Lst.Add(BTrue)
                Dim BFalse As New Variable
                BFalse.Name = "FALSE"
                BFalse.Value = False
                BFalse.Type = Ast_ExpressionFactory.AST_NODE._boolean
                Lst.Add(BFalse)
                Dim BNull As New Variable
                BNull.Name = "NULL"
                BNull.Value = Nothing
                BNull.Type = Ast_ExpressionFactory.AST_NODE._null
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
            Public Function Define(ByRef Name As String, nType As String) As Object
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
            Public Function CheckVar(ByRef VarName As String) As Boolean
                For Each item In LocalMemory
                    If item.Name = VarName Then
                        Return True
                    End If
                Next
                CheckVar = False
            End Function
        End Class
    End Namespace
End Namespace


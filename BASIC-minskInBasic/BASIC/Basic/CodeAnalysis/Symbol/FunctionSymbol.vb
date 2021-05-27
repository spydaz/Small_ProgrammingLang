'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Symbol\FunctionSymbol.vb
'
' summary:	Function symbol class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.Collections.Immutable
Imports System.Reflection
Imports Basic.CodeAnalysis.Syntax

Namespace Global.Basic.CodeAnalysis.Symbols

    Friend Module BuiltinFunctions

#If BASIC Then
    Public ReadOnly Hex As New FunctionSymbol("HEX$", ImmutableArray.Create(New ParameterSymbol("num", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly Oct As New FunctionSymbol("OCT$", ImmutableArray.Create(New ParameterSymbol("num", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly Asc As New FunctionSymbol("ASC", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String)), TypeSymbol.Int)
    Public ReadOnly Chr As New FunctionSymbol("CHR$", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly Instr As New FunctionSymbol("INSTR", ImmutableArray.Create(New ParameterSymbol("start", TypeSymbol.Int), New ParameterSymbol("string1", TypeSymbol.String), New ParameterSymbol("string2", TypeSymbol.String)), TypeSymbol.Int)
    Public ReadOnly LCase As New FunctionSymbol("LCASE$", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String)), TypeSymbol.String)
    Public ReadOnly Len As New FunctionSymbol("LEN", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String)), TypeSymbol.Int)
    Public ReadOnly MidFunction As New FunctionSymbol("MID$", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String), New ParameterSymbol("start", TypeSymbol.Int), New ParameterSymbol("num", TypeSymbol.int)), TypeSymbol.String)
    Public ReadOnly Right As New FunctionSymbol("RIGHT$", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String), New ParameterSymbol("num", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly Space As New FunctionSymbol("SPACE$", ImmutableArray.Create(New ParameterSymbol("num", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly Str As New FunctionSymbol("STR$", ImmutableArray.Create(New ParameterSymbol("num", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly StringFunction As New FunctionSymbol("STRING$", ImmutableArray.Create(New ParameterSymbol("num", TypeSymbol.Int), New ParameterSymbol("value", TypeSymbol.Int)), TypeSymbol.String)
    Public ReadOnly UCase As New FunctionSymbol("UCASE$", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String)), TypeSymbol.String)
    Public ReadOnly Val As New FunctionSymbol("VAL", ImmutableArray.Create(New ParameterSymbol("value", TypeSymbol.String)), TypeSymbol.Int)
#End If
        ''' <summary>   The print. </summary>
        Public ReadOnly Print As New FunctionSymbol("print", ImmutableArray.Create(New ParameterSymbol("text", TypeSymbol.Any, 0)), TypeSymbol.Void)
        ''' <summary>   The input. </summary>
        Public ReadOnly Input As New FunctionSymbol("input", ImmutableArray(Of ParameterSymbol).Empty, TypeSymbol.String)
        ''' <summary>   The random. </summary>
        Public ReadOnly Rnd As New FunctionSymbol("rnd", ImmutableArray.Create(New ParameterSymbol("max", TypeSymbol.Int, 0)), TypeSymbol.Int)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets all items in this collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>
        ''' An enumerator that allows foreach to be used to process all items in this collection.
        ''' </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Function GetAll() As IEnumerable(Of FunctionSymbol)
            Return GetType(BuiltinFunctions).GetFields(BindingFlags.Public Or BindingFlags.Static).
                                       Where(Function(f) f.FieldType = GetType(FunctionSymbol)).
                                       Select(Function(f) CType(f.GetValue(Nothing), FunctionSymbol))
        End Function

    End Module

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary> A function symbol. </summary>
    '''
    ''' <remarks> Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public NotInheritable Class FunctionSymbol
        Inherits Symbol

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="name">         The name. </param>
        ''' <param name="paremeters">   The paremeters. </param>
        ''' <param name="type">         The type. </param>
        ''' <param name="declaration">  (Optional) The declaration. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Sub New(name As String, paremeters As ImmutableArray(Of ParameterSymbol), type As TypeSymbol, Optional declaration As FunctionDeclarationSyntax = Nothing)
            MyBase.New(name)
            Parameters = paremeters
            Me.Type = type
            Me.Declaration = declaration
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the kind. </summary>
        '''
        ''' <value> The kind. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides ReadOnly Property Kind As SymbolKind = SymbolKind.Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the declaration. </summary>
        '''
        ''' <value> The declaration. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Declaration As FunctionDeclarationSyntax

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets options for controlling the operation. </summary>
        '''
        ''' <value> The parameters. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Parameters As ImmutableArray(Of ParameterSymbol)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the type. </summary>
        '''
        ''' <value> The type. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Type As TypeSymbol

    End Class

End Namespace
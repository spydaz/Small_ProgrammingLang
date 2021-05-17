﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Symbols

  Public NotInheritable Class ParameterSymbol
    Inherits LocalVariableSymbol

    Sub New(name As String, type As TypeSymbol, ordinal As Integer)
      MyBase.New(name, True, type, Nothing)
      Me.Ordinal = ordinal
    End Sub

    Public Overrides ReadOnly Property Kind As SymbolKind = SymbolKind.Parameter
    Public ReadOnly Property Ordinal As Integer

  End Class

End Namespace
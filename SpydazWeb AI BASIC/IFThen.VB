Public Function IF_THEN_ELSE(ByRef Condition As Boolean) As Boolean
    If Condition Then
        Dim A = ""
    Else
        Dim B = ""
    End If

    Return True
End Function
Public Function IF_THEN(ByRef Condition As Boolean) As Boolean
    If Condition Then
        Dim B = ""

    End If

    Return True
End Function

''' basically Select CaseCmd where Condition Is true
Public Function IF_THEN_ElseIf(ByRef Condition As Boolean, ElseIfCondition As Boolean) As Boolean
    If Condition Then
        Dim A = ""
    ElseIf ElseIfCondition = True Then
        Dim B = ""
    End If

    Return True
End Function
''' basically Select CaseCmd where Condition Is true and else
Public Function IF_THEN_ElseIfElse(ByRef Condition As Boolean, ElseIfCondition As Boolean) As Boolean
    If Condition Then
        Dim A = ""
    ElseIf ElseIfCondition = True Then
        Dim B = ""
    Else
        Dim C = ""
    End If

    Return True
End Function
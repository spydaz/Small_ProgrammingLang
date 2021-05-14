Imports System.Text
Imports System.Web.Script.Serialization
Imports SDK.SpydazWeb.AI.Basic.CodeAnalysis.Syntax
Imports SDK.SpydazWeb.AI.Basic.CodeAnalysis.Syntax.SyntaxNodes

Namespace SpydazWeb.AI.Basic
    Namespace CodeAnalysis
        Public Class Lexer
            Private _Script As String = ""
            Private _Position As Integer
            Private CurrentChar As Char
            Public _Diagnostics As New List(Of String)
            Public Property Diagnostics As List(Of String)
                Get
                    Return _Diagnostics

                End Get
                Set(value As List(Of String))
                    _Diagnostics = value
                End Set
            End Property

            Private ReadOnly Property Current As Char
                Get

                    If _Position > _Script.Length - 1 Then
                        Return "\"
                    Else
                        Return _Script(_Position)
                    End If

                End Get
            End Property
            Public Sub New(ByRef iScript As String)
                _Script = iScript
            End Sub
            Private Sub _Next()
                _Position += 1
            End Sub

            ''' <summary>
            ''' 
            ''' 
            '''-Numerical
            '''-WhiteSpace
            '''-Operators +-*/()
            ''' </summary>
            ''' <returns></returns>
            Public Function _NextToken() As SyntaxToken
                '-Numerical
                Dim _start As Integer = 0
                Dim _length As Integer = 0
                Dim _iText As String = ""
                If _Position >= _Script.Length Then
                    Return New SyntaxToken(SyntaxType.EOF, "EOF", _Position - 1, "EOF", Nothing)

                End If

                'Numerical
                If Char.IsDigit(Current) Then
                    'Capture StartPoint ForSlicer
                    _start = _Position
                    'UseInternal Lexer PER (CHAR)
                    While Char.IsDigit(Current)
                        'Iterate Forwards until end of digits
                        _Next()
                    End While
                    'get length 
                    _length = _Position - _start
                    'Get Slice
                    _iText = _Script.Substring(_start, _length)
                    Dim value As Integer = 0
                    If Integer.TryParse(_iText, value) = False Then
                        _Diagnostics.Add("The number is not recognized as integer:" & _iText)
                    End If

                    Return New SyntaxToken(SyntaxType.Numerical, "Numerical", _start, _iText, value)
                End If
                'WhiteSpace
                If Char.IsWhiteSpace(Current) Then
                    'Capture StartPoint ForSlicer
                    _start = _Position
                    'UseInternal Lexer PER (CHAR)
                    While Char.IsWhiteSpace(Current)
                        'Iterate Forwards until end of digits
                        _Next()
                    End While
                    'get length 
                    _length = _Position - _start
                    'Get Slice
                    _iText = _Script.Substring(_start, _length)


                    Return New SyntaxToken(SyntaxType.WhiteSpace, "WhiteSpace", _start, _iText, _iText)
                End If
                'Operators
                Select Case Current
                    Case ChrW(10), ChrW(13)
                        Return New SyntaxToken(SyntaxType.EOF, "EOF", _Position, "EOF", Nothing)

                    Case "+"
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Add_Operator, "Add_Operator", _Position - 1, "+", Nothing)

                    Case "-"
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Sub_Operator, "Sub_Operator", _Position - 1, "-", Nothing)

                    Case "*"
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Multiply_Operator, "Multiply_Operator", _Position - 1, "*", Nothing)

                    Case "/"
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Divide_Operator, "Divide_Operator", _Position - 1, "/", Nothing)
                    Case ")"
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Close_Parenthesise, "Close_Parenthesise", _Position - 1, ")", Nothing)

                    Case "("
                        _Position += 1
                        Return New SyntaxToken(SyntaxType.Open_Parenthesise, "Open_Parenthesise", _Position - 1, ")", Nothing)

                End Select


                _Position += 1
                _Diagnostics.Add("Unrecognized Character in input: '" & Current & "' at Position : " & _Position - 1)
                Return New SyntaxToken(SyntaxType.BadToken, "BadToken", _Position - 1, Current, Nothing)

            End Function
        End Class
        Public Class Parser
            Private _Tree As List(Of SyntaxToken)
            Private Script As String
            Private _Position As Integer = 0
            Public _Diagnostics As New List(Of String)
            Private ReadOnly Property CurrentToken As SyntaxToken
                Get
                    Return _Peek(0)
                End Get
            End Property
            Public Sub New(ByRef _Script As String)
                Script = _Script
                'AST TREE
                _Tree = New List(Of SyntaxToken)
                'LEXER
                Dim SAL_VB_ As New Lexer(_Script)
                'TOKEN
                Dim MyTok As SyntaxToken
                'CURRENT TOK
                MyTok = SAL_VB_._NextToken
                'iterate through all tokens until EOF
                Do While MyTok._SyntaxType <> SyntaxType.EOF
                    'Clean Tokens As the arrive in the Tree
                    If MyTok._SyntaxType <> SyntaxType.BadToken OrElse
                        MyTok._SyntaxType <> SyntaxType.WhiteSpace Then
                        _Tree.Add(MyTok)
                    End If


                    'Get And Check Next
                    MyTok = SAL_VB_._NextToken
                Loop
                _Tree.Add(New SyntaxToken(SyntaxType.EOF, "EOF", _Position, "EOF", Nothing))
            End Sub
            Private Function _Peek(ByVal offset As Integer) As SyntaxToken
                Dim index = _Position + offset
                If index >= _Tree.Count Then Return _Tree(_Tree.Count - 1)
                Return _Tree(index)
            End Function

            Public Function Parse() As Ast_SyntaxTree
                Return _ExpressionSyntaxTree()
            End Function

            Public Function _ExpressionSyntaxTree() As Ast_SyntaxTree
                Dim ExpressionLst As New List(Of Ast_ExpressionSyntaxNode)

                Dim Expression = _Expression()
                ExpressionLst.Add(Expression)
                Dim End_ofFileToken = _MatchToken(SyntaxType.EOF)

                Dim MyTree = New Ast_SyntaxTree(_Diagnostics, ExpressionLst, End_ofFileToken)
                Return MyTree
            End Function
            Public Function _Expression() As Ast_ExpressionSyntaxNode
                Return _AddativeExpression()
            End Function
            Public Function _GetNextToken() As SyntaxToken
                Dim iCurrentToken = CurrentToken
                _Position += 1
                Return iCurrentToken
            End Function
            Public Function _MatchToken(ByRef Expected As SyntaxType) As SyntaxToken
                Dim iCurrentToken = CurrentToken
                If iCurrentToken IsNot Nothing Then

                    If iCurrentToken._SyntaxType = Expected Then
                        'get Expected token
                        Return _GetNextToken()
                    Else
                        _Diagnostics.Add("Unrecognized Token in input: '" & iCurrentToken._SyntaxStr & "' at Position : " & _Position & vbNewLine &
                                     " Expected : " & Expected.ToString & vbNewLine)
                        'Generate Token (fakes)
                        Return New SyntaxToken(Expected, "Generated", _Position, "", "")
                    End If
                Else

                End If
                Return Nothing
            End Function
            Public Function _AddativeExpression() As Ast_ExpressionSyntaxNode
                Dim _left = _MultiplicativeExpression()
                Select Case CurrentToken._SyntaxType

                    Case SyntaxType.Add_Operator
                        While CurrentToken._SyntaxType = SyntaxType.Add_Operator


                            Dim _iOperator = _GetNextToken()
                            Dim _Right = _MultiplicativeExpression()
                            _left = New Ast_BinaryExpressionSyntax(_left, _iOperator, _Right)

                            Return _left
                        End While
                    Case SyntaxType.Sub_Operator
                        While CurrentToken._SyntaxType = SyntaxType.Sub_Operator
                            Dim _iOperator = _GetNextToken()
                            Dim _Right = _MultiplicativeExpression()
                            _left = New Ast_BinaryExpressionSyntax(_left, _iOperator, _Right)
                            Return _left
                        End While


                        Exit Select
                End Select
                Return _left
            End Function
            Public Function _MultiplicativeExpression() As Ast_ExpressionSyntaxNode
                Dim _left = _PrimaryExpression()
                Select Case CurrentToken._SyntaxType


                    Case SyntaxType.Multiply_Operator
                        While CurrentToken._SyntaxType = SyntaxType.Multiply_Operator
                            Dim _iOperator = _GetNextToken()
                            Dim _Right = _PrimaryExpression()
                            _left = New Ast_BinaryExpressionSyntax(_left, _iOperator, _Right)
                            Return _left
                        End While
                    Case SyntaxType.Divide_Operator
                        While CurrentToken._SyntaxType = SyntaxType.Divide_Operator
                            Dim _iOperator = _GetNextToken()
                            Dim _Right = _PrimaryExpression()
                            _left = New Ast_BinaryExpressionSyntax(_left, _iOperator, _Right)
                            Return _left
                        End While
                    Case Else

                        Exit Select
                End Select
                Return _left
            End Function
            Public Function _PrimaryExpression() As Ast_ExpressionSyntaxNode


                Dim nde As SyntaxToken

                Select Case CurrentToken._SyntaxType
                    Case SyntaxType.Numerical
                        'Number Expression
                        nde = _MatchToken(SyntaxType.Numerical)


                End Select

                Return New Ast_NumericExpressionSyntax(nde)
            End Function
        End Class
        Public Class Evaluator
            Public _tree As Ast_SyntaxTree
            Public Sub New(ByRef _itree As Ast_SyntaxTree)
                _tree = _itree

            End Sub

            Public Function _Evaluate() As Object
                For Each item In _tree.Syntax
                    Return _EvaluateExpresssion(item)
                Next
                Return "Evaluation Error"
            End Function

            Public Function _EvaluateExpresssion(ByRef iNode As Object) As Object

                If iNode.SyntaxType = SyntaxType.NumericalExpression Then

                    Return iNode.NumberToken._Value
                Else

                End If
                If iNode.SyntaxType = SyntaxType.BinaryExpression Then

                    Dim _Left As Integer = _EvaluateExpresssion(iNode._Left)
                    Dim _Right As Integer = _EvaluateExpresssion(iNode._Right)

                    Select Case iNode._Operator._SyntaxType
                        Case SyntaxType.Add_Operator
                            Return _Left + _Right
                        Case SyntaxType.Sub_Operator
                            Return _Left - _Right
                        Case SyntaxType.Multiply_Operator
                            Return _Left * _Right
                        Case SyntaxType.Divide_Operator
                            Return _Left / _Right
                        Case Else
                            Throw New Exception("Unexpected Binary Operator :" & iNode._Operator._SyntaxStr)
                    End Select

                Else

                End If

                Throw New Exception("Unexpected Expression :" & iNode.SyntaxTypeStr)
                Return 0
            End Function
        End Class
        Namespace Syntax
            Public Class SyntaxToken
                Public _Text As String
                Public _Position As Integer
                Public _SyntaxType As SyntaxType
                Public _SyntaxStr As String
                Public _Value As Object
                ''' <summary>
                ''' Serializes object to json
                ''' </summary>
                ''' <returns> </returns>
                Public Function ToJson() As String
                    Return FormatJsonOutput(ToString)
                End Function
                Public Overrides Function ToString() As String
                    Dim Converter As New JavaScriptSerializer
                    Return Converter.Serialize(Me)
                End Function
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
                Public Sub New(ByRef iSyntaxType As SyntaxType, ByRef iSyntaxTypeStr As String, ByRef iPosition As Integer, ByRef iText As String, ByRef iValue As Object)
                    Me._Position = iPosition
                    Me._Text = iText
                    Me._SyntaxType = iSyntaxType
                    Me._Value = iValue
                    Me._SyntaxStr = iSyntaxTypeStr
                End Sub
            End Class
            Public Enum SyntaxType
                Numerical = 1
                WhiteSpace = 0
                Operators = 10
                Add_Operator = 11
                Sub_Operator = 12
                Multiply_Operator = 13
                Divide_Operator = 14
                Open_Parenthesise = 101
                Close_Parenthesise = 102
                BadToken
                Expressions = 200
                NumericalExpression = 201
                BinaryExpression = 202
                EOF
            End Enum
            Namespace SyntaxNodes
                Public Class Ast_SyntaxTree
                    Public Syntax As List(Of Ast_ExpressionSyntaxNode)
                    Public Diagnostics As List(Of String)
                    Public EndofFileToken As SyntaxToken
                    ''' <summary>
                    ''' Serializes object to json
                    ''' </summary>
                    ''' <returns> </returns>
                    Public Function ToJson() As String
                        Return FormatJsonOutput(ToString)
                    End Function
                    Public Overrides Function ToString() As String
                        Dim Converter As New JavaScriptSerializer
                        Return Converter.Serialize(Me)
                    End Function
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
                    Public Sub New(ByRef iDiagnostics As List(Of String),
                               ByRef iSyntax As List(Of Ast_ExpressionSyntaxNode), ByRef iEndofFileToken As SyntaxToken)
                        Me.Syntax = iSyntax
                        Me.Diagnostics = iDiagnostics
                        Me.EndofFileToken = iEndofFileToken
                    End Sub
                    Public Shared Function Parse(ByRef Script As String) As Ast_SyntaxTree
                        Dim MyParser As New Parser(Script)
                        Return MyParser.Parse
                    End Function

                End Class
                Public MustInherit Class Ast_SyntaxNode
                    Public SyntaxType As SyntaxType
                    Public SyntaxTypeStr As String
                    Public MustOverride Function GetChildren() As List(Of SyntaxToken)

                    ''' <summary>
                    ''' Serializes object to json
                    ''' </summary>
                    ''' <returns> </returns>
                    Public Function ToJson() As String
                        Return FormatJsonOutput(ToString)
                    End Function
                    Public Overrides Function ToString() As String
                        Dim Converter As New JavaScriptSerializer
                        Return Converter.Serialize(Me)
                    End Function
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

                    Public Sub New(ByRef SyntaxType As SyntaxType)
                        Me.SyntaxType = SyntaxType
                    End Sub
                End Class
                Public MustInherit Class Ast_ExpressionSyntaxNode
                    Inherits Ast_SyntaxNode

                    Protected Sub New(ByRef SyntaxType As SyntaxType)
                        MyBase.New(SyntaxType)
                    End Sub
                End Class
                Public Class Ast_NumericExpressionSyntax
                    Inherits Ast_ExpressionSyntaxNode
                    Public NumberToken As SyntaxToken
                    Public Sub New(ByRef NumberToken As SyntaxToken)
                        MyBase.New(SyntaxType.NumericalExpression)
                        SyntaxTypeStr = "NumericalExpression"
                        Me.NumberToken = NumberToken
                    End Sub

                    Public Overrides Function GetChildren() As List(Of SyntaxToken)
                        Dim lst As New List(Of SyntaxToken)

                        lst.Add(NumberToken)
                        Return lst
                    End Function
                End Class
                Public Class Ast_BinaryExpressionSyntax
                    Inherits Ast_ExpressionSyntaxNode

                    Public Property _Left As Ast_ExpressionSyntaxNode
                    Public Property _Right As Ast_ExpressionSyntaxNode
                    Public Property _Operator As SyntaxToken

                    Public Sub New(ByRef _iLeft As Ast_ExpressionSyntaxNode,
                              ByRef _iOperator As SyntaxToken,
                             ByRef _iRight As Ast_ExpressionSyntaxNode)
                        MyBase.New(SyntaxType.BinaryExpression)
                        SyntaxTypeStr = "BinaryExpression"
                        _Left = _iLeft
                        _Right = _iRight
                        _Operator = _iOperator
                    End Sub

                    Public Overrides Function GetChildren() As List(Of SyntaxToken)
                        Dim lst As New List(Of SyntaxToken)
                        lst.AddRange(_Left.GetChildren)
                        lst.Add(_Operator)
                        lst.AddRange(_Right.GetChildren)

                        Return lst
                    End Function
                End Class
            End Namespace
        End Namespace

    End Namespace

End Namespace

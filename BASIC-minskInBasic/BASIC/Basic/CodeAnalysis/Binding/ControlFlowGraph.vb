'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\Binding\ControlFlowGraph.vb
'
' summary:	Control flow graph class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports System.IO
Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Syntax
Imports System.CodeDom.Compiler

Namespace Global.Basic.CodeAnalysis.Binding
    ''' <summary> . </summary>

    Friend NotInheritable Class ControlFlowGraph

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Constructor. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="start">            The start. </param>
        ''' <param name="![end]">           The [end]. </param>
        ''' <param name="As BasicBlock">    as basic block. </param>
        ''' <param name="blocks">           The blocks. </param>
        ''' <param name="branches">         The branches. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub New(start As BasicBlock, [end] As BasicBlock, blocks As List(Of BasicBlock), branches As List(Of BasicBlockBranch))
            Me.Start = start
            Me.End = [end]
            Me.Blocks = blocks
            Me.Branches = branches
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the start. </summary>
        '''
        ''' <value> The start. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Start() As BasicBlock
        Public ReadOnly Property [End]() As BasicBlock

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the blocks. </summary>
        '''
        ''' <value> The blocks. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Blocks() As List(Of BasicBlock)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the branches. </summary>
        '''
        ''' <value> The branches. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public ReadOnly Property Branches() As List(Of BasicBlockBranch)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A basic block. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public NotInheritable Class BasicBlock

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Initializes a new instance of the <see cref="T:System.Object" /> class. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New()
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Constructor. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="isStart">    True if is start, false if not. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(isStart As Boolean)
                Me.IsStart = isStart
                IsEnd = Not isStart
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the is start. </summary>
            '''
            ''' <value>   The is start. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property IsStart() As Boolean

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the is end. </summary>
            '''
            ''' <value>   The is end. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property IsEnd() As Boolean

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the statements. </summary>
            '''
            ''' <value>   The statements. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property Statements() As New List(Of BoundStatement)()

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the incoming. </summary>
            '''
            ''' <value>   The incoming. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property Incoming() As New List(Of BasicBlockBranch)()

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the outgoing. </summary>
            '''
            ''' <value>   The outgoing. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property Outgoing() As New List(Of BasicBlockBranch)()

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Returns a string that represents the current object. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns> A string that represents the current object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function ToString() As String

                If IsStart Then
                    Return "<Start>"
                End If

                If IsEnd Then
                    Return "<End>"
                End If

                Using writer = New StringWriter()
                    Using indentedWriter = New IndentedTextWriter(writer)
                        For Each statement In Statements
                            statement.WriteTo(indentedWriter)
                        Next
                        Return writer.ToString()
                    End Using
                End Using

            End Function

        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A basic block branch. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public NotInheritable Class BasicBlockBranch

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Constructor. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="from">           Source for the. </param>
            ''' <param name="![to]">          The [to]. </param>
            ''' <param name="As BasicBlock">  as basic block. </param>
            ''' <param name="condition">      The condition. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Sub New(from As BasicBlock, [to] As BasicBlock, condition As BoundExpression)
                Me.From = from
                Me.To = [to]
                Me.Condition = condition
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the source for the. </summary>
            '''
            ''' <value>   from. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property From() As BasicBlock
            Public ReadOnly Property [To]() As BasicBlock

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Gets or sets the condition. </summary>
            '''
            ''' <value>   The condition. </value>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public ReadOnly Property Condition() As BoundExpression

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Returns a string that represents the current object. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <returns> A string that represents the current object. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Overrides Function ToString() As String
                If Condition Is Nothing Then
                    Return String.Empty
                End If
                Return Condition.ToString()
            End Function

        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A basic block builder. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public NotInheritable Class BasicBlockBuilder
            ''' <summary> The statements. </summary>

            Private ReadOnly m_statements As New List(Of BoundStatement)()
            ''' <summary> The blocks. </summary>
            Private ReadOnly m_blocks As New List(Of BasicBlock)()

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Builds the given block. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="Exception">  Thrown when an exception error condition occurs. </exception>
            '''
            ''' <param name="block">  The block. </param>
            '''
            ''' <returns> A List(Of BasicBlock) </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function Build(block As BoundBlockStatement) As List(Of BasicBlock)
                For Each statement In block.Statements
                    Select Case statement.Kind
                        Case BoundNodeKind.LabelStatement
                            StartBlock()
                            m_statements.Add(statement)
                        Case BoundNodeKind.GotoStatement, BoundNodeKind.ConditionalGotoStatement, BoundNodeKind.ReturnStatement
                            m_statements.Add(statement)
                            StartBlock()
                        Case BoundNodeKind.NopStatement, BoundNodeKind.VariableDeclaration, BoundNodeKind.ExpressionStatement
                            m_statements.Add(statement)
                        Case Else
                            Throw New Exception($"Unexpected statement: {statement.Kind}")
                    End Select
                Next
                EndBlock()
                Return m_blocks.ToList()
            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Starts a block. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Sub StartBlock()
                EndBlock()
            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Ends a block. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Sub EndBlock()
                If m_statements.Count > 0 Then
                    Dim block = New BasicBlock()
                    block.Statements.AddRange(m_statements)
                    m_blocks.Add(block)
                    m_statements.Clear()
                End If
            End Sub

        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A graph builder. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public NotInheritable Class GraphBuilder
            ''' <summary> The block from statement. </summary>

            Private ReadOnly m_blockFromStatement As New Dictionary(Of BoundStatement, BasicBlock)()
            ''' <summary> The block from label. </summary>
            Private ReadOnly m_blockFromLabel As New Dictionary(Of BoundLabel, BasicBlock)()
            ''' <summary> The branches. </summary>
            Private ReadOnly m_branches As New List(Of BasicBlockBranch)()
            ''' <summary> The start. </summary>
            Private ReadOnly m_start As New BasicBlock(isStart:=True)
            ''' <summary> The end. </summary>
            Private ReadOnly m_end As New BasicBlock(isStart:=False)

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Builds the given blocks. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <exception cref="Exception">  Thrown when an exception error condition occurs. </exception>
            '''
            ''' <param name="blocks"> The blocks. </param>
            '''
            ''' <returns> A ControlFlowGraph. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Public Function Build(blocks As List(Of BasicBlock)) As ControlFlowGraph
                If Not blocks.Any() Then
                    Connect(m_start, m_end)
                Else
                    Connect(m_start, blocks.First())
                End If

                For Each block In blocks
                    For Each statement In block.Statements
                        m_blockFromStatement.Add(statement, block)
                        If TypeOf statement Is BoundLabelStatement Then
                            Dim labelStatement = CType(statement, BoundLabelStatement)
                            m_blockFromLabel.Add(labelStatement.Label, block)
                        End If
                    Next
                Next

                For i = 0 To blocks.Count - 1
                    Dim current = blocks(i)
                    Dim [next] = If(i = blocks.Count - 1, m_end, blocks(i + 1))
                    For Each statement In current.Statements
                        Dim isLastStatementInBlock = statement Is current.Statements.Last()
                        Select Case statement.Kind
                            Case BoundNodeKind.GotoStatement
                                Dim gs = CType(statement, BoundGotoStatement)
                                Dim toBlock = m_blockFromLabel(gs.Label)
                                Connect(current, toBlock)
                            Case BoundNodeKind.ConditionalGotoStatement
                                Dim cgs = CType(statement, BoundConditionalGotoStatement)
                                Dim thenBlock = m_blockFromLabel(cgs.Label)
                                Dim elseBlock = [next]
                                Dim negatedCondition = Negate(cgs.Condition)
                                Dim thenCondition = If(cgs.JumpIfTrue, cgs.Condition, negatedCondition)
                                Dim elseCondition = If(cgs.JumpIfTrue, negatedCondition, cgs.Condition)
                                Connect(current, thenBlock, thenCondition)
                                Connect(current, elseBlock, elseCondition)
                            Case BoundNodeKind.ReturnStatement
                                Connect(current, m_end)
                            Case BoundNodeKind.NopStatement,
                                 BoundNodeKind.VariableDeclaration,
                                 BoundNodeKind.LabelStatement,
                                 BoundNodeKind.ExpressionStatement
                                If isLastStatementInBlock Then
                                    Connect(current, [next])
                                End If
                            Case Else
                                Throw New Exception($"Unexpected statement: {statement.Kind}")
                        End Select
                    Next
                Next

ScanAgain:
                For Each block In blocks
                    If Not block.Incoming.Any() Then
                        RemoveBlock(blocks, block)
                        GoTo ScanAgain
                    End If
                Next block

                blocks.Insert(0, m_start)
                blocks.Add(m_end)

                Return New ControlFlowGraph(m_start, m_end, blocks, m_branches)

            End Function

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Connects. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="from">           Source for the. </param>
            ''' <param name="![to]">          The [to]. </param>
            ''' <param name="As BasicBlock">  as basic block. </param>
            ''' <param name="condition">      (Optional) The condition. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Sub Connect(from As BasicBlock, [to] As BasicBlock, Optional condition As BoundExpression = Nothing)

                If TypeOf condition Is BoundLiteralExpression Then
                    Dim l = CType(condition, BoundLiteralExpression)
                    Dim value = CBool(l.Value)
                    If value Then
                        condition = Nothing
                    Else
                        Return
                    End If
                End If

                Dim branch = New BasicBlockBranch(from, [to], condition)
                from.Outgoing.Add(branch)
                [to].Incoming.Add(branch)
                m_branches.Add(branch)

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Removes the block. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="blocks"> The blocks. </param>
            ''' <param name="block">  The block. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Sub RemoveBlock(blocks As List(Of BasicBlock), block As BasicBlock)

                For Each branch In block.Incoming
                    branch.From.Outgoing.Remove(branch)
                    m_branches.Remove(branch)
                Next

                For Each branch In block.Outgoing
                    branch.To.Incoming.Remove(branch)
                    m_branches.Remove(branch)
                Next

                blocks.Remove(block)

            End Sub

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary> Negates the given condition. </summary>
            '''
            ''' <remarks> Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="condition">  The condition. </param>
            '''
            ''' <returns> A BoundExpression. </returns>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Private Function Negate(condition As BoundExpression) As BoundExpression
                If TypeOf condition Is BoundLiteralExpression Then
                    Dim literal = CType(condition, BoundLiteralExpression)
                    Dim value = CBool(literal.Value)
                    Return New BoundLiteralExpression(Not value)
                End If
                Dim op = BoundUnaryOperator.Bind(SyntaxKind.BangToken, TypeSymbol.Bool)
                Debug.Assert(op IsNot Nothing)
                Return New BoundUnaryExpression(op, condition)
            End Function
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Quotes. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="text"> The text. </param>
        '''
        ''' <returns>   A String. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function Quote(text As String) As String
            Return """" & text.Replace("""", "\""") & """"
            Return """" & text.TrimEnd().Replace("\", "\\").Replace("""", "\""").Replace(Environment.NewLine, "\l") & """"
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Writes to. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="writer">   The writer. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub WriteTo(writer As TextWriter)

            writer.WriteLine("digraph G {")

            Dim blockIds = New Dictionary(Of BasicBlock, String)()

            For i = 0 To Blocks.Count - 1
                Dim id = $"N{i}"
                blockIds.Add(Blocks(i), id)
            Next

            For Each block In Blocks
                Dim id = blockIds(block)
                Dim label = Quote(block.ToString().Replace(Environment.NewLine, "\l"))
                writer.WriteLine($"    {id} [label = {label} shape = box]")
            Next

            For Each branch In Branches
                Dim fromId = blockIds(branch.From)
                Dim toId = blockIds(branch.To)
                Dim label = Quote(branch.ToString())
                writer.WriteLine($"    {fromId} -> {toId} [label = {label}]")
            Next

            writer.WriteLine("}")

        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Creates a new ControlFlowGraph. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="body"> The body. </param>
        '''
        ''' <returns>   A ControlFlowGraph. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function Create(body As BoundBlockStatement) As ControlFlowGraph
            Dim basicBlockBuilder = New BasicBlockBuilder()
            Dim blocks = basicBlockBuilder.Build(body)
            Dim graphBuilder = New GraphBuilder()
            Return graphBuilder.Build(blocks)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   All paths return. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="body"> The body. </param>
        '''
        ''' <returns>   True if it succeeds, false if it fails. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Shared Function AllPathsReturn(body As BoundBlockStatement) As Boolean

      Dim graph = Create(body)

      For Each branch In graph.End.Incoming
        'Dim lastStatement = branch.From.Statements.Last()
        'If lastStatement.Kind <> BoundNodeKind.ReturnStatement Then
        Dim lastStatement = branch.From.Statements.LastOrDefault
        If lastStatement Is Nothing OrElse lastStatement.Kind <> BoundNodeKind.ReturnStatement Then
          Return False
        End If
      Next

      Return True

    End Function

  End Class

End Namespace
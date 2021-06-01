Imports System.Windows.Forms
Imports AI_BASIC.Syntax.SyntaxNodes
Imports AI_BASIC.Typing

Public Class SyntaxViewer
    Public Sub New(ByRef Tree As List(Of SyntaxNode))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        display(Tree)

    End Sub

    Public Sub display(ByRef Tree As List(Of SyntaxNode))
        Dim root As New TreeNode
        root.ImageIndex = 22
        root.Text = "Program"
        Try




            For Each item In Tree
                Dim ItemNode As New TreeNode
                ItemNode.Tag = item.ToJson
                ItemNode.ImageIndex = 30
                Select Case item._SyntaxType
                    Case SyntaxType._NumericLiteralExpression
                        Dim ItemNodeData As New TreeNode
                        Dim n As NumericalExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        ItemNodeData.Text = n._Literal
                        ItemNodeData.ImageIndex = 24
                        ItemNodeData.Tag = n.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._IdentifierExpression
                        Dim i As IdentifierExpression = item
                        Dim ItemNodeData As New TreeNode
                        ItemNode.Text = item._SyntaxTypeStr
                        ItemNodeData.Text = i._Literal
                        ItemNodeData.ImageIndex = 24
                        ItemNodeData.Tag = i.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._StringExpression
                        Dim s As StringExpression = item
                        Dim ItemNodeData As New TreeNode
                        ItemNode.Text = item._SyntaxTypeStr
                        ItemNodeData.Text = s._Literal
                        ItemNodeData.ImageIndex = 24
                        ItemNodeData.Tag = s.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._BooleanLiteralExpression
                        Dim _b As BooleanLiteralExpression = item
                        Dim ItemNodeData As New TreeNode
                        ItemNode.Text = item._SyntaxTypeStr
                        ItemNodeData.ImageIndex = 24
                        ItemNodeData.Text = _b._Literal
                        ItemNodeData.Tag = _b.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._VariableDeclaration
                        Dim V As VariableDeclarationExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        ItemNode.Tag = item.ToJson
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 24
                        ItemNodeData.Text = V._Literal
                        ItemNodeData.Tag = V.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 28
                        ItemNodeData2.Text = V._literalTypeStr
                        ItemNodeData2.Tag = V.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._AssignmentExpression
                        Dim a As AssignmentExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 28
                        ItemNodeData.Text = a._identifier._SyntaxTypeStr
                        ItemNodeData.Tag = a._identifier.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 29
                        ItemNodeData2.Text = a.Operand._SyntaxTypeStr
                        ItemNodeData2.Tag = a.Operand.ToJson
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.Text = a.Value._SyntaxTypeStr
                        ItemNodeData3.Tag = a.Value.ToJson
                        ItemNodeData2.ImageIndex = 24
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.ConditionalExpression
                        Dim bC As BinaryExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 27
                        ItemNodeData.Text = bC._Left._SyntaxTypeStr
                        ItemNodeData.Tag = bC._Left.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 29
                        ItemNodeData2.Text = bC._Operator._SyntaxTypeStr
                        ItemNodeData2.Tag = bC._Operator.ToJson
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.Text = bC._Right._SyntaxTypeStr
                        ItemNodeData3.ImageIndex = 27
                        ItemNodeData3.Tag = bC._Right.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType._BinaryExpression
                        Dim b As BinaryExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 27
                        ItemNodeData.Text = b._Left._SyntaxTypeStr
                        ItemNodeData.Tag = b._Left.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.Text = b._Operator._SyntaxTypeStr
                        ItemNodeData2.Tag = b._Operator.ToJson
                        ItemNodeData2.ImageIndex = 29
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.Text = b._Right._SyntaxTypeStr
                        ItemNodeData3.Tag = b._Right.ToJson
                        ItemNodeData3.ImageIndex = 27
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.AddativeExpression
                        Dim bA As BinaryExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 27
                        ItemNodeData.Text = bA._Left._SyntaxTypeStr
                        ItemNodeData.Tag = bA._Left.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.Text = bA._Operator._SyntaxTypeStr
                        ItemNodeData2.Tag = bA._Operator.ToJson
                        ItemNodeData2.ImageIndex = 29
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.ImageIndex = 27
                        ItemNodeData3.Text = bA._Right._SyntaxTypeStr
                        ItemNodeData3.Tag = bA._Right.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.MultiplicativeExpression
                        Dim bM As BinaryExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 27
                        ItemNodeData.Text = bM._Left._SyntaxTypeStr
                        ItemNodeData.Tag = bM._Left.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.Text = bM._Operator._SyntaxTypeStr
                        ItemNodeData2.Tag = bM._Operator.ToJson
                        ItemNodeData2.ImageIndex = 29
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.ImageIndex = 27
                        ItemNodeData3.Text = bM._Right._SyntaxTypeStr
                        ItemNodeData3.Tag = bM._Right.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.ifThenExpression
                        Dim _IT As IfThenExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 26
                        ItemNodeData.Text = _IT.IfCondition._SyntaxTypeStr
                        ItemNodeData.Tag = _IT.IfCondition.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 25
                        ItemNodeData2.Text = _IT.ThenCondition._SyntaxTypeStr
                        ItemNodeData2.Tag = _IT.ThenCondition.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.ifElseExpression
                        Dim I_E As IfElseExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 26
                        ItemNodeData.Text = I_E.IfCondition._SyntaxTypeStr
                        ItemNodeData.Tag = I_E.IfCondition.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 25
                        ItemNodeData2.Text = I_E.ThenCondition._SyntaxTypeStr
                        ItemNodeData2.Tag = I_E.ThenCondition.ToJson
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.Text = I_E.ElseCondition._SyntaxTypeStr
                        ItemNodeData3.Tag = I_E.ElseCondition.ToJson
                        ItemNodeData.ImageIndex = 26
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.ForExpression
                        Dim f As ForExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 28
                        ItemNodeData.Text = f._Identifier._SyntaxTypeStr
                        ItemNodeData.Tag = f._Identifier.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData2.ImageIndex = 24
                        ItemNodeData2.Text = f.LowerStart._SyntaxTypeStr
                        ItemNodeData2.Tag = f.LowerStart.ToJson
                        Dim ItemNodeData3 As New TreeNode
                        ItemNodeData3.ImageIndex = 30
                        ItemNodeData3.Text = f.Body._SyntaxTypeStr
                        ItemNodeData3.Tag = f.Body.ToJson
                        Dim ItemNodeData4 As New TreeNode
                        ItemNodeData4.ImageIndex = 24
                        ItemNodeData4.Text = f.UpperStart._SyntaxTypeStr
                        ItemNodeData4.Tag = f.UpperStart.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        ItemNode.Nodes.Add(ItemNodeData4)
                        ItemNode.Nodes.Add(ItemNodeData3)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.DO_WhileExpression
                        Dim w As WhileExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 26
                        ItemNodeData.Text = w.Condition._SyntaxTypeStr
                        ItemNodeData.Tag = w.Condition.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData.ImageIndex = 30
                        ItemNodeData2.Text = w.Body._SyntaxTypeStr
                        ItemNodeData2.Tag = w.Body.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        root.Nodes.Add(ItemNode)
                    Case SyntaxType.DO_UntilExpression
                        Dim u As UntilExpression = item
                        ItemNode.Text = item._SyntaxTypeStr
                        Dim ItemNodeData As New TreeNode
                        ItemNodeData.ImageIndex = 26
                        ItemNodeData.Text = u.Condition._SyntaxTypeStr
                        ItemNodeData.Tag = u.Condition.ToJson
                        Dim ItemNodeData2 As New TreeNode
                        ItemNodeData.ImageIndex = 30
                        ItemNodeData2.Text = u.Body._SyntaxTypeStr
                        ItemNodeData2.Tag = u.Body.ToJson
                        ItemNode.Nodes.Add(ItemNodeData)
                        ItemNode.Nodes.Add(ItemNodeData2)
                        root.Nodes.Add(ItemNode)

                End Select
            Next
        Catch ex As Exception

        End Try
        DISPLAY_TREE.Nodes.Add(root)
    End Sub

    Private Sub DISPLAY_TREE_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DISPLAY_TREE.AfterSelect
        CodeDisplay.Text = DISPLAY_TREE.SelectedNode.Tag

    End Sub

    Private Sub ButtonClearTree_Click(sender As Object, e As EventArgs) Handles ButtonClearTree.Click
        DISPLAY_TREE.Nodes.Clear()
    End Sub
End Class
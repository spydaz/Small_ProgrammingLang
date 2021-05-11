

Imports SAL_VM.STACK_VM.ZX81_GPU
''' <summary>
''' 
''' 
''' Uses Logo Programming Lang as Controller See (Decoder)
''' </summary>
Public Class GDU

    Inherits PictureBox
    Private iTurtlePosition As New Point(0, 0)
    Private iTurtlePenUP_DWN As Boolean = False
    Private iDirection As Integer = 0
    Private iTurtleShow As Boolean = False
    Private iTurtleColor As Color = Color.Black
    Private iTurtlePenSize As Integer = 3
    Private iTurtleGraphics As System.Drawing.Graphics
    Private iX_ As Integer = 0
    Private iY_ As Integer = 0
    Private iTurtlePen As New System.Drawing.Pen(TurtleColor, TurtlePenSize)
    Public CenterPoint = New PointF(Me.Width - Me.Width / 2.0F, Me.Height - Me.Height / 2.0F)
    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        TurtleGraphics = Me.CreateGraphics()
    End Sub

    Public Property Current_X_Position As Integer
        Get
            Return Current_X_Position
        End Get
        Set(value As Integer)
            iX_ = value
        End Set
    End Property
    Public Property Current_Y_Position As Integer
        Get
            Return Current_Y_Position
        End Get
        Set(value As Integer)
            iY_ = value
        End Set
    End Property
    Public Sub SetTurtlePen(nTurtleColor As Color, nTurtlePenSize As Integer)
        iTurtlePen = New System.Drawing.Pen(nTurtleColor, nTurtlePenSize)
    End Sub
    Public Property TurtlePosition As Point
        Get
            Return iTurtlePosition
        End Get
        Set(value As Point)
            iTurtlePosition = value
        End Set
    End Property
    Public Property TurtlePenUP_DWN As Boolean
        Get
            Return iTurtlePenUP_DWN
        End Get
        Set(value As Boolean)
            iTurtlePenUP_DWN = value
        End Set
    End Property
    Public Property Direction As Integer
        Get
            Return iDirection
        End Get
        Set(value As Integer)
            iDirection = Direction
        End Set
    End Property
    Public Property TurtleShow As Boolean
        Get
            Return iTurtleShow
        End Get
        Set(value As Boolean)
            iTurtleShow = value

        End Set
    End Property
    Public Property TurtleColor As Color
        Get
            Return iTurtleColor
        End Get
        Set(value As Color)
            iTurtleColor = value
        End Set
    End Property
    Public Property TurtlePenSize As Integer
        Get
            Return iTurtlePenSize
        End Get
        Set(value As Integer)
            iTurtlePenSize = value
        End Set
    End Property
    Public Property TurtleGraphics As System.Drawing.Graphics
        Get
            Return iTurtleGraphics
        End Get
        Set(value As System.Drawing.Graphics)
            iTurtleGraphics = value
        End Set
    End Property


#Region "Comands"

    Public Sub _FD(ByRef Distance As Integer)
        Dim NePosition As New Point(TurtlePosition.X + Distance, TurtlePosition.Y)
        TurtleGraphics.DrawLine(New Pen(TurtleColor, TurtlePenSize), TurtlePosition, NePosition)
        TurtleGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        TurtlePosition = NePosition
        Me.Invalidate()
    End Sub
    Public Sub _BK(ByRef Distance As Integer)
        Dim NePosition As New Point(TurtlePosition.X - Distance, TurtlePosition.Y)
        TurtleGraphics.DrawLine(New Pen(TurtleColor, TurtlePenSize), TurtlePosition, NePosition)
        TurtleGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        TurtlePosition = NePosition
        Me.Invalidate()
    End Sub
    Public Sub _RT(ByRef Degrees As Integer)
        Direction += Degrees
    End Sub
    Public Sub _LT(ByRef Degrees As Integer)
        Direction -= Degrees
    End Sub
    Public Sub _CS()
        TurtleGraphics.Clear(Color.LightGray)
    End Sub
    Public Sub _CT()
        TurtlePosition = CenterPoint
    End Sub
    ''' <summary>
    ''' Pen Drawing
    ''' </summary>
    Public Sub _PD()
        TurtlePenUP_DWN = True
    End Sub
    ''' <summary>
    ''' Not Drawing
    ''' </summary>
    Public Sub _PU()
        TurtlePenUP_DWN = False
    End Sub
    Public Sub _HT()
        TurtleShow = False
    End Sub
    Public Sub _ST()
        TurtleShow = True
    End Sub
    Public Sub _SetPos(ByRef X_CoOrd As Integer, ByRef Y_CoOrd As Integer)
        TurtlePosition = New Point(X_CoOrd, Y_CoOrd)
    End Sub
    Public Sub _SetPenColor(ByRef Col As Integer)
        iTurtlePen.Color = _SetColor(Col)
    End Sub
    Public Sub _SetBackGroundColor(ByRef Col As Integer)
        Me.BackColor = _SetColor(Col)
    End Sub
    Public Function _SetColor(ByRef Colour As Integer) As Color
        ' SetColor = Color.White
        Select Case Colour
            Case 1
                Return Color.Red
            Case 2
                Return Color.Orange
            Case 3
                Return Color.Yellow
            Case 4
                Return Color.Green
            Case 5
                Return Color.Blue
            Case 6
                Return Color.Purple
            Case 7
                Return Color.Brown
            Case 8
                Return Color.Black
            Case 9
                Return Color.White


        End Select
    End Function

#End Region

    Public Function RotateImage(ByVal bmp As Image, ByVal angleDegrees As Single) As Bitmap
        Dim rotatedImage = New Bitmap(bmp.Width, bmp.Height)

        Using g = Graphics.FromImage(rotatedImage)
            g.TranslateTransform(bmp.Width / 2.0F, bmp.Height / 2.0F)
            g.RotateTransform(90 - angleDegrees)
            g.TranslateTransform(-bmp.Width / 2.0F, -bmp.Height / 2.0F)
            g.DrawImage(bmp, New Point(0, 0))
        End Using

        Return rotatedImage
    End Function

#Region "Evaluator "
    ''' <summary>
    '''fd – forward
    '''bk – backward
    '''rt – right
    '''lt – left
    '''cs – clearscreen
    '''pu − penup
    '''pd − pendown
    '''ht − hideturtle
    '''st − showturtle
    ''' </summary>
    ''' <param name="Command"></param>
    Public Sub Decode(ByRef Command As LogoCmd)
        Select Case Command.Cmd
            Case "FD"
                _FD(Command.Value)
            Case "BK"
                _BK(Command.Value)
            Case "RT"
                _RT(Command.Value)
            Case "LT"
                _LT(Command.Value)
            Case "PU"
                _PU()
            Case "PD"
                _PD()
            Case "ST"
                _ST()
            Case "HT"
                _HT()
            Case "SETPEN"
                _SetPenColor(Command.Value)
            Case "SETBACK"
                _SetBackGroundColor(Command.Value)
        End Select

    End Sub
#End Region
End Class

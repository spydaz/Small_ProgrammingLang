Imports System.Drawing.Drawing2D
Imports System.Threading


Namespace SmallProgLang


    Public Class TURTLE



        Public X_axis As Integer
        Public Y_axis As Integer
        Private Const MAX_CANVAS_WIDTH = 3000
        Private Const MAX_CANVAS_HEIGHT = 3000
        Private drawingImage As Image
        Private drawingGraphics As Graphics
        Public turtleImage As Image
        Private m_Angle As Double = 90
        Private Color As Color = Color.Red
        Private penWidth As Double = 1
        Private ReadOnly turtlePicture As New PictureBox
        Public _CONTROL As Panel
        Public _PenStatus As PenStatus = PenStatus.Down

        Public PenColor As Color
        Public Enum PenStatus
            Up
            Down
        End Enum

        Public Sub New(control As Panel, turtleImage As Image)
            'Me.MAX_CANVAS_WIDTH = _Screen.Width
            'Me.MAX_CANVAS_HEIGHT = _Screen.Height
            Me.turtleImage = turtleImage
            Me._CONTROL = control
            InitalizeTurtle(control.Width, control.Height)
            ShowHideTurtle = True
        End Sub
        Private Sub InvalidateControl()
            _CONTROL.Invalidate()


            _CONTROL.Update()
            Thread.Sleep(100)
            Application.DoEvents()

        End Sub
        Private Sub OnClientSizeChanged(ByVal sender As Object, ByVal e As EventArgs)
            If _CONTROL IsNot Nothing Then
                Reset()
                _CONTROL.Invalidate()
            End If
        End Sub
        Public Property _Angle As Integer
            Get
                Return m_Angle
            End Get
            Set(value As Integer)


                If (value < 0) Then

                    value = 0

                Else
                    If value > 360 Then
                        value = 360
                    End If
                End If
                m_Angle = value

            End Set
        End Property

        Private Sub InitalizeTurtle(ByRef maximumCanvasWidth As Integer, ByRef maximumCanvasHeight As Integer)
            m_Angle = 90.0
            Color = Color.Black
            penWidth = 1.0
            _CONTROL.Controls.Add(turtlePicture)
            AddHandler _CONTROL.Paint, AddressOf OnControlPaint
            AddHandler _CONTROL.ClientSizeChanged, AddressOf OnClientSizeChanged

            drawingImage = New Bitmap(maximumCanvasWidth, maximumCanvasHeight)
            drawingGraphics = Graphics.FromImage(drawingImage)
            drawingGraphics.Clear(Color.LightGray)
            drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias
        End Sub
        Private Sub _CenterScreen()
            _Reset()
        End Sub
        Private Shared Function WorldPositionToControl(ByVal control As Control, ByVal original As PointF) As PointF
            Dim w = control.ClientSize.Width
            Dim h = control.ClientSize.Height
            Return New PointF(w / 2.0F + original.X, h / 2.0F - original.Y)
        End Function
        Private Shared Function ControlPositionToWorld(ByVal control As Control, ByVal original As PointF) As PointF
            Return New PointF(original.X - control.ClientSize.Width / 2.0F, control.ClientSize.Height / 2.0F - original.Y)
        End Function
        Public Property Position As PointF
            Get
                Dim centerPoint = New PointF(Me.turtlePicture.Left + Me.turtlePicture.Width / 2.0F, Me.turtlePicture.Top + Me.turtlePicture.Height / 2.0F)
                Return ControlPositionToWorld(_CONTROL, centerPoint)
            End Get
            Set(ByVal value As PointF)
                Dim centerInControl = WorldPositionToControl(_CONTROL, value)
                Dim left = centerInControl.X - Me.turtlePicture.Width / 2.0
                Dim top = centerInControl.Y - Me.turtlePicture.Height / 2.0
                Me.turtlePicture.Left = Convert.ToInt32(left)
                Me.turtlePicture.Top = Convert.ToInt32(top)
            End Set
        End Property
        Public Sub _Reset()
            _Angle = 90.0F
            _Rotate()
            Position = New Point(0, 0)
        End Sub

        Dim ShowHideTurtle = False
        Public Sub ShowTurtle()
            ShowHideTurtle = True
        End Sub

        Public Sub HideTurtle()
            ShowHideTurtle = False
        End Sub
        Public Sub SetPenWidth(ByVal width As Single)
            penWidth = width
        End Sub
        Public Sub SetPenColor(ByVal color As Color)
            Me.PenColor = color
        End Sub
        Public Sub SetPenColor(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer)
            PenColor = Color.FromArgb(r, g, b)
        End Sub
        Public Sub DrawLine(ByVal _from As PointF, ByVal _to As PointF)
            Using pen = New Pen(PenColor, Me.penWidth) With {
        .StartCap = LineCap.Round,
        .EndCap = LineCap.Round
    }
                Dim fromPoint = WorldPositionToControl(_CONTROL, _from)
                Dim toPoint = WorldPositionToControl(_CONTROL, _to)
                Me.drawingGraphics.DrawLine(pen, fromPoint, toPoint)

            End Using
        End Sub
        Private Sub OnControlPaint(ByVal sender As Object, ByVal e As PaintEventArgs)
            If _CONTROL IsNot Nothing Then
                e.Graphics.DrawImage(Me.drawingImage, 0, 0)
            End If
        End Sub
        ''' <summary>
        ''' Forwards Command
        ''' </summary>
        ''' <param name="Amt"></param>
        Public Sub _forward(ByRef Amt As Integer)

            Dim toX = Convert.ToSingle(Me.Position.X + Amt * Math.Cos(_Angle * Math.PI / 180))
            Dim toY = Convert.ToSingle(Me.Position.Y + Amt * Math.Sin(_Angle * Math.PI / 180))
            Dim origPosition = Me.Position
            Me.Position = New PointF(toX, toY)

            If _PenStatus = PenStatus.Down Then
                DrawLine(origPosition, New PointF(toX, toY))
            End If

        End Sub
        Public Sub _backward(ByRef Amt As Integer)

            _forward(-Amt)
        End Sub
        Public Sub _Right(ByRef Degrees As Integer)

            _Angle += Degrees
            _Rotate()
        End Sub
        Public Sub _Left(ByRef Degrees As Integer)

            _Angle -= Degrees
            _Rotate()
        End Sub



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
        Public Sub _Rotate()
            turtlePicture.Image = RotateImage(turtleImage, _Angle)
            turtlePicture.Width = turtlePicture.Image.Width
            turtlePicture.Height = turtlePicture.Image.Height
        End Sub



        Public Sub _Clear()
            drawingGraphics.Clear(Color.White)
            _Reset()
        End Sub
    End Class
End Namespace

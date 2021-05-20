Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Threading
Imports System.Windows.Forms

Namespace Nakov.TurtleGraphics
    Public Module Turtle
        Private xField As Integer

        Public Property X As Integer
            Get
                Call InitOnDemand()
                Return xField
            End Get
            Set(ByVal value As Integer)
                Call InitOnDemand()
                xField = value
            End Set
        End Property

        Private yField As Integer

        Public Property Y As Integer
            Get
                Call InitOnDemand()
                Return yField
            End Get
            Set(ByVal value As Integer)
                Call InitOnDemand()
                yField = value
            End Set
        End Property

        Private angleField As Single

        Public Property Angle As Single
            Get
                Call InitOnDemand()
                Return angleField
            End Get
            Set(ByVal value As Single)
                Call InitOnDemand()
                angleField = value Mod 360

                If angleField < 0 Then
                    angleField += 360
                End If
            End Set
        End Property

        Public Property PenColor As Color
            Get
                Call InitOnDemand()
                Return drawPen.Color
            End Get
            Set(ByVal value As Color)
                Call InitOnDemand()
                drawPen.Color = value
            End Set
        End Property

        Public Property PenSize As Single
            Get
                Call InitOnDemand()
                Return drawPen.Width
            End Get
            Set(ByVal value As Single)
                Call InitOnDemand()
                drawPen.Width = value
            End Set
        End Property

        Private penVisibleField As Boolean

        Public Property PenVisible As Boolean
            Get
                Call InitOnDemand()
                Return penVisibleField
            End Get
            Set(ByVal value As Boolean)
                Call InitOnDemand()
                penVisibleField = value
            End Set
        End Property

        Public Property ShowTurtle As Boolean
            Get
                Call InitOnDemand()
                Return turtleHeadImage.Visible
            End Get
            Set(ByVal value As Boolean)
                Call InitOnDemand()
                turtleHeadImage.Visible = value
            End Set
        End Property

        Private delayField As Integer

        Public Property Delay As Integer
            Get
                Call InitOnDemand()
                Return delayField
            End Get
            Set(ByVal value As Integer)
                Call InitOnDemand()
                delayField = value
            End Set
        End Property

        Public Const DrawAreaSize As Integer = 10000
        Public ReadOnly DefaultColor As Color = Color.Blue
        Public Const DefaultPenSize As Integer = 7
        Private drawControl As Control
        Private drawImage As Image
        Private drawGraphics As Graphics
        Private drawPen As Pen
        Private turtleHeadImage As PictureBox

        Public Sub Init(ByVal Optional targetControl As Control = Nothing)
            ' Dispose all resources if already allocated
            Call Dispose()

            ' Initialize the drawing control (sufrace)
            drawControl = targetControl

            If drawControl Is Nothing Then
                ' If no target control is provided, use the currently active form
                drawControl = Form.ActiveForm
            End If

            SetDoubleBuffered(drawControl)

            ' Create an empty graphics area to be used by the turtle
            drawImage = New Bitmap(DrawAreaSize, DrawAreaSize)
            AddHandler drawControl.Paint, AddressOf DrawControl_Paint
            AddHandler drawControl.ClientSizeChanged, AddressOf DrawControl_ClientSizeChanged
            drawGraphics = Graphics.FromImage(drawImage)
            drawGraphics.SmoothingMode = SmoothingMode.AntiAlias

            ' Initialize the pen size and color
            drawPen = New Pen(DefaultColor, DefaultPenSize)
            drawPen.StartCap = LineCap.Round
            drawPen.EndCap = LineCap.Round

            ' Initialize the turtle position and other settings
            X = 0
            Y = 0
            Angle = 0
            PenVisible = True
            ' Delay = 0;  // Intentionally preserve the "Delay" settings

            ' Initialize the turtle head image
            turtleHeadImage = New PictureBox()
            turtleHeadImage.BackColor = Color.Transparent
            drawControl.Controls.Add(turtleHeadImage)
        End Sub

        Public Sub Dispose()
            If drawControl IsNot Nothing Then
                ' Remove the associated events for the drawing control
                RemoveHandler drawControl.Paint, AddressOf DrawControl_Paint
                RemoveHandler drawControl.ClientSizeChanged, AddressOf DrawControl_ClientSizeChanged

                ' Release the pen object
                Call drawPen.Dispose()
                drawPen = Nothing

                ' Release the graphic object
                Call drawGraphics.Dispose()
                drawGraphics = Nothing

                ' Release the draw surface (image) object
                Call drawImage.Dispose()
                drawImage = Nothing

                ' Release the turtle (head) image
                drawControl.Controls.Remove(turtleHeadImage)
                Call turtleHeadImage.Dispose()
                turtleHeadImage = Nothing

                ' Release the drawing control
                Call drawControl.Invalidate()
                drawControl = Nothing
            End If
        End Sub

        Public Sub Reset()
            Call Dispose()
        End Sub

        Public Sub Forward(ByVal Optional distance As Single = 10)
            Dim angleRadians = Angle * Math.PI / 180
            Dim newX = X + CSng(distance * Math.Sin(angleRadians))
            Dim newY = Y + CSng(distance * Math.Cos(angleRadians))
            MoveTo(newX, newY)
        End Sub

        Public Sub Backward(ByVal Optional distance As Single = 10)
            Forward(-distance)
        End Sub

        Public Sub MoveTo(ByVal newX As Single, ByVal newY As Single)
            Call InitOnDemand()
            Dim fromX As Integer = DrawAreaSize / 2 + X
            Dim fromY As Integer = DrawAreaSize / 2 - Y
            X = newX
            Y = newY

            If PenVisible Then
                Dim toX = DrawAreaSize / 2 + X
                Dim toY = DrawAreaSize / 2 - Y
                drawGraphics.DrawLine(drawPen, CSng(fromX), CSng(fromY), CSng(toX), CSng(toY))
            End If

            Call DrawTurtle()
            Call PaintAndDelay()
        End Sub

        Public Sub Rotate(ByVal angleDelta As Single)
            Call InitOnDemand()
            Angle += angleDelta
            Call DrawTurtle()
            Call PaintAndDelay()
        End Sub

        Public Sub RotateTo(ByVal newAngle As Single)
            Call InitOnDemand()
            Angle = newAngle
            Call DrawTurtle()
            Call PaintAndDelay()
        End Sub

        Public Sub PenUp()
            PenVisible = False
        End Sub

        Public Sub PenDown()
            PenVisible = True
        End Sub

        Private Sub SetDoubleBuffered(ByVal control As Control)
            ' set instance non-public property with name "DoubleBuffered" to true
            GetType(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty Or BindingFlags.Instance Or BindingFlags.NonPublic, Nothing, control, New Object() {True})
        End Sub

        Private Sub InitOnDemand()
            ' Create the drawing surface if it does not already exist
            If drawControl Is Nothing Then
                Call Init()
            End If
        End Sub

        Private Sub DrawTurtle()
            If ShowTurtle Then
                Dim turtleImg = My.Resources.Turtle
                turtleImg = RotateImage(turtleImg, angleField)
                Dim turtleImgSize = Math.Max(turtleImg.Width, turtleImg.Height)
                turtleHeadImage.BackgroundImage = turtleImg
                turtleHeadImage.Width = turtleImg.Width
                turtleHeadImage.Height = turtleImg.Height
                Dim turtleX = 1 + drawControl.ClientSize.Width / 2 + X - turtleHeadImage.Width / 2
                Dim turtleY = 1 + drawControl.ClientSize.Height / 2 - Y - turtleHeadImage.Height / 2
                turtleHeadImage.Left = CInt(Math.Round(turtleX))
                turtleHeadImage.Top = CInt(Math.Round(turtleY))
            End If
        End Sub

        Private Function RotateImage(ByVal bmp As Bitmap, ByVal angleDegrees As Single) As Bitmap
            Dim rotatedImage As Bitmap = New Bitmap(bmp.Width, bmp.Height)

            Using g = Graphics.FromImage(rotatedImage)
                ' Set the rotation point as the center into the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2)

                ' Rotate
                g.RotateTransform(angleDegrees)

                ' Restore the rotation point into the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2)

                ' Draw the image on the new bitmap
                g.DrawImage(bmp, New Point(0, 0))
            End Using

            bmp.Dispose()
            Return rotatedImage
        End Function

        Private Sub PaintAndDelay()
            Call drawControl.Invalidate()
            ' No delay -> invalidate the control, so it will be repainted later
            If Delay = 0 Then
            Else
                ' Immediately paint the control and them delay
                Call drawControl.Update()
                Thread.Sleep(Delay)
                Call Application.DoEvents()
            End If
        End Sub

        Private Sub DrawControl_ClientSizeChanged(ByVal sender As Object, ByVal e As EventArgs)
            Call drawControl.Invalidate()
            Call DrawTurtle()
        End Sub

        Private Sub DrawControl_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
            If drawControl IsNot Nothing Then
                Dim top = (drawControl.ClientSize.Width - DrawAreaSize) / 2
                Dim left = (drawControl.ClientSize.Height - DrawAreaSize) / 2
                ' TODO: needs a fix -> does not work correctly when drawControl has AutoScroll
                e.Graphics.DrawImage(drawImage, CInt(top), CInt(left))
            End If
        End Sub
    End Module
End Namespace

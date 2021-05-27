'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\Consoles\UserControls\linenum_rtf.vb
'
' summary:	Linenum RTF class
'---------------------------------------------------------------------------------------------------

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace LineNumbers

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A line numbering. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    <ComponentModel.DefaultProperty("ParentRichTextBox")>
    Public Class LineNumbering
        Inherits Control

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   A line number item. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Class LineNumberItem
            ''' <summary>   The line number. </summary>
            Friend LineNumber As Integer
            ''' <summary>   The rectangle. </summary>
            Friend Rectangle As Rectangle

            '''////////////////////////////////////////////////////////////////////////////////////////////////////
            ''' <summary>   Constructor. </summary>
            '''
            ''' <remarks>   Leroy, 27/05/2021. </remarks>
            '''
            ''' <param name="zLineNumber">  The line number. </param>
            ''' <param name="zRectangle">   The rectangle. </param>
            '''////////////////////////////////////////////////////////////////////////////////////////////////////

            Friend Sub New(ByVal zLineNumber As Integer, ByVal zRectangle As Rectangle)
                LineNumber = zLineNumber
                Rectangle = zRectangle
            End Sub
        End Class

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Values that represent line number dock sides. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Enum LineNumberDockSide As Byte
            ''' <summary>   An enum constant representing the none option. </summary>
            None = 0
            ''' <summary>   An enum constant representing the left option. </summary>
            Left = 1
            ''' <summary>   An enum constant representing the right option. </summary>
            Right = 2
            ''' <summary>   An enum constant representing the height option. </summary>
            Height = 4
        End Enum
        ''' <summary>   The with events field z coordinate parent control. </summary>

        Private withEventsField_zParent As RichTextBox = Nothing

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the parent. </summary>
        '''
        ''' <value> The z coordinate parent. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Property zParent As RichTextBox
            Get
                Return withEventsField_zParent
            End Get
            Set(ByVal value As RichTextBox)

                If withEventsField_zParent IsNot Nothing Then
                    RemoveHandler withEventsField_zParent.LocationChanged, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.Move, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.Resize, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.DockChanged, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.TextChanged, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.MultilineChanged, AddressOf zParent_Changed
                    RemoveHandler withEventsField_zParent.HScroll, AddressOf zParent_Scroll
                    RemoveHandler withEventsField_zParent.VScroll, AddressOf zParent_Scroll
                    RemoveHandler withEventsField_zParent.ContentsResized, AddressOf zParent_ContentsResized
                    RemoveHandler withEventsField_zParent.Disposed, AddressOf zParent_Disposed
                End If

                withEventsField_zParent = value

                If withEventsField_zParent IsNot Nothing Then
                    AddHandler withEventsField_zParent.LocationChanged, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.Move, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.Resize, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.DockChanged, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.TextChanged, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.MultilineChanged, AddressOf zParent_Changed
                    AddHandler withEventsField_zParent.HScroll, AddressOf zParent_Scroll
                    AddHandler withEventsField_zParent.VScroll, AddressOf zParent_Scroll
                    AddHandler withEventsField_zParent.ContentsResized, AddressOf zParent_ContentsResized
                    AddHandler withEventsField_zParent.Disposed, AddressOf zParent_Disposed
                End If
            End Set
        End Property


        'private Windows.Forms.Timer withEventsField_zTimer = new Windows.Forms.Timer();
        'private Windows.Forms.Timer zTimer {
        'private Timer withEventsField_zTimer = new Windows.Forms.Timer();
        Private withEventsField_zTimer As Timer = New Timer()

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the timer. </summary>
        '''
        ''' <value> The z coordinate timer. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Property zTimer As Timer
            Get
                Return withEventsField_zTimer
            End Get
            Set(ByVal value As Timer)

                If withEventsField_zTimer IsNot Nothing Then
                    RemoveHandler withEventsField_zTimer.Tick, AddressOf zTimer_Tick
                End If

                withEventsField_zTimer = value

                If withEventsField_zTimer IsNot Nothing Then
                    AddHandler withEventsField_zTimer.Tick, AddressOf zTimer_Tick
                End If
            End Set
        End Property
        ''' <summary>   True to automatically sizing. </summary>

        Private zAutoSizing As Boolean = True
        ''' <summary>   Size of the automatic sizing. </summary>
        Private zAutoSizing_Size As Size = New Size(0, 0)
        'private Rectangle zContentRectangle = null;
        Private zContentRectangle As Rectangle
        ''' <summary>   The dock side. </summary>
        Private zDockSide As LineNumberDockSide = LineNumberDockSide.Left
        ''' <summary>   True if parent is scrolling. </summary>
        Private zParentIsScrolling As Boolean = False
        ''' <summary>   True to enable see through mode, false to disable it. </summary>
        Private zSeeThroughMode As Boolean = False
        ''' <summary>   True to gradient show. </summary>
        Private zGradient_Show As Boolean = True
        ''' <summary>   The gradient direction. </summary>
        Private zGradient_Direction As LinearGradientMode = LinearGradientMode.Horizontal
        ''' <summary>   The gradient start color. </summary>
        Private zGradient_StartColor As Color = Color.FromArgb(0, 0, 0, 0)
        ''' <summary>   The gradient end color. </summary>
        Private zGradient_EndColor As Color = Color.LightSteelBlue
        ''' <summary>   True to grid lines show. </summary>
        Private zGridLines_Show As Boolean = True
        ''' <summary>   The grid lines thickness. </summary>
        Private zGridLines_Thickness As Single = 1
        ''' <summary>   The grid lines style. </summary>
        Private zGridLines_Style As DashStyle = DashStyle.Dot
        ''' <summary>   The grid lines color. </summary>
        Private zGridLines_Color As Color = Color.SlateGray
        ''' <summary>   True to border lines show. </summary>
        Private zBorderLines_Show As Boolean = True
        ''' <summary>   The border lines thickness. </summary>
        Private zBorderLines_Thickness As Single = 1
        ''' <summary>   The border lines style. </summary>
        Private zBorderLines_Style As DashStyle = DashStyle.Dot
        ''' <summary>   The border lines color. </summary>
        Private zBorderLines_Color As Color = Color.SlateGray
        ''' <summary>   True to margin lines show. </summary>
        Private zMarginLines_Show As Boolean = True
        ''' <summary>   The margin lines side. </summary>
        Private zMarginLines_Side As LineNumberDockSide = LineNumberDockSide.Right
        ''' <summary>   The margin lines thickness. </summary>
        Private zMarginLines_Thickness As Single = 1
        ''' <summary>   The margin lines style. </summary>
        Private zMarginLines_Style As DashStyle = DashStyle.Solid
        ''' <summary>   The margin lines color. </summary>
        Private zMarginLines_Color As Color = Color.SlateGray
        ''' <summary>   True to line numbers show. </summary>
        Private zLineNumbers_Show As Boolean = True
        ''' <summary>   True to line numbers show leading zeroes. </summary>
        Private zLineNumbers_ShowLeadingZeroes As Boolean = True
        ''' <summary>   True to line numbers show as hexadecimal. </summary>
        Private zLineNumbers_ShowAsHexadecimal As Boolean = False
        ''' <summary>   True to line numbers clip by item rectangle. </summary>
        Private zLineNumbers_ClipByItemRectangle As Boolean = True
        ''' <summary>   The line numbers offset. </summary>
        Private zLineNumbers_Offset As Size = New Size(0, 0)
        ''' <summary>   The line numbers format. </summary>
        Private zLineNumbers_Format As String = "0"
        ''' <summary>   The line numbers alignment. </summary>
        Private zLineNumbers_Alignment As ContentAlignment = ContentAlignment.TopRight
        ''' <summary>   True to line numbers anti alias. </summary>
        Private zLineNumbers_AntiAlias As Boolean = True
        ''' <summary>   The line is. </summary>
        Private zLNIs As List(Of LineNumberItem) = New List(Of LineNumberItem)()
        ''' <summary>   The point in parent. </summary>
        Private zPointInParent As Point = New Point(0, 0)
        ''' <summary>   The point in me. </summary>
        Private zPointInMe As Point = New Point(0, 0)
        ''' <summary>   The parent in me. </summary>
        Private zParentInMe As Integer = 0

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>
        ''' Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with
        ''' default settings.
        ''' </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub New()
            If True Then
                SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
                SetStyle(ControlStyles.ResizeRedraw, True)
                SetStyle(ControlStyles.SupportsTransparentBackColor, True)
                SetStyle(ControlStyles.UserPaint, True)
                SetStyle(ControlStyles.AllPaintingInWmPaint, True)
                Margin = New Padding(0)
                Padding = New Padding(0, 0, 2, 0)
            End If

            If True Then
                zTimer.Enabled = True
                zTimer.Interval = 200
                zTimer.Stop()
            End If

            Update_SizeAndPosition()
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>
        ''' Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.
        ''' </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="e">    An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            AutoSize = False
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   This property is not relevant for this class. </summary>
        '''
        ''' <value> <see langword="true" /> if enabled; otherwise, <see langword="false" />. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        <ComponentModel.Browsable(False)>
        Public Overrides Property AutoSize As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the automatic sizing. </summary>
        '''
        ''' <value> The automatic sizing. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this property to automatically resize the control (and reposition it if needed).")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property AutoSizing As Boolean
            Get
                Return zAutoSizing
            End Get
            Set(ByVal value As Boolean)
                zAutoSizing = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the parent rich text box. </summary>
        '''
        ''' <value> The parent rich text box. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this property to enable LineNumbers for the chosen RichTextBox.")>
        <ComponentModel.Category("Add LineNumbers to")>
        Public Property ParentRichTextBox As RichTextBox
            Get
                Return zParent
            End Get
            Set(ByVal value As RichTextBox)
                zParent = value

                If zParent IsNot Nothing Then
                    Parent = zParent.Parent
                    zParent.Refresh()
                End If

                Text = ""
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the dock side. </summary>
        '''
        ''' <value> The dock side. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this property to dock the LineNumbers to a chosen side of the chosen RichTextBox.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property DockSide As LineNumberDockSide
            Get
                Return zDockSide
            End Get
            Set(ByVal value As LineNumberDockSide)
                zDockSide = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the see through mode. </summary>
        '''
        ''' <value> The see through mode. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this property to enable the control to act as an overlay ontop of the RichTextBox.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property _SeeThroughMode_ As Boolean
            Get
                Return zSeeThroughMode
            End Get
            Set(ByVal value As Boolean)
                zSeeThroughMode = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the show border lines. </summary>
        '''
        ''' <value> The show border lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("BorderLines are shown on all sides of the LineNumber control.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property Show_BorderLines As Boolean
            Get
                Return zBorderLines_Show
            End Get
            Set(ByVal value As Boolean)
                zBorderLines_Show = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the color of the border lines. </summary>
        '''
        ''' <value> The color of the border lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BorderLines_Color As Color
            Get
                Return zBorderLines_Color
            End Get
            Set(ByVal value As Color)
                zBorderLines_Color = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the border lines thickness. </summary>
        '''
        ''' <value> The border lines thickness. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BorderLines_Thickness As Single
            Get
                Return zBorderLines_Thickness
            End Get
            Set(ByVal value As Single)
                zBorderLines_Thickness = Math.Max(1, Math.Min(255, value))
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the border lines style. </summary>
        '''
        ''' <value> The border lines style. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BorderLines_Style As DashStyle
            Get
                Return zBorderLines_Style
            End Get
            Set(ByVal value As DashStyle)
                If value = DashStyle.Custom Then value = DashStyle.Solid
                zBorderLines_Style = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the show grid lines. </summary>
        '''
        ''' <value> The show grid lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("GridLines are the horizontal divider-lines shown above each LineNumber.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property Show_GridLines As Boolean
            Get
                Return zGridLines_Show
            End Get
            Set(ByVal value As Boolean)
                zGridLines_Show = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the color of the grid lines. </summary>
        '''
        ''' <value> The color of the grid lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property GridLines_Color As Color
            Get
                Return zGridLines_Color
            End Get
            Set(ByVal value As Color)
                zGridLines_Color = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the grid lines thickness. </summary>
        '''
        ''' <value> The grid lines thickness. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property GridLines_Thickness As Single
            Get
                Return zGridLines_Thickness
            End Get
            Set(ByVal value As Single)
                zGridLines_Thickness = Math.Max(1, Math.Min(255, value))
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the grid lines style. </summary>
        '''
        ''' <value> The grid lines style. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property GridLines_Style As DashStyle
            Get
                Return zGridLines_Style
            End Get
            Set(ByVal value As DashStyle)
                If value = DashStyle.Custom Then value = DashStyle.Solid
                zGridLines_Style = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the show margin lines. </summary>
        '''
        ''' <value> The show margin lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("MarginLines are shown on the Left or Right (or both in Height-mode) of the LineNumber control.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property Show_MarginLines As Boolean
            Get
                Return zMarginLines_Show
            End Get
            Set(ByVal value As Boolean)
                zMarginLines_Show = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the margin lines side. </summary>
        '''
        ''' <value> The margin lines side. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property MarginLines_Side As LineNumberDockSide
            Get
                Return zMarginLines_Side
            End Get
            Set(ByVal value As LineNumberDockSide)
                zMarginLines_Side = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the color of the margin lines. </summary>
        '''
        ''' <value> The color of the margin lines. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property MarginLines_Color As Color
            Get
                Return zMarginLines_Color
            End Get
            Set(ByVal value As Color)
                zMarginLines_Color = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the margin lines thickness. </summary>
        '''
        ''' <value> The margin lines thickness. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property MarginLines_Thickness As Single
            Get
                Return zMarginLines_Thickness
            End Get
            Set(ByVal value As Single)
                zMarginLines_Thickness = Math.Max(1, Math.Min(255, value))
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the margin lines style. </summary>
        '''
        ''' <value> The margin lines style. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property MarginLines_Style As DashStyle
            Get
                Return zMarginLines_Style
            End Get
            Set(ByVal value As DashStyle)
                If value = DashStyle.Custom Then value = DashStyle.Solid
                zMarginLines_Style = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the show background gradient. </summary>
        '''
        ''' <value> The show background gradient. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("The BackgroundGradient is a gradual blend of two colors, shown in the back of each LineNumber's item-area.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property Show_BackgroundGradient As Boolean
            Get
                Return zGradient_Show
            End Get
            Set(ByVal value As Boolean)
                zGradient_Show = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the color of the background gradient alpha. </summary>
        '''
        ''' <value> The color of the background gradient alpha. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BackgroundGradient_AlphaColor As Color
            Get
                Return zGradient_StartColor
            End Get
            Set(ByVal value As Color)
                zGradient_StartColor = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the color of the background gradient beta. </summary>
        '''
        ''' <value> The color of the background gradient beta. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BackgroundGradient_BetaColor As Color
            Get
                Return zGradient_EndColor
            End Get
            Set(ByVal value As Color)
                zGradient_EndColor = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the background gradient direction. </summary>
        '''
        ''' <value> The background gradient direction. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Appearance")>
        Public Property BackgroundGradient_Direction As LinearGradientMode
            Get
                Return zGradient_Direction
            End Get
            Set(ByVal value As LinearGradientMode)
                zGradient_Direction = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the show line nrs. </summary>
        '''
        ''' <value> The show line nrs. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Category("Additional Behavior")>
        Public Property Show_LineNrs As Boolean
            Get
                Return zLineNumbers_Show
            End Get
            Set(ByVal value As Boolean)
                zLineNumbers_Show = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs clipped by item rectangle. </summary>
        '''
        ''' <value> The line nrs clipped by item rectangle. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this to set whether the LineNumbers are allowed to spill out of their item-area, or should be clipped by it.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_ClippedByItemRectangle As Boolean
            Get
                Return zLineNumbers_ClipByItemRectangle
            End Get
            Set(ByVal value As Boolean)
                zLineNumbers_ClipByItemRectangle = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs leading zeroes. </summary>
        '''
        ''' <value> The line nrs leading zeroes. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this to set whether the LineNumbers should have leading zeroes (based on the total amount of textlines).")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_LeadingZeroes As Boolean
            Get
                Return zLineNumbers_ShowLeadingZeroes
            End Get
            Set(ByVal value As Boolean)
                zLineNumbers_ShowLeadingZeroes = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs as hexadecimal. </summary>
        '''
        ''' <value> The line nrs as hexadecimal. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this to set whether the LineNumbers should be shown as hexadecimal values.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_AsHexadecimal As Boolean
            Get
                Return zLineNumbers_ShowAsHexadecimal
            End Get
            Set(ByVal value As Boolean)
                zLineNumbers_ShowAsHexadecimal = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs offset. </summary>
        '''
        ''' <value> The line nrs offset. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this property to manually reposition the LineNumbers, relative to their current location.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_Offset As Size
            Get
                Return zLineNumbers_Offset
            End Get
            Set(ByVal value As Size)
                zLineNumbers_Offset = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs alignment. </summary>
        '''
        ''' <value> The line nrs alignment. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this to align the LineNumbers to a chosen corner (or center) within their item-area.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_Alignment As ContentAlignment
            Get
                Return zLineNumbers_Alignment
            End Get
            Set(ByVal value As ContentAlignment)
                zLineNumbers_Alignment = value
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the line nrs anti alias. </summary>
        '''
        ''' <value> The line nrs anti alias. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Description("Use this to apply Anti-Aliasing to the LineNumbers (high quality). Some fonts will look better without it, though.")>
        <ComponentModel.Category("Additional Behavior")>
        Public Property LineNrs_AntiAlias As Boolean
            Get
                Return zLineNumbers_AntiAlias
            End Get
            Set(ByVal value As Boolean)
                zLineNumbers_AntiAlias = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the font of the text displayed by the control. </summary>
        '''
        ''' <value>
        ''' The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The
        ''' default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" />
        ''' property.
        ''' </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.Browsable(True)>
        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
                Refresh()
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Gets or sets the text associated with this control. </summary>
        '''
        ''' <value> The text associated with this control. </value>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        <ComponentModel.DefaultValue("")>
        <ComponentModel.AmbientValue("")>
        <ComponentModel.Browsable(False)>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = ""
                Invalidate()
            End Set
        End Property

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>
        ''' Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        ''' </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="e">    An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            If DesignMode = True Then Refresh()
            MyBase.OnSizeChanged(e)
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>
        ''' Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.
        ''' </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="e">    An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Protected Overrides Sub OnLocationChanged(ByVal e As EventArgs)
            If DesignMode = True Then Refresh()
            MyBase.OnLocationChanged(e)
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>
        ''' Forces the control to invalidate its client area and immediately redraw itself and any child
        ''' controls.
        ''' </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Overrides Sub Refresh()
            '   Note: don't change the order here, first the Mybase.Refresh, then the Update_SizeAndPosition.
            MyBase.Refresh()
            Update_SizeAndPosition()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Updates the size and position. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Sub Update_SizeAndPosition()
            If AutoSize = True Then Return
            If Dock = DockStyle.Bottom Or Dock = DockStyle.Fill Or Dock = DockStyle.Top Then Return
            Dim zNewLocation = Location
            Dim zNewSize = Size

            If zAutoSizing = True Then
                'switch (true) {
                '	case zParent == null:
                If zParent Is Nothing Then
                    ' --- ReminderMessage sizing
                    If zAutoSizing_Size.Width > 0 Then zNewSize.Width = zAutoSizing_Size.Width
                    If zAutoSizing_Size.Height > 0 Then zNewSize.Height = zAutoSizing_Size.Height
                    Size = zNewSize

                    ' break;
                ElseIf Dock = DockStyle.Left Or Dock = DockStyle.Right Then
                    '--- zParent isNot Nothing for the following cases
                    'case this.Dock == DockStyle.Left | this.Dock == DockStyle.Right:
                    If zAutoSizing_Size.Width > 0 Then zNewSize.Width = zAutoSizing_Size.Width
                    Width = zNewSize.Width

                    'break;
                ElseIf zDockSide <> LineNumberDockSide.None Then
                    ' --- DockSide is active L/R/H
                    'case zDockSide != LineNumberDockSide.None:
                    If zAutoSizing_Size.Width > 0 Then zNewSize.Width = zAutoSizing_Size.Width
                    zNewSize.Height = zParent.Height
                    If zDockSide = LineNumberDockSide.Left Then zNewLocation.X = zParent.Left - zNewSize.Width - 1
                    If zDockSide = LineNumberDockSide.Right Then zNewLocation.X = zParent.Right + 1
                    zNewLocation.Y = zParent.Top
                    Location = zNewLocation
                    Size = zNewSize

                    'break;
                ElseIf zDockSide = LineNumberDockSide.None Then
                    ' --- DockSide = None, but AutoSizing is still setting the Width
                    'case zDockSide == LineNumberDockSide.None:
                    If zAutoSizing_Size.Width > 0 Then zNewSize.Width = zAutoSizing_Size.Width
                    Size = zNewSize

                    'break;
                End If
            Else
                ' --- No AutoSizing
                'switch (true) {
                If zParent Is Nothing Then
                    'case zParent == null:
                    ' --- ReminderMessage sizing
                    If zAutoSizing_Size.Width > 0 Then zNewSize.Width = zAutoSizing_Size.Width
                    If zAutoSizing_Size.Height > 0 Then zNewSize.Height = zAutoSizing_Size.Height
                    Size = zNewSize

                    'break;
                ElseIf zDockSide <> LineNumberDockSide.None Then
                    ' --- No AutoSizing, but DockSide L/R/H is active so height and position need updates.
                    'case zDockSide != LineNumberDockSide.None:
                    zNewSize.Height = zParent.Height
                    If zDockSide = LineNumberDockSide.Left Then zNewLocation.X = zParent.Left - zNewSize.Width - 1
                    If zDockSide = LineNumberDockSide.Right Then zNewLocation.X = zParent.Right + 1
                    zNewLocation.Y = zParent.Top
                    Location = zNewLocation
                    Size = zNewSize

                    'break;
                End If
            End If
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Updates the visible line number items.
        ''' This Sub determines which textlines are visible in the ParentRichTextBox, and makes LineNumberItems (LineNumber + ItemRectangle)
        ''' for each visible line. They are put into the zLNIs List that will be used by the OnPaint event to draw the LineNumberItems. 
        '''              </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Sub Update_VisibleLineNumberItems()

            ' ################
            Dim tmpY As Integer
            ' ###############
            zLNIs.Clear()
            zAutoSizing_Size = New Size(0, 0)
            zLineNumbers_Format = "0"
            'initial setting
            '   To measure the LineNumber's width, its Format 0 is replaced by w as that is likely to be one of the widest characters in non-monospace fonts. 
            If zAutoSizing = True Then zAutoSizing_Size = New Size(TextRenderer.MeasureText(zLineNumbers_Format.Replace("0"c, "W"c), Font).Width, 0)
            'zAutoSizing_Size = new Size(TextRenderer.MeasureText(zLineNumbers_Format.Replace("0".ToCharArray(), "W".ToCharArray()), this.Font).Width, 0);

            If zParent Is Nothing OrElse String.IsNullOrEmpty(zParent.Text) Then Return

            ' --- Make sure the LineNumbers are aligning to the same height as the zParent textlines by converting to screencoordinates
            '   and using that as an offset that gets added to the points for the LineNumberItems
            zPointInParent = zParent.PointToScreen(zParent.ClientRectangle.Location)
            zPointInMe = PointToScreen(New Point(0, 0))
            '   zParentInMe is the vertical offset to make the LineNumberItems line up with the textlines in zParent.
            zParentInMe = zPointInParent.Y - zPointInMe.Y + 1
            '   The first visible LineNumber may not be the first visible line of text in the RTB if the LineNumbercontrol's .Top is lower on the form than
            '   the .Top of the parent RichTextBox. Therefor, zPointInParent will now be used to find zPointInMe's equivalent height in zParent, 
            '   which is needed to find the best StartIndex later on.
            zPointInParent = zParent.PointToClient(zPointInMe)

            ' --- NOTES: 
            '   Additional complication is the fact that when wordwrap is enabled on the RTB, the wordwrapped text spills into the RTB.Lines collection, 
            '   so we need to split the text into lines ourselves, and use the Index of each zSplit-line's first character instead of the RTB's.
            Dim zSplit As String() = zParent.Text.Split(vbCrLf.ToCharArray())

            If zSplit.Length < 2 Then
                '   Just one line in the text = one linenumber
                '   NOTE:  zContentRectangle is built by the zParent.ContentsResized event.
                Dim zPoint = zParent.GetPositionFromCharIndex(0)
                zLNIs.Add(New LineNumberItem(1, New Rectangle(New Point(0, zPoint.Y - 1 + zParentInMe), New Size(Width, zContentRectangle.Height - zPoint.Y))))
            Else
                '   Multiple lines, but store only those LineNumberItems for lines that are visible.
                Dim zTimeSpan As TimeSpan = New TimeSpan(Now.Ticks)
                Dim zPoint As Point = New Point(0, 0)
                Dim zStartIndex = 0
                Dim zSplitStartLine = 0
                Dim zA = zParent.Text.Length - 1
                ' #########################

                'this.FindStartIndex(ref zStartIndex, ref zA, ref zPointInParent.Y);
                tmpY = zPointInParent.Y
                FindStartIndex(zStartIndex, zA, tmpY)
                zPointInParent.Y = tmpY

                ' ################


                '   zStartIndex now holds the index of a character in the first visible line from zParent.Text
                '   Now it will be pointed at the first character of that line (chr(10) = Linefeed part of the vbCrLf constant)
                zStartIndex = Math.Max(0, Math.Min(zParent.Text.Length - 1, zParent.Text.Substring(0, zStartIndex).LastIndexOf(Chr(10)) + 1))

                '   We now need to find out which zSplit-line that character is in, by counting the vbCrlf appearances that come before it.
                zSplitStartLine = Math.Max(0, zParent.Text.Substring(0, zStartIndex).Split(vbCrLf.ToCharArray()).Length - 1)

                '   zStartIndex starts off pointing at the first character of the first visible line, and will be then be pointed to 
                '   the index of the first character on the next line.
                For zA = zSplitStartLine To zSplit.Length - 1
                    zPoint = zParent.GetPositionFromCharIndex(zStartIndex)
                    zStartIndex += Math.Max(1, zSplit(zA).Length + 1)
                    If zPoint.Y + zParentInMe > Height Then Exit For ' TODO: might not be correct. Was : Exit For
                    '   For performance reasons, the list of LineNumberItems (zLNIs) is first built with only the location of its 
                    '   itemrectangle being used. The height of those rectangles will be computed afterwards by comparing the items' Y coordinates.
                    zLNIs.Add(New LineNumberItem(zA + 1, New Rectangle(0, zPoint.Y - 1 + zParentInMe, Width, 1)))

                    If zParentIsScrolling = True AndAlso Now.Ticks > zTimeSpan.Ticks + 500000 Then
                        '   The more lines there are in the RTB, the slower the RTB's .GetPositionFromCharIndex() method becomes
                        '   To avoid those delays from interfering with the scrollingspeed, this speedbased exit for is applied (0.05 sec)
                        '   zLNIs will have at least 1 item, and if that's the only one, then change its location to 0,0 to make it readable
                        If zLNIs.Count = 1 Then zLNIs(0).Rectangle.Y = 0
                        ' zLNIs(0).Rectangle.Y = 0;

                        zParentIsScrolling = False
                        zTimer.Start()
                        Exit For ' TODO: might not be correct. Was : Exit For
                    End If
                Next

                If zLNIs.Count = 0 Then Return

                '   Add an extra placeholder item to the end, to make the heightcomputation easier
                If zA < zSplit.Length Then
                    '   getting here means the for/next loop was exited before reaching the last zSplit textline
                    '   zStartIndex will still be pointing to the startcharacter of the next line, so we can use that:
                    zPoint = zParent.GetPositionFromCharIndex(Math.Min(zStartIndex, zParent.Text.Length - 1))
                    zLNIs.Add(New LineNumberItem(-1, New Rectangle(0, zPoint.Y - 1 + zParentInMe, 0, 0)))
                Else
                    '   getting here means the for/next loop ran to the end (zA is now zSplit.Length). 
                    zLNIs.Add(New LineNumberItem(-1, New Rectangle(0, zContentRectangle.Bottom, 0, 0)))
                End If

                '   And now we can easily compute the height of the LineNumberItems by comparing each item's Y coordinate with that of the next line.
                '   There's at least two items in the list, and the last item is a "nextline-placeholder" that will be removed.
                For zA = 0 To zLNIs.Count - 2
                    zLNIs(zA).Rectangle.Height = Math.Max(1, zLNIs(zA + 1).Rectangle.Y - zLNIs(zA).Rectangle.Y)
                    'zLNIs(zA).Rectangle.Height = Math.Max(1, zLNIs(zA + 1).Rectangle.Y - zLNIs(zA).Rectangle.Y);
                Next
                '   Removing the placeholder item
                zLNIs.RemoveAt(zLNIs.Count - 1)

                ' Set the Format to the width of the highest possible number so that LeadingZeroes shows the correct amount of zeroes.
                If zLineNumbers_ShowAsHexadecimal = True Then
                    'zLineNumbers_Format = "".PadRight(zSplit.Length.ToString("X").Length, "0");
                    zLineNumbers_Format = "".PadRight(zSplit.Length.ToString("X").Length, "0"c)
                Else
                    'zLineNumbers_Format = "".PadRight(zSplit.Length.ToString().Length, "0");
                    zLineNumbers_Format = "".PadRight(zSplit.Length.ToString().Length, "0"c)
                End If
            End If

            '   To measure the LineNumber's width, its Format 0 is replaced by w as that is likely to be one of the widest characters in non-monospace fonts. 
            If zAutoSizing = True Then zAutoSizing_Size = New Size(TextRenderer.MeasureText(zLineNumbers_Format.Replace("0"c, "W"c), Font).Width, 0)
            'zAutoSizing_Size = new Size(TextRenderer.MeasureText(zLineNumbers_Format.Replace("0".ToCharArray(), "W".ToCharArray()), this.Font).Width, 0);
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Searches for the first start index.
        ''' FindStartIndex is a recursive Sub (one that calls itself) to compute the first visible line that should have a LineNumber.
        '''               </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="zMin">     [in,out] The minimum. </param>
        ''' <param name="zMax">     [in,out] The maximum. </param>
        ''' <param name="zTarget">  [in,out] Target for the. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Sub FindStartIndex(ByRef zMin As Integer, ByRef zMax As Integer, ByRef zTarget As Integer)
            '   Recursive Sub to compute best starting index - only run when zParent is known to exist
            If zMax = zMin + 1 Or zMin = (zMax + zMin) / 2 Then Return

            If zParent.GetPositionFromCharIndex((zMax + zMin) / 2).Y = zTarget Then
                'switch (zParent.GetPositionFromCharIndex((zMax + zMin) / 2).Y) {
                '	case  // ERROR: Case labels with binary operators are unsupported : Equality
                '   BestStartIndex found
                'break;
                zMin = (zMax + zMin) / 2
            ElseIf zParent.GetPositionFromCharIndex((zMax + zMin) / 2).Y > zTarget Then
                'case  // ERROR: Case labels with binary operators are unsupported : GreaterThan
                'zTarget:
                '   Look again, in lower half
                zMax = (zMax + zMin) / 2
                'break;
                FindStartIndex(zMin, zMax, zTarget)
            ElseIf zParent.GetPositionFromCharIndex((zMax + zMin) / 2).Y < 0 Then
                ' case  // ERROR: Case labels with binary operators are unsupported : LessThan
                '0:
                '   Look again, in top half
                zMin = (zMax + zMin) / 2
                FindStartIndex(zMin, zMax, zTarget)
                'break;
            End If
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event. 
        ''' OnPaint will go through the enabled elements (vertical ReminderMessage, GridLines, LineNumbers, BorderLines, MarginLines) and will
        ''' draw them if enabled. At the same time, it will build GraphicsPaths for each of those elements (that are enabled), which will be used 
        ''' in SeeThroughMode (if it's active): the figures in the GraphicsPaths will form a customized outline for the control by setting them as the 
        ''' Region of the LineNumber control. Note: the vertical ReminderMessages are only drawn during designtime. 
        '''             </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="e">    A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the
        '''                     event data. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            '   Build the list of visible LineNumberItems (= zLNIs) first. (doesn't take long, so it can stay in OnPaint)
            Update_VisibleLineNumberItems()
            MyBase.OnPaint(e)

            ' --- QualitySettings
            If zLineNumbers_AntiAlias = True Then
                e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            Else
                e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SystemDefault
            End If

            ' --- Local Declarations
            Dim zTextToShow = ""
            Dim zReminderToShow = ""
            Dim zSF As StringFormat = New StringFormat()
            Dim zTextSize As SizeF = Nothing
            Dim zPen As Pen = New Pen(ForeColor)
            Dim zBrush As SolidBrush = New SolidBrush(ForeColor)
            Dim zPoint As Point = New Point(0, 0)
            Dim zItemClipRectangle As Rectangle = New Rectangle(0, 0, 0, 0)

            '   NOTE: The GraphicsPaths are only used for SeeThroughMode
            '   FillMode.Winding: combined outline ( Alternate: XOR'ed outline )
            Dim zGP_GridLines As GraphicsPath = New GraphicsPath(FillMode.Winding)
            Dim zGP_BorderLines As GraphicsPath = New GraphicsPath(FillMode.Winding)
            Dim zGP_MarginLines As GraphicsPath = New GraphicsPath(FillMode.Winding)
            Dim zGP_LineNumbers As GraphicsPath = New GraphicsPath(FillMode.Winding)
            Dim zRegion As Region = New Region(ClientRectangle)


            ' ----------------------------------------------
            ' --- DESIGNTIME / NO VISIBLE ITEMS
            If DesignMode = True Then
                '   Show a vertical reminder message
                If zParent Is Nothing Then
                    zReminderToShow = "-!- Set ParentRichTextBox -!-"
                Else
                    If zLNIs.Count = 0 Then zReminderToShow = "LineNrs (  " & zParent.Name & "  )"
                End If

                If zReminderToShow.Length > 0 Then
                    ' --- Centering and Rotation for the reminder message
                    e.Graphics.TranslateTransform(Width / 2, Height / 2)
                    e.Graphics.RotateTransform(-90)
                    zSF.Alignment = StringAlignment.Center
                    zSF.LineAlignment = StringAlignment.Center
                    ' --- Show the reminder message (with small shadow)
                    zTextSize = e.Graphics.MeasureString(zReminderToShow, Font, zPoint, zSF)
                    e.Graphics.DrawString(zReminderToShow, Font, Brushes.WhiteSmoke, 1, 1, zSF)
                    e.Graphics.DrawString(zReminderToShow, Font, Brushes.Firebrick, 0, 0, zSF)
                    e.Graphics.ResetTransform()

                    'Rectangle zReminderRectangle = new Rectangle(this.Width / 2 - zTextSize.Height / 2, this.Height / 2 - zTextSize.Width / 2, zTextSize.Height, zTextSize.Width);
                    Dim zReminderRectangle As Rectangle = New Rectangle(Width / 2 - zTextSize.Height / 2, Height / 2 - zTextSize.Width / 2, zTextSize.Height, zTextSize.Width)
                    zGP_LineNumbers.AddRectangle(zReminderRectangle)
                    zGP_LineNumbers.CloseFigure()

                    If zAutoSizing = True Then
                        zReminderRectangle.Inflate(zTextSize.Height * 0.2, zTextSize.Width * 0.1)
                        'zReminderRectangle.Inflate(zTextSize.Height * 0.2, zTextSize.Width * 0.1);
                        zAutoSizing_Size = New Size(zReminderRectangle.Width, zReminderRectangle.Height)
                    End If
                End If
            End If


            ' ----------------------------------------------
            ' --- DESIGN OR RUNTIME / WITH VISIBLE ITEMS (which means zParent exists)
            If zLNIs.Count > 0 Then
                '   The visible LineNumberItems with their BackgroundGradient and GridLines
                '   Loop through every visible LineNumberItem
                Dim zLGB As LinearGradientBrush = Nothing
                zPen = New Pen(zGridLines_Color, zGridLines_Thickness)
                zPen.DashStyle = zGridLines_Style
                zSF.Alignment = StringAlignment.Near
                zSF.LineAlignment = StringAlignment.Near
                'zSF.FormatFlags = StringFormatFlags.FitBlackBox + StringFormatFlags.NoClip + StringFormatFlags.NoWrap;
                zSF.FormatFlags = StringFormatFlags.FitBlackBox Or StringFormatFlags.NoClip Or StringFormatFlags.NoWrap

                For zA = 0 To zLNIs.Count - 1
                    ' --- BackgroundGradient
                    If zGradient_Show = True Then
                        'zLGB = new Drawing2D.LinearGradientBrush(zLNIs(zA).Rectangle, zGradient_StartColor, zGradient_EndColor, zGradient_Direction);
                        zLGB = New LinearGradientBrush(zLNIs(zA).Rectangle, zGradient_StartColor, zGradient_EndColor, zGradient_Direction)
                        e.Graphics.FillRectangle(zLGB, zLNIs(zA).Rectangle)
                        'e.Graphics.FillRectangle(zLGB, zLNIs(zA).Rectangle);
                    End If

                    ' --- GridLines
                    If zGridLines_Show = True Then
                        e.Graphics.DrawLine(zPen, New Point(0, zLNIs(zA).Rectangle.Y), New Point(Width, zLNIs(zA).Rectangle.Y))
                        'e.Graphics.DrawLine(zPen, new Point(0, zLNIs(zA).Rectangle.Y), new Point(this.Width, zLNIs(zA).Rectangle.Y));

                        '   NOTE: Every item in a GraphicsPath is a closed figure, so instead of adding gridlines as lines, we'll add them
                        '   as rectangles that loop out of sight. Their height uses the zContentRectangle which is the maxsize of 
                        '   the ParentRichTextBox's contents. 
                        '   NOTE: Slight adjustment needed when the first item has a negative Y coordinate. 
                        '   This explains the " - zLNIs(0).Rectangle.Y" (which adds the negative size to the height 
                        '   to make sure the rectangle's bottompart stays out of sight) 
                        'zGP_GridLines.AddRectangle(new Rectangle(-zGridLines_Thickness, zLNIs(zA).Rectangle.Y, this.Width + zGridLines_Thickness * 2, this.Height - zLNIs(0).Rectangle.Y + zGridLines_Thickness));
                        zGP_GridLines.AddRectangle(New Rectangle(-zGridLines_Thickness, zLNIs(zA).Rectangle.Y, Width + zGridLines_Thickness * 2, Height - zLNIs(zA).Rectangle.Y + zGridLines_Thickness))
                        zGP_GridLines.CloseFigure()
                    End If

                    ' --- LineNumbers
                    If zLineNumbers_Show = True Then
                        '   TextFormatting
                        If zLineNumbers_ShowLeadingZeroes = True Then
                            'zTextToShow = (zLineNumbers_ShowAsHexadecimal ? zLNIs(zA).LineNumber.ToString("X") : zLNIs(zA).LineNumber.ToString(zLineNumbers_Format));
                            zTextToShow = If(zLineNumbers_ShowAsHexadecimal, zLNIs(zA).LineNumber.ToString("X"), zLNIs(zA).LineNumber.ToString(zLineNumbers_Format))
                        Else
                            zTextToShow = (If(zLineNumbers_ShowAsHexadecimal, zLNIs(zA).LineNumber.ToString("X"), zLNIs(zA).LineNumber.ToString()))
                            'zTextToShow = (zLineNumbers_ShowAsHexadecimal ? zLNIs(zA).LineNumber.ToString("X") : zLNIs(zA).LineNumber.ToString);
                        End If

                        '   TextSizing
                        zTextSize = e.Graphics.MeasureString(zTextToShow, Font, zPoint, zSF)
                        '   TextAlignment and positioning   (zPoint = TopLeftCornerPoint of the text)
                        '   TextAlignment, padding, manual offset (via LineNrs_Offset) and zTextSize are all included in the calculation of zPoint. 
                        Select Case zLineNumbers_Alignment
                            Case ContentAlignment.TopLeft
                                zPoint = New Point(zLNIs(zA).Rectangle.Left + Padding.Left + zLineNumbers_Offset.Width, zLNIs(zA).Rectangle.Top + Padding.Top + zLineNumbers_Offset.Height)
                            Case ContentAlignment.MiddleLeft
                                zPoint = New Point(zLNIs(zA).Rectangle.Left + Padding.Left + zLineNumbers_Offset.Width, zLNIs(zA).Rectangle.Top + zLNIs(zA).Rectangle.Height / 2 + zLineNumbers_Offset.Height - zTextSize.Height / 2)
                            Case ContentAlignment.BottomLeft
                                zPoint = New Point(zLNIs(zA).Rectangle.Left + Padding.Left + zLineNumbers_Offset.Width, zLNIs(zA).Rectangle.Bottom - Padding.Bottom + 1 + zLineNumbers_Offset.Height - zTextSize.Height)
                            Case ContentAlignment.TopCenter
                                zPoint = New Point(zLNIs(zA).Rectangle.Width / 2 + zLineNumbers_Offset.Width - zTextSize.Width / 2, zLNIs(zA).Rectangle.Top + Padding.Top + zLineNumbers_Offset.Height)
                            Case ContentAlignment.MiddleCenter
                                zPoint = New Point(zLNIs(zA).Rectangle.Width / 2 + zLineNumbers_Offset.Width - zTextSize.Width / 2, zLNIs(zA).Rectangle.Top + zLNIs(zA).Rectangle.Height / 2 + zLineNumbers_Offset.Height - zTextSize.Height / 2)
                            Case ContentAlignment.BottomCenter
                                zPoint = New Point(zLNIs(zA).Rectangle.Width / 2 + zLineNumbers_Offset.Width - zTextSize.Width / 2, zLNIs(zA).Rectangle.Bottom - Padding.Bottom + 1 + zLineNumbers_Offset.Height - zTextSize.Height)
                            Case ContentAlignment.TopRight
                                zPoint = New Point(zLNIs(zA).Rectangle.Right - Padding.Right + zLineNumbers_Offset.Width - zTextSize.Width, zLNIs(zA).Rectangle.Top + Padding.Top + zLineNumbers_Offset.Height)
                            Case ContentAlignment.MiddleRight
                                zPoint = New Point(zLNIs(zA).Rectangle.Right - Padding.Right + zLineNumbers_Offset.Width - zTextSize.Width, zLNIs(zA).Rectangle.Top + zLNIs(zA).Rectangle.Height / 2 + zLineNumbers_Offset.Height - zTextSize.Height / 2)
                            Case ContentAlignment.BottomRight
                                zPoint = New Point(zLNIs(zA).Rectangle.Right - Padding.Right + zLineNumbers_Offset.Width - zTextSize.Width, zLNIs(zA).Rectangle.Bottom - Padding.Bottom + 1 + zLineNumbers_Offset.Height - zTextSize.Height)
                        End Select
                        '   TextClipping
                        zItemClipRectangle = New Rectangle(zPoint, zTextSize.ToSize())

                        If zLineNumbers_ClipByItemRectangle = True Then
                            '   If selected, the text will be clipped so that it doesn't spill out of its own LineNumberItem-area.
                            '   Only the part of the text inside the LineNumberItem.Rectangle should be visible, so intersect with the ItemRectangle
                            '   The SetClip method temporary restricts the drawing area of the control for whatever is drawn next.
                            zItemClipRectangle.Intersect(zLNIs(zA).Rectangle)
                            e.Graphics.SetClip(zItemClipRectangle)
                        End If
                        '   TextDrawing
                        e.Graphics.DrawString(zTextToShow, Font, zBrush, zPoint, zSF)
                        e.Graphics.ResetClip()
                        '   The GraphicsPath for the LineNumber is just a rectangle behind the text, to keep the paintingspeed high and avoid ugly artifacts.
                        zGP_LineNumbers.AddRectangle(zItemClipRectangle)
                        zGP_LineNumbers.CloseFigure()
                    End If
                Next

                ' --- GridLinesThickness and Linestyle in SeeThroughMode. All GraphicsPath lines are drawn as solid to keep the paintingspeed high.
                If zGridLines_Show = True Then
                    zPen.DashStyle = DashStyle.Solid
                    zGP_GridLines.Widen(zPen)
                End If

                ' --- Memory CleanUp
                If zLGB IsNot Nothing Then zLGB.Dispose()
            End If


            ' ----------------------------------------------
            ' --- DESIGN OR RUNTIME / ALWAYS
            'Point zP_Left = new Point(Math.Floor(zBorderLines_Thickness / 2), Math.Floor(zBorderLines_Thickness / 2));
            'Point zP_Right = new Point(this.Width - Math.Ceiling(zBorderLines_Thickness / 2), this.Height - Math.Ceiling(zBorderLines_Thickness / 2));

            Dim zP_Left As Point = New Point(Math.Floor(zBorderLines_Thickness / 2), Math.Floor(zBorderLines_Thickness / 2))
            Dim zP_Right As Point = New Point(Width - Math.Ceiling(zBorderLines_Thickness / 2), Height - Math.Ceiling(zBorderLines_Thickness / 2))

            ' --- BorderLines 
            Dim zBorderLines_Points As Point() = {New Point(zP_Left.X, zP_Left.Y), New Point(zP_Right.X, zP_Left.Y), New Point(zP_Right.X, zP_Right.Y), New Point(zP_Left.X, zP_Right.Y), New Point(zP_Left.X, zP_Left.Y)}

            If zBorderLines_Show = True Then
                zPen = New Pen(zBorderLines_Color, zBorderLines_Thickness)
                zPen.DashStyle = zBorderLines_Style
                e.Graphics.DrawLines(zPen, zBorderLines_Points)
                zGP_BorderLines.AddLines(zBorderLines_Points)
                zGP_BorderLines.CloseFigure()
                '   BorderThickness and Style for SeeThroughMode
                zPen.DashStyle = DashStyle.Solid
                zGP_BorderLines.Widen(zPen)
            End If


            ' --- MarginLines 
            If zMarginLines_Show = True AndAlso zMarginLines_Side > LineNumberDockSide.None Then
                zP_Left = New Point(-zMarginLines_Thickness, -zMarginLines_Thickness)
                zP_Right = New Point(Width + zMarginLines_Thickness, Height + zMarginLines_Thickness)
                zPen = New Pen(zMarginLines_Color, zMarginLines_Thickness)
                zPen.DashStyle = zMarginLines_Style

                If zMarginLines_Side = LineNumberDockSide.Left Or zMarginLines_Side = LineNumberDockSide.Height Then
                    e.Graphics.DrawLine(zPen, New Point(Math.Floor(zMarginLines_Thickness / 2), 0), New Point(Math.Floor(zMarginLines_Thickness / 2), Height - 1))
                    zP_Left = New Point(Math.Ceiling(zMarginLines_Thickness / 2), -zMarginLines_Thickness)
                End If

                If zMarginLines_Side = LineNumberDockSide.Right Or zMarginLines_Side = LineNumberDockSide.Height Then
                    e.Graphics.DrawLine(zPen, New Point(Width - Math.Ceiling(zMarginLines_Thickness / 2), 0), New Point(Width - Math.Ceiling(zMarginLines_Thickness / 2), Height - 1))
                    zP_Right = New Point(Width - Math.Ceiling(zMarginLines_Thickness / 2), Height + zMarginLines_Thickness)
                End If
                '   GraphicsPath for the MarginLines(s):
                '   MarginLines(s) are drawn as a rectangle connecting the zP_Left and zP_Right points, which are either inside or 
                '   outside of sight, depending on whether the MarginLines at that side is visible. zP_Left: TopLeft and ZP_Right: BottomRight
                zGP_MarginLines.AddRectangle(New Rectangle(zP_Left, New Size(zP_Right.X - zP_Left.X, zP_Right.Y - zP_Left.Y)))
                zPen.DashStyle = DashStyle.Solid
                zGP_MarginLines.Widen(zPen)
            End If


            ' ----------------------------------------------
            ' --- SeeThroughMode
            '   combine all the GraphicsPaths (= zGP_... ) and set them as the region for the control.
            If zSeeThroughMode = True Then
                zRegion.MakeEmpty()
                zRegion.Union(zGP_BorderLines)
                zRegion.Union(zGP_MarginLines)
                zRegion.Union(zGP_GridLines)
                zRegion.Union(zGP_LineNumbers)
            End If

            ' --- Region
            If zRegion.GetBounds(e.Graphics).IsEmpty = True Then
                '   Note: If the control is in a condition that would show it as empty, then a border-region is still drawn regardless of it's borders on/off state.
                '   This is added to make sure that the bounds of the control are never lost (it would remain empty if this was not done).
                zGP_BorderLines.AddLines(zBorderLines_Points)
                zGP_BorderLines.CloseFigure()
                zPen = New Pen(zBorderLines_Color, 1)
                zPen.DashStyle = DashStyle.Solid
                zGP_BorderLines.Widen(zPen)
                zRegion = New Region(zGP_BorderLines)
            End If

            Region = zRegion


            ' ----------------------------------------------
            ' --- Memory CleanUp
            If zPen IsNot Nothing Then zPen.Dispose()
            If zBrush IsNot Nothing Then zPen.Dispose()
            If zRegion IsNot Nothing Then zRegion.Dispose()
            If zGP_GridLines IsNot Nothing Then zGP_GridLines.Dispose()
            If zGP_BorderLines IsNot Nothing Then zGP_BorderLines.Dispose()
            If zGP_MarginLines IsNot Nothing Then zGP_MarginLines.Dispose()
            If zGP_LineNumbers IsNot Nothing Then zGP_LineNumbers.Dispose()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Event handler. Called by zTimer for tick events. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   Source of the event. </param>
        ''' <param name="e">        Event information. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        Private Sub zTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            zParentIsScrolling = False
            zTimer.Stop()
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Event handler. Called by zParent for changed events. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   Source of the event. </param>
        ''' <param name="e">        Event information. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub zParent_Changed(ByVal sender As Object, ByVal e As EventArgs)
            Refresh()
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Event handler. Called by zParent for scroll events. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   Source of the event. </param>
        ''' <param name="e">        Event information. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub zParent_Scroll(ByVal sender As Object, ByVal e As EventArgs)
            zParentIsScrolling = True
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Event handler. Called by zParent for contents resized events. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   Source of the event. </param>
        ''' <param name="e">        Contents resized event information. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub zParent_ContentsResized(ByVal sender As Object, ByVal e As ContentsResizedEventArgs)
            zContentRectangle = e.NewRectangle
            Refresh()
            Invalidate()
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Event handler. Called by zParent for disposed events. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="sender">   Source of the event. </param>
        ''' <param name="e">        Event information. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub zParent_Disposed(ByVal sender As Object, ByVal e As EventArgs)
            ParentRichTextBox = Nothing
            Refresh()
            Invalidate()
        End Sub
    End Class
End Namespace

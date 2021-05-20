using System.Drawing;
using System.Windows.Forms;

namespace SAL_VM
{
    /// <summary>
/// 
/// 
/// Uses Logo Programming Lang as Controller See (Decoder)
/// </summary>
    public class GDU : PictureBox
    {
        public GDU()
        {
            iTurtlePen = new Pen(TurtleColor, TurtlePenSize);
            CenterPoint = new PointF(Width - Width / 2.0f, Height - Height / 2.0f);
            // Add any initialization after the InitializeComponent() call.
            TurtleGraphics = CreateGraphics();
        }

        private Point iTurtlePosition = new Point(0, 0);
        private bool iTurtlePenUP_DWN = false;
        private int iDirection = 0;
        private bool iTurtleShow = false;
        private Color iTurtleColor = Color.Black;
        private int iTurtlePenSize = 3;
        private Graphics iTurtleGraphics;
        private int iX_ = 0;
        private int iY_ = 0;
        private Pen iTurtlePen;
        public object CenterPoint;

        public int Current_X_Position
        {
            get
            {
                int Current_X_PositionRet = default;
                return Current_X_PositionRet;
            }

            set
            {
                iX_ = value;
            }
        }

        public int Current_Y_Position
        {
            get
            {
                int Current_Y_PositionRet = default;
                return Current_Y_PositionRet;
            }

            set
            {
                iY_ = value;
            }
        }

        public void SetTurtlePen(Color nTurtleColor, int nTurtlePenSize)
        {
            iTurtlePen = new Pen(nTurtleColor, nTurtlePenSize);
        }

        public Point TurtlePosition
        {
            get
            {
                return iTurtlePosition;
            }

            set
            {
                iTurtlePosition = value;
            }
        }

        public bool TurtlePenUP_DWN
        {
            get
            {
                return iTurtlePenUP_DWN;
            }

            set
            {
                iTurtlePenUP_DWN = value;
            }
        }

        public int Direction
        {
            get
            {
                return iDirection;
            }

            set
            {
                iDirection = Direction;
            }
        }

        public bool TurtleShow
        {
            get
            {
                return iTurtleShow;
            }

            set
            {
                iTurtleShow = value;
            }
        }

        public Color TurtleColor
        {
            get
            {
                return iTurtleColor;
            }

            set
            {
                iTurtleColor = value;
            }
        }

        public int TurtlePenSize
        {
            get
            {
                return iTurtlePenSize;
            }

            set
            {
                iTurtlePenSize = value;
            }
        }

        public Graphics TurtleGraphics
        {
            get
            {
                return iTurtleGraphics;
            }

            set
            {
                iTurtleGraphics = value;
            }
        }


        #region Comands

        public void _FD(ref int Distance)
        {
            var NePosition = new Point(TurtlePosition.X + Distance, TurtlePosition.Y);
            TurtleGraphics.DrawLine(new Pen(TurtleColor, TurtlePenSize), TurtlePosition, NePosition);
            TurtleGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            TurtlePosition = NePosition;
            Invalidate();
        }

        public void _BK(ref int Distance)
        {
            var NePosition = new Point(TurtlePosition.X - Distance, TurtlePosition.Y);
            TurtleGraphics.DrawLine(new Pen(TurtleColor, TurtlePenSize), TurtlePosition, NePosition);
            TurtleGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            TurtlePosition = NePosition;
            Invalidate();
        }

        public void _RT(ref int Degrees)
        {
            Direction += Degrees;
        }

        public void _LT(ref int Degrees)
        {
            Direction -= Degrees;
        }

        public void _CS()
        {
            TurtleGraphics.Clear(Color.LightGray);
        }

        public void _CT()
        {
            TurtlePosition = (Point)CenterPoint;
        }
        /// <summary>
    /// Pen Drawing
    /// </summary>
        public void _PD()
        {
            TurtlePenUP_DWN = true;
        }
        /// <summary>
    /// Not Drawing
    /// </summary>
        public void _PU()
        {
            TurtlePenUP_DWN = false;
        }

        public void _HT()
        {
            TurtleShow = false;
        }

        public void _ST()
        {
            TurtleShow = true;
        }

        public void _SetPos(ref int X_CoOrd, ref int Y_CoOrd)
        {
            TurtlePosition = new Point(X_CoOrd, Y_CoOrd);
        }

        public void _SetPenColor(ref int Col)
        {
            iTurtlePen.Color = _SetColor(ref Col);
        }

        public void _SetBackGroundColor(ref int Col)
        {
            BackColor = _SetColor(ref Col);
        }

        public Color _SetColor(ref int Colour)
        {
            // SetColor = Color.White
            switch (Colour)
            {
                case 1:
                    {
                        return Color.Red;
                    }

                case 2:
                    {
                        return Color.Orange;
                    }

                case 3:
                    {
                        return Color.Yellow;
                    }

                case 4:
                    {
                        return Color.Green;
                    }

                case 5:
                    {
                        return Color.Blue;
                    }

                case 6:
                    {
                        return Color.Purple;
                    }

                case 7:
                    {
                        return Color.Brown;
                    }

                case 8:
                    {
                        return Color.Black;
                    }

                case 9:
                    {
                        return Color.White;
                    }
            }

            return default;
        }

        #endregion

        public Bitmap RotateImage(Image bmp, float angleDegrees)
        {
            var rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (var g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(bmp.Width / 2.0f, bmp.Height / 2.0f);
                g.RotateTransform(90f - angleDegrees);
                g.TranslateTransform(-bmp.Width / 2.0f, -bmp.Height / 2.0f);
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }

        #region Evaluator 
        /// <summary>
    /// fd – forward
    /// bk – backward
    /// rt – right
    /// lt – left
    /// cs – clearscreen
    /// pu − penup
    /// pd − pendown
    /// ht − hideturtle
    /// st − showturtle
    /// </summary>
    /// <param name="Command"></param>
        public void Decode(ref STACK_VM.ZX81_GPU.LogoCmd Command)
        {
            switch (Command.Cmd ?? "")
            {
                case "FD":
                    {
                        _FD(ref Command.Value);
                        break;
                    }

                case "BK":
                    {
                        _BK(ref Command.Value);
                        break;
                    }

                case "RT":
                    {
                        _RT(ref Command.Value);
                        break;
                    }

                case "LT":
                    {
                        _LT(ref Command.Value);
                        break;
                    }

                case "PU":
                    {
                        _PU();
                        break;
                    }

                case "PD":
                    {
                        _PD();
                        break;
                    }

                case "ST":
                    {
                        _ST();
                        break;
                    }

                case "HT":
                    {
                        _HT();
                        break;
                    }

                case "SETPEN":
                    {
                        _SetPenColor(ref Command.Value);
                        break;
                    }

                case "SETBACK":
                    {
                        _SetBackGroundColor(ref Command.Value);
                        break;
                    }
            }
        }
        #endregion
    }
}
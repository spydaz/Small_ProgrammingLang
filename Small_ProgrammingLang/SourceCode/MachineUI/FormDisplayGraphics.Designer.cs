using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SDK
{
    [DesignerGenerated()]
    public partial class FormDisplayGraphics : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            GDU_DISPLAY = new GDU();
            ((System.ComponentModel.ISupportInitialize)GDU_DISPLAY).BeginInit();
            SuspendLayout();
            // 
            // GDU_DISPLAY
            // 
            GDU_DISPLAY.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GDU_DISPLAY.Current_X_Position = 0;
            GDU_DISPLAY.Current_Y_Position = 0;
            GDU_DISPLAY.Direction = 0;
            GDU_DISPLAY.Location = new Point(12, 12);
            GDU_DISPLAY.Name = "GDU_DISPLAY";
            GDU_DISPLAY.Size = new Size(776, 306);
            GDU_DISPLAY.TabIndex = 0;
            GDU_DISPLAY.TabStop = false;
            GDU_DISPLAY.TurtleColor = Color.Black;
            GDU_DISPLAY.TurtlePenSize = 3;
            GDU_DISPLAY.TurtlePenUP_DWN = false;
            GDU_DISPLAY.TurtlePosition = new Point(0, 0);
            GDU_DISPLAY.TurtleShow = false;
            // 
            // FormDisplayGraphics
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(GDU_DISPLAY);
            DoubleBuffered = true;
            Name = "FormDisplayGraphics";
            Text = "FormDisplayGraphics";
            ((System.ComponentModel.ISupportInitialize)GDU_DISPLAY).EndInit();
            ResumeLayout(false);
        }

        internal GDU GDU_DISPLAY;
    }
}
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SAL_VM
{
    [DesignerGenerated()]
    public partial class SAL_ZX21_GDU : Form
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
            GroupBox6 = new GroupBox();
            GDU_DISPLAY = new GDU();
            GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GDU_DISPLAY).BeginInit();
            SuspendLayout();
            // 
            // GroupBox6
            // 
            GroupBox6.BackColor = Color.Transparent;
            GroupBox6.BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            GroupBox6.BackgroundImageLayout = ImageLayout.Stretch;
            GroupBox6.Controls.Add(GDU_DISPLAY);
            GroupBox6.Dock = DockStyle.Fill;
            GroupBox6.ForeColor = Color.Lime;
            GroupBox6.Location = new Point(0, 0);
            GroupBox6.Margin = new Padding(3, 4, 3, 4);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Padding = new Padding(3, 4, 3, 4);
            GroupBox6.Size = new Size(998, 698);
            GroupBox6.TabIndex = 3;
            GroupBox6.TabStop = false;
            // 
            // GDU_DISPLAY
            // 
            GDU_DISPLAY.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GDU_DISPLAY.Current_X_Position = 0;
            GDU_DISPLAY.Current_Y_Position = 0;
            GDU_DISPLAY.Direction = 0;
            GDU_DISPLAY.Location = new Point(12, 20);
            GDU_DISPLAY.Name = "GDU_DISPLAY";
            GDU_DISPLAY.Size = new Size(980, 485);
            GDU_DISPLAY.TabIndex = 0;
            GDU_DISPLAY.TabStop = false;
            GDU_DISPLAY.TurtleColor = Color.Black;
            GDU_DISPLAY.TurtlePenSize = 3;
            GDU_DISPLAY.TurtlePenUP_DWN = false;
            GDU_DISPLAY.TurtlePosition = new Point(0, 0);
            GDU_DISPLAY.TurtleShow = false;
            // 
            // SAL_ZX21_GDU
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 698);
            Controls.Add(GroupBox6);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "SAL_ZX21_GDU";
            Text = "S.A.L2020© ";
            GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GDU_DISPLAY).EndInit();
            ResumeLayout(false);
        }

        internal GroupBox GroupBox6;
        internal GDU GDU_DISPLAY;
    }
}
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SDK
{
    [DesignerGenerated()]
    public partial class FormDisplayConsole : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayConsole));
            GroupBox1 = new GroupBox();
            Zx81_DisplayScreen = new RichTextBox();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.BackColor = Color.Transparent;
            GroupBox1.Controls.Add(Zx81_DisplayScreen);
            GroupBox1.Dock = DockStyle.Top;
            GroupBox1.Location = new Point(0, 0);
            GroupBox1.Margin = new Padding(4);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(4);
            GroupBox1.Size = new Size(1005, 726);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            // 
            // Zx81_DisplayScreen
            // 
            Zx81_DisplayScreen.BackColor = Color.WhiteSmoke;
            Zx81_DisplayScreen.BorderStyle = BorderStyle.FixedSingle;
            Zx81_DisplayScreen.Cursor = Cursors.Arrow;
            Zx81_DisplayScreen.Dock = DockStyle.Fill;
            Zx81_DisplayScreen.Font = new Font("Consolas", 14.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            Zx81_DisplayScreen.Location = new Point(4, 24);
            Zx81_DisplayScreen.Margin = new Padding(4);
            Zx81_DisplayScreen.Name = "Zx81_DisplayScreen";
            Zx81_DisplayScreen.Size = new Size(997, 698);
            Zx81_DisplayScreen.TabIndex = 0;
            Zx81_DisplayScreen.Text = "SpydazWeb AI S.A.L Copyright © 2020" + '\n' + "Ready." + '\n';
            // 
            // FormDisplayConsole
            // 
            AutoScaleDimensions = new SizeF(9.0f, 19.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1005, 982);
            Controls.Add(GroupBox1);
            DoubleBuffered = true;
            Font = new Font("Microsoft Tai Le", 11.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(1402, 1021);
            MinimumSize = new Size(1015, 1021);
            Name = "FormDisplayConsole";
            Text = "S.A.L2020© ";
            GroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal RichTextBox Zx81_DisplayScreen;
    }
}
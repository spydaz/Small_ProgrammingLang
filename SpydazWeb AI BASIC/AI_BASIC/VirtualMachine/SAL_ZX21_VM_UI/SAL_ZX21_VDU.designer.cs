using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SAL_VM
{
    [DesignerGenerated()]
    public partial class SAL_ZX21_VDU : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SAL_ZX21_VDU));
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
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(670, 474);
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
            Zx81_DisplayScreen.Location = new Point(3, 16);
            Zx81_DisplayScreen.Name = "Zx81_DisplayScreen";
            Zx81_DisplayScreen.Size = new Size(664, 455);
            Zx81_DisplayScreen.TabIndex = 0;
            Zx81_DisplayScreen.Text = "SpydazWeb AI ZX81 Copyright © 2020" + '\n' + "Ready." + '\n';
            // 
            // FormDisplayConsole
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(670, 672);
            Controls.Add(GroupBox1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(938, 711);
            MinimumSize = new Size(680, 711);
            Name = "FormDisplayConsole";
            Text = "SpydazWeb AI ZX2020";
            GroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal RichTextBox Zx81_DisplayScreen;
    }
}
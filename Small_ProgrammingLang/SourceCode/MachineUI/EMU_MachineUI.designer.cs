using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SDK
{
    [DesignerGenerated()]
    public partial class EMU_MachineUI : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(EMU_MachineUI));
            GroupBox1 = new GroupBox();
            _ButtonRunCode = new Button();
            _ButtonRunCode.Click += new EventHandler(ButtonRunCode_Click);
            _ButtonNewScrn = new Button();
            _ButtonNewScrn.Click += new EventHandler(ButtonNewScrn_Click);
            ComboBoxURL = new ComboBox();
            GroupBox2 = new GroupBox();
            BrowserMain = new WebBrowser();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(_ButtonRunCode);
            GroupBox1.Controls.Add(_ButtonNewScrn);
            GroupBox1.Controls.Add(ComboBoxURL);
            GroupBox1.Dock = DockStyle.Bottom;
            GroupBox1.ForeColor = Color.White;
            GroupBox1.Location = new Point(0, 508);
            GroupBox1.Margin = new Padding(4, 5, 4, 5);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(4, 5, 4, 5);
            GroupBox1.Size = new Size(731, 67);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Select Online System";
            // 
            // ButtonRunCode
            // 
            _ButtonRunCode.Anchor = AnchorStyles.Right;
            _ButtonRunCode.BackColor = Color.DimGray;
            _ButtonRunCode.Location = new Point(402, 22);
            _ButtonRunCode.Name = "_ButtonRunCode";
            _ButtonRunCode.Size = new Size(105, 34);
            _ButtonRunCode.TabIndex = 3;
            _ButtonRunCode.Tag = "Opens in Current Window";
            _ButtonRunCode.Text = "Run System";
            _ButtonRunCode.UseVisualStyleBackColor = false;
            // 
            // ButtonNewScrn
            // 
            _ButtonNewScrn.Anchor = AnchorStyles.Right;
            _ButtonNewScrn.BackColor = Color.DimGray;
            _ButtonNewScrn.Location = new Point(292, 21);
            _ButtonNewScrn.Name = "_ButtonNewScrn";
            _ButtonNewScrn.Size = new Size(104, 35);
            _ButtonNewScrn.TabIndex = 2;
            _ButtonNewScrn.Tag = "Opens in New Window";
            _ButtonNewScrn.Text = "New Scrn";
            _ButtonNewScrn.UseVisualStyleBackColor = false;
            // 
            // ComboBoxURL
            // 
            ComboBoxURL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ComboBoxURL.FormattingEnabled = true;
            ComboBoxURL.Items.AddRange(new object[] { "Amiga Workbench Simulator", "Windows 3.1", "Windows 3.1 with CD-ROM", "Macintosh System 7", "Windows 95", "OS/2", "Windows 93", "Atari ST", "Windows 1.0", "EmuOS", "Mac Oxs Lion 10", "PCDOS 5", "Mac os7", "Mermaid diagrams", "AST Explorer", "Visual basic", "Nearly Parser", "Code Script Prettier", ".NET", "Web Assembly Studio", "WASM Explorer", "VB Online Compiler" });
            ComboBoxURL.Location = new Point(7, 28);
            ComboBoxURL.Name = "ComboBoxURL";
            ComboBoxURL.Size = new Size(261, 28);
            ComboBoxURL.TabIndex = 0;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBox2.Controls.Add(BrowserMain);
            GroupBox2.Location = new Point(7, 6);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(712, 400);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            // 
            // BrowserMain
            // 
            BrowserMain.Dock = DockStyle.Fill;
            BrowserMain.Location = new Point(3, 24);
            BrowserMain.MinimumSize = new Size(20, 20);
            BrowserMain.Name = "BrowserMain";
            BrowserMain.ScriptErrorsSuppressed = true;
            BrowserMain.Size = new Size(706, 373);
            BrowserMain.TabIndex = 0;
            BrowserMain.Url = new Uri("HTTP://www.spydazweb.co.uk", UriKind.Absolute);
            // 
            // EMU_MachineUI
            // 
            AutoScaleDimensions = new SizeF(9.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(731, 575);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            DoubleBuffered = true;
            Font = new Font("Comic Sans MS", 11.1f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "EMU_MachineUI";
            Text = "Online System";
            GroupBox1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        private Button _ButtonRunCode;

        internal Button ButtonRunCode
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonRunCode;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonRunCode != null)
                {
                    _ButtonRunCode.Click -= ButtonRunCode_Click;
                }

                _ButtonRunCode = value;
                if (_ButtonRunCode != null)
                {
                    _ButtonRunCode.Click += ButtonRunCode_Click;
                }
            }
        }

        private Button _ButtonNewScrn;

        internal Button ButtonNewScrn
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonNewScrn;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonNewScrn != null)
                {
                    _ButtonNewScrn.Click -= ButtonNewScrn_Click;
                }

                _ButtonNewScrn = value;
                if (_ButtonNewScrn != null)
                {
                    _ButtonNewScrn.Click += ButtonNewScrn_Click;
                }
            }
        }

        internal ComboBox ComboBoxURL;
        internal GroupBox GroupBox2;
        internal WebBrowser BrowserMain;
    }
}
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SAL_VM
{
    [DesignerGenerated()]
    public partial class SAL_ZX21_ConsoleUI : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SAL_ZX21_ConsoleUI));
            GroupBox1 = new GroupBox();
            RichTextBoxCodeEntry = new RichTextBox();
            _ButtonRunCode = new Button();
            _ButtonRunCode.Click += new EventHandler(ButtonRunCode_Click);
            RichTextBoxInfo = new RichTextBox();
            _ButtonClrScrn = new Button();
            _ButtonClrScrn.Click += new EventHandler(ButtonClrScrn_Click);
            _ButtonNewScrn = new Button();
            _ButtonNewScrn.Click += new EventHandler(ButtonNewScrn_Click);
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(RichTextBoxCodeEntry);
            GroupBox1.Dock = DockStyle.Bottom;
            GroupBox1.ForeColor = Color.White;
            GroupBox1.Location = new Point(0, 562);
            GroupBox1.Margin = new Padding(4, 5, 4, 5);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(4, 5, 4, 5);
            GroupBox1.Size = new Size(932, 168);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Enter Machine Code ";
            // 
            // RichTextBoxCodeEntry
            // 
            RichTextBoxCodeEntry.BackColor = SystemColors.Info;
            RichTextBoxCodeEntry.Dock = DockStyle.Fill;
            RichTextBoxCodeEntry.Font = new Font("Microsoft Sans Serif", 14.1f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            RichTextBoxCodeEntry.Location = new Point(4, 26);
            RichTextBoxCodeEntry.Margin = new Padding(4, 5, 4, 5);
            RichTextBoxCodeEntry.Name = "RichTextBoxCodeEntry";
            RichTextBoxCodeEntry.Size = new Size(924, 137);
            RichTextBoxCodeEntry.TabIndex = 0;
            RichTextBoxCodeEntry.Text = "";
            // 
            // ButtonRunCode
            // 
            _ButtonRunCode.Anchor = AnchorStyles.Right;
            _ButtonRunCode.BackColor = Color.DimGray;
            _ButtonRunCode.Location = new Point(828, 522);
            _ButtonRunCode.Name = "_ButtonRunCode";
            _ButtonRunCode.Size = new Size(80, 32);
            _ButtonRunCode.TabIndex = 1;
            _ButtonRunCode.Text = "Run Code";
            _ButtonRunCode.UseVisualStyleBackColor = false;
            // 
            // RichTextBoxInfo
            // 
            RichTextBoxInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RichTextBoxInfo.Location = new Point(12, 27);
            RichTextBoxInfo.Name = "RichTextBoxInfo";
            RichTextBoxInfo.Size = new Size(908, 445);
            RichTextBoxInfo.TabIndex = 2;
            RichTextBoxInfo.Text = "";
            // 
            // ButtonClrScrn
            // 
            _ButtonClrScrn.Anchor = AnchorStyles.Right;
            _ButtonClrScrn.BackColor = Color.DimGray;
            _ButtonClrScrn.Location = new Point(630, 522);
            _ButtonClrScrn.Name = "_ButtonClrScrn";
            _ButtonClrScrn.Size = new Size(89, 32);
            _ButtonClrScrn.TabIndex = 1;
            _ButtonClrScrn.Text = "Clear Scrn";
            _ButtonClrScrn.UseVisualStyleBackColor = false;
            // 
            // ButtonNewScrn
            // 
            _ButtonNewScrn.Anchor = AnchorStyles.Right;
            _ButtonNewScrn.BackColor = Color.DimGray;
            _ButtonNewScrn.Location = new Point(725, 522);
            _ButtonNewScrn.Name = "_ButtonNewScrn";
            _ButtonNewScrn.Size = new Size(97, 32);
            _ButtonNewScrn.TabIndex = 1;
            _ButtonNewScrn.Text = "New Scrn";
            _ButtonNewScrn.UseVisualStyleBackColor = false;
            // 
            // X86_MACHINE
            // 
            AutoScaleDimensions = new SizeF(9.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = My.Resources.Resources.Dell_UltraSharp_27;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(932, 730);
            Controls.Add(RichTextBoxInfo);
            Controls.Add(_ButtonNewScrn);
            Controls.Add(_ButtonClrScrn);
            Controls.Add(_ButtonRunCode);
            Controls.Add(GroupBox1);
            DoubleBuffered = true;
            Font = new Font("Comic Sans MS", 11.1f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "X86_MACHINE";
            Text = "X86_MACHINE_CODE";
            GroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal GroupBox GroupBox1;
        internal RichTextBox RichTextBoxCodeEntry;
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

        internal RichTextBox RichTextBoxInfo;
        private Button _ButtonClrScrn;

        internal Button ButtonClrScrn
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonClrScrn;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonClrScrn != null)
                {
                    _ButtonClrScrn.Click -= ButtonClrScrn_Click;
                }

                _ButtonClrScrn = value;
                if (_ButtonClrScrn != null)
                {
                    _ButtonClrScrn.Click += ButtonClrScrn_Click;
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
    }
}
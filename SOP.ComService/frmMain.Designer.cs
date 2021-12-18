
namespace SOP.ComService
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.portModule1 = new SOP.ComService.Controls.PortModule();
            this.portModule2 = new SOP.ComService.Controls.PortModule();
            this.portModule3 = new SOP.ComService.Controls.PortModule();
            this.portModule4 = new SOP.ComService.Controls.PortModule();
            this.portModule5 = new SOP.ComService.Controls.PortModule();
            this.portModule6 = new SOP.ComService.Controls.PortModule();
            this.label1 = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnShow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 422);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(832, 175);
            this.textBox1.TabIndex = 0;
            // 
            // portModule1
            // 
            this.portModule1.autoStart = false;
            this.portModule1.baudrate = 9600;
            this.portModule1.BoxName = "Kênh 1";
            this.portModule1.comPort = "";
            this.portModule1.databit = 8;
            this.portModule1.Location = new System.Drawing.Point(12, 12);
            this.portModule1.Name = "portModule1";
            this.portModule1.parity = System.IO.Ports.Parity.None;
            this.portModule1.Size = new System.Drawing.Size(273, 199);
            this.portModule1.stopbit = System.IO.Ports.StopBits.One;
            this.portModule1.TabIndex = 1;
            this.portModule1.userId = 0;
            this.portModule1.userName = "";
            // 
            // portModule2
            // 
            this.portModule2.autoStart = false;
            this.portModule2.baudrate = 9600;
            this.portModule2.BoxName = "Kênh 2";
            this.portModule2.comPort = "";
            this.portModule2.databit = 8;
            this.portModule2.Location = new System.Drawing.Point(291, 12);
            this.portModule2.Name = "portModule2";
            this.portModule2.parity = System.IO.Ports.Parity.None;
            this.portModule2.Size = new System.Drawing.Size(273, 199);
            this.portModule2.stopbit = System.IO.Ports.StopBits.One;
            this.portModule2.TabIndex = 1;
            this.portModule2.userId = 0;
            this.portModule2.userName = "";
            // 
            // portModule3
            // 
            this.portModule3.autoStart = false;
            this.portModule3.baudrate = 9600;
            this.portModule3.BoxName = "Kênh 3";
            this.portModule3.comPort = "";
            this.portModule3.databit = 8;
            this.portModule3.Location = new System.Drawing.Point(570, 12);
            this.portModule3.Name = "portModule3";
            this.portModule3.parity = System.IO.Ports.Parity.None;
            this.portModule3.Size = new System.Drawing.Size(273, 199);
            this.portModule3.stopbit = System.IO.Ports.StopBits.One;
            this.portModule3.TabIndex = 1;
            this.portModule3.userId = 0;
            this.portModule3.userName = "";
            // 
            // portModule4
            // 
            this.portModule4.autoStart = false;
            this.portModule4.baudrate = 9600;
            this.portModule4.BoxName = "Kênh 4";
            this.portModule4.comPort = "";
            this.portModule4.databit = 8;
            this.portModule4.Location = new System.Drawing.Point(12, 217);
            this.portModule4.Name = "portModule4";
            this.portModule4.parity = System.IO.Ports.Parity.None;
            this.portModule4.Size = new System.Drawing.Size(273, 199);
            this.portModule4.stopbit = System.IO.Ports.StopBits.One;
            this.portModule4.TabIndex = 1;
            this.portModule4.userId = 0;
            this.portModule4.userName = "";
            // 
            // portModule5
            // 
            this.portModule5.autoStart = false;
            this.portModule5.baudrate = 9600;
            this.portModule5.BoxName = "Kênh 5";
            this.portModule5.comPort = "";
            this.portModule5.databit = 8;
            this.portModule5.Location = new System.Drawing.Point(291, 217);
            this.portModule5.Name = "portModule5";
            this.portModule5.parity = System.IO.Ports.Parity.None;
            this.portModule5.Size = new System.Drawing.Size(273, 199);
            this.portModule5.stopbit = System.IO.Ports.StopBits.One;
            this.portModule5.TabIndex = 1;
            this.portModule5.userId = 0;
            this.portModule5.userName = "";
            // 
            // portModule6
            // 
            this.portModule6.autoStart = false;
            this.portModule6.baudrate = 9600;
            this.portModule6.BoxName = "Kênh 6";
            this.portModule6.comPort = "";
            this.portModule6.databit = 8;
            this.portModule6.Location = new System.Drawing.Point(570, 217);
            this.portModule6.Name = "portModule6";
            this.portModule6.parity = System.IO.Ports.Parity.None;
            this.portModule6.Size = new System.Drawing.Size(273, 199);
            this.portModule6.stopbit = System.IO.Ports.StopBits.One;
            this.portModule6.TabIndex = 1;
            this.portModule6.userId = 0;
            this.portModule6.userName = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(559, 600);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "Trung tâm CNTT&&TT Lào Cai\r\nSđt hỗ trợ: 02143841889";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.trayIcon.Text = "ComService";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShow,
            this.btnExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // btnShow
            // 
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(180, 22);
            this.btnShow.Text = "Hiện cửa sổ";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(180, 22);
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 644);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portModule3);
            this.Controls.Add(this.portModule2);
            this.Controls.Add(this.portModule6);
            this.Controls.Add(this.portModule5);
            this.Controls.Add(this.portModule4);
            this.Controls.Add(this.portModule1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "ComService";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Controls.PortModule portModule1;
        private Controls.PortModule portModule2;
        private Controls.PortModule portModule3;
        private Controls.PortModule portModule4;
        private Controls.PortModule portModule5;
        private Controls.PortModule portModule6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnShow;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
    }
}


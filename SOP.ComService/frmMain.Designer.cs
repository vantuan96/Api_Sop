
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.portModule1 = new SOP.ComService.Controls.PortModule();
            this.portModule2 = new SOP.ComService.Controls.PortModule();
            this.portModule3 = new SOP.ComService.Controls.PortModule();
            this.portModule4 = new SOP.ComService.Controls.PortModule();
            this.portModule5 = new SOP.ComService.Controls.PortModule();
            this.portModule6 = new SOP.ComService.Controls.PortModule();
            this.label1 = new System.Windows.Forms.Label();
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
            this.portModule1.BoxName = "Kênh 1";
            this.portModule1.Location = new System.Drawing.Point(12, 12);
            this.portModule1.Name = "portModule1";
            this.portModule1.Size = new System.Drawing.Size(273, 199);
            this.portModule1.TabIndex = 1;
            // 
            // portModule2
            // 
            this.portModule2.BoxName = "Kênh 2";
            this.portModule2.Location = new System.Drawing.Point(291, 12);
            this.portModule2.Name = "portModule2";
            this.portModule2.Size = new System.Drawing.Size(273, 199);
            this.portModule2.TabIndex = 1;
            // 
            // portModule3
            // 
            this.portModule3.BoxName = "Kênh 3";
            this.portModule3.Location = new System.Drawing.Point(570, 12);
            this.portModule3.Name = "portModule3";
            this.portModule3.Size = new System.Drawing.Size(273, 199);
            this.portModule3.TabIndex = 1;
            // 
            // portModule4
            // 
            this.portModule4.BoxName = "Kênh 4";
            this.portModule4.Location = new System.Drawing.Point(12, 217);
            this.portModule4.Name = "portModule4";
            this.portModule4.Size = new System.Drawing.Size(273, 199);
            this.portModule4.TabIndex = 1;
            // 
            // portModule5
            // 
            this.portModule5.BoxName = "Kênh 5";
            this.portModule5.Location = new System.Drawing.Point(291, 217);
            this.portModule5.Name = "portModule5";
            this.portModule5.Size = new System.Drawing.Size(273, 199);
            this.portModule5.TabIndex = 1;
            // 
            // portModule6
            // 
            this.portModule6.BoxName = "Kênh 6";
            this.portModule6.Location = new System.Drawing.Point(570, 217);
            this.portModule6.Name = "portModule6";
            this.portModule6.Size = new System.Drawing.Size(273, 199);
            this.portModule6.TabIndex = 1;
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
            this.Load += new System.EventHandler(this.frmMain_Load);
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
    }
}


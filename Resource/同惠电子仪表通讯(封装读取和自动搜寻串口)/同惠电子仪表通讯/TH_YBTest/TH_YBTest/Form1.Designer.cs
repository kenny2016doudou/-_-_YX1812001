namespace TH_YBTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cboxStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxDataBit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxParity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gboxR = new System.Windows.Forms.GroupBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtRaddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRS = new System.Windows.Forms.RichTextBox();
            this.gboxW = new System.Windows.Forms.GroupBox();
            this.TH_TIMER = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.gboxR.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.cboxStopBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboxDataBit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboxRate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboxParity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboxCom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(730, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(650, 64);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关闭串口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(567, 64);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(64, 30);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cboxStopBits
            // 
            this.cboxStopBits.FormattingEnabled = true;
            this.cboxStopBits.Items.AddRange(new object[] {
            "One",
            "None",
            "OnePointFive",
            "Two"});
            this.cboxStopBits.Location = new System.Drawing.Point(633, 25);
            this.cboxStopBits.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxStopBits.Name = "cboxStopBits";
            this.cboxStopBits.Size = new System.Drawing.Size(92, 20);
            this.cboxStopBits.TabIndex = 9;
            this.cboxStopBits.Text = "One";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(591, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位:";
            // 
            // cboxDataBit
            // 
            this.cboxDataBit.FormattingEnabled = true;
            this.cboxDataBit.Items.AddRange(new object[] {
            "8",
            "7"});
            this.cboxDataBit.Location = new System.Drawing.Point(495, 25);
            this.cboxDataBit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxDataBit.Name = "cboxDataBit";
            this.cboxDataBit.Size = new System.Drawing.Size(92, 20);
            this.cboxDataBit.TabIndex = 7;
            this.cboxDataBit.Text = "8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "数据位:";
            // 
            // cboxRate
            // 
            this.cboxRate.FormattingEnabled = true;
            this.cboxRate.Location = new System.Drawing.Point(352, 25);
            this.cboxRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxRate.Name = "cboxRate";
            this.cboxRate.Size = new System.Drawing.Size(92, 20);
            this.cboxRate.TabIndex = 5;
            this.cboxRate.Text = "9600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "波特率:";
            // 
            // cboxParity
            // 
            this.cboxParity.FormattingEnabled = true;
            this.cboxParity.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Old",
            "Space"});
            this.cboxParity.Location = new System.Drawing.Point(210, 25);
            this.cboxParity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxParity.Name = "cboxParity";
            this.cboxParity.Size = new System.Drawing.Size(92, 20);
            this.cboxParity.TabIndex = 3;
            this.cboxParity.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "奇偶校验:";
            // 
            // cboxCom
            // 
            this.cboxCom.FormattingEnabled = true;
            this.cboxCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cboxCom.Location = new System.Drawing.Point(57, 25);
            this.cboxCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxCom.Name = "cboxCom";
            this.cboxCom.Size = new System.Drawing.Size(92, 20);
            this.cboxCom.TabIndex = 1;
            this.cboxCom.Text = "COM3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM口:";
            // 
            // gboxR
            // 
            this.gboxR.Controls.Add(this.btnRead);
            this.gboxR.Controls.Add(this.txtRaddr);
            this.gboxR.Controls.Add(this.label6);
            this.gboxR.Controls.Add(this.txtRS);
            this.gboxR.Enabled = false;
            this.gboxR.Location = new System.Drawing.Point(11, 120);
            this.gboxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxR.Name = "gboxR";
            this.gboxR.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxR.Size = new System.Drawing.Size(357, 300);
            this.gboxR.TabIndex = 1;
            this.gboxR.TabStop = false;
            this.gboxR.Text = "读取";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(251, 49);
            this.btnRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(64, 30);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtRaddr
            // 
            this.txtRaddr.Location = new System.Drawing.Point(91, 22);
            this.txtRaddr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRaddr.Name = "txtRaddr";
            this.txtRaddr.Size = new System.Drawing.Size(225, 21);
            this.txtRaddr.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "地址:";
            // 
            // txtRS
            // 
            this.txtRS.Location = new System.Drawing.Point(10, 89);
            this.txtRS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRS.Name = "txtRS";
            this.txtRS.Size = new System.Drawing.Size(337, 136);
            this.txtRS.TabIndex = 0;
            this.txtRS.Text = "";
            // 
            // gboxW
            // 
            this.gboxW.Enabled = false;
            this.gboxW.Location = new System.Drawing.Point(384, 120);
            this.gboxW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxW.Name = "gboxW";
            this.gboxW.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxW.Size = new System.Drawing.Size(357, 300);
            this.gboxW.TabIndex = 2;
            this.gboxW.TabStop = false;
            this.gboxW.Text = "写入";
            // 
            // TH_TIMER
            // 
            this.TH_TIMER.Interval = 200;
            this.TH_TIMER.Tick += new System.EventHandler(this.TH_TIMER_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 430);
            this.Controls.Add(this.gboxW);
            this.Controls.Add(this.gboxR);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "同惠电子通讯测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gboxR.ResumeLayout(false);
            this.gboxR.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboxCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxStopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxDataBit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxParity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox gboxR;
        private System.Windows.Forms.GroupBox gboxW;
        private System.Windows.Forms.RichTextBox txtRS;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtRaddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer TH_TIMER;

    }
}


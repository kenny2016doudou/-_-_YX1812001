namespace YD_YBTest
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboxStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxDataBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxParity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gboxR = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRParmAddr = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnR = new System.Windows.Forms.Button();
            this.txtRResponse = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRCmd = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.gboxW = new System.Windows.Forms.GroupBox();
            this.txtWParmAddr = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWResponse = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtWCmd = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnW = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.txtRAddr = new System.Windows.Forms.NumericUpDown();
            this.txtWAddr = new System.Windows.Forms.NumericUpDown();
            this.txtWValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.gboxR.SuspendLayout();
            this.gboxW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWAddr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cboxStopBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboxBaudRate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboxDataBits);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboxParity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboxCom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1064, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(969, 54);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 33);
            this.button2.TabIndex = 11;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(866, 54);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 10;
            this.button1.Text = "打开";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboxStopBits
            // 
            this.cboxStopBits.FormattingEnabled = true;
            this.cboxStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePointFive",
            "Two"});
            this.cboxStopBits.Location = new System.Drawing.Point(749, 57);
            this.cboxStopBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxStopBits.Name = "cboxStopBits";
            this.cboxStopBits.Size = new System.Drawing.Size(100, 25);
            this.cboxStopBits.TabIndex = 9;
            this.cboxStopBits.Text = "One";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(687, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位:";
            // 
            // cboxBaudRate
            // 
            this.cboxBaudRate.FormattingEnabled = true;
            this.cboxBaudRate.Items.AddRange(new object[] {
            "9600"});
            this.cboxBaudRate.Location = new System.Drawing.Point(580, 57);
            this.cboxBaudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxBaudRate.Name = "cboxBaudRate";
            this.cboxBaudRate.Size = new System.Drawing.Size(100, 25);
            this.cboxBaudRate.TabIndex = 7;
            this.cboxBaudRate.Text = "9600";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "波特率:";
            // 
            // cboxDataBits
            // 
            this.cboxDataBits.FormattingEnabled = true;
            this.cboxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cboxDataBits.Location = new System.Drawing.Point(411, 57);
            this.cboxDataBits.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxDataBits.Name = "cboxDataBits";
            this.cboxDataBits.Size = new System.Drawing.Size(100, 25);
            this.cboxDataBits.TabIndex = 5;
            this.cboxDataBits.Text = "8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位:";
            // 
            // cboxParity
            // 
            this.cboxParity.FormattingEnabled = true;
            this.cboxParity.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Odd",
            "Space"});
            this.cboxParity.Location = new System.Drawing.Point(241, 57);
            this.cboxParity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxParity.Name = "cboxParity";
            this.cboxParity.Size = new System.Drawing.Size(100, 25);
            this.cboxParity.TabIndex = 3;
            this.cboxParity.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
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
            this.cboxCom.Location = new System.Drawing.Point(65, 57);
            this.cboxCom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxCom.Name = "cboxCom";
            this.cboxCom.Size = new System.Drawing.Size(91, 25);
            this.cboxCom.TabIndex = 1;
            this.cboxCom.Text = "COM5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM口:";
            // 
            // gboxR
            // 
            this.gboxR.Controls.Add(this.txtRAddr);
            this.gboxR.Controls.Add(this.label15);
            this.gboxR.Controls.Add(this.label14);
            this.gboxR.Controls.Add(this.txtRParmAddr);
            this.gboxR.Controls.Add(this.label13);
            this.gboxR.Controls.Add(this.label8);
            this.gboxR.Controls.Add(this.btnR);
            this.gboxR.Controls.Add(this.txtRResponse);
            this.gboxR.Controls.Add(this.label7);
            this.gboxR.Controls.Add(this.txtRCmd);
            this.gboxR.Controls.Add(this.label6);
            this.gboxR.Controls.Add(this.shapeContainer1);
            this.gboxR.Enabled = false;
            this.gboxR.Location = new System.Drawing.Point(16, 170);
            this.gboxR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxR.Name = "gboxR";
            this.gboxR.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxR.Size = new System.Drawing.Size(526, 567);
            this.gboxR.TabIndex = 1;
            this.gboxR.TabStop = false;
            this.gboxR.Text = "读取";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(237, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 17);
            this.label15.TabIndex = 19;
            this.label15.Text = "(参数代号:如 0C)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(237, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "(仪表读取的地址:如 2)";
            // 
            // txtRParmAddr
            // 
            this.txtRParmAddr.Location = new System.Drawing.Point(90, 76);
            this.txtRParmAddr.Name = "txtRParmAddr";
            this.txtRParmAddr.Size = new System.Drawing.Size(134, 23);
            this.txtRParmAddr.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 16;
            this.label13.Text = "参数代号:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "地址:";
            // 
            // btnR
            // 
            this.btnR.Location = new System.Drawing.Point(407, 28);
            this.btnR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(101, 33);
            this.btnR.TabIndex = 5;
            this.btnR.Text = "发送";
            this.btnR.UseVisualStyleBackColor = true;
            this.btnR.Click += new System.EventHandler(this.btnR_Click);
            // 
            // txtRResponse
            // 
            this.txtRResponse.Location = new System.Drawing.Point(12, 309);
            this.txtRResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRResponse.Name = "txtRResponse";
            this.txtRResponse.Size = new System.Drawing.Size(496, 225);
            this.txtRResponse.TabIndex = 4;
            this.txtRResponse.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "响应";
            // 
            // txtRCmd
            // 
            this.txtRCmd.Location = new System.Drawing.Point(13, 135);
            this.txtRCmd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRCmd.Name = "txtRCmd";
            this.txtRCmd.Size = new System.Drawing.Size(495, 113);
            this.txtRCmd.TabIndex = 1;
            this.txtRCmd.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "发送指令";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 20);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(520, 543);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 8;
            this.lineShape1.X2 = 507;
            this.lineShape1.Y1 = 249;
            this.lineShape1.Y2 = 249;
            // 
            // gboxW
            // 
            this.gboxW.Controls.Add(this.txtWValue);
            this.gboxW.Controls.Add(this.txtWAddr);
            this.gboxW.Controls.Add(this.txtWParmAddr);
            this.gboxW.Controls.Add(this.label16);
            this.gboxW.Controls.Add(this.label12);
            this.gboxW.Controls.Add(this.txtWResponse);
            this.gboxW.Controls.Add(this.label11);
            this.gboxW.Controls.Add(this.txtWCmd);
            this.gboxW.Controls.Add(this.label10);
            this.gboxW.Controls.Add(this.btnW);
            this.gboxW.Controls.Add(this.label9);
            this.gboxW.Controls.Add(this.shapeContainer2);
            this.gboxW.Enabled = false;
            this.gboxW.Location = new System.Drawing.Point(548, 170);
            this.gboxW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxW.Name = "gboxW";
            this.gboxW.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxW.Size = new System.Drawing.Size(531, 567);
            this.gboxW.TabIndex = 2;
            this.gboxW.TabStop = false;
            this.gboxW.Text = "写入";
            // 
            // txtWParmAddr
            // 
            this.txtWParmAddr.Location = new System.Drawing.Point(304, 33);
            this.txtWParmAddr.Name = "txtWParmAddr";
            this.txtWParmAddr.Size = new System.Drawing.Size(134, 23);
            this.txtWParmAddr.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(239, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 18;
            this.label16.Text = "参数代号:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 17);
            this.label12.TabIndex = 14;
            this.label12.Text = "写入值:";
            // 
            // txtWResponse
            // 
            this.txtWResponse.Location = new System.Drawing.Point(22, 314);
            this.txtWResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWResponse.Name = "txtWResponse";
            this.txtWResponse.Size = new System.Drawing.Size(495, 220);
            this.txtWResponse.TabIndex = 9;
            this.txtWResponse.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "响应";
            // 
            // txtWCmd
            // 
            this.txtWCmd.Location = new System.Drawing.Point(22, 135);
            this.txtWCmd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWCmd.Name = "txtWCmd";
            this.txtWCmd.Size = new System.Drawing.Size(495, 123);
            this.txtWCmd.TabIndex = 12;
            this.txtWCmd.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "发送指令";
            // 
            // btnW
            // 
            this.btnW.Location = new System.Drawing.Point(357, 94);
            this.btnW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnW.Name = "btnW";
            this.btnW.Size = new System.Drawing.Size(160, 33);
            this.btnW.TabIndex = 8;
            this.btnW.Text = "发送";
            this.btnW.UseVisualStyleBackColor = true;
            this.btnW.Click += new System.EventHandler(this.btnW_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "地址:";
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 20);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(525, 543);
            this.shapeContainer2.TabIndex = 13;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 19;
            this.lineShape2.X2 = 518;
            this.lineShape2.Y1 = 249;
            this.lineShape2.Y2 = 249;
            // 
            // txtRAddr
            // 
            this.txtRAddr.Location = new System.Drawing.Point(90, 36);
            this.txtRAddr.Name = "txtRAddr";
            this.txtRAddr.Size = new System.Drawing.Size(134, 23);
            this.txtRAddr.TabIndex = 20;
            // 
            // txtWAddr
            // 
            this.txtWAddr.Location = new System.Drawing.Point(91, 34);
            this.txtWAddr.Name = "txtWAddr";
            this.txtWAddr.Size = new System.Drawing.Size(134, 23);
            this.txtWAddr.TabIndex = 21;
            // 
            // txtWValue
            // 
            this.txtWValue.Location = new System.Drawing.Point(91, 77);
            this.txtWValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txtWValue.Name = "txtWValue";
            this.txtWValue.Size = new System.Drawing.Size(134, 23);
            this.txtWValue.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 753);
            this.Controls.Add(this.gboxW);
            this.Controls.Add(this.gboxR);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "宇电仪表通讯测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gboxR.ResumeLayout(false);
            this.gboxR.PerformLayout();
            this.gboxW.ResumeLayout(false);
            this.gboxW.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWAddr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboxStopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxBaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxDataBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxParity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gboxR;
        private System.Windows.Forms.RichTextBox txtRCmd;
        private System.Windows.Forms.Label label6;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.GroupBox gboxW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox txtRResponse;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox txtWResponse;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox txtWCmd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnW;
        private System.Windows.Forms.Label label9;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRParmAddr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWParmAddr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown txtRAddr;
        private System.Windows.Forms.NumericUpDown txtWValue;
        private System.Windows.Forms.NumericUpDown txtWAddr;
    }
}


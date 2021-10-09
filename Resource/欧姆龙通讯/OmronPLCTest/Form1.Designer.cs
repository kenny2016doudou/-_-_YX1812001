namespace OmronPLCTest
{
    partial class Form1
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
            this.btnReadPLC = new System.Windows.Forms.Button();
            this.txtReadPoint = new System.Windows.Forms.TextBox();
            this.gbOmronRead = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbRDataType = new System.Windows.Forms.ComboBox();
            this.rtxtReadValue = new System.Windows.Forms.RichTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReadBit = new System.Windows.Forms.TextBox();
            this.cmbRArea = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxtReadResponse = new System.Windows.Forms.RichTextBox();
            this.rtxtReadSend = new System.Windows.Forms.RichTextBox();
            this.gbOmronWrite = new System.Windows.Forms.GroupBox();
            this.cboxW = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbWDataType = new System.Windows.Forms.ComboBox();
            this.rtxtWriteValue = new System.Windows.Forms.RichTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtWData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWbit = new System.Windows.Forms.TextBox();
            this.rtxtWriteCommand = new System.Windows.Forms.RichTextBox();
            this.cmbWArea = new System.Windows.Forms.ComboBox();
            this.rtxtWriteResponse = new System.Windows.Forms.RichTextBox();
            this.txtWpoint = new System.Windows.Forms.TextBox();
            this.btnWritePLC = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbmStopBits = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtdataBits = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCOM = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.欧姆龙指令发送 = new System.Windows.Forms.TabPage();
            this.gbOmronCommand = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtxtResponse = new System.Windows.Forms.RichTextBox();
            this.rtxtCommand = new System.Windows.Forms.RichTextBox();
            this.精简命令发送 = new System.Windows.Forms.TabPage();
            this.txt读取指令状态信息 = new System.Windows.Forms.RichTextBox();
            this.btn欧姆龙读取 = new System.Windows.Forms.Button();
            this.cmb数据类型 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt读取地址 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.欧姆龙写入 = new System.Windows.Forms.TabPage();
            this.txt写入值 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn欧姆龙写入 = new System.Windows.Forms.Button();
            this.cbox写入数据类型 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt写入地址 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.欧姆龙状态控制 = new System.Windows.Forms.TabPage();
            this.txt状态控制 = new System.Windows.Forms.RichTextBox();
            this.btn控制给0 = new System.Windows.Forms.Button();
            this.btn控制给1 = new System.Windows.Forms.Button();
            this.cbox控制点位数据类型 = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt控制点位 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.gbOmronRead.SuspendLayout();
            this.gbOmronWrite.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.欧姆龙指令发送.SuspendLayout();
            this.gbOmronCommand.SuspendLayout();
            this.精简命令发送.SuspendLayout();
            this.欧姆龙写入.SuspendLayout();
            this.欧姆龙状态控制.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadPLC
            // 
            this.btnReadPLC.Location = new System.Drawing.Point(256, 88);
            this.btnReadPLC.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadPLC.Name = "btnReadPLC";
            this.btnReadPLC.Size = new System.Drawing.Size(86, 30);
            this.btnReadPLC.TabIndex = 0;
            this.btnReadPLC.Text = "读取";
            this.btnReadPLC.UseVisualStyleBackColor = true;
            this.btnReadPLC.Click += new System.EventHandler(this.btnReadPLC_Click);
            // 
            // txtReadPoint
            // 
            this.txtReadPoint.Location = new System.Drawing.Point(9, 49);
            this.txtReadPoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtReadPoint.Name = "txtReadPoint";
            this.txtReadPoint.Size = new System.Drawing.Size(78, 32);
            this.txtReadPoint.TabIndex = 1;
            // 
            // gbOmronRead
            // 
            this.gbOmronRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbOmronRead.Controls.Add(this.label21);
            this.gbOmronRead.Controls.Add(this.cmbRDataType);
            this.gbOmronRead.Controls.Add(this.rtxtReadValue);
            this.gbOmronRead.Controls.Add(this.label17);
            this.gbOmronRead.Controls.Add(this.label13);
            this.gbOmronRead.Controls.Add(this.label1);
            this.gbOmronRead.Controls.Add(this.label12);
            this.gbOmronRead.Controls.Add(this.txtReadBit);
            this.gbOmronRead.Controls.Add(this.cmbRArea);
            this.gbOmronRead.Controls.Add(this.label4);
            this.gbOmronRead.Controls.Add(this.label3);
            this.gbOmronRead.Controls.Add(this.rtxtReadResponse);
            this.gbOmronRead.Controls.Add(this.rtxtReadSend);
            this.gbOmronRead.Controls.Add(this.btnReadPLC);
            this.gbOmronRead.Controls.Add(this.txtReadPoint);
            this.gbOmronRead.Enabled = false;
            this.gbOmronRead.Location = new System.Drawing.Point(562, 115);
            this.gbOmronRead.Margin = new System.Windows.Forms.Padding(4);
            this.gbOmronRead.Name = "gbOmronRead";
            this.gbOmronRead.Padding = new System.Windows.Forms.Padding(4);
            this.gbOmronRead.Size = new System.Drawing.Size(357, 436);
            this.gbOmronRead.TabIndex = 2;
            this.gbOmronRead.TabStop = false;
            this.gbOmronRead.Text = "欧姆龙通信[读取]";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(252, 24);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 21);
            this.label21.TabIndex = 15;
            this.label21.Text = "数据类型";
            // 
            // cmbRDataType
            // 
            this.cmbRDataType.FormattingEnabled = true;
            this.cmbRDataType.Items.AddRange(new object[] {
            "Bit",
            "Word"});
            this.cmbRDataType.Location = new System.Drawing.Point(256, 51);
            this.cmbRDataType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRDataType.Name = "cmbRDataType";
            this.cmbRDataType.Size = new System.Drawing.Size(86, 29);
            this.cmbRDataType.TabIndex = 4;
            // 
            // rtxtReadValue
            // 
            this.rtxtReadValue.Location = new System.Drawing.Point(9, 348);
            this.rtxtReadValue.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtReadValue.Name = "rtxtReadValue";
            this.rtxtReadValue.Size = new System.Drawing.Size(333, 82);
            this.rtxtReadValue.TabIndex = 7;
            this.rtxtReadValue.Text = "";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 323);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 21);
            this.label17.TabIndex = 12;
            this.label17.Text = "解析数据值";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(189, 24);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 21);
            this.label13.TabIndex = 11;
            this.label13.Text = "数据位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "内存区域";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 24);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 21);
            this.label12.TabIndex = 9;
            this.label12.Text = "数据点";
            // 
            // txtReadBit
            // 
            this.txtReadBit.Location = new System.Drawing.Point(202, 49);
            this.txtReadBit.Margin = new System.Windows.Forms.Padding(4);
            this.txtReadBit.Name = "txtReadBit";
            this.txtReadBit.Size = new System.Drawing.Size(46, 32);
            this.txtReadBit.TabIndex = 3;
            // 
            // cmbRArea
            // 
            this.cmbRArea.FormattingEnabled = true;
            this.cmbRArea.Items.AddRange(new object[] {
            "CIO_Bit",
            "WR_Bit",
            "HR_Bit",
            "AR_Bit",
            "CIO_Word",
            "WR_Word",
            "HR_Word",
            "AR_Word",
            "TIM_Flag",
            "CNT_Flag",
            "TIM_PV",
            "CNT_PV",
            "DM_Bit",
            "DM_Word"});
            this.cmbRArea.Location = new System.Drawing.Point(99, 51);
            this.cmbRArea.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRArea.Name = "cmbRArea";
            this.cmbRArea.Size = new System.Drawing.Size(88, 29);
            this.cmbRArea.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 205);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "输出报文";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "输入报文";
            // 
            // rtxtReadResponse
            // 
            this.rtxtReadResponse.Location = new System.Drawing.Point(9, 230);
            this.rtxtReadResponse.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtReadResponse.Name = "rtxtReadResponse";
            this.rtxtReadResponse.Size = new System.Drawing.Size(335, 82);
            this.rtxtReadResponse.TabIndex = 6;
            this.rtxtReadResponse.Text = "";
            // 
            // rtxtReadSend
            // 
            this.rtxtReadSend.Location = new System.Drawing.Point(9, 128);
            this.rtxtReadSend.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtReadSend.Name = "rtxtReadSend";
            this.rtxtReadSend.Size = new System.Drawing.Size(333, 57);
            this.rtxtReadSend.TabIndex = 5;
            this.rtxtReadSend.Text = "";
            // 
            // gbOmronWrite
            // 
            this.gbOmronWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOmronWrite.Controls.Add(this.cboxW);
            this.gbOmronWrite.Controls.Add(this.label22);
            this.gbOmronWrite.Controls.Add(this.cmbWDataType);
            this.gbOmronWrite.Controls.Add(this.rtxtWriteValue);
            this.gbOmronWrite.Controls.Add(this.label16);
            this.gbOmronWrite.Controls.Add(this.label18);
            this.gbOmronWrite.Controls.Add(this.txtWData);
            this.gbOmronWrite.Controls.Add(this.label2);
            this.gbOmronWrite.Controls.Add(this.label6);
            this.gbOmronWrite.Controls.Add(this.label14);
            this.gbOmronWrite.Controls.Add(this.label15);
            this.gbOmronWrite.Controls.Add(this.label5);
            this.gbOmronWrite.Controls.Add(this.txtWbit);
            this.gbOmronWrite.Controls.Add(this.rtxtWriteCommand);
            this.gbOmronWrite.Controls.Add(this.cmbWArea);
            this.gbOmronWrite.Controls.Add(this.rtxtWriteResponse);
            this.gbOmronWrite.Controls.Add(this.txtWpoint);
            this.gbOmronWrite.Controls.Add(this.btnWritePLC);
            this.gbOmronWrite.Enabled = false;
            this.gbOmronWrite.Location = new System.Drawing.Point(927, 115);
            this.gbOmronWrite.Margin = new System.Windows.Forms.Padding(4);
            this.gbOmronWrite.Name = "gbOmronWrite";
            this.gbOmronWrite.Padding = new System.Windows.Forms.Padding(4);
            this.gbOmronWrite.Size = new System.Drawing.Size(355, 436);
            this.gbOmronWrite.TabIndex = 5;
            this.gbOmronWrite.TabStop = false;
            this.gbOmronWrite.Text = "欧姆龙通信[写入]";
            // 
            // cboxW
            // 
            this.cboxW.AutoSize = true;
            this.cboxW.Location = new System.Drawing.Point(250, 82);
            this.cboxW.Name = "cboxW";
            this.cboxW.Size = new System.Drawing.Size(90, 25);
            this.cboxW.TabIndex = 20;
            this.cboxW.Text = "On/Off";
            this.cboxW.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(252, 21);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 21);
            this.label22.TabIndex = 17;
            this.label22.Text = "数据类型";
            // 
            // cmbWDataType
            // 
            this.cmbWDataType.FormattingEnabled = true;
            this.cmbWDataType.Items.AddRange(new object[] {
            "Bit",
            "Word"});
            this.cmbWDataType.Location = new System.Drawing.Point(256, 46);
            this.cmbWDataType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWDataType.Name = "cmbWDataType";
            this.cmbWDataType.Size = new System.Drawing.Size(83, 29);
            this.cmbWDataType.TabIndex = 5;
            // 
            // rtxtWriteValue
            // 
            this.rtxtWriteValue.Location = new System.Drawing.Point(17, 379);
            this.rtxtWriteValue.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtWriteValue.Name = "rtxtWriteValue";
            this.rtxtWriteValue.Size = new System.Drawing.Size(311, 51);
            this.rtxtWriteValue.TabIndex = 8;
            this.rtxtWriteValue.Text = "";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 83);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 21);
            this.label16.TabIndex = 19;
            this.label16.Text = "数据值";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(37, 354);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(291, 21);
            this.label18.TabIndex = 14;
            this.label18.Text = "(如果返回的是0000 代表操作成功)";
            // 
            // txtWData
            // 
            this.txtWData.Location = new System.Drawing.Point(10, 107);
            this.txtWData.Margin = new System.Windows.Forms.Padding(4);
            this.txtWData.Name = "txtWData";
            this.txtWData.Size = new System.Drawing.Size(147, 32);
            this.txtWData.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "数据位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 232);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "输出报文";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(97, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 21);
            this.label14.TabIndex = 16;
            this.label14.Text = "内存区域";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 21);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 21);
            this.label15.TabIndex = 15;
            this.label15.Text = "数据点";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 142);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "输入报文";
            // 
            // txtWbit
            // 
            this.txtWbit.Location = new System.Drawing.Point(195, 46);
            this.txtWbit.Margin = new System.Windows.Forms.Padding(4);
            this.txtWbit.Name = "txtWbit";
            this.txtWbit.Size = new System.Drawing.Size(47, 32);
            this.txtWbit.TabIndex = 3;
            // 
            // rtxtWriteCommand
            // 
            this.rtxtWriteCommand.Location = new System.Drawing.Point(10, 165);
            this.rtxtWriteCommand.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtWriteCommand.Name = "rtxtWriteCommand";
            this.rtxtWriteCommand.Size = new System.Drawing.Size(318, 57);
            this.rtxtWriteCommand.TabIndex = 6;
            this.rtxtWriteCommand.Text = "";
            // 
            // cmbWArea
            // 
            this.cmbWArea.FormattingEnabled = true;
            this.cmbWArea.Items.AddRange(new object[] {
            "CIO_Bit",
            "WR_Bit",
            "HR_Bit",
            "AR_Bit",
            "CIO_Word",
            "WR_Word",
            "HR_Word",
            "AR_Word",
            "TIM_Flag",
            "CNT_Flag",
            "TIM_PV",
            "CNT_PV",
            "DM_Bit",
            "DM_Word"});
            this.cmbWArea.Location = new System.Drawing.Point(101, 46);
            this.cmbWArea.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWArea.Name = "cmbWArea";
            this.cmbWArea.Size = new System.Drawing.Size(86, 29);
            this.cmbWArea.TabIndex = 2;
            // 
            // rtxtWriteResponse
            // 
            this.rtxtWriteResponse.Location = new System.Drawing.Point(17, 257);
            this.rtxtWriteResponse.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtWriteResponse.Name = "rtxtWriteResponse";
            this.rtxtWriteResponse.Size = new System.Drawing.Size(311, 82);
            this.rtxtWriteResponse.TabIndex = 7;
            this.rtxtWriteResponse.Text = "";
            // 
            // txtWpoint
            // 
            this.txtWpoint.Location = new System.Drawing.Point(10, 46);
            this.txtWpoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtWpoint.Name = "txtWpoint";
            this.txtWpoint.Size = new System.Drawing.Size(83, 32);
            this.txtWpoint.TabIndex = 1;
            // 
            // btnWritePLC
            // 
            this.btnWritePLC.Location = new System.Drawing.Point(256, 114);
            this.btnWritePLC.Margin = new System.Windows.Forms.Padding(4);
            this.btnWritePLC.Name = "btnWritePLC";
            this.btnWritePLC.Size = new System.Drawing.Size(83, 30);
            this.btnWritePLC.TabIndex = 0;
            this.btnWritePLC.Text = "写入";
            this.btnWritePLC.UseVisualStyleBackColor = true;
            this.btnWritePLC.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.cbmStopBits);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtdataBits);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbCOM);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(11, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1270, 102);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口通信";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(1140, 44);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(120, 32);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbmStopBits
            // 
            this.cbmStopBits.FormattingEnabled = true;
            this.cbmStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePointFive",
            "Two"});
            this.cbmStopBits.Location = new System.Drawing.Point(987, 47);
            this.cbmStopBits.Name = "cbmStopBits";
            this.cbmStopBits.Size = new System.Drawing.Size(114, 29);
            this.cbmStopBits.TabIndex = 9;
            this.cbmStopBits.Text = "Two";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(910, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 21);
            this.label11.TabIndex = 8;
            this.label11.Text = "停止位:";
            // 
            // txtdataBits
            // 
            this.txtdataBits.Location = new System.Drawing.Point(804, 47);
            this.txtdataBits.Name = "txtdataBits";
            this.txtdataBits.Size = new System.Drawing.Size(100, 32);
            this.txtdataBits.TabIndex = 7;
            this.txtdataBits.Text = "7";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(724, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 21);
            this.label10.TabIndex = 6;
            this.label10.Text = "数据位:";
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Odd",
            "Space"});
            this.cmbParity.Location = new System.Drawing.Point(601, 47);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(114, 29);
            this.cmbParity.TabIndex = 5;
            this.cmbParity.Text = "Even";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(486, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 21);
            this.label9.TabIndex = 4;
            this.label9.Text = "奇偶校验位:";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(355, 47);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(110, 32);
            this.txtRate.TabIndex = 3;
            this.txtRate.Text = "9600";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(278, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "波特率:";
            // 
            // cmbCOM
            // 
            this.cmbCOM.FormattingEnabled = true;
            this.cmbCOM.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cmbCOM.Location = new System.Drawing.Point(135, 47);
            this.cmbCOM.Name = "cmbCOM";
            this.cmbCOM.Size = new System.Drawing.Size(121, 29);
            this.cmbCOM.TabIndex = 1;
            this.cmbCOM.Text = "COM5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "COM端口:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.欧姆龙指令发送);
            this.tabControl1.Controls.Add(this.精简命令发送);
            this.tabControl1.Controls.Add(this.欧姆龙写入);
            this.tabControl1.Controls.Add(this.欧姆龙状态控制);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(11, 115);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(544, 436);
            this.tabControl1.TabIndex = 8;
            // 
            // 欧姆龙指令发送
            // 
            this.欧姆龙指令发送.Controls.Add(this.gbOmronCommand);
            this.欧姆龙指令发送.Location = new System.Drawing.Point(4, 30);
            this.欧姆龙指令发送.Name = "欧姆龙指令发送";
            this.欧姆龙指令发送.Padding = new System.Windows.Forms.Padding(3);
            this.欧姆龙指令发送.Size = new System.Drawing.Size(536, 402);
            this.欧姆龙指令发送.TabIndex = 0;
            this.欧姆龙指令发送.Text = "欧姆龙命令";
            this.欧姆龙指令发送.UseVisualStyleBackColor = true;
            // 
            // gbOmronCommand
            // 
            this.gbOmronCommand.Controls.Add(this.label20);
            this.gbOmronCommand.Controls.Add(this.label19);
            this.gbOmronCommand.Controls.Add(this.btnSend);
            this.gbOmronCommand.Controls.Add(this.rtxtResponse);
            this.gbOmronCommand.Controls.Add(this.rtxtCommand);
            this.gbOmronCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOmronCommand.Enabled = false;
            this.gbOmronCommand.Location = new System.Drawing.Point(3, 3);
            this.gbOmronCommand.Name = "gbOmronCommand";
            this.gbOmronCommand.Size = new System.Drawing.Size(530, 396);
            this.gbOmronCommand.TabIndex = 7;
            this.gbOmronCommand.TabStop = false;
            this.gbOmronCommand.Text = "命令输入输出";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 28);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(86, 21);
            this.label20.TabIndex = 14;
            this.label20.Text = "输出报文";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 190);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 21);
            this.label19.TabIndex = 14;
            this.label19.Text = "输入报文";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(103, 342);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(178, 48);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtxtResponse
            // 
            this.rtxtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtResponse.Location = new System.Drawing.Point(7, 62);
            this.rtxtResponse.Name = "rtxtResponse";
            this.rtxtResponse.Size = new System.Drawing.Size(514, 123);
            this.rtxtResponse.TabIndex = 1;
            this.rtxtResponse.Text = "";
            // 
            // rtxtCommand
            // 
            this.rtxtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtCommand.Location = new System.Drawing.Point(7, 214);
            this.rtxtCommand.Name = "rtxtCommand";
            this.rtxtCommand.Size = new System.Drawing.Size(514, 123);
            this.rtxtCommand.TabIndex = 0;
            this.rtxtCommand.Text = "";
            // 
            // 精简命令发送
            // 
            this.精简命令发送.Controls.Add(this.label29);
            this.精简命令发送.Controls.Add(this.textBox1);
            this.精简命令发送.Controls.Add(this.txt读取指令状态信息);
            this.精简命令发送.Controls.Add(this.btn欧姆龙读取);
            this.精简命令发送.Controls.Add(this.cmb数据类型);
            this.精简命令发送.Controls.Add(this.label24);
            this.精简命令发送.Controls.Add(this.txt读取地址);
            this.精简命令发送.Controls.Add(this.label23);
            this.精简命令发送.Location = new System.Drawing.Point(4, 30);
            this.精简命令发送.Name = "精简命令发送";
            this.精简命令发送.Padding = new System.Windows.Forms.Padding(3);
            this.精简命令发送.Size = new System.Drawing.Size(536, 402);
            this.精简命令发送.TabIndex = 1;
            this.精简命令发送.Text = "欧姆龙读取";
            this.精简命令发送.UseVisualStyleBackColor = true;
            // 
            // txt读取指令状态信息
            // 
            this.txt读取指令状态信息.Location = new System.Drawing.Point(34, 140);
            this.txt读取指令状态信息.Margin = new System.Windows.Forms.Padding(4);
            this.txt读取指令状态信息.Name = "txt读取指令状态信息";
            this.txt读取指令状态信息.Size = new System.Drawing.Size(495, 249);
            this.txt读取指令状态信息.TabIndex = 18;
            this.txt读取指令状态信息.Text = "";
            // 
            // btn欧姆龙读取
            // 
            this.btn欧姆龙读取.Location = new System.Drawing.Point(409, 95);
            this.btn欧姆龙读取.Name = "btn欧姆龙读取";
            this.btn欧姆龙读取.Size = new System.Drawing.Size(121, 38);
            this.btn欧姆龙读取.TabIndex = 17;
            this.btn欧姆龙读取.Text = "读取";
            this.btn欧姆龙读取.UseVisualStyleBackColor = true;
            this.btn欧姆龙读取.Click += new System.EventHandler(this.btn欧姆龙读取_Click);
            // 
            // cmb数据类型
            // 
            this.cmb数据类型.FormattingEnabled = true;
            this.cmb数据类型.Items.AddRange(new object[] {
            "Bit",
            "Word"});
            this.cmb数据类型.Location = new System.Drawing.Point(177, 60);
            this.cmb数据类型.Margin = new System.Windows.Forms.Padding(4);
            this.cmb数据类型.Name = "cmb数据类型";
            this.cmb数据类型.Size = new System.Drawing.Size(127, 29);
            this.cmb数据类型.TabIndex = 16;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(82, 63);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(86, 21);
            this.label24.TabIndex = 2;
            this.label24.Text = "数据类型";
            // 
            // txt读取地址
            // 
            this.txt读取地址.Location = new System.Drawing.Point(177, 13);
            this.txt读取地址.Name = "txt读取地址";
            this.txt读取地址.Size = new System.Drawing.Size(127, 32);
            this.txt读取地址.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(82, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(83, 21);
            this.label23.TabIndex = 0;
            this.label23.Text = "点       位";
            // 
            // 欧姆龙写入
            // 
            this.欧姆龙写入.Controls.Add(this.txt写入值);
            this.欧姆龙写入.Controls.Add(this.richTextBox1);
            this.欧姆龙写入.Controls.Add(this.btn欧姆龙写入);
            this.欧姆龙写入.Controls.Add(this.cbox写入数据类型);
            this.欧姆龙写入.Controls.Add(this.label25);
            this.欧姆龙写入.Controls.Add(this.txt写入地址);
            this.欧姆龙写入.Controls.Add(this.label26);
            this.欧姆龙写入.Location = new System.Drawing.Point(4, 30);
            this.欧姆龙写入.Name = "欧姆龙写入";
            this.欧姆龙写入.Padding = new System.Windows.Forms.Padding(3);
            this.欧姆龙写入.Size = new System.Drawing.Size(536, 402);
            this.欧姆龙写入.TabIndex = 2;
            this.欧姆龙写入.Text = "欧姆龙写入";
            this.欧姆龙写入.UseVisualStyleBackColor = true;
            // 
            // txt写入值
            // 
            this.txt写入值.Location = new System.Drawing.Point(7, 107);
            this.txt写入值.Multiline = true;
            this.txt写入值.Name = "txt写入值";
            this.txt写入值.Size = new System.Drawing.Size(511, 66);
            this.txt写入值.TabIndex = 25;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(7, 227);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(522, 165);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            // 
            // btn欧姆龙写入
            // 
            this.btn欧姆龙写入.Location = new System.Drawing.Point(397, 185);
            this.btn欧姆龙写入.Name = "btn欧姆龙写入";
            this.btn欧姆龙写入.Size = new System.Drawing.Size(121, 38);
            this.btn欧姆龙写入.TabIndex = 23;
            this.btn欧姆龙写入.Text = "写入";
            this.btn欧姆龙写入.UseVisualStyleBackColor = true;
            this.btn欧姆龙写入.Click += new System.EventHandler(this.btn欧姆龙写入_Click);
            // 
            // cbox写入数据类型
            // 
            this.cbox写入数据类型.FormattingEnabled = true;
            this.cbox写入数据类型.Items.AddRange(new object[] {
            "Bit",
            "Word"});
            this.cbox写入数据类型.Location = new System.Drawing.Point(256, 72);
            this.cbox写入数据类型.Margin = new System.Windows.Forms.Padding(4);
            this.cbox写入数据类型.Name = "cbox写入数据类型";
            this.cbox写入数据类型.Size = new System.Drawing.Size(127, 29);
            this.cbox写入数据类型.TabIndex = 22;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(161, 75);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(86, 21);
            this.label25.TabIndex = 21;
            this.label25.Text = "数据类型";
            // 
            // txt写入地址
            // 
            this.txt写入地址.Location = new System.Drawing.Point(256, 21);
            this.txt写入地址.Name = "txt写入地址";
            this.txt写入地址.Size = new System.Drawing.Size(127, 32);
            this.txt写入地址.TabIndex = 20;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(161, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(83, 21);
            this.label26.TabIndex = 19;
            this.label26.Text = "点       位";
            // 
            // 欧姆龙状态控制
            // 
            this.欧姆龙状态控制.Controls.Add(this.txt状态控制);
            this.欧姆龙状态控制.Controls.Add(this.btn控制给0);
            this.欧姆龙状态控制.Controls.Add(this.btn控制给1);
            this.欧姆龙状态控制.Controls.Add(this.cbox控制点位数据类型);
            this.欧姆龙状态控制.Controls.Add(this.label27);
            this.欧姆龙状态控制.Controls.Add(this.txt控制点位);
            this.欧姆龙状态控制.Controls.Add(this.label28);
            this.欧姆龙状态控制.Location = new System.Drawing.Point(4, 30);
            this.欧姆龙状态控制.Name = "欧姆龙状态控制";
            this.欧姆龙状态控制.Padding = new System.Windows.Forms.Padding(3);
            this.欧姆龙状态控制.Size = new System.Drawing.Size(536, 402);
            this.欧姆龙状态控制.TabIndex = 3;
            this.欧姆龙状态控制.Text = "欧姆龙状态控制";
            this.欧姆龙状态控制.UseVisualStyleBackColor = true;
            // 
            // txt状态控制
            // 
            this.txt状态控制.Location = new System.Drawing.Point(7, 164);
            this.txt状态控制.Name = "txt状态控制";
            this.txt状态控制.Size = new System.Drawing.Size(523, 232);
            this.txt状态控制.TabIndex = 29;
            this.txt状态控制.Text = "";
            // 
            // btn控制给0
            // 
            this.btn控制给0.Location = new System.Drawing.Point(310, 120);
            this.btn控制给0.Name = "btn控制给0";
            this.btn控制给0.Size = new System.Drawing.Size(91, 35);
            this.btn控制给0.TabIndex = 28;
            this.btn控制给0.Text = "置0";
            this.btn控制给0.UseVisualStyleBackColor = true;
            this.btn控制给0.Click += new System.EventHandler(this.btn控制给0_Click);
            // 
            // btn控制给1
            // 
            this.btn控制给1.Location = new System.Drawing.Point(166, 120);
            this.btn控制给1.Name = "btn控制给1";
            this.btn控制给1.Size = new System.Drawing.Size(91, 35);
            this.btn控制给1.TabIndex = 27;
            this.btn控制给1.Text = "置1";
            this.btn控制给1.UseVisualStyleBackColor = true;
            this.btn控制给1.Click += new System.EventHandler(this.btn控制给1_Click);
            // 
            // cbox控制点位数据类型
            // 
            this.cbox控制点位数据类型.FormattingEnabled = true;
            this.cbox控制点位数据类型.Items.AddRange(new object[] {
            "Bit",
            "Word"});
            this.cbox控制点位数据类型.Location = new System.Drawing.Point(259, 72);
            this.cbox控制点位数据类型.Margin = new System.Windows.Forms.Padding(4);
            this.cbox控制点位数据类型.Name = "cbox控制点位数据类型";
            this.cbox控制点位数据类型.Size = new System.Drawing.Size(127, 29);
            this.cbox控制点位数据类型.TabIndex = 26;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(164, 75);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(86, 21);
            this.label27.TabIndex = 25;
            this.label27.Text = "数据类型";
            // 
            // txt控制点位
            // 
            this.txt控制点位.Location = new System.Drawing.Point(259, 21);
            this.txt控制点位.Name = "txt控制点位";
            this.txt控制点位.Size = new System.Drawing.Size(127, 32);
            this.txt控制点位.TabIndex = 24;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(164, 24);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(83, 21);
            this.label28.TabIndex = 23;
            this.label28.Text = "点       位";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(177, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 32);
            this.textBox1.TabIndex = 19;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(30, 104);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(143, 21);
            this.label29.TabIndex = 20;
            this.label29.Text = "读取的数据通道";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 558);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbOmronWrite);
            this.Controls.Add(this.gbOmronRead);
            this.Font = new System.Drawing.Font("华文楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "壹星科技欧姆龙通讯测试工具";
            this.gbOmronRead.ResumeLayout(false);
            this.gbOmronRead.PerformLayout();
            this.gbOmronWrite.ResumeLayout(false);
            this.gbOmronWrite.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.欧姆龙指令发送.ResumeLayout(false);
            this.gbOmronCommand.ResumeLayout(false);
            this.gbOmronCommand.PerformLayout();
            this.精简命令发送.ResumeLayout(false);
            this.精简命令发送.PerformLayout();
            this.欧姆龙写入.ResumeLayout(false);
            this.欧姆龙写入.PerformLayout();
            this.欧姆龙状态控制.ResumeLayout(false);
            this.欧姆龙状态控制.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadPLC;
        private System.Windows.Forms.TextBox txtReadPoint;
        private System.Windows.Forms.GroupBox gbOmronRead;
        private System.Windows.Forms.RichTextBox rtxtReadResponse;
        private System.Windows.Forms.RichTextBox rtxtReadSend;
        private System.Windows.Forms.GroupBox gbOmronWrite;
        private System.Windows.Forms.RichTextBox rtxtWriteCommand;
        private System.Windows.Forms.RichTextBox rtxtWriteResponse;
        private System.Windows.Forms.Button btnWritePLC;
        private System.Windows.Forms.ComboBox cmbRArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCOM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox cbmStopBits;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtdataBits;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReadBit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtWbit;
        private System.Windows.Forms.ComboBox cmbWArea;
        private System.Windows.Forms.TextBox txtWpoint;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtWData;
        private System.Windows.Forms.RichTextBox rtxtReadValue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RichTextBox rtxtWriteValue;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbRDataType;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbWDataType;
        private System.Windows.Forms.CheckBox cboxW;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 欧姆龙指令发送;
        private System.Windows.Forms.GroupBox gbOmronCommand;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox rtxtResponse;
        private System.Windows.Forms.RichTextBox rtxtCommand;
        private System.Windows.Forms.TabPage 精简命令发送;
        private System.Windows.Forms.TextBox txt读取地址;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btn欧姆龙读取;
        private System.Windows.Forms.ComboBox cmb数据类型;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.RichTextBox txt读取指令状态信息;
        private System.Windows.Forms.TabPage 欧姆龙写入;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn欧姆龙写入;
        private System.Windows.Forms.ComboBox cbox写入数据类型;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt写入地址;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt写入值;
        private System.Windows.Forms.TabPage 欧姆龙状态控制;
        private System.Windows.Forms.Button btn控制给0;
        private System.Windows.Forms.Button btn控制给1;
        private System.Windows.Forms.ComboBox cbox控制点位数据类型;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt控制点位;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.RichTextBox txt状态控制;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label29;
    }
}
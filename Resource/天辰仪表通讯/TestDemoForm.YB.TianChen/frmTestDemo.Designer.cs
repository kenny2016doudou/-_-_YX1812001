namespace TestDemoForm.YB.TianChen
{
    partial class frmTestDemo
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
            this.SP_YB = new System.IO.Ports.SerialPort(this.components);
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.nudZLDY = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.nudJLDY = new System.Windows.Forms.NumericUpDown();
            this.btnSet = new System.Windows.Forms.Button();
            this.timer_YB = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ledXLDL = new LxControl.LxLedControl();
            this.ledZLNY = new LxControl.LxLedControl();
            this.ledJLNY = new LxControl.LxLedControl();
            this.ledJSB = new LxControl.LxLedControl();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZLDY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJLDY)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledXLDL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledZLNY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledJLNY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledJSB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnSet);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.label22);
            this.groupBox10.Controls.Add(this.nudZLDY);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.nudJLDY);
            this.groupBox10.Location = new System.Drawing.Point(12, 150);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox10.Size = new System.Drawing.Size(530, 97);
            this.groupBox10.TabIndex = 170;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "电压值设置";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(216, 47);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 37;
            this.label17.Text = "KV";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(139, 26);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 14);
            this.label22.TabIndex = 36;
            this.label22.Text = "直流电压：";
            // 
            // nudZLDY
            // 
            this.nudZLDY.DecimalPlaces = 1;
            this.nudZLDY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudZLDY.Location = new System.Drawing.Point(143, 43);
            this.nudZLDY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudZLDY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudZLDY.Name = "nudZLDY";
            this.nudZLDY.Size = new System.Drawing.Size(67, 21);
            this.nudZLDY.TabIndex = 35;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(93, 47);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 33;
            this.label20.Text = "KV";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(16, 26);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 14);
            this.label18.TabIndex = 31;
            this.label18.Text = "交流电压：";
            // 
            // nudJLDY
            // 
            this.nudJLDY.DecimalPlaces = 1;
            this.nudJLDY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudJLDY.Location = new System.Drawing.Point(21, 43);
            this.nudJLDY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudJLDY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudJLDY.Name = "nudJLDY";
            this.nudJLDY.Size = new System.Drawing.Size(67, 21);
            this.nudJLDY.TabIndex = 28;
            // 
            // btnSet
            // 
            this.btnSet.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.Location = new System.Drawing.Point(322, 27);
            this.btnSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(112, 32);
            this.btnSet.TabIndex = 38;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // timer_YB
            // 
            this.timer_YB.Tick += new System.EventHandler(this.timer_YB_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ledJSB);
            this.groupBox1.Controls.Add(this.ledXLDL);
            this.groupBox1.Controls.Add(this.ledZLNY);
            this.groupBox1.Controls.Add(this.ledJLNY);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 118);
            this.groupBox1.TabIndex = 171;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仪表数显仪";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(273, 72);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 16);
            this.label12.TabIndex = 124;
            this.label12.Text = "泄露电流(mA)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(134, 72);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 123;
            this.label6.Text = "直流耐压(Kv)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(14, 72);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 122;
            this.label7.Text = "交流耐压(Kv)";
            // 
            // ledXLDL
            // 
            this.ledXLDL.BackColor = System.Drawing.Color.Transparent;
            this.ledXLDL.BackColor_1 = System.Drawing.Color.Black;
            this.ledXLDL.BackColor_2 = System.Drawing.Color.DimGray;
            this.ledXLDL.BevelRate = 0.5F;
            this.ledXLDL.FadedColor = System.Drawing.Color.DimGray;
            this.ledXLDL.ForeColor = System.Drawing.Color.Lime;
            this.ledXLDL.HighlightOpaque = ((byte)(50));
            this.ledXLDL.Location = new System.Drawing.Point(276, 31);
            this.ledXLDL.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ledXLDL.Name = "ledXLDL";
            this.ledXLDL.Size = new System.Drawing.Size(91, 24);
            this.ledXLDL.TabIndex = 142;
            this.ledXLDL.Text = "000000";
            this.ledXLDL.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledXLDL.TotalCharCount = 6;
            // 
            // ledZLNY
            // 
            this.ledZLNY.BackColor = System.Drawing.Color.Transparent;
            this.ledZLNY.BackColor_1 = System.Drawing.Color.Black;
            this.ledZLNY.BackColor_2 = System.Drawing.Color.DimGray;
            this.ledZLNY.BevelRate = 0.5F;
            this.ledZLNY.FadedColor = System.Drawing.Color.DimGray;
            this.ledZLNY.ForeColor = System.Drawing.Color.Lime;
            this.ledZLNY.HighlightOpaque = ((byte)(50));
            this.ledZLNY.Location = new System.Drawing.Point(128, 31);
            this.ledZLNY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ledZLNY.Name = "ledZLNY";
            this.ledZLNY.Size = new System.Drawing.Size(91, 24);
            this.ledZLNY.TabIndex = 141;
            this.ledZLNY.Text = "000000";
            this.ledZLNY.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledZLNY.TotalCharCount = 6;
            // 
            // ledJLNY
            // 
            this.ledJLNY.BackColor = System.Drawing.Color.Transparent;
            this.ledJLNY.BackColor_1 = System.Drawing.Color.Black;
            this.ledJLNY.BackColor_2 = System.Drawing.Color.DimGray;
            this.ledJLNY.BevelRate = 0.5F;
            this.ledJLNY.FadedColor = System.Drawing.Color.DimGray;
            this.ledJLNY.ForeColor = System.Drawing.Color.Lime;
            this.ledJLNY.HighlightOpaque = ((byte)(50));
            this.ledJLNY.Location = new System.Drawing.Point(7, 31);
            this.ledJLNY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ledJLNY.Name = "ledJLNY";
            this.ledJLNY.Size = new System.Drawing.Size(91, 24);
            this.ledJLNY.TabIndex = 140;
            this.ledJLNY.Text = "000000";
            this.ledJLNY.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledJLNY.TotalCharCount = 6;
            // 
            // ledJSB
            // 
            this.ledJSB.BackColor = System.Drawing.Color.Transparent;
            this.ledJSB.BackColor_1 = System.Drawing.Color.Black;
            this.ledJSB.BackColor_2 = System.Drawing.Color.DimGray;
            this.ledJSB.BevelRate = 0.5F;
            this.ledJSB.FadedColor = System.Drawing.Color.DimGray;
            this.ledJSB.ForeColor = System.Drawing.Color.Lime;
            this.ledJSB.HighlightOpaque = ((byte)(50));
            this.ledJSB.Location = new System.Drawing.Point(403, 31);
            this.ledJSB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ledJSB.Name = "ledJSB";
            this.ledJSB.Size = new System.Drawing.Size(91, 24);
            this.ledJSB.TabIndex = 143;
            this.ledJSB.Text = "000000";
            this.ledJSB.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledJSB.TotalCharCount = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(414, 72);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 16);
            this.label13.TabIndex = 144;
            this.label13.Text = "计时表(S)";
            // 
            // frmTestDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 271);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox10);
            this.Name = "frmTestDemo";
            this.Text = "天辰仪表示例";
            this.Load += new System.EventHandler(this.frmTestDemo_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZLDY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJLDY)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledXLDL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledZLNY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledJLNY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledJSB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort SP_YB;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown nudZLDY;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudJLDY;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Timer timer_YB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private LxControl.LxLedControl ledXLDL;
        private LxControl.LxLedControl ledZLNY;
        private LxControl.LxLedControl ledJLNY;
        private LxControl.LxLedControl ledJSB;
        private System.Windows.Forms.Label label13;
    }
}
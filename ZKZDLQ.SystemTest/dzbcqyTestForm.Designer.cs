namespace ZKZDLQ.SystemTest
{
    partial class dzbcqyTestForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dzbcqyTestForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txt_QiYa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bufferedAiCtrl1 = new Automation.BDaq.BufferedAiCtrl(this.components);
            this.openQYButton = new GlassButton.GlassButton();
            this.glassButton1 = new GlassButton.GlassButton();
            this.glassButton2 = new GlassButton.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            chartArea2.AxisX.Interval = 50;
            chartArea2.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Aquamarine;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.Maximum = 750;
            chartArea2.AxisX.Minimum = 0;
            chartArea2.AxisX.ScaleBreakStyle.Spacing = 1;
            chartArea2.AxisX.Title = "时间(ms)";
            chartArea2.AxisY.Interval = 0.5;
            chartArea2.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Aquamarine;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY.Maximum = 5.5;
            chartArea2.AxisY.Minimum = -0.5;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 90F;
            chartArea2.Position.Width = 100F;
            chartArea2.Position.Y = 8F;
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Top;
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.InterlacedRowsColor = System.Drawing.Color.White;
            legend2.Name = "Legend1";
            legend2.TitleBackColor = System.Drawing.Color.White;
            legend2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Cyan;
            series4.Legend = "Legend1";
            series4.Name = "合闸";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Color = System.Drawing.Color.Black;
            series5.Legend = "Legend1";
            series5.Name = "分闸";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Color = System.Drawing.Color.Red;
            series6.Legend = "Legend1";
            series6.Name = "主触头";
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(578, 222);
            this.chart1.TabIndex = 16;
            this.chart1.Text = "chart1";
            // 
            // txt_QiYa
            // 
            this.txt_QiYa.BackColor = System.Drawing.Color.White;
            this.txt_QiYa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_QiYa.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_QiYa.Location = new System.Drawing.Point(99, 224);
            this.txt_QiYa.Name = "txt_QiYa";
            this.txt_QiYa.ReadOnly = true;
            this.txt_QiYa.Size = new System.Drawing.Size(91, 35);
            this.txt_QiYa.TabIndex = 28;
            this.txt_QiYa.Text = "0";
            this.txt_QiYa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(196, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 22);
            this.label3.TabIndex = 27;
            this.label3.Text = "（kPa）";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(2, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 23);
            this.label4.TabIndex = 26;
            this.label4.Text = "当前气压";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bufferedAiCtrl1
            // 
            this.bufferedAiCtrl1._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("bufferedAiCtrl1._StateStream")));
            this.bufferedAiCtrl1.Stopped += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.bufferedAiCtrl1_Stopped);
            // 
            // openQYButton
            // 
            this.openQYButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.openQYButton.Location = new System.Drawing.Point(495, 230);
            this.openQYButton.Name = "openQYButton";
            this.openQYButton.Size = new System.Drawing.Size(83, 29);
            this.openQYButton.TabIndex = 29;
            this.openQYButton.Text = "停止";
            this.openQYButton.Click += new System.EventHandler(this.openQYButton_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton1.Location = new System.Drawing.Point(293, 230);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(51, 29);
            this.glassButton1.TabIndex = 29;
            this.glassButton1.Text = "置位";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.Location = new System.Drawing.Point(350, 230);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(48, 29);
            this.glassButton2.TabIndex = 29;
            this.glassButton2.Text = "复位";
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // dzbcqyTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 265);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.openQYButton);
            this.Controls.Add(this.txt_QiYa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dzbcqyTestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "dzbcqyTestForm";
            this.Load += new System.EventHandler(this.dzbcqyTestForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dzbcqyTestForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox txt_QiYa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private Automation.BDaq.BufferedAiCtrl bufferedAiCtrl1;
        private GlassButton.GlassButton openQYButton;
        private GlassButton.GlassButton glassButton1;
        private GlassButton.GlassButton glassButton2;
    }
}
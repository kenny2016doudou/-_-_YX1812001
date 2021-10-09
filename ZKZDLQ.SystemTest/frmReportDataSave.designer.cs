namespace ZKZDLQ.SystemTest
{
    partial class frmReportDataSave
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检测检修项目 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检修标准 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检修结果 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.处理意见 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.自检 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.互检员 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.测试数据ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_quit = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_cxch = new System.Windows.Forms.Label();
            this.lbl_zdlq = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.检测检修项目,
            this.检修标准,
            this.检修结果,
            this.处理意见,
            this.自检,
            this.互检员,
            this.测试数据ID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(3, 111);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowTemplate.Height = 60;
            this.dataGridView1.Size = new System.Drawing.Size(1037, 560);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // 序号
            // 
            this.序号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.序号.DataPropertyName = "序号";
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.序号.Width = 51;
            // 
            // 检测检修项目
            // 
            this.检测检修项目.DataPropertyName = "检测检修项目";
            this.检测检修项目.HeaderText = "检测检修项目";
            this.检测检修项目.Name = "检测检修项目";
            this.检测检修项目.ReadOnly = true;
            this.检测检修项目.Width = 200;
            // 
            // 检修标准
            // 
            this.检修标准.DataPropertyName = "检修标准";
            this.检修标准.HeaderText = "检修标准";
            this.检修标准.Name = "检修标准";
            this.检修标准.ReadOnly = true;
            this.检修标准.Width = 200;
            // 
            // 检修结果
            // 
            this.检修结果.DataPropertyName = "检修结果";
            this.检修结果.HeaderText = "检修结果";
            this.检修结果.Name = "检修结果";
            this.检修结果.Width = 150;
            // 
            // 处理意见
            // 
            this.处理意见.DataPropertyName = "处理意见";
            this.处理意见.HeaderText = "处理意见";
            this.处理意见.Name = "处理意见";
            this.处理意见.Width = 150;
            // 
            // 自检
            // 
            this.自检.DataPropertyName = "自检";
            this.自检.HeaderText = "自检";
            this.自检.Name = "自检";
            this.自检.ReadOnly = true;
            this.自检.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 互检员
            // 
            this.互检员.DataPropertyName = "互检员";
            this.互检员.HeaderText = "互检员";
            this.互检员.Name = "互检员";
            this.互检员.ReadOnly = true;
            this.互检员.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 测试数据ID
            // 
            this.测试数据ID.DataPropertyName = "测试数据ID";
            this.测试数据ID.HeaderText = "测试数据ID";
            this.测试数据ID.Name = "测试数据ID";
            this.测试数据ID.Width = 5;
            // 
            // btn_quit
            // 
            this.btn_quit.Location = new System.Drawing.Point(912, 677);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(123, 36);
            this.btn_quit.TabIndex = 1;
            this.btn_quit.Text = "退出";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 677);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(200, 36);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "主断路器编号：";
            // 
            // lbl_cxch
            // 
            this.lbl_cxch.AutoSize = true;
            this.lbl_cxch.Font = new System.Drawing.Font("宋体", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_cxch.ForeColor = System.Drawing.Color.Red;
            this.lbl_cxch.Location = new System.Drawing.Point(722, 41);
            this.lbl_cxch.Name = "lbl_cxch";
            this.lbl_cxch.Size = new System.Drawing.Size(33, 33);
            this.lbl_cxch.TabIndex = 3;
            this.lbl_cxch.Text = "2";
            // 
            // lbl_zdlq
            // 
            this.lbl_zdlq.AutoSize = true;
            this.lbl_zdlq.Font = new System.Drawing.Font("宋体", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_zdlq.ForeColor = System.Drawing.Color.Red;
            this.lbl_zdlq.Location = new System.Drawing.Point(264, 41);
            this.lbl_zdlq.Name = "lbl_zdlq";
            this.lbl_zdlq.Size = new System.Drawing.Size(33, 33);
            this.lbl_zdlq.TabIndex = 3;
            this.lbl_zdlq.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(537, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "拆下车号：";
            // 
            // frmReportDataSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 719);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_cxch);
            this.Controls.Add(this.lbl_zdlq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmReportDataSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检   修   记   录   内   容";
            this.Load += new System.EventHandler(this.frmReportDataSave_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 检测检修项目;
        private System.Windows.Forms.DataGridViewTextBoxColumn 检修标准;
        private System.Windows.Forms.DataGridViewTextBoxColumn 检修结果;
        private System.Windows.Forms.DataGridViewTextBoxColumn 处理意见;
        private System.Windows.Forms.DataGridViewTextBoxColumn 自检;
        private System.Windows.Forms.DataGridViewTextBoxColumn 互检员;
        private System.Windows.Forms.DataGridViewTextBoxColumn 测试数据ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_cxch;
        private System.Windows.Forms.Label lbl_zdlq;
        private System.Windows.Forms.Label label4;

    }
}
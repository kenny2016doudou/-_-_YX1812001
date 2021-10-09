namespace TestSystem.FormManage
{
    partial class frmMain
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
            this.btnExit = new System.Windows.Forms.Button();
            this.labTitle = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.picSystemSet = new System.Windows.Forms.PictureBox();
            this.picTest = new System.Windows.Forms.PictureBox();
            this.picUserManual = new System.Windows.Forms.PictureBox();
            this.picDataQuery = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("楷体_GB2312", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(767, 561);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 32);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退  出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button5_Click);
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("楷体_GB2312", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.Location = new System.Drawing.Point(302, 50);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(216, 48);
            this.labTitle.TabIndex = 9;
            this.labTitle.Text = "试验系统";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::TestSystem.FormManage.Properties.Resources.LOGO1;
            this.pictureBox3.Location = new System.Drawing.Point(97, 24);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(199, 86);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 131;
            this.pictureBox3.TabStop = false;
            // 
            // picSystemSet
            // 
            this.picSystemSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSystemSet.Image = global::TestSystem.FormManage.Properties.Resources.image005;
            this.picSystemSet.Location = new System.Drawing.Point(579, 333);
            this.picSystemSet.Name = "picSystemSet";
            this.picSystemSet.Size = new System.Drawing.Size(183, 109);
            this.picSystemSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSystemSet.TabIndex = 8;
            this.picSystemSet.TabStop = false;
            this.picSystemSet.Click += new System.EventHandler(this.picSystemSet_Click);
            // 
            // picTest
            // 
            this.picTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTest.Image = global::TestSystem.FormManage.Properties.Resources.image009;
            this.picTest.Location = new System.Drawing.Point(247, 149);
            this.picTest.Name = "picTest";
            this.picTest.Size = new System.Drawing.Size(175, 105);
            this.picTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTest.TabIndex = 7;
            this.picTest.TabStop = false;
            this.picTest.Click += new System.EventHandler(this.picTest_Click);
            // 
            // picUserManual
            // 
            this.picUserManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUserManual.Image = global::TestSystem.FormManage.Properties.Resources.image017;
            this.picUserManual.Location = new System.Drawing.Point(246, 337);
            this.picUserManual.Name = "picUserManual";
            this.picUserManual.Size = new System.Drawing.Size(176, 105);
            this.picUserManual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUserManual.TabIndex = 6;
            this.picUserManual.TabStop = false;
            this.picUserManual.Click += new System.EventHandler(this.picUserManual_Click);
            // 
            // picDataQuery
            // 
            this.picDataQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDataQuery.Image = global::TestSystem.FormManage.Properties.Resources.image013;
            this.picDataQuery.Location = new System.Drawing.Point(588, 150);
            this.picDataQuery.Name = "picDataQuery";
            this.picDataQuery.Size = new System.Drawing.Size(174, 104);
            this.picDataQuery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDataQuery.TabIndex = 5;
            this.picDataQuery.TabStop = false;
            this.picDataQuery.Click += new System.EventHandler(this.picDataQuery_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 674);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.picSystemSet);
            this.Controls.Add(this.picTest);
            this.Controls.Add(this.picUserManual);
            this.Controls.Add(this.picDataQuery);
            this.Controls.Add(this.btnExit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验系统";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDataQuery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picSystemSet;
        private System.Windows.Forms.PictureBox picTest;
        private System.Windows.Forms.PictureBox picUserManual;
        private System.Windows.Forms.PictureBox picDataQuery;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}


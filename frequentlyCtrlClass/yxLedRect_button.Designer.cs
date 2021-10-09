namespace frequentlyCtrlClass
{
    partial class yxLedRect_button
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glassButton1 = new GlassButton.GlassButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // glassButton1
            // 
            this.glassButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glassButton1.ForeColor = System.Drawing.Color.Red;
            this.glassButton1.InnerBorderColor = System.Drawing.Color.Gray;
            this.glassButton1.Location = new System.Drawing.Point(0, 0);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(97, 25);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "glassButton1";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // yxLedRect_button
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.glassButton1);
            this.Name = "yxLedRect_button";
            this.Size = new System.Drawing.Size(97, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.yxLedRect_Paint);
            this.ResumeLayout(false);

        }

        #endregion

#pragma warning disable CS0246 // 未能找到类型或命名空间名“GlassButton”(是否缺少 using 指令或程序集引用?)
        private GlassButton.GlassButton glassButton1;
#pragma warning restore CS0246 // 未能找到类型或命名空间名“GlassButton”(是否缺少 using 指令或程序集引用?)
        private System.Windows.Forms.ToolTip toolTip1;




    }
}

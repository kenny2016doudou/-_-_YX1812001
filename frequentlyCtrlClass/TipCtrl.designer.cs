namespace frequentlyCtrlClass
{
    partial class TipCtrl
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
            this.label1 = new System.Windows.Forms.Label();
            this.ovalShape7 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "未定义";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ovalShape7
            // 
            this.ovalShape7.BackColor = System.Drawing.Color.White;
            this.ovalShape7.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.ovalShape7.BorderColor = System.Drawing.Color.MintCream;
            this.ovalShape7.FillColor = System.Drawing.Color.Lime;
            this.ovalShape7.FillGradientColor = System.Drawing.Color.White;
            this.ovalShape7.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.ovalShape7.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.ovalShape7.Location = new System.Drawing.Point(2, 0);
            this.ovalShape7.Name = "ovalShape7";
            this.ovalShape7.SelectionColor = System.Drawing.SystemColors.HighlightText;
            this.ovalShape7.Size = new System.Drawing.Size(30, 32);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.shapeContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(171, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 31);
            this.panel1.TabIndex = 2;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ovalShape7});
            this.shapeContainer2.Size = new System.Drawing.Size(35, 31);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // TipCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "TipCtrl";
            this.Size = new System.Drawing.Size(206, 31);
            this.SizeChanged += new System.EventHandler(this.TipCtrl_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
#pragma warning disable CS0234 // 命名空间“Microsoft.VisualBasic”中不存在类型或命名空间名“PowerPacks”(是否缺少程序集引用?)
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape7;
#pragma warning restore CS0234 // 命名空间“Microsoft.VisualBasic”中不存在类型或命名空间名“PowerPacks”(是否缺少程序集引用?)
        private System.Windows.Forms.Panel panel1;
#pragma warning disable CS0234 // 命名空间“Microsoft.VisualBasic”中不存在类型或命名空间名“PowerPacks”(是否缺少程序集引用?)
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
#pragma warning restore CS0234 // 命名空间“Microsoft.VisualBasic”中不存在类型或命名空间名“PowerPacks”(是否缺少程序集引用?)
    }
}

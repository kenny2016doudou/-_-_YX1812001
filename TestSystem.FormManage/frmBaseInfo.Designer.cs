﻿namespace TestSystem.FormManage
{
    partial class frmBaseInfo
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
            this.uC_TypeSet1 = new SDG_SystemSet.UC_TypeSet();
            this.SuspendLayout();
            // 
            // uC_TypeSet1
            // 
            this.uC_TypeSet1.Location = new System.Drawing.Point(12, 12);
            this.uC_TypeSet1.Name = "uC_TypeSet1";
            this.uC_TypeSet1.Size = new System.Drawing.Size(557, 434);
            this.uC_TypeSet1.TabIndex = 0;
            this.uC_TypeSet1.Tag = "类型设置";
            // 
            // frmBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 456);
            this.Controls.Add(this.uC_TypeSet1);
            this.Name = "frmBaseInfo";
            this.Text = "基本信息设置";
            this.ResumeLayout(false);

        }

        #endregion

        private SDG_SystemSet.UC_TypeSet uC_TypeSet1;
    }
}
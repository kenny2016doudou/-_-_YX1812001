using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSystem.FormManage
{
    public partial class frmSystemSet : CSharpWin.SkinForm
    {
        public frmSystemSet()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 动态加载数据查询用户控件
        /// </summary>
        private void LoadUC()
        {
            UserControl[] ucs = UserControlManager.GetUCList(ControlType.参数设置);
            //this.Size.Height = 11;//Convert.ToInt32(Math.Round((ucs[0].Size.Height * 1.0269),0));
            //this.Size.Width =Convert.ToInt32( Math.Round((ucs[0].Size.Width * 1.1455),0));

            for (int i = 0; i < ucs.Length; i++)
            {
                this.Size = new Size(Convert.ToInt32(Math.Round(ucs[i].Size.Width * 1.0078, 0)), Convert.ToInt32(Math.Round(ucs[i].Size.Height * 1.03643, 0)));
                this.Controls.Add(ucs[i]);
            }
            if (ucs[0].Tag == null)
            {
                MessageBox.Show("未设置Tag");
            }
            else
            {
                //this.Text = ucs[0].Tag.ToString();
                ucs[0].Dock = DockStyle.Fill;
                this.CenterToScreen();
            }
        }

        private void frmSystemSet_Load(object sender, EventArgs e)
        {
            LoadUC();
        }


    }
}

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
    public partial class frmUserSet : CSharpWin.SkinForm
    {
        public frmUserSet()
        {
            InitializeComponent();
        }

        private void frmUserSet_Load(object sender, EventArgs e)
        {
            if (!UserControlManager.IsDefautSystemSet())
            {
                //调用其他其他查询用户控件
                LoadUC();
            }
            else
            {

                UserControl uc_userset = new UC_UserForm();
                this.Size = new Size(Convert.ToInt32(Math.Round(uc_userset.Size.Width * 1.0078, 0)), Convert.ToInt32(Math.Round(uc_userset.Size.Height * 1.03643, 0)));
                uc_userset.Dock = DockStyle.Fill;
                this.Controls.Add(uc_userset);
                this.CenterToScreen();

            }
        }

        /// <summary>
        /// 动态加载用户设置控件
        /// </summary>
        private void LoadUC()
        {
            UserControl[] ucs = UserControlManager.GetUCList(ControlType.用户管理);
            //this.Size.Height = 11;//Convert.ToInt32(Math.Round((ucs[0].Size.Height * 1.0269),0));
            //this.Size.Width =Convert.ToInt32( Math.Round((ucs[0].Size.Width * 1.1455),0));

            for (int i = 0; i < ucs.Length; i++)
            {
                this.Size = new Size(Convert.ToInt32(Math.Round(ucs[i].Size.Width * 1.0078, 0)), Convert.ToInt32(Math.Round(ucs[i].Size.Height * 1.03643, 0)));
                ucs[i].Dock = DockStyle.Fill;
                this.Controls.Add(ucs[i]);
            }
            this.Text = ucs[0].Tag.ToString();

            this.CenterToScreen();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSystem.FormManage
{
    public partial class frmData : CSharpWin.SkinForm
    {
        public frmData()
        {
            InitializeComponent();
        }

        private void frmData_Load(object sender, EventArgs e)
        {
       


            if (!UserControlManager.IsDefautDataQuery())
            {
                //调用其他其他查询用户控件
                LoadUC();

            }
            else
            {

                UserControl uc_dataquery = new UC_DataForm();
                this.Size = new Size(Convert.ToInt32(Math.Round(uc_dataquery.Size.Width * 1.0078, 0)), Convert.ToInt32(Math.Round(uc_dataquery.Size.Height * 1.03643, 0)));
                uc_dataquery.Dock = DockStyle.Fill;
                this.Controls.Add(uc_dataquery);
                this.CenterToScreen();

            }
        }

        /// <summary>
        /// 动态加载数据查询用户控件
        /// </summary>
        private void LoadUC()
        {
            UserControl[] ucs = UserControlManager.GetUCList(ControlType.数据查询);
            //this.Size.Height = 11;//Convert.ToInt32(Math.Round((ucs[0].Size.Height * 1.0269),0));
            //this.Size.Width =Convert.ToInt32( Math.Round((ucs[0].Size.Width * 1.1455),0));

            for (int i = 0; i < ucs.Length; i++)
            {
                this.Size = new Size(Convert.ToInt32(Math.Round(ucs[i].Size.Width * 1.0078, 0)), Convert.ToInt32(Math.Round(ucs[i].Size.Height * 1.03643, 0)));
                this.Controls.Add(ucs[i]);
                ucs[i].Dock = DockStyle.Fill;
            }
            this.CenterToScreen();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.DataAccess.Interface.UserModule;
using TestSystem.DataAccess;
using TestSystem.Common.Constant;
using TestSystem.Model.Entity.UserModule;
using System.Diagnostics;
using Soar.Framework;
using ZKZDLQ.SystemTest;

namespace TestSystem.FormManage
{
    public partial class frmLogin : CSharpWin.SkinForm
    {
        ITS_UserDAO userDao = BLL_Reference<ITS_UserDAO>.CreateBaseObj("TS_User");
        MDIMainTest tempparent;
        public frmLogin()
        {
            InitializeComponent();
        }

        public frmLogin(MDIMainTest parentform)
        {
            InitializeComponent();
            //this.Text = parentform.Text;
            this.labTitle.Text = parentform.Text;
            tempparent = parentform;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码！");
            }
            else
            {
                TS_User user = userDao.SelectUser("", cmbUserName.Text.Trim(), "", "");
                if (user.Password == txtPwd.Text.Trim())
                {
                    UserRights.UserName = this.cmbUserName.Text;
                    if (user.QX.Contains("管理"))
                        UserRights.Manager = true;
                    else
                        UserRights.Manager = false;
                    tempparent.LoginOK();
                    
                    Logger.GetInstance().AddLogItem(LogLevel.Info, UserRights.UserName + "登录系统!", DateTime.Now); 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误，请重新登录！");
                    this.txtPwd.Text = "";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.FindForm().Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            DataSet dataSet = userDao.SelectUser("", "", "", "", "", "");
            this.cmbUserName.DataSource = dataSet.Tables["TS_User"];
            this.cmbUserName.DisplayMember = "User_Name";
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Parent.FindForm().Close();
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            TS_User user = userDao.SelectUser("", cmbUserName.Text.Trim(), "", "");
            if (user.Password == txtPwd.Text.Trim())
            {
                UserRights.UserName = this.cmbUserName.Text;
                if (user.QX.Contains("管理"))
                    UserRights.Manager = true;
                else
                    UserRights.Manager = false;
                tempparent.LoginOK();
                 
                Logger.GetInstance().AddLogItem(LogLevel.Info, UserRights.UserName + "登录系统!", DateTime.Now);
                this.Close();
            }
            else
            {
                //MessageBox.Show("密码错误，请重新登录！");
                //this.txtPwd.Text = "";
            }
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void getkeyword()
        {
            //Process.Start(Application.StartupPath+"\\Keyboard\\OSK.exe");
            //bool ifget = false;
            //Process[] tps = Process.GetProcesses();
            //for(int i=0;i< tps.Length;i++)
            //{
            //    if (tps[i].ProcessName.CompareTo("OSK") == 0)
            //    { 
            //        ifget = true;
            //        break;
            //    }
            //}
            //if(!ifget)
            //{
            //    //Process.Start(Application.StartupPath + "\\Keyboard\\OSK.exe");
            //} 
        }

        private void cmbUserName_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process[] tps = Process.GetProcesses();
            for (int i = 0; i < tps.Length; i++)
            {
                if (tps[i].ProcessName.CompareTo("OSK") == 0)
                {
                    tps[i].Close();
                    break;
                }
            }
            Process.Start(Application.StartupPath + "\\Keyboard\\OSK.exe");
        }
    }
}

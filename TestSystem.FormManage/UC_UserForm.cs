using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.DataAccess.Interface.UserModule;
using TestSystem.DataAccess;
using TestSystem.Model.Entity.UserModule;
using System.Diagnostics;

namespace TestSystem.FormManage
{
    public partial class UC_UserForm : UserControl
    {
        ITS_UserDAO userDao = BLL_Reference<ITS_UserDAO>.CreateBaseObj("TS_User");
        string _User_ID = "";
        public UC_UserForm()
        {
            InitializeComponent();
        }

        #region 公用方法
        /// <summary>
        /// 输入框验证
        /// </summary>
        /// <returns></returns>
        private string VilidateInput()
        {
            bool flag = true;
            string tip = "";
            if (txtUser_Name.Text.Trim() == "")
            {
                tip += "用户名";

                flag = false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                tip += " 密码";
                flag = false;
            }
            if (cmbQX.Text.Trim() == "")
            {
                tip += " 权限";
                flag = false;
            }

            if (txtUser_Tel.Text.Trim() == "")
            {
                tip += " 联系方式";
                flag = false;
            }

            if (!flag)
            {
                tip += "不能为空！";
            }
            return tip;


        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            DataSet ds = userDao.SelectUser("", "", "", "", "", "");
            dataGridView1.DataSource = ds.Tables["TS_User"].DefaultView;

        }

        /// <summary>
        /// 清除输入框中的值
        /// </summary>
        private void ClearInputData()
        {
            txtUser_Name.Text = "";
            txtPassword.Text = "";
            txtUser_Tel.Text = "";
            cmbQX.Text = "";
            _User_ID = "";

        } 
        #endregion

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadData();
            ClearInputData();
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            string tip = VilidateInput();
            if (tip == "")
            {
                
                TS_User user = new TS_User();
                
                user.User_Name = txtUser_Name.Text.Trim();
                user.Password = txtPassword.Text.Trim();
                user.QX = cmbQX.Text.Trim();
                user.User_Tel = txtUser_Tel.Text.Trim();
                user.Create_Date = DateTime.Now;
                user.Deleted = 0;
                if (_User_ID == "") //新增
                {
                    user.User_ID = Guid.NewGuid().ToString();
                    bool flagSave = userDao.SaveData(user);
                    if (!flagSave)
                    {
                        MessageBox.Show("已存在该用户！请重新输入用户信息！");
                    }
                }
                else //修改
                {
                    user.User_ID = _User_ID;
                    userDao.ModifyData(user);
                }

                LoadData();
                ClearInputData();
            }
            else
            {
                MessageBox.Show(tip);
            }
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1.CurrentRow.Cells["User_ID"].Value != null)
            {                
                txtUser_Name.Text = dataGridView1.CurrentRow.Cells["User_Name"].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                cmbQX.Text = dataGridView1.CurrentRow.Cells["QX"].Value.ToString();
                txtUser_Tel.Text = dataGridView1.CurrentRow.Cells["User_Tel"].Value.ToString();
                _User_ID = dataGridView1.CurrentRow.Cells["User_ID"].Value.ToString();
                btnSave.Text = "编辑";
                txtUser_Name.Enabled = false;                
            }            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputData();
            btnSave.Text = "新增";
            txtUser_Name.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_User_ID != "")
            {
                if (MessageBox.Show("您确定要删除该用户信息吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    TS_User User = userDao.SelectUser(_User_ID, "", "", "");
                    if (User.User_Name != "admin")
                    {
                        userDao.DeleteDataById(_User_ID);
                        LoadData();
                        ClearInputData();
                    }
                    else
                    {
                        MessageBox.Show("该admin用户为系统用户，不能删除！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的用户信息！");
            }
        }
        private void getkeyword()
        {
            Process.Start(Application.StartupPath + "\\Keyboard\\OSK.exe");
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
        private void btnQuery_Click(object sender, EventArgs e)
        {
            getkeyword();
            string _user_Name = "";
            string _user_Tel = "";
            string _qx = "";
            
            if (txtQUser_Name.Text.Trim() != "")
            {
                _user_Name = txtQUser_Name.Text.Trim();
            }

            if (txtQUser_Tel.Text.Trim() != "")
            {
                _user_Tel = txtQUser_Tel.Text.Trim();
            }

            if (cmbQQX.Text.Trim() != "")
            {
                _qx = cmbQQX.Text.Trim();
            }

            DataSet ds = userDao.SelectUserForQuery(_user_Name, _qx, _user_Tel, "", "");
           dataGridView1.DataSource = ds.Tables["TS_User"].DefaultView;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
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

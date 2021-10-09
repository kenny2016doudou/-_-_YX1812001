using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.DataAccess.Interface.SchemeModule;
using TestSystem.DataAccess;
using TestSystem.Model.Entity.SchemeModule;
using TestSystem.Common.Constant;
using System.Collections;
using Soar.Framework;
using System.Diagnostics;
using ZKZDLQ.SystemTest;

namespace TestSystem.FormManage
{
    public partial class MDIMainTest : Form//CSharpWin.SkinForm
    {
        ISchemeInfo bll_SchemeInfo = BLL_Reference<ISchemeInfo>.CreateObj("SchemeInfo");

        public MDIMainTest()
        {
            InitializeComponent();
            ToolStripManager.Renderer = new CSharpWin.ProfessionalToolStripRendererEx();
            SchemeInfo _SchemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            string schemeName = _SchemeInfo.Scheme_Name;
            this.Text = schemeName;

        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MDIMainTest_Load(object sender, EventArgs e)
        {
            Logger.GetInstance().Run();
            frmLogin frm = new frmLogin(this);
            frm.MdiParent = this;
            frm.Show();
            frm.Location = new Point(Convert.ToInt32((this.Size.Width - frm.Size.Width) / 2), 200);


            tsmi_开始测试.Visible = UserControlManager.IsHaveMenuBy(ControlType.测试);
            tsmi_历史数据查询.Visible = UserControlManager.IsHaveMenuBy(ControlType.数据查询);
            tsmi_系统参数设置.Visible = UserControlManager.IsHaveMenuBy(ControlType.参数设置);
            开始测试UToolStripMenuItem.Visible = UserControlManager.IsHaveMenuBy(ControlType.测试);
            系统参数设置OToolStripMenuItem.Visible = UserControlManager.IsHaveMenuBy(ControlType.参数设置);
            历史数据查询OToolStripMenuItem.Visible = UserControlManager.IsHaveMenuBy(ControlType.数据查询);
            用户设置ToolStripMenuItem.Visible = UserControlManager.IsHaveMenuBy(ControlType.用户管理);
        }

        /// <summary>
        /// 登陆成功共 将功能设为可用
        /// </summary>
        public void LoginOK()
        {
            tsmi_开始测试.Enabled = true;
            tsmi_历史数据查询.Enabled = true;
            tsmi_系统参数设置.Enabled = true;
            开始测试UToolStripMenuItem.Enabled = true;
            系统参数设置OToolStripMenuItem.Enabled = true;
            历史数据查询OToolStripMenuItem.Enabled = true;
            用户设置ToolStripMenuItem.Enabled = true;
        }


        private void 开始测试UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveMdiChildrenBy("测试设备选型") && !HaveMdiChildrenBy("受电弓测试系统"))
            {
                frmMainTest fmt = new frmMainTest();
                fmt.MdiParent = this;
                fmt.Show();

                //fmt.Location = new Point(Convert.ToInt32((this.Size.Width - fmt.Size.Width) / 2), 0);
            }
        }

        private void 系统参数设置OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UserRights.Manager)
            {
                MessageBox.Show("当前登录用户不具备管理权限,请与管理员联系!");
                return;
            }
            if (!HaveMdiChildrenBy("系统参数设置") && !HaveMdiChildrenBy("受电弓测试系统"))
            {
                frmSystemSet fss = new frmSystemSet();
                fss.MdiParent = this;

                fss.Show();
                //fss.Location = new Point(Convert.ToInt32((this.Size.Width - fss.Size.Width) / 2), 0);
            }
        }

        private void 历史数据查询OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveMdiChildrenBy("历史数据查询") && !HaveMdiChildrenBy("受电弓测试系统"))
            {
                frmData fd = new frmData();
                fd.MdiParent = this;

                fd.Show();
                //fd.Location = new Point(Convert.ToInt32((this.Size.Width - fd.Size.Width) / 2), 0);
            }
        }

        private void MDIMainTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                if (this.MdiChildren[0].Text != "系统登录")
                {
                    MessageBox.Show("您还有程序正在运行!请关闭后退出本系统!");
                    e.Cancel = true;
                }
                else
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

        }

        private void 用户设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UserRights.Manager)
            {
                MessageBox.Show("当前登录用户不具备管理权限,请与管理员联系!");
                return;
            }
            if (!HaveMdiChildrenBy("用户设置") && !HaveMdiChildrenBy("受电弓测试系统"))
            {
                frmUserSet fu = new frmUserSet();
                fu.MdiParent = this;

                fu.Show();
                //fu.Location = new Point(Convert.ToInt32((this.Size.Width - fu.Size.Width) / 2), 0);
            }
        }


        bool HaveMdiChildrenBy(string value)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Text == value)
                {
                    return true;
                }
            }
            return false;
        }

        private void 日志查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Logger.GetInstance().openFile();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\helps\\帮助文档.doc");
        }
    }
}

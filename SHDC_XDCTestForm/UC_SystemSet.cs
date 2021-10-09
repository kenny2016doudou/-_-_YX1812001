using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.BusinessRule.Interface.SchemeModule;
using TestSystem.BusinessRule.SchemeModule;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PubClass;
using Soar.Framework;

namespace SHDC_XDCTestForm
{
    public partial class UC_SystemSet : UserControl
    {
        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        ISchemeRowBO schemeRowBO = new TestSystem.BusinessRule.SchemeModule.SchemeRowBO();
        XDCManager xdc = null;
        int int_status = 0;//0默认状态，1新增状态，2修改状态
        public UC_SystemSet()
        {
            InitializeComponent();
            xdc = XDCManager.getInstance();
            LoadData();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        void LoadData()
        {

            //DataSet ds = schemeRowBO.SelectData("Config");
            //if (cmb_xdcxh.DataSource != null)
            //    cmb_xdcxh.DataSource = null;
            //cmb_xdcxh.DataSource = ds.Tables[0].DefaultView;
            //cmb_xdcxh.ValueMember = "Col_Code";
            //cmb_xdcxh.DisplayMember = "Col_Name";



            //#region 查表中所有信息，显示某一列数据

            //DataTable ds = schemeRowBO.SelectData("Config");
            //cmb_xdcxh.DataSource = ds;
            //cmb_xdcxh.DisplayMember = "蓄电池型号";
            //cmb_xdcxh.ValueMember = "蓄电池型号";
            //#endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }



        /// <summary>
        /// 添加配置信息
        /// </summary>
        public void Add()
        {
            if (string.IsNullOrEmpty(txt_cdsj1.Text))
            {
                MessageBox.Show("充电时间设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_cddl1.Text))
            {
                MessageBox.Show("充电电流设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fdsj1.Text))
            {
                MessageBox.Show("放电时间设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fddl1.Text))
            {
                MessageBox.Show("充电电流设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_qsdy1.Text))
            {
                MessageBox.Show("起始电压设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_cdzzdy1.Text))
            {
                MessageBox.Show("充电终止电压设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fdzzdy1.Text))
            {
                MessageBox.Show("放电终止电压设置不能为空!");
                return;
            }

            string[] argColName = new string[11];
            string[] argColContent = new string[11];
            argColName[0] = "蓄电池型号";
            argColContent[0] = comboBox1.Text.Trim() + "电池组";

            argColName[1] = "充电时间设置";
            argColContent[1] = txt_cdsj1.Text;


            argColName[2] = "充电电流设置";
            argColContent[2] = txt_cddl1.Text;

            argColName[3] = "放电时间设置";
            argColContent[3] = txt_fdsj1.Text;

            argColName[4] = "放电电流设置";
            argColContent[4] = txt_fddl1.Text;

            argColName[5] = "起始电压设置";
            argColContent[5] = txt_qsdy1.Text;

            argColName[6] = "充电终止电压设置";
            argColContent[6] = txt_cdzzdy1.Text;

            argColName[7] = "放电终止电压设置";
            argColContent[7] = txt_fdzzdy1.Text;

            argColName[8] = "过载电流设置";
            argColContent[8] = "0";

            argColName[9] = "充电模式设置";
            argColContent[9] = "";

            argColName[10] = "循环次数设置";
            argColContent[10] = textBox19.Text;

            ISchemeRowBO _SchemeRowBO = new SchemeRowBO();
            _SchemeRowBO.SaveData("Config", argColName, argColContent, false);

        }


        /// <summary>
        /// 让textbox只能输入数字和浮点数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;

            //if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == '\b')
            //{
            //    if (e.KeyChar == '.')
            //    {
            //        foreach (Control c in this.Controls[0].Controls)
            //        {
            //            if (c.Focused)
            //            {
            //                if (e.KeyChar == '.')
            //                {
            //                    if (c.Text.IndexOf('.') < 0)
            //                    {
            //                        e.Handled = false;
            //                    }
            //                    else
            //                    {
            //                        e.Handled = true;
            //                    }
            //                }
            //            }

            //        }
            //    }
            //    else
            //    {
            //        e.Handled = false;
            //    }

            //}
        }


        /// <summary>
        /// 修改配置信息
        /// </summary>
        public void Mod()
        {
            if (string.IsNullOrEmpty(txt_cdsj1.Text))
            {
                MessageBox.Show("充电时间设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_cddl1.Text))
            {
                MessageBox.Show("充电电流设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fdsj1.Text))
            {
                MessageBox.Show("放电时间设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fddl1.Text))
            {
                MessageBox.Show("充电电流设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_qsdy1.Text))
            {
                MessageBox.Show("起始电压设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_cdzzdy1.Text))
            {
                MessageBox.Show("充电终止电压设置不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(txt_fdzzdy1.Text))
            {
                MessageBox.Show("放电终止电压设置不能为空!");
                return;
            }

            string[] argColName = new string[11];
            string[] argColContent = new string[11];
            argColName[0] = "蓄电池型号";
            argColContent[0] = comboBox1.Text.Trim() + "电池组";

            argColName[1] = "充电时间设置";
            argColContent[1] = txt_cdsj1.Text;


            argColName[2] = "充电电流设置";
            argColContent[2] = txt_cddl1.Text;


            argColName[3] = "放电时间设置";
            argColContent[3] = txt_fdsj1.Text;

            argColName[4] = "放电电流设置";
            argColContent[4] = txt_fddl1.Text;


            argColName[5] = "起始电压设置";
            argColContent[5] = txt_qsdy1.Text;

            argColName[6] = "充电终止电压设置";
            argColContent[6] = txt_cdzzdy1.Text;

            argColName[7] = "放电终止电压设置";
            argColContent[7] = txt_fdzzdy1.Text;

            argColName[8] = "过载电流设置";
            argColContent[8] = "0";

            argColName[9] = "充电模式设置";
            argColContent[9] = "";

            argColName[10] = "循环次数设置";
            argColContent[10] = textBox19.Text;

            ISchemeRowBO _SchemeRowBO = new SchemeRowBO();
            if (!_SchemeRowBO.ModifyData("Config", "蓄电池型号", argColName, argColContent))
            {
                MessageBox.Show("电池组参数保存错误!");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            schemeRowBO = new SchemeRowBO();
            DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", comboBox1.Text.Trim() + "电池组", "", "");
            if (ds2.Rows.Count > 0)
            {
                try
                {

                    Mod();
                    int_status = 0;
                    schemeRowBO = new SchemeRowBO();
                    DataTable dt = schemeRowBO.SelectData("Config", "蓄电池型号", comboBox1.Text.Trim() + "电池组", "", "");
                    xdc.P1_充电电流设置值 = dt.Rows[0]["充电电流设置"].ToString();
                    xdc.P1_充电时间设置值 = dt.Rows[0]["充电时间设置"].ToString();
                    xdc.P1_充电终止电压设置值 = dt.Rows[0]["充电终止电压设置"].ToString();
                    xdc.P1_放电电流设置值 = dt.Rows[0]["放电电流设置"].ToString();
                    xdc.P1_放电时间设置值 = dt.Rows[0]["放电时间设置"].ToString();
                    xdc.P1_放电终止电压设置值 = dt.Rows[0]["放电终止电压设置"].ToString();
                    xdc.P1_循环次数设置值 = dt.Rows[0]["循环次数设置"].ToString();
                    xdc.P1_起始电压设置值 = dt.Rows[0]["起始电压设置"].ToString();

                    MessageBox.Show("数据保存成功!");

                    Logger.GetInstance().AddLogItem(LogLevel.Info, UserRights.UserName + "修改充放电参数!" + comboBox1.Text.Trim(), DateTime.Now);
                }
                catch
                {
                    MessageBox.Show("数据保存失败!");
                }
            }
            else
            {
                try
                {
                    string remString = comboBox1.Text.Trim();
                    Add();
                    int_status = 0; int_status = 0;
                    comboBox1.Items.Clear();
                    schemeRowBO = new SchemeRowBO();
                    ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", "", "", "");
                    for (int i = 0; i < ds2.Rows.Count; i++)
                    {
                        if (ds2.Rows[i]["蓄电池型号"].ToString().Contains("电池组"))
                        {
                            if (!comboBox1.Items.Contains(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", "")))
                            {
                                comboBox1.Items.Add(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", ""));
                            }
                        }
                    }
                    comboBox1.Text = remString;
                    MessageBox.Show("数据保存成功!");
                    Logger.GetInstance().AddLogItem(LogLevel.Info, UserRights.UserName + "修改充放电参数!" + comboBox1.Text.Trim(), DateTime.Now);
                }
                catch
                {
                    MessageBox.Show("数据保存失败!");
                }
            }

        }

        private string temp_cmbtext = "";


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //string tempstr = xdc.P1_充电电流设置值;
                //if (tempstr != null && tempstr.Length > 0)
                //    txt_cddl2.Text = (float.Parse(tempstr) / 10).ToString("0.0");


                //txt_cdsj2.Text = xdc.P1_充电时间设置值;
                //txt_fdsj2.Text = xdc.P1_放电时间设置值;

                //tempstr = xdc.P1_充电终止电压设置值;
                //if (tempstr != null && tempstr.Length > 0)
                //    txt_cdzzdy2.Text = (float.Parse(tempstr) / 10).ToString("0.0");

                //tempstr = xdc.P1_放电电流设置值;
                //if (tempstr != null && tempstr.Length > 0)
                //    txt_fddl2.Text = (float.Parse(tempstr) / 10).ToString("0.0");

                //tempstr = xdc.P1_放电终止电压设置值;
                //if (tempstr != null && tempstr.Length > 0)
                //    txt_fdzzdy2.Text = (float.Parse(tempstr) / 10).ToString("0.0");

                //tempstr = xdc.P1_起始电压设置值;
                //if (tempstr != null && tempstr.Length > 0)
                //    txt_qsdy2.Text = (float.Parse(tempstr) / 10).ToString("0.0");


                ////textBox18.Text = xdc.Pdj_循环次数设置值;

                string tempstr = xdc.P1_充电电流设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_cddl2.Text = (float.Parse(tempstr)).ToString("0.0");

                tempstr = xdc.P1_充电时间设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_cdsj2.Text = tempstr;

                tempstr = xdc.P1_充电终止电压设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_cdzzdy2.Text = (float.Parse(tempstr)).ToString("0.0");

                tempstr = xdc.P1_放电电流设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_fddl2.Text = (float.Parse(tempstr)).ToString("0.0");

                tempstr = xdc.P1_放电时间设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_fdsj2.Text = tempstr;

                tempstr = xdc.P1_放电终止电压设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_fdzzdy2.Text = (float.Parse(tempstr)).ToString("0.0");

                tempstr = xdc.P1_起始电压设置值;
                if (tempstr != null && tempstr.Length > 0)
                    txt_qsdy2.Text = (float.Parse(tempstr)).ToString("0.0");

                tempstr = xdc.P1_循环次数设置值;
                if (tempstr != null && tempstr.Length > 0)
                    textBox18.Text = (float.Parse(tempstr)).ToString("0.0");

            }
            catch
            {
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ChangeColor(txt_cddl2);
            ChangeColor(txt_cdsj2);
            ChangeColor(txt_cdzzdy2);
            ChangeColor(txt_fddl2);
            ChangeColor(txt_fdsj2);
            ChangeColor(txt_fdzzdy2);
            ChangeColor(txt_qsdy2);
        }

        private void ChangeColor(TextBox tx)
        {
            if (tx.ForeColor == Color.Black)
            {
                tx.ForeColor = Color.White;
            }
            else if (txt_qsdy2.ForeColor == Color.White)
            {
                tx.ForeColor = Color.Black;
            }

        }


        void txtPwd_LostFocus(object sender, EventArgs e)
        {
            LoadKeyBoardClass.unLoad();
        }

        void txtPwd_GotFocus(object sender, EventArgs e)
        {
            LoadKeyBoardClass.toLoad();
        }

        private void UC_SystemSet_Load(object sender, EventArgs e)
        {
            txt_cddl1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_cddl1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_cdsj1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_cdsj1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_qsdy1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_qsdy1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_cdzzdy1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_cdzzdy1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_fddl1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_fddl1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_fdsj1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_fdsj1.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_fdzzdy1.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_fdzzdy1.LostFocus += new EventHandler(txtPwd_LostFocus);


            DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", "", "", "");
            for (int i = 0; i < ds2.Rows.Count; i++)
            {
                if (ds2.Rows[i]["蓄电池型号"].ToString().Contains("电池组"))
                {
                    if (!comboBox1.Items.Contains(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", "")))
                    {
                        comboBox1.Items.Add(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", ""));
                    }
                }
            }
        }

        private void 键盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                LoadKeyBoardClass.unLoad();
            }
            LoadKeyBoardClass.toLoad();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", comboBox1.Text.Trim() + "电池组", "", "");
            try
            {
                this.txt_cddl1.Text = ds2.Rows[0]["充电电流设置"].ToString();
                this.txt_cdsj1.Text = ds2.Rows[0]["充电时间设置"].ToString();
                this.txt_fdsj1.Text = ds2.Rows[0]["放电时间设置"].ToString();
                this.txt_fddl1.Text = ds2.Rows[0]["放电电流设置"].ToString();

                this.txt_qsdy1.Text = ds2.Rows[0]["起始电压设置"].ToString();
                this.txt_cdzzdy1.Text = ds2.Rows[0]["充电终止电压设置"].ToString();
                this.txt_fdzzdy1.Text = ds2.Rows[0]["放电终止电压设置"].ToString();

                this.textBox19.Text = ds2.Rows[0]["循环次数设置"].ToString();
            }
            catch
            {
            }

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] argColName = new string[1];
                string[] argColContent = new string[1];
                argColName[0] = "蓄电池型号";
                argColContent[0] = comboBox1.Text.Trim() + "电池组";

                ISchemeRowBO _SchemeRowBO = new SchemeRowBO();
                if (!_SchemeRowBO.DeleteData("Config", argColName, argColContent))
                {

                }

                comboBox1.Items.Clear();
                schemeRowBO = new SchemeRowBO();
                DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", "", "", "");
                for (int i = 0; i < ds2.Rows.Count; i++)
                {
                    if (ds2.Rows[i]["蓄电池型号"].ToString().Contains("电池组"))
                    {
                        if (!comboBox1.Items.Contains(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", "")))
                        {
                            comboBox1.Items.Add(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", ""));
                        }
                    }

                }
                comboBox1.Text = "";
                Logger.GetInstance().AddLogItem(LogLevel.Info, UserRights.UserName + "删除充放电参数!" + comboBox1.Text.Trim(), DateTime.Now);
            }
            catch
            {
                MessageBox.Show("删除数据错误!");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

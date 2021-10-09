using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using SchemeManage.BusinessRule.Interface.SchemeModule;
using SchemeManage.BusinessRule.SchemeModule;
using TestSystem.Command.ControlCenter;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PubClass;
using System.Threading;
using frequentlyCtrlClass;
using Soar.Framework;

namespace SHDC_XDCTestForm
{
    public partial class UC_MainTest : UserControl
    {
        private Logger runLogger = Logger.GetInstance();
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
        public UC_MainTest()
        {
            InitializeComponent();
            xdc = XDCManager.getInstance();

        }
        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        ISchemeRowBO schemeRowBO = new SchemeRowBO();
        XDCManager xdc = null;
        private int remIndexValue = -1, SelectTestIndex = 0;

        #region 警告报警
        private void Warning(Label lab, bool b)
        {
            if (b)
            {
                lab.BackColor = Color.Red;
            }
            else
            {
                lab.BackColor = Color.Lime;
            }
        }
        #endregion

        #region 确认、充电、放电、停止操作

        private void btn_1_qrxh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_1_chexing.Text) || string.IsNullOrEmpty(txt_1_chehao.Text) || string.IsNullOrEmpty(txt_1_chuchangbianhao.Text))
            {
                MessageBox.Show("请填写完整的工况后进行试验！");
                return;
            }
            xdc.Set1确认操作();
            btn_1_chongdian.Enabled = true;
            btn_1_fangdian.Enabled = true;
        }

        private void btn_1_chongdian_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                xdc.Com_Siemens.ExcuteCommand_Pulse("自动循环启动");
                chart1.Series["充电电压曲线"].Points.Clear();
                chart1.Series["充电电流曲线"].Points.Clear(); 
                timer_chart1.Enabled = true;
                runLogger.AddLogItem(LogLevel.Info, UserRights.UserName + "启动循环充放电!", DateTime.Now);
            }
            else
            {
                if (!ifsetpara)
                {
                    MessageBox.Show("请选择蓄电池型号进行参数设置,然后进行充电操作!");
                    return;
                }
                if (SelectTestIndex == 2)//整组
                {
                    if (MessageBox.Show("确定对整组蓄电池进行充电操作吗?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        chart1.Series["充电电压曲线"].Points.Clear();
                        chart1.Series["充电电流曲线"].Points.Clear();
                        xdc.Set1充电操作();
                        timer_chart1.Enabled = true;
                        runLogger.AddLogItem(LogLevel.Info, UserRights.UserName + "启动充电!", DateTime.Now);
                    }
                }
            }
            
        }

        private void btn_1_tingzhi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定停止操作吗?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                xdc.Com_Siemens.ExcuteCommand_Pulse("自动循环停止");
                xdc.Set1停止操作();
                timer_chart1.Enabled = false;
                runLogger.AddLogItem(LogLevel.Info, UserRights.UserName + "停止充放电!", DateTime.Now);
            }
        }

        private void btn_1_fangdian_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                xdc.Com_Siemens.ExcuteCommand_Pulse("自动循环启动");
                chart1.Series["放电电压曲线"].Points.Clear();
                chart1.Series["放电电流曲线"].Points.Clear();
                timer_chart1.Enabled = true;
                runLogger.AddLogItem(LogLevel.Info, UserRights.UserName + "启动循环充放电!", DateTime.Now);
            }
            else
            {
                if (!ifsetpara)
                {
                    MessageBox.Show("请选择蓄电池型号进行参数设置,然后进行放电操作!");
                    return;
                }
                if (SelectTestIndex == 2)//整组
                {
                    if (MessageBox.Show("确定对整组蓄电池进行放电操作吗?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        chart1.Series["放电电压曲线"].Points.Clear();
                        chart1.Series["放电电流曲线"].Points.Clear();
                        xdc.Set1放电操作();
                        timer_chart1.Enabled = true;
                        runLogger.AddLogItem(LogLevel.Info, UserRights.UserName + "启动放电!", DateTime.Now);
                    }
                }
            }
           
        }
        #endregion

        #region 显示警告以及运行状态

        private int ljtimes = 0;
        /// <summary>
        /// 1通道数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_1_Tick(object sender, EventArgs e)
        {
            try
            {
                Display_1_Yxzt();
                foreach (TipCtrl tttc in flowLayoutPanel1.Controls)
                {
                    tttc.ActiveTip(xdc.getWarnningState(tttc.tipDesc));
                    if (tttc.ActiveValue())
                    {
                        if(ljtimes>5)
                        Logger.GetInstance().AddLogItem(LogLevel.Warn, tttc.tipDesc + "!", DateTime.Now);
                    }
                }
                if (ljtimes > 5)
                    ljtimes = 0;
                ljtimes++;
                SelectTestIndex = 2;
                remIndexValue = 2;

                if (remIndexValue == 2)
                {
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
                        textBox2.Text = (float.Parse(tempstr)).ToString("0.0");

                    tempstr = xdc.P1_已循环次数;
                    if (tempstr != null && tempstr.Length > 0)
                        textBox1.Text = (float.Parse(tempstr)).ToString("0.0");

                    tempstr = xdc.P1_循环充电静置时间设置;
                    if (tempstr != null && tempstr.Length > 0)
                        textBox4.Text = (float.Parse(tempstr)).ToString("0.0");

                    tempstr = xdc.P1_循环充电已静置时间;
                    if (tempstr != null && tempstr.Length > 0)
                        textBox5.Text = (float.Parse(tempstr)).ToString("0.0");

                    tempstr = xdc.P1_循环放电已静置时间;
                    if (tempstr != null && tempstr.Length > 0)
                        textBox6.Text = (float.Parse(tempstr)).ToString("0.0");
                }
            }
            catch (Exception ee)
            {
            }
        }

        #endregion

        #region 显示运行状态

        /// <summary>
        /// 显示运行状态
        /// </summary>
        public void Display_1_Yxzt()
        {
            string tempstr = "";
            switch (xdc.P1_显示运行状态)
            {
                case "0":
                    lab_1_yxzt.Text = "待机";
                    btn_1_qrxh.Enabled = true;
                    break;
                case "1":
                    lab_1_yxzt.Text = "正在充电";
                    btn_1_qrxh.Enabled = false;
                    btn_1_tingzhi.Enabled = true;
                    txt_1_cddl.Text = xdc.P1_显示充电电流;
                    txt_1_fddl.Text = "0";
                    txt_1_fdass.Text = "0";
                    break;
                case "2":
                    lab_1_yxzt.Text = "正在放电";
                    btn_1_qrxh.Enabled = false;
                    btn_1_tingzhi.Enabled = true;
                    txt_1_cddl.Text = "0";
                    txt_1_fddl.Text = xdc.P1_显示放电电流;
                    tempstr = xdc.Com_Siemens.GetReader("放电安时").ToString();
                    if (tempstr != null && tempstr.Length > 0)
                        txt_1_fdass.Text = (float.Parse(tempstr)).ToString("0.0");
                    break;
                case "3":
                    lab_1_yxzt.Text = "故障";
                    btn_1_qrxh.Enabled = false;
                    btn_1_tingzhi.Enabled = true;
                    break;
                case "4":
                    lab_1_yxzt.Text = "充电完成";
                    btn_1_qrxh.Enabled = true;
                    btn_1_tingzhi.Enabled = true;
                    txt_1_cddl.Text = "0";
                    txt_1_fddl.Text = "0";
                    txt_1_fdass.Text = "0";
                    break;
                case "5":
                    lab_1_yxzt.Text = "放电完成";
                    btn_1_qrxh.Enabled = true;
                    btn_1_tingzhi.Enabled = true;
                    txt_1_cddl.Text = "0";
                    txt_1_fddl.Text = "0";
                    txt_1_fdass.Text = "0";
                    break;
                case "7":
                    lab_1_yxzt.Text = "停止";
                    btn_1_qrxh.Enabled = true;
                    btn_1_tingzhi.Enabled = true;
                    timer_chart1.Enabled = false;
                    txt_1_cddl.Text = "0";
                    txt_1_fddl.Text = "0";
                    txt_1_fdass.Text = "0";
                    break;
                case "8":
                    lab_1_yxzt.Text = "程序自动完成";
                    btn_1_qrxh.Enabled = true;
                    btn_1_tingzhi.Enabled = true;
                    timer_chart1.Enabled = false;
                    txt_1_cddl.Text = "0";
                    txt_1_fddl.Text = "0";
                    txt_1_fdass.Text = "0";
                    break;
            }
            if (xdc.Com_Siemens.GetReader("充电达到限压条件").ToString().Contains("1"))
                label19.Text = "恒压";
            else
                label19.Text = "恒流";
        }

        #endregion

        #region 画图

     
        double userTimer1 = 0;
        private bool ifcd = false;
        private bool iffd = false;
        private void timer_chart1_Tick(object sender, EventArgs e)
        {
            try
            {               
                if (lab_1_yxzt.Text.Contains("正在充电"))
                {
                    if (!ifcd)
                    {
                        chart1.Series["充电电压曲线"].Points.Clear();
                        chart1.Series["充电电流曲线"].Points.Clear();
                        ifcd = true;
                        iffd = false;
                    }
                    if (ifcd)
                    {
                        userTimer1 = double.Parse(xdc.Com_Siemens.GetReader("充电曲线计时").ToString());
                        if (userTimer1 > 0)
                        {
                            chart1.Series["充电电压曲线"].Points.AddXY(userTimer1, txt_1_dcdy.Text);
                            chart1.Series["充电电流曲线"].Points.AddXY(userTimer1, txt_1_cddl.Text);
                        }
                    }
                }
                if (lab_1_yxzt.Text.Contains("正在放电"))
                {
                    if (!iffd)
                    {
                        chart1.Series["放电电压曲线"].Points.Clear();
                        chart1.Series["放电电流曲线"].Points.Clear();
                        ifcd = false;
                        iffd = true;
                    }
                    if (iffd)
                    {
                        userTimer1 = double.Parse(xdc.Com_Siemens.GetReader("放电曲线计时").ToString());
                        if (userTimer1 > 0)
                        {
                            chart1.Series["放电电压曲线"].Points.AddXY(userTimer1, txt_1_dcdy.Text);
                            chart1.Series["放电电流曲线"].Points.AddXY(userTimer1, txt_1_fddl.Text);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region 数据保存
        private void Save1()
        {
            FileStream fsObj = null;
            BinaryReader binRdr = null;
            try
            {

                string[] argColNameBaseinfo = new string[6];
                object[] argColContentBaseinfo = new object[6];

                argColNameBaseinfo[0] = "车型";
                argColContentBaseinfo[0] = txt_1_chexing.Text.Trim();
                argColNameBaseinfo[1] = "车号";
                argColContentBaseinfo[1] = txt_1_chehao.Text.Trim();
                argColNameBaseinfo[2] = "设备编号";
                argColContentBaseinfo[2] = txt_1_chuchangbianhao.Text.Trim();
                argColNameBaseinfo[3] = "试验时间";
                argColContentBaseinfo[3] = dptime1.Value.ToString();

                argColNameBaseinfo[4] = "试验人员";
                argColContentBaseinfo[4] = UserRights.UserName;

                argColNameBaseinfo[5] = "其他";
                argColContentBaseinfo[5] = "";


                ISchemeRowBO _SchemeRowBO_BaseInfo = new SchemeRowBO();
                _SchemeRowBO_BaseInfo.SaveData("BaseInfo", argColNameBaseinfo, argColContentBaseinfo, false);

                DataTable dt = null;
                dt = schemeRowBO.SelectData("Config", "蓄电池型号", "", "", "");
                int remIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["蓄电池型号"].ToString().Contains("电池组"))
                    {
                        remIndex = i;
                        break;
                    }
                }

                string chartname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                chart1.SaveImage(Application.StartupPath + @"\picture\" + chartname, System.Drawing.Imaging.ImageFormat.Jpeg);
              

                string[] argColName = new string[10];
                object[] argColContent = new object[10];
                argColName[0] = "蓄电池型号";
                argColContent[0] = txt_1_chuchangbianhao.Text;
                argColName[1] = "充电时间";
                argColContent[1] = dt.Rows[remIndex]["充电时间设置"];
                argColName[2] = "充电电流";
                argColContent[2] = dt.Rows[remIndex]["充电电流设置"];
                argColName[3] = "放电时间";
                argColContent[3] = dt.Rows[remIndex]["放电时间设置"];

                argColName[4] = "放电电流";
                argColContent[4] = dt.Rows[remIndex]["放电电流设置"];

                //argColName[5] = "循环次数";
                //argColContent[5] = dt.Rows[remIndex]["循环次数设置"]; 

                argColName[5] = "起始电压";
                argColContent[5] = dt.Rows[remIndex]["起始电压设置"];

                argColName[6] = "充电终止电压";
                argColContent[6] = dt.Rows[remIndex]["充电终止电压设置"];

                argColName[7] = "放电终止电压";
                argColContent[7] = dt.Rows[remIndex]["放电终止电压设置"];

                argColName[8] = "充放电图形";
                argColContent[8] = chartname;  

                argColName[9] = "放电安时数";
                argColContent[9] = txt_1_fdass.Text;

                ISchemeRowBO _SchemeRowBO = new SchemeRowBO();
                _SchemeRowBO.SaveData("TestData", argColName, argColContent, true);
                MessageBox.Show("蓄电池充放电监测数据保存成功!");
            }
            catch(Exception ee)
            {
                MessageBox.Show("蓄电池充放电监测数据保存失败!");
            }
        }

        private void btn_1_save_Click(object sender, EventArgs e)
        {

            Save1();
        }

        #endregion

        #region 退出程序

        private void btn_quit_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().Close();
                //0，3，7，8
                //if (
                //    (xdc.P1_显示运行状态 == "0"
                //    || xdc.P1_显示运行状态 == "3"
                //    || xdc.P1_显示运行状态 == "7"
                //    || xdc.P1_显示运行状态 == "8"
                //    )
                //    )
                //{
                //    this.FindForm().Close();
                //}
                //else
                //{
                //    MessageBox.Show("正在运行中!无法关闭程序!");
                //}
            }
            catch
            {
                for (int i = 0; i < 2; i++)
                {
                    LoadKeyBoardClass.unLoad();
                }
                this.FindForm().Close();
            }

        }
        #endregion

        public string retStr = "";
        bool ifsetpara = false;

        private void UC_MainTest_Load(object sender, EventArgs e)
        {
            NewTest tt_NewTest = new NewTest();
            if (tt_NewTest.ShowDialog(this) == DialogResult.OK)
            {
                retStr = tt_NewTest.retStr;
                groupBox12.Text = retStr + " --- 试验工况";
            }
            else
            {
                this.Enabled = false;
                timer1.Enabled = true;
                return;
            }
            if(retStr.Length>1)
            {
                ifsetpara = true;
            }
            int setValue = 0;
            if (int.TryParse("120", out setValue))
            {
                xdc.Com_Siemens.ExcuteCommand_Write("循环充电静置时间设置", setValue);
                xdc.Com_Siemens.ExcuteCommand_Write("循环放电静置时间设置", setValue);
            }
            Thread.Sleep(1000);
            init();
            timer_1.Enabled = true;
            timer_xsz.Enabled = true;

            txt_1_chexing.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_1_chexing.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_1_chehao.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_1_chehao.LostFocus += new EventHandler(txtPwd_LostFocus);

            txt_1_chuchangbianhao.GotFocus += new EventHandler(txtPwd_GotFocus);
            txt_1_chuchangbianhao.LostFocus += new EventHandler(txtPwd_LostFocus);
            chart1.Series["充电电压曲线"].Points.AddXY(0, 0);
            chart1.Series["充电电流曲线"].Points.AddXY(0, 0);
        }

        private void timer_xsz_Tick(object sender, EventArgs e)
        {
            string tempstr;
            if (SelectTestIndex == 2)//整组
            {
                txt_1_dcdy.Text = xdc.P1_显示电池电压; 
            }
            txt_1_cdsj.Text = xdc.P1_显示充电时间;
            txt_1_fdsj.Text = xdc.P1_显示放电时间;
        }

        public void init()
        {
            tabPage1.Text = "整组电池充放电";
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = double.Parse(xdc.P1_充电时间设置值);
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = chart1.ChartAreas["ChartArea1"].AxisX.Maximum / 10;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 240;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 20;

            chart1.ChartAreas["ChartArea2"].AxisX.Maximum = double.Parse(xdc.P1_放电时间设置值);
            chart1.ChartAreas["ChartArea2"].AxisX.Interval = chart1.ChartAreas["ChartArea2"].AxisX.Maximum / 10;
            chart1.ChartAreas["ChartArea2"].AxisY.Maximum = 100;
            chart1.ChartAreas["ChartArea2"].AxisY.Interval = 10;


            chart1.Series["充电电压曲线"].Points.Clear();
            chart1.Series["充电电流曲线"].Points.Clear();
            chart1.Series["充电电压曲线"].Points.AddXY(0, 0);
            chart1.Series["充电电流曲线"].Points.AddXY(0, 0);
            this.btn_1_qrxh.Enabled = true;
        }


        void txtPwd_LostFocus(object sender, EventArgs e)
        {
            LoadKeyBoardClass.unLoad();
        }

        void txtPwd_GotFocus(object sender, EventArgs e)
        {
            LoadKeyBoardClass.toLoad();
        }

        private void 键盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                LoadKeyBoardClass.unLoad();
            }
            LoadKeyBoardClass.toLoad();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                xdc.Com_Siemens.ExcuteCommand_Set("自动循环选择");
            else
                xdc.Com_Siemens.ExcuteCommand_Reset("自动循环选择");
        }

        private void txt_1_chexing_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void txt_1_chehao_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void txt_1_chuchangbianhao_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void dptime1_Enter(object sender, EventArgs e)
        {
            getkeyword();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int setValue = 0;
            if (int.TryParse(textBox3.Text, out setValue))
            {
                xdc.Com_Siemens.ExcuteCommand_Write("循环充电静置时间设置", setValue);
                xdc.Com_Siemens.ExcuteCommand_Write("循环放电静置时间设置", setValue);
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.FindForm().Close();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            //xdc.Com.ExcuteCommand("1#返回操作按下");
           // xdc.Com.ExcuteCommand("1#返回操作弹起");
        }

    }
}

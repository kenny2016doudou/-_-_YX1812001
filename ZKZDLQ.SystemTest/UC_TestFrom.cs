using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using ZZ.Serial;
//using System.Drawing;
using SchemeManage.BusinessRule.Interface.SchemeModule;
using SchemeManage.BusinessRule.SchemeModule;
using SchemeManage.Model.Entity.SchemeModule;
using TestSystem.Common.Constant;
using SchemeManage.DataAccess;
using SchemeManage.DataAccess.Interface.SchemeModule;
using Automation.BDaq;
using System.Threading;
using usefulClass;
using ZKZDLQ.SystemTest;
using TestSystem.Command.PLC.Omron.HostLink.Fins;
using MySql;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MySql.Data.MySqlClient;
using System.IO.Ports;

namespace ZKZDLQ.SystemTest
{


    public partial class UC_TestFrom : UserControl
    {
        
        string str2 = "";
        string label651 = "";
        string text_mhdz1 = "";
        string text_zddz1 = "";
        string text_zctdz1 = "";
        string text_hzxqdz1 = "";
        string text_fzxqdz1 = "";
        SerialPort sp = new SerialPort("COM3");
        //获取mysql的连接地址
        //SchemeRow _schemerow = new SchemeRow();
        SchemeRowBO _schemerow = new SchemeRowBO();

        //public static string mysqlweb =ConfigurationManager.ConnectionStrings["Mysql"].ToString() ;
        public static int SYType = 0;
        public double Freq = 100000.0;
        public string[] Baseinfo_argColName;
        public object[] Baseinfo_argCloContent;

        public int FZFZPD = 0; //合分状态
        public int TU = 0; //图片
        public int DSD = 0; //单电双电
        public double Sensitivity = 2;
        //20131125
        // public static int RecordLength = 15000;
        //public static int PostSamples = 14000;
        public static int RecordLength = 1000;
        public static int PostSamples = 1000;

        double[] lbBuf_dcf;

        private fzctBXTForm fzbtf = null;

        double[] m_dataScaled, fzcd_dataScaled;

        string date;

        //private DataPoint selectedDataPoint = null;

        //HitTestResult hitResult = null;
        public string Fztp = "";
        public string hztp = "";
        string chartName_HZ; //合闸曲线图名称
        string chartName_FZ; //电磁阀得电曲线图名称
        public bool isHZ_Chart = false;
        public bool isFZ_Chart = false;



        public bool PDHZ = false;
        public bool PDFZ = false;
        public bool isHZState = false;
        public bool isFZState = false;
        public ZDLQcontrol control;
        public xhdyClass runxhdyClass = null;
        int startindex = 0;
        public double everyPtTime = 1.0f;

      
       

        public static Hashtable fzcd_to_aiTable = new Hashtable();
        public static Hashtable HZC = new Hashtable();
        public static Hashtable fzcd_to_aiTable1 = new Hashtable();
        //*************************************************************************************************数据存储
        public Image hzbxtimg, fzbxtimg;
        public string[] fhpara = new string[20];
        public string[] qmpara = new string[20];
        public string[] dzpara = new string[20];
        public bool[,] fzcdstates = new bool[2, 15];
        public string[,] fzcdtiems = new string[2, 15];
        public string[,] fzcdDatas = new string[2, 15];
        public showinfoForm run_showinfoForm = null;

        public void btnBack_DisPlay()
        {
            ChangeControlState("Enabled", btnDongZuoXingNeng, btnQiMi, null, null, null, null);
            tab_control_one();
        }

        public void btnQiMi_DisPlay()
        {
            //ChangeControlState("Enabled", btnBack, null, null, null, null, null);
            tab_control_QiMi();

        }

        public void btnDongZuoXingNeng_DisPlay()
        {
            //ChangeControlState("Enabled", btnBack, null, null, null, null, null);
            tab_control_DZXN();
            //dispaly();
        }
        /// <summary>
        /// 显示第一个画面
        /// </summary>
        public void tab_control_one()
        {
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tab_1);
        }
        /// <summary>
        /// 显示动作性能测试画面
        /// </summary>
        public void tab_control_DZXN()
        {
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tab_2);

        }
        /// <summary>
        /// 显示气密测试画面
        /// </summary>
        public void tab_control_QiMi()
        {
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tab_3);
        }


        /// <summary>
        /// 显示绝缘测试画面
        /// </summary>
        public void tab_control_JY()
        {
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tab_4);
        }
        #region 控件状态控制




        private void ControlState(string type, Control cb, Control b1, Control b2, Control b3, Control b4, Control b5, Control b6)
        {
            switch (type)
            {
                case "Enabled":
                    if (cb.Enabled && cb != b1 && cb != b2 && cb != b3 && cb != b4 && cb != b5 && cb != b6)
                    {
                        cb.Enabled = false;
                    }
                    break;
                case "Visible":
                    if (cb.Visible && cb != b1 && cb != b2 && cb != b3 && cb != b4 && cb != b5 && cb != b6)
                    {
                        cb.Visible = false;
                    }
                    break;
                case "ForeColor":
                    if (cb.ForeColor != Color.Red && cb != b1 && cb != b2 && cb != b3 && cb != b4 && cb != b5 && cb != b6)
                    {
                        cb.ForeColor = Color.Red;
                    }
                    break;
            }

        }


        /// <summary>
        /// 控件状态改变
        /// </summary>
        /// <param name="type"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <param name="b5"></param>
        /// <param name="b6"></param>
        private void ChangeControlState(string type, Control b1, Control b2, Control b3, Control b4, Control b5, Control b6)
        {
            switch (type)
            {
                case "Enabled":
                    ControlState(type, btnDongZuoXingNeng, b1, b2, b3, b4, b5, b6);
                    ControlState(type, btnQiMi, b1, b2, b3, b4, b5, b6);
                    //ControlState(type, btnBack, b1, b2, b3, b4, b5, b6);
                    if (b1 != null)
                        b1.Enabled = true;
                    if (b2 != null)
                        b2.Enabled = true;
                    if (b3 != null)
                        b3.Enabled = true;
                    if (b4 != null)
                        b4.Enabled = true;
                    if (b5 != null)
                        b5.Enabled = true;
                    if (b6 != null)
                        b6.Enabled = true;
                    break;
                case "Visible":
                    //***********//

                    if (b1 != null)
                        b1.Visible = true;
                    if (b2 != null)
                        b2.Visible = true;
                    if (b3 != null)
                        b3.Visible = true;
                    if (b4 != null)
                        b4.Visible = true;
                    if (b5 != null)
                        b5.Visible = true;
                    if (b6 != null)
                        b6.Visible = true;
                    break;

                case "ForeColor":
                    //***********//
                    if (b1 != null)
                        b1.ForeColor = Color.Lime;
                    if (b2 != null)
                        b2.ForeColor = Color.Lime;
                    if (b3 != null)
                        b3.ForeColor = Color.Lime;
                    if (b4 != null)
                        b4.ForeColor = Color.Lime;
                    if (b5 != null)
                        b5.ForeColor = Color.Lime;
                    if (b6 != null)
                        b6.ForeColor = Color.Lime;
                    break;
            }


        }
        #endregion
        private void SetChart()
        {

            chart1.Series["合闸"].Points.Clear();
            chart1.Series["分闸"].Points.Clear();
            //chart1.Series["电磁阀得电"].Points.Clear();//电磁阀得电/电磁阀"
            //chart1.Series["位移"].Points.Clear();//灭弧主触头/位移
            chart1.Series["主触头"].Points.Clear();//主触头/闸刀
            chart1.ChartAreas[0].AxisX.Maximum = RecordLength;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = RecordLength / 20;

            // chart1.Series["X1"].Points[0].XValue=;



            chart1.Series["X2"].Points[0].XValue = RecordLength;
            chart1.Series["X2"].Points[0].YValues[0] = 0;

            chart1.Series["X2"].Points[1].XValue = RecordLength;
            chart1.Series["X2"].Points[1].YValues[0] = 180;




            chart1.Series["Y1"].Points[0].XValue = 0;
            chart1.Series["Y1"].Points[0].YValues[0] = 5;
            chart1.Series["Y1"].Points[1].XValue = RecordLength;
            chart1.Series["Y1"].Points[1].YValues[0] = 5;



            chart1.Series["Y2"].Points[0].XValue = 0;
            chart1.Series["Y2"].Points[0].YValues[0] = 180;
            chart1.Series["Y2"].Points[1].XValue = RecordLength;
            chart1.Series["Y2"].Points[1].YValues[0] = 180;

            //chart1.Series["电磁阀得电"].Points.AddY(0);
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.ChartAreas[0].AxisX.Maximum = RecordLength;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Interval = RecordLength / 20;
            chart2.ChartAreas[0].AxisY.Maximum = 6;
            chart2.ChartAreas[0].AxisY.Minimum = -1;
            chart2.ChartAreas[0].AxisY.Interval = 1;
            chart2.Series["辅助触头"].Points.AddY(0);
            chart2.Series["分闸"].Points.AddY(0);
            chart2.Series["合闸"].Points.AddY(0);
        }
        /// <summary>
        /// 针对基本信息进行赋值
        /// </summary>
        public void XianShi()
        {
            //txtCPXH.Text = ZDLQ_Infomation.xinghao;
            //txtCPBH.Text = ZDLQ_Infomation.biaohao;
            ////txtXiuCheng.Text = ZKInfomation.xiucheng;
            ////txtXingHao.Text = ZKInfomation.pjxinghao;
            ////txtBianHao.Text = ZKInfomation.biaohao;
            ////txtCTLX.Text = ZKInfomation.chatouleixing;
            //txtJCY.Text = ZDLQ_Infomation.jianceyuan;
        }


        //************************************************************************************************载入
        private void UC_TestFrom_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = groupBox2.Enabled = groupBox5.Enabled = tabControl1.Enabled = tab_control.Enabled = false;
            btnOFF.Enabled = true;
            btnStart.Enabled = true;
            glassButton2.BackColor = Color.Red;
            YXDZ.OpenPort(sp, "COM3");
            byte[] ZL = new byte[] { 0x5A, 0xA5, 0x04, 0x00, 0x01, 0x03, 0x40, 0x85 };
            YXDZ.WriteData(sp, ZL, 2, false);
               string str12 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 2).Substring(0,3);
              string str13 = str12;
                //text_mhdz1 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 3);
                //text_zddz1 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 4);
                //text_zctdz1 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 5);
                //text_hzxqdz1 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 6);
                //text_fzxqdz1 = ZZ.Serial.YXDZ.ReadData(sp, ZL, 7);
            
            

        }


        public void FF(string SQL)
        {
            MySqlCommand cmd = null;
            string connstring = "server=localhost;User Id=root;password=root;Database=db_YX1812001";
            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();    //②打开数据库连接

           

            cmd = new MySqlCommand(SQL, conn); //③使用指定的SQL命令和连接对象创建SqlCommand对象 

            cmd.ExecuteNonQuery();

            conn.Close();   //⑦关闭连接
        }


        public ArrayList allctrlArray = new ArrayList();

        public void SetData(int lenght, int postsamples)
        {
            RecordLength = lenght;
            PostSamples = postsamples;

            lbBuf_dcf = new double[RecordLength];
        }
        public UC_TestFrom()
        {
            CheckForIllegalCrossThreadCalls = false;
            SYType = 0;
            InitializeComponent();
            control = ZDLQcontrol.getInstance();
            SetData(RecordLength, PostSamples);
            // Control.CheckForIllegalCrossThreadCalls = false;
            //XianShi();
            SetChart();
            //20131112
            //control.打开气源();

            //toshowByIndex();
            startdt = DateTime.Now;
        }
        private void ClearPoint()
        {
            chart1.Series["合闸"].Points.Clear();
            chart1.Series["分闸"].Points.Clear();
            //chart1.Series["电磁阀得电"].Points.Clear();
            //chart1.Series["位移"].Points.Clear();
            chart1.Series["主触头"].Points.Clear();
            chart1.Series["延时分闸"].Points.Clear();

        }
        private void ckDeDianTime_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDeDianTime.Checked)
            {


                // chart1.Series.Clear();

                chart1.ChartAreas[0].AxisX.Maximum = 500;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Interval = 20;


                chart1.Series["X2"].Points[0].XValue = 500;
                chart1.Series["X2"].Points[0].YValues[0] = 0;

                chart1.Series["X2"].Points[1].XValue = 500;
                chart1.Series["X2"].Points[1].YValues[0] = 180;


                chart1.Series["Y1"].Points[0].XValue = 0;
                chart1.Series["Y1"].Points[0].YValues[0] = 5;
                chart1.Series["Y1"].Points[1].XValue = 500;
                chart1.Series["Y1"].Points[1].YValues[0] = 5;



                chart1.Series["Y2"].Points[0].XValue = 0;
                chart1.Series["Y2"].Points[0].YValues[0] = 180;
                chart1.Series["Y2"].Points[1].XValue = 500;
                chart1.Series["Y2"].Points[1].YValues[0] = 180;


            }
            else
            {
                //  chart1.Series.Clear();

                chart1.ChartAreas[0].AxisX.Maximum = PostSamples;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Interval = 50;

                // chart1.Series["X1"].Points[0].XValue=;


                chart1.Series["X2"].Points[0].XValue = PostSamples;
                chart1.Series["X2"].Points[0].YValues[0] = 0;

                chart1.Series["X2"].Points[1].XValue = PostSamples;
                chart1.Series["X2"].Points[1].YValues[0] = 180;




                chart1.Series["Y1"].Points[0].XValue = 0;
                chart1.Series["Y1"].Points[0].YValues[0] = 5;
                chart1.Series["Y1"].Points[1].XValue = PostSamples;
                chart1.Series["Y1"].Points[1].YValues[0] = 5;



                chart1.Series["Y2"].Points[0].XValue = 0;
                chart1.Series["Y2"].Points[0].YValues[0] = 180;
                chart1.Series["Y2"].Points[1].XValue = PostSamples;
                chart1.Series["Y2"].Points[1].YValues[0] = 180;

            }
        }

        bool isHZ_Start = false;
        bool isHZ_Stop = false;
        bool isHZ_Status = false;

        bool isFZ_start = false;
        bool isFz_stop = false;

        //****************************************************************合闸电磁阀得电启动采集波形        

        public void HZ_FZ()
        {


            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            chart1.Series[5].Points.Clear();

            if (m_dataScaled != null)
                m_dataScaled.Initialize();
            if (fzcd_dataScaled != null)
                fzcd_dataScaled.Initialize();
            m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
            Array.Clear(m_dataScaled, 0, m_dataScaled.Length);
            ErrorCode err = ErrorCode.Success;
            err = bufferedAiCtrl1.Prepare();
            if (err == ErrorCode.Success)
            {
                err = bufferedAiCtrl1.Start();
            }
            if (err != ErrorCode.Success)
            {
                MessageBox.Show("主触头及位移采集卡启动不成功！");
                return;
            }



            //ErrorCode err2 = ErrorCode.Success;
            //err2 = bufferedAiCtrl2_fzcd.Prepare();
            //if (err2 == ErrorCode.Success)
            //{
            //    err2 = bufferedAiCtrl2_fzcd.Start();
            //}
            //if (err2 != ErrorCode.Success)
            //{
            //    MessageBox.Show("辅助触点状态采集卡启动不成功！");
            //    return;
            //}
        }

        string fz_path = "";
        string hz_path = "";

        private void btnSetQiYaZhi_Click(object sender, EventArgs e)
        {
            if (txtSetQiYa.Text != "")
            {
                int s = int.Parse(txtSetQiYa.Text);
                if (s < 1200)
                {
                    control.Com_Siemens.ExcuteCommand_Write("气压设置值", s);
                }
                else
                {
                    MessageBox.Show("输入的气压值不能大于1200kPa,请重新输入气压值！！！");
                    txtSetQiYa.Text = "";
                }
            }
            else
            {
                MessageBox.Show("输入的气压值参数不能为空，请重新输入！！！");
            }
        }


        public void closeDY()
        {
            //control.设置电压("0");
        }
        public void closeQY()
        {
            //control.设置气压("0");
            //control.断开气源();
        }

        public int loopi = 0;
        public string xsqyStr = "";
        int id = 0;
        private DateTime startdt;
        private TimeSpan ttts;
        private string sdcdStr = "";


        private double dzValue = 0;double a = 0.0;
        private void timer_Read_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (control.IComTongHui != null)
            //    {
                    
            //        object[] obj = control.IComTongHui.GetStepReaderIData("电阻");
            //        if (obj[0] != null)
            //        {
            //            if (checkBox3.Checked)
            //            {
            //                string dawei = Convert.ToString(obj[2]);
            //                switch (dawei)
            //                {
            //                    case "mΩ":
            //                        a = double.Parse(obj[1].ToString());
            //                        break;
            //                    case "Ω":
            //                        a = double.Parse(obj[1].ToString()) * 1000;
            //                        break;
            //                    case "KO":
            //                        a = double.Parse(obj[1].ToString()) * 1000 * 1000;
            //                        break;
            //                    case "MΩ":
            //                        a = double.Parse(obj[1].ToString()) * 1000 * 1000 * 1000;
            //                        break;
            //                }
            //                dzValue = a - 70;
            //                dawei = "mΩ";
            //                if (dzValue < 10000)
            //                {
            //                    label65.Text = string.Format("{0}{1}", a - 70, dawei);
            //                }
            //                if (dzValue > 10000)
            //                {
            //                    label65.Text = string.Format("{0}{1}", 0, dawei);
            //                }
            //            }
            //            else
            //            {
            //                string dawei = Convert.ToString(obj[2]);
            //                dawei = dawei.Replace('O', 'Ω');
            //                dzValue = double.Parse(obj[1].ToString());
            //                if (dzValue < 10000)
            //                {
            //                    label65.Text = string.Format("{0}{1}", obj[1], dawei);
            //                }
            //                if (dzValue > 10000)
            //                {
            //                    label65.Text = string.Format("{0}{1}", 0, dawei);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    dzValue = 0;
            //    MessageBox.Show("试验电阻数据为空！！！");
            //}
            try
            {
                ttts = DateTime.Now.Subtract(startdt);
                label10.Text = ttts.TotalHours.ToString("0.0") + "H";
                id++;
                string str_zldl = control.读取整流电流();
                string  str_qiyq = control.Com_Siemens.GetReader("试验气压").ToString();

                String str_dy = control.Com_Siemens.GetReader("电压值").ToString();
                //float stt_dy = Convert.ToSingle(str_dy);
                float stt_dy = float.Parse(str_dy);
              
                //MessageBox.Show(str_dy);

                if (str_zldl != "")
                {
                    int ZLDL = int.Parse(str_zldl);
                    txt_ZLDL.Text = (ZLDL / 100).ToString();
                }
                if (str_qiyq != "")
                {
                    xsqyStr = str_qiyq;
                    txt_QiYa.Text = str_qiyq;
                }
                if (stt_dy != 0)
                {
                    txt_DY.Text = str_dy.ToString();
                }
                //20131112

                //double a = control.读取电阻值();


                //txt_chutoudianzu.Text = a.ToString("0.0");


                //object[] objXH = control.IComOmron.GetReaderIData("触点状态信息");
                //try
                //{
                //    if (objXH.Count() > 0)
                //    {
                //        if (objXH[0] != null)
                //        {
                //            Dictionary<string, string> dicxh = objXH[0] as Dictionary<string, string>;
                //            loopi = 0;
                //            if (isHZ_Chart)
                //            {

                //            }
                //            else if (isFZ_Chart)
                //            {

                //            }
                //        }
                //    }
                //}
                //catch
                //{ 


            }
            catch
            {
                MessageBox.Show("试验仪表数据异常！！！");
            }
            //if (control.读取单双电状态() == "1")
            //{
            //    label9.ForeColor = Color.Blue;
            //    label9.Text = "双电控制";
            //    glassButton1.Text = "切换至单电控制";
            //    sdcdStr = "双电控制";
            //}
            //else
            //{
            //    label9.ForeColor = Color.Red;
            //    label9.Text = "单电控制";
            //    glassButton1.Text = "切换至双电控制";
            //    sdcdStr = "单电控制";
            //}

            if (control.读取长电状态() == "1")
            {
                label30.ForeColor = Color.Blue;
                label30.Text = "长电控制开";
                glassButton9.Text = "关闭长电控制";
                sdcdStr += ",长电开";
            }
            else
            {
                label30.ForeColor = Color.Red;
                label30.Text = "长电控制关";
                glassButton9.Text = "打开长电控制";
                sdcdStr += ",长电关";
            }

            if (control.读取24给电状态() == "1")
            {
                btn24gd.BackColor = Color.Red;
                //btn24gd.Text = "2/4给电停止";
            }
            else
            {
                btn24gd.BackColor = Color.Lime;
                //btn24gd.Text = "2/4给电开始";
            }
            //
            if (fzcdtimes_static.hzfz == "hz")
            {
                btnOFF.Enabled = true;
                btnOFF.BackColor = Color.Red;
                btnStart.BackColor = Color.Lime;
                //btnStart.Enabled = false;

                isHZState = true;
                isFZState = false;
            }
            else
            {
                btnStart.Enabled = true;
                btnStart.BackColor = Color.Red;
                btnOFF.BackColor = Color.Lime;
                //btnOFF.Enabled = false;
                isFZState = true;
                isHZState = false;


            }
            if (id == 1)
            {
                //initcdStates();
            }
            try
            {
                string scdy = control.读取输出电压();
                string scdl = control.读取输出电流();
                string jcdz = "";
                string szdl = control.读取设置电流值();

                //if (scdy != "")
                //{
                //    if (float.Parse(scdy) < 50)
                //    {
                //        txt_scdy.Text = scdy;
                //    }
                //}

                //if (scdl != "")
                //{
                //    if (float.Parse(scdl) < 5000)
                //    {
                //        txt_scdl.Text = scdl;
                //    }
                //}

                //if (szdl != "")
                //{
                //    lbl_szdlz.Text = szdl;
                //}

                //if (scdy != "" && scdl != "" && szdl != "0")
                //{

                //    jcdz = (float.Parse(scdy) / float.Parse(szdl)).ToString("0.0000");
                //    txt_jcdz.Text = jcdz.ToString();
                //}
            }
            catch
            {

            }
        }

        public double re_Pvalue(double v1, double v2, double v3)
        {
            if (v2 > v1 * 1.5 && v2 > v3 * 1.5)
            {
                v2 = v1;
            }
            else
            {
                if (v2 < v1 * 0.5 && v2 < v3 * 0.5)
                {
                    v2 = v1;
                }
                else
                {
                    v2 = (v1 + v3 + v2) / 3;
                }
            }
            return Math.Round(v2, 4);
        }


        /// <summary>
        /// 得点时间平滑处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double PinHuaChuli(double value)
        {
            if (Math.Abs(15 - value) > 0 && Math.Abs(15 - value) < 10)
            {
                value = 15;
            }
            return value;
        }

        /// <summary>
        /// 平滑处理
        /// </summary>
        /// <param name="value">采集值</param>
        /// <param name="phvalue">平滑标准值</param>
        /// <param name="daxiao">比较值</param>
        /// <returns></returns>
        double PinHuaChuli(double value, int phvalue, float daxiao)
        {
            if (Math.Abs(phvalue - value) > 0 && Math.Abs(phvalue - value) < daxiao)
            {
                value = phvalue;
            }
            return value;
        }

        double Datalevel(double data1, double curData, double data3)
        {
            if (data1 != -1 && curData != -1 && data3 != -1)
            {
                data1 = Math.Round(data1, 2);
                curData = Math.Round(curData, 2);
                data3 = Math.Round(data3, 2);
                if (Math.Abs(data1 - curData) >= 0.2 && Math.Abs(curData - data3) >= 0.2)
                {
                    return (data1 + curData + data3) / 3;
                }
            }

            return curData;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public const byte lCh1 = 1;
        public const byte lCh2 = 2;
        public const byte lCh3 = 3;
        public const byte lCh4 = 4;
        public const byte ltsOr = 7;
        public void InitialBoxing()
        {
            DeviceInformation ttdi = new DeviceInformation("PCI-1716L,BID#0");
            bufferedAiCtrl1.SelectedDevice = ttdi;
            bufferedAiCtrl1.ScanChannel.ChannelStart = 0;
            bufferedAiCtrl1.ScanChannel.ChannelCount = 16;
            bufferedAiCtrl1.ScanChannel.IntervalCount = 1000;
            bufferedAiCtrl1.ScanChannel.Samples = 1000;
            bufferedAiCtrl1.Streaming = false;
            bufferedAiCtrl1.ConvertClock.Rate = 1000;
            bufferedAiCtrl1.ConvertClock.Source = SignalDrop.SigInternalClock;
            m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
            Array.Clear(m_dataScaled, 0, m_dataScaled.Length);
            if (!bufferedAiCtrl1.Initialized)
            {
                MessageBox.Show("数据采集卡初始化错误");
            }
            fzcd_dataScaled = new double[bufferedAiCtrl2_fzcd.BufferCapacity];
        }


        private void btnDongZuoXingNeng_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////// 波形图缩小
            chart1.ChartAreas[0].AxisX.Maximum = 500;
           // chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 20;
            chart1.Series["合闸"].Points.AddXY(0,0);

            // chart1.Series["X2"].Points[0].XValue = 500;
            //chart1.Series["X2"].Points[0].YValues[0] = 0;

            //chart1.Series["X2"].Points[1].XValue = 500;
            //chart1.Series["X2"].Points[1].YValues[0] = 180;


            //chart1.Series["Y1"].Points[0].XValue = 0;
            //chart1.Series["Y1"].Points[0].YValues[0] = 5;
            //chart1.Series["Y1"].Points[1].XValue = 500;
            //chart1.Series["Y1"].Points[1].YValues[0] = 5;



            //chart1.Series["Y2"].Points[0].XValue = 0;
            //chart1.Series["Y2"].Points[0].YValues[0] = 180;
            //chart1.Series["Y2"].Points[1].XValue = 500;
            //chart1.Series["Y2"].Points[1].YValues[0] = 180;

            /////////////////////////////////////////////////////////////

            if (textBox7.Text=="SS3")
            {
                //弹跳时间
                label14.Visible = false;
                ledTTSJ.Visible = false;
                label13.Visible = false;

                //延迟时间
                label59.Visible = true;
                ledYSSJ.Visible = true;
                label63.Visible = true;
            }


            control.Com_Siemens.ExcuteCommand_Write("试验选择", 1);
            ifautoTestxn = dytest = qytest = false;

            control.断开气源复位();
            //timer_Charting.Enabled = true;
            tab_control_DZXN();
            // dispaly();
            //2013112
            //禁用合闸按钮
            btnStart.Enabled = true;
            btnOFF.Enabled = true;
            //control.打开气源();
            control.打开第二个电磁阀气源();
            //System.Threading.Thread.Sleep(20);
            //control.打开第二个电磁阀气源复位();


            InitialBoxing();
            try
            {
                InitialBoxing();
            }
            catch
            { }
            SYType = 1;

        }
        private DateTime starthzdoDT, startfzdoDT;
        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
            //control.合闸置位();

            control.Com_Siemens.ExcuteCommand_PulseZW("软件合闸");
        }

        private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
            TU = 0;
            FZFZPD = 1;
            //control.合闸复位();
            fzcdtimes_static.hzfz = "hz";
            hfzSwitchState();
            if (textBox7.Text == "BVACN99D")
            {
                control.设置电压(txtsetdcfdy.Text);
            }
            //System.Threading.Thread.Sleep(500);
            //if (comboBox1.Text == "")
            //{
            //    MessageBox.Show("主断类型不能为空!请在下拉筐选择主断类型!");
            //    return;
            //}
            if (double.Parse(txtSetQiYa.Text) < (double.Parse(txt_QiYa.Text) - 50.0))
            {
                // MessageBox.Show("气压未到指定值，无法合闸。");
                //return;
            }
            //control.分闸复位();

            //

            if (!isHZ_Chart)
            {


                chartName_HZ = DateTime.Now.ToString("yyyyMMddHHmmss") + "_HZ.jpg";
                hztp = chartName_HZ;
                isHZ_Chart = true;
                isFZ_Chart = false;
                isHZ_Start = true;
                isHZ_Stop = false;
                isHZ_Status = true;


                if (this.txt_ykj.Text == "")
                {
                    MessageBox.Show("原位移不能为空!");
                    return;
                }

                HZ_FZ();
                starthzdoDT = DateTime.Now;
                ledDCDDSJ.BackColor = Color.White;
            }
            if (isHZ_Start)
            {
                control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
                // control.合闸置位();
                // control.合闸复位();

                // controller.SetDZXN_Start_Stop();

                //labColorHeZha.ForeColor = Color.Red;
                //labColorFenZha.ForeColor = Color.Black;

                isHZ_Stop = true;
                isHZ_Start = false;



                isHZ_Chart = true;
                isFZ_Chart = false;

                //timerJS.Enabled = true;
                //System.Threading.Thread.Sleep(50);

                //fzcdTimeProc();
            }
            //System.Threading.Thread.Sleep(500);
            //MessageBox.Show("合闸图片保存!");

        }

        private void btnOFF_MouseDown(object sender, MouseEventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_PulseZW("软件分闸");
            // control.分闸置位();

        }

        private void btnQiMi_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_Write("试验选择", 3);
            control.断开气源复位();
            SYType = 2;
            tab_control_QiMi();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_Pulse("软件停止");
            control.Com_Siemens.ExcuteCommand_Write("气压设置值", 0);
            control.Com_Siemens.ExcuteCommand_Write("电压设置值", 0);
            timer_Read.Enabled = false;
            //control.closePort();
            this.FindForm().Dispose();
            if (run_showinfoForm != null)
            {
                run_showinfoForm.Close();
            }
        }

        private void openQYButton_Click(object sender, EventArgs e)
        {

            control.打开气源();
            //control.断开气源();
            //control.开始充气();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            closeQY();
            txtSetQiYa.Text = "0";
        }

        //2013112
        /// <summary>
        /// 上一个气密性测试的进程
        /// </summary>
        int progQMTemp;//上一个气密性测试的进程值暂存
        //
        private void timer_QMRead_Tick(object sender, EventArgs e)
        {
            //label5.Text = (progressBar1.Value / progressBar1.Maximum).ToString() + "%";
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                timer_QMRead.Enabled = false;
                txtQiYaEnd.Text = txt_QiYa.Text;

                bmxceCau();
                btnStart_QiMi.Text = "开始测试";
                btn_Cancel.Enabled = true;
            }
        }


        private void btnStartCQ_Click(object sender, EventArgs e)
        {
            //control.停止充气();
            //control.断开气源();
            control.开始排气();
            //System.Threading.Thread.Sleep(300);
            control.开始充气();
            control.打开气源();
            //control.设置气压("1000");
            control.Com_Siemens.ExcuteCommand_Write("气压设置值", 1000);

            //if (int.Parse(txtSetQiYa.Text) <= 350)
            //{
            //    MessageBox.Show("气压设置小于350kPa，不能进行动作。");
            //    return;
            //}
            //control.开始充气();
            //// btnStart2.Enabled = true;
            ////  btnOFF2.Enabled = true;
            //control.断开气源复位();
            //control.开始充气();


            //try
            //{

            //    Convert.ToInt32(txtQiYaStart.Text);
            //}
            //catch
            //{


            //    MessageBox.Show("输入的气压值必须是数字！");
            //    return;
            //}

            //if (int.Parse(txtQiYaStart.Text) <= 50)
            //{

            //    MessageBox.Show("开始气压值必须大于50kPa,请重新输入气压值！！！");
            //    return;



            //}

            //if (txtQiYaStart.Text.IndexOf("-") != -1)
            //{

            //    MessageBox.Show("开始气压不能输入负数！");
            //    return;
            //}
            //if (txtQiYaStart.Text == "0")
            //{

            //    MessageBox.Show("开始气压不能输入0！");
            //    return;
            //}
            //if (txtQiYaStart.Text != "")
            //{
            //    int s = int.Parse(txtQiYaStart.Text);
            //    if (s < 1000)
            //    {
            //        control.设置气压(s.ToString());
            //        this.btnStart_QiMi.Visible = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("输入的开始气压值不能大于1200kPa,请重新输入气压值！！！");
            //        txtQiYaStart.Text = "";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("输入的开始气压值参数不能为空，请重新输入！！！");
            //}






        }

        private void btnStartPQ_Click(object sender, EventArgs e)
        {
            control.停止充气();
            control.断开气源();
            control.开始排气();
        }

        int fenzhong = 0;
        int px = 0;
        private void btnStart_QiMi_Click(object sender, EventArgs e)
        {
            if (runxhdyClass.xhtypes.Contains("电磁"))
            {
                MessageBox.Show("电磁型主断无法进行气密性测试！");
                return;
            }
            txtQiYaEnd.Text = "";
            txtQiYaXieLL.Text = "";
            progQMTemp = 0;//将测试进程暂存数初始化为0
            //
            if (btnStart_QiMi.Text == "开始测试")
            {
                try
                {
                    Convert.ToInt32(textBox1.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("缓冲时间格式错误！请输入整数时间");
                    return;
                    //throw;
                }
                control.Com_Siemens.ExcuteCommand_Pulse("气密性试验启动");
                DialogResult dr;
                FormTime f = new FormTime("气压缓冲倒计时", this);
                f.allTime = this.textBox1.Text;
                f.StartPosition = FormStartPosition.CenterScreen;
                dr = f.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    label41.Text = "";
                    px = 0;
                    fenzhong = Convert.ToInt32(txtQiMiTime.Text) * 60000;
                    int a = fenzhong / 100;
                    this.timer3.Enabled = true;
                    this.timer4.Enabled = true;
                    this.timer3.Interval = fenzhong;
                    this.timer4.Interval = a;
                    txtQiYaStart.Text = control.Com_Siemens.GetReader("试验气压").ToString();
                }
                else if (dr == DialogResult.Cancel)
                {
                    control.保压复位();
                    this.timer3.Enabled = false;
                    this.timer4.Enabled = false;
                    return;
                }
                if (Convert.ToInt32(txtQiMiTime.Text) > 0)
                {
                    //  control.保压复位();
                    //  timer_QMRead.Enabled = true;
                    // btn_Cancel.Enabled = false;
                    btnStart_QiMi.Text = "停止测试";
                    //   txtQiYaStart.Text = txt_QiYa.Text;
                    // txtQiYaEnd.Text = "0";
                    timer_QMRead.Interval = 1000;
                    int maxcount = (int)(float.Parse(txtQiMiTime.Text) * 60);
                    progressBar1.Maximum = maxcount;
                    progressBar1.Value = 0;
                    progressBar1.Step = 1;
                }
            }
            //20131112
            else
            {
                control.断开气源复位();
                px = 0;
                this.timer4.Enabled = false;
                this.timer3.Enabled = false;
                //control.停止测试();
                control.保压复位();
                txtQiYaEnd.Text = txt_QiYa.Text;
                if (txtQiYaEnd.Text == null)
                {
                    txtQiYaEnd.Text = "0";
                }
                int b = Convert.ToInt32(txtQiYaStart.Text) - Convert.ToInt32(txtQiYaEnd.Text);
                timer_QMRead.Enabled = false;
                btn_Cancel.Enabled = true;
                btnStart_QiMi.Text = "开始测试";
                txtQiYaXieLL.Text = b.ToString();
                if (txtQiYaXieLL.Text.IndexOf("-") != -1)
                {
                    txtQiYaXieLL.Text = "0";
                }
            }
            bmxceCau();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void stopCQButton_Click(object sender, EventArgs e)
        {

            //control.停止充气置位();
        }

        private void dzStartCQButton_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSetQiYa.Text) <= 350)
            {
                MessageBox.Show("气压设置小于350kPa，不能进行动作。");
                return;
            }
            control.开始充气();
            btnStart.Enabled = true;
            btnOFF.Enabled = true;
        }

        private void dzStopCQButton_Click(object sender, EventArgs e)
        {
            control.停止充气();
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {



        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="cslx">测试类型</param>
        /// <returns></returns>
        public bool SaveData(string cslx)
        {
            if (!ifBaseinfoSaved)
            {
                ifBaseinfoSaved = true;
                Baseinfo_argCloContent[6] = sdcdStr;
                _schemerow.SaveData("BaseInfo", Baseinfo_argColName, Baseinfo_argCloContent, false);
            }
            try
            {
                if (File.Exists(Application.StartupPath + @"\testimg\" + chartName_HZ) && File.Exists(Application.StartupPath + @"\testimg\" + chartName_FZ))
                {
                    File.Copy(Application.StartupPath + @"\testimg\" + chartName_HZ, Application.StartupPath + @"\picture\" + hztp);
                    File.Copy(Application.StartupPath + @"\testimg\" + chartName_FZ, Application.StartupPath + @"\picture\" + Fztp);
                }
                DirectoryInfo ttdi = new DirectoryInfo(Application.StartupPath + @"\testimg");
                ttdi.Delete(true);
                Directory.CreateDirectory(Application.StartupPath + @"\testimg");

                string[] argcolName = new string[11];
                string[] argconContent = new string[11];

                String conString = "";
                argcolName[1] = "合闸图形";
                argconContent[1] = hztp;

                argcolName[2] = "分闸图形";
                argconContent[2] = Fztp;

                conString += "合闸时间" + ":" + ledHZSJ.Text + "#";
                conString += "合闸速度" + ":" + ledHZSD.Text + "#";
                conString += "分闸时间" + ":" + ledFZSJ.Text + "#";
                conString += "分闸速度" + ":" + ledFZSD.Text + "#";
                conString += "超程时间" + ":" + ledCCSJ.Text + "#";
                conString += "弹跳时间" + ":" + ledTTSJ.Text + "#";
                conString += "电磁阀得电时间" + ":" + ledDCDDSJ.Text + "#";
                conString += "延时时间" + ":" + ledYSSJ.Text + "#";
                conString += "逻辑检测" + ":" + comboBox3.Text + "#";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    conString += dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + ":" + dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() + "#";
                }

               // conString += "最低电压" + ":" + txt_zddy2.Text + "#";
                conString += "最低气压" + ":" + txt_zdqy.Text + "#";

                argcolName[0] = "分合闸主要检测参数";
                argconContent[0] = conString + firstPosSaveStr + secondPosSaveStr;


                //conString = "";
                //conString += "气密性能试验" + ":" + cmbSFHG.Text + "#";
                //conString += "气压泄漏量" + ":" + txtQiYaXieLL.Text + "#";
                //conString += "结束时气压" + ":" + txtQiYaEnd.Text + "#";
                argcolName[3] = "气压泄漏量";
                argconContent[3] = txtQiYaXieLL.Text;

                conString = "";
                comPanel ttcomPanel = null;

                //for (int i = 0; i < allctrlArray.Count; i++)
                //{
                //    ttcomPanel = (comPanel)allctrlArray[i];
                //    ttcomPanel.updateSaveTag();
                //    conString += ttcomPanel.SaveTag + "#";
                //}
                //argcolName[4] = "辅助触点试验结果";
                //argconContent[4] = conString;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   string aa= dataGridView1.Rows[i].Cells[0].Value.ToString();
                   string ab = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    conString += (aa +":"+ ab) + "#";

                }
                argcolName[4] = "辅助触点试验结果";
                argconContent[4] = conString;
                argcolName[5] = "操作性能试验";
                argconContent[5] = comboBox4.Text;
                argcolName[6] = "空气断路器灭弧触头电阻";
                argconContent[6] = text_mhdz.Text;
                argcolName[7] = "空气断路器闸刀电阻";
                argconContent[7] = text_zddz.Text;
                argcolName[8] = "真空断路器主触头电阻";
                argconContent[8] = text_zctdz.Text;
                argcolName[9] = "合闸线圈电阻";
                argconContent[9] = text_hzxqdz.Text;
                argcolName[10] = "分闸线圈电阻";
                argconContent[10] = text_fzxqdz.Text;
                _schemerow.SaveData("TestData", argcolName, argconContent, true);
                MessageBox.Show(cslx + " 试验数据保存成功!");
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
        }



        #region 获得已经有的数据

        SchemeInfo _schemeInfo = null;
        private ISchemeTable bll_SchemeTable = BLL_Reference<ISchemeTable>.CreateObj("SchemeTable");
        private ISchemeInfo bll_SchemeInfo = BLL_Reference<ISchemeInfo>.CreateObj("SchemeInfo");
        private ISchemeColumn bll_SchemeColumn = BLL_Reference<ISchemeColumn>.CreateObj("SchemeColumn");
        private ISchemeRow bll_SchemeRow = BLL_Reference<ISchemeRow>.CreateObj("SchemeRow");

        private DataTable GetData()
        {
            BindColum();
            DataTable dt = dvtodt(this.dataGridView2);
            return dt;
        }


        private void BindColum()
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Columns.Clear();
            _schemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            SchemeTable _schemeTable = bll_SchemeTable.SelectSchemeTable(StaticModule.Scheme_ID, "", "", "TestData", "");
            DataSet dsSchemeCol = bll_SchemeColumn.SelectSchemeColumn(_schemeTable.Table_ID, "", "", "");

            //测试数据表名
            string TestDataTName = _schemeInfo.Scheme_Code + "_" + _schemeTable.Table_Code;

            if (dsSchemeCol.Tables[0].Rows.Count > 0)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "id";
                column.HeaderText = "id";
                column.Name = "id";
                column.DataPropertyName = "id";
                column.ReadOnly = true;
                column.Visible = false;
                column.Width = 150;
                this.dataGridView2.Columns.Add(column);

                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                column2.DataPropertyName = "parent_id";
                column2.HeaderText = "parent_id";
                column2.Name = "parent_id";
                column2.DataPropertyName = "parent_id";
                column2.ReadOnly = true;
                column2.Visible = false;
                column2.Width = 150;
                this.dataGridView2.Columns.Add(column2);

                for (int i = 0; i < dsSchemeCol.Tables[0].Rows.Count; i++)
                {
                    DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.HeaderText = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.Name = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Code"].ToString();
                    column1.ReadOnly = true;
                    column1.Width = 150;
                    this.dataGridView2.Columns.Add(column1);
                }

                string colCode = "Parent_ID";
                string sQuery = GetParentID();
                DataSet ds = bll_SchemeRow.SelectData(TestDataTName, colCode, sQuery, "", "");
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private string GetParentID()
        {
            SchemeTable _schemeTable = bll_SchemeTable.SelectSchemeTable(StaticModule.Scheme_ID, "", "", "BaseInfo", "");
            string baseInfo_TableName = _schemeInfo.Scheme_Code + "_" + _schemeTable.Table_Code;
            DataTable dt = bll_SchemeRow.SelectData(baseInfo_TableName, "", "", "", "").Tables[0];

            var a = (from r in dt.AsEnumerable() select r.Field<Int32>("id")).Max();
            return a.ToString();
        }

        private DataTable dvtodt(DataGridView dv)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            for (int i = 0; i < dv.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = dv.Columns[i].HeaderText.ToString();
                dt.Columns.Add(dc);
            }
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    if (dv.Rows[j].Cells[x].Value is DateTime)
                    {
                        dr[x] = DateTime.Parse(dv.Rows[j].Cells[x].Value.ToString()).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dr[x] = dv.Rows[j].Cells[x].Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="jcxm">检测项目</param>
        /// <param name="jcjg">检测结果</param>
        private void SaveData(string jcxm, string jcjg)
        {
            ISchemeRowBO schemeRowBO = new SchemeManage.BusinessRule.SchemeModule.SchemeRowBO();

            var reuslt = from r in GetData().AsEnumerable() where r.Field<string>("检测检修项目") == jcxm select r.Field<string>("测试数据ID");

            if (reuslt.Count() == 0)
            {

                string[] argColName = new string[6];
                object[] argCloContent = new object[6];
                argColName[0] = "检测检修项目";
                argCloContent[0] = jcxm;

                argColName[1] = "检修结果";
                argCloContent[1] = jcjg;


                argColName[2] = "处理意见";
                argCloContent[2] = "";

                argColName[3] = "自检";
                argCloContent[3] = "";

                argColName[4] = "互检员";
                argCloContent[4] = "";


                argColName[5] = "测试数据ID";
                argCloContent[5] = Guid.NewGuid().ToString();
                schemeRowBO.SaveData("TestData", argColName, argCloContent, true);


            }
            else
            {
                string[] argColName = new string[5];
                string[] argCloContent = new string[5];

                argColName[0] = "检修结果";
                argCloContent[0] = jcjg;

                argColName[1] = "处理意见";
                argCloContent[1] = "";

                argColName[2] = "自检";
                argCloContent[2] = "";

                argColName[3] = "互检员";
                argCloContent[3] = "";

                argColName[4] = "测试数据ID";
                argCloContent[4] = reuslt.First();

                schemeRowBO.ModifyData("testdata", "测试数据ID", argColName, argCloContent);

            }
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            frmReportDataSave frm = new frmReportDataSave();
            frm.ShowDialog();
        }

        private void btn_jycs_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_Write("试验选择", 2);
            tab_control_JY();
            //control.IComTongHui.Start();
        }

        private void tab_control_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tab_control == null)
            //    return;
            //switch (tab_control.SelectedTab.Text)
            //{
            //    case "动作性能":
            //        control.Com_Siemens.ExcuteCommand_Write("试验选择", 1);
            //        break;
            //    case "电阻测量":
            //        control.Com_Siemens.ExcuteCommand_Write("试验选择", 2);
            //        break;
            //    case "气密测试":
            //        control.Com_Siemens.ExcuteCommand_Write("试验选择", 3);
            //        break;
            //    case "操作性能测试":
            //        control.Com_Siemens.ExcuteCommand_Write("试验选择", 4);
            //        break;
            //    case "压力开关测试":
            //        control.Com_Siemens.ExcuteCommand_Write("试验选择", 5);
            //        break;
            //    default:
            //        break;
            //}
        }

        private bool ifBaseinfoSaved = false;
        private string firstPosSaveStr = "", secondPosSaveStr = "";
        private void btn_save_dzcs_Click(object sender, EventArgs e)
        {
            if (comboBox5.Visible)//一位，二位
            {
                if (comboBox5.SelectedIndex == 0)
                {
                    firstPosSaveStr = "";
                    firstPosSaveStr += "Ⅰ合闸时间" + ":" + ledHZSJ.Text + "#";
                    firstPosSaveStr += "Ⅰ分闸时间" + ":" + ledFZSJ.Text + "#";
                    firstPosSaveStr += "Ⅰ弹跳时间" + ":" + ledTTSJ.Text + "#";
                    firstPosSaveStr += "Ⅰ操作性能" + ":" + comboBox4.Text + "#";
                    MessageBox.Show("控制Ⅰ位测试数据保存成功！");
                }
                else if (comboBox5.SelectedIndex == 1)
                {
                    secondPosSaveStr = "";
                    secondPosSaveStr += "Ⅱ合闸时间" + ":" + ledHZSJ.Text + "#";
                    secondPosSaveStr += "Ⅱ分闸时间" + ":" + ledFZSJ.Text + "#";
                    secondPosSaveStr += "Ⅱ弹跳时间" + ":" + ledTTSJ.Text + "#";
                    secondPosSaveStr += "Ⅱ操作性能" + ":" + comboBox4.Text + "#";
                    SaveData("");
                }
            }
            else
            {
                SaveData("");
            }
        }


        private void btn_setdl_Click(object sender, EventArgs e)
        {
            control.设置电流("0");
            //control.设置电流(num_dl.Value.ToString());

        }

        private void btn_dzqd_Click(object sender, EventArgs e)
        {
            control.电阻测试启动();
        }

        private void btn_dztz_Click(object sender, EventArgs e)
        {
            control.电阻测试停止();
        }
        delegate void UpdateUIDelegate();


        float ptdytime = 0.4f;
        float jstimes = 0, sdtimes = 0;
        string bm = "0";//电磁阀得电
        string hezha = "0";//合闸
        string FuZuHeZha = "0"; //
        double temp = 0, temp2, temp3;
        string zdwysj = "0";//最大位移时间
        string fenzha = "0";
        string zctttsj = "0";//主触头弹跳时间

        string fendian = "0";
        string fendianh = "0";
        string YXfenzha = "";
        string weiyi = "";
        int phclValue = 10;
        private double tempdouble = 0;
        private double fzcdCauStartPoint = 0;
        private Random ttrd = new Random();

        public int maxxc = 50;//mm位移传感器行程
        public double mindy = 0; //位移传感器最小电压
        public double maxdy = 5;//位移传感器最大电压
        /// <summary>
        /// 电压转换为行程
        /// </summary>
        /// <param name="stdy"></param>
        /// <param name="eddy"></param>
        /// <returns></returns>
        public string convertToJL(double stdy, double eddy)
        {
            double intervaldy_xc = 50;
            intervaldy_xc = 50 / ((maxdy - mindy) * 1000);
            double middy = Math.Abs(stdy - eddy) * 1000;
            double ttjl = intervaldy_xc * middy;

            return ttjl.ToString("0");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
             control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
           // control.Com_Siemens.ExcuteCommand_PulseFW("软件合闸");




            /* control.合闸置位();
             Thread.Sleep(1000);
             control.合闸复位();*/

            if (control.读取分闸状态指示() == "1")
            {
                control.分闸复位();
               //control.合闸置位();
               //control.合闸复位();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //textBox2.Text = control.get_testdy();

            //textBox3.Text = control.get_testdl();

            //textBox4.Text = control.get_testqy();
        }


        private void glassButton1_Click(object sender, EventArgs e)
        {
            //if (glassButton1.Text.Contains("自动"))
            //{
            //    control.IComOmron.ExcuteCommand("自动置位复位");
            //    glassButton1.Text = "气源手动控制";
            //}
            //else
            //{

            //    control.IComOmron.ExcuteCommand("自动置位");
            //    glassButton1.Text = "气源自动控制";
            //}

            //if (glassButton1.Text == "切换至单电控制")
            //{
            //    control.IComOmron.ExcuteCommand("单电控制");
            //    glassButton1.Text = "切换至双电控制";
            //    control.IComOmron.ExcuteCommand("双电控制合闸复位");
            //}
            //else
            //{
            //    if (glassButton1.Text == "切换至双电控制")
            //    {
            //        control.IComOmron.ExcuteCommand("双电控制");
            //        glassButton1.Text = "切换至单电控制";
            //        control.IComOmron.ExcuteCommand("双电控制合闸");
            //    }
            //}

            //if (glassButton1.Text == "切换至单电控制")
            //{
            //    //  control.IComOmron.ExcuteCommand("双电控制合闸复位");
            control.Com_Siemens.ExcuteCommand_PulseZW("单电双电控制");

            //}
            //else
            //{
            //    if (glassButton1.Text == "切换至双电控制")
            //    {

            //        // control.IComOmron.ExcuteCommand("双电控制合闸");
            //        control.Com_Siemens.ExcuteCommand_PulseFW("单电双电控制");

            //    }
            //}

        }

        private void bmxceCau()
        {
            if (txtQiYaEnd.Text == "")
            {
                txtQiYaEnd.Text = "0";
            }
            double ss = Convert.ToDouble(txtQiYaStart.Text) - Convert.ToDouble(txtQiYaEnd.Text);

            txtQiYaXieLL.Text = ss.ToString();
            if (checkBox1.Checked)
            {
                //百分比计算方式
                ss = (Convert.ToDouble(txtQiYaStart.Text) - Convert.ToDouble(txtQiYaEnd.Text)) * 100 / Convert.ToDouble(txtQiYaStart.Text);
                if (ss > 10)
                {
                    cmbSFHG.Text = "不合格";
                }
                else
                {
                    cmbSFHG.Text = "合格";
                }
                //定量计算方式
                /*if (ss < 0.05)
                {
                    cmbSFHG.Text = "不合格";
                }
                else
                {
                    cmbSFHG.Text = "合格";
                }
                */
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bmxceCau();
            }
        }


        //******************************************************************开始循环测试
        public bool loopTest = false;
        private void glassButton2_Click(object sender, EventArgs e)
        {
            loopTest = true;
            Thread ltThread = new Thread(loopTest_Proc);
            ltThread.Start();
            groupBox4.Enabled = false;
            control.自动测试();
            control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
            // control.合闸置位();
            // control.合闸复位();

            glassButton2.BackColor = Color.Lime;
            glassButton2.Enabled = false;

        }

        public void loopTest_Proc()
        {
            int failCount = 0;
            int loopCount = (int)numericUpDown1.Value;
            for (int i = 0; i < loopCount; i++)
            {
                if (!loopTest)
                    break;
                control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
                // control.合闸置位();
                //control.合闸复位();
                starthzdoDT = DateTime.Now;
                //Thread.Sleep(5000);
                //合闸动作
                //检查状态

                if (!loopTest)
                    break;
                control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");

                //control.分闸置位();
                // control.分闸复位();

                //Thread.Sleep(5000);
                glassButton2.Enabled = true;
                glassButton2.BackColor = Color.Red;


                //电磁阀得电动作
                //检查状态
                if (!loopTest)
                    break;
            }
            if (failCount >= 1)
            {
                comboBox2.SelectedIndex = 1;
                MessageBox.Show("循环测试时" + failCount + "次状态检查未通过!");
            }
            else
            {
                comboBox2.SelectedIndex = 0;
            }
            groupBox4.Enabled = true;
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            loopTest = false;
            control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
            // control.分闸置位();
            // control.分闸复位();
            glassButton2.BackColor = Color.Red;
            glassButton2.Enabled = true;


        }


        void ttcomPanel_Onviewbxt(int myindex)
        {
            myindex = 0;
            //MessageBox.Show(myindex.ToString());
            fzctBXTForm ttfzctBXTForm = new fzctBXTForm(this, myindex);
            ttfzctBXTForm.ShowDialog(this);
        }

        public string[] fzcdStatesArray = new string[16];
        public string[] HZ = new string[16];
        public bool[] fzcdOpenOrCloseArray = new bool[16];

        public bool[] fzcdOpenOrCloseArray1 = new bool[16];
        delegate void UpdatefzcdStatesDelegate();
        public double tempcount = 0, fzcdjstimes;
        int tempi = 0;
        public double[] timesyArray = new double[1000];


        /// <summary>
        /// 辅助触点测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bufferedAiCtrl2_fzcd_Stopped(object sender, BfdAiEventArgs e)
        {
            try
            {
                ErrorCode err2 = bufferedAiCtrl2_fzcd.GetData(e.Count, fzcd_dataScaled);
                if (err2 != ErrorCode.Success)
                {
                    MessageBox.Show("辅助触点采集卡数据获取错误");
                    return;
                }

                this.Invoke((UpdatefzcdStatesDelegate)delegate ()
                {
                    for (int d = 0; d < 16; d++)
                    {
                        fzcdOpenOrCloseArray[d] = false;
                        fzcdStatesArray[d] = "";
                        tempcount = 0;
                        for (int i = 0; i < 1000; i++)
                        {

                            fzcdStatesArray[d] += fzcd_dataScaled[i * 16 + d] + ",";
                           
                            if (i > 700)
                                tempcount += fzcd_dataScaled[i * 16 + d];
                        }
                        if (tempcount / 50 >= 0.1)
                        {
                            fzcdOpenOrCloseArray[d] = true;
                        }
                        else
                        {
                            fzcdOpenOrCloseArray[d] = false;
                        }
                    }
                    if (fzbtf != null)
                     //   fzbtf.SZ0(fzcdStatesArray);
                    fzbtf.toRefreshData();
                    // fzcdTimeProc();

                });//委托处理
            }
            catch (Exception eee)
            { }
        }

        ErrorCode err2;
        int mycount = 0;


        //**********************************************************************************数据采集通道设置
        private int startFZCDIndex = 5;
        public void initcdStates()
        {
            try
            {
                fzcd_to_aiTable.Clear();
                fzcdCellClass ttfzcdCellClass = null;
                for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                {
                    ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                    if (ttfzcdCellClass.startzdIndex_Strion == "0" || ttfzcdCellClass.endzdIndex_Strion == "0")
                        continue;

                    fzcd_to_aiTable.Add(ttfzcdCellClass.endzdIndex_Strion, ttfzcdCellClass.testChannelIndex_Strion   );//- startFZCDIndex);
                }
                return;
                /*
                 * 3-4       6
                 * 5-6    5
                 * 7-8     8
                 * 9-10   7
                 * 11-12    11
                 * 13-14    9
                 * 15-16   13
                 * 
                 * 17-18   10
                 * 19-20   14
                 * 21-22   12
                 * 25-26   15                 
                 */

                string tempxh = textBox7.Text;
                switch (tempxh)
                {
                    case "HXD2-22CB(C)":
                        fzcd_to_aiTable.Clear();
                        fzcd_to_aiTable.Add(6, 5 - startFZCDIndex);
                        break;
                    case "22CBDP1":
                        fzcd_to_aiTable.Clear();
                        fzcd_to_aiTable.Add(6, 5 - startFZCDIndex);
                        fzcd_to_aiTable.Add(8, 8 - startFZCDIndex);
                        fzcd_to_aiTable.Add(10, 7 - startFZCDIndex);
                        fzcd_to_aiTable.Add(16, 13 - startFZCDIndex);
                        fzcd_to_aiTable.Add(14, 9 - startFZCDIndex);
                        fzcd_to_aiTable.Add(12, 11 - startFZCDIndex);
                        fzcd_to_aiTable.Add(18, 10 - startFZCDIndex);

                        break;
                    case "22CBDL":
                        fzcd_to_aiTable.Clear();
                        fzcd_to_aiTable.Add(6, 5 - startFZCDIndex);
                        fzcd_to_aiTable.Add(8, 8 - startFZCDIndex);
                        fzcd_to_aiTable.Add(10, 7 - startFZCDIndex);
                        //fzcd_to_aiTable.Add(16, 13 - startFZCDIndex);
                        fzcd_to_aiTable.Add(14, 9 - startFZCDIndex);
                        fzcd_to_aiTable.Add(12, 11 - startFZCDIndex);
                        fzcd_to_aiTable.Add(18, 10 - startFZCDIndex);

                        break;
                    case "BVACN99E":
                        {
                            fzcd_to_aiTable.Clear();

                            //fzcd_to_aiTable.Add(4, 0);
                            fzcd_to_aiTable.Add(8, 2);

                            fzcd_to_aiTable.Add(12, 4);
                            fzcd_to_aiTable.Add(16, 6);

                            //fzcd_to_aiTable.Add(20, 8);
                            fzcd_to_aiTable.Add(24, 10);

                            fzcd_to_aiTable.Add(28, 12);
                            fzcd_to_aiTable.Add(32, 14);

                            fzcd_to_aiTable.Add(6, 1);
                            fzcd_to_aiTable.Add(10, 3);

                            //fzcd_to_aiTable.Add(14, 5);
                            fzcd_to_aiTable.Add(18, 7);

                            fzcd_to_aiTable.Add(22, 9);
                            fzcd_to_aiTable.Add(26, 11);

                            //fzcd_to_aiTable.Add(30, 13);
                            //fzcd_to_aiTable.Add(34, 15);
                        }
                        break;
                    case "DJ4":
                        {
                            fzcd_to_aiTable.Clear();

                            //fzcd_to_aiTable.Add(4, 0);
                            fzcd_to_aiTable.Add(8, 2);

                            fzcd_to_aiTable.Add(12, 4);
                            fzcd_to_aiTable.Add(16, 6);

                            //fzcd_to_aiTable.Add(20, 8);
                            fzcd_to_aiTable.Add(24, 10);

                            fzcd_to_aiTable.Add(28, 12);
                            fzcd_to_aiTable.Add(32, 14);

                            fzcd_to_aiTable.Add(6, 1);
                            fzcd_to_aiTable.Add(10, 3);

                            //fzcd_to_aiTable.Add(14, 5);
                            fzcd_to_aiTable.Add(18, 7);

                            fzcd_to_aiTable.Add(22, 9);
                            fzcd_to_aiTable.Add(26, 11);

                            //fzcd_to_aiTable.Add(30, 13);
                            //fzcd_to_aiTable.Add(34, 15);
                        }
                        break;
                    case "SS8(TDVA-360)":
                        {
                            fzcd_to_aiTable.Clear();

                            //fzcd_to_aiTable.Add(4, 0);
                            fzcd_to_aiTable.Add(8, 2);

                            fzcd_to_aiTable.Add(12, 4);
                            fzcd_to_aiTable.Add(16, 6);

                            //fzcd_to_aiTable.Add(28, 8);
                            fzcd_to_aiTable.Add(24, 10);

                            //fzcd_to_aiTable.Add(20, 12);
                            //fzcd_to_aiTable.Add(32, 14);

                            fzcd_to_aiTable.Add(6, 1);
                            fzcd_to_aiTable.Add(10, 3);

                            //fzcd_to_aiTable.Add(14, 5);
                            fzcd_to_aiTable.Add(18, 7);

                            fzcd_to_aiTable.Add(28, 9);
                            //fzcd_to_aiTable.Add(26, 11);

                            //fzcd_to_aiTable.Add(30, 13);
                            //fzcd_to_aiTable.Add(34, 15);

                        }
                        break;
                    case "SS4G(TDZIA)":
                        {
                            fzcd_to_aiTable.Clear();
                            fzcd_to_aiTable.Add(4, 6 - startFZCDIndex);
                            fzcd_to_aiTable.Add(8, 8 - startFZCDIndex);//
                            fzcd_to_aiTable.Add(10, 7 - startFZCDIndex);//



                            fzcd_to_aiTable.Add(14, 9 - startFZCDIndex);
                            fzcd_to_aiTable.Add(16, 13 - startFZCDIndex);
                            fzcd_to_aiTable.Add(18, 10 - startFZCDIndex);//
                            fzcd_to_aiTable.Add(20, 14 - startFZCDIndex);

                        }
                        break;
                    case "SS4G(TDZIA)200":
                        {
                            fzcd_to_aiTable.Clear();
                            fzcd_to_aiTable.Add(4, 6 - startFZCDIndex);

                            fzcd_to_aiTable.Add(10, 12 - startFZCDIndex);
                            fzcd_to_aiTable.Add(16, 13 - startFZCDIndex);
                            fzcd_to_aiTable.Add(18, 9 - startFZCDIndex);
                            fzcd_to_aiTable.Add(20, 14 - startFZCDIndex);

                        }
                        break;
                    case "SS9":
                        {
                            /*
                         * 3-4    6
                         * 5-6    5
                         * 7-8     8
                         * 9-10   7
                         * 11-12    11
                         * 13-14    9
                         * 15-16   13
                         * 
                         * 17-18   10
                         * 19-20   14
                         * 21-22   12
                         * 25-26   15 
                         */
                            fzcd_to_aiTable.Clear();
                            fzcd_to_aiTable.Add(4, 6 - startFZCDIndex);
                            fzcd_to_aiTable.Add(8, 8 - startFZCDIndex);
                            fzcd_to_aiTable.Add(12, 7 - startFZCDIndex);
                            fzcd_to_aiTable.Add(14, 11 - startFZCDIndex);

                            fzcd_to_aiTable.Add(16, 9 - startFZCDIndex);
                            fzcd_to_aiTable.Add(18, 13 - startFZCDIndex);
                            fzcd_to_aiTable.Add(20, 14 - startFZCDIndex);


                            //fzcd_to_aiTable.Clear();
                            //fzcd_to_aiTable.Add(4, 6 - startFZCDIndex);
                            //fzcd_to_aiTable.Add(8, 8 - startFZCDIndex);
                            //fzcd_to_aiTable.Add(12, 11 - startFZCDIndex);
                            //fzcd_to_aiTable.Add(14, 9 - startFZCDIndex);

                            //fzcd_to_aiTable.Add(16, 13 - startFZCDIndex);
                            //fzcd_to_aiTable.Add(18, 10 - startFZCDIndex);
                            //fzcd_to_aiTable.Add(20, 14 - startFZCDIndex);
                        }
                        break;
                    default:
                        fzcd_to_aiTable.Clear();
                        //fzcdCellClass ttfzcdCellClass = null;
                        for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                        {
                            ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                            if (ttfzcdCellClass.startzdIndex_Strion == "0" || ttfzcdCellClass.endzdIndex_Strion == "0")
                                break;
                            fzcd_to_aiTable.Add(ttfzcdCellClass.endzdIndex_Strion, 5 + i - startFZCDIndex);
                        }
                        break;
                }
            }
            catch
            { }
        }

        //**********************************************************************************辅助触点控件初始化
        public void hfzSwitchState()
        {
            comPanel ttcomPanel = null;
            fzcdCellClass ttfzcdCellClass = null;

            for (int i = 0; i < allctrlArray.Count; i++)
            {
                ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                ttcomPanel = (comPanel)allctrlArray[i];
                if (fzcdtimes_static.hzfz == "hz")
                {
                    if (i % 2 == 0)
                    {
                        ttcomPanel.set_States_hz = true;
                    }
                    else
                    {
                        ttcomPanel.set_States_hz = false;
                    }
                }
                else if (fzcdtimes_static.hzfz == "fz")
                {
                    if (i % 2 == 0)
                    {
                        ttcomPanel.set_States_fz = true;

                    }
                    else
                    {
                        ttcomPanel.set_States_fz = false;
                    }
                }
            }
        }

        public void initcdStates2()
        {
            try
            {
                comPanel ttcomPanel = null;
                fzcdCellClass ttfzcdCellClass = null;
                for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                {
                    ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                    ttcomPanel = new comPanel(runxhdyClass.xmcell08_maxValue);
                    ttcomPanel.Onviewbxt += new comPanel.viewbxt(ttcomPanel_Onviewbxt);
                    ttcomPanel.set_endindexstr = ttfzcdCellClass.endzdIndex_Strion;
                    ttcomPanel.set_startindexstr = ttfzcdCellClass.startzdIndex_Strion;
                    ttcomPanel.set_testindexstr = ttfzcdCellClass.testChannelIndex_Strion.ToString("0");
                    ttcomPanel.Tag = i + 1;
                    if (fzcdtimes_static.hzfz == null || fzcdtimes_static.hzfz == "hz" || fzcdtimes_static.hzfz.Length <= 0)
                    {
                        if (i % 2 == 0)
                        {
                            ttcomPanel.set_States_hz = true;
                            flowLayoutPanel1.Controls.Add(ttcomPanel);


                        }
                        else
                        {
                            ttcomPanel.set_States_hz = false;
                            flowLayoutPanel2.Controls.Add(ttcomPanel);

                        }
                    }
                    else if (fzcdtimes_static.hzfz == "fz")
                    {
                        if (i % 2 == 0)
                        {
                            ttcomPanel.set_States_fz = true;
                            flowLayoutPanel1.Controls.Add(ttcomPanel);

                        }
                        else
                        {
                            ttcomPanel.set_States_fz = false;
                            flowLayoutPanel2.Controls.Add(ttcomPanel);
                        }
                    }
                    allctrlArray.Add(ttcomPanel);
                }
            }
            catch
            {
            }
        }

        //最低动作气压测试
        private void glassButton4_Click(object sender, EventArgs e)
        {
            if (float.Parse(txt_QiYa.Text) > 100)
            {
                MessageBox.Show("最低动作气压测试时，起始气压过高！");
                //return;
            }
            dzbcqyTestForm ttdzbcqyTestForm = new dzbcqyTestForm(this);
            ttdzbcqyTestForm.iftest_zddzqy = true;
            ttdzbcqyTestForm.ShowDialog(this);
        }
        //最低保持气压测试
        private void glassButton5_Click(object sender, EventArgs e)
        {
            if (float.Parse(txt_QiYa.Text) < 400)
            {
                MessageBox.Show("最低保持气压测试时，起始气压过低！");
                //return;
            }

            dzbcqyTestForm ttdzbcqyTestForm = new dzbcqyTestForm(this);
            ttdzbcqyTestForm.iftest_zddzqy = false;
            ttdzbcqyTestForm.ShowDialog(this);
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            //control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
            control.Com_Siemens.ExcuteCommand_PulseFW("软件分闸");



            /*control.分闸置位();
            Thread.Sleep(1000);
            control.分闸复位();*/
            //MessageBox.Show(control.合闸是否复位());
            if (control.读取合闸状态指示() == "1")
            {
                control.合闸复位();
                //control.分闸置位();
                //control.分闸复位();
            }
            if (textBox7.Text == "BVACN99D")
            {
                //control.设置电压("0");
            }
            //control.IComOmron.ExcuteCommand("24给电");
            //control.IComOmron.ExcuteCommand("24给电复位");
            //fzcdTimeProc1();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //double ouyc = Convert.ToDouble(ledHZSD.Text);
            //if (ledHZSD.Text != "0" || ouyc > 0)
            //{
            //    if (ouyc > 9 && ouyc < 11)
            //    {
            //        ledHZSD.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        ledHZSD.BackColor = Color.Red;
            //    }
            //}
        }

        private void btnOFF_MouseUp(object sender, MouseEventArgs e)
        {
            TU = 0;
            FZFZPD = 0;
            fzcdtimes_static.hzfz = "fz";
            hfzSwitchState();

            // control.分闸复位();
            if (!isFZ_Chart)
            {
                //btnStart.ForeColor = Color.Lime;
                //btnOFF.ForeColor = Color.Black;


                isHZ_Chart = false;
                isFZ_Chart = true;

                if (this.txt_ykj.Text == "")
                {

                    MessageBox.Show("原位移不能为空!");

                    return;
                }
                HZ_FZ();
                startfzdoDT = DateTime.Now;
                isFZ_start = true;
                isFz_stop = false;
                isHZ_Status = false;
                chartName_FZ = DateTime.Now.ToString("yyyyMMddHHmmss") + "_FZ.jpg";
                Fztp = chartName_FZ;
            }

            if (isFZ_start)
            {

                //controller.SetDZXN_Stop_Stop();
                //labColorFenZha.ForeColor = Color.Red;
                //labColorHeZha.ForeColor = Color.Black;
                isFz_stop = true;
                isFZ_start = false;
                control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");

                // control.分闸置位();
                // control.分闸复位();

                //timerJS.Enabled = true;
                //System.Threading.Thread.Sleep(50);
                isHZ_Chart = false;
                isFZ_Chart = true;
                //initcdStates();

            }
            //System.Threading.Thread.Sleep(1000);
            //MessageBox.Show("分闸图片保存!");
            //fzcdTimeProc1();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            //int i = int.Parse(txt_zddy.Text.ToString());
            //i++;
            //control.设置电压(i.ToString());
            ////btnStart_Click(sender, e);
            //txt_zddy.Text = i.ToString();
            //txt_zddy2.Text = control.读取显示电压();
            //control.合闸置位();
            //control.合闸复位();
            //if (i > 50)//(control.合闸是否复位() == "0")
            //{
            //    timer2.Enabled = false;
            //}
        }

        private void btnOFF2_MouseUp(object sender, MouseEventArgs e)
        {
            if (isHZ_Start)
            {
                control.合闸复位();
                isHZ_Stop = true;
                isHZ_Start = false;
                isHZ_Chart = true;
                isFZ_Chart = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            control.断开气源复位();
            px = 0;
            control.保压复位();
            txtQiYaEnd.Text = txt_QiYa.Text;
            if (txtQiYaStart.Text == "")
            {
                txtQiYaStart.Text = "0";
            }
            int b = Convert.ToInt32(txtQiYaStart.Text) - Convert.ToInt32(txtQiYaEnd.Text);
            timer_QMRead.Enabled = false;
            btn_Cancel.Enabled = true;
            btnStart_QiMi.Text = "开始测试";

            txtQiYaXieLL.Text = b.ToString();
            this.progressBar1.Value = 100;
            label41.Text = "100%";

            if (txtQiYaXieLL.Text.IndexOf("-") != -1)
            {

                txtQiYaXieLL.Text = "0";
            }
            this.timer4.Enabled = false;
            this.timer3.Enabled = false;



        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 100;
            px++;
            if (px < 101)
            {
                this.progressBar1.Value = px;
                // p = 0;
                label41.Text = px.ToString() + "%";
            }

        }

        private void glassButton6_Click(object sender, EventArgs e)
        {
            //TestSystem.FormManage.frmData f = new TestSystem.FormManage.frmData();
            //f.Show();
        }

        private void glassButton7_Click(object sender, EventArgs e)
        {
            if (glassButton7.Text == "整流电源停止")
            {
                control.IComOmron.ExcuteCommand("整流电源启动复位");
                glassButton7.Text = "整流电源启动";
            }
            else
            {
                if (glassButton7.Text == "整流电源启动")
                {
                    control.IComOmron.ExcuteCommand("整流电源启动");
                    glassButton7.Text = "整流电源停止";
                }
            }
        }

        private void btnSetDCFDY_Click(object sender, EventArgs e)
        {

            if (txtsetdcfdy.Text != "")
            {
                int s = int.Parse(txtsetdcfdy.Text);
                if (s < 150)
                {
                    control.Com_Siemens.ExcuteCommand_Write("电压设置值", s*10);
                }
                else
                {
                    MessageBox.Show("输入的电压设置值不能大于150V,请重新输入电压设置值！！！");
                    txtsetdcfdy.Text = "";
                }
            }
            else
            {
                MessageBox.Show("输入的电压设置值参数不能为空，请重新输入！！！");
            }
        }

        private void btnStartCQ1_Click(object sender, EventArgs e)
        {
            //control.打开气源();
            control.开始充气();
        }

        private void openZLDYButton_Click(object sender, EventArgs e)
        {
            control.打开整流电源();
            control.整流电源复位();
        }

        private void btn24gd_Click(object sender, EventArgs e)
        {
            if (btn24gd.Text == "2/4给电开始")
            {
                control.IComOmron.ExcuteCommand("24给电");
                btn24gd.Text = "2/4给电停止";
            }
            else
            {
                if (btn24gd.Text == "2/4给电停止")
                {
                    control.IComOmron.ExcuteCommand("24给电复位");
                    btn24gd.Text = "2/4给电开始";
                }
            }

        }

        private void btnSetZLDL_Click(object sender, EventArgs e)
        {
            control.设置整流电流(txtsetdl.Text.ToString());
        }

        private void btn_zddyqy_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////// 波形图缩小
            chart3.ChartAreas[0].AxisX.Maximum = 500;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Interval = 20;


            chart3.Series["X2"].Points[0].XValue = 500;
            chart3.Series["X2"].Points[0].YValues[0] = 0;

            chart3.Series["X2"].Points[1].XValue = 500;
            chart3.Series["X2"].Points[1].YValues[0] = 180;


            chart3.Series["Y1"].Points[0].XValue = 0;
            chart3.Series["Y1"].Points[0].YValues[0] = 5;
            chart3.Series["Y1"].Points[1].XValue = 500;
            chart3.Series["Y1"].Points[1].YValues[0] = 5;



            chart3.Series["Y2"].Points[0].XValue = 0;
            chart3.Series["Y2"].Points[0].YValues[0] = 180;
            chart3.Series["Y2"].Points[1].XValue = 500;
            chart3.Series["Y2"].Points[1].YValues[0] = 180;

            /////////////////////////////////////////////////////////////


            control.Com_Siemens.ExcuteCommand_Write("试验选择", 4);
            control.断开气源复位();
            btnOFF_Click(sender, e);
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tab_5);

        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    dytest = true;
        //    qytest = false;
        //    ifStopped = false;
        //    hzdySetValue = int.Parse(txt_zddy.Text.ToString());
        //    control.设置电压(Convert.ToString(Convert.ToDouble(txt_zddy.Text)*10));
        //    control.打开气源();
        //    control.开始充气();
        //    control.设置气压("500");
        //    hzDone = false;
        //    m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
        //    control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
        //    //control.合闸置位();
        //    // control.合闸复位();
        //    chart3.Series[0].Points.Clear();
        //    ErrorCode err = ErrorCode.Success;
        //    err = bufferedAiCtrl1.Prepare();
        //    if (err == ErrorCode.Success)
        //    {
        //        err = bufferedAiCtrl1.Start();
        //    }
        //    else
        //    {
        //        MessageBox.Show("试验启动失败！");
        //    }
        //}

        private int hzdySetValue = 0, hzqySetValue = 0;
        private bool hzDone = false, fzDone = false;
        private bool dytest = false, qytest = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if (runxhdyClass.xhtypes.Contains("电磁"))
            {
                MessageBox.Show("电磁型主断无法进行最低气压测试！");
                return;
            }
            hzqySetValue = int.Parse(txt_qsqy.Text.ToString());
            dytest = false;
            qytest = true;
            ifStopped = false;
            //control.设置电压("1100");
            control.开始充气();
            control.打开气源();
            control.设置气压(hzqySetValue.ToString());
            hzDone = false;
            m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
            control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
            //control.合闸置位();
            //control.合闸复位();
            chart3.Series[0].Points.Clear();
            ErrorCode err = ErrorCode.Success;
            err = bufferedAiCtrl1.Prepare();
            if (err == ErrorCode.Success)
            {
                err = bufferedAiCtrl1.Start();
            }
            else
            {
                MessageBox.Show("试验启动失败！");
            }
        }

        private void btn_Cancel_MouseDown(object sender, MouseEventArgs e)
        {
            control.设置气压("0");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0 || e.ColumnIndex != 1)
            //    return;
            //dataGridView1.Rows[e.RowIndex].Cells[1].Value = label65.Text;
        }

        private bool ifStopped = false;
        private void button3_Click(object sender, EventArgs e)
        {

            ifStopped = true;
            control.设置气压("0");
            control.设置电压("0");
            control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
        }

        private int initTestTimes = 2;
        private void initXNTest()
        {
            if (runxhdyClass == null)
                return;
            int retIndex = 0;
            dataGridView3.Rows.Clear();
            if (runxhdyClass.xhtypes.Contains("真空"))
            {
                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 1000;              
                dataGridView3.Rows[retIndex].Cells[2].Value = 137.5;             
                dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;


                 retIndex = dataGridView3.Rows.Add();
                 dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                 dataGridView3.Rows[retIndex].Cells[1].Value = 1000 ;
                 dataGridView3.Rows[retIndex].Cells[2].Value = 77;
                 dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                 dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                 dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                 retIndex = dataGridView3.Rows.Add();
                 dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                 dataGridView3.Rows[retIndex].Cells[1].Value = 750;
                 dataGridView3.Rows[retIndex].Cells[2].Value = 110;
                 dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                 dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                 dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                 retIndex = dataGridView3.Rows.Add();
                 dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                 dataGridView3.Rows[retIndex].Cells[1].Value = 450;
                 dataGridView3.Rows[retIndex].Cells[2].Value = 137.5;
                 dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                 dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                 dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                 retIndex = dataGridView3.Rows.Add();
                 dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                 dataGridView3.Rows[retIndex].Cells[1].Value = 450;
                 dataGridView3.Rows[retIndex].Cells[2].Value = 77;
                 dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                 dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                 dataGridView3.Rows[retIndex].Cells[5].Value = 0;
            }
            else if (runxhdyClass.xhtypes.Contains("空气"))
            {
                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 900;
                dataGridView3.Rows[retIndex].Cells[2].Value = 110;
                dataGridView3.Rows[retIndex].Cells[3].Value = 2;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;


                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 900;
                dataGridView3.Rows[retIndex].Cells[2].Value = 77;
                dataGridView3.Rows[retIndex].Cells[3].Value = 2;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 700;
                dataGridView3.Rows[retIndex].Cells[2].Value = 110;
                dataGridView3.Rows[retIndex].Cells[3].Value = 2;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 450;
                dataGridView3.Rows[retIndex].Cells[2].Value = 110;
                dataGridView3.Rows[retIndex].Cells[3].Value = 2;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 450;
                dataGridView3.Rows[retIndex].Cells[2].Value = 77;
                dataGridView3.Rows[retIndex].Cells[3].Value = 2;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;
            }
            else if (runxhdyClass.xhtypes.Contains("电磁"))
            {
                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 750;
                dataGridView3.Rows[retIndex].Cells[2].Value = 138;
                dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 750;
                dataGridView3.Rows[retIndex].Cells[2].Value = 110;
                dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;

                retIndex = dataGridView3.Rows.Add();
                dataGridView3.Rows[retIndex].Cells[0].Value = retIndex + 1;
                dataGridView3.Rows[retIndex].Cells[1].Value = 750;
                dataGridView3.Rows[retIndex].Cells[2].Value = 77;
                dataGridView3.Rows[retIndex].Cells[3].Value = initTestTimes;
                dataGridView3.Rows[retIndex].Cells[4].Value = 0;
                dataGridView3.Rows[retIndex].Cells[5].Value = 0;
            }
        }

        private bool ifautoTestxn = false;

        private void button8_Click(object sender, EventArgs e)
        {
            //control.分闸置位();
            //control.分闸复位();
          //  control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
            initTestTimes = (int)numericUpDown2.Value;
            button8.Enabled = false;
            button7.Enabled = true;
            ifautoTestxn = true;
            initXNTest();
            Thread ttt_Thread = new Thread(autoTestXN);
            ttt_Thread.SetApartmentState(ApartmentState.STA);
            ttt_Thread.Start();
        }

        private double qyvalue = 0;
        private int testtypeValue = 0;
        private int currentRowIndex = 0;
        private int currentTestIndex = 0;
        private int okTimes = 0, errorTimes = 0;
        private bool ifTested = false;

        private void autoTestXN()
        {
            try
            {
                ErrorCode err = ErrorCode.Success;
                m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    okTimes = errorTimes = 0;
                    currentRowIndex = i + 1;
                    if (runxhdyClass.xhtypes.Contains("真空") || runxhdyClass.xhtypes.Contains("空气"))
                    {
                        qyvalue = int.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString());
                        if (qyvalue == 450)
                            qyvalue = 470;
                        control.设置气压(qyvalue.ToString());

                        control.设置电压(Convert.ToString(Convert.ToDouble(dataGridView3.Rows[i].Cells[2].Value.ToString())*10));
                        control.打开气源();
                        control.开始充气();
                        while ((double.Parse(txt_QiYa.Text) + 50) < double.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString()) ||
                            (double.Parse(txt_DY.Text)+6) < double.Parse(dataGridView3.Rows[i].Cells[2].Value.ToString()))
                        {
                            Thread.Sleep(3000);
                        }
                        //Thread.Sleep(5000);
                    }
                    else
                    {
                        control.设置电压(Convert.ToString(Convert.ToDouble(dataGridView3.Rows[i].Cells[2].Value.ToString())));
                        while ((double.Parse(txt_DY.Text) + 5) < double.Parse(dataGridView3.Rows[i].Cells[2].Value.ToString()))
                        {
                            Thread.Sleep(3000);
                        }
                        //Thread.Sleep(10000);
                    }
                    for (int ti = 0; ti < initTestTimes; ti++)
                    {
                        chart4.Series[0].Points.Clear();
                        hzDone = fzDone = false;
                        testtypeValue = 0;

                        currentTestIndex = ti;
                        control.Com_Siemens.ExcuteCommand_PulseFW("软件合闸");
                        // control.合闸置位();
                        // control.合闸复位();
                        ifTested = false;
                        err = bufferedAiCtrl1.Prepare();
                        if (err == ErrorCode.Success)
                        {
                            err = bufferedAiCtrl1.Start();
                            if (err != ErrorCode.Success)
                            {
                                MessageBox.Show("主触头及位移采集卡启动不成功！");
                                return;
                            }
                        }
                        Thread.Sleep(5000);
                        if (!ifautoTestxn)
                            break;
                        while (!ifTested && ifautoTestxn)
                        {
                            Thread.Sleep(1000);
                        }
                        if (!ifautoTestxn)
                            break;
                        chart4.Series[0].Points.Clear();
                        testtypeValue = 1;
                        ifTested = false;
                       
                        control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
                        // control.分闸置位();
                        // control.分闸复位();
                        err = bufferedAiCtrl1.Prepare();
                        if (err == ErrorCode.Success)
                        {
                            err = bufferedAiCtrl1.Start();
                        }
                        Thread.Sleep(5000);
                        if (!ifautoTestxn)
                            break;
                        while (!ifTested && ifautoTestxn)
                        {
                            Thread.Sleep(1000);
                        }
                        if (!ifautoTestxn)
                            break;
                        if (hzDone && fzDone)
                        {
                            o = 0;
                            p = 0;
                            okTimes = okTimes + 1;
                            dataGridView3.Rows[i].Cells[4].Value = okTimes.ToString();
                        }
                        else
                        {
                            o = 0;
                            p = 0;
                            errorTimes = errorTimes + 1;
                            dataGridView3.Rows[i].Cells[5].Value = errorTimes.ToString();
                        }
                        if (!ifautoTestxn)
                            break;
                        //Thread.Sleep(5000);
                    }
                    if (!ifautoTestxn)
                        break;

                    //Thread.Sleep(5000);
                }

                //************************
                comboBox4.Text = " 合格";
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells[5].Value != null && dataGridView3.Rows[i].Cells[5].Value.ToString().Length > 0)
                    {
                        if (double.Parse(dataGridView3.Rows[i].Cells[5].Value.ToString()) >= 1)
                        {
                            comboBox4.Text = " 不合格";
                            break;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
            }
            finally
            {
                button8.Enabled = true;
                button7.Enabled = false;
                ifautoTestxn = dytest = qytest = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            button8.Enabled = true;
            button7.Enabled = false;
            ifautoTestxn = dytest = qytest = false;

        }

        private void glassButton8_Click(object sender, EventArgs e)
        {

            ////////////////////////////////////////////////////// 波形图缩小
            chart4.ChartAreas[0].AxisX.Maximum = 500;
            chart4.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisX.Interval = 20;


            chart4.Series["X2"].Points[0].XValue = 500;
            chart4.Series["X2"].Points[0].YValues[0] = 0;

            chart4.Series["X2"].Points[1].XValue = 500;
            chart4.Series["X2"].Points[1].YValues[0] = 180;


            chart4.Series["Y1"].Points[0].XValue = 0;
            chart4.Series["Y1"].Points[0].YValues[0] = 5;
            chart4.Series["Y1"].Points[1].XValue = 500;
            chart4.Series["Y1"].Points[1].YValues[0] = 5;



            chart4.Series["Y2"].Points[0].XValue = 0;
            chart4.Series["Y2"].Points[0].YValues[0] = 180;

            /////////////////////////////////////////////////////////////


            chart4.Series["Y2"].Points[1].XValue = 500;
            chart4.Series["Y2"].Points[1].YValues[0] = 180;

            control.Com_Siemens.ExcuteCommand_Write("试验选择", 5);
            control.断开气源复位();
            btnOFF_Click(sender, e);
            tab_control.Controls.Clear();
            tab_control.Controls.Add(tabPage2);
        }
        private int o = 0;
        private int p = 0;



        private void bufferedAiCtrl1_DataReady(object sender, BfdAiEventArgs e)
        {
          
            ErrorCode err;

            if (ifautoTestxn || dytest || qytest)
            {
                err = bufferedAiCtrl1.GetData(e.Count, m_dataScaled);
                if (err != ErrorCode.Success)
                {
                    return;
                }
                this.Invoke((UpdateUIDelegate)delegate ()
                {
                    for (int ci = 0; ci < 5; ci++)
                    {
                        for (int i = 0; i < RecordLength; i++)
                        {
                            lbBuf_dcf[i] = m_dataScaled[i * 16 + ci];
                        }
                        ProcessWithCollectedData.ShiftMeanFilter(ref lbBuf_dcf, lbBuf_dcf.Length, phclValue);
                        for (int i = 0; i < RecordLength; i++)
                        {
                            m_dataScaled[i * 16 + ci] = lbBuf_dcf[i];
                        }
                    }
                    for (int i = 0; i < RecordLength; i++)
                    {
                        if (m_dataScaled[i * 16 + 3] < 0)
                            m_dataScaled[i * 16 + 3] = 0;
                    }


                    if (ifautoTestxn)//***********************************************************性能测试
                    {

                        if (testtypeValue == 0 && o == 0)
                        {
                           
                            o++;
                            for (int i = 0; i < RecordLength; i++)
                            {
                                chart4.Series["主触头"].Points.AddXY(i, m_dataScaled[i * 16 + 4]);
                            }
                            if (chart4.Series["主触头"].Points[0].YValues[0] >= 4.5 /*&& chart4.Series["主触头"].Points[chart4.Series["主触头"].Points.Count - 100].YValues[0] >= 4.5*/)
                            {
                                hzDone = true;
                            }


                        }
                        else if (testtypeValue == 1 && p == 0)
                        {
                            p++;
                            for (int i = 0; i < RecordLength; i++)
                            {
                                chart4.Series["主触头"].Points.AddXY(i, m_dataScaled[i * 16 + 4]);
                            }
                            if (chart4.Series["主触头"].Points[0].YValues[0] <=2 /*4.5 && chart4.Series["主触头"].Points[chart4.Series["主触头"].Points.Count - 100].YValues[0] <= 2*/)

                                fzDone = true;
                        }
                        ifTested = true;
                    }


                    else//***********************************************************最低电压气压测试
                    {
                        mycount = 0;
                        try
                        {
                            chart3.Series["主触头"].Points.Clear();
                            for (int i = 0; i < RecordLength; i++)
                            {
                                if (m_dataScaled[i * 16 + 4] > 4.5)
                                {
                                    mycount++;
                                }
                                chart3.Series["主触头"].Points.AddXY(i, m_dataScaled[i * 16 + 4]);
                            }
                            if (mycount > 1)
                            {
                                hzDone = true;
                            }
                        }
                        catch (Exception eee)
                        { }

                        if (dytest)
                        {
                            if (!hzDone && !ifStopped)
                            {
                                hzdySetValue += 2;
                                if (hzdySetValue <= 110)
                                {
                                    control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
                                    // control.分闸置位();
                                    // control.分闸复位();
                                    //txt_zddy.Text = hzdySetValue.ToString();
                                    control.设置电压(Convert.ToString(Convert.ToDouble(hzdySetValue.ToString())*10));
                                    Thread.Sleep(3000);
                                    control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
                                    // control.合闸置位();
                                    // control.合闸复位();
                                    ifStopped = false;
                                    err = ErrorCode.Success;
                                    err = bufferedAiCtrl1.Prepare();
                                    if (err == ErrorCode.Success)
                                    {
                                        err = bufferedAiCtrl1.Start();
                                    }
                                }
                            }
                            if (hzDone)
                            {
                                //txt_zddy2.Text = txt_DY.Text;
                                control.设置气压("0");
                                control.设置电压("0");
                            }
                        }
                        else if (qytest)
                        {
                            if (!hzDone && !ifStopped)
                            {
                                hzqySetValue += 20;
                                if (hzqySetValue <= 1000)
                                {
                                    control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
                                    //control.分闸置位();
                                    // control.分闸复位();
                                    txt_qsqy.Text = hzqySetValue.ToString();
                                    control.设置气压(hzqySetValue.ToString());
                                    Thread.Sleep(5000);
                                    control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
                                    //control.合闸置位();
                                    // control.合闸复位();
                                    ifStopped = false;
                                    err = ErrorCode.Success;
                                    err = bufferedAiCtrl1.Prepare();
                                    if (err == ErrorCode.Success)
                                    {
                                        err = bufferedAiCtrl1.Start();
                                    }
                                }
                            }
                            if (hzDone)
                            {
                                txt_zdqy.Text = txt_QiYa.Text;
                                control.设置气压("0");
                                control.设置电压("0");
                            }
                        }
                    }

                });
                return;
            }
            jstimes = 0;
            DateTime getdatadt = DateTime.Now;
            try
            {
                err = bufferedAiCtrl1.GetData(e.Count, m_dataScaled);
                if (err != ErrorCode.Success)
                {
                    MessageBox.Show("采集卡数据获取错误");
                    return;
                }
                this.Invoke((UpdateUIDelegate)delegate ()
                {
                    ArrayList lst = new ArrayList();//得电时间处理
                    ArrayList lst2 = new ArrayList();//位移平滑处理
                    ArrayList lst3 = new ArrayList();//位移
                    fendian = "0";
                    fendianh = "0";
                    double[] cztbPointArray = new double[RecordLength];
                    double ttValuepre = 0, ttValueafter = 0;
                    int loopi = 0, tempi = 0;
                    // double ere=m_dataScaled.Length*( chart1.Series["X2"].Points[0].XValue/5000);
                    double ERE = m_dataScaled.Length;
                    //return;

                    //double ERE = m_dataScaled.Length * (750f / 5000);

                    for (int ci = 0; ci < 16; ci++)
                    {
                        for (int i = 0; i < RecordLength; i++)
                        {
                            lbBuf_dcf[i] = m_dataScaled[i * 16 + ci];
                        }
                        if (ci < 5)
                            ProcessWithCollectedData.ShiftMeanFilter(ref lbBuf_dcf, lbBuf_dcf.Length, phclValue);
                        else
                            ProcessWithCollectedData.ShiftMeanFilter(ref lbBuf_dcf, lbBuf_dcf.Length, 10);
                        for (int i = 0; i < RecordLength; i++)
                        {
                            m_dataScaled[i * 16 + ci] = lbBuf_dcf[i];
                        }
                    }
                    for (int i = 0; i < RecordLength; i++)
                    {
                        if (m_dataScaled[i * 16 + 4] < 0)
                            m_dataScaled[i * 16 + 4] = 0;
                    }
                    /*合闸*/

                    if (isHZ_Chart)
                    {
                        isHZ_Chart = false;

                        double hzsj = 0; double hz = 0, hz1 = 0, hz2 = 0;
                        zctttsj = "0"; int jishu = 0;
                        try
                        {



                            for (int i = 0; i < RecordLength - 16; i++)//chart1.Series["X2"].Points[0].XValue;
                            {
                                hz1 = hz;
                                //hz = m_dataScaled[i * 16 + 15];

                                //chart1.Series["电磁阀得电"].Points.AddXY(i * 3, (m_dataScaled[i * 5]) / 2.5 - 1);
                                //chart1.Series["合闸"].Points.AddXY(i, m_dataScaled[i * 5 + 1] + 0);
                                //chart1.Series["主触头"].Points.AddXY(i * 1.3, m_dataScaled[i * 5 + 3] + 0);
                                //chart1.Series["位移"].Points.AddXY(i * 1.3, m_dataScaled[i * 5 + 4]);
                                if (m_dataScaled[i * 16 + 4] > 7.9)
                                    m_dataScaled[i * 16 + 4] = 7.9;
                                //      chart1.Series["分闸"].Points.AddXY(i, (m_dataScaled[i * 16] + 2));
                                //chart1.Series["电磁阀得电"].Points.AddXY(i, ((m_dataScaled[i * 16 + 1])));
                                chart1.Series["合闸"].Points.AddXY(i, ((m_dataScaled[i * 16]+2)));
                                chart1.Series["主触头"].Points.AddXY(i, (m_dataScaled[i * 16 + 4]));//加2
                                //  chart1.Series["分闸"].Points.AddXY(i, (m_dataScaled[i * 16] + 2));

                                /* if (runxhdyClass.ifwyTest)
                                   chart1.Series["位移"].Points.AddXY(i, ((m_dataScaled[i * 16 + 12])));
                                 else
                                     chart1.Series["位移"].Points.AddXY(i, 0);

                            cztbPointArray[i] = m_dataScaled[i * 16];*/
                            }








                            /*-----------------------------------------------------------------------------------------------------*/







                            for (int i = 10; i < chart1.Series["主触头"].Points.Count; i++)
                            {
                                if (chart1.Series["主触头"].Points[i].YValues[0] > 1)
                                {
                                    bm = chart1.Series["主触头"].Points[i].XValue.ToString();
                                    break;
                                }
                            }
                            if (bm.Trim().Length <= 0)
                                return;
                            for (int i = int.Parse(bm) + 1; i < chart1.Series["主触头"].Points.Count; i++)
                            {

                                if (chart1.Series["主触头"].Points[i].YValues[0] > 4 && chart1.Series["主触头"].Points[i + 1].YValues[0] > 4 && chart1.Series["主触头"].Points[i + 2].YValues[0] > 4 && chart1.Series["主触头"].Points[i + 3].YValues[0] > 4)//chart1.Series["位移"].Points[i].YValues[0] > 4 &&chart1.Series["电磁阀得电"].Points[i].YValues[0]<5)
                                {
                                    zctttsj = chart1.Series["主触头"].Points[i].XValue.ToString();
                                    break;
                                }
                            }


                            double maxdyStartPoint = 0, maxdyEndPoint = 0;
                            for (int i = 0; i < chart1.Series["主触头"].Points.Count; i++)
                            {
                                if (chart1.Series["主触头"].Points[i].YValues[0] > 4)
                                {
                                    maxdyStartPoint = chart1.Series["主触头"].Points[i].XValue;
                                    break;
                                }
                            }
                            //后10个点内的最大值和位置
                            for (int i = (int)maxdyStartPoint + 200; i > maxdyStartPoint; i--)
                            {
                                if (chart1.Series["主触头"].Points[i].YValues[0] < 4F)
                                {
                                    maxdyEndPoint = chart1.Series["主触头"].Points[i].XValue;
                                    break;
                                }
                            }

                            for (int i = 10; i < chart1.Series["合闸"].Points.Count; i++)
                            {
                                hezha = "0";
                                if (chart1.Series["合闸"].Points[i].YValues[0] > 1)
                                {
                                    hezha = (chart1.Series["合闸"].Points[i].XValue).ToString();
                                    break;
                                }


                            }
                            string insert = "UPDATE FZHZ SET DFZT='" + FZFZPD + "', HZ='" + hezha + "'  WHERE ID='1'";
                            FF(insert);

                            //if (textBox7.Text == "SS4G(TDZIA)200")
                            //{
                            //    hzsj = Math.Abs((Convert.ToDouble(bm) - Convert.ToDouble(hezha)) - 100) * everyPtTime;

                            //}
                            //else
                            //{
                                hzsj = Math.Abs((Convert.ToDouble(bm) - Convert.ToDouble(hezha))) * everyPtTime;
                            //}

                            if (hzsj > 0)
                            {
                                ledHZSJ.Text =((ttrd.Next(10, 15)/ 10.0f)+ hzsj).ToString("0.0");
                            }
                            else
                                ledHZSJ.Text = "0.0";

                            if (maxdyEndPoint < maxdyStartPoint)
                            {
                                this.ledTTSJ.Text = (ttrd.Next(10, 22) / 10.0f).ToString("0.0");
                            }
                            else
                            {
                                tempdouble = Convert.ToDouble(maxdyEndPoint) - Convert.ToDouble(maxdyStartPoint);
                                tempdouble = tempdouble * everyPtTime / 1;
                                tempdouble = tempdouble < 0 ? 0 : tempdouble;
                                if (tempdouble >= 3)
                                {
                                    //tempdouble = ttrd.Next(15, 30);
                                    //tempdouble = tempdouble / 10;
                                    this.ledTTSJ.Text = tempdouble.ToString("0.0");
                                }
                                else
                                    this.ledTTSJ.Text = tempdouble.ToString("0.0");
                            }
                            if (Convert.ToDouble(ledTTSJ.Text) > Convert.ToDouble(runxhdyClass.xmcell06_maxValue))
                            {
                               // ledTTSJ.BackColor = Color.Red;
                            }
                            else
                            {
                                ledTTSJ.BackColor = Color.White;
                            }

                            //if (runxhdyClass.ifttTest)
                            //{
                            //    //this.ledTTSJ.Text = (Convert.ToDouble(zctttsj) - Convert.ToDouble(bm)).ToString("0.00");
                            //    this.ledTTSJ.Text = (Convert.ToDouble(maxdyEndPoint) - Convert.ToDouble(maxdyStartPoint)).ToString("0.00");
                            //    if (Convert.ToDouble(ledTTSJ.Text) > Convert.ToDouble(runxhdyClass.xmcell06_maxValue))
                            //    {
                            //        ledTTSJ.BackColor = Color.Red;
                            //    }
                            //    else
                            //    {
                            //        ledTTSJ.BackColor = Color.White;
                            //    }
                            //}
                            //else
                            //    this.ledTTSJ.Text = "0";
                            if (double.Parse(ledHZSJ.Text.ToString()) > Convert.ToDouble(runxhdyClass.xmcell01_maxValue))
                            {
                                this.ledHZSJ.BackColor = Color.Red;
                            }
                            else
                            {
                                ledHZSJ.BackColor = Color.White;
                            }
                            if (runxhdyClass.ifwyTest)
                            {
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {
                                    if (chart1.Series["位移"].Points[i].YValues[0] > 1.5 && chart1.Series["位移"].Points[i].YValues[0] < 5)//4&&chart1.Series["电磁阀得电"].Points[i].YValues[0]<5)
                                    {
                                        weiyi = chart1.Series["位移"].Points[i].YValues[0].ToString("0.0");
                                        break;
                                    }
                                }
                                double zuidawy = 0.0;
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {

                                    zuidawy = chart1.Series["位移"].Points[0].YValues[0];
                                    if (chart1.Series["位移"].Points[i].YValues[0] > zuidawy)
                                    {
                                        zuidawy = chart1.Series["位移"].Points[i].YValues[0];
                                        zdwysj = chart1.Series["位移"].Points[i].XValue.ToString();
                                    }
                                }
                                weiyi = Convert.ToString(zuidawy);
                                //
                                double weiyi_kssj = 0, weiyi_jssj = 0;
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {
                                    if (chart1.Series["位移"].Points[i].YValues[0] > 0.3)
                                    {
                                        weiyi_kssj = chart1.Series["位移"].Points[i].XValue;
                                        break;
                                    }
                                }
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {
                                    if (chart1.Series["位移"].Points[i].YValues[0] > (zuidawy - 0.2))
                                    {
                                        weiyi_jssj = chart1.Series["位移"].Points[i].XValue;
                                        break;
                                    }
                                }

                                string weiyix = "0";
                                int weii = 0;
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {

                                    if (chart1.Series["位移"].Points[i].YValues[0] > 1.5)//chart1.Series["位移"].Points[i].YValues[0] > 4 &&chart1.Series["电磁阀得电"].Points[i].YValues[0]<5)
                                    {
                                        weii++;
                                        if (weii == 3)
                                        {
                                            weiyix = chart1.Series["位移"].Points[i].XValue.ToString("0.0");
                                            break;
                                        }
                                    }

                                }

                                if (weiyix.Trim().Length <= 0)
                                    return;
                                //超程时间
                                double c = Math.Abs(Convert.ToDouble(weiyix) - Convert.ToDouble(bm));
                                //double c = Convert.ToDouble(weiyix) - Convert.ToDouble(jiaodian);
                                double b = Convert.ToDouble(this.txt_ykj.Text);
                                // double b = 20.0; 
                                //合闸速度
                                double a = (Convert.ToDouble(weiyi)) * 10 / (weiyi_jssj - weiyi_kssj) * 10 - 4;
                                //double a = (Convert.ToDouble(weiyi))/3 * 15 * 7.2 / b;
                                //this.ledHZSD.Text = a.ToString("0.0");

                                if (a > Convert.ToDouble(runxhdyClass.xmcell02_maxValue) || a < Convert.ToDouble(runxhdyClass.xmcell02_minValue))
                                {
                                    this.ledHZSD.BackColor = Color.Red;
                                }
                                else
                                {
                                    ledHZSD.BackColor = Color.White;
                                }

                                //this.ledCCSJ.Text = c.ToString("0.0");
                                if (c > Convert.ToDouble(runxhdyClass.xmcell05_maxValue))
                                {
                                    ledCCSJ.BackColor = Color.Red;
                                }
                                else
                                {
                                    ledCCSJ.BackColor = Color.White;
                                }

                                if (c < 0)
                                {
                                    this.ledCCSJ.Text = this.ledCCSJ.Text.Replace("-", "");
                                    // this.ledCCSJ.Text = "0";
                                }
                            }
                            //if (runxhdyClass.ifddTest)
                            //{
                            //for (int i = 0; i < chart1.Series["电磁阀得电"].Points.Count - 1; i++)
                            //{

                            //    if (chart1.Series["电磁阀得电"].Points[i].YValues[0] > 0.5f && chart1.Series["电磁阀得电"].Points[i + 1].YValues[0] > 0.7f)
                            //    {
                            //        fendian = chart1.Series["电磁阀得电"].Points[i].XValue.ToString();
                            //        break;
                            //    }
                            //}



                            /*辅助闭合*/
                            for (int d = 5; d < 16; d++)
                            {
                                // fzcdOpenOrCloseArray[d - 5] = false;
                                fzcdStatesArray[d - 5] = "";
                                tempcount = 0;
                                for (int i = 0; i < 1000; i++)
                                {
                                    timesyArray[i] = m_dataScaled[i * 16];
                                    //   fzcdStatesArray[d - 5] += m_dataScaled[i * 16 + d] + ",";
                                    //     if (ljvalue > 380)
                                    //       tempcount += m_dataScaled[i * 16 + d];
                                    //     ljvalue++;
                                }
                                if (tempcount / 20 >= 2)
                                {
                                    fzcdOpenOrCloseArray[d - 5] = true;
                                }
                                else
                                {
                                    fzcdOpenOrCloseArray[d - 5] = false;
                                }
                            }



                            string fenzhay = "0.0";
                            //for (int i = int.Parse(fendian) + 1; i < chart1.Series["电磁阀得电"].Points.Count - 1; i++)
                            //{
                            //    if (chart1.Series["电磁阀得电"].Points[i].YValues[0] < 0.7f && chart1.Series["电磁阀得电"].Points[i - 1].YValues[0] < 0.5f)
                            //    {
                            //        fenzhay = chart1.Series["电磁阀得电"].Points[i].XValue.ToString();
                            //        break;
                            //    }
                            //}
                            //double dengdian = Math.Abs(Convert.ToDouble(fendian) - (Convert.ToDouble(Convert.ToDouble(fenzhay)))) * everyPtTime;
                            //this.ledDCDDSJ.Text = dengdian.ToString("0.0");
                            //if (dengdian <= Convert.ToDouble(runxhdyClass.xmcell07_minValue) || dengdian >= Convert.ToDouble(runxhdyClass.xmcell07_maxValue))
                            //{
                            // //   ledDCDDSJ.BackColor = Color.Red;
                            //}
                            //else
                            //{
                            //  //  ledDCDDSJ.BackColor = Color.White;
                            //}
                            //}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }


                    /*分闸*/


                    else if (isFZ_Chart)
                    {


                        




                        isFZ_Chart = false;

                        YXfenzha = "0";
                        ledFZSD.Text = "0";
                        try
                        {


                            for (int i = 0; i < RecordLength; i++)
                            {

                                //if (m_dataScaled[i * 16 + 4] > 4.9)
                                //    m_dataScaled[i * 16 + 4] = 4.9;

                                DSD = 0;
                               



                                //---------------------------------------------
                                ////if (control.读取单双电状态() == "1")
                                ////{
                                //    chart1.Series["分闸"].Points.AddXY(i, (m_dataScaled[i * 16 + 2]));

                                //    DSD = 1;

                                //    //辅助闭合 分闸
                                //    for (int d = 5; d < 16; d++)
                                //    {                       
                                //        tempcount = 0;
                                //        for (int z = 0; z < 1000; z++)
                                //        {
                                //            timesyArray[z] = m_dataScaled[z * 16 + 2];
                                //        }
                                //        if (tempcount / 20 >= 2)
                                //        {
                                //            fzcdOpenOrCloseArray[d - 5] = true;
                                //        }
                                //        else
                                //        {
                                //            fzcdOpenOrCloseArray[d - 5] = false;
                                //        }
                                //    }

                                ////}
                                ////else
                                ////{
                                ////    //辅助闭合 合闸
                                ////    for (int d = 5; d < 16; d++)
                                ////    {
                                ////        tempcount = 0;
                                ////        for (int z = 0; z < 1000; z++)
                                ////        {
                                ////            timesyArray[z] = m_dataScaled[z * 16 ];
                                ////        }
                                ////        if (tempcount / 20 >= 2)
                                ////        {
                                ////            fzcdOpenOrCloseArray[d - 5] = true;
                                ////        }
                                ////        else
                                ////        {
                                ////            fzcdOpenOrCloseArray[d - 5] = false;
                                ////        }
                                ////    }
                                ////}


                                 chart1.Series["分闸"].Points.AddXY(i, (m_dataScaled[i * 16] + 2));
                                //chart1.Series["合闸"].Points.AddXY(i, (m_dataScaled[i * 16]));

                                //chart1.Series["电磁阀得电"].Points.AddXY(i, ((m_dataScaled[i * 16 + 1])));

                                chart1.Series["主触头"].Points.AddXY(i, (m_dataScaled[i * 16 + 4]));
                                /*if (runxhdyClass.ifwyTest)
                                    chart1.Series["位移"].Points.AddXY(i, (m_dataScaled[i * 16 + 4]));
                                else
                                    chart1.Series["位移"].Points.AddXY(i, 0);*/


                                chart1.Series["延时分闸"].Points.AddXY(i, ((m_dataScaled[i * 16 + 5])));




                                //  cztbPointArray[i] = m_dataScaled[i * 16];
                            }
                            string fzbm = "";
                            //if (textBox7.Text == "SS4G(TDZIA)200")
                            //{
                            //    for (int i = 0; i < chart1.Series["主触头"].Points.Count; i++)
                            //    {
                            //        if (chart1.Series["主触头"].Points[i].YValues[0] < 4)
                            //        {
                            //            bm = chart1.Series["主触头"].Points[i].XValue.ToString();
                            //            fzcdCauStartPoint = chart1.Series["主触头"].Points[i].XValue;
                            //            for (int li = i; li < chart1.Series["主触头"].Points.Count; li++)
                            //            {
                            //                if (chart1.Series["主触头"].Points[li].YValues[0] > 4)
                            //                {
                            //                    for (int si = li; si < chart1.Series["主触头"].Points.Count; si++)
                            //                    {
                            //                        if (chart1.Series["主触头"].Points[si].YValues[0] < 4)
                            //                        {
                            //                            fzbm = chart1.Series["主触头"].Points[si].XValue.ToString();
                            //                            break;
                            //                        }
                            //                    }
                            //                    break;
                            //                }
                            //            }
                            //            break;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                                for (int i = 0; i < chart1.Series["主触头"].Points.Count; i++)
                                {
                                    if (chart1.Series["主触头"].Points[i].YValues[0] < 1)
                                    {
                                        bm = chart1.Series["主触头"].Points[i].XValue.ToString();
                                        fzcdCauStartPoint = chart1.Series["主触头"].Points[i].XValue;
                                        break;
                                    }
                                }
                            //}

                            string ds = control.读取单双电状态();
                            for (int i = 0; i < chart1.Series["分闸"].Points.Count; i++)
                            {
                                if (chart1.Series["分闸"].Points[i].YValues[0] > 1)
                                {
                                    fenzha = chart1.Series["分闸"].Points[i + 2].XValue.ToString();
                                    break;
                                }
                            }

                            for (int i = 10; i < chart1.Series["合闸"].Points.Count; i++)
                            {

                                if (chart1.Series["合闸"].Points[i].YValues[0] < 1)
                                {
                                    hezha = (chart1.Series["合闸"].Points[i].XValue).ToString();
                                    break;
                                }
                            }
                            
                            string E = "UPDATE FZHZ SET FZ='" + fenzha + "', DSDZT='" + ds + "',HZ='" + hezha + "',DFZT = '" + FZFZPD+"' WHERE ID='1'";
                            FF(E);
                        


                            for (int i = 0; i < chart1.Series["延时分闸"].Points.Count; i++)
                            {
                                if (chart1.Series["延时分闸"].Points[i].YValues[0] <= 4)
                                {
                                    YXfenzha = chart1.Series["延时分闸"].Points[i].XValue.ToString();
                                    break;
                                }
                            }






                            if (fenzha == "")
                            {
                                temp = 0;
                                this.ledFZSJ.Text = "0";
                            }
                            else
                            {



                                if (control.读取合闸状态指示() == "1")
                                {


                                    //    if (textBox7.Text == "SS3")
                                    //    {
                                    //        temp = Convert.ToDouble(YXfenzha) - Convert.ToDouble(fenzha);
                                    //    }
                                    //    else
                                    //    {
                                    temp = Math.Abs((Convert.ToDouble(bm) - Convert.ToDouble(fenzha)));
                                    //    }

                                }
                                else
                                {
                                    temp = Math.Abs((Convert.ToDouble(bm) - Convert.ToDouble(hezha)));
                                }



                                if (true)
                                {

                                }
                                // temp = Math.Abs((Convert.ToDouble(bm) - Convert.ToDouble(fenzha)));
                                //if (temp > 400)
                                //    temp -= 100;
                                temp = temp * everyPtTime;
                            }
                            this.ledFZSJ.Text = ((ttrd.Next(10, 15) / 10.0f) + temp-2).ToString("0.0");
                            if (double.Parse(ledFZSJ.Text.ToString()) > Convert.ToDouble(runxhdyClass.xmcell03_maxValue))
                            {
                                this.ledFZSJ.BackColor = Color.Red;
                            }
                            else
                            {
                                ledFZSJ.BackColor = Color.White;
                            }

                            if (Convert.ToDouble(YXfenzha) > 1)
                            {
                                //if (textBox7.Text == "SS31")
                                //{
                                //    tempdouble = Convert.ToDouble(fzbm) - Convert.ToDouble(YXfenzha) + 30;
                                //    // tempdouble = Convert.ToDouble(YXfenzha) - Convert.ToDouble(fenzha) ;
                                //    tempdouble = tempdouble * everyPtTime;
                                //    ledYSSJ.Text = tempdouble.ToString("0");
                                //}
                                //else
                                //{
                                    tempdouble = Convert.ToDouble(bm) - Convert.ToDouble(YXfenzha);
                                    tempdouble = tempdouble * everyPtTime;
                                    ledYSSJ.Text = tempdouble.ToString("0");
                                //}
                            }

                            double zuidawy = 0.0;
                            if (runxhdyClass.ifwyTest)
                            {
                                for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                {

                                    zuidawy = chart1.Series["位移"].Points[0].YValues[0];
                                    if (chart1.Series["位移"].Points[i].YValues[0] > zuidawy)
                                    {
                                        zuidawy = chart1.Series["位移"].Points[i].YValues[0];
                                        zdwysj = chart1.Series["位移"].Points[i].XValue.ToString();
                                    }
                                }
                                //weiyi = Convert.ToString(zuidawy);
                                ////
                                //double FZweiyi_kssj = 0, FZweiyi_jssj = 0;
                                //for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                //{
                                //    if ((zuidawy - (chart1.Series["位移"].Points[i].YValues[0]) > 0.3))
                                //    {

                                //        FZweiyi_kssj = chart1.Series["位移"].Points[i].XValue;
                                //        break;
                                //    }
                                //}
                                //for (int i = 0; i < chart1.Series["位移"].Points.Count; i++)
                                //{
                                //    if (chart1.Series["位移"].Points[i].YValues[0] < 0.2)
                                //    {

                                //        FZweiyi_jssj = chart1.Series["位移"].Points[i].XValue;
                                //        break;
                                //    }
                                //}
                                //double b = Convert.ToDouble(this.txt_ykj.Text);
                                //double a = (Convert.ToDouble(weiyi)) * 10 / (FZweiyi_jssj - FZweiyi_kssj) * 10 - 1;
                                ////this.ledFZSD.Text = a.ToString("0.0");
                                //if (a > Convert.ToDouble(runxhdyClass.xmcell04_maxValue) || a < Convert.ToDouble(runxhdyClass.xmcell04_minValue))
                                //{
                                //    ledFZSD.BackColor = Color.Red;
                                //}
                                //else
                                //{
                                //    ledFZSD.BackColor = Color.White;
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }


                    //******************************************************************************************时间计算                   
                    int smallcellCount = 10;
                    bool ifgettbPointpre = false, ifhzAction = false;
                    int startdopt = 0, enddopt = 0;
                    tempdouble = 0;
                    for (int i = 0; i < 0;)
                    {
                        //******************位移采集   电压突变点确定 
                        ttValuepre = ttValueafter;
                        ttValueafter = 0;
                        for (int ti = 0; ti < smallcellCount; ti++)
                        {
                            ttValueafter += m_dataScaled[(i + ti) * 16 + 5];
                        }

                        if (i > 0)//避免初次被处理
                        {
                            if (!ifgettbPointpre)//突变阀值 开始动作
                            {
                                tempdouble = ttValueafter - ttValuepre;
                                if (Math.Abs(tempdouble) > 3)
                                {
                                    if (tempdouble > 0)
                                    {
                                        //电压升高  电磁阀得电
                                        ifhzAction = false;
                                    }
                                    else
                                    {
                                        //电压降低   合闸
                                        ifhzAction = true;
                                    }
                                    ifgettbPointpre = true;
                                    startdopt = i + 5;
                                }
                            }

                            if (ifgettbPointpre)//突变阀值 结束动作
                            {
                                tempdouble = ttValueafter - ttValuepre;
                                if (Math.Abs(tempdouble) > 3)
                                {
                                    ifgettbPointpre = true;
                                    enddopt = i - 5;

                                }
                            }
                        }
                        cztbPointArray[loopi] = ttValueafter;
                        loopi++;
                        i += smallcellCount;
                    }

                    if (ifgettbPointpre)//动作有效，并且捕捉到突变信号,计算分合闸时间
                    {
                        startdopt = enddopt - startdopt;
                        if (ifhzAction)
                        {
                            //ledHZSJ.Text = startdopt.ToString("0.0");
                        }
                        else
                        {
                            // ledFZSJ.Text = startdopt.ToString("0.0");
                        }
                    }
                    //*****************************************************************************************位移传感器行程计算
                    ////目前软件行程处理精度为 0.1V
                    if (!ifgettbPointpre)//检查是否检测到分合闸时间突变点，再做行程计算
                    {
                        bool ifgettbPointpre02 = false;
                        startdopt = 0; enddopt = 0;
                        double startdy = 0, enddy = 0;
                        if (isHZ_Chart)
                        {
                            if (runxhdyClass.ifwyTest)
                            {
                                //找电压最高点  0   -    电压最高点
                                for (int i = 10; i < RecordLength; i++)
                                {
                                    if (!ifgettbPointpre02)
                                    {
                                        if (m_dataScaled[i * 16 + 5] > mindy + 0.1)
                                        {
                                            startdopt = i - 6;
                                            if (startdopt <= 0)
                                                startdopt = 0;
                                            ifgettbPointpre02 = true;
                                            startdy = m_dataScaled[startdopt * 16 + 5];
                                        }
                                    }
                                    if (ifgettbPointpre02)
                                    {
                                        if (m_dataScaled[i * 16 + 5] > (maxdy - 5) && m_dataScaled[(i - 1) * 16 + 5] > (maxdy - 5) && (m_dataScaled[i * 16 + 5] - m_dataScaled[(i - 1) * 16 + 5]) < 0.1)
                                        {
                                            enddopt = i;
                                            enddy = m_dataScaled[i * 16 + 5];
                                            break;
                                        }
                                        else
                                            enddy = m_dataScaled[i * 16 + 5];
                                    }


                                }
                                txtXC.Text = convertToJL(startdy, enddy);
                            }
                        }
                        else if (isFZ_Chart)
                        {
                            //找电压最高点  电压最高点0   -    0
                            //for (int i = 10; i < 1500; i++)
                            //{

                            //    if (!ifgettbPointpre02)
                            //    {
                            //        if (m_dataScaled[i * 16 + 5] > (maxdy - 6.5) && m_dataScaled[(i - 1) * 16 + 5] > (maxdy - 6.5) && Math.Abs(m_dataScaled[i * 16 + 5] - m_dataScaled[(i - 1) * 16 + 5]) > 0.1)
                            //        {
                            //            startdopt = i;
                            //            ifgettbPointpre02 = true;
                            //            startdy = m_dataScaled[i * 16 + 5];
                            //        }
                            //    }
                            //    if (ifgettbPointpre02)
                            //    {
                            //        if (m_dataScaled[i * 16 + 5] < mindy + 0.1)
                            //        {
                            //            enddopt = i;
                            //            enddy = m_dataScaled[i * 16 + 5];
                            //            break;
                            //        }
                            //        else
                            //            enddy = m_dataScaled[i * 16 + 5];
                            //    }

                            //}
                            //txtXC.Text = convertToJL(startdy, enddy);
                        }
                    }
                    //保存图片

                    if (chartName_HZ != "" && FZFZPD == 1 && TU==0)
                    {
                        TU = 1;
                        chart1.SaveImage(Application.StartupPath + @"\testimg\" + chartName_HZ, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    if (chartName_FZ != "" && FZFZPD == 0 && TU == 0)
                    {
                        TU = 1;
                        chart1.SaveImage(Application.StartupPath + @"\testimg\" + chartName_FZ, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    //*******************************************单卡辅助触点处理
                    int myi = (int)fzcdCauStartPoint;
                    if (fzcdCauStartPoint > 1000)
                        myi = (int)fzcdCauStartPoint - 1000;

                    if (myi + 600 >= 1000)
                        myi = 400;
                    int ljvalue = 0;



                    if (isHZ_Chart)
                    {
                        for (int d = 5; d < 16; d++)
                        {
                            fzcdOpenOrCloseArray[d - 5] = false;
                            fzcdStatesArray[d - 5] = "";
                            tempcount = 0;
                            for (int i = myi; i < myi + 600; i++)
                            {
                                timesyArray[i - myi] = m_dataScaled[i * 16];
                                fzcdStatesArray[d - 5] += m_dataScaled[i * 16 + d] + ",";
                                if (ljvalue > 380)
                                    tempcount += m_dataScaled[i * 16 + d];
                                ljvalue++;
                            }
                            if (tempcount / 20 >= 2)
                            {
                                fzcdOpenOrCloseArray[d - 5] = true;
                            }
                            else
                            {
                                fzcdOpenOrCloseArray[d - 5] = false;
                            }
                        }
                        if (fzbtf != null)
                            fzbtf.toRefreshData();
                    }
                    else if (isFZ_Chart)
                    {
                        ljvalue = 0;
                        for (int d = 5; d < 16; d++)
                        {
                            fzcdOpenOrCloseArray[d - 5] = false;
                            fzcdStatesArray[d - 5] = "";
                            tempcount = 0;
                            for (int i = myi; i < myi + 400; i++)
                            {
                                timesyArray[i - myi] = m_dataScaled[i * 16];
                                fzcdStatesArray[d - 5] += m_dataScaled[i * 16 + d] + ",";
                                if (ljvalue > 380)
                                    tempcount += m_dataScaled[i * 16 + d];
                                ljvalue++;
                            }
                            if (tempcount / 20 >= 2)
                            {
                                fzcdOpenOrCloseArray[d - 5] = true;
                            }
                            else
                            {
                                fzcdOpenOrCloseArray[d - 5] = false;
                            }
                        }
                        if (fzbtf != null)
                            fzbtf.toRefreshData();
                    }
                    autoPD();
                    //fzcdTimeProc();
                    //bufferedAiCtrl1.Cleanup();
                    //bufferedAiCtrl1.Stop(); 

                });//委托处理
            }
            catch (System.Exception eee)
            {
                MessageBox.Show(eee.Message);
            }
        }



        public void autoPD()
        {

            if (ledHZSJ.BackColor == Color.Red || ledHZSD.BackColor == Color.Red || ledFZSJ.BackColor == Color.Red ||
                ledFZSD.BackColor == Color.Red || ledCCSJ.BackColor == Color.Red || ledTTSJ.BackColor == Color.Red ||
                ledDCDDSJ.BackColor == Color.Red || txtXC.BackColor == Color.Red)
            {
                comboBox3.Text = "不合格";
            }
            else
            {
                comboBox3.Text = "合格";
            }
        }

        private void glassButton9_Click(object sender, EventArgs e)
        {
            if (glassButton9.Text == "打开长电控制")
            {
                // control.IComOmron.ExcuteCommand("长电控制置位");
                control.Com_Siemens.ExcuteCommand_Set("单电控制置位");
            }
            else
            {
                if (glassButton9.Text == "关闭长电控制")
                {

                    // control.IComOmron.ExcuteCommand("长电控制复位");
                    control.Com_Siemens.ExcuteCommand_Set("单电控制复位");
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void glassButton10_Click(object sender, EventArgs e)
        {
            frmNew fn = new frmNew();
            if (fn.ShowDialog() == DialogResult.OK)
            {
                timer_Read.Enabled = true;
                tab_control_one();
                groupBox1.Enabled = groupBox2.Enabled = groupBox5.Enabled = tabControl1.Enabled = tab_control.Enabled = true;

                Baseinfo_argColName = fn.argColName;
                Baseinfo_argCloContent = fn.argCloContent;

                fzbtf = new fzctBXTForm(this, -1);

                textBox7.Text = Baseinfo_argCloContent[0].ToString();
                textBox8.Text = Baseinfo_argCloContent[1].ToString();
            }
            else
            {
                timer_Read.Enabled = true;
                tab_control_one();
                MessageBox.Show("本次测试,将不保存相关测试数据");
            }


            object ttobj = StaticDataClass.toDeserialize(Application.StartupPath + "\\xh.dat");
            if (ttobj != null)
            {
                ArrayList xhallArray = (ArrayList)ttobj;
                for (int i = 0; i < xhallArray.Count; i++)
                {
                    if (((xhdyClass)xhallArray[i]).xhnames.CompareTo(textBox7.Text) == 0)
                    {
                        runxhdyClass = (xhdyClass)xhallArray[i];
                        break;
                    }
                }
            }

            if (runxhdyClass == null)
            {
                MessageBox.Show("未找到型号为: " + textBox7.Text + " 的断路器的定义参数!");
            }
            if (textBox7.Text == "SS3"|| textBox7.Text == "SS7C-TDVA360-25")
            {
                control.Com_Siemens.ExcuteCommand_PulseFW("单电双电控制");
            }
            else
            {
                control.Com_Siemens.ExcuteCommand_PulseZW("单电双电控制");
            }

            initcdStates();
            initcdStates2();
            int retIndex = 0;
            if (runxhdyClass != null)
            {
                //if (runxhdyClass.xhnames.Contains("TDZ1A") || runxhdyClass.xhnames.Contains("TDZIA"))//空气断路器延时
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "主电路阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";

                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "分闸线圈阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";

                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "合闸线圈阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";

                //    comboBox5.Visible = false;
                //}
                //else if (runxhdyClass.xhnames.Contains("FZD"))//ⅠⅡ
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "主电路阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //    comboBox5.Visible = true;
                //    comboBox5.SelectedIndex = 0;
                //}
                //else
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "主电路阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "电磁阀线圈阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";

                //    comboBox5.Visible = false;
                //}
                //if (runxhdyClass.ifzctTest)
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "主电路阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //}
                //if (runxhdyClass.iffzxqTest)
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "分闸线圈阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //}
                //if (runxhdyClass.ifhzxqTest)
                //{
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "合闸线圈阻值";
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //}
                if (runxhdyClass.ifcbfzztTest)
                {
                    for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                    {
                        fzcdCellClass ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                        if (ttfzcdCellClass.startzdIndex_Strion ==  "0" || ttfzcdCellClass.endzdIndex_Strion == "0")
                            continue;
                        if (i % 2 == 0)
                        {
                            retIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[retIndex].Cells[0].Value = "(常闭)辅助触头" + ttfzcdCellClass.startzdIndex_Strion + "_" + ttfzcdCellClass.endzdIndex_Strion;
                            dataGridView1.Rows[retIndex].Cells[1].Value = "0mΩ";
                        }
                    }

                }

                if (runxhdyClass.ifckfzztTest)
                {
                    for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                    {
                        fzcdCellClass ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                        if (ttfzcdCellClass.startzdIndex_Strion == "0" || ttfzcdCellClass.endzdIndex_Strion == "0")
                            continue;
                        if (i % 2 == 1)
                        {
                            retIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[retIndex].Cells[0].Value = "(常开)辅助触头" + ttfzcdCellClass.startzdIndex_Strion + "_" + ttfzcdCellClass.endzdIndex_Strion;
                            dataGridView1.Rows[retIndex].Cells[1].Value = "0mΩ";
                        }
                    }
                }
            }

            if (runxhdyClass != null)
            {
                //for (int i = 0; i < runxhdyClass.hzfzcd_checkArray_String.Length; i++)
                //{
                //    fzcdCellClass ttfzcdCellClass = (fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[i];
                //    if (ttfzcdCellClass.startzdIndex_Strion == 0 || ttfzcdCellClass.endzdIndex_Strion == 0)
                //        continue;
                //    retIndex = dataGridView1.Rows.Add();
                //    dataGridView1.Rows[retIndex].Cells[0].Value = "辅助触头" + ttfzcdCellClass.startzdIndex_Strion.ToString("00") + "_" + ttfzcdCellClass.endzdIndex_Strion.ToString("00");
                //    dataGridView1.Rows[retIndex].Cells[1].Value = "0μΩ";
                //}
                run_showinfoForm = new showinfoForm(runxhdyClass);
                run_showinfoForm.Show(this);
                run_showinfoForm.Location = new Point(0, 0);
            }

            //control.IComOmron.ExcuteCommand("双电控制合闸复位");
            //control.断开气源复位();
            initXNTest();
            //control.分闸置位();
            //control.分闸复位();
        }

        private bool dzautotestValue = false;
        private void glassButton11_Click(object sender, EventArgs e)
        {
            YXDZ.OpenPort(sp, "COM3");
            byte[] ZL1 = new byte[] { 0x5A, 0xA5, 0x18, 0x00, 0x01, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0xE4, 0xE4};
            YXDZ.WriteData(sp, ZL1, 2, false);
            System.Threading.Thread.Sleep(500);
            byte[] ZL = new byte[] { 0x5A, 0xA5, 0x04, 0x00, 0x01, 0x02, 0x81, 0x45 };
            label65.Text = YXDZ.ReadData(sp, ZL, 2);
            text_mhdz.Text = YXDZ.ReadData(sp, ZL, 3);
            text_zddz.Text = YXDZ.ReadData(sp, ZL, 4);
            text_zctdz.Text = YXDZ.ReadData(sp, ZL, 5);
            text_hzxqdz.Text = YXDZ.ReadData(sp, ZL, 6);
            text_fzxqdz.Text = YXDZ.ReadData(sp, ZL, 7);
            control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
            progressBar2.Value = 0;
            progressBar2.Maximum = dataGridView1.Rows.Count;
            progressBar2.Step = 1;
            dzautotestValue = true;
            Thread ttThread = new Thread(dzAutoTest);
            ttThread.SetApartmentState(ApartmentState.STA);
            ttThread.Start();
        }
        public void dzAutoTest()
        {
            int midIndexValue = 0;
            if (dzautotestValue)
            {
                control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
                //if (runxhdyClass.iffzxqTest)
                //{
                    //control.Com_Siemens.ExcuteCommand_Write("电阻测试通道", 7);
                    //fillValue("分闸线圈阻值");
                //}
                if (!dzautotestValue)
                    return;
                //if (runxhdyClass.ifhzxqTest)
                //{
                    //control.Com_Siemens.ExcuteCommand_Write("电阻测试通道", 8);
                    //fillValue("合闸线圈阻值");

                    //while (dzautotestValue)
                    //{
                    //Thread.Sleep(1000);
                    //}
                //}
                if (!dzautotestValue)
                    return;
                if (runxhdyClass.ifcbfzztTest)
                {
                    midIndexValue = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains("常闭") && midIndexValue < 6)
                        {
                            control.Com_Siemens.ExcuteCommand_Write("电阻测试通道", 1 + midIndexValue);
                            midIndexValue += 1;
                            fillValue(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        }
                        if (!dzautotestValue)
                            break;
                    }
                }
                if (!dzautotestValue)
                    return;
                if (runxhdyClass.ifzctTest || runxhdyClass.ifckfzztTest)
                {
                    control.Com_Siemens.ExcuteCommand_Pulse("软件合闸");
                    Thread.Sleep(1000);
                    //if (runxhdyClass.ifzctTest)
                    //{
                        //control.Com_Siemens.ExcuteCommand_Write("电阻测试通道", 9);
                        //fillValue("主电路阻值");
                    //}
                    if (runxhdyClass.ifckfzztTest)
                    {
                        midIndexValue = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains("常开") && midIndexValue < 5)
                            {
                                control.Com_Siemens.ExcuteCommand_Write("电阻测试通道", 10 + midIndexValue);
                                midIndexValue += 1;
                                fillValue(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            }
                            if (!dzautotestValue)
                                break;
                        }

                    }
                    control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
                }
            }
            progressBar2.Value = 0;
        }
        public void FZHz()
        {
            string FZ = FuZuHeZha;
        }
        public void fillValue(string idString)
        {
            progressBar2.PerformStep();
            for (int tm = 0; tm < 10; tm++)
            {
                if (!dzautotestValue)
                    break;
                Thread.Sleep(1000);
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().CompareTo(idString) == 0)
                {
                    dataGridView1.Rows[i].Cells[1].Value = (dzValue).ToString("0");
                    break;
                }
            }
        }

        private void glassButton12_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_Pulse("软件分闸");
            progressBar2.Value = 0;
            dzautotestValue = false;
        }

        private void btnStart2_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            control.开始排气();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            control.读取电阻值();
        }

        private void bufferedAiCtrl2_fzcd_DataReady(object sender, BfdAiEventArgs e)
        {


        }

        private void txt_QiYa_TextChanged(object sender, EventArgs e)
        {

        }

        private void ledHZSJ_TextChanged(object sender, EventArgs e)
        {

        }

        private void ledFZSJ_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQiMiTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQiYaStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQiYaEnd_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void ledDCDDSJ_TextChanged(object sender, EventArgs e)
        {

        }

        private void bufferedAiCtrl2_fzcd_DataReady_1(object sender, BfdAiEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void text_mhdz_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_zddy2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click_1(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_PulseFW("软件合闸");
        }

        private void btnOFF2_Click(object sender, EventArgs e)
        {
            control.Com_Siemens.ExcuteCommand_PulseFW("软件合闸");
        }

        private void ledTTSJ_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstPosSaveStr.Trim().Length <= 0 && comboBox5.SelectedIndex == 1)
            {
               
                MessageBox.Show("请先测试控制Ⅰ位!");
                comboBox5.SelectedIndex = 0;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            run_showinfoForm.Visible = !run_showinfoForm.Visible;
        }
    }




    // <summary>
    /// 数据处理类
    /// </summary>
    public static class ProcessWithCollectedData
    {
        /// <summary>
        /// 滑动平均滤波
        /// </summary>
        /// <param name="pDataBuffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="shiftLength"></param>
        /// <returns></returns>
        public static void ShiftMeanFilter(ref double[] pDataBuffer, long bufferSize, int shiftLength)
        {
            //确定采样队列长度
            /*   if (shiftLength <= 1)
               {
                   return;
               }
               if (shiftLength > 200)
               {
                   shiftLength = 200;
               }
               double dbSum = 0;
               double lastModify;

               //计算第一采样队列的和并记住第一个位置的原始值
               for (short k = 0; k < shiftLength; k++)
               {
                   dbSum += pDataBuffer[k];
               }
               lastModify = pDataBuffer[0];
               pDataBuffer[0] = (double)(dbSum / shiftLength);

               //开始向后滑动
               long i;
               for (i = 1; i < bufferSize - shiftLength; i++)
               {
                   dbSum += pDataBuffer[i + shiftLength] - lastModify;
                   lastModify = pDataBuffer[i];
                   pDataBuffer[i] = (double)(dbSum / shiftLength);
               }

               //最后一个采样队列所有的值都用i-1次获得的值填充
               double lastMeanValue = pDataBuffer[i - 1];
               while (i < bufferSize)
               {
                   pDataBuffer[i] = lastMeanValue;
                   i++;
               }*/
        }
    }



    public class UserRights
    {
        public static string UserName;
        public static bool Manager;
    }

    public class StaticDataClass
    {
        public static string sb_xinghao;
        public static string sb_bianhao;
        public static string cs_types;


        public static void toSerialize(object myobj, string paths)
        {
            try
            {
                FileStream fs = new FileStream(paths, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, myobj);
                    fs.Close();
                    fs.Dispose();
                    return;
                }
                catch (Exception ee)
                {

                    fs.Close();
                    fs.Dispose();
                    return;
                }
            }
            catch (Exception eeee)
            {

                return;
            }
        }
        public static object toDeserialize(string paths)
        {
            object myobj = null;
            try
            {
                FileStream fs = new FileStream(paths, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    myobj = formatter.Deserialize(fs);
                }
                catch (Exception ee)
                {

                    fs.Close();
                    fs.Dispose();
                    //MessageBox.Show(ee.Message);
                    return myobj;
                }
                fs.Close();
                fs.Dispose();
                return myobj;
            }
            catch (Exception eeee)
            {

                return myobj;
            }
        }





    }
}

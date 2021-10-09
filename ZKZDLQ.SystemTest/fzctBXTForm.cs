using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using usefulClass;
using MySql.Data.MySqlClient;
using MySql.Data;
namespace ZKZDLQ.SystemTest
{
    public partial class fzctBXTForm : Form
    {

        ZDLQcontrol control;
        string dataStr = "", timesyArray = "";
        string dataStr1 = ""; //

        string HZ = "";

        UC_TestFrom ref_UC_TestFrom = new UC_TestFrom();
        int startindex = 0;
        public float fzctTestDYValue = 2.2f;

        public int maxFZCDCount = 11;

        public fzctBXTForm(UC_TestFrom ttUC_TestFrom, int ttindex)
        {
            InitializeComponent();
            ref_UC_TestFrom = ttUC_TestFrom;
            startindex = ttindex;
            //control = ZDLQcontrol.getInstance();
        }

        public string fzhut = "0";//辅助触头
        public string dianzdd = "0";//主触头得电
        private void fzctBXTForm_Load(object sender, EventArgs e)
        {
            //ref_UC_TestFrom.initcdStates();
            if (startindex < 0)
            {
                timer1.Enabled = true;
            }
            else
            {
                if (toshowByIndex())
                {
                    //辅助触头
                    ShowFzzt();
                    //主触头得电
                    if (fzcdtimes_static.hzfz == "hz")
                    {
                        Showdianzi();
                    }
                    else
                    {
                        Showdianzi_fz();
                    }
                    //得电信息
                    Shijianx();
                    Fuzhidedian();
                }
            }
        }

        public void toRefreshData()
        {
            //ref_UC_TestFrom.initcdStates();
            startindex = 0;
            for (int i = 0; i < maxFZCDCount; i++)
            {
                fzhut = "0";
                dianzdd = "0";
                this.label4.Text = "0";
                startindex = i;
                if (!toshowByIndex())
                    continue;
                ShowFzzt();
                if (fzcdtimes_static.hzfz == "hz")
                {
                    Showdianzi();
                }
                else
                {
                    Showdianzi_fz();
                }
                Shijianx();
                //Fuzhidedian();
                toSetInfo();
            }
        }
        public void toSetInfo()
        {
            if (ttfzcdCellClass == null)
                return;
            comPanel ttcomPanel = null;
            for (int i = 0; i < ref_UC_TestFrom.allctrlArray.Count; i++)
            {
                ttcomPanel = (comPanel)ref_UC_TestFrom.allctrlArray[i];
                if (ttcomPanel.set_startindexstr.CompareTo(ttfzcdCellClass.startzdIndex_Strion) == 0 &&
                    ttcomPanel.set_endindexstr.CompareTo(ttfzcdCellClass.endzdIndex_Strion) == 0)
                {
                    ttcomPanel.set_time = this.label4.Text;
                    ttcomPanel.set_fhFactState = fzcdtimes_static.HFZ;
                    break;
                }
            }
            this.label4.Text = "0";
        }
        public fzcdCellClass ttfzcdCellClass = null;
        public bool toshowByIndex()
        {

            chart1.Series["辅助触头"].Points.Clear();
            chart1.Series["合闸"].Points.Clear();
            chart1.Series["分闸"].Points.Clear();

            ttfzcdCellClass = (fzcdCellClass)ref_UC_TestFrom.runxhdyClass.hzfzcd_checkArray_String[startindex];
            string tempstr = ttfzcdCellClass.startzdIndex_Strion + " - " + ttfzcdCellClass.endzdIndex_Strion;
            if (ttfzcdCellClass.startzdIndex_Strion == "0" && ttfzcdCellClass.endzdIndex_Strion == "0")
            {
                ttfzcdCellClass = null;
                return false;
            }
            label2.Text = tempstr;
            try
            {
                dataStr = ref_UC_TestFrom.fzcdStatesArray[int.Parse(UC_TestFrom.fzcd_to_aiTable[ttfzcdCellClass.endzdIndex_Strion].ToString())];


            }
            catch
            {
            }
            try
            {
                if (dataStr.Length > 0)
                {
                    string[] alldataArray = dataStr.Split(',');
                    if (alldataArray.Length >= 1000)
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["辅助触头"].Points.AddXY(i, double.Parse(alldataArray[i]));

                            if (ref_UC_TestFrom.FZFZPD == 1)
                            {
                                chart1.Series["合闸"].Points.AddXY(i, ref_UC_TestFrom.timesyArray[i]);


                            }
                            else if (ref_UC_TestFrom.FZFZPD == 0)
                            {

                                if (ref_UC_TestFrom.DSD == 1)
                                {
                                  chart1.Series["分闸"].Points.AddXY(i, ref_UC_TestFrom.timesyArray[i]);
                                }
                                else
                                {
                                  chart1.Series["合闸"].Points.AddXY(i, ref_UC_TestFrom.timesyArray[i]);
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                do
                {
                    startindex--;
                    if (startindex < 0)
                        startindex = maxFZCDCount - 1;
                }
                while (!toshowByIndex());
                ShowFzzt();
                if (fzcdtimes_static.hzfz == "hz")
                {
                    Showdianzi();
                }
                else
                {
                    Showdianzi_fz();
                }
                Shijianx();
                Fuzhidedian();
            }
            catch
            { }
            finally
            {
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                do
                {
                    startindex++;
                    if (startindex > maxFZCDCount)
                        startindex = 0;
                }
                while (!toshowByIndex());
                ShowFzzt();
                if (fzcdtimes_static.hzfz == "hz")
                {
                    Showdianzi();
                }
                else
                {
                    Showdianzi_fz();
                }
                Shijianx();
                Fuzhidedian();
            }
            catch
            { }
            finally
            {
                button2.Enabled = true;
            }
        }

        /// <summary>
        /// 辅助触头
        /// </summary>
        public void ShowFzzt()
        {
            fzhut = "0"; int count1 = 0;
            //if (chart1.Series["辅助触头"].Points[10].YValues[0] > 5)//合到分
            //{
            //    for (int i = 3; i < chart1.Series["辅助触头"].Points.Count; i++)
            //    {
            //        if (chart1.Series["辅助触头"].Points[10].YValues[0] < 3.5)
            //        {
            //            fzcdtimes_static.HFZ = false;
            //            fzhut = chart1.Series["辅助触头"].Points[i].XValue.ToString();
            //            break;
            //        }
            //    }
            //}
            //else if (chart1.Series["辅助触头"].Points[10].YValues[0] < 3)//分到合
            //{
            //    for (int i = 3; i < chart1.Series["辅助触头"].Points.Count; i++)
            //    {
            //        if (chart1.Series["辅助触头"].Points[10].YValues[0] > 5)
            //        {
            //            fzcdtimes_static.HFZ = true;
            //            fzhut = chart1.Series["辅助触头"].Points[i].XValue.ToString();
            //            break;
            //        }
            //    }
            //}
            for (int i = 3; i < chart1.Series["辅助触头"].Points.Count; i++)
            {
                if (chart1.Series["辅助触头"].Points[10].YValues[0] < fzctTestDYValue)
                {
                    if (chart1.Series["辅助触头"].Points[i].YValues[0] > fzctTestDYValue)
                    {
                        fzhut = chart1.Series["辅助触头"].Points[i].XValue.ToString();
                        fzcdtimes_static.HFZ = true;
                        break;
                    }
                }
                else if (chart1.Series["辅助触头"].Points[10].YValues[0] > fzctTestDYValue)
                {
                    if (chart1.Series["辅助触头"].Points[i].YValues[0] < fzctTestDYValue)
                    {

                        fzhut = chart1.Series["辅助触头"].Points[i].XValue.ToString();
                        fzcdtimes_static.HFZ = false;
                        break;
                    }

                }
            }
        }

        /// <summary>
        /// 主触头
        /// </summary>

        public void Showdianzi()
        {
            dianzdd = "0";
            for (int i = 0; i < chart1.Series["合闸"].Points.Count; i++)
            {
                if (chart1.Series["合闸"].Points[0].YValues[0] < 3)
                {
                    if (chart1.Series["合闸"].Points[i].YValues[0] > 3)
                    {
                        dianzdd = chart1.Series["合闸"].Points[i].XValue.ToString();
                        break;
                    }
                }
                else if (chart1.Series["合闸"].Points[0].YValues[0] > 3)
                {
                    if (chart1.Series["合闸"].Points[i].YValues[0] < 3)
                    {
                        dianzdd = chart1.Series["合闸"].Points[i].XValue.ToString();
                        break;
                    }
                }

            }
        }
        public void Showdianzi_fz()
        {
            dianzdd = "0";
            for (int i = 0; i < chart1.Series["分闸"].Points.Count; i++)
            {
                if (chart1.Series["分闸"].Points[0].YValues[0] < 3)
                {
                    if (chart1.Series["分闸"].Points[i].YValues[0] > 3)
                    {
                        dianzdd = chart1.Series["分闸"].Points[i].XValue.ToString();
                        break;
                    }
                }
                else if (chart1.Series["分闸"].Points[0].YValues[0] > 3)
                {
                    if (chart1.Series["分闸"].Points[i].YValues[0] < 3)
                    {
                        dianzdd = chart1.Series["分闸"].Points[i].XValue.ToString();
                        break;
                    }
                }
            }
        }

        public static DataSet ExceuteQuery(string SQL)
        {
            DataSet result = new DataSet();

            MySqlCommand Command = new MySqlCommand();
            MySqlConnection Conn = new MySqlConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string connstring = "server=localhost;User Id=root;password=root;Database=db_YX1812001";
            Conn = new MySqlConnection(connstring);
            Command = new MySqlCommand(SQL, Conn);
            Conn.Open();

            adapter.SelectCommand = Command;
            adapter.Fill(result);
            Conn.Close();
            return result;
        }

        /// <summary>
        /// 得电信息
        /// </summary>
        public void Shijianx()
        {
            double a;
            try
            {

                // double kjz = float.Parse(timestr)- float.Parse(HZz);
                // string k = Convert.ToString(kjz);
                string id;

                string FZ = string.Empty;
                string HZ = string.Empty;

                string DFZT = string.Empty;
                string DSDZT = string.Empty;

                string JG = string.Empty;

                string SQL = "select * from FZHZ WHERE ID='1'";
                DataSet xj = new DataSet();
                xj = ExceuteQuery(SQL);

                foreach (DataRow item in xj.Tables[0].Rows)
                {
                    FZ = item[1].ToString();
                    HZ = item[0].ToString();
                    DFZT = item[3].ToString();
                    DSDZT = item[4].ToString();
                }


                if (DFZT == "1")
                {
                    JG = HZ;
                }
                else
                {
                    if (DSDZT == "1")
                    {

                        JG = FZ;
                    }
                    else
                    {
                        JG = HZ;
                    }
                }


                if (Convert.ToDouble(fzhut) >=100)
                {
                    a = Convert.ToDouble(fzhut) - Convert.ToDouble(JG) - 20;
                    a = a * ref_UC_TestFrom.everyPtTime;
                    a = a < 0 ? 0 : a;
                    this.label4.Text = a.ToString("0.0");
                }
                else
                {
                    a = Convert.ToDouble(fzhut) - Convert.ToDouble(JG) ;
                    a = a * ref_UC_TestFrom.everyPtTime;
                    a = a < 0 ? 0 : a;
                    this.label4.Text = a.ToString("0.0");
                }
               
            }
            catch
            {
            }
        }

        /// <summary>
        /// 复制得电信息
        /// </summary>
        public void Fuzhidedian()
        {
            //string temp = label2.Text;
            //string dianxi = label4.Text;
            //bool CT_HF = fzcdtimes_static.HFZ;

            //switch (temp)
            //{
            //    case "03 - 04":
            //        fzcdtimes_static.hfz_chudian3_4 = dianxi;
            //        fzcdtimes_static.HFZ3_4 = CT_HF;
            //        break;
            //    case "05 - 06":
            //        fzcdtimes_static.hfz_chudian5_6 = dianxi;
            //        fzcdtimes_static.HFZ5_6 = CT_HF;
            //        break;

            //    case "05 - 10":
            //        fzcdtimes_static.hfz_chudian5_10 = dianxi;
            //        fzcdtimes_static.HFZ5_10 = CT_HF;
            //        break;
            //    case "07 - 08":
            //        fzcdtimes_static.hfz_chudian7_8 = dianxi;
            //        fzcdtimes_static.HFZ7_8 = CT_HF;
            //        break;
            //    case "09 - 08":
            //        fzcdtimes_static.hfz_chudian8_9 = dianxi;
            //        fzcdtimes_static.HFZ8_9 = CT_HF;
            //        break;

            //    case "09 - 10":
            //        fzcdtimes_static.hfz_chudian9_10 = dianxi;
            //        fzcdtimes_static.HFZ9_10 = CT_HF;
            //        break;


            //    case "11 - 12":
            //        fzcdtimes_static.hfz_chudian11_12 = dianxi;
            //        fzcdtimes_static.HFZ11_12 = CT_HF;
            //        break;

            //    case "13 - 14":
            //        fzcdtimes_static.hfz_chudian13_14 = dianxi;
            //        fzcdtimes_static.HFZ13_14 = CT_HF;
            //        break;
            //    case "15 - 16":
            //        fzcdtimes_static.hfz_chudian15_16 = dianxi;
            //        fzcdtimes_static.HFZ15_16 = CT_HF;
            //        break;

            //    case "17 - 16":
            //        fzcdtimes_static.hfz_chudian16_17 = dianxi;
            //        fzcdtimes_static.HFZ16_17 = CT_HF;
            //        break;

            //    case "17 - 18":
            //        fzcdtimes_static.hfz_chudian17_18 = dianxi;
            //        fzcdtimes_static.HFZ17_18 = CT_HF;
            //        break;
            //    case "19 - 18":
            //        fzcdtimes_static.hfz_chudian18_19 = dianxi;
            //        fzcdtimes_static.HFZ18_19 = CT_HF;
            //        break;

            //    case "19 - 20":
            //        fzcdtimes_static.hfz_chudian19_20 = dianxi;
            //        fzcdtimes_static.HFZ19_20 = CT_HF;
            //        break;

            //    case "29 - 20":
            //        fzcdtimes_static.hfz_chudian20_29 = dianxi;
            //        fzcdtimes_static.HFZ20_29 = CT_HF;
            //        break;


            //    case "21 - 22":
            //        fzcdtimes_static.hfz_chudian21_22 = dianxi;
            //        fzcdtimes_static.HFZ21_22 = CT_HF;
            //        break;

            //    case "23 - 24":
            //        fzcdtimes_static.hfz_chudian23_24 = dianxi;
            //        fzcdtimes_static.HFZ23_24 = CT_HF;
            //        break;

            //    case "25 - 26":
            //        fzcdtimes_static.hfz_chudian25_26 = dianxi;
            //        fzcdtimes_static.HFZ25_26 = CT_HF;
            //        break;

            //    case "27 - 28":
            //        fzcdtimes_static.hfz_chudian27_28 = dianxi;
            //        fzcdtimes_static.HFZ27_28 = CT_HF;
            //        break;
            //    case "29 - 30":
            //        fzcdtimes_static.hfz_chudian29_30 = dianxi;
            //        fzcdtimes_static.HFZ29_30 = CT_HF;
            //        break;


            //    case "31 - 30":
            //        fzcdtimes_static.hfz_chudian30_31 = dianxi;
            //        fzcdtimes_static.HFZ30_31 = CT_HF;
            //        break;
            //    case "31 - 32":
            //        fzcdtimes_static.hfz_chudian31_32 = dianxi;
            //        fzcdtimes_static.HFZ31_32 = CT_HF;
            //        break;

            //    case "33 - 32":
            //        fzcdtimes_static.hfz_chudian32_33 = dianxi;
            //        fzcdtimes_static.HFZ32_33 = CT_HF;
            //        break;
            //    case "33 - 34":
            //        fzcdtimes_static.hfz_chudian33_34 = dianxi;
            //        fzcdtimes_static.HFZ33_34 = CT_HF;
            //        break;

            //    case "35 - 34":
            //        fzcdtimes_static.hfz_chudian34_35 = dianxi;
            //        fzcdtimes_static.HFZ34_35 = CT_HF;
            //        break;

            //    case "01 - 05":
            //        fzcdtimes_static.hfz_chudian1_5 = dianxi;
            //        fzcdtimes_static.HFZ1_5 = CT_HF;
            //        break;

            //    case "01 - 08":
            //        fzcdtimes_static.hfz_chudian1_8 = dianxi;
            //        fzcdtimes_static.HFZ1_8 = CT_HF;
            //        break;

            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toRefreshData();
        }
    }
}
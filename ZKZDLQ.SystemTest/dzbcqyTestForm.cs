using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Automation.BDaq;

namespace ZKZDLQ.SystemTest
{
    public partial class dzbcqyTestForm : Form
    {
        ErrorCode err;
        public bool iftest_zddzqy = true;
        UC_TestFrom ref_UC_TestFrom = null;
        double[] m_dataScaled;
        public bool ifrun = true;
        public dzbcqyTestForm(UC_TestFrom ttUC_TestFrom)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            ref_UC_TestFrom = ttUC_TestFrom;
            if (iftest_zddzqy)
                Text = "最低动作气压测试窗口";
            else
                Text = "最低保持气压测试窗口";

            if (iftest_zddzqy)
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低动作气压置位");
            else
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低保持气压置位");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txt_QiYa.Text = ref_UC_TestFrom.xsqyStr;
        }
        delegate void UpdateUIDelegate();

        private void bufferedAiCtrl1_Stopped(object sender, Automation.BDaq.BfdAiEventArgs e)
        {
            try
            {
                bufferedAiCtrl1.GetData(e.Count, m_dataScaled);
                this.Invoke((UpdateUIDelegate)delegate()
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    if (iftest_zddzqy)
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["主触头"].Points.AddXY(i, m_dataScaled[i * 5]);
                            chart1.Series["合闸"].Points.AddXY(i, m_dataScaled[i * 5 + 1]);
                            //chart1.Series["位移"].Points.AddXY(i, m_dataScaled[i * 5 + 4]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            chart1.Series["主触头"].Points.AddXY(i, m_dataScaled[i * 5]);
                            chart1.Series["分闸"].Points.AddXY(i, m_dataScaled[i * 5 + 1]);
                            //chart1.Series["位移"].Points.AddXY(i, m_dataScaled[i * 5 + 4]);
                        }
                    }
                    if (ifrun)
                    {
                        err = bufferedAiCtrl1.Prepare();
                        if (err == ErrorCode.Success)
                        {
                            bufferedAiCtrl1.Start();
                        }
                    }
                    else
                    {
                        bufferedAiCtrl1.Release();
                    }
                });
            }
            catch (Exception eee)
            { }
        }

        private void dzbcqyTestForm_Load(object sender, EventArgs e)
        {
            m_dataScaled = new double[bufferedAiCtrl1.BufferCapacity];
            err = ErrorCode.Success;
            err = bufferedAiCtrl1.Prepare();
            if (err == ErrorCode.Success)
            {
                err = bufferedAiCtrl1.Start();
                if (iftest_zddzqy)
                {
                    ref_UC_TestFrom.control.合闸置位();
                    System.Threading.Thread.Sleep(100);
                    ref_UC_TestFrom.control.合闸复位();
                }
                else
                {
                    ref_UC_TestFrom.control.分闸置位();
                    System.Threading.Thread.Sleep(100);
                    ref_UC_TestFrom.control.分闸复位();
                }
            }
            if (err != ErrorCode.Success)
            {
                MessageBox.Show("主触头及位移采集卡启动不成功！");
                return;
            }
        }

        private void openQYButton_Click(object sender, EventArgs e)
        {
            ifrun = false;
            
            timer1.Enabled = false;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (iftest_zddzqy)
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低动作气压置位");
            else
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低保持气压置位");
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            if (iftest_zddzqy)
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低动作气压复位");
            else
                ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低保持气压复位");
        }

        private void dzbcqyTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低动作气压复位");
            ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低保持气压复位");
            ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低动作气压复位");
            ref_UC_TestFrom.control.IComOmron.ExcuteCommand("最低保持气压复位");
        }
    }
}

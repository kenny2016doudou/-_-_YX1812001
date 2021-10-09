using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace TestDemoForm.YB.TianChen
{
    static class Program
    {
 
    }
    public partial class frmTestDemo : Form
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmTestForm());
        }

       
        TianChengYB tc = new TianChengYB();

        private decimal jldyValue = 0;
        private decimal zldyValue = 0;
        private decimal zjdyValue = 0;

        public frmTestDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读设置值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTestDemo_Load(object sender, EventArgs e)
        {
            OpenPort(); //打开通信端口
            jldyValue = Convert.ToDecimal(tc.ReadParameter("$0201", SP_YB));//$0201 ->02：仪表地址，01：参数值地址
            nudJLDY.Value = jldyValue;

            zldyValue = Convert.ToDecimal(tc.ReadParameter("$0301", SP_YB));
            nudZLDY.Value = zldyValue;
        }

        /// <summary>
        /// 打开串口通行
        /// </summary>
        private void OpenPort()
        {
            try
            {
                //通讯控件设置
                SP_YB.PortName = "COM3"; //仪表使用的COM口号
                SP_YB.StopBits = StopBits.One;
                SP_YB.Parity = Parity.None;
                SP_YB.BaudRate = 9600;
                SP_YB.DataBits = 8;

                SP_YB.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "!请重新打开");
                this.Close();
            }
        }


        /// <summary>
        /// 设置用户输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {

            tc.WriteParameter("02", "01+" + (Convert.ToInt32(nudJLDY.Value * 10).ToString("0000")), SP_YB);
            
            tc.WriteParameter("03", "01+" + (Convert.ToInt32(nudZLDY.Value * 10).ToString("0000")), SP_YB);
            
        }

        /// <summary>
        /// 实时读取仪表显示值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_YB_Tick(object sender, EventArgs e)
        {
            SP_YB.Write(tc.SendStr("#02"));//02：仪表地址
            System.Threading.Thread.Sleep(20);
            ledJLNY.Text = tc.GetSend(SP_YB.ReadExisting()).ToString();
            SP_YB.Write(tc.SendStr("#03"));
            System.Threading.Thread.Sleep(20);
            ledZLNY.Text = tc.GetSend(SP_YB.ReadExisting()).ToString();
            SP_YB.Write(tc.SendStr("#05"));
            System.Threading.Thread.Sleep(20);
            ledXLDL.Text = tc.GetSend(SP_YB.ReadExisting()).ToString();
            SP_YB.Write(tc.SendStr("#06"));
            System.Threading.Thread.Sleep(20);
            ledJSB.Text = tc.GetSend(SP_YB.ReadExisting()).ToString();

        }

      
    }
}

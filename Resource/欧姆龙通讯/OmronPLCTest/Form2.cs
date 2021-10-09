using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace OmronPLCTest
{
    public partial class Form2 : Form
    {
        SerialPort sp;
        public Form2()
        {
            InitializeComponent();
            sp = new SerialPort("COM5",2400,Parity.None,8,StopBits.One);
            sp.Open();
            textBox1.AppendText(sp.ReadExisting() + "\r\n");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(30);
            string response=sp.ReadExisting();
            textBox1.AppendText( response+"\r\n");
            if (response.Length == 11)//代表压力测试仪的数据发送完整
            {
                textBox7.Text = MPA(response);
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
        }
        //10000202372  压力测试仪最大量程是2500KP(2.5MPA)
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(sp.ReadExisting() + "\r\n");
        }

        /// <summary>
        /// 压力换算
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string MPA(string data)
        {
            string value = "";
            try
            {
                
                value = data.Substring(6, data.Length - 6);
                double PaValue = double.Parse(value);
                value = (PaValue / 10000).ToString();
              
            }
            catch
            {
 
            }
            return value;
        }

    }
}

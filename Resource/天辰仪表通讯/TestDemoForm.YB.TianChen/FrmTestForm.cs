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
    public partial class FrmTestForm : Form
    {
        SerialPort sp;
        TianChengYB tc = new TianChengYB();
        public FrmTestForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 发送读取命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(tc.SendStr(textBox1.Text));//02：仪表地址
            //sp.Write(textBox1.Text+(char)13);
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text=tc.GetSend(sp.ReadExisting()).ToString();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp == null)
                {
                    sp = new SerialPort();
                    Parity parity;
                    switch (comboBox2.Text)
                    {
                        case "Even":
                            parity = Parity.Even;
                            break;
                        case "Mark":
                            parity = Parity.Mark;
                            break;
                        case "None":
                            parity = Parity.None;
                            break;
                        case "Odd":
                            parity = Parity.Odd;
                            break;
                        case "Space":
                            parity=Parity.Space;
                            break;
                        default:
                            parity=Parity.None;
                            break;
                    }
                    
                    StopBits stopbits;
                    switch(comboBox5.Text)
                    {
                        case "None":
                            stopbits=StopBits.None;
                            break;
                        case "One":
                            stopbits=StopBits.One;
                            break;
                        case "OnePointFive":
                            stopbits=StopBits.OnePointFive;
                            break;
                        case "Two":
                            stopbits=StopBits.Two;
                            break;
                        default:
                            stopbits=StopBits.One;
                            break;
                    }
                    sp.Parity=parity;
                    sp.StopBits=stopbits;
                    sp.PortName=comboBox1.Text;
                    sp.BaudRate=int.Parse(comboBox3.Text);
                    sp.DataBits=int.Parse(comboBox4.Text);
                    if(!sp.IsOpen)
                    {
                        sp.Open();
                        button2.Enabled=false;
                        button3.Enabled=true;
                        groupBox1.Enabled = button3.Enabled;
                        groupBox2.Enabled = button3.Enabled;
                    }
                }
            }
            catch
            {
                sp = null;
            }
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (sp != null&&sp.IsOpen)
            {
                sp.Dispose();
                sp = null;
                button2.Enabled = true;
                button3.Enabled = false;
                groupBox1.Enabled = button3.Enabled;
                groupBox2.Enabled = button3.Enabled;
 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Command.ControlCenter;
using System.IO.Ports;

namespace YD_YBTest
{
    public partial class Form2 : Form
    {
        public IComControl Com = null;//操作对象

        private SerialPort sp;//串口对象

        private string[] Command = new string[50];//地址集合
       
        public Form2()
        {
            InitializeComponent();
            Init();
            LoadData();
        }

        void Init()
        {
            sp=new SerialPort();
            if (!this.sp.IsOpen)
            {
                try
                {
                    TestSystem.Command.YB.YuDian.YuDian_Port.getInstance().OpenPort(sp, "COM3", StopBits.One, Parity.None, 9600, 8);
                }
                catch
                {

                }
            }

            Command[0] = "1#01";
            Command[1] = "2#01";
            Command[2] = "3#01";
        }

        void LoadData()
        {
            Com = ComControl_Factory.getInstance().CreateComControl(ref sp);

            //==========读取PV
            TestSystem.Command.YB.YuDian.ReadPV_YuDianYB R02_01 = new TestSystem.Command.YB.YuDian.ReadPV_YuDianYB(Command[0]);
            Com.SetReader("02#01", R02_01);

            //TestSystem.Command.YB.YuDian.ReadPV_YuDianYB R03_01 = new TestSystem.Command.YB.YuDian.ReadPV_YuDianYB(Command[1]);
            //Com.SetReader("03#01", R03_01);

            //TestSystem.Command.YB.YuDian.ReadPV_YuDianYB R04_01 = new TestSystem.Command.YB.YuDian.ReadPV_YuDianYB(Command[2]);
            //Com.SetReader("04#01", R04_01);

            ////===========读取SV
            //TestSystem.Command.YB.YuDian.ReadSV_YuDianYB R02_01SV = new TestSystem.Command.YB.YuDian.ReadSV_YuDianYB(Command[0]);
            //Com.SetReader("02#01SV", R02_01SV);

            //TestSystem.Command.YB.YuDian.ReadSV_YuDianYB R03_01SV = new TestSystem.Command.YB.YuDian.ReadSV_YuDianYB(Command[1]);
            //Com.SetReader("03#01SV", R03_01SV);

            //TestSystem.Command.YB.YuDian.ReadSV_YuDianYB R04_01SV = new TestSystem.Command.YB.YuDian.ReadSV_YuDianYB(Command[2]);
            //Com.SetReader("04#01SV", R04_01SV);

            ////写入
            //TestSystem.Command.YB.YuDian.Write_YuDianYB W02_01 = new TestSystem.Command.YB.YuDian.Write_YuDianYB(Command[0]);
            //Com.SetCommand("W02#01", W02_01);

            Com.Start();

        }

        private void PLC_Tick(object sender, EventArgs e)
        {
            try
            {
               label4.Text=  Convert.ToInt32( Com.GetReader("02#01").ToString(),16).ToString();
               //label5.Text = Convert.ToInt32(Com.GetReader("03#01").ToString(),16).ToString();
               //label6.Text = Convert.ToInt32(Com.GetReader("04#01").ToString(), 16).ToString();

               //label9.Text = Convert.ToInt32(Com.GetReader("02#01SV").ToString(), 16).ToString();
               //label8.Text = Convert.ToInt32(Com.GetReader("03#01SV").ToString(), 16).ToString();
               //label7.Text = Convert.ToInt32(Com.GetReader("04#01SV").ToString(), 16).ToString();
            }
            catch
            {
 
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            sp.Dispose();
            
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Com.GetWriteDataCommand("W02#01").StrData = textBox1.Text;
            Com.ExcuteCommand("W02#01");

        }
    }
}

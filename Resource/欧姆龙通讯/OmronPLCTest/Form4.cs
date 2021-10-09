using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Command.PLC.Omron.HostLink.Fins;
using System.IO.Ports;
using TestSystem.Command.ControlCenter;

namespace OmronPLCTest
{
    public partial class Form4 : Form
    {
        OmronStaticControl control;
        SerialPort sp;
        public Form4()
        {
            InitializeComponent();
            sp = new SerialPort();
            control = new OmronStaticControl(ref sp);
            control.LoadData();
        }

        private void Omron_PLC_Tick(object sender, EventArgs e)
        {
           lblD0.Text=  control.Com.GetReader("读取D0位置").ToString();
           //lblD1_D5.Text = control.Com.GetReader("读取D1-D5位置")
           lblD1_D5.Text = "";
           if (control.Com.GetReaderIData("读取D1-D5位置") != null)
           {
               foreach (var s in control.Com.GetReaderIData("读取D1-D5位置"))
               {
                   lblD1_D5.Text += s.ToString() + "-";
               }
           }
           label1.Text = control.Com.GetReader("W0_01状态").ToString();

           richTextBox1.Text = "";
           if (control.Com.GetReaderIData("100状态") != null)
           {
               Dictionary<string, string> s = (Dictionary<string,string>)control.Com.GetReaderIData("100状态")[0];
               //foreach (var val in s)
               //{
               //    richTextBox1.Text +=string.Format("{0} ={1} {2}",val.Key,val.Value,Environment.NewLine);
               //}
               richTextBox1.Text += string.Format("{0}", s["I100.00状态"]);

           }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.Com.GetWriteDataCommand("写入D0位置").StrData = textBox1.Text;
            control.Com.ExcuteCommand("写入D0位置");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control.Com.GetWriteDataCommand("写入D1-D5位置").StrData = textBox2.Text;
            control.Com.ExcuteCommand("写入D1-D5位置");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.Com.ExcuteCommand("W0_01摁住");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.Com.ExcuteCommand("W0_01复位");

        }

        int i = 0;
        private void button5_Click(object sender, EventArgs e)
        {
           

        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            Omron_ClickDown write = new Omron_ClickDown();
            DataType dtype = DataType.Bit; 
            write.ExcuteUndo("W2.03", sp, dtype, true);
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            Omron_ClickUp write = new Omron_ClickUp();
            DataType dtype = DataType.Bit;
            write.ExcuteUndo("W2.03", sp, dtype, false);
        }


    }
}

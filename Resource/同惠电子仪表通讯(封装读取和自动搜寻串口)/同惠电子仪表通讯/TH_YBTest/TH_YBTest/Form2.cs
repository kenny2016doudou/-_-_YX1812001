using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.Command.Interface;
using System.IO.Ports;
using TestSystem.Command.YB.TongHui;
using TestSystem.Command.ControlCenter;
namespace TH_YBTest
{
    public partial class Form2 : Form
    {
        StaticControl control;
        SerialPort sp;
        public Form2()
        {
            InitializeComponent();
            sp = new SerialPort();
            control = new StaticControl(ref sp);
            control.LoadData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label7.Text = control.ICom.GetReaderIData("读取仪表")[1].ToString();
            }
            catch
            {
 
            }
        }
    }
}

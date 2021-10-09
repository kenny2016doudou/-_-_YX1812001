using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using TestSystem.Command.YB.TongHui;
namespace TH_YBTest
{
    public partial class Form1 : Form
    {
        SerialPort sp;
        public Form1()
        {
            InitializeComponent();
           
        }
        //一起采用9600波特率,无奇偶校验,1位起始位,8位数据位,1位停止位.
        protected override void OnLoad(EventArgs e)
        {
            cboxCom.Text = TongHuiYB_SerialPort.GetTongHuiCOM(9600, Parity.None, 8, StopBits.One);
            base.OnLoad(e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp == null)
                {
                    sp = new SerialPort();
                    Parity parity;
                    switch (cboxParity.Text)
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
                            parity = Parity.Space;
                            break;
                        default:
                            parity = Parity.None;
                            break;
                    }

                    StopBits stopbits;
                    switch (cboxStopBits.Text)
                    {
                        case "None":
                            stopbits = StopBits.None;
                            break;
                        case "One":
                            stopbits = StopBits.One;
                            break;
                        case "OnePointFive":
                            stopbits = StopBits.OnePointFive;
                            break;
                        case "Two":
                            stopbits = StopBits.Two;
                            break;
                        default:
                            stopbits = StopBits.One;
                            break;
                    }
                    sp.Parity = parity;
                    sp.StopBits = stopbits;
                    sp.PortName = cboxCom.Text;
                    sp.BaudRate = int.Parse(cboxRate.Text);
                    sp.DataBits = int.Parse(cboxDataBit.Text);
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                        btnOpen.Enabled = false;
                        btnClose.Enabled = true;
                        gboxR.Enabled = btnClose.Enabled;
                        gboxR.Enabled = btnClose.Enabled;

                    }
                }
            }
            catch
            {
                sp = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (sp != null && sp.IsOpen)
            {
                sp.Dispose();
                sp = null;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                gboxR.Enabled = btnClose.Enabled;
                gboxR.Enabled = btnClose.Enabled;
                TH_TIMER.Enabled = btnClose.Enabled;

            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //TH_TIMER.Enabled = true;
            if (sp != null)
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(txtRaddr.Text + "\n");
                System.Threading.Thread.Sleep(500);
                txtRS.Text = sp.ReadExisting();
            }
        }

        private void TH_TIMER_Tick(object sender, EventArgs e)
        {
            if (sp != null)
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(txtRaddr.Text+"\n");
                //System.Threading.Thread.Sleep(200);
                txtRS.Text = sp.ReadExisting();
            }
        }



    }
}

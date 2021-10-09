using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementSpecificTools.PlcConnectivity;

namespace ManagementSpecificTools
{
    public partial class CommStateCtrl : UserControl
    {
        public CommStateCtrl()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Plc.ifConnected)
            {
                this.BackColor = Color.Lime;
                toolTip1.SetToolTip(this, "已连接且工作正常!");
            }
            else
            {
                this.BackColor = Color.Red;
                toolTip1.SetToolTip(this, "未连接或者不能稳定通信!");
            }
        }

        private void CommStateCtrl_Click(object sender, EventArgs e)
        {
            //if (Plc.ifConnected)
            //{
            //    toolTip1.Show("已连接且工作正常!", this);
            //}
            //else
            //{
            //    toolTip1.Show("未连接或者不能稳定通信!", this);
            //}
        }

    }
}

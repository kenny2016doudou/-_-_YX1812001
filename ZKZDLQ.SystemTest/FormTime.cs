using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZKZDLQ.SystemTest
{
    public partial class FormTime : Form
    {
        private int maxvalue = 0;
        private UC_TestFrom ref_UC_TestFrom;
        private bool ifjz = false;
        public FormTime(string p_desc,UC_TestFrom p_UC_TestFrom)
        {
            InitializeComponent();
            label1.Text = p_desc;
            ref_UC_TestFrom = p_UC_TestFrom;
        }
        public string allTime
        {
            set
            {
                this.l.Text = value;
                maxvalue = int.Parse(this.l.Text);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(l.Text);
            if (t <= 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                l.Text = (t - 1).ToString();
                if (t < (maxvalue - 5) && !ifjz)
                {
                    ifjz = true;
                    ref_UC_TestFrom.control.断开气源();
                    ref_UC_TestFrom.control.停止充气置位();
                    ref_UC_TestFrom.control.保压置位();
                }
            }
        }

        private void btnOFF2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

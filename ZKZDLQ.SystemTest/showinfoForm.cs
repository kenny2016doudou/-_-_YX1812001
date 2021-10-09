using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using usefulClass;

namespace ZKZDLQ.SystemTest
{
    public partial class showinfoForm : Form
    {
        public showinfoForm(xhdyClass p_xhdyClass)
        {
            InitializeComponent();
            this.Text = p_xhdyClass.xhnames + "  " + p_xhdyClass.dkway;
            textBox1.Text = p_xhdyClass.dkInfo;
        }

        private void showinfoForm_Load(object sender, EventArgs e)
        {

        }

        private void showinfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Visible = false;
                e.Cancel = true;
            }
        }
    }
}

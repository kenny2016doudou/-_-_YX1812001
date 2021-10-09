using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHDC_XDCTestForm
{
    public partial class UC_lable : UserControl
    {
        public UC_lable()
        {
            InitializeComponent();
        }

        public string MyTitle
        {
            get { return this.lbl_title.Text; }
            set { this.lbl_title.Text = value; }
        }


        public string MyUnit
        {
            get { return this.lbl_unit.Text; }
            set { this.lbl_unit.Text ="（" +value+"）"; }
        }

        public string MyMainData
        {
            get { return this.txt_maindata.Text; }
            set { this.txt_maindata.Text =  value ; }
        }
      
    }
}

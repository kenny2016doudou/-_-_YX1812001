﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestManage
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
            //skinEngine1.SkinFile = ZZ.Control.StaticControl.SkinName;
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\NYJ\help.htm");
        }
    }
}
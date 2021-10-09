using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace TestSystem.FormManage
{
    public partial class frmMain : Form
    {
        private Form parentfrm;
        public frmMain(Form parentfrm)
        {
            InitializeComponent();
            this.parentfrm = parentfrm;
           // this.skinEngine1.SkinFile = ZZ.Control.StaticControl.SkinName;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {            
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picTest_Click(object sender, EventArgs e)
        {
           // SHDC_YWTestForm.frmNewTest fmt = new SHDC_YWTestForm.frmNewTest();
            
            //fmt.Show();
            SHDC_SDCGQTestFrom.frmSDCGQTest ft = new SHDC_SDCGQTestFrom.frmSDCGQTest();
            ft.ShowDialog();

        }

        private void picDataQuery_Click(object sender, EventArgs e)
        {
            frmData fd = new frmData(this);
            fd.Show();
            this.Hide();
        }

        private void picSystemSet_Click(object sender, EventArgs e)
        {
            frmSystemSet fss = new frmSystemSet(this);
            fss.Show();
            this.Hide();
        }

        private void picUserManual_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentfrm.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}

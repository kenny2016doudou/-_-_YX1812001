using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TestSystem.BusinessRule.Interface.SchemeModule;
using TestSystem.BusinessRule.SchemeModule;

namespace SHDC_XDCTestForm
{
    public partial class NewTest : Form
    {
        ISchemeRowBO schemeRowBO = new TestSystem.BusinessRule.SchemeModule.SchemeRowBO();
        public string retStr = "";
        XDCManager xdc = null;

        public NewTest()
        {
            InitializeComponent();
            xdc = XDCManager.getInstance();
        }

        private void NewTest_Load(object sender, EventArgs e)
        {
            DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", "", "", "");
            for (int i = 0; i < ds2.Rows.Count; i++)
            {
                if (ds2.Rows[i]["蓄电池型号"].ToString().Contains("电池组"))
                {
                    if (!comboBox1.Items.Contains(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", "")))
                    {
                        comboBox1.Items.Add(ds2.Rows[i]["蓄电池型号"].ToString().Replace("电池组", ""));
                    }
                }
                if (ds2.Rows[i]["蓄电池型号"].ToString().Contains("单节电池"))
                {
                    if (!comboBox1.Items.Contains(ds2.Rows[i]["蓄电池型号"].ToString().Replace("单节电池", "")))
                    {
                        comboBox1.Items.Add(ds2.Rows[i]["蓄电池型号"].ToString().Replace("单节电池", ""));
                    }
                }
            }
            timer1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ds2 = schemeRowBO.SelectData("Config", "蓄电池型号", comboBox1.Text.Trim() + "电池组", "", "");
            try
            {
                this.txt_cddl1.Text = ds2.Rows[0]["充电电流设置"].ToString();
                this.txt_cdsj1.Text = ds2.Rows[0]["充电时间设置"].ToString();
                this.txt_fdsj1.Text = ds2.Rows[0]["放电时间设置"].ToString();
                this.txt_fddl1.Text = ds2.Rows[0]["放电电流设置"].ToString();

                this.txt_qsdy1.Text = ds2.Rows[0]["起始电压设置"].ToString();
                this.txt_cdzzdy1.Text = ds2.Rows[0]["充电终止电压设置"].ToString();
                this.txt_fdzzdy1.Text = ds2.Rows[0]["放电终止电压设置"].ToString();
                this.textBox19.Text = ds2.Rows[0]["循环次数设置"].ToString();
            }
            catch
            {
            }

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            retStr = comboBox1.Text.Trim();
            if (retStr.Length > 0)
            {
                ISchemeRowBO _SchemeRowBO_BaseInfo = new SchemeRowBO();
                DataTable dt = _SchemeRowBO_BaseInfo.SelectData("Config", "蓄电池型号", retStr + "电池组", "", "");
                xdc.P1_充电电流设置值 = dt.Rows[0]["充电电流设置"].ToString();
                xdc.P1_充电时间设置值 = dt.Rows[0]["充电时间设置"].ToString();
                xdc.P1_充电终止电压设置值 = dt.Rows[0]["充电终止电压设置"].ToString();
                xdc.P1_放电电流设置值 = dt.Rows[0]["放电电流设置"].ToString();
                xdc.P1_放电时间设置值 = dt.Rows[0]["放电时间设置"].ToString();
                xdc.P1_放电终止电压设置值 = dt.Rows[0]["放电终止电压设置"].ToString();
                xdc.P1_循环次数设置值 = dt.Rows[0]["循环次数设置"].ToString();
                xdc.P1_起始电压设置值 = dt.Rows[0]["起始电压设置"].ToString();
            }
            Thread.Sleep(1000);
            this.DialogResult = DialogResult.OK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tempstr = xdc.P1_充电电流设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_cddl2.Text = (float.Parse(tempstr)).ToString("0.0");

            tempstr = xdc.P1_充电时间设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_cdsj2.Text = tempstr;

            tempstr = xdc.P1_充电终止电压设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_cdzzdy2.Text = (float.Parse(tempstr)).ToString("0.0");

            tempstr = xdc.P1_放电电流设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_fddl2.Text = (float.Parse(tempstr)).ToString("0.0");

            tempstr = xdc.P1_放电时间设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_fdsj2.Text = tempstr;

            tempstr = xdc.P1_放电终止电压设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_fdzzdy2.Text = (float.Parse(tempstr)).ToString("0.0");

            tempstr = xdc.P1_起始电压设置值;
            if (tempstr != null && tempstr.Length > 0)
                txt_qsdy2.Text = (float.Parse(tempstr)).ToString("0.0");

            tempstr = xdc.P1_循环次数设置值;
            if (tempstr != null && tempstr.Length > 0)
                textBox18.Text = (float.Parse(tempstr)).ToString("0.0");
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

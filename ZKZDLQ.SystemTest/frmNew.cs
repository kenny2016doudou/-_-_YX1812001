using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SchemeManage.BusinessRule.Interface.SchemeModule;
using SchemeManage.BusinessRule.SchemeModule;
using System.Collections;
using usefulClass;
using TestSystem.Common.Constant;
using System.IO;

namespace ZKZDLQ.SystemTest
{
    public partial class frmNew : CSharpWin.SkinForm
    {
        ISchemeRowBO schemeRowBO = new SchemeRowBO();
        public string[] argColName;
        public object[] argCloContent;
        public frmNew()
        {
            InitializeComponent();
            GetInfo();
            comboBox2.Text = TestSystem.Common.Constant.StaticModule.UserName; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Length <= 0 || comboBox5.Text.Length <= 0)
                {
                    MessageBox.Show("主断路器型号和编号不能为空！");
                    return;
                }
                argColName = new string[9];
                argCloContent = new object[9];
                argColName[0] = "主断路器型号";
                argCloContent[0] = comboBox1.Text;

                //TestSystem.Command.PLC.Omron.HostLink.Fins.StaticDataClass.sb_xinghao = comboBox1.Text;

                argColName[1] = "主断路器编号";
                argCloContent[1] = comboBox5.Text;

                //TestSystem.Command.PLC.Omron.HostLink.Fins.StaticDataClass.sb_bianhao = comboBox5.Text;
                argColName[2] = "检测人员";
                argCloContent[2] = comboBox2.Text;

                argColName[3] = "检测时间";
                argCloContent[3] = dp_检测日期.Value.ToString();

                argColName[4] = "车号";
                argCloContent[4] = comboBox3.Text;

                argColName[5] = "配属";
                argCloContent[5] = comboBox4.Text;

                argColName[6] = "给电状态";
                argCloContent[6] = "";


                int remCount = 0;
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\tempbh.dat", false);
                if (!comboBox5.Items.Contains(comboBox5.Text.Trim()) && comboBox5.Text.Trim().Length > 0)
                {
                    remCount++;
                    sw.WriteLine(comboBox5.Text.Trim());
                }
                for (int i = 0; i < comboBox5.Items.Count; i++)
                {
                    tempstr = comboBox5.Items[i].ToString().Trim();
                    if (tempstr.Length > 0)
                    {
                        remCount++;
                        sw.WriteLine(tempstr);
                    }
                    if (remCount >= 10)
                        break;
                }
                sw.Close();

                remCount = 0;
                sw = new StreamWriter(Application.StartupPath + "\\tempps.dat", false);
                if (!comboBox4.Items.Contains(comboBox4.Text.Trim()) && comboBox4.Text.Trim().Length > 0)
                {
                    remCount++;
                    sw.WriteLine(comboBox4.Text.Trim());
                }
                for (int i = 0; i < comboBox4.Items.Count; i++)
                {
                    tempstr = comboBox4.Items[i].ToString().Trim();
                    if (tempstr.Length > 0)
                    {
                        remCount++;
                        sw.WriteLine(tempstr);
                    }
                    if (remCount >= 10)
                        break;
                }
                sw.Close();

                //schemeRowBO.SaveData("BaseInfo", argColName, argCloContent, false);
                this.FindForm().DialogResult = DialogResult.OK;
            }
            catch
            {
            }
        }

        public void GetInfo()
        {
            object ttobj = StaticDataClass.toDeserialize(Application.StartupPath + "\\xh.dat");
            if (ttobj == null)
            {
                ArrayList xhallArray = new ArrayList();
                StaticDataClass.toSerialize(xhallArray, Application.StartupPath + "\\xh.dat");
            }
            else
            {
                ArrayList xhallArray = (ArrayList)ttobj;
                for (int i = 0; i < xhallArray.Count; i++)
                {
                    comboBox1.Items.Add(((xhdyClass)xhallArray[i]).xhnames);
                }
            }
            try
            {
                DataTable dt = schemeRowBO.SelectData("baseinfo");
                comboBox2.DataSource = (from baseinfo in dt.Copy().AsEnumerable() select baseinfo).GroupBy(p => p.Field<string>("检测人员")).ToList();
                comboBox2.DisplayMember = "key";
                comboBox2.ValueMember = "key";
                if (comboBox2.Items.Count <= 0)
                {
                    comboBox2.DataSource = null;
                    comboBox2.Items.Add(StaticModule.UserName);
                }


                dt = schemeRowBO.SelectData("baseinfo");
                comboBox3.DataSource = (from baseinfo in dt.Copy().AsEnumerable() select baseinfo).GroupBy(p => p.Field<string>("车型号")).ToList();
                comboBox3.DisplayMember = "key";
                comboBox3.ValueMember = "key";
                if (comboBox3.Items.Count <= 0)
                {
                    comboBox3.DataSource = null;
                    comboBox3.Items.Add(StaticModule.UserName);
                }
            }
            catch
            {
                //意外处理
            }
            if (File.Exists(Application.StartupPath + "\\tempbh.dat"))
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\tempbh.dat");
                while (!sr.EndOfStream)
                {
                    tempstr = sr.ReadLine().Trim();
                    if (tempstr.Length > 0 && !comboBox5.Items.Contains(tempstr))
                        comboBox5.Items.Add(tempstr);
                }
                sr.Close();
            }

            if (File.Exists(Application.StartupPath + "\\tempps.dat"))
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\tempps.dat");
                while (!sr.EndOfStream)
                {
                    tempstr = sr.ReadLine().Trim();
                    if (tempstr.Length > 0 && !comboBox4.Items.Contains(tempstr))
                        comboBox4.Items.Add(tempstr);
                }
                sr.Close();
            }
        }

        private string tempstr = "";
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmNew_Load(object sender, EventArgs e)
        {

        }

    }
}

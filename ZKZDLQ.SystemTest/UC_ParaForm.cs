using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TestSystem.Command.PLC.Omron.HostLink.Fins;
using usefulClass;

namespace ZKZDLQ.SystemTest
{
    public partial class UC_ParaForm : UserControl
    {
        public ArrayList xhallArray = new ArrayList();
        public xhdyClass runxhdyClass = null;

        public UC_ParaForm()
        {
            InitializeComponent();
            initcdStates(true);
            initcdStates(false);
            initcdStatess(true);
            initcdStatess(false);

            object ttobj = StaticDataClass.toDeserialize(Application.StartupPath + "\\xh.dat");
            if (ttobj == null)
            {
                xhallArray = new ArrayList();
                StaticDataClass.toSerialize(xhallArray, Application.StartupPath + "\\xh.dat");
            }
            else
            {
                xhallArray = (ArrayList)ttobj;
                for (int i = 0; i < xhallArray.Count; i++)
                {
                    listBox1.Items.Add(((xhdyClass)xhallArray[i]).xhnames);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //保存参数
            this.FindForm().Dispose();
        }


        public void initcdStates(bool ifhz)
        {
          
        }
        public void initcdStatess(bool ifhz)
        {

        }
        void ttbt_Click(object sender, EventArgs e)
        {
            GlassButton.GlassButton ttbt = (GlassButton.GlassButton)sender;
            if (ttbt.BackColor == Color.Red)
                ttbt.BackColor = Color.LimeGreen;
            else
                ttbt.BackColor = Color.Red;
        }
        public void togetPara()
        {
            runxhdyClass.xhtypes = comboBox1.Text;
            runxhdyClass.xmcell01_maxValue = (float)numericUpDown2.Value;

            runxhdyClass.xmcell02_minValue = (float)numericUpDown3.Value;
            runxhdyClass.xmcell02_maxValue = (float)numericUpDown4.Value;

            runxhdyClass.xmcell03_minValue = (float)numericUpDown1.Value;
            runxhdyClass.xmcell03_maxValue = (float)numericUpDown7.Value;

            runxhdyClass.xmcell04_minValue = (float)numericUpDown5.Value;
            runxhdyClass.xmcell04_maxValue = (float)numericUpDown6.Value;

            runxhdyClass.xmcell05_maxValue = (float)numericUpDown9.Value;

            runxhdyClass.xmcell06_maxValue = (float)numericUpDown10.Value;

            runxhdyClass.xmcell07_minValue = (float)numericUpDown8.Value;
            runxhdyClass.xmcell07_maxValue = (float)numericUpDown15.Value;

            runxhdyClass.xmcell08_maxValue = (float)numericUpDown11.Value;
            //*****************************************************************辅助触点
            int ttindex = 1;
           // ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox2.Text);
         
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox2.Text;

            //((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox3.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox3.Text;

          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown12.Value;

        ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown12.Value; 
            ttindex = 2;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox4.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox4.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox5.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox5.Text;


            // ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown21.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown21.Value;
            ttindex = 3;
         //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox6.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox6.Text;

            //    ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox7.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox7.Text;
            //    ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown13.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown13.Value;
            ttindex = 4;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox8.Text);

           ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox8.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox9.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox9.Text;
            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown22.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown22.Value;
            ttindex = 5;
        //    ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox10.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox10.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox11.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox11.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown14.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown14.Value;
            ttindex = 6;
         //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox12.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox12.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox13.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox13.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown23.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown23.Value;
            ttindex = 7;
         //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox14.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox14.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox15.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox15.Text;
            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown16.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown16.Value;
            ttindex = 8;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox16.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox16.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox17.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox17.Text;
            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown24.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown24.Value;
            ttindex = 9;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox18.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox18.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox19.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox19.Text;
            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown17.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown17.Value;
            ttindex = 10;
           // ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox20.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox20.Text;

            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox21.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox21.Text;
            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown25.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown25.Value;
            ttindex = 11;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox22.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox22.Text;

            // ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox23.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox23.Text;
            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown18.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown18.Value;
            ttindex = 12;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox24.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox24.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox25.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox25.Text;
            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown26.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown26.Value;
            ttindex = 13;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox26.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox26.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox27.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox27.Text;
            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown19.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown19.Value;
            ttindex = 14;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox28.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox28.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox29.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox29.Text;
            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown27.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown27.Value;
            ttindex = 15;
          //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex = int.Parse(textBox30.Text);

            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion = textBox30.Text;

            //  ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex = int.Parse(textBox31.Text);
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion = textBox31.Text;
            //   ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex = (int)numericUpDown20.Value;
            ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion = (int)numericUpDown20.Value;
            runxhdyClass.ifwyTest = checkBox1.Checked;
            runxhdyClass.ifttTest = checkBox2.Checked;
            runxhdyClass.ifccTest = checkBox3.Checked;
            runxhdyClass.ifddTest = checkBox4.Checked;
            runxhdyClass.ifsdTest = checkBox5.Checked;

            runxhdyClass.dkway = comboBox2.Text;
            runxhdyClass.dkInfo = textBox33.Text;


            runxhdyClass.iffzxqTest = checkBox6.Checked;
            runxhdyClass.ifhzxqTest = checkBox7.Checked;
            runxhdyClass.ifzctTest = checkBox8.Checked;
            runxhdyClass.ifcbfzztTest = checkBox9.Checked;
            runxhdyClass.ifckfzztTest = checkBox10.Checked;

        }

        //增加
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
            {
                MessageBox.Show("增加断路器型号定义时,型号不能为空!");
                return;
            }
            runxhdyClass = new xhdyClass();
            runxhdyClass.xhnames = textBox1.Text;

            togetPara();
            xhallArray.Add(runxhdyClass);
            //hzfzcd_checkArray_String
            listBox1.Items.Add(runxhdyClass.xhnames);
            StaticDataClass.toSerialize(xhallArray, Application.StartupPath + "\\xh.dat");
        }
        //修改
        private void button3_Click(object sender, EventArgs e)
        {
            if (runxhdyClass == null)
            {
                MessageBox.Show("请先选定型号在进行修改操作!");
                return;
            }
            togetPara();
            for (int i = 0; i < xhallArray.Count; i++)
            {
                if (((xhdyClass)xhallArray[i]).xhnames.CompareTo(runxhdyClass.xhnames) == 0)
                {
                    xhallArray.RemoveAt(i);
                    xhallArray.Insert(i, runxhdyClass);
                    break;
                }
            }
            StaticDataClass.toSerialize(xhallArray, Application.StartupPath + "\\xh.dat");

        }
        //删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (runxhdyClass == null)
            {
                MessageBox.Show("请先选定型号在进行删除操作!");
                return;
            }
            for (int i = 0; i < xhallArray.Count; i++)
            {
                if (((xhdyClass)xhallArray[i]).xhnames.CompareTo(runxhdyClass.xhnames) == 0)
                {
                    xhallArray.RemoveAt(i);
                    listBox1.Items.RemoveAt(i);
                    textBox1.Text = "";
                    runxhdyClass = null;
                    break;
                }
            }
            StaticDataClass.toSerialize(xhallArray, Application.StartupPath + "\\xh.dat");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            runxhdyClass = (xhdyClass)xhallArray[listBox1.SelectedIndex];
            textBox1.Text = runxhdyClass.xhnames;
            comboBox1.Text = runxhdyClass.xhtypes;
            runxhdyClass.init();

            numericUpDown2.Value = (Decimal)runxhdyClass.xmcell01_maxValue;

            numericUpDown3.Value = (Decimal)runxhdyClass.xmcell02_minValue;
            numericUpDown4.Value = (Decimal)runxhdyClass.xmcell02_maxValue;

            numericUpDown1.Value = (Decimal)runxhdyClass.xmcell03_minValue;
            numericUpDown7.Value = (Decimal)runxhdyClass.xmcell03_maxValue;

            numericUpDown5.Value = (Decimal)runxhdyClass.xmcell04_minValue;
            numericUpDown6.Value = (Decimal)runxhdyClass.xmcell04_maxValue;

            numericUpDown9.Value = (Decimal)runxhdyClass.xmcell05_maxValue;

            numericUpDown10.Value = (Decimal)runxhdyClass.xmcell06_maxValue;

            numericUpDown8.Value = (Decimal)runxhdyClass.xmcell07_minValue;
            numericUpDown15.Value = (Decimal)runxhdyClass.xmcell07_maxValue;

            numericUpDown11.Value = (Decimal)runxhdyClass.xmcell08_maxValue;
            /**/
            int ttindex = 1;
            // textBox2.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox2.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox3.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox3.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown12.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown12.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;
            ttindex = 2;
            //textBox4.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox4.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox5.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox5.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown21.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown21.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 3;
            //textBox6.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox6.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox7.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox7.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown13.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown13.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 4;
            //textBox8.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox8.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox9.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox9.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown22.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown22.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 5;
            //textBox10.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox10.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox11.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox11.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown14.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown14.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;


            ttindex = 6;
            //textBox12.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox12.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox13.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox13.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown23.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown23.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 7;
            //textBox14.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox14.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox15.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox15.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown16.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown16.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 8;
            //textBox16.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox16.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox17.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox17.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown24.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown24.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 9;
            //textBox18.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox18.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox19.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox19.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown17.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown17.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 10;
            //textBox20.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox20.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox21.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox21.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown25.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown25.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 11;
            //textBox22.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox22.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox23.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox23.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown18.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown18.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;


            ttindex = 12;
            //textBox24.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox24.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox25.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox25.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown26.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown26.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 13;
            //textBox26.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox26.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox27.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox27.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown19.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown19.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 14;
            //textBox28.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox28.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox29.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox29.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown27.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown27.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            ttindex = 15;
            //textBox30.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).startzdIndex.ToString();
            textBox30.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).startzdIndex_Strion;
            //textBox31.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).endzdIndex.ToString();
            textBox31.Text = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).endzdIndex_Strion;
            //numericUpDown20.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray[ttindex - 1]).testChannelIndex;
            numericUpDown20.Value = ((fzcdCellClass)runxhdyClass.hzfzcd_checkArray_String[ttindex - 1]).testChannelIndex_Strion;

            checkBox1.Checked = runxhdyClass.ifwyTest;
            checkBox2.Checked = runxhdyClass.ifttTest;
            checkBox3.Checked = runxhdyClass.ifccTest;
            checkBox4.Checked = runxhdyClass.ifddTest;
            checkBox5.Checked = runxhdyClass.ifsdTest;

            textBox32.Text = runxhdyClass.reportName;
            comboBox2.Text = runxhdyClass.dkway;
            textBox33.Text = runxhdyClass.dkInfo;


            checkBox6.Checked = runxhdyClass.iffzxqTest;
            checkBox7.Checked = runxhdyClass.ifhzxqTest;
            checkBox8.Checked = runxhdyClass.ifzctTest;
            checkBox9.Checked = runxhdyClass.ifcbfzztTest;
            checkBox10.Checked = runxhdyClass.ifckfzztTest;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 断路器试验设置_Enter(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            //TextBox tttb = null;
            //try
            //{
            //    tttb = (TextBox)sender;
            //    if (tttb.Text.Length <= 0 || tttb.Text.CompareTo("0")==0)
            //    {
            //        tttb.BackColor = SystemColors.Window;
            //        return;
            //    }
            //    if (int.Parse(tttb.Text) % 2 == 1 && int.Parse(tttb.Text) < 36)
            //    {
            //        tttb.BackColor = Color.LimeGreen;
            //    }
            //    else
            //    {
            //        tttb.BackColor = Color.Red;
            //    }
            //}
            //catch (Exception ee)
            //{
            //    tttb.BackColor = Color.Red;
            //}
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            //TextBox tttb = null;
            //try
            //{
            //    tttb = (TextBox)sender;
            //    if (tttb.Text.Length <= 0 || tttb.Text.CompareTo("0") == 0)
            //    {
            //        tttb.BackColor = SystemColors.Window;
            //        return;
            //    }
            //    if (int.Parse(tttb.Text) % 2 == 0 && int.Parse(tttb.Text) < 36)
            //    {
            //        tttb.BackColor = Color.LimeGreen;
            //    }
            //    else
            //    {
            //        tttb.BackColor = Color.Red;
            //    }
            //}
            //catch (Exception ee)
            //{
            //    tttb.BackColor = Color.Red;
            //}
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Report\\";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                textBox32.Text = openFileDialog1.SafeFileName;
                if (runxhdyClass != null)
                {
                    runxhdyClass.reportName = textBox32.Text.Trim();
                }
            }
        }


    }
}

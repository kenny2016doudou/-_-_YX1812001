using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Net.Sockets;
using ManagementSpecificTools.PlcConnectivity;
using System.Reflection;

namespace ManagementSpecificTools
{
    public partial class MSTF : Form
    {
        public SiemensCommManage Com_Siemens = null;
        private XmlDocument xmlDocument = new XmlDocument();
        public MSTF()
        {
            InitializeComponent();
            Com_Siemens = SiemensCommManage.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                toUpdateCFG();
                Com_Siemens.Start();
                timer2.Enabled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Com_Siemens.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Plc.Instance.PLCStatue)
            {
                case 0:
                    label1.Text = "Not Connect";
                    button1.Enabled = true;
                    button2.Enabled = false;
                    break;
                case 1:
                    label1.Text = "Trying TCP Connect";
                    break;
                case 2:
                    label1.Text = "TCP Connected";
                    button1.Enabled = false;
                    button2.Enabled = true;
                    break;
                case 3:
                    label1.Text = "PLC Handled Ok";
                    button1.Enabled = false;
                    button2.Enabled = true;
                    break;
                case 4:
                    label1.Text = "Fetching...";
                    button1.Enabled = false;
                    button2.Enabled = true;
                    break;
                case 5:
                    label1.Text = "PLC Handled Error";
                    button1.Enabled = true;
                    button2.Enabled = false;
                    break;
                case 6:
                    label1.Text = "Communicate Error";
                    button1.Enabled = true;
                    button2.Enabled = false;
                    break;
                default:
                    break;
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Com_Siemens == null)
                return;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem item = listView1.Items[i];
                foreach (KeyValuePair<string, EmbedTag> embedtag in Com_Siemens.TagGroups.EmbedTags)
                {
                    EmbedTag ta = embedtag.Value;
                    if (item.Text.CompareTo(ta.Name) == 0)
                    {
                        item.SubItems[4].Text = ta.tagCell.ItemValue.ToString();
                        break;
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Plc.Instance.Disconnect();
        }

        private void 修改数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            string name = listView1.SelectedItems[0].SubItems[0].Text;
            Form2 frm = new Form2();
            double tempdoublevalue = 0;
            if (double.TryParse(listView1.SelectedItems[0].SubItems[4].Text, out tempdoublevalue))
            {
                frm.DataValue = tempdoublevalue;
            }
            frm.DataType = listView1.SelectedItems[0].SubItems[3].Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Com_Siemens.ExcuteCommand_Write(listView1.SelectedItems[0].SubItems[0].Text, frm.DataValue);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void showInfo()
        {
            try
            {
                foreach (S7.Net.CpuType suit in Enum.GetValues(typeof(S7.Net.CpuType)))
                {
                    comboBox1.Items.Add(suit);
                }
                //string HostName = Dns.GetHostName();
                //IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                //for (int i = 0; i < IpEntry.AddressList.Length; i++)
                //{
                //    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        comboBox2.Items.Add(IpEntry.AddressList[i].ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
            {
                xmlDocument.Load(Path.Combine(Application.StartupPath, "syscfg.xml"));
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                comboBox1.Text = xmlNode.Attributes["name"].Value;
                txtIP.Text = xmlNode.Attributes["ip"].Value;
            }
            foreach (KeyValuePair<string, EmbedTag> embedtag in Com_Siemens.TagGroups.EmbedTags)
            {
                EmbedTag ta = embedtag.Value;
                ListViewItem item = new ListViewItem(ta.Name);
                item.Tag = ta;
                item.SubItems.Add(ta.AccessType);
                item.SubItems.Add(ta.Accessaddress);
                item.SubItems.Add(ta.DataType);
                item.SubItems.Add("");
                item.SubItems.Add(ta.Desc);
                listView1.Items.Add(item);
            }
        }

        private void AddEmbedTag(string pname, string paccesstype, string paccessaddress, string pdatatype, string pdesc)
        {
            bool ifExist = false;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Text.CompareTo(pname) == 0)
                    ifExist = true;
            }
            if (ifExist)
            {
                MessageBox.Show("指令: " + pname + " 已经存在!");
                return;
            }
            if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
            {
                xmlDocument.Load(Path.Combine(Application.StartupPath, "syscfg.xml"));
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                XmlNodeList xmlNodeList = documentElement.ChildNodes;
                XmlNodeList xmlNodeList2 = xmlNodeList[0].ChildNodes;
                for (int j = 0; j < xmlNodeList2.Count; j++)
                {
                    string text2 = xmlNodeList2[j].Attributes["name"].Value.ToUpper();
                    if (text2.CompareTo(pname) == 0)
                    {
                        ifExist = true;
                        xmlNodeList[0].RemoveChild(xmlNodeList2[j]);
                        break;
                    }
                }
            }
            if (ifExist)
            {
                MessageBox.Show("指令: " + pname + " 已经存在!");
                return;
            }
            addCore(pname, paccesstype, paccessaddress, pdatatype, pdesc);
        }

        private void addCore(string pname, string paccesstype, string paccessaddress, string pdatatype, string pdesc)
        {
            EmbedTag ta = new EmbedTag(pname, paccesstype, paccessaddress, pdatatype, pdesc);
            ListViewItem item = new ListViewItem(ta.Name);
            item.Tag = ta;
            item.SubItems.Add(ta.AccessType);
            item.SubItems.Add(ta.Accessaddress);
            item.SubItems.Add(ta.DataType);
            item.SubItems.Add("");
            item.SubItems.Add(ta.Desc);
            listView1.Items.Add(item);
            Com_Siemens.AddEmbedTag(ta.Name, ta);

            XmlNode documentElement = xmlDocument.DocumentElement;
            XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
            comboBox1.Text = xmlNode.Attributes["name"].Value;
            txtIP.Text = xmlNode.Attributes["ip"].Value;
            XmlNodeList xmlNodeList = documentElement.SelectNodes("//TagGroup");
            XmlNodeList xmlNodeList2 = xmlNodeList[0].SelectNodes("EmbedTag");
            XmlNode newXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "EmbedTag", "");
            XmlElement node = (XmlElement)newXmlNode;
            node.SetAttribute("name", ta.Name);
            node.SetAttribute("accessType", ta.AccessType);
            node.SetAttribute("accessAddress", ta.Accessaddress);
            node.SetAttribute("dataType", ta.DataType);
            node.SetAttribute("desc", ta.Desc);
            xmlNodeList[0].AppendChild(newXmlNode);
            xmlDocument.Save(Path.Combine(Application.StartupPath, "syscfg.xml"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showInfo();
            timer1.Start();
            button2.Enabled = false;
        }

        private void toUpdateCFG()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
            {
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                xmlNode.Attributes["name"].Value = comboBox1.Text;
                xmlNode.Attributes["ip"].Value = txtIP.Text;
                xmlDocument.Save(Path.Combine(Application.StartupPath, "syscfg.xml"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Plc.Instance.Write(PlcTags.BitVariable, 1);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Plc.Instance.Write(PlcTags.BitVariable, 0);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductAddress();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                label5.Visible = numericUpDown4.Visible = true;
            }
            else
                label5.Visible = numericUpDown4.Visible = false;
            ProductAddress();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ProductAddress();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            ProductAddress();
        }

        public void ProductAddress()
        {
            if (ifIngore)
                return;
            if (comboBox2.Text.Trim().Length <= 0 || comboBox3.Text.Trim().Length <= 0)
                return;
            string tempStr = "";
            switch (comboBox2.Text)
            {
                case "V":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "DB1.DBX" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                        case "Byte":
                            tempStr = "DB1.DBB" + numericUpDown3.Value.ToString();
                            break;
                        case "Word":
                            tempStr = "DB1.DBW" + numericUpDown3.Value.ToString();
                            break;
                        case "DWord":
                            tempStr = "DB1.DBD" + numericUpDown3.Value.ToString();
                            break;
                        case "Real":
                            tempStr = "DB1.DBF" + numericUpDown3.Value.ToString();
                            break;
                    }
                    break;
                case "M":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "M" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                        case "Byte":
                            tempStr = "MB" + numericUpDown3.Value.ToString();
                            break;
                        case "Word":
                            tempStr = "MW" + numericUpDown3.Value.ToString();
                            break;
                        case "DWord":
                            tempStr = "MD" + numericUpDown3.Value.ToString();
                            break;
                    }
                    break;
                case "A":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "A" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                        case "Byte":
                            tempStr = "AB" + numericUpDown3.Value.ToString();
                            break;
                        case "Word":
                            tempStr = "AW" + numericUpDown3.Value.ToString();
                            break;
                        case "DWord":
                            tempStr = "AD" + numericUpDown3.Value.ToString();
                            break;
                    }
                    break;
                case "E":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "E" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                        case "Byte":
                            tempStr = "EB" + numericUpDown3.Value.ToString();
                            break;
                        case "Word":
                            tempStr = "EW" + numericUpDown3.Value.ToString();
                            break;
                        case "DWord":
                            tempStr = "ED" + numericUpDown3.Value.ToString();
                            break;
                    }
                    break;
                case "I":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "I" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                    }
                    break;
                case "O":
                    switch (comboBox3.Text)
                    {
                        case "Bit":
                            tempStr = "O" + numericUpDown3.Value.ToString() + "." + numericUpDown4.Value.ToString();
                            break;
                    }
                    break;
            }
            label7.Text = tempStr;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (label7.Text.Trim().Length <= 0)
                return;
            AddEmbedTag(textBox1.Text, groupBox2.Tag.ToString(), label7.Text, comboBox3.Text, richTextBox1.Text);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton ttrb = (RadioButton)sender;
            if (ttrb.Checked)
            {
                groupBox2.Tag = ttrb.Text;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            toUpdateCFG();
        }

        private ListViewItem EidtlistViewItem = null;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EidtlistViewItem = listView1.SelectedItems[0];
                EmbedTag tag = (EmbedTag)EidtlistViewItem.Tag;
                textBox1.Text = tag.Name;
                comboBox3.Text = tag.DataType;
                richTextBox1.Text = tag.Desc;
                if (tag.AccessType.CompareTo("Read") == 0)
                    radioButton1.Checked = true;
                else if (tag.AccessType.CompareTo("Write") == 0)
                    radioButton2.Checked = true;
                else if (tag.AccessType.CompareTo("Read/Write") == 0)
                    radioButton3.Checked = true;
                label7.Text = tag.Accessaddress;
                if (tag.Accessaddress.Substring(0, 2).Contains("DB"))
                {
                    comboBox2.Text = "V";
                    if (tag.Accessaddress.Contains("DBF"))
                    {
                        string tempstr = label7.Text.Substring(label7.Text.IndexOf("DBF") + 3);
                        numericUpDown3.Value = int.Parse(tempstr);
                    }
                    else if (tag.Accessaddress.Contains("DBD"))
                    {
                        string tempstr = label7.Text.Substring(label7.Text.IndexOf("DBD") + 3);
                        numericUpDown3.Value = int.Parse(tempstr);
                    }
                    else if (tag.Accessaddress.Contains("DBW"))
                    {
                        string tempstr = label7.Text.Substring(label7.Text.IndexOf("DBW") + 3);
                        numericUpDown3.Value = int.Parse(tempstr);
                    }
                }
                else if (tag.Accessaddress.Substring(0, 2).Contains("MW"))
                {
                    comboBox2.Text = "M";
                    if (tag.Accessaddress.Contains("MW"))
                    {
                        string tempstr = label7.Text.Substring(label7.Text.IndexOf("MW") + 2);
                        numericUpDown3.Value = int.Parse(tempstr);
                    } 
                }
                else if (tag.Accessaddress.Substring(0, 2).Contains("MD"))
                {
                    comboBox2.Text = "M";
                    if (tag.Accessaddress.Contains("MD"))
                    {
                        string tempstr = label7.Text.Substring(label7.Text.IndexOf("MD") + 2);
                        numericUpDown3.Value = int.Parse(tempstr);
                    }
                }
                else if (tag.Accessaddress.Substring(0, 2).Contains("M"))
                {
                    comboBox2.Text = "M";
                    string tempstr = label7.Text.Substring(label7.Text.IndexOf("M") + 1);
                    tempstr = tempstr.Substring(0, tempstr.IndexOf("."));
                    numericUpDown3.Value = int.Parse(tempstr);

                    tempstr = label7.Text.Substring(label7.Text.IndexOf(".") + 1);
                    numericUpDown4.Value = int.Parse(tempstr);
                }
                else if (tag.Accessaddress.Substring(0, 2).Contains("I"))
                {
                    comboBox2.Text = "I";
                }
                else if (tag.Accessaddress.Substring(0, 2).Contains("O"))
                {
                    comboBox2.Text = "O";
                }
            }
            catch
            { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (EidtlistViewItem == null)
                return;
            EmbedTag tag = (EmbedTag)EidtlistViewItem.Tag;
            Com_Siemens.RemoveEmbedTag(tag.Name);
            listView1.Items.Remove(EidtlistViewItem);
            EidtlistViewItem = null;
        }

        private void 删除配置信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EidtlistViewItem == null)
                return;
            EmbedTag tag = (EmbedTag)EidtlistViewItem.Tag;
            Com_Siemens.RemoveEmbedTag(tag.Name);
            listView1.Items.Remove(EidtlistViewItem);
            EidtlistViewItem = null;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (EidtlistViewItem == null)
                return;
            EmbedTag tag = (EmbedTag)EidtlistViewItem.Tag;
            Com_Siemens.RemoveEmbedTag(tag.Name);
            listView1.Items.Remove(EidtlistViewItem);
            EidtlistViewItem = null;
            AddEmbedTag(textBox1.Text, groupBox2.Tag.ToString(), label7.Text, comboBox3.Text, richTextBox1.Text);
        }

        private bool ifIngore = false;
        private void listView1_Enter(object sender, EventArgs e)
        {
            ifIngore = true;
        }

        private void listView1_Leave(object sender, EventArgs e)
        {
            ifIngore = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Contains("选择状态"))
                    radioButton1.Checked = true;
                else if (textBox1.Text.Contains("状态"))
                    radioButton1.Checked = true;
            }
            catch
            { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            string name = listView1.SelectedItems[0].SubItems[0].Text;
            if (listView1.SelectedItems[0].SubItems[3].Text.CompareTo("Bit") == 0)
            {
                Com_Siemens.ExcuteCommand_Pulse(name);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            string name = listView1.SelectedItems[0].SubItems[0].Text;
            if (listView1.SelectedItems[0].SubItems[3].Text.CompareTo("Bit") == 0)
            {
                Com_Siemens.ExcuteCommand_Set(name);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            string name = listView1.SelectedItems[0].SubItems[0].Text;
            if (listView1.SelectedItems[0].SubItems[3].Text.CompareTo("Bit") == 0)
            {
                Com_Siemens.ExcuteCommand_Reset(name);
            }
        }

        private void 清空配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清空所有配置信息？(清空后不可恢复！)", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                EmbedTag tag = (EmbedTag)listView1.Items[i].Tag;
                Com_Siemens.RemoveEmbedTag(tag.Name);
            }
            listView1.Items.Clear();
            EidtlistViewItem = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog(this) != DialogResult.Cancel)
            {
                textBox3.Text = openFileDialog1.SafeFileName;
                textBox3.Tag = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// import tag 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim().Length <= 0)
                return;
            if (!File.Exists(textBox3.Tag.ToString()))
                return;
            if (checkBox1.Checked)
            {
                清空配置ToolStripMenuItem_Click(sender, e);
            }
            GoldPrinter.ExcelAccess excel = new GoldPrinter.ExcelAccess();
            try
            {
                int nullLineCount = 0;
                string[] tempStr = new string[5];
                string strExcelTemplateFile = textBox3.Tag.ToString();
                excel.Open(strExcelTemplateFile);//打开上面保存好的excel文件
                excel.IsVisibledExcel = false;
                for (int li = 1; li <= 200; li++)
                {
                    nullLineCount = 0;
                    for (int ci = 1; ci <= 4; ci++)
                    {
                        tempStr[ci] = excel.GetCellText(li, ci);
                    }
                    if (tempStr[1].ToUpper().Contains("名称"))
                        continue;
                    if (tempStr[1].ToUpper().Contains("END"))
                        break;
                    if (tempStr[1].Trim().Length <= 0)
                    {
                        nullLineCount++;
                        continue;
                    }
                    if (nullLineCount >= 5)
                        break;
                    //
                    textBox1.Text = tempStr[1];
                    //
                    if (tempStr[3].ToUpper().Contains("读") && tempStr[3].ToUpper().Contains("写"))
                    {
                        radioButton3.Checked = true;
                    }
                    else if (tempStr[3].ToUpper().Contains("读"))
                    {
                        radioButton1.Checked = true;
                    }
                    else if (tempStr[3].ToUpper().Contains("写"))
                    {
                        radioButton2.Checked = true;
                    }
                    //
                    if (tempStr[2].ToUpper().Contains("VD"))
                    {
                        comboBox2.Text = "V";
                        if (tempStr[4].ToUpper().Contains("浮点数"))
                        {
                            comboBox3.Text = "Real";
                        }
                        else
                        {
                            comboBox3.Text = "DWord";
                        }
                        numericUpDown3.Value = decimal.Parse(tempStr[2].ToUpper().Replace("VD", ""));
                        numericUpDown4.Value = 0;
                    }
                    else if (tempStr[2].ToUpper().Contains("VW"))
                    {
                        comboBox2.Text = "V";
                        comboBox3.Text = "Word";
                        numericUpDown3.Value = decimal.Parse(tempStr[2].ToUpper().Replace("VW", ""));
                        numericUpDown4.Value = 0;
                    }
                    else if (tempStr[2].ToUpper().Contains("MD"))
                    {
                        comboBox2.Text = "M";
                        if (tempStr[4].ToUpper().Contains("浮点数"))
                        {
                            comboBox3.Text = "Real";
                        }
                        else
                        {
                            comboBox3.Text = "DWord";
                        }
                        numericUpDown3.Value = decimal.Parse(tempStr[2].ToUpper().Replace("MD", ""));
                        numericUpDown4.Value = 0;
                    }
                    else if (tempStr[2].ToUpper().Contains("MW"))
                    {
                        comboBox2.Text = "M";
                        comboBox3.Text = "Word";
                        numericUpDown3.Value = decimal.Parse(tempStr[2].ToUpper().Replace("MW", ""));
                        numericUpDown4.Value = 0;
                    }
                    else if (tempStr[2].ToUpper().Contains("V"))
                    {
                        comboBox2.Text = "V";
                        comboBox3.Text = "Bit";
                        tempStr[2] = tempStr[2].ToUpper().Replace("V", "");
                        numericUpDown3.Value = decimal.Parse(tempStr[2].Substring(0, tempStr[2].IndexOf('.')));
                        numericUpDown4.Value = decimal.Parse(tempStr[2].Substring(tempStr[2].IndexOf('.') + 1, tempStr[2].Length - tempStr[2].IndexOf('.') - 1));
                    }
                    else if (tempStr[2].ToUpper().Contains("M"))
                    {
                        comboBox2.Text = "M";
                        comboBox3.Text = "Bit";
                        tempStr[2] = tempStr[2].ToUpper().Replace("M", "");
                        numericUpDown3.Value = decimal.Parse(tempStr[2].Substring(0, tempStr[2].IndexOf('.')));
                        numericUpDown4.Value = decimal.Parse(tempStr[2].Substring(tempStr[2].IndexOf('.') + 1, tempStr[2].Length - tempStr[2].IndexOf('.') - 1));
                    }
                    //
                    richTextBox1.Text = tempStr[4];
                    AddEmbedTag(textBox1.Text, groupBox2.Tag.ToString(), label7.Text, comboBox3.Text, richTextBox1.Text);
                }
            }
#pragma warning disable CS0168 // 声明了变量“ee”，但从未使用过
            catch (Exception ee)
#pragma warning restore CS0168 // 声明了变量“ee”，但从未使用过
            { }
            finally
            {
                excel.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) != DialogResult.Cancel)
            {
                textBox2.Text = openFileDialog2.FileName;
                textBox4.Text = openFileDialog2.SafeFileName.Substring(0, openFileDialog2.SafeFileName.Length-4);
                //var classes = Assembly.Load(textBox2.Text).GetTypes();
                //foreach (var item in classes)
                //{
                    
                //}
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string asmfile = textBox2.Text;
            string dll_name = textBox4.Text;
            string control_name = "UC_Form";

            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(asmfile);
            Type type = asm.GetType(dll_name + "." + control_name);
            try
            {
                UserControl tt_uc = (UserControl)System.Activator.CreateInstance(type);
                TreeNode treeNode = new TreeNode(tt_uc.Name);
                toLoadControlTree(treeNode,tt_uc.Controls);
                treeView1.Nodes.Add(treeNode);
            }
#pragma warning disable CS0168 // 声明了变量“ee”，但从未使用过
            catch (Exception ee)
#pragma warning restore CS0168 // 声明了变量“ee”，但从未使用过
            {
                Form tt_form = (Form)System.Activator.CreateInstance(type);
                TreeNode treeNode = new TreeNode(tt_form.Name);
                toLoadControlTree(treeNode, tt_form.Controls);
                treeView1.Nodes.Add(treeNode);
            }
        }
        private void toLoadControlTree(TreeNode parentNode, Control.ControlCollection  ttcc)
        {
            if (ttcc == null || ttcc.Count <= 0)
                return;
            string tempstr = "";
            for (int i = 0; i < ttcc.Count; i++)
            {
                tempstr = ttcc[i].GetType().Name.ToUpper();
                if (tempstr.Contains("BUTTON") || tempstr.Contains("GLASSBUTTON")|| tempstr.Contains("TEXTBOX") || tempstr.Contains("LABEL"))
                {
                    TreeNode treeNode = new TreeNode(ttcc[i].Name);
                    parentNode.Nodes.Add(treeNode); 
                }
                else if (tempstr.Contains("GROUPBOX") || tempstr.Contains("PANEL") || tempstr.Contains("TABCONTROL") || tempstr.Contains("TABPAGE"))
                {
                    TreeNode treeNode = new TreeNode(ttcc[i].Name);
                    parentNode.Nodes.Add(treeNode);
                    toLoadControlTree(treeNode, ttcc[i].Controls);
                }
            }
        }
    }
}

/*V
M
A
E
I
O
 *  btnConnect.IsEnabled = Plc.Instance.ConnectionState == ConnectionStates.Offline;
            btnDisconnect.IsEnabled = Plc.Instance.ConnectionState != ConnectionStates.Offline;
            lblConnectionState.Text = Plc.Instance.ConnectionState.ToString();
            ledMachineInRun.Fill = Plc.Instance.Db1.BitVariable0 ? Brushes.Green : Brushes.Gray;
            lblSpeed.Content = Plc.Instance.Db1.IntVariable;
            lblTemperature.Content = Plc.Instance.Db1.RealVariable;
            lblAutomaticSpeed.Content = Plc.Instance.Db1.DIntVariable;
            lblSetDwordVariable.Content = Plc.Instance.Db1.DWordVariable;
            // statusbar
            lblReadTime.Text = Plc.Instance.CycleReadTime.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);


 * private void txtSetRealVariable_TextChanged(object sender, TextChangedEventArgs e)
        {
            double realVar;
            bool canConvert = Double.TryParse(txtSetTemperature.Text, out realVar);
            if (canConvert)
            {
                Plc.Instance.Write(PlcTags.DoubleVariable, realVar);
            }
        }

        private void txtSetWordVariable_TextChanged(object sender, TextChangedEventArgs e)
        {
            short wordVar;
            bool canConvert = short.TryParse(txtSetSpeed.Text, out wordVar);
            if (canConvert)
            {
                Plc.Instance.Write(PlcTags.IntVariable, wordVar);
            }
        }

        private void txtSetDIntVariable_TextChanged(object sender, TextChangedEventArgs e)
        {
            int dintVar;
            bool canConvert = int.TryParse(txtSetAutomaticSpeed.Text, out dintVar);
            if (canConvert)
            {
                Plc.Instance.Write(PlcTags.DIntVariable, dintVar);
            }
        }

        private void txtSetSetDwordVariable_TextChanged(object sender, TextChangedEventArgs e)
        {
            ushort dwordVar;
            bool canConvert = ushort.TryParse(txtSetDwordVariable.Text, out dwordVar);
            if (canConvert)
            {
                Plc.Instance.Write(PlcTags.DwordVariable, dwordVar);
            }
        }
 */

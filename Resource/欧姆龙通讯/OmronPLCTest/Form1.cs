using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using TestSystem.Command.PLC.Omron.HostLink.Fins;


namespace OmronPLCTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbCOM.Text= Omron_SerialPort.GetOmronCOM(9600, Parity.Even, 7, StopBits.Two);
           
        }
        //H0  H2
        SerialPort sp=null;
        private void btnReadPLC_Click(object sender, EventArgs e)
        {
           
           AreaCode ac;
           switch (cmbRArea.Text)
           {
               case "CIO_Bit":
                   ac = AreaCode.CIO_Bit;
                   break;
               case "WR_Bit":
                   ac = AreaCode.WR_Bit;
                   break;
               case "HR_Bit":
                   ac = AreaCode.HR_Bit;
                   break;
               case "AR_Bit":
                   ac = AreaCode.AR_Bit;
                   break;
               case "CIO_Word":
                   ac = AreaCode.CIO_Word;
                   break;
               case "WR_Word":
                   ac = AreaCode.WR_Word;
                   break;
               case "HR_Word":
                   ac = AreaCode.HR_Word;
                   break;
               case "AR_Word":
                   ac = AreaCode.AR_Word;
                   break;
               case "TIM_Flag":
                   ac = AreaCode.TM_Flag;
                   break;
               case "CNT_PV":
                   ac = AreaCode.CNT_PV;
                   break;
               case "TIM_PV":
                   ac = AreaCode.TIM_PV;
                   break;
               case "CNT_Flag":
                   ac = AreaCode.CNT_Flag;
                   break;
               case "DM_Bit":
                   ac = AreaCode.DM_Bit;
                   break;
               case "DM_Word":
                   ac = AreaCode.DM_Word;
                   break;

               default:
                   ac = AreaCode.DM_Word;
                   break;

           }
           DataType dtype;
           switch (cmbRDataType.Text)
           {
               case "Bit":
                   dtype = DataType.Bit;
                   break;
               case "Word":
                   dtype = DataType.Word;
                   break;
               default:
                   dtype = DataType.Word;
                   break;
           }
           if (txtReadPoint.Text.IndexOf('.') > 0)
           {
               Omron_BatchReadStatus reader = new Omron_BatchReadStatus()
               {
                   Area=ac,
                   DType=dtype,
                   StrPoint=txtReadPoint.Text,
                   Sleep=100
               };
               reader.Read(ref sp);
               StringBuilder s = new StringBuilder();

               for (int i = 15; i >= 0; i--)
               {
                   s.Append(reader.DataMuster[i]).ToString();
               }
               string bitStr = txtReadPoint.Text.Split('.')[1];
               string value = "";
               if (int.Parse(bitStr)<=15)
               {
                   value = reader.DataMuster[int.Parse(bitStr)].ToString();
               }
               
               rtxtReadValue.AppendText(string.Format("在内存区域地址{0} 点位:{1}的值是{2} \r\n 位置{3}={4}", cmbRArea.Text, txtReadPoint.Text, s.ToString(), bitStr, value));
           }
           else
           {
               int point = int.Parse(txtReadPoint.Text.Trim());
               int bit = int.Parse(txtReadBit.Text.Trim());
               string cmdStr = "";
               Omron_Reader render = new Omron_Reader() { Point = point, Sleep = 200, Area = ac, DType = dtype };
               string response = render.ReadCommandString(ref sp, ref cmdStr);
               rtxtReadSend.Text = cmdStr;
               rtxtReadResponse.Text = response;
               rtxtReadValue.Text = "";
               if (response != "")
               {
                   foreach (var str in render.GetReadResponse(response))
                   {
                       rtxtReadValue.AppendText(string.Format("在内存区域地址{0} 点位:{1}的值是{2} \r\n", cmbRArea.Text, point, str));
                   }
               }
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            AreaCode ac;
            switch (cmbWArea.Text)
            {
                case "CIO_Bit":
                    ac = AreaCode.CIO_Bit;
                    break;
                case "WR_Bit":
                    ac = AreaCode.WR_Bit;
                    break;
                case "HR_Bit":
                    ac = AreaCode.HR_Bit;
                    break;
                case "AR_Bit":
                    ac = AreaCode.AR_Bit;
                    break;
                case "CIO_Word":
                    ac = AreaCode.CIO_Word;
                    break;
                case "WR_Word":
                    ac = AreaCode.WR_Word;
                    break;
                case "HR_Word":
                    ac = AreaCode.HR_Word;
                    break;
                case "AR_Word":
                    ac = AreaCode.AR_Word;
                    break;
                case "TIM_Flag":
                    ac = AreaCode.TM_Flag;
                    break;
                case "CNT_PV":
                    ac = AreaCode.CNT_PV;
                    break;
                case "TIM_PV":
                    ac = AreaCode.TIM_PV;
                    break;
                case "CNT_Flag":
                    ac = AreaCode.CNT_Flag;
                    break;
                case "DM_Bit":
                    ac = AreaCode.DM_Bit;
                    break;
                case "DM_Word":
                    ac = AreaCode.DM_Word;
                    break;

                default:
                    ac = AreaCode.DM_Word;
                    break;

            }
            DataType dtype;
            switch (cmbWDataType.Text)
            {
                case "Bit":
                    dtype = DataType.Bit;
                    break;
                case "Word":
                    dtype = DataType.Word;
                    break;
                default:
                    dtype = DataType.Word;
                    break;
            }
            int bit = int.Parse(txtWbit.Text.Trim());
            string cmdStr = "";
            if (txtWpoint.Text.IndexOf('.') > 0)
            {
                Omron_WriteData write = new Omron_WriteData() {StrPoint=txtWpoint.Text,Area=ac,DType=dtype,Sleep=200 };
                string response = write.ExcuteUndo(ref sp, ref cmdStr, cboxW.Checked);
                rtxtWriteCommand.Text = cmdStr;
                rtxtWriteResponse.Text = response;
                rtxtWriteValue.Text = "";
                //if (response != "")
                //{
                //    foreach (var str in Omron_Write.GetResponse(response))
                //    {

                //        rtxtWriteValue.AppendText(string.Format("在内存区域地址{0} 点位:{1}的值是{2} \r\n", cmbRArea.Text, txtWpoint.Text, str));

                //    }
                //}
            }
            else
            {
                int point = int.Parse(txtWpoint.Text.Trim());
                string[] strArr = txtWData.Text.Trim().Split(',');
                int[] valueArr = new int[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                {
                    valueArr[i] = int.Parse(strArr[i]);
                }
                Omron_WriteData write = new Omron_WriteData() { IntPoint = point, Area = ac, DType = dtype, Sleep = 200,Data=valueArr };
                string response = write.Excute(ref sp,ref cmdStr);
                rtxtWriteCommand.Text = cmdStr;
                rtxtWriteResponse.Text = response;
                rtxtWriteValue.Text = "";
                if (response != "")
                {
                    foreach (var str in write.GetWriteResponse(response))
                    {

                        rtxtWriteValue.AppendText(string.Format("在内存区域地址{0} 点位:{1}的值是{2} \r\n", cmbRArea.Text, point, str));

                    }
                }
            }
           
        }

        bool isOpen = false;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (isOpen == false)
                {
                    string COM = cmbCOM.Text;
                    int Rate = int.Parse(txtRate.Text.Trim());
                    Parity parity;
                    switch (cmbParity.Text)
                    {
                        case "Even":
                            parity = Parity.Even;
                            break;
                        case "Mark":
                            parity = Parity.Mark;
                            break;
                        case "None":
                            parity = Parity.None;
                            break;
                        case "Odd":
                            parity = Parity.Odd;
                            break;
                        case "Space":
                            parity = Parity.Space;
                            break;
                        default:
                            parity = Parity.Even;
                            break;

                    }
                    int dataBits = int.Parse(txtdataBits.Text.Trim());
                    StopBits stopbits;
                    switch (cbmStopBits.Text)
                    {
                        case "None":
                            stopbits = StopBits.None;
                            break;
                        case "One":
                            stopbits = StopBits.One;
                            break;
                        case "OnePointFive":
                            stopbits = StopBits.OnePointFive;
                            break;
                        case "Two":
                            stopbits = StopBits.Two;
                            break;
                        default:
                            stopbits = StopBits.None;
                            break;
                    }
                    if (sp == null)
                    {
                        sp = new SerialPort(COM, Rate, parity, dataBits, stopbits);
                        if (sp.IsOpen == false)
                        {
                            sp.Open();
                        }
                        gbOmronCommand.Enabled = true;
                        gbOmronRead.Enabled = true;
                        gbOmronWrite.Enabled = true;
                        tabControl1.Enabled = true;
                        btnOpen.Text = "关闭串口";
                        isOpen = true;
                    }
                    
                }
                else
                {
                    if (sp != null)
                    {
                        
                        if (sp.IsOpen == true)
                        {
                            sp.Close();
                        }
                        gbOmronCommand.Enabled = false;
                        gbOmronRead.Enabled = false;
                        gbOmronWrite.Enabled = false;
                        tabControl1.Enabled = false
                            ;
                        btnOpen.Text = "打开";
                        isOpen = false;
                        sp = null;
                    }
                   
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sp = null;
                btnOpen.Text = "打开";
                isOpen = false;
            }
           
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (sp != null && sp.IsOpen == true)
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(rtxtCommand.Text.Trim() + (char)13);
                System.Threading.Thread.Sleep(100);
                rtxtResponse.Text=sp.ReadExisting();
            }
            
        }

        private void btn欧姆龙读取_Click(object sender, EventArgs e)
        {
             Omron_Reader reader = new Omron_Reader();
             DataType dtype;
             switch (cmb数据类型.Text)
             {
               case "Bit":
                   dtype = DataType.Bit;
                   break;
               case "Word":
                   dtype = DataType.Word;
                   break;
               default:
                   dtype = DataType.Word;
                   break;
             }
            txt读取指令状态信息.Text= reader.ReadCommandString(txt读取地址.Text, dtype, sp,int.Parse(textBox1.Text));
        }

        private void btn欧姆龙写入_Click(object sender, EventArgs e)
        {
            Omron_WriteData write = new Omron_WriteData();
            DataType dtype;
            switch (cbox写入数据类型.Text)
            {
                case "Bit":
                    dtype = DataType.Bit;
                    break;
                case "Word":
                    dtype = DataType.Word;
                    break;
                default:
                    dtype = DataType.Word;
                    break;
            }
            if(txt写入值.Text.EndsWith(","))
            {
                txt写入值.Text=txt写入值.Text.Substring(txt写入值.Text.Length-1,1);
            }
            string[] arr=txt写入值.Text.Split(',');
            int [] data=new int[arr.Count()];
            for(int i =0;i<arr.Count();i++)
            {
                data[i]=int.Parse(arr[i]);
            }
           richTextBox1.Text= write.Excute(txt写入地址.Text, dtype, sp, data);
        }

        private void btn控制给1_Click(object sender, EventArgs e)
        {
            Omron_ClickDown write = new Omron_ClickDown();
            DataType dtype;
            switch (cbox控制点位数据类型.Text)
            {
                case "Bit":
                    dtype = DataType.Bit;
                    break;
                case "Word":
                    dtype = DataType.Word;
                    break;
                default:
                    dtype = DataType.Word;
                    break;
            }

            txt状态控制.Text = write.ExcuteUndo(txt控制点位.Text, sp, dtype, true);
        }

        private void btn控制给0_Click(object sender, EventArgs e)
        {
            Omron_ClickUp write = new Omron_ClickUp();
            DataType dtype;
            switch (cbox控制点位数据类型.Text)
            {
                case "Bit":
                    dtype = DataType.Bit;
                    break;
                case "Word":
                    dtype = DataType.Word;
                    break;
                default:
                    dtype = DataType.Word;
                    break;
            }

            txt状态控制.Text = write.ExcuteUndo(txt控制点位.Text, sp, dtype, false);
        }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace YD_YBTest
{
    public partial class Form1 : Form
    {
        SerialPort sp=null;
        public Form1()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp == null)
                {
                    sp = new SerialPort();
                    Parity parity;
                    switch (cboxParity.Text)
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
                            parity = Parity.None;
                            break;
                    }
                    sp.Parity = parity;
                    StopBits stopBits;
                    switch (cboxStopBits.Text)
                    {
                        case "None":
                            stopBits = StopBits.None;
                            break;
                        case "One":
                            stopBits = StopBits.One;
                            break;
                        case "OnePointFive":
                            stopBits = StopBits.OnePointFive;
                            break;
                        case "Two":
                            stopBits = StopBits.Two;
                            break;
                        default:
                            stopBits = StopBits.One;
                            break;
                    }
                    sp.StopBits = stopBits;


                    sp.PortName = cboxCom.Text;

                    sp.BaudRate = int.Parse(cboxBaudRate.Text);

                    sp.DataBits = int.Parse(cboxDataBits.Text);



                    sp.Open();

                    gboxR.Enabled = true;
                    gboxW.Enabled = true;

                }
            }
            catch
            {
                sp = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sp = null;
            gboxR.Enabled = false;
            gboxW.Enabled = false;
        }

        private void btnR_Click(object sender, EventArgs e)
        {
          
            SendRead();
        }

        void SendRead()
        {

            string cmd = TestSystem.Command.YB.YuDian.YuDianYB.SendStr(txtRAddr.Text, txtRParmAddr.Text);
            txtRCmd.Text = string.Format("发送指令:{0}\r", cmd);
            txtRCmd.Text += string.Format("通过串口发送转换以后的指令:\r");
            byte[] chars = TestSystem.Command.YB.YuDian.YuDianYB.ReadCommand(cmd);
            foreach (var val in chars)
            {
                txtRCmd.Text += (val.ToString().Length == 1 ? "0" + val.ToString() : val.ToString()) + "  ";
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(chars, 0, chars.Length);
            System.Threading.Thread.Sleep(200);
            byte[] b = new byte[10];
            sp.Read(b, 0, b.Length);

            txtRResponse.Text = string.Format("获得16进制返回的数据:\r");
            string[] response = TestSystem.Command.YB.YuDian.YuDianYB.ResponseConvertToHex16(b);
            if (TestSystem.Command.YB.YuDian.YuDianYB.CheckFCS(response, int.Parse(txtRAddr.Text)))
            {
                foreach (var val in response)
                {
                    txtRResponse.Text += (val.ToString().Length == 1 ? "0" + val.ToString().ToUpper() : val.ToString().ToUpper()) + "  ";
                }
                txtRResponse.Text += "\r";
                txtRResponse.Text += string.Format("其中: 1位是高位 2位是低位 是PV值 测量值的话应该是:低位在前高位在后 \r");
                txtRResponse.Text += string.Format("其中: 3位是高位 4位是低位 是SV值 给定值的话应该是:低位在前高位在后 \r");
                txtRResponse.Text += string.Format("其中: 5位是高位 6位是低位 是输出MV及报警状态 低位在前高位在后 \r");
                txtRResponse.Text += string.Format("其中: 8位是高位 7位是低位 是所读或所写参数值 低位在前高位在后 \r");
                txtRResponse.Text += string.Format("其中: 9位是高位 10位是低位 是校验码 是:低位在前高位在后 \r");
                txtRResponse.Text += string.Format("其中校验码计算是高低位互换后的前8位相加+addr 和高低位互换后的校验码校验\r");
                txtRResponse.Text += string.Format("PV={0}  SV={1}", TestSystem.Command.YB.YuDian.YuDianYB.ReadPV(response,int.Parse(txtRAddr.Text)),TestSystem.Command.YB.YuDian.YuDianYB.ReadSV(response,int.Parse(txtRAddr.Text)));
            }
        }

        /// <summary>
        /// 将10进制数转换为16进制 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string HexTo16(int value)
        {
            return Convert.ToString(value, 16).ToUpper();
        }

        /// <summary>
        /// 构建读取发送指令
        /// </summary>
        /// <param name="addr">仪表地址 如:2 ,3 ,4 </param>
        /// <param name="parm_addr">16进制参数代号</param>
        /// <returns></returns>
        public string ReadCommand(string addr, string parm_addr)
        {

            string command = "";
            int addr_AI = 128;
            try
            {
                int _addr = 0;
                int _parm_addr = Convert.ToInt32(parm_addr, 16);//将参数代号转换为10进制
                int _readArea = 52;
                if (int.TryParse(addr, out _addr))
                {
                    string crc=ReadCRC(_addr,_parm_addr);
                    command = HexTo16(_addr + addr_AI) + HexTo16(_addr + addr_AI) + _readArea + parm_addr.ToUpper() + "0000" + crc;
                }

            }
            catch
            {
 
            }
            return command;
        }

        /// <summary>
        /// 构建写入发送命令
        /// </summary>
        /// <param name="addr">仪表地址:2 ,3 ,4</param>
        /// <param name="parm_addr">参数代号:0C 详细参见宇电参数代号文档</param>
        /// <param name="parm_value">需要写入的值</param>
        /// <returns></returns>
        public string WriteCommand(string addr, string parm_addr, string parm_value)
        {
            string command = "";
            int addr_AI = 128;

            try
            {
                int _addr = 0;
                int _parm_addr = Convert.ToInt32(parm_addr, 16);//将参数代号转换为10进制
                int _readArea = 43;
                int _parm_value = int.Parse(parm_value);
                if (int.TryParse(addr, out _addr))
                {
                    string crc = WriteCRC(_addr, _parm_addr, _parm_value);//校验码
                    if (_parm_value < Convert.ToInt32("FFFF", 16))
                    {
                        if (_parm_value < 0)
                        {
                            //int value = 65535 + Math.Abs(_parm_value) - 65536;//补码求值
                            //string value_16= Convert.ToString(value, 16);//将值转换为16进制
                            //command = HexTo16(_addr + addr_AI) + HexTo16(_addr + addr_AI) + _readArea + parm_addr.ToUpper() + "0000" + crc;
                        }
                        else
                        {


                            string value = CRCRead(_parm_value);//将写入值高低位互换发送.
                           
                            command = HexTo16(_addr + addr_AI) + HexTo16(_addr + addr_AI) + _readArea + parm_addr.ToUpper() + value + crc;

                        }
                    }
                }

            }
            catch
            {

            }
            return command;
        }
        /// <summary>
        ///  写入指令 16位求和校验计算
        /// </summary>
        /// <param name="addrmint"></param>
        /// <param name="parm_addr"></param>
        /// <param name="parm_value"></param>
        /// <returns></returns>
        public string WriteCRC(int addr, int parm_addr, int parm_value)
        {
            string CRC="";
            if (parm_value < Convert.ToInt32("FFFF", 16))
            {
                ushort CRCValue = 0;
                if (parm_value < 0)
                {
                    CRCValue = (ushort)(parm_addr * 256 + 67 + 65535 + Math.Abs(parm_value) - 65536 + addr);//计算读取校验码方式
                }
                else
                {
                    CRCValue = (ushort)(parm_addr * 256 + 67 + parm_value + addr);
                  
                }
                CRC = CRCRead(CRCValue);
                 
            }
            return CRC;
        }
        /// <summary>
        /// 读取指令 16位求和校验计算
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="parm_addr"></param>
        /// <returns></returns>
        public string ReadCRC(int addr, int parm_addr)
        {
            ushort CRCValue = (ushort)(parm_addr * 256 + 82 + addr);//计算读取校验码方式

            //获得16进制数 低位在前高位在后  校验码高低位互换
            string CRC = CRCRead(CRCValue);

            return CRC;
        }


        /// <summary>
        /// CRC 读取命令校验
        /// </summary>
        /// <param name="value">按照公式:参数代号*256+82+addr</param>
        /// <returns></returns>
        public string CRCRead(int value)
        {
            string CRC = Convert.ToString(value, 16).ToUpper();//C54
            if (CRC.Length == 1)
            {
                CRC = "0" + CRC + "00";
            }
            else if (CRC.Length == 2)
            {
                CRC = CRC + "00";
            }
            else if (CRC.Length == 3)
            {
                string H = CRC.Substring(1, 2);//高位
                string L = "0" + CRC.Substring(0, 1);//低位

                CRC = H + L;
            }
            else if (CRC.Length == 4)
            {
                string H = CRC.Substring(2, 2);//高位
                string L = CRC.Substring(0, 2);//低位
                CRC = H + L;
            }
            return CRC;
        }


        /// <summary>
        /// 构建发送命令
        /// </summary>
        /// <param name="CmdStr"></param>
        /// <returns></returns>
        public byte[] Command(string CmdStr)
        {
            byte[] cmdbytes = new byte[8];

            if (CmdStr.Length == 16)
            {
                for (int i = 0; i < CmdStr.Length; i++)
                {
                    if (i / 8 < 1)
                    {
                        cmdbytes[i] = (byte)Convert.ToInt32(CmdStr.Substring(i * 2, 2), 16);// 将命令从16进制转换成10进制 再转换为byte数组
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return cmdbytes;
        }

        /// <summary>
        /// 将返回的结果集合转换为16进制数 
        /// </summary>
        /// <param name="response">返回长度为10的16进制集合</param>
        /// <returns></returns>
        public string[] ResponseConvertToHex16(byte[] response)
        {
            string[] Hex16Bytes = new string[10] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            for (int i = 0; i < response.Length; i++)
            {
                string v=Convert.ToString(response[i], 16);
                Hex16Bytes[i] = v.Length==1?"0"+v:v;
               
            }
            return Hex16Bytes;
        }

        private void btnW_Click(object sender, EventArgs e)
        {


            WriteSend();
        }

        public void WriteSend()
        {
            //写入操作
            string cmd = TestSystem.Command.YB.YuDian.YuDianYB.WriteStr(txtWAddr.Text, txtWParmAddr.Text, txtWValue.Text);
            txtWCmd.Text = string.Format("发送指令:{0}\r", cmd);
            txtWCmd.Text += string.Format("通过串口发送转换以后的指令:\r");
            byte[] chars = TestSystem.Command.YB.YuDian.YuDianYB.WriteCommand(cmd);
            foreach (var val in chars)
            {
                txtWCmd.Text += (val.ToString().Length == 1 ? "0" + val.ToString() : val.ToString()) + "  ";
            }

            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(chars, 0, chars.Length);
            System.Threading.Thread.Sleep(200);
            byte[] b = new byte[10];
            sp.Read(b, 0, b.Length);
            txtWResponse.Text = string.Format("获得16进制返回的数据:\r");
            var readarr = TestSystem.Command.YB.YuDian.YuDianYB.ResponseConvertToHex16(b);
            if (TestSystem.Command.YB.YuDian.YuDianYB.CheckFCS(readarr, int.Parse(txtWAddr.Text)))
            {
                foreach (var val in readarr)
                {
                    txtWResponse.Text += (val.ToString().Length == 1 ? "0" + val.ToString().ToUpper() : val.ToString().ToUpper()) + "  ";
                }
                txtWResponse.Text += "\r";
                txtWResponse.Text += string.Format("其中: 1位是高位 2位是低位 是PV值 测量值的话应该是:低位在前高位在后 \r");
                txtWResponse.Text += string.Format("其中: 3位是高位 4位是低位 是SV值 给定值的话应该是:低位在前高位在后 \r");
                txtWResponse.Text += string.Format("其中: 5位是高位 6位是低位 是输出MV及报警状态 低位在前高位在后 \r");
                txtWResponse.Text += string.Format("其中: 8位是高位 7位是低位 是所读或所写参数值 低位在前高位在后 \r");
                txtWResponse.Text += string.Format("其中: 9位是高位 10位是低位 是校验码 是:低位在前高位在后 \r");
                txtWResponse.Text += string.Format("其中校验码计算是高低位互换后的前8位相加+addr 和高低位互换后的校验码校验\r");
                txtWResponse.Text += string.Format("16进制值: PV={0}  SV={1}", TestSystem.Command.YB.YuDian.YuDianYB.ReadPV(readarr, int.Parse(txtWAddr.Text)), TestSystem.Command.YB.YuDian.YuDianYB.ReadSV(readarr, int.Parse(txtWAddr.Text)));
            }
        }



        /*
         *宇电仪表通讯指令只有读和写
         *读：地址代号+52H(82)+要读的参数代号+0+0+校验码
         *写：地址代号+43H(67)+要写的参数代号+写入数低字节+写入数高字节+校验码
         *地址代号：
         */

    }
}

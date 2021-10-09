using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ZZ.Serial
{
    public class SCM345
    {

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string ReadData(System.IO.Ports.SerialPort sp, int a1, int a2, int a3, int a4, int a5)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            byte[] b = new byte[5];
            //for (int i = 0; i < count; i++)
            //{
            //b[b] += Convert.ToByte(Convert.ToString(0x104, 16));
            b[0] = Convert.ToByte(Convert.ToString(a1, 16));
            b[1] = Convert.ToByte(Convert.ToString(a2, 16));
            b[2] = Convert.ToByte(Convert.ToString(a3, 16));
            b[3] = Convert.ToByte(Convert.ToString(a4, 16));
            b[4] = Convert.ToByte(Convert.ToString(a5, 16));
            //}

            //sp.DiscardInBuffer();
            //System.Threading.Thread.Sleep(20);
            sp.Write(b, 0, b.Length);
            //sp.Write("68 04 00 01 05");
            //System.Threading.Thread.Sleep(100);

            string str = "";

            //if (sp.IsOpen && sp.ReadBufferSize > 0)
            //{

            //    sp.DiscardOutBuffer();
            //    System.Threading.Thread.Sleep(100);
            //    //str = sp.ReadExisting();
            //}
            int loopi = 0;
            while (loopi < 100 && sp.BytesToRead == 0)
            {
                Thread.Sleep(10);
                loopi++;
            }
            byte[] buffer = new byte[0x400];
            if (sp.IsOpen && sp.BytesToRead != 0)
            {
                sp.Read(buffer, 0, 10);
                str = sp.ReadExisting();
            }
            string redstr = "";

            redstr += Convert.ToString((buffer[4]), 16);
            if (redstr.Length == 2)
            {
                redstr = "-" + redstr.Substring(1, redstr.Length - 1);
            }

            redstr += Convert.ToString((buffer[5]), 16);
            redstr += "." + Convert.ToString((buffer[6]), 16);
            return redstr;
        }


        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        //public static string ReadData(System.IO.Ports.SerialPort sp, string[] args)
        //{

        //    byte[] b = GetSendStr(args[0], args[1], args[2], args[3], args[4]);
        //    //serialPort1.Write();//02,
        //    //string cmdstr = sanling.GetSendStr("02", "30", "31304338", "3031", "03");
        //    //serialPort1.Write(cmdstr);
        //    //byte[] b = GetSendStr("02", "30", "31304338", "3031", "03");//D100
        //    //byte[] b = sanling.GetSendStr("02", "30", "31304636", "3034", "03");//D123-127
        //    //byte[] b = sanling.GetSendStr("05", "3030", "4646425241583030", "3030", "3039");
        //    sp.Write(b, 0, b.Length);
        //    System.Threading.Thread.Sleep(200);
        //    byte[] buffer = new byte[0x400];
        //    if (sp.IsOpen && sp.BytesToRead != 0)
        //    {
        //        sp.Read(buffer, 0, 100);
        //    }
        //    string redstr = "";
        //    for (int i = 0; i < 100; i++)
        //    {
        //        redstr += "-" + buffer[i].ToString();
        //    }
        //    return redstr;
        //}

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sp"></param>
        public static void QiDong(System.IO.Ports.SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            System.Threading.Thread.Sleep(100);

            byte[] b = new byte[2];
            b[0] = Convert.ToByte(35);
            b[1] = Convert.ToByte(71);

            sp.Write(b, 0, b.Length);
            System.Threading.Thread.Sleep(20);
            sp.DiscardInBuffer();
            System.Threading.Thread.Sleep(20);
            sp.DiscardOutBuffer();
            System.Threading.Thread.Sleep(20);
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sp"></param>
        public static void TingZhi(System.IO.Ports.SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            byte[] b = new byte[2];
            b[0] = Convert.ToByte(35);
            b[1] = Convert.ToByte(85);
            for (int i = 0; i < 5; i++)
            {

                sp.DiscardInBuffer();
                System.Threading.Thread.Sleep(20);
                sp.DiscardOutBuffer();
                System.Threading.Thread.Sleep(20);
                sp.Write(b, 0, b.Length);
                System.Threading.Thread.Sleep(20);
                if (sp.ReadExisting() != "")
                {
                    break;
                }


            }
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="strGY">高压</param>
        /// <param name="strDZ">电阻</param>
        /// <param name="strTime">时间</param>
        /// <param name="sp">串口对象</param>
        /// <returns></returns>
        public static string SheZhiCanShu(string strGY, string strDZ, string strTime, System.IO.Ports.SerialPort sp)
        {
            //设置参数
            try
            {
                int gy = Convert.ToInt32(strGY);
                int dz = Convert.ToInt32(strDZ);
                int sj = Convert.ToInt32(Convert.ToDouble(strTime) * 10);

                byte[] b = new byte[11];
                b[0] = Convert.ToByte(35);
                b[1] = Convert.ToByte(83);
                b[2] = Convert.ToByte(2);
                b[3] = Convert.ToByte(0);

                string gyHex = DataChange.IntToHexStr(gy);
                b[4] = Convert.ToByte(gyHex.Substring(2, 2), 16);
                b[5] = Convert.ToByte(gyHex.Substring(0, 2), 16);

                string dzHex = DataChange.IntToHexStr(dz);
                b[6] = Convert.ToByte(dzHex.Substring(2, 2), 16);
                b[7] = Convert.ToByte(dzHex.Substring(0, 2), 16);

                string sjHex = DataChange.IntToHexStr(sj);
                b[8] = Convert.ToByte(sjHex.Substring(2, 2), 16);
                b[9] = Convert.ToByte(sjHex.Substring(0, 2), 16);

                int sum = 0;
                for (int i = 2; i <= 9; i++)
                {
                    sum = (b[i] + sum) % 256;
                }
                b[10] = Convert.ToByte(sum);
                sp.Write(b, 0, b.Length);
                System.Threading.Thread.Sleep(20);

                byte[] r = new byte[20];
                if (sp.IsOpen && sp.ReadBufferSize > 0)
                {
                    sp.Read(r, 0, r.Length);
                }
                System.Threading.Thread.Sleep(20);
                sp.DiscardInBuffer();
                System.Threading.Thread.Sleep(20);
                sp.DiscardOutBuffer();
                string str = "";
                foreach (byte bb in r)
                {
                    if (str == "")
                    {
                        str = bb.ToString();
                    }
                    else
                    {
                        str += "-" + bb.ToString();
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static void OpenPort(SerialPort sp, string PortName)
        {
            try
            {
                if (!sp.IsOpen)
                {
                    //通讯控件设置
                    sp.PortName = PortName;
                    sp.StopBits = StopBits.One;
                    sp.Parity = Parity.None;
                    sp.BaudRate = 9600;
                    sp.DataBits = 8;

                    sp.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

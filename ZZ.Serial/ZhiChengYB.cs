using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ZZ.Serial
{
    public class ZhiChengYB
    {

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string ReadData(System.IO.Ports.SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            byte[] b = new byte[2];
            b[0] = Convert.ToByte(35);
            b[1] = Convert.ToByte(68);
            sp.DiscardInBuffer();
            System.Threading.Thread.Sleep(20);
            sp.Write(b, 0, b.Length);
            System.Threading.Thread.Sleep(20);

            string str = "";

            if (sp.IsOpen && sp.ReadBufferSize > 0)
            {

                sp.DiscardOutBuffer();
                System.Threading.Thread.Sleep(20);
                str = sp.ReadExisting();
            }
            if (str == "" || str.Trim() == "NN")
            {
                return "NN";
            }
            else
            {
                if (str.IndexOf("V") == 0 || str.IndexOf("M") == 0)
                {
                    return "NN";
                }
                try
                {
                    return str.Substring(str.IndexOf('V') + 1, str.IndexOf('M') - str.IndexOf('V') - 1).Trim() + "-" + str.Substring(str.Length - 2, 1);
                }
                catch
                {
                    return "NN";
                }
            }
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ZZ.Serial
{
    public class DaoDianWenKongYB
    {
        private static string str_readdata;
        private static string str_writedata;

        /// <summary>
        /// 读取数据报文
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="subAddr"></param>
        /// <param name="code"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        public static string SendReadData(string addr, string subAddr, string code, string dataLength)
        {

            str_readdata =Convert.ToChar(2)+ addr + subAddr + "R" + code + dataLength+Convert.ToChar(3)+CheckData(addr,subAddr,"R",code,dataLength,"")+Convert.ToChar(13)+Convert.ToChar(10);
            return str_readdata;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr">本机地址</param>
        /// <param name="subAddr">分机地址</param>
        /// <param name="code">指定代码（参表）</param>
        /// <param name="writeData">写入数据，16进制值写入，格式为例：,0A8B</param>
        /// <returns></returns>dataLength
        public static string SendWriteData( string addr, string subAddr,string code, string writeData)
        {
            str_writedata = Convert.ToChar(2) + addr + subAddr + "W" + code + "0" +writeData+ Convert.ToChar(3) + CheckData(addr, subAddr, "W", code, "0", writeData) + Convert.ToChar(13);
            return str_writedata;
        }


        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        public static string CheckData(string addr, string subAddr, string rw, string code, string dataLength, string writeData)
        {
            

            string stotal = addr + subAddr + rw + code + dataLength + writeData;
            int iTotal = 0;
            char[] cCode = stotal.ToCharArray();
            for (int i = 0; i < cCode.Length; i++)
            {
                iTotal += Convert.ToInt32(cCode[i]);
            }
            string Hex = Convert.ToString(iTotal + 5, 16);
            if (Hex.Length > 1)
            {
                Hex = Hex.Substring(Hex.Length - 2, 2);
            }
            else
            {
                Hex = "0" + Hex;
            }
            return Hex.ToUpper() ;
        }


        public static void OpenPort(System.IO.Ports.SerialPort sp, string portName)
        {
            try
            {
                //通讯控件设置
                sp.PortName = portName;
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.Even;
                sp.BaudRate = 9600;
                sp.DataBits = 7;

                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

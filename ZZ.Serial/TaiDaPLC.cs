using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ZZ.Serial
{
    public class TaiDaPLC
    {
        private static string plcAddr = "01";
     
        /// <summary>
        /// PLC地址
        /// </summary>
        public static string PLCAddr
        {
            get
            {
                return plcAddr;
            }
            set
            {
                plcAddr = value;
            }
        }

        /// <summary>
        /// 返回发送指令字符串，调用e.g:sp.Write(TaiDaPLC.FaSong_TaiDa_PLC(nyjCommand[0].ToString(),true, "FF00"));
        /// </summary>
        /// <param name="point">PLC地址</param>
        /// <param name="isRead">读/写状态</param>
        /// <param name="lastPoint">地址读取长度/状态</param>
        /// <returns></returns>
        public static string FaSong_TaiDa_PLC(string point, bool isRead, string lastPoint)
        {
            if (lastPoint.Length == 4)
            {
                string dz = plcAddr + ConvertPLC(point, isRead) + lastPoint;
                string sSql = SplitPLC(dz);
                string[] args = sSql.Split(',');
                return Send_TaiDa_PLC(args[0], args[1], args[2], args[3], args[4], args[5]);
            }

            return "";        
        }

        /// <summary>
        /// PLC字符串增加逗号
        /// </summary>
        /// <param name="sPLC"></param>
        /// <returns></returns>
        private static string SplitPLC(string sPLC)
        {
            string spl = "";
            for (int i = 0; i < sPLC.Length; i += 2)
            {
                spl += sPLC.Substring(i, 2) + ",";

            }
            return spl.Substring(0, spl.Length - 1);
        }

        /// <summary>
        /// ConvertPLC补充函数
        /// </summary>
        /// <param name="s1">PLC地址（十六进制）</param>
        /// <param name="s2">寄存器地址（十六进制）</param>
        /// <returns></returns>
        private static string ConvertPoint(string s1, string s2) //"1C2", "09"
        {
            string sReturn = "";
            if (s1.Length > 2) 
            {
                //int iplc = DataChange.HexToInt(s2) + Int32.Parse(s1.Substring(0, 1));
                int iplc = DataChange.HexToInt(s2);
                string temp = Convert.ToString(iplc, 16).ToUpper();
                if (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                sReturn += temp;
                sReturn += s1.Substring(1, 2);
            }
            else if (s1.Length < 2)
            {
                s1 = "0" + s1;
                sReturn += s2;
                sReturn += s1;
            }
            else
            {
                sReturn += s2;
                sReturn += s1;
            }
            return sReturn;
        }
        /// <summary>
        /// 转换PLC指令 S,X,Y,M,T,D
        /// </summary>
        /// <param name="sPLC"></param>
        /// <returns></returns>
        private static string ConvertPLC(string sPLC, bool isRead)
        {
            string sReturn = "";
            string PL1 = sPLC.Substring(0, 1);
            string PL2 = sPLC.Substring(1, sPLC.Length - 1);
            int pl = Int32.Parse(PL2);
            if (PL1 == "M")
            {
                if (isRead)
                {
                    sReturn += "02";
                }
                else
                {
                    sReturn += "05";
                }
                string spl = Convert.ToString(pl, 16).ToUpper();

                if (pl >= 0 && pl <= 255)
                {
                    sReturn += ConvertPoint(spl, "08");   
                }
                else if (pl > 255 && pl <= 511)
                {
                    sReturn += ConvertPoint(spl, "09");                   
                }
                else if (pl > 511 && pl <= 767)
                {
                    sReturn += ConvertPoint(spl, "0A");                  
                }
                else if (pl > 767 && pl <= 1023)
                {
                    sReturn += ConvertPoint(spl, "0B");                    
                }
                else if (pl > 1023 && pl <= 1279)
                {
                    sReturn += ConvertPoint(spl, "0C");
                }
            }
            else if (PL1 == "X")
            {
                sReturn += "02";
                sReturn += "04";
                string spl = Convert.ToString(pl, 8);
                if (spl.Length < 2)
                {
                    spl = "0" + spl;
                }
                sReturn += spl;
            }
            else if (PL1 == "T")
            {
                if (isRead)
                {
                    sReturn += "01";
                }
                else
                {
                    sReturn += "06";
                }

                if (pl >= 0 && pl < 256)
                {
                    sReturn += "06";
                }

                string spl = Convert.ToString(pl, 16).ToUpper();
                if (spl.Length < 2)
                {
                    spl = "0" + spl;
                }
                sReturn += spl;

            }
            else if (PL1 == "D")
            {
                if (isRead)
                {
                    sReturn += "03";
                }
                else
                {
                    sReturn += "06";
                }
                string spl = Convert.ToString(pl, 16).ToUpper();
                if (pl >= 0 && pl <= 255)
                {
                    sReturn += ConvertPoint(spl, "10");                   
                }
                else if (pl > 255 && pl <= 511)
                {
                    
                    sReturn += ConvertPoint(spl, "11");                    
                }
                else if (pl > 511 && pl <= 767)
                {
                    sReturn += ConvertPoint(spl, "12");                   
                }
                else if (pl > 767 && pl <= 1023)
                {
                    sReturn += ConvertPoint(spl, "13");                    
                }
                else if (pl > 1023 && pl <= 1279)
                {
                    sReturn += ConvertPoint(spl, "14");
                   
                }               
            }
            else if (PL1 == "Y")
            {
                if (isRead)
                {
                    sReturn += "02";
                }
                else
                {
                    sReturn += "05";
                }

                sReturn += "05";
                string spl = Convert.ToString(pl, 8);
                if (spl.Length < 2)
                {
                    spl = "0" + spl;
                }
                sReturn += spl;

            }
            else if (PL1 == "S")
            {
                if (isRead)
                {
                    sReturn += "03";
                }
                else
                {
                    sReturn += "05";
                }
                string spl = Convert.ToString(pl, 16).ToUpper();
                if (pl >= 0 && pl <= 255)
                {
                    sReturn += ConvertPoint(spl, "00");                   
                }
                else if (pl > 255 && pl <= 511)
                {
                    sReturn += ConvertPoint(spl, "01");                    
                }
                else if (pl > 511 && pl <= 767)
                {
                    sReturn += ConvertPoint(spl, "02");                   
                }
                else if (pl > 767 && pl <= 1023)
                {
                    sReturn += ConvertPoint(spl, "03");                   
                }
            }
            else if (PL1 == "C")
            {
                if (isRead)
                {
                    sReturn += "03";
                }
                else
                {
                    sReturn += "05";
                }
                string spl = Convert.ToString(pl, 16).ToUpper();
                sReturn += ConvertPoint(spl, "03");    
            }



            return sReturn;
        }


        /// <summary>
        /// 台达PLC发送指令
        /// </summary>
        /// <returns></returns>
        public static string Send_TaiDa_PLC(string Device_addr, string func_code, string data_addeHi, string data_addeLo, string data_Hi, string data_Lo)
        {
            int i = DataChange.HexToInt(Device_addr) + DataChange.HexToInt(func_code) + DataChange.HexToInt(data_addeHi) +
                    DataChange.HexToInt(data_addeLo) + DataChange.HexToInt(data_Hi) + DataChange.HexToInt(data_Lo);//将输入的16进制字符命令，累加求和
            int iMod = i % 256;//取摸，以保证其值不超过256，既一个字节
            int intLRC = 255 - iMod + 1;//将iMod去反，再加1
            string LowerstrLRC = Convert.ToString(intLRC, 16); //十进制转化成对应的16进制类型(小写)
            if (LowerstrLRC.Length == 1)
            {
                LowerstrLRC = "0" + LowerstrLRC;
            }
            string UpperstrLRC = LowerstrLRC.ToUpper();//将小写转大写。如eb变为EB
            string SendStr = ":" + Device_addr + func_code + data_addeHi + data_addeLo + data_Hi + data_Lo + UpperstrLRC + "\r\n";
            return SendStr;
        }
        /// <summary>
        /// 解析DeltaPLC 01 02 命令返回的的数据，此函数为DeltaCommandText函数的补充
        /// </summary>
        /// <param name="deltaReturnData">接收到的16进制的Delta数据,长度必须为偶数</param>
        /// <param name="codeNumber">对应的DeltaPLC的功能码</param>
        /// <returns></returns>

        public static string FormatDeltaReturnData(string deltaReturnData, int codeNumber)
        {
            int dataLength = deltaReturnData.Length;
            int i = 0;
            string[] s = new string[dataLength];
            string returnData = "", data = "";

            switch (codeNumber)
            {
                case 1:
                    break;
                case 2:
                    for (i = 0; i < dataLength; i++)
                    {
                        s[i] = Convert.ToString(int.Parse(deltaReturnData.Substring(i, 1), System.Globalization.NumberStyles.HexNumber), 2);
                        while (s[i].Length < 4)
                        {
                            s[i] = "0" + s[i];
                        }
                        data += s[i];
                    }
                    dataLength = data.Length;
                    string[] rs = new string[dataLength];
                    for (int j = 0; j < dataLength; j++)
                    {
                        rs[j] = data.Substring((dataLength) - j - 1, 1);
                        returnData += rs[j];
                    }
                    break;
                case 3:
                    for (i = 0; i < dataLength / 4; i++)
                    {
                        s[i] = Convert.ToString(int.Parse(deltaReturnData.Substring(i * 4, 4), System.Globalization.NumberStyles.HexNumber));
                        while (s[i].Length < 4)
                        {
                            s[i] = "0" + s[i];
                        }
                        returnData += s[i];

                    }
                    break;
                default:
                    break;


            }




            return returnData;
        }


        public static void OpenPort(SerialPort sp, string portName)
        {
            try
            {
                //通讯控件设置
                sp.PortName = portName;
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.None;
                sp.BaudRate = 9600;
                sp.DataBits = 8;
                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void OpenPort(SerialPort sp, string portName, StopBits stopBits, Parity parity, int baudRate, int dataBits)
        {
            try
            {
                //通讯控件设置
                sp.PortName = portName;
                sp.StopBits = stopBits;
                sp.Parity = parity;
                sp.BaudRate = baudRate;
                sp.DataBits = dataBits;
                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void OpenPort(ref SerialPort sp, string portName, StopBits stopBits, Parity parity, int baudRate, int dataBits)
        {
            try
            {
                //通讯控件设置
                sp.PortName = portName;
                sp.StopBits = stopBits;
                sp.Parity = parity;
                sp.BaudRate = baudRate;
                sp.DataBits = dataBits;
                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

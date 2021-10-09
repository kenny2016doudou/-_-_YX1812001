using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ZZ.Serial
{
    public class SanLingPLC
    {
        private static string plcAddr = "FF";
        private static string dbits = "16";
        private static string hexad = "十进制";

        public static string Hexad
        {
            get { return SanLingPLC.hexad; }
            set { SanLingPLC.hexad = value; }
        }
        public static string Dbits
        {
            get { return dbits; }
            set { dbits = value; }
        }

        public static string PlcAddr
        {
            get { return plcAddr; }
            set { plcAddr = value; }
        }
               
  
      
        /// <summary>
        /// 开启COM口
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="portName"></param>
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


        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string ReadData(System.IO.Ports.SerialPort sp,string[] args)
        {

            byte[] b = GetSendStr(args[0], args[1], args[2], args[3], args[4]);
            //serialPort1.Write();//02,
            //string cmdstr = sanling.GetSendStr("02", "30", "31304338", "3031", "03");
            //serialPort1.Write(cmdstr);
            //byte[] b = GetSendStr("02", "30", "31304338", "3031", "03");//D100
            //byte[] b = sanling.GetSendStr("02", "30", "31304636", "3034", "03");//D123-127
            //byte[] b = sanling.GetSendStr("05", "3030", "4646425241583030", "3030", "3039");
            sp.Write(b, 0, b.Length);
            System.Threading.Thread.Sleep(200);
            byte[] buffer = new byte[0x400];
            if (sp.IsOpen && sp.BytesToRead != 0)
            {
                sp.Read(buffer, 0, 100);
            }
            string redstr = "";
            for (int i = 0; i < 100; i++)
            {
                redstr += "-" + buffer[i].ToString();
            }
            return redstr;           
        }
       
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <returns></returns>
        public static byte[] GetSendStr(string STX, string func_code, string address, string dataCount, string ETX)
        {
            string SendStr = STX + func_code + address + dataCount + ETX;
            int len = SendStr.Length / 2;
            string[] args = new string[len];
            try
            {
                for (int i = 0; i < len; i++)
                {
                    args[i] = SendStr.Substring(i * 2, 2);
                }
            }
            catch
            {
                //
            }
            int intHex = 0;//将输入的16进制字符命令，累加求和
            for (int i = 1; i < len; i++)//STX为1
            {
                intHex += DataChange.HexToInt(args[i]);
            }
           

            string LRC = Convert.ToString(intHex, 16).ToUpper();
            while (LRC.Length < 4)
            {
                LRC = "0" + LRC;
            }
            LRC = LRC.Substring(LRC.Length - 2, 2);

            //将LRC转换成ASC再转成16进制
            ASCIIEncoding asc = new ASCIIEncoding();
            byte[] lrcs = asc.GetBytes(LRC);
            string LRC1 = Convert.ToString(lrcs[0], 16);
            string LRC2 = Convert.ToString(lrcs[1], 16);
            if (LRC1.Length == 1)
            {
                LRC1 = "0" + LRC1;
            }
            if (LRC2.Length == 1)
            {
                LRC2 = "0" + LRC2;
            }
            LRC = LRC1 + LRC2;
            byte[] b = new byte[args.Length + 2];
            for (int i = 0; i < args.Length; i++)
            {
                b[i] = Convert.ToByte(args[i], 16);
            }
            b[args.Length] = Convert.ToByte(LRC1, 16);
            b[args.Length + 1] = Convert.ToByte(LRC2, 16);
            //return SendStr + LRC + "\r\n";
            return b;
        }
              

             
        /// <summary>
        /// 指令转换（头，站号，PLC号，元件首地址，尾，校验）
        /// </summary>
        /// <param name="mPoint">PLC指令</param>
        /// <param name="isRead">0:读，1：写， -1 ：无效</param>
        /// <param name="isOn">0：开启，1：关闭 -1：无效</param>
        /// <param name="bytes">读取字节长度</param>  
        /// <returns></returns>
        public static string ConvertPoint(string mPoint, int isRead, int isOn, string bytes)
        {
            string strReturn = "";
            string startP = mPoint.Substring(0, 1);
            string endP = mPoint.Substring(1, mPoint.Length - 1);

            bool flag = true;

            strReturn += "02"; //STX

            //---------命令取值begin-------------//
            if (isRead != -1)
            {
                flag = true;
                if (startP == "X" || startP == "Y" || startP == "M" || startP == "S"
                    || startP == "T" || startP == "C" || startP == "D")
                {
                    if (isRead == 0)
                    {
                        strReturn += ","+ Convert.ToString(Convert.ToChar("0"), 16);
                        
                    }
                    else if (isRead == 1)
                    {
                        strReturn += ","+Convert.ToString(Convert.ToChar("1"), 16);
                        
                    }
                }
            }

            if (isOn != -1 && startP != "D")
            {
                flag = false;
                if (isOn == 0)
                {
                    strReturn += "," + Convert.ToString(Convert.ToChar("7"), 16);
                    
                }
                else if (isOn == 1)
                {
                    strReturn += "," + Convert.ToString(Convert.ToChar("8"), 16);
                  
                }                
            }
            //---------命令取值end--------------//

            strReturn += "," + CalculateAddress(Convert.ToInt32(endP),flag);

            //读取位数 e.g. 3034 表示读4个字节数据
            strReturn += "," + bytes;
            if (flag)
            {
                strReturn += ",03";//ETX
            }

            return strReturn;
        }

        
        /// <summary>
        /// 首地址取值 - ConvertPoint补充函数
        /// </summary>
        /// <param name="address"></param>
        /// <param name="flag">true: read/write  ,  false:on/of</param>
        /// <returns></returns>
        public static string CalculateAddress(int address, bool flag)
        {
            string ss = "";
            int num1 = 0;
            int num2 = 0;
            if (flag)
            {
                num1 = address * 2;
                num2 = num1 + DataChange.HexToInt("1000");
            }
            else
            {
                num1 = address / 8;
                num2 = num1 + DataChange.HexToInt("100");
            }
            string num3 = DataChange.IntToHexStr(num2);
           
            //十六进制转ascii码
            for (int i = 0; i < num3.Length; i++)
            {
                char temp = Convert.ToChar(num3.Substring(i, 1));
                ss += Convert.ToString(temp, 16);
            }

            return ss;
        }


        /// <param name="isOn">0：开启，1：关闭 -1：无效</param>
        /// <param name="bytes">读取字节长度</param>
        /// <param name="flag">true: read/write  ,  false:on/of</param>

        /// <summary>
        /// 指令转换（请求, 站号，PLC号（默认FF），命令，延时（默认A：100毫秒），元件首地址，原件数量，校验）
        /// </summary>
        /// <param name="qingQiu">请求</param>
        /// <param name="zhanHao">站号</param>
        /// <param name="isRead">true：读， false：写 </param>
        /// <param name="mPoint">PLC</param>
        /// <param name="mLength">元件数量</param>
        /// <param name="writeStr">写入值</param>
        /// <returns></returns>
        public static byte[] ConvertPointYuanJian(string qingQiu, string zhanHao, bool isRead, string mPoint, string mLength, string writeStr)
        {
            string sr = "";
            string sReturn = GetQingQiu(qingQiu) + ",";

            string strReturn = "";

            strReturn += zhanHao;
            strReturn += "," + plcAddr;
            if (isRead)
            {
                strReturn += "BR";
            }
            else
            {

                strReturn += "WW";
            }

            strReturn += "A";

            string m1 = mPoint.Substring(0, 1);
            string m2 = mPoint.Substring(1, mPoint.Length - 1);
            while (m2.Length < 4)
            {
                m2 = "0" + m2;
            }

            strReturn += m1;
            strReturn += m2;

            strReturn += "," + mLength;

            if (writeStr != "")
            {
                strReturn += ",";
                while (writeStr.Length < 4)
                {
                    writeStr += "0" + writeStr;
                }
                strReturn += writeStr;
            }


            sr += sReturn;
            for (int i = 0; i < strReturn.Length; i++)
            {

                string temp = strReturn.Substring(i, 1);
                if (temp == ",")
                {
                    sr += ",";
                    continue;
                }
                char tmp = Convert.ToChar(temp);
                sr += Convert.ToString(tmp, 16).ToUpper();
            }

            string[] args = sr.Split(',');
            if (args.Length == 5)
            {
                return GetSendStr(args[0], args[1], args[2], args[3], args[4]);
            }
            else
            {
                return GetSendStr(args[0], args[1], args[2], args[3], "");
            }
            //ENQ  0 0  F F  B R  A  X 0 0 0 0  0 9  4 2

        }

        /// <summary>
        /// 获取请求的地址 - ConvertPointYuanJian补充函数
        /// </summary>
        /// <param name="strQingQiu"></param>
        /// <returns></returns>
        public static string GetQingQiu(string strQingQiu)
        {
            if (strQingQiu == "ENQ")
            {
                return "05";
            }
            else if (strQingQiu == "ACK")
            {
                return "06";
            }
            else if (strQingQiu == "STX")
            {
                return "02";
            }
            else if (strQingQiu == "EXT")
            {
                return "03";
            }
            return "";
        }

        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mPoint"></param>
        /// <param name="isFuWei">true:复位 , false :置位</param>
        /// <returns></returns>
        public static byte[] ConvertPLC(string mPoint,bool isFuWei)
        {            
            string Address_M = "";
            string m1 = mPoint.Substring(0, 1);
            string m2 = mPoint.Substring(1, mPoint.Length - 1);

            switch (m1)
            {                
                case "M":
                    Address_M = M_Address(m2);
                    break;
                case "T":
                    Address_M = T_Address(m2);
                    break;
                case "C":
                    Address_M = C_Address(m2);
                    break;

                case "Y":
                    Address_M = Y_Address(m2);
                    break;
                case "S":
                    Address_M = S_Address(m2);
                    break;
                case "X":
                    Address_M = X_Address(m2);
                    break;
            }

            if (!Restrictions(m1, m2))
            {
                if (!isFuWei)
                {
                    //sReturn = "02," +"7" + Address_M + "03";
                    char tmp = Convert.ToChar("7");

                    return GetSendStr("02", Convert.ToString(tmp, 16).ToUpper(), strToAsc(Address_M), "", "03");
                }
                else
                {
                    char tmp = Convert.ToChar("8");
                    return GetSendStr("02", Convert.ToString(tmp, 16).ToUpper(), strToAsc(Address_M), "", "03");
                }
            }           
            else
            {

                return null;
            }
            
        }

        /// <summary>
        /// 单个字符转16进制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string strToAsc(string str)
        {
            ASCIIEncoding asc = new ASCIIEncoding();
            string sReturn ="";
            char[] chr = str.ToCharArray();
            for (int i = 0; i < chr.Length; i++)
            { 
                byte[] lrcs = asc.GetBytes(chr[i].ToString());
                sReturn += Convert.ToString(lrcs[0], 16).ToString();

            }
            return sReturn;
        }

        /// <summary>
        /// T点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string T_Address(string data)
        {
            string m_address = "";
            int idata = Convert.ToInt32(data);
            string s1 = DataChange.IntToHexStr(idata);
            while (s1.Length < 2)
            {
                s1 = "0" + s1;                
            }
            m_address = s1.Substring(s1.Length - 2, 2) + "06";

            return m_address;
        }

        /// <summary>
        /// C点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string C_Address(string data)
        {
            string m_address = "";
            int idata = Convert.ToInt32(data);
            string s1 = DataChange.IntToHexStr(idata);
            while (s1.Length < 2)
            {
                s1 = "0" + s1;                
            }
            m_address = s1.Substring(s1.Length - 2, 2) + "0E";

            return m_address;
        }

        /// <summary>
        /// Y点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Y_Address(string data)
        {
            string m_address = "";
            int idata = Convert.ToInt32(data);
            string s1 = Convert.ToString(idata, 8);
            while (s1.Length < 2)
            {
                s1 = "0" + s1;                
            }
            m_address = s1.Substring(s1.Length - 2, 2) + "05";
            return m_address;
            
        }

        /// <summary>
        /// X点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string X_Address(string data)
        {
            string m_address = "";
            int idata = Convert.ToInt32(data);
            string s1 = Convert.ToString(idata, 8);
            while (s1.Length < 2)
            {
                s1 = "0" + s1;                
            }
            m_address = s1.Substring(s1.Length - 2, 2) + "04";
            return m_address;

        }

        /// <summary>
        /// S点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string S_Address(string data)
        {
            string m_address = "";
            int idata = Convert.ToInt32(data);
            string s1 = DataChange.IntToHexStr(idata);
            if (idata > 255)
            {               
                m_address = "0" + s1.Substring(0, 1) + s1.Substring(s1.Length - 2, 2);
            }
            else if (idata > 15)
            {
                m_address = s1 + "00";
            }
            else
            {
                m_address = "0" + s1 + "00";
            }

            return m_address;
        }
       

        /// <summary>
        /// 获取M点
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string M_Address(string str)
        {           
            string m_address;
            int s = Convert.ToInt32(str);//11
            if (s < 256)
            {
                string s1 = DataChange.IntToHexStr(s);
                while (s1.Length < 2)
                {
                    s1 = "0" + s1;
                }
                m_address = s1.Substring(s1.Length - 2, 2) + "08";
            }
            else
            {
                string s1 = DataChange.IntToHexStr(s);
                m_address = s1.Substring(s1.Length - 2, 2) + "0" + s1.Substring(0, 1) + "8";
            }
            return m_address;
        }

        //范围判断
        public static bool Restrictions(string addr, string data)
        {
            bool flag = false;
            if (addr == null || data == null) return flag;
            int idata = Convert.ToInt32(data);
            switch (addr)
            { 
                case "M":
                    if (idata > 1023)
                    {
                        flag = true;
                    }                   
                    break;
                case "T":
                    if (idata > 255) flag = true;
                    
                    break;
                case "C":
                    if (idata > 255) flag = true;
                    
                    break;
                case "Y":
                    if (idata > 177) flag = true;
                    break;
                case "S":
                    if (idata > 999) flag = true;
                    break;
                case "X":
                    if (idata > 177) flag = true;
                    break;
                default:
                    flag = false;
                    break;

            }

            return flag;
        }

        /// <summary>
        /// 读PLC
        /// </summary>
        /// <param name="mPoint"></param>
        /// <returns></returns>
        public static byte[] ReadPLC(string mPoint)
        {            
            string Address_M = "";
            string address = "";
            string m1 = mPoint.Substring(0, 1);
            string m2 = mPoint.Substring(1, mPoint.Length - 1);
            int im2 = Convert.ToInt32(m2);
            switch (m1)
            {
                case "D":
                    if (im2 > 999)
                    {
                        return null;
                    }
                    else
                    {
                        address = DataChange.IntToHexStr(im2 * 2 + 4096);
                        if (dbits == "16") //16位数据
                        {
                            Address_M = "0" + address + "02";
                        }
                        else
                        {//32位数据
                            Address_M = "0" + address + "04";
                        }
                    }
                    break;
                case "T":
                    if (im2 > 127)
                    {
                        return null;
                    }
                    else
                    {
                        address = "0" + DataChange.IntToHexStr(im2 * 2 + 2048);
                        if (dbits == "16") //16位数据
                        {
                            Address_M = "0" + address + "02";
                        }
                        else
                        {//32位数据
                            Address_M = "0" + address + "04";
                        }
                    }
                    break;
                case "C":
                    if (im2 > 127)
                    {
                        return null;
                    }
                    else
                    {
                        address = "0" + DataChange.IntToHexStr(im2 * 2 + 2560);
                        if (dbits == "16") //16位数据
                        {
                            Address_M = "0" + address + "02";
                        }
                        else
                        {//32位数据
                            Address_M = "0" + address + "04";
                        }
                    }
                    break;               
            }
           

            return GetSendStr("02", strToAsc(Address_M), "", "", "03");
                      
        }



        /// <summary>
        /// 读取XY点的命令
        /// </summary>
        /// <param name="mPoint"></param>
        /// <returns></returns>
        public static string ReadPLC_XY(string mPoint)
        {
            string senddata = "";
            if (mPoint.Substring(0, 1) == "X")
            {
               senddata="0008004"+Convert.ToChar(3);


               return (Convert.ToChar(2) + senddata + SumChk(senddata)).ToUpper();
            }
            else if (mPoint.Substring(0, 1) == "Y")
            {
                senddata = "000A004" + Convert.ToChar(3);
                return (Convert.ToChar(2) + senddata + SumChk(senddata)).ToUpper();
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 写PLC
        /// </summary>
        /// <param name="mPoint"></param>
        /// <param name="writeStr"></param>
        /// <returns></returns>
        public static byte[] WritePLC(string mPoint,string writeStr)
        {          
            string Address_M = "";
            string address = "";
            string data_1;
            string data_2;
            string data_3;
            string data_4;
            string m1 = mPoint.Substring(0, 1);
            string m2 = mPoint.Substring(1, mPoint.Length - 1);
            int im2 = Convert.ToInt32(m2);
            switch (m1)
            {
                case "D":
                    if (im2 > 999)
                    {
                        return null;
                    }
                    if (dbits == "16")
                    {
                        address = "1" + DataChange.IntToHexStr(im2 * 2 + 4096) + "02";
                    }
                    else
                    {
                        address = "1" + DataChange.IntToHexStr(im2 * 2 + 4096) + "04";
                    }
                    break;
                case "T":
                    if (im2 > 127)
                    {
                        return null;
                    }
                    if (dbits == "16")
                    {
                        address = "1" + "0" + DataChange.IntToHexStr(im2 * 2 + 2048) + "02";
                    }
                    else
                    {
                        address = "1" + "0" + DataChange.IntToHexStr(im2 * 2 + 2048) + "04";
                    }
                    break;
                case "C":
                    if (im2 > 127)
                    {
                        return null;
                    }
                    if (dbits == "16")
                    {
                        address = "1" + "0" + DataChange.IntToHexStr(im2 * 2 + 2560) + "02";
                    }
                    else
                    {
                        address = "1" + "0" + DataChange.IntToHexStr(im2 * 2 + 2560) + "04";
                    }
                    break;
            }

            if (hexad == "十进制")
            {
                if (dbits == "32")
                {
                    if (double.Parse(writeStr) > 2147483648) return null;
                    string tmp = "00000000" + DataChange.IntToHexStr(Convert.ToInt32(writeStr));
                    data_1 = tmp.Substring(tmp.Length - 8, 8);
                    string tmp_21 = data_1.Substring(data_1.Length - 4, 4);
                    string tmp21 = tmp_21.Substring(tmp_21.Length - 2, 2);
                    string tmp_22 = data_1.Substring(data_1.Length - 4, 4);
                    string tmp22 = tmp_22.Substring(0, 2);
                    data_2 = tmp21 + tmp22;

                    string tmp_31 = data_1.Substring(0, 4);
                    string tmp31 = tmp_31.Substring(tmp_31.Length - 2, 2);
                    string tmp_32 = data_1.Substring(0, 4);
                    string tmp32 = tmp_32.Substring(0, 2);
                    data_3 = tmp31 + tmp32;

                    data_4 = data_2 + data_3;
                    Address_M = address + data_4;

                    return GetSendStr("02", strToAsc(Address_M), "", "", "03");
                }
                else
                {
                    if (double.Parse(writeStr) > 32767)
                    {
                        return null;
                    }
                    string tmp = "00000000" + DataChange.IntToHexStr(Convert.ToInt32(writeStr));
                    data_1 = tmp.Substring(tmp.Length - 4, 4);
                    data_1 = data_1.Substring(data_1.Length - 2, 2) + data_1.Substring(0, 2);

                    Address_M = address + data_1;
                    return GetSendStr("02", strToAsc(Address_M), "", "", "03");
                }
            }
            else
            {
                if (dbits == "32")
                {
                    if (double.Parse(DataChange.IntToHexStr(Convert.ToInt32(writeStr))) > 2147483648) return null;
                    data_1 = Right("00000000" + writeStr, 8);
                    data_2 = Right(Right(data_1, 4), 2) + Left(Right(data_1, 4), 2);
                    data_3 = Right(Left(data_1, 4), 2) + Left(Left(data_1, 4), 2);
                    data_4 = data_2 + data_3;
                    Address_M = address + data_4;
                    return GetSendStr("02", strToAsc(Address_M), "", "", "03");
                }
                else
                {
                    if (double.Parse(DataChange.IntToHexStr(Convert.ToInt32(writeStr))) > 32767) return null;
                    data_1 = Right("00000000" + writeStr, 4);
                    data_1 = Right(data_1, 2) + Left(data_1, 2);
                    Address_M = address + data_1;
                    return GetSendStr("02", strToAsc(Address_M), "", "", "03");
                }
            }
            
        }       

        public static string Right(string var1, int var2)
        {

            string result;
            result = var1.Substring(var1.Length - var2, var2);
            
            return result;
        }


        public static string Left(string var1, int var2)
        {
            return var1.Substring(0, var2);
        }


        public static string FormaData(byte[] buffer)
        {
            string redstr = "";
            for (int i = 0; i < 100; i++)
            {
                redstr += "-" + buffer[i].ToString();
            }


            string[] s1 = redstr.Split('-');
            int kaishi = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == "2")
                {
                    kaishi = i;
                    break;
                }
            }
              return Convert.ToInt32(Convert.ToChar(int.Parse(s1[kaishi + 1])).ToString() + Convert.ToChar(int.Parse(s1[kaishi + 2])).ToString(),10).ToString();
        }


   
        /// <summary>
        /// 获取Add校验核
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string SumChk(string data)
        {
            int chk=0;
            ASCIIEncoding asc = new ASCIIEncoding();
           
            for (int i = 0; i < data.Length; i++)
            {
               chk+= asc.GetBytes(data.Substring(i,1))[0];
            }
            string temp = Convert.ToString(chk,16);
            return Right(temp, 2);

        }


        /// <summary>
        /// 获得xy的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetXorY(byte[] buffer, int index)
        {
            string data="";
            string redstr = "";
            for (int i = 0; i < 100; i++)
            {
                redstr += "-" + buffer[i].ToString();
            }

            string[] s = redstr.Split('-');

            for (int i = 2; i <= 9; i++)
            {
                data += Convert.ToChar(int.Parse(s[i]));
            }

            string[] tempdata = new string[4];
            tempdata[0] = dectoBin(Convert.ToInt32(data.Substring(0, 2), 16));
            tempdata[1] = dectoBin(Convert.ToInt32(data.Substring(2, 2), 16));
            tempdata[2] = dectoBin(Convert.ToInt32(data.Substring(4, 2), 16));
            tempdata[3] = dectoBin(Convert.ToInt32(data.Substring(6, 2), 16));

            string result = tempdata[3] + tempdata[2] + tempdata[1] + tempdata[0];
            result = Reverse(result);
            char[] ch_result = result.ToCharArray();
            int temp = index / 10;
            return ch_result[temp*8+index%10].ToString();
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Reverse(string data)
        {
            string sReturn = "";
            for (int i = data.Length-1; i >= 0; i--)
            {
                sReturn += data[i];
            }
            return sReturn;
        }

        /// <summary>
        /// 密码算法 10进制转换2进制
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        private static string dectoBin(int var)
        {


            const string Bins = "00000000100100011010001010110011110001001101010111100110111101111";

            string y = Convert.ToString(var, 16);
            string s = "";
            for (int i = 0; i < y.Length; i++)
            {
                s += Bins.Substring(Convert.ToInt32(y.Substring(i, 1), 16) * 4 + 1, 4);
            }

            while (s.Length < 8)
            {
                s = "0" + s;
            }
            return s;
        }

    } 

    


}

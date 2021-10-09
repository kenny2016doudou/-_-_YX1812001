using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.PLC.Omron.HostLink.Fins;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    internal class OmronPLC
    {

        private static string plcAddr = "00";
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
        /// 电脑与PLC直接连接不通过网络,读取数据
       /// </summary>
       /// <param name="ac">枚举区域代码</param>
       /// <param name="address">读取地址</param>
       /// <param name="number">读取位数</param>
       /// <returns></returns>
        public static string SendReadString(AreaCode ac,int address,int number)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@");
            sb.Append(plcAddr);//站号
            sb.Append("FA");
            sb.Append("0");//响应等待时间 设置值为0-F，例如：设置为2 则响应等待时间为 20ms
            sb.Append("00");//ICF 消息控制区域，直接连PC机，设置为 00
            sb.Append("00");//DA2 固定为00
            sb.Append("00");//SA2 固定为00
            sb.Append("00");//SID 固定为00
            sb.Append("0101");//读
            sb.Append(CodeSwitch(ac));//读取区域 例如DM,WR,CIO区等
            sb.Append(AddZero_Right(Convert.ToString(address, 16),6));//读取地址

            sb.Append(AddZero_Left(Convert.ToString(number, 16),4));//读取位数
            
            return FCS(sb.ToString());
        }
        /// <summary>
        /// 电脑与PLC直接连接 ,读取数据
        /// </summary>
        /// <param name="ac">区域代码</param>
        /// <param name="address">连接地址2个byte位 对应4个16进制 例如:对应D130  那么位置就是0082</param>
        /// <param name="value">读取的数据通道个数</param>
        /// <param name="dt">数据类型</param>
        /// <returns></returns>
        public static string SendReadString(AreaCode ac, int address, int number, DataType dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@");
            sb.Append(plcAddr);//站号
            sb.Append("FA");
            sb.Append("0");//响应等待时间 设置值为0-F，例如：设置为2 则响应等待时间为 20ms
            sb.Append("00");//ICF 消息控制区域，直接连PC机，设置为 00
            sb.Append("00");//DA2 固定为00
            sb.Append("00");//SA2 固定为00
            sb.Append("00");//SID 固定为00
            sb.Append("0101");//读
            sb.Append(CodeSwitch(ac));//读取区域 例如DM,WR,CIO区等
            sb.Append(AddZero_Left(Convert.ToString(address, 16), 4));//读取地址
            if (dt == DataType.Bit)
            {
                sb.Append("0D");
            }
            else if (dt == DataType.Word)
            {
                sb.Append("00");
            }
            sb.Append(AddZero_Left(Convert.ToString(number, 16), 4));//读取位数

            return FCS(sb.ToString());
        }


        /// <summary>
        /// 判断通信知否成功执行
        /// </summary>
        /// <param name="ResposeString">返回的响应码</param>
        /// <param name="cmdType">命令类型 是Read 还是Write</param>
        /// <returns></returns>
        public static bool CommandExecuteResult(string ResponseString,CommandType cmdType)
        {
            bool result = false;
            try
            {
                int Index=0;
                if (cmdType == CommandType.Read)
                {
                    Index=ResponseString.IndexOf("0101");
                   
                    if ( Index> 0)
                    {
                        result = ResponseString.Substring(Index + 4, 4) == "0000" ? true : false;
                    }
                }
                else if (cmdType == CommandType.Write)
                {
                    Index = ResponseString.IndexOf("0102");
                    if (Index > 0)
                    {
                        result = ResponseString.Substring(Index + 4, 4) == "0000" ? true : false;
                    }
                }
            }
            catch
            {
 
            }

            return result;
        }

        /// <summary>
        /// 根据返回的对象获得数据值
        /// </summary>
        /// <param name="ResponseString">响应内容</param>
        /// <param name="cmdType">命令类型 是Read 还是Write</param>
        /// <returns></returns>
        public static string[] GetResponse(string ResponseString,CommandType cmdType)
        {
            string[] Response = null;

            try
            {
                if (CommandExecuteResult(ResponseString, CommandType.Read))
                {
                    int Start=ResponseString.IndexOf("0101")+8;
                    int DataCount = SubStr(ResponseString, Start, ResponseString.Length - Start).Length / 4;
                    Response = new string[DataCount-1];
                    for (int i = 0; i < Response.Length; i++)
                    {
                        Response[i] = Convert.ToInt32(SubStr(ResponseString,Start+i*4,4), 16).ToString();
                    }
                    
                }
                else if (CommandExecuteResult(ResponseString, CommandType.Write))
                {
                    int Start = ResponseString.IndexOf("0102") + 8;
                    int DataCount = SubStr(ResponseString, Start, ResponseString.Length - Start).Length / 4;
                    Response = new string[DataCount-1];
                    for (int i = 0; i < Response.Length; i++)
                    {
                        Response[i] = Convert.ToInt32(SubStr(ResponseString, Start + i * 4, 4), 16).ToString();
                    }
                }

            }
            catch
            {
 
            }

            return Response;

 
        }

        /// <summary>
        /// 根据返回的对象获得数据值
        /// </summary>
        /// <param name="ResponseString">响应内容</param>
        /// <param name="cmdType">命令类型 是Read 还是Write</param>
        /// <param name="sublength">在命令代码后面截取的位数</param>
        /// <returns></returns>
        public static string[] GetResponse(string ResponseString, CommandType cmdType, int sublength)
        {
            string[] Response = null;

            try
            {
                if (CommandExecuteResult(ResponseString, CommandType.Read))
                {
                    int Start = ResponseString.IndexOf("0101") + sublength;
                    int DataCount = SubStr(ResponseString, Start, ResponseString.Length - Start).Length / 4;
                    Response = new string[DataCount - 1];
                    for (int i = 0; i < Response.Length; i++)
                    {
                        Response[i] = Convert.ToInt32(SubStr(ResponseString, Start + i * 4, 4), 16).ToString();
                    }

                }
                else if (CommandExecuteResult(ResponseString, CommandType.Write))
                {
                    int Start = ResponseString.IndexOf("0102") + sublength;
                    int DataCount = SubStr(ResponseString, Start, ResponseString.Length - Start).Length / 4;
                    Response = new string[DataCount - 1];
                    for (int i = 0; i < Response.Length; i++)
                    {
                        Response[i] = SubStr(ResponseString, Start + i * 4, 4);
                    }
                }

            }
            catch
            {
               

            }

            return Response;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="Start">截取字符串起始位置</param>
        /// <param name="length">截取字符串长度</param>
        /// <returns></returns>
        public static string SubStr(string Str, int Start, int length)
        {
            return Str.Substring(Start, length);
        }


        /// <summary>
        /// 电脑与PLC直接连接不通过网络,写入数据
        /// </summary>
        /// <param name="ac">枚举区域代码</param>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入值</param>
        /// <returns></returns>
        public static string SendWriteString(AreaCode ac, int address, params int[] value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@");
            sb.Append(plcAddr);//站号
            sb.Append("FA");
            sb.Append("0");//响应等待时间 设置值为0-F，例如：设置为2 则响应等待时间为 20ms
            sb.Append("00");//ICF 消息控制区域，直接连PC机，设置为 00
            sb.Append("00");//DA2 固定为00
            sb.Append("00");//SA2 固定为00
            sb.Append("00");//SID 固定为00
            sb.Append("0102");//写
            sb.Append(CodeSwitch(ac));//读取区域 例如DM,WR,CIO区等
            sb.Append(AddZero_Right(Convert.ToString(address, 16), 6));//读取地址
            for (int i = 0; i < value.Length; i++)
            {
                sb.Append(AddZero_Left(Convert.ToString(value[i], 16), 4));//读取位数
            }
            return FCS(sb.ToString());
        }

        /// <summary>
        /// 电脑与PLC直接连接不通过网络,写入数据
        /// </summary>
        /// <param name="ac">内存区域枚举对象</param>
        /// <param name="address">内存地址</param>
        /// <param name="number">连接地址2个byte位 对应4个16进制 例如:对应D130  那么位置就是0082</param>
        /// <param name="dt">数据类型</param>
        /// <param name="value">写入的数据内容</param>
        /// <returns></returns>
        public static string SendWriteString(AreaCode ac, int address, int number,DataType dt, params int[] value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@");
            sb.Append(plcAddr);//站号
            sb.Append("FA");
            sb.Append("0");//响应等待时间 设置值为0-F，例如：设置为2 则响应等待时间为 20ms
            sb.Append("00");//ICF 消息控制区域，直接连PC机，设置为 00
            sb.Append("00");//DA2 固定为00
            sb.Append("00");//SA2 固定为00
            sb.Append("00");//SID 固定为00
            sb.Append("0102");//写
            sb.Append(CodeSwitch(ac));//读取区域 例如DM,WR,CIO区等
            sb.Append(AddZero_Left(Convert.ToString(address, 16), 4));//读取地址
            if (dt == DataType.Bit)
            {
                sb.Append("0D");
            }
            else if (dt == DataType.Word)
            {
                sb.Append("00");
            }
            sb.Append(AddZero_Left(Convert.ToString(number, 16), 4));//读取位数

            for (int i = 0; i < value.Length; i++)
            {
                sb.Append(AddZero_Left(Convert.ToString(value[i], 16), 4));//写入数据
            }

            return FCS(sb.ToString());
        }
        /// <summary>
        /// 向右补零
        /// </summary>
        /// <param name="value">需要补零的字符串</param>
        /// <param name="number">补零后字符串的长度</param>
        /// <returns></returns>
        private static string AddZero_Right(string value, int number)
        {
            int tempcount = number - value.Length;
            if (tempcount>0)
            {
                for (int i = 0; i < tempcount; i++)
                {
                    value = value + "0";
                }
            }
            return value;
        }
        /// <summary>
        /// 向左补零
        /// </summary>
        /// <param name="value">需要补零的字符串</param>
        /// <param name="number">补零后字符串的长度</param>
        /// <returns></returns>
        private static string AddZero_Left(string value, int number)
        {
            int tempcount = number - value.Length;
            if (tempcount > 0)
            {
                for (int i = 0; i < tempcount; i++)
                {
                    value = "0" + value;
                }
            }
            return value;
        }

        /// <summary>
        /// 将数据进行FCS校验，并返回一个完整的命令字符串
        /// </summary>
        /// <param name="data">增加FCS校验的数据</param>
        /// <returns>带有FCS校验验并可发送到 OMRON PLC的数据</returns>0
        private static string FCS(string data)
        {
            string hostlink = data + ComputeFCS(data) + "*" + (char)13;
            return hostlink;
        }

        /// 上位机校验
        /// </summary>
        /// <param name="linkstring">进行校验的数据，以@开始的字符串</param>
        /// <returns></returns>
        private static string ComputeFCS(string linkstring)
        {
            char inFcs = (char)linkstring[0];
            int fcsResult = (int)inFcs;
            for (int i = 1; i < linkstring.Length; i++)
            {
                inFcs = (char)linkstring[i];
                fcsResult ^= (int)inFcs;
            }
            return fcsResult.ToString("X");
        }
        /// <summary>
        /// 对带有FCS校验的数据进行校验
        /// </summary>
        /// <param name="receives">带有FCS的数据</param>
        /// <returns>校验正确返回 true 否则返回 false</returns>
        public static bool CheckFCS(string receives)
        {
            int i = receives.IndexOf('*');
            string data = receives.Substring(0, i - 2);
            if (receives.Substring(i - 2, 2).Equals(ComputeFCS(data)))
                return true;
            else return false;
        }

        private static string CodeSwitch(AreaCode e)
        {
            switch (e.ToString())
            {
                case "CIO_Bit":
                    return "30";
                case "CIO_Word":
                    return "B0";
                case "WR_Bit":
                    return "31";
                case "WR_Word":
                    return "B1";
                case "HR_Bit":
                    return "32";
                case "HR_Word":
                    return "B2";
                case "AR_Bit":
                    return "33";
                case "AR_Word":
                    return "B3";
                case "TIM_Flag":
                    return "09";
                case "CNT_Flag":
                    return "09";
                case "TIM_PV":
                    return "89";
                case "CNT_PV":
                    return "89";
                case "DM_Bit":
                    return "02";
                case "DM_Word":
                    return "82";
                default :
                    return null;
            }
        }
    }

   



    
}

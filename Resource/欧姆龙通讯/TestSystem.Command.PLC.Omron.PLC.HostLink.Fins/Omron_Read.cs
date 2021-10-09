using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    internal class Omron_Read
    {
        
        public static string[] GetResponse(string str)
        {
          return  OmronPLC.GetResponse(str, CommandType.Read);
        }

        /// <summary>
        /// 读取命令
        /// </summary>
        /// <param name="ac">内存区域代码</param>
        /// <param name="address">操作地址</param>
        /// <param name="number">操作数据通道</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <returns></returns>
        public static string ReadCommand(AreaCode ac, int address, int number,DataType dt, SerialPort sp)
        {
            string response = "";
            response = OmronPLC.SendReadString(ac, address, number, dt);
            if (sp != null)
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(response);
            if (number == 1)
            {
                System.Threading.Thread.Sleep(100);//欧姆龙100毫秒数据延迟
            }
            else
            {
                System.Threading.Thread.Sleep(100+(number-1)*10);
            }
            response = sp.ReadExisting();

            if (response.IndexOf("*") > 0)
            {
                return response;
            }
            else
            {
                return ReadCommand(ac, address, number, dt, sp);
 
            }
          
        }
        /// <summary>
        /// 读取命令
        /// </summary>
        /// <param name="ac">内存区域代码</param>
        /// <param name="address">操作地址</param>
        /// <param name="number">操作数据通道</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <returns></returns>
        public static string ReadCommand(AreaCode ac, int address, int number, DataType dt, SerialPort sp, ref string cmdString)
        {
            string response = "";
            cmdString = OmronPLC.SendReadString(ac, address, number, dt);
            if (sp != null)
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(cmdString);
            System.Threading.Thread.Sleep(200);//欧姆龙100毫秒数据延迟
            response = sp.ReadExisting();
            if (response.IndexOf("*") > 0)
            {
                return response;
            }
            else
            {
               return ReadCommand(ac, address, number, dt, sp, ref  cmdString);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac">内存区域代码</param>
        /// <param name="address">操作地址</param>
        /// <param name="sleep">获得响应的延迟时间</param>
        /// <param name="number">操作数据通道</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <returns></returns>
        public static string ReadCommand(AreaCode ac, int address,int sleep, int number, DataType dt, SerialPort sp, ref string cmdString)
        {
            string response = "";
            cmdString = OmronPLC.SendReadString(ac, address, number, dt);
            if (sp != null)
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            sp.Write(cmdString);
            System.Threading.Thread.Sleep(sleep);//欧姆龙100毫秒数据延迟
            response = sp.ReadExisting();
            if (response.IndexOf("*") > 0)
            {
                return response;
            }
            else
            {
               return ReadCommand(ac, address, sleep, number, dt, sp, ref  cmdString);
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac">内存区域代码</param>
        /// <param name="address">操作地址</param>
        /// <param name="sleep">获得响应的延迟时间</param>
        /// <param name="number">操作数据通道</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <returns></returns>
        public static string ReadStatus(AreaCode ac, string address, int sleep, int number, DataType dt, SerialPort sp, ref string cmdString)
        {
            string status = "";

            string response = "";
            if (address.IndexOf('.') > 0)
            {
                int point = int.Parse(address.Split('.')[0]);
                cmdString = OmronPLC.SendReadString(ac, point, number, dt);
                if (sp != null)
                {
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                    }
                }
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(cmdString);
                System.Threading.Thread.Sleep(sleep);//欧姆龙100毫秒数据延迟
                response = sp.ReadExisting();

                if (response.IndexOf("*") > 0)
                {
                    string[] getResponseValue = GetResponse(response);
                    Omron_Binary binary = new Omron_Binary();
                    if (getResponseValue.Length > 0)
                    {
                        foreach (var str in getResponseValue)
                        {
                            status = binary.BinaryConvert(address.Split('.')[1], int.Parse(str));
                        }
                    }
                }
                else
                {
                    //如果返回的响应不完整,再次发送读取状态的指令
                  status= ReadStatus(ac, address, sleep, number, dt, sp, ref cmdString);
                }
            }
           
            return status;
        }

        /// <summary>
        /// 批量读取状态信息
        /// </summary>
        /// <param name="CommandStr"></param>
        /// <param name="dType"></param>
        /// <param name="sp"></param>
        /// <param name="Channel"></param>
        /// <returns></returns>
        public static object[] CommandReadStatus(string CommandStr, DataType dType, SerialPort sp, int Channel)
        {
            object[] objStatus = new object[1];
            string ResponseStr = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                
                if (CommandStr != "")
                {
                   ResponseStr= CommandRead(CommandStr, dType, sp, Channel);
                   string Area = CommandStr.Substring(0,1);
                   string Code = CommandStr.Substring(1, CommandStr.Length-1);//获得起始位
                   if (Code.IndexOf(".") > 0)
                   {

                   }
                   else
                   {
                       Omron_Binary binary = new Omron_Binary();

                       int StartIndex = int.Parse(Code);
                       var valueArr = ResponseStr.Split('|');
                       for (var i = 0; i < valueArr.Length; i++)
                       {
                           string _binary = binary.Int32ToBinary(int.Parse(valueArr[i]));
                           var arr = binary.BinaryConvertToArr(_binary);
                           for (int j = 0; j < arr.Length; j++)
                           {
                               dic.Add(string.Format("{0}.{1}", Area + StartIndex, j<10?string.Format("0{0}",j):string.Format("{0}",j)), arr[j]);
                           }
                           StartIndex++;
                       }
                   }
                   
                   objStatus[0] = dic;
                }
            }
            catch
            {

            }

            return objStatus;
        }
        /// <summary>
        /// 通过传入字符串命令通讯 
        /// </summary>
        /// <param name="CommandStr">传入点位 例如I0 D100 H0 </param>
        /// <param name="dType">数据类型 只有Bit  和Word 数据类型</param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string CommandRead(string CommandStr,DataType dType,SerialPort sp,int Channel)
        {
            string Response = string.Empty;
            try
            {
                if (CommandStr != "")
                {
                    string areCode = CommandStr.Substring(0, 1);
                    AreaCode code = OmronControl.GetAreaCode(areCode);
                    string address=CommandStr.Substring(1,CommandStr.Length-1);
                    int point = 0;
                    string CmdString="";
                    if (address.IndexOf('.') > 0)
                    {
                        //存在状态读取
                        Response=ReadStatus(code, address, 100, 1, dType, sp, ref CmdString);
                    }
                    else
                    {
                        
                        point = int.Parse(address);
                        Response = ReadCommand(code, point, Channel, dType, sp);
                        
                        string [] ResponseArr=GetResponse(Response);
                        Response = "";
                        foreach (var str in ResponseArr)
                        {
                            Response += string.Format("{0}|", str);
                        }
                       //假如是读取一个数据通道的话 就是返回单个数据通道的  如果是多个连续的数据通道就会返回多个通道的值 用|隔开。
                       
                       Response= Response.Remove(Response.Length - 1, 1);
                    }
                }
            }
            catch
            {
 
            }
            return Response;
        }

        

    }
}

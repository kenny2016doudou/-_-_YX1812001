using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    internal class Omron_Write
    {
        public static string[] GetResponse(string str)
        {
            return OmronPLC.GetResponse(str,CommandType.Write,4);
        }

        /// <summary>
        /// 写入命令
        /// </summary>
        /// <param name="ac">内存区域代码</param>
        /// <param name="address">操作地址</param>
        /// <param name="number">操作数据通道</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="data">写入的数据</param>
        /// <returns></returns>
        public static string WriteCommand(AreaCode ac, int address, int number, DataType dt, SerialPort sp, params int[] data)
        {
            string response = "";
            response = OmronPLC.SendWriteString(ac, address, number, dt, data);
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
            if (data.Length == 1)
            {
                System.Threading.Thread.Sleep(100);//欧姆龙100毫秒数据延迟
            }
            else
            {
                System.Threading.Thread.Sleep(100 + (data.Length - 1) * 10);
            }
            response = sp.ReadExisting();

            if (response.IndexOf("*") > 0)
            {
                return response;
            }
            else
            {
                return WriteCommand(ac, address, number, dt, sp, data);
            }
        }
        /// <summary>
        /// 写入欧姆龙操作命令
        /// </summary>
        /// <param name="ac">内存区域</param>
        /// <param name="address">内存区域的地址</param>
        /// <param name="number">数据位</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <param name="data">写入的数据</param>
        /// <returns></returns>
        public static string WriteCommand(AreaCode ac, int address, int number, DataType dt, SerialPort sp, ref string cmdString, params int[] data)
        {
            string response = "";
            cmdString = OmronPLC.SendWriteString(ac, address, number, dt, data);
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
                return WriteCommand(ac, address, number, dt, sp, ref  cmdString, data);
            }
        }
        /// <summary>
        /// 写入欧姆龙操作命令
        /// </summary>
        /// <param name="ac">内存区域</param>
        /// <param name="address">内存区域的地址</param>
        /// <param name="number">数据位</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <param name="sleep">获得响应的延迟时间</param>
        /// <param name="data">写入的数据</param>
        /// <returns></returns>
        public static string WriteCommand(AreaCode ac, int address, int number, DataType dt, SerialPort sp, ref string cmdString,int sleep, params int[] data)
        {
            string response = "";
            cmdString = OmronPLC.SendWriteString(ac, address, number, dt, data);
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
                return WriteCommand(ac, address, number, dt, sp, ref  cmdString, sleep, data);
            }
        }
        /// <summary>
        /// 写入欧姆龙操作命令主要是操作类似W0.01  W0.02 这样的操作地址
        /// </summary>
        /// <param name="ac">内存区域</param>
        /// <param name="address">内存区域的地址</param>
        /// <param name="number">数据位</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <param name="OnOrOff">是否是命令模式</param>
        /// <returns></returns>
        public static string WriteCommand(AreaCode ac, string address, int number, DataType dt, SerialPort sp, ref string cmdString,bool OnOrOff)
        {
            string response = "";
            try
            {

                int AreaValue = 0;
                TestSystem.Command.PLC.Omron.HostLink.Fins.Omron_Binary binary = new Omron_Binary();
                if (address.IndexOf('.') > 0)
                {
                    string[] Responses = Omron_Read.GetResponse(Omron_Read.ReadCommand(ac, int.Parse(address.Split('.')[0]), number, dt, sp));
                    if (Responses.Length > 0)
                    {
                      AreaValue=int.Parse(Responses[0]);
                    }
                    int AreaAddress = binary.BinaryConvertToInt32(binary.Binary(address, OnOrOff, AreaValue));
                    cmdString = OmronPLC.SendWriteString(ac, int.Parse(address.Split('.')[0]), number, dt, new int[] { AreaAddress });
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
                    System.Threading.Thread.Sleep(100);//欧姆龙100毫秒数据延迟
                    response = sp.ReadExisting();
                    if (response.IndexOf("*") > 0)
                    {
                        return response;
                    }
                    else
                    {
                        response= WriteCommand(ac, address, number, dt, sp, ref  cmdString, OnOrOff);
                    }

                }

               
            }
            catch
            {
                
            }
            return response;
           
        }
        /// <summary>
        /// 写入欧姆龙操作命令主要是操作类似W0.01  W0.02 这样的操作地址
        /// </summary>
        /// <param name="ac">内存区域</param>
        /// <param name="address">内存区域的地址</param>
        /// <param name="number">数据位</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="cmdString">命令字符串</param>
        /// <param name="sleep">获得响应的延迟时间</param>
        /// <param name="OnOrOff">是否是命令模式</param>
        /// <returns></returns>
        public static string WriteCommand(AreaCode ac, string address, int number, DataType dt, SerialPort sp, ref string cmdString,int sleep, bool OnOrOff)
        {
            string response = "";
            try
            {

                int AreaValue = 0;
                TestSystem.Command.PLC.Omron.HostLink.Fins.Omron_Binary binary = new Omron_Binary();
                if (address.IndexOf('.') > 0)
                {
                    string[] Responses = Omron_Read.GetResponse(Omron_Read.ReadCommand(ac, int.Parse(address.Split('.')[0]), number, dt, sp));
                    if (Responses.Length > 0)
                    {
                        AreaValue = int.Parse(Responses[0]);
                    }
                    int AreaAddress = binary.BinaryConvertToInt32(binary.Binary(address, OnOrOff, AreaValue));
                    cmdString = OmronPLC.SendWriteString(ac, int.Parse(address.Split('.')[0]), number, dt, new int[] { AreaAddress });
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
                        response = WriteCommand(ac, address, number, dt, sp, ref  cmdString, sleep, OnOrOff);
                    }
                }
            }
            catch
            {

            }
            return response;
        }

        public static string WriteCommand(string CommandStr, DataType dType, SerialPort sp,params int[] data)
        {
            string Response = string.Empty;
            try
            {
                if (CommandStr != "")
                {
                    string areCode = CommandStr.Substring(0, 1);
                    AreaCode code = OmronControl.GetAreaCode(areCode);
                    string address = CommandStr.Substring(1, CommandStr.Length - 1);
                    int point = 0;
                    //string CmdString = "";
                    if (address.IndexOf('.') > 0)
                    {
                        //存在状态操作
                        //Response = ReadStatus(code, address, 100, 1, dType, sp, ref CmdString);
                        //状态值写入
                    }
                    else
                    {

                        point = int.Parse(address);
                        //Response = ReadCommand(code, point, 1, dType, sp);
                        Response = WriteCommand(code, point, data.Length, dType, sp, data);
                        string[] ResponseArr = GetResponse(Response);
                        Response = "";
                        foreach (var str in ResponseArr)
                        {
                            Response += string.Format("{0}|", str);
                        }
                        //假如是读取一个数据通道的话 就是返回单个数据通道的  如果是多个连续的数据通道就会返回多个通道的值 用|隔开。

                        Response = Response.Remove(Response.Length - 1, 1);
                    }
                }
            }
            catch
            {

            }

            return Response;
        }

        public static string WriteCommand(string CommandStr, DataType dType, SerialPort sp, bool OnOrOff)
        {
            string Response = string.Empty;
            try
            {
                if (CommandStr != "")
                {
                    string areCode = CommandStr.Substring(0, 1);
                    AreaCode code = OmronControl.GetAreaCode(areCode);
                    string address = CommandStr.Substring(1, CommandStr.Length - 1);
                    string CmdString = "";
                   
                    if (address.IndexOf('.') > 0)
                    {
                        //存在状态操作
                        //Response = ReadStatus(code, address, 100, 1, dType, sp, ref CmdString);
                        //状态值写入
                       Response= WriteCommand(code, address, 1, dType, sp, ref CmdString, OnOrOff);
                       string[] ResponseArr = GetResponse(Response);
                       Response = "";
                       foreach (var str in ResponseArr)
                       {
                           Response += string.Format("{0}|", str);
                       }
                       //假如是读取一个数据通道的话 就是返回单个数据通道的  如果是多个连续的数据通道就会返回多个通道的值 用|隔开。

                       Response = Response.Remove(Response.Length - 1, 1);
                    }
                    //else
                    //{

                    //    //point = int.Parse(address);
                    //    ////Response = ReadCommand(code, point, 1, dType, sp);
                    //    //Response = WriteCommand(code, point, data.Length, dType, sp, data);
                    //    //string[] ResponseArr = GetResponse(Response);
                    //    //Response = "";
                    //    //foreach (var str in ResponseArr)
                    //    //{
                    //    //    Response += string.Format("{0}|", str);
                    //    //}
                    //    ////假如是读取一个数据通道的话 就是返回单个数据通道的  如果是多个连续的数据通道就会返回多个通道的值 用|隔开。

                    //    //Response = Response.Remove(Response.Length - 1, 1);
                    //}
                }
            }
            catch
            {

            }

            return Response;
        }
        /// <summary>
        /// 写入指令0,1
        /// </summary>
        /// <param name="CommandStr">地址例如:W1.01</param>
        /// <param name="dType">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="OnOrOff">置0 或者置1</param>
        /// <param name="PointValue">写入的地址的值</param>
        /// <returns></returns>
        public static string WriteCommand(string CommandStr, DataType dType, SerialPort sp, bool OnOrOff, int PointValue)
        {
            string Response = string.Empty;
            try
            {
                if (CommandStr != "")
                {
                    string areCode = CommandStr.Substring(0, 1);
                    AreaCode code = OmronControl.GetAreaCode(areCode);
                    string address = CommandStr.Substring(1, CommandStr.Length - 1);
                    string CmdString = "";
                    Omron_Binary binary = new Omron_Binary();
                    string[] binaryArr = binary.BinaryConvertToArr(binary.Int32ToBinary(PointValue));
                    if (address.IndexOf('.') > 0)
                    {
                        //查找需要进行修改的值.
                        string point = address.Substring(address.IndexOf('.'), address.Length - address.IndexOf('.'));
                        int index = int.Parse(point.Remove(0, 1));
                        if (index >= 0 && index <= 15)
                        {
                            binaryArr[index] = OnOrOff ? "1" : "0";
                        }

                        //转换后的组成二进制数据转换成需要写入到PLC中的值指令

                        string binaryvalue = "";
                        for (int i = 15; i >= 0; i--)
                        {
                            binaryvalue += binaryArr[i];
                        }
                        int value = binary.BinaryConvertToInt32(binaryvalue);
                        address = address.Substring(0, address.IndexOf('.'));
                        CmdString = OmronPLC.SendWriteString(code, int.Parse(address), new int[] { value });
                        sp.DiscardOutBuffer();
                        sp.DiscardInBuffer();
                        sp.Write(CmdString);
                        Response = sp.ReadExisting();
                        if (Response.IndexOf("*") > 0)
                        {

                        }
                        else
                        {
                            Response = WriteCommand(CommandStr, dType, sp, OnOrOff, PointValue);
                        }
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

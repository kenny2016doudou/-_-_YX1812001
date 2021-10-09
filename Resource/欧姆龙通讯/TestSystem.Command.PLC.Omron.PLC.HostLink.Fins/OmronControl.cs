using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    internal class OmronControl
    {
        private static OmronControl uniqueInstance;
        
        private OmronControl() { }



        public static OmronControl getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new OmronControl();
            }


            return uniqueInstance;
        }


        public bool ExcuteWrite(string point, string lastPoint, int sleep, SerialPort sp)
        {

            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                Thread.Sleep(10);
                //sp.Write(TaiDaPLC.FaSong_TaiDa_PLC(point, false, lastPoint));
                Thread.Sleep(sleep);
                //sp.DiscardOutBuffer();
                string a = sp.ReadExisting();
                //sp.DiscardOutBuffer();
                //sp.DiscardInBuffer();
                //Thread.Sleep(20);
                if (a.IndexOf("*") > 0)
                {
                    return this.ExcuteWrite(point, lastPoint, sleep, sp);
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 写入命令 On/OFF 
        /// </summary>
        /// <param name="point">写入的点位(W0.00 ,W0.01)</param>
        /// <param name="ac">所属存储空间(CIO,WR,DM)</param>
        /// <param name="sleep">响应延迟时间</param>
        /// <param name="dt">数据类型 (Bit,Word)</param>
        /// <param name="sp">串口对象</param>
        /// <param name="Undo">ON/OFF</param>
        /// <returns></returns>
        public bool ExcuteWrite(string point, AreaCode ac, int sleep, DataType dt,SerialPort sp,bool Undo)
        {
            bool result = false;
            try
            {
                string cmdStr = "";
                string response = Omron.HostLink.Fins.Omron_Write.WriteCommand(ac, point, 1, dt, sp, ref cmdStr, sleep, Undo);
                result= OmronPLC.CommandExecuteResult(response, CommandType.Write);
            }
            catch
            {
 
            }

            return result;
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="point">写入的点位 (H0,H2,H4)</param>
        /// <param name="ac">所属存储空间(CIO,WR,DM)</param>
        /// <param name="sleep">响应延迟时间</param>
        /// <param name="dt">数据类型 (Bit,Word)</param>
        /// <param name="sp">串口对象</param>
        /// <param name="data">写入的数据</param>
        /// <returns></returns>
        public bool ExcuteWriteData(int point, AreaCode ac, int sleep, DataType dt, SerialPort sp, params int[] data)
        {
            bool result = false;
            try
            {
                string cmdStr = "";
                string response = Omron.HostLink.Fins.Omron_Write.WriteCommand(ac, point, 1, dt, sp, ref cmdStr, sleep, data);
                result = OmronPLC.CommandExecuteResult(response, CommandType.Write);
            }
            catch
            {

            }

            return result;
        }
        /// <summary>
        /// 解析写入命令响应代码解析
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public string[] GetWriteResponse(string Response)
        {
            return Omron_Write.GetResponse(Response);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="point">读取的点位 (D1,D2)</param>
        /// <param name="ac">所属存储空间(CIO,WR,DM)</param>
        /// <param name="sleep">响应延迟时间</param>
        /// <param name="dt">数据类型</param>
        /// <param name="sp">串口对象</param>
        /// <returns></returns>
        public string[] ExcuteReaderData(int point, AreaCode ac, int sleep, DataType dt, SerialPort sp)
        {
            string[] ResponseData = null;
            try
            {
                string cmdStr = "";
                string response = Omron.HostLink.Fins.Omron_Read.ReadCommand(ac, point, sleep, 1, dt, sp, ref cmdStr);
                ResponseData = OmronPLC.GetResponse(response, CommandType.Read);
            }
            catch
            {
 
            }
            return ResponseData;

        }



        /// <summary>
        /// 批量读取多状态信息
        /// </summary>
        /// <param name="cmdStr">点位(D100,I100)</param>
        /// <param name="Channel">数据通道</param>
        /// <param name="dt">数据类型 Bit 或者Word</param>
        /// <param name="sp">串口</param>
        /// <returns></returns>
        public object[] ExcuteReaderData(string cmdStr,int Channel, DataType dt, SerialPort sp)
        {
            object[] ResponseData = null;
            try
            {

                ResponseData = Omron.HostLink.Fins.Omron_Read.CommandReadStatus(cmdStr, dt, sp, Channel);
            }
            catch
            {

            }
            return ResponseData;

        }


        /// <summary>
        /// 将响应内容解析
        /// </summary>
        /// <param name="ResponseStr">响应命令</param>
        /// <returns></returns>
        public string[] GetReaderResponse(string ResponseStr)
        {
            return Omron_Read.GetResponse(ResponseStr);
        }

        /// <summary>
        /// 读取状态信息
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ac"></param>
        /// <param name="sleep"></param>
        /// <param name="dt"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string ExcuteReaderStatus(string point, AreaCode ac, int sleep, DataType dt, SerialPort sp)
        {
            string ResponseData = null;
            try
            {
                string cmdStr = "";
                string response = Omron.HostLink.Fins.Omron_Read.ReadStatus(ac, point, sleep, 1, dt, sp, ref cmdStr);
                ResponseData = response;
            }
            catch
            {

            }
            return ResponseData;
        }
        /// <summary>
        /// 将状态转换成一组二进制数据返回
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ac"></param>
        /// <param name="sleep"></param>
        /// <param name="dt"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string[] ExcuteReaderStatusArray(string point, AreaCode ac, int sleep, DataType dt, SerialPort sp)
        {
            string[] ResponseData=null ;
            string CmdString = "";
            try
            {
                Omron_Binary binary = new Omron_Binary();
                //string cmdStr = "";
                if (point.IndexOf('.') > 0)
                {
                    int address = int.Parse(point.Split('.')[0]);
                    string[] ResponseArr = ExcuteReaderData(address, ac, sleep, dt, sp);
                    
                    foreach (var str in ResponseArr)
                    {
                        if (str.Length > 0)
                        {
                            CmdString = binary.Int32ToBinary(int.Parse(str));
                        }
                    }
                  
                }
                ResponseData = binary.BinaryConvertToArr(CmdString);
            }
            catch
            {

            }
            return ResponseData;
        }

        public object[] ExcuteReaderStatusArray(string cmdstr, int Channel, DataType dt, SerialPort sp)
        {
            object[] ResponseData = null;
            //string CmdString = "";
            try
            {
                Omron_Binary binary = new Omron_Binary();
                //string cmdStr = "";
                ResponseData= ExcuteReaderData(cmdstr, Channel, dt, sp);
                //if (point.IndexOf('.') > 0)
                //{
                //    int address = int.Parse(point.Split('.')[0]);
                //    string[] ResponseArr = (address, ac, sleep, dt, sp);

                //    foreach (var str in ResponseArr)
                //    {
                //        if (str.Length > 0)
                //        {
                //            CmdString = binary.Int32ToBinary(int.Parse(str));
                //        }
                //    }

                //}
                //ResponseData = binary.BinaryConvertToArr(CmdString);
            }
            catch
            {

            }
            return ResponseData;
        }



        /// <summary>
        /// 返回内存区域代码
        /// </summary>
        /// <param name="areaCode">内存区域代码简写</param>
        /// <returns></returns>
       public static AreaCode GetAreaCode(string areaCode)
        {
            AreaCode acode;
            switch (areaCode)
            {
                case "I":
                    acode = AreaCode.CIO_Word;
                    break;
                case "D":
                    acode = AreaCode.DM_Word;
                    break;
                case "W":
                    acode = AreaCode.WR_Word;
                    break;
                case "H":
                    acode = AreaCode.HR_Word;
                    break;
                case "Q":
                    acode = AreaCode.CIO_Word;
                    break;
                case "C":
                    acode=AreaCode.CNT_PV;
                    break;
                default:
                    acode = AreaCode.CIO_Word;
                    break;
            }
            return acode;
        }

        /// <summary>
        /// 通过传入参数单点或者多点读取
        /// </summary>
        /// <param name="Address">通讯地址 如:D0</param>
        /// <param name="dType">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="channel">通道个数</param>
        /// <returns></returns>
       public string ReadCommandString(string Address, DataType dType, SerialPort sp, int channel)
       {
           return Omron_Read.CommandRead(Address, dType, sp, channel);
       }

        /// <summary>
        /// 通过传入参数往单点或者多点写入
        /// </summary>
        /// <param name="CommandStr">通讯地址 如:D0</param>
        /// <param name="dType">数据类型</param>
        /// <param name="sp">串口</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
       public string Excute(string CommandStr, DataType dType, System.IO.Ports.SerialPort sp, params int[] data)
       {
           return Omron.HostLink.Fins.Omron_Write.WriteCommand(CommandStr, dType, sp, data);
       }

        /// <summary>
        ///通过传入参数写入0/1
        /// </summary>
        /// <param name="CmdString">通讯地址</param>
        /// <param name="sp">串口</param>
        /// <param name="dtype">数据类型</param>
        /// <param name="Undo">给0或者给1</param>
        /// <returns></returns>
       public string ExcuteUndo(string CmdString, System.IO.Ports.SerialPort sp, DataType dtype, bool Undo)
       {
           return Omron.HostLink.Fins.Omron_Write.WriteCommand(CmdString, dtype, sp, Undo);

       }

        //========================提供测试功能 均会返回请求命令 和响应命令
        public string ReadCommand(int point, AreaCode ac, int sleep, DataType dt, SerialPort sp,ref string cmdStr )
        {

            string Response = Omron.HostLink.Fins.Omron_Read.ReadCommand(ac, point, sleep, 1, dt, sp, ref cmdStr);
            return Response;
        }


        public string WriterCommand(int point, AreaCode ac, int sleep, DataType dt, SerialPort sp, ref string cmdStr,params int[] valueData)
        {
            return Omron.HostLink.Fins.Omron_Write.WriteCommand(ac, point, 1, dt, sp, ref cmdStr, valueData);
        }


        public string WriterCommand(string point, AreaCode ac, int sleep, DataType dt, SerialPort sp, ref string cmdStr, bool Undo)
        {
            return Omron.HostLink.Fins.Omron_Write.WriteCommand(ac, point, 1, dt, sp, ref cmdStr, Undo);
        }


       


        








    }

}

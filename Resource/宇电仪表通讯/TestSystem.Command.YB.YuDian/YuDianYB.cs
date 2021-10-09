using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.YuDian
{
    /// <summary>
    /// 宇电仪表
    /// </summary>
    public sealed class YuDianYB
    {
        /// <summary>
        /// 读取命令构建
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="parm_addr"></param>
        public static string SendStr(string addr, string parm_addr)
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
                    string crc = ReadCRC(_addr, _parm_addr);
                    command = HexTo16(_addr + addr_AI) + HexTo16(_addr + addr_AI) + _readArea + parm_addr.ToUpper() + "0000" + crc;
                }

            }
            catch
            {

            }
            return command;
 
        }

        /// <summary>
        /// 将返回的结果集合转换为16进制数 
        /// </summary>
        /// <param name="response">返回长度为10的16进制集合</param>
        /// <returns></returns>
        public static string[] ResponseConvertToHex16(byte[] response)
        {
            string[] Hex16Bytes = new string[10] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            for (int i = 0; i < response.Length; i++)
            {
                string v = Convert.ToString(response[i], 16);
                Hex16Bytes[i] = v.Length == 1 ? "0" + v.ToUpper() : v.ToUpper();

            }
            return Hex16Bytes;
        }

        /// <summary>
        /// 发送读取指令
        /// </summary>
        /// <param name="SendStr">发送字符串</param>
        /// <returns></returns>
        public static byte[] ReadCommand(string SendStr)
        {
           return Command(SendStr);
        }

        /// <summary>
        /// 发送命令构建
        /// </summary>
        /// <param name="addr">仪表地址</param>
        /// <param name="parm_addr">参数代号16进制地址</param>
        /// <param name="parm_value">参数值(只支持0到FFFF值写入)</param>
        /// <returns></returns>
        public static string WriteStr(string addr, string parm_addr, string parm_value)
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
        /// 发送写入指令
        /// </summary>
        /// <param name="WriteStr"></param>
        /// <returns></returns>
        public static byte[] WriteCommand(string WriteStr)
        {
            return Command(WriteStr);
        }

        /// <summary>
        /// 上位机对于返回的数据进行数据校验
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static bool CheckFCS(object[] Response,int addr) 
        {
            bool result = false;
            try
            {
                int valueFcs = 0x00;//定义校验数
                int fcs = 0x00;//校验码
                if (Response.Length == 10)
                {
                    //数据校验的方式是前8位 2个字节2个字节进行高低位互换求和+Addr 与9,10 字节校验码进行比较
                    for (int i = 0; i < Response.Length; i++)
                    {
                        if (i < 8)
                        {
                            if (i % 2 == 0)
                            {
                                valueFcs += Convert.ToInt32(Response[i + 1].ToString() + Response[i].ToString(), 16);
                            }
                            else
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (i % 2 == 0)
                            {
                                fcs = Convert.ToInt32(Response[i + 1].ToString() + Response[i].ToString(), 16);
                            }
                        }

 
                    }
                }

                //返回结果校验
                if ((valueFcs + addr) == fcs)
                {
                    result = true;
                }
            }
            catch
            {
 
            }

            return result;
        }

        /// <summary>
        /// 读取PV值 (16进制)
        /// </summary>
        /// <param name="response"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static string ReadPV(object[] response,int addr)
        {
            string result = "";
            if (CheckFCS(response, addr))
            {
                result = response[1].ToString() + response[0].ToString();
            }
            return result;
        }


        /// <summary>
        /// 读取SV值 (16进制)
        /// </summary>
        /// <param name="response"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static string ReadSV(object[] response, int addr)
        {
            string result = "";
            if (CheckFCS(response, addr))
            {
                result = response[7].ToString() + response[6].ToString();
            }
            return result;
        }
        /// <summary>
        /// 将10进制数转换为16进制 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string HexTo16(int value)
        {
            return Convert.ToString(value, 16).ToUpper();
        }

        /// <summary>
        /// 格式化PV值返回
        /// </summary>
        /// <param name="responseArr"></param>
        /// <returns></returns>
        public static string FormatResonsePV(string[] responseArr)
        {

            string value = "";



            return value;
        }
       
        /// <summary>
        ///  写入指令 16位求和校验计算
        /// </summary>
        /// <param name="addrmint"></param>
        /// <param name="parm_addr"></param>
        /// <param name="parm_value"></param>
        /// <returns></returns>
        static string WriteCRC(int addr, int parm_addr, int parm_value)
        {
            string CRC = "";
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
         static string ReadCRC(int addr, int parm_addr)
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
        static string CRCRead(int value)
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
        /// 构建发送命令 发送命令由8位 字节组成
        /// </summary>
        /// <param name="CmdStr"></param>
        /// <returns></returns>
        static byte[] Command(string CmdStr)
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

        

         
    }
}

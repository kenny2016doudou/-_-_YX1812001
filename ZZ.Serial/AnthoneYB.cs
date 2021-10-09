﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace ZZ.Serial
{
    public class AnthoneYB
    {
        /// <summary>
        /// 安东仪表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string GetComStr(int code, System.IO.Ports.SerialPort sp)
        {
            byte[] args = new byte[1];
            args[0] = Convert.ToByte(code);

            sp.Parity = Parity.Space;
            sp.Write(args, 0, args.Length);
            byte[] buffer = new byte[0x400];
            if (sp.IsOpen && (sp.BytesToRead != 0))
            {
                sp.Read(buffer, 0, 20);
            }

            //SP_YB.Close();
            string redstr = "0";
            byte[] buff = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                buff[i] = buffer[i];
            }
            string o = Convert.ToChar(Convert.ToInt32(buff[6])).ToString();
            string k = Convert.ToChar(Convert.ToInt32(buff[7])).ToString();
            if (o + k == "OK")
            {
                float a = float.Parse(((buff[1] * Math.Pow(2, 8) + buff[0])).ToString());
                float b = (float)10;
                redstr = (a / b).ToString();
            }
            return redstr;
        }
        /// <summary>
        /// 表通讯
        /// </summary>
        /// <param name="commandText">发送的命令，位数必须为偶数</param>
        public string CommandTextStr(string commandText,System.IO.Ports.SerialPort sp)
        {
            ModBus mb = new ModBus();
            if (commandText.Length % 2 != 0)
            {
                MessageBox.Show("通讯命令格式错误！");
                return "00";
            }
            else
            {
                string m_CommandText = "";
                string temp = "";
                byte[] buf1 = new byte[7];
                byte[] buf = new byte[5];
                //字符串转为数组
                for (int i = 0; i < 5; i++)
                {
                    buf[i] = byte.Parse(commandText.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    buf1[i] = buf[i];
                }
                temp = mb.CalculateCrc16(buf).ToString("X");//CRC校验，并输出大写十六进制
                for (int i = temp.Length; i < 4; i++)
                {
                    temp = "0" + temp;
                }
                //数据补0，确保为00格式，而不是0格式。
                for (int i = 0; i < commandText.Length; i++)
                {
                    if (commandText[i] < 16)
                    {
                        m_CommandText += "0" + commandText[i].ToString();
                    }
                    else
                    {
                        m_CommandText += commandText[i].ToString();
                    }
                }
                buf1[5] = byte.Parse(temp.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                buf1[6] = byte.Parse(temp.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                m_CommandText += temp;
                sp.Write(buf1, 0, 7);

                byte[] buffer = new byte[8];
                System.Threading.Thread.Sleep(100);
                sp.Read(buffer, 0, 8);
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                byte[] intBuffer = new byte[8];
                //高低字节转换
                for (int i = 0; i < 8; i++)
                {
                    intBuffer[i] = buffer[7-i];
                }
                string redstr = "";
                if (intBuffer != null)
                {
                    if (intBuffer.Length >= 4)
                    {
                        float a=float.Parse(((intBuffer[1] * Math.Pow(2,8) + intBuffer[0])+1).ToString());
                        float b=(float)10;
                        redstr = (a/b).ToString();
                    }
                }

                return redstr;
            }
        }
        private byte[] CRC16(byte[] data)
        {
            byte CRC16Hi;
            byte CRC16Lo;
            CRC16Hi = 0xFF;
            CRC16Lo = 0xFF;
            long iIndex;
            for (int i = 0; i < data.Length; i++)
            {
                iIndex = CRC16Hi ^ data[i];
                CRC16Hi = (byte)(CRC16Lo ^ _crcHi[iIndex]); //低位处理
                CRC16Lo = _crcLo[iIndex]; //高位处理
            }

            byte[] ReturnData = new byte[2];
            ReturnData[0] = CRC16Hi; //CRC高位
            ReturnData[1] = CRC16Lo; //CRC低位

            return ReturnData;
        }


        private readonly byte[] _crcLo = new byte[]//CRC低位字节值表
        { 
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26, 
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };

        private readonly byte[] _crcHi = new byte[] //CRC高位字节值表
        { 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };

        /// <summary>
        /// 写仪表参数值
        /// </summary>
        /// <param name="paramType"></param>
        /// <param name="paramValue"></param>
        public void WriteParameter(int paramType, int paramValue, System.IO.Ports.SerialPort sp)
        {
           
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            byte[] b = new byte[4];
            b[0] = Convert.ToByte(87);//写命令
            b[1] = Convert.ToByte(paramType);//写参数类型
            b[2] = Convert.ToByte(paramValue); //参数值
            b[3] = Convert.ToByte(0);
            Thread.Sleep(50);
            sp.Parity = Parity.Space;
            sp.Write(b, 0, b.Length);
            byte[] buffer = new byte[0x400];
            Thread.Sleep(50);
            if (sp.IsOpen && (sp.BytesToRead != 0))
            {
                sp.Read(buffer, 0, 20);
            }
            
            
        }

        /// <summary>
        /// 读仪表参数值
        /// </summary>
        /// <param name="paramType"></param>
        /// <param name="paramValue"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string ReadParameter(int paramType, System.IO.Ports.SerialPort sp)
        {
            
            
              
                byte[] b = new byte[2];
                b[0] = Convert.ToByte(82);//读命令
                b[1] = Convert.ToByte(paramType);//参数类型

                sp.Parity = Parity.Space;
                Thread.Sleep(20);
                sp.DiscardOutBuffer();
                Thread.Sleep(200);
                sp.Write(b, 0, b.Length);
                Thread.Sleep(500);
                byte[] buffer = new byte[0x400];
                if (sp.IsOpen && (sp.BytesToRead != 0))
                {
                    sp.Read(buffer, 0, 20);
                }
                Thread.Sleep(20);
                sp.DiscardInBuffer();
                string redstr = "0";
                byte[] buff = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    buff[i] = buffer[i];
                }
                string o = Convert.ToChar(Convert.ToInt32(buff[2])).ToString();
                string k = Convert.ToChar(Convert.ToInt32(buff[3])).ToString();
                if (o + k == "OK")
                {
                    float a = float.Parse(((buff[1] * Math.Pow(2, 8) + buff[0])).ToString());
                    float b2 = (float)10;
                    redstr = (a / b2).ToString();
                }
                //if (float.Parse(redstr) != 0)
                //{
                    return redstr;
                //}
            
        }

    }
}

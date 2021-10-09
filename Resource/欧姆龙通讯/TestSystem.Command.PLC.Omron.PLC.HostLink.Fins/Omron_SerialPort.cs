using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    /// <summary>
    /// 查找欧姆龙可用通信串口
    /// </summary>
    public class Omron_SerialPort
    {
        public static string GetOmronCOM(int Rate, Parity parity, int DataBits, StopBits stopbit)
        {

            string ComName = "";

            try
            {
                foreach(var Serial in SerialPort.GetPortNames())
                {
                    SerialPort sp = new SerialPort();
                    sp.PortName = Serial;
                    sp.Parity = parity;
                    sp.BaudRate = Rate;
                    sp.DataBits = DataBits;
                    sp.StopBits = stopbit;
                    if (!sp.IsOpen)
                    {
                        try
                        {
                            sp.Open();
                            sp.DiscardInBuffer();
                            sp.DiscardOutBuffer();
                            System.Threading.Thread.Sleep(10);
                            //尝试发送通讯命令查看返回信息。
                            sp.Write("@00FA0000000000101B200000D000172*" + (char)13);//写入H0
                            System.Threading.Thread.Sleep(100);
                            string response = sp.ReadExisting();
                            if (response == "@00FA004000000001010000000043*" + (char)13)
                            {
                                ComName = Serial;
                                sp.Close();
                                break;
                            }
                            sp.Close();
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }
            }
            catch
            {
                throw new Exception("获取COM通讯口失败。");
 
            }
            return ComName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TianChen
{
    public class TianChenYB_SerialPort
    {
        public static string GetTianChenYB_COM(int Rate, Parity parity, int DataBits, StopBits stopbit, string address)
        {

            string ComName = "";


            foreach (var Serial in SerialPort.GetPortNames())
            {
                try
                {
                    SerialPort sp = new SerialPort();
                    sp.PortName = Serial;
                    sp.Parity = parity;
                    sp.BaudRate = Rate;
                    sp.DataBits = DataBits;
                    sp.StopBits = stopbit;
                    if (!sp.IsOpen)
                    {
                        sp.Open();
                        sp.DiscardInBuffer();
                        sp.DiscardOutBuffer();
                        System.Threading.Thread.Sleep(10);
                        //尝试发送通讯命令查看返回信息。
                        string code = "#" + address;//读地址为1的天辰仪表值
                        TianChengYB yb = new TianChengYB();
                        sp.Write(yb.SendStr(code));

                        System.Threading.Thread.Sleep(100);
                        string response = sp.ReadExisting();
                        if (response.Length > 1)
                        {
                            ComName = Serial;
                            sp.Close();
                            break;
                        }
                        sp.Close();
                    }

                }
                catch
                {
                    // throw new Exception("获取COM通讯口失败。");

                }
            }

            return ComName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TongHui
{
    /// <summary>
    /// 查找同惠仪表通讯COM口
    /// </summary>
    public class TongHuiYB_SerialPort
    {
        //未开启的通讯返回指令为R9=+999999 M0
        
        public static string GetTongHuiCOM(int Rate, Parity parity, int DataBits, StopBits stopbit)
        {

            string ComName = "";
            string ResponseCommand = "R9=+999999 MO";
            try
            {
                foreach (var Serial in SerialPort.GetPortNames())
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
                            sp.Write("?\n");//写入H0
                            //System.Threading.Thread.Sleep(500);
                            for (int i = 0; i < 5; i++)
                            {
                                System.Threading.Thread.Sleep(200);
                                string response = sp.ReadExisting();
                                
                                if (response.Contains(ResponseCommand) || response.Trim().Length == 13)
                                {
                                    ComName = Serial;
                                    sp.Close();
                                    break;
                                }
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

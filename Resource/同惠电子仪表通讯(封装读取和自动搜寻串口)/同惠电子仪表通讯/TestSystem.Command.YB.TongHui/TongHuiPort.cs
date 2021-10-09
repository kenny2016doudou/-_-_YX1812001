using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TongHui
{
    public class TongHuiPort
    {
        private static TongHuiPort uniqueInstance;

        private TongHuiPort() { }

        public static TongHuiPort getInstance()
        {
            if (uniqueInstance == null) uniqueInstance = new TongHuiPort();

            return uniqueInstance;
        }

        public void OpenPort(SerialPort sp, string PortName, Parity parity, int BaudRate, int DataBits, StopBits stopBits)
        {
            try
            {
                if (sp != null)
                {
                    sp.PortName = PortName;
                    sp.Parity = parity;
                    sp.BaudRate = BaudRate;
                    sp.DataBits = DataBits;
                    sp.StopBits = stopBits;
                    sp.Open();
                }
            }
            catch
            {
 
            }
        }
    }
}

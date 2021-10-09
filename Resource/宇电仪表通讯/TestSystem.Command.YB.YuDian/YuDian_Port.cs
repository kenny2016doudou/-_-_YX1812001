using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.YuDian
{
    public class YuDian_Port
    {
        private static YuDian_Port uniqueInstance;

        private YuDian_Port() { }



        public static YuDian_Port getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new YuDian_Port();
            }


            return uniqueInstance;
        }

        public void OpenPort(SerialPort sp, string portName, StopBits stopBits, Parity parity, int baudRate, int dataBits)
        {
            try
            {
                //通讯控件设置
                sp.PortName = portName;
                sp.StopBits = stopBits;
                sp.Parity = parity;
                sp.BaudRate = baudRate;
                sp.DataBits = dataBits;
                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

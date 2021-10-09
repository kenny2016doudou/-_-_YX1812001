using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class Omron_Port
    {
        private static Omron_Port uniqueInstance;

        private Omron_Port() { }



        public static Omron_Port getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new Omron_Port();
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

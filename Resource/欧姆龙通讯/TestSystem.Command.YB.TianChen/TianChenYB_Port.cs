using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TianChen
{
    public class TianChenYB_Port
    {
        private static TianChenYB_Port uniqueInstance;

        private TianChenYB_Port() { }



        public static TianChenYB_Port getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new TianChenYB_Port();
            }


            return uniqueInstance;
        }

        public bool OpenPort(string portName, SerialPort sp, int baudRate, Parity parity, int dataBits, StopBits stopBits)
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
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}

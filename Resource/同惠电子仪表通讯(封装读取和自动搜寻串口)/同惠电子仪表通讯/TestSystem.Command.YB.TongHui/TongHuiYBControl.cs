using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TongHui
{
    internal class TongHuiYBControl
    {
        private static TongHuiYBControl uniqueInstance;
        private TongHuiYBControl() { }

        public static TongHuiYBControl getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new TongHuiYBControl();
            }
            return uniqueInstance;

        }

        public string[] ExcuteRead( int sleep ,TongHuiCommand Command,System.IO.Ports.SerialPort sp)
        {
            string[] str = new string[3];
            try
            {
               string response=TongHuiYB.ExecuteCommandRead(sleep, Command, sp);
               str=TongHuiYB.GetResponse(response);

            }
            catch
            {
 
            }
            return str;
 
        }
    }
}

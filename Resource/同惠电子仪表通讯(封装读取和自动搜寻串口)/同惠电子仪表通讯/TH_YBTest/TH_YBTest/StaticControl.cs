using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;
using System.IO.Ports;
using TestSystem.Command.YB.TongHui;
using TestSystem.Command.ControlCenter;

namespace TH_YBTest
{
    public class StaticControl
    {
        public IComControl ICom = null;

        private SerialPort sp;

        public TongHuiCommand[] Command = new TongHuiCommand[20];//命令集合

        public StaticControl(ref SerialPort sp)
        {
            this.sp = sp;
            if (!this.sp.IsOpen)
            {
                string COM = TongHuiYB_SerialPort.GetTongHuiCOM(9600, Parity.None, 8, StopBits.One);
                TongHuiPort.getInstance().OpenPort(sp, COM, Parity.None, 9600, 8, StopBits.One);
            }

            Command[0] = TongHuiCommand.V;//读取
        }


        public void LoadData()
        {
            ICom = ComControl_Factory.getInstance().CreateComControl(ref sp);

            TongHuiRead ReadTonghui = new TongHuiRead(Command[0]);

            ICom.SetReader("读取仪表", ReadTonghui);


            ICom.Start();
        }
    }
}

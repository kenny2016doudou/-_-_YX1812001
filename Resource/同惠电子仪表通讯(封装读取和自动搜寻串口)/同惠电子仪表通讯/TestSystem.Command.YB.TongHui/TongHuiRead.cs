using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TongHui
{
    public class TongHuiRead:TestSystem.Command.Interface.IRead
    {
        private TongHuiCommand command;
        private int sleep = 300;
        private object[] datas;
       

        public TongHuiRead()
        {
            command = TongHuiCommand.V;
            
        }

        public TongHuiRead(int sleep)
        {
            this.sleep = sleep;
            command = TongHuiCommand.V;
        }

        public TongHuiRead(TongHuiCommand command)
        {
            this.command = command;
        }

        
        #region IRead 成员

        public object Data
        {
            get
            {
                if (datas == null)
                {
                    return 0;
                }
                else
                {
                    return datas[0];
                }

            }
        }

        public object[] DataMuster
        {
            get { return datas; }
        }

        public void Read(ref System.IO.Ports.SerialPort sp)
        {
            datas = TongHuiYBControl.getInstance().ExcuteRead(sleep, command, sp);
        }

        #endregion
    }
}

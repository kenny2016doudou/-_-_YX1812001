using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using TestSystem.Command.Interface;


namespace TestSystem.Command.YB.TianChen
{
    public class Reader_YBTianChen:IRead
    {

        private string point;
        private string address;
        private int sleep = 50;
        private object[] datas;
        private object data;

        public Reader_YBTianChen(string point)
        {
            this.point = point;
            
        }

        public Reader_YBTianChen( string address,string point)
        {
            this.point = point;
            this.address = address;
        }

        public Reader_YBTianChen(string address, string point, int sleep)
        {
            this.point = point;
            this.address = address;
            this.sleep = sleep;
        }

        #region IRead 成员

        public void Read(ref SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            data = YBTianChenControl.getInstance().ExcuteRead(this.address,this.point, sleep, sp);
            
        }


        object IRead.Data
        {
            get { return data; }
        }

        public object[] DataMuster
        {
            get { return datas; }
        }

        #endregion


    }
}

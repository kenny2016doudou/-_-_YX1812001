using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class Omron_ReaderStatus : IRead
    {
        public string StrPoint { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }
        private object[] _Data { get; set; }
        private object _SignData { get; set; }

        public Omron_ReaderStatus()
        {

        }

        public Omron_ReaderStatus(OmronCommandData ocd, int sleep)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.StrPoint = ocd.StrAddress;
            this.Sleep = sleep;
        }

        public Omron_ReaderStatus(OmronCommandData ocd)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.StrPoint = ocd.StrAddress;
            this.Sleep = 100;
        }


        #region IRead 成员

        public object Data
        {
            get {
                if (_SignData == null)
                {
                    return 0;

                }
                else
                {
                    return _SignData;
                }
            }
        }

        public object[] DataMuster
        {
            get { return _Data; }
        }

        public void Read(ref System.IO.Ports.SerialPort sp)
        {
            //_Data = new string[4];
            _SignData = OmronControl.getInstance().ExcuteReaderStatus(StrPoint, Area, Sleep, DType, sp);
        }
        #endregion
    }
}

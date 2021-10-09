using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;
using System.IO.Ports;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class Omron_Reader:IRead
    {
        public int Point { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }
        public int Number { get; set; }
        private string[] _Data { get; set; }
        private string _SignData { get; set; }
        public string cmdstr { get; set; }
        #region IRead 成员

        public object Data
        {
            get
            {
                if (_Data == null)
                {
                    return 0;
                }
                else
                {
                    return _Data[0];
                }
                
            }
        }

        public object[] DataMuster
        {
            get { return _Data; }
        }

        public void Read(ref System.IO.Ports.SerialPort sp)
        {
            //_Data = OmronControl.getInstance().ExcuteReaderData(Point,Area,Sleep,Number,DType, sp);
            _Data = Omron_Read.CommandRead(cmdstr, DType, sp, Number).Split('|');
        }

        #endregion

        public Omron_Reader()
        {
            this.Number = 1;
        }
  

        public Omron_Reader(OmronCommandData ocd)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.IntAddress;
            this.Sleep = 100;
            this.Number = 1;
        }


        public Omron_Reader(OmronCommandData ocd, int number)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.IntAddress;
            this.Sleep = 100 + number*10;
            this.Number = number;
        }

        public Omron_Reader(OmronCommandData ocd, int number,int sleep)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.IntAddress;
            this.Sleep = sleep;
            this.Number = number;
        }

        public Omron_Reader(string Address, DataType dType, int channel)
        {
            this.DType = dType;
            this.cmdstr = Address;
            this.Number = channel;
        }
      
        public string ReadCommandString(ref System.IO.Ports.SerialPort sp, ref string CmdStr)
        {
            return OmronControl.getInstance().ReadCommand(Point, Area, Sleep, DType, sp, ref CmdStr);
        }

        /// <summary>
        /// 直接传入通讯地址以及数据类型查询数据
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="dType"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string ReadCommandString(string Address, DataType dType, SerialPort sp,int channel)
        {
            return Omron_Read.CommandRead(Address, dType, sp, channel);
        }
        public string[] GetReadResponse(string Response)
        {
            return OmronControl.getInstance().GetReaderResponse(Response);
        }

       
    }
}

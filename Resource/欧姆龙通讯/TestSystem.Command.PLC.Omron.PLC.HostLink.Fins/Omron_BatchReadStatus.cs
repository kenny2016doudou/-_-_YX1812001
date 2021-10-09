using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    /// <summary>
    /// 批量读取状态
    /// </summary>
    public class Omron_BatchReadStatus:IRead
    {
        public string StrPoint { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }
        public Dictionary<string, string> KeyValue;
        private object[] _Data { get; set; }
        private string _SignData { get; set; }
        public string cmdstr { get; set; }
        public int Channel { get; set; }
        //private object[] _tempdata=null;
        #region IRead 成员

        public object Data
        {
            get { return _Data[0]; }
        }

        public object[] DataMuster
        {
            get { return _Data; }
        }

        public void Read(ref System.IO.Ports.SerialPort sp)
        {
            //_Data = OmronControl.getInstance().ExcuteReaderStatusArray(StrPoint, Area, Sleep, DType, sp);
            try
            {
                _Data = OmronControl.getInstance().ExcuteReaderStatusArray(cmdstr, Channel, DType, sp);
                if (_Data[0] != null)
                {
                    Dictionary<string, string> dics = (Dictionary<string, string>)_Data[0];
                    if (dics.Count > 0)
                    {
                        Dictionary<string, string> TempDic = new Dictionary<string, string>();
                        foreach (var dic in KeyValue)
                        {
                            TempDic.Add(dic.Key, dics[dic.Value]);
                        }
                        _Data[0] = TempDic;
                    }
                }
            }
            catch
            {
                _Data[0] = null;
            }
        }


        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="dType"></param>
        /// <param name="channel"></param>
        public Omron_BatchReadStatus(string Address, DataType dType, int channel,Dictionary<string,string> KeyValue)
        {
            this.DType = dType;
            this.cmdstr = Address;
            this.Channel = channel;
            this.KeyValue = KeyValue;
        }
        public Omron_BatchReadStatus()
        {

        }
    }
}

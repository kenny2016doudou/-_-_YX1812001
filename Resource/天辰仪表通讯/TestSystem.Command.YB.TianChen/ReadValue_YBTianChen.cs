using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;
using System.IO.Ports;

namespace TestSystem.Command.YB.TianChen
{
    public class ReadValue_YBTianChen : IRead
    {
        public ReadValue_YBTianChen()
        {
            Sleep = 100;

        }
        /// <summary>
        /// 仪表地址 例:#01
        /// </summary>
        public string AddressStr { get; set; }

        /// <summary>
        /// 睡眠时间
        /// </summary>
        public int Sleep { get; set; }

        public object[] datas;
        private object data;

        #region IRead 成员

        public void Read(ref SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            data = YBTianChenControl.getInstance().ExcuteReadValue(AddressStr, Sleep, sp);

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

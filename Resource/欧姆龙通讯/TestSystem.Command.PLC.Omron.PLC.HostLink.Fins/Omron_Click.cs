using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    /// <summary>
    /// 摁下,复位
    /// </summary>
    public class Omron_Click : ICommand
    {
        public string Point { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }


        public Omron_Click()
        {

        }

        public Omron_Click(OmronCommandData ocd, int sleep)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.StrAddress;
            this.Sleep = sleep;
        }

        public Omron_Click(OmronCommandData ocd)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.StrAddress;
            this.Sleep = 100;
        }



        #region ICommand 成员

        public bool Excute(ref System.IO.Ports.SerialPort sp)
        {
            OmronControl.getInstance().ExcuteWrite(Point, Area, Sleep, DType, sp, true);
            return OmronControl.getInstance().ExcuteWrite(Point, Area, Sleep, DType, sp, false);
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class Omron_ClickDown:ICommand
    {
        public string Point { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }
        public bool Undo { get; set; }

        public Omron_ClickDown()
        {
            
        }

        public Omron_ClickDown(OmronCommandData ocd,int sleep)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.StrAddress;
            this.Sleep = sleep;
        }

        public Omron_ClickDown(OmronCommandData ocd)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            this.Point = ocd.StrAddress;
            this.Sleep = 100;
        }
        public Omron_ClickDown(string address, DataType dtype, bool undo)
        {
            this.Point = address;
            this.DType = dtype;
            this.Undo = undo;
        }

        public Omron_ClickDown(string address)
        {
            this.Point = address;
            this.DType = DataType.Bit;
            this.Undo = true;
        }

        public string ExcuteUndo(string CmdString, System.IO.Ports.SerialPort sp, DataType dtype, bool Undo)
        {
            return Omron.HostLink.Fins.Omron_Write.WriteCommand(CmdString, dtype, sp, Undo);

        }

        #region ICommand 成员



        bool ICommand.Excute(ref System.IO.Ports.SerialPort sp)
        {
           //return OmronControl.getInstance().ExcuteWrite(Point, Area, Sleep, DType, sp, true);

           string response = Omron.HostLink.Fins.Omron_Write.WriteCommand(Point, DType, sp, Undo);
           return OmronPLC.CommandExecuteResult(response, CommandType.Write);
        }

        #endregion
    }
}

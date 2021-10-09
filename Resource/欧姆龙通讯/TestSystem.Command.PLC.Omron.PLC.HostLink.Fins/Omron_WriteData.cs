using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class Omron_WriteData:ICommand_WriteData
    {
        public string StrPoint { get; set; }
        public int IntPoint { get; set; }
        public AreaCode Area { get; set; }
        public DataType DType { get; set; }
        public int Sleep { get; set; }
        public int[] Data { get; set; }
        public int Channel { get; set; }

        public Omron_WriteData()
        {

        }

        public Omron_WriteData(OmronCommandData ocd, int sleep)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            if (!string.IsNullOrEmpty(ocd.StrAddress))
            {
                this.StrPoint = ocd.StrAddress;
            }
            else
            {
                this.IntPoint = ocd.IntAddress;
            }
            this.Sleep = sleep;
        }

        public Omron_WriteData(OmronCommandData ocd)
        {
            this.Area = ocd.AC;
            this.DType = ocd.DType;
            if (!string.IsNullOrEmpty(ocd.StrAddress))
            {
                this.StrPoint = ocd.StrAddress;
            }
            else
            {
                this.IntPoint = ocd.IntAddress;
            }
            this.Sleep = 100;
        }

        public Omron_WriteData(string address, DataType dType)
        {
            this.StrPoint = address;
            this.DType = dType;
        }
        public string Excute(string CommandStr, DataType dType, System.IO.Ports.SerialPort sp, params int[] data)
        {
            return Omron.HostLink.Fins.Omron_Write.WriteCommand(CommandStr, dType, sp, data);
        }
        #region ICommand 成员

        public bool Excute(ref System.IO.Ports.SerialPort sp)
        {
            //return Omron.HostLink.Fins.OmronControl.getInstance().ExcuteWriteData(IntPoint, Area, Sleep, DType, sp, Data);
            string response= Omron.HostLink.Fins.Omron_Write.WriteCommand(StrPoint, DType, sp, Data);
            return OmronPLC.CommandExecuteResult(response, CommandType.Write);
        }

        #endregion
        public string ExcuteUndo(ref System.IO.Ports.SerialPort sp,ref string CmdStr,bool Undo)
        {
            return Omron.HostLink.Fins.OmronControl.getInstance().WriterCommand(StrPoint, Area, Sleep, DType, sp, ref CmdStr, Undo);
        }
        public string Excute(ref System.IO.Ports.SerialPort sp, ref string CmdStr)
        {
            return Omron.HostLink.Fins.OmronControl.getInstance().WriterCommand(IntPoint, Area, Sleep, DType, sp, ref CmdStr, Data);
        }

        public string[] GetWriteResponse(string Response)
        {
            return OmronControl.getInstance().GetWriteResponse(Response);
        }

        #region ICommand_WriteData 成员

        public string StrData
        {
            set {
                if (value.Split(',').Count() == 1)
                {
                    if (value.EndsWith(","))
                    {
                        value = value.Substring(value.Length - 1, 1);
                    }
                    Data = new int[1];
                    Data[0] = int.Parse(value);
                }
                else
                {
                    if (value.EndsWith(","))
                    {
                        value = value.Substring(value.Length - 1, 1);
                    }
                    string[] strArr=value.Split(',');
                    int count = strArr.Count();

                    Data = new int[count];
                    for (var i = 0; i < count;i++ )
                    {
                        Data[i] = int.Parse(strArr[i]);
                    }

 
                }
            }
        }

        #endregion
    }
}

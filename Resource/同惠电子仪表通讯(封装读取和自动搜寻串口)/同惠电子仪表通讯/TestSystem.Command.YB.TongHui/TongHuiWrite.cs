using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TongHui
{
    public class TongHuiWrite : TestSystem.Command.Interface.ICommand_WriteData
    {
        #region ICommand_WriteData 成员

        public string StrData
        {
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region ICommand 成员

        public bool Excute(ref System.IO.Ports.SerialPort sp)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

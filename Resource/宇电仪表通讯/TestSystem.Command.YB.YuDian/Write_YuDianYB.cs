using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.YB.YuDian
{
    public class Write_YuDianYB : ICommand_WriteData
    {
        private string addr;

        private string parm_addr;

        private int sleep = 150;

        private object[] datas;

        private string strData;

        public Write_YuDianYB() { }
        public Write_YuDianYB(string addr, string parm_addr, int sleep)
        {
            this.addr = addr;
            this.parm_addr = parm_addr;
            this.sleep = sleep;
        }

        public Write_YuDianYB(string addr, string parm_addr, int sleep, string strData)
        {
            this.addr = addr;
            this.parm_addr = parm_addr;
            this.sleep = sleep;
            this.strData = strData;
        }

        /// <summary>
        /// 写入地址#参数代号
        /// </summary>
        /// <param name="addrs"></param>
        /// <param name="strData"></param>
        public Write_YuDianYB(string addrs, string strData)
        {
            string[] str = addrs.Split('#');
            this.addr = str[0];
            this.parm_addr = str[1];
            this.strData = strData;
        }

        /// <summary>
        /// 写入地址#参数代号
        /// </summary>
        /// <param name="addrs"></param>
        /// <param name="strData"></param>
        public Write_YuDianYB(string addrs)
        {
            string[] str = addrs.Split('#');
            this.addr = str[0];
            this.parm_addr = str[1];
        }



        #region ICommand_WriteData 成员

        public string StrData
        {
            set { strData = value; }
        }

        #endregion

        #region ICommand 成员

        public bool Excute(ref System.IO.Ports.SerialPort sp)
        {
            try
            {
                return YBYudianControl.getInstance().ExcuteWrite(addr, parm_addr, strData, sleep, sp, out datas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

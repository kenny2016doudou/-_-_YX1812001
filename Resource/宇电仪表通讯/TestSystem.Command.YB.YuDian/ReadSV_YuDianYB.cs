using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.Interface;

namespace TestSystem.Command.YB.YuDian
{
    public class ReadSV_YuDianYB:IRead
    {
        private string addr;

        private string parm_addr;

        private int sleep = 150;

        private object[] datas;

       public ReadSV_YuDianYB() { }

        public ReadSV_YuDianYB(string addr, string parm_addr)
        {
            this.addr = addr;
            this.parm_addr = parm_addr;
        }

        public ReadSV_YuDianYB(string addr, string parm_addr, int sleep)
        {
            this.addr = addr;
            this.parm_addr = parm_addr;
            this.sleep = sleep;
        }

        /// <summary>
        /// 读取地址#参数代号
        /// </summary>
        /// <param name="addrs"></param>
        public ReadSV_YuDianYB(string addrs)
        {
            string[] str = addrs.Split('#');
            this.addr = str[0];
            this.parm_addr = str[1];
        }
        /// <summary>
        /// 读取地址#参数代号
        /// </summary>
        /// <param name="addrs"></param>
        public ReadSV_YuDianYB(string addrs,int sleep)
        {
            string[] str = addrs.Split('#');
            this.addr = str[0];
            this.parm_addr = str[1];
            this.sleep = sleep;
        }
        #region IRead 成员

        public object Data
        {
            get
            {
              //返回PV值
              return  YBYudianControl.getInstance().ReadSV(datas, addr);
            }
        }

        public object[] DataMuster
        {
            get { return datas; }
        }

        public void Read(ref System.IO.Ports.SerialPort sp)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            YBYudianControl.getInstance().ExcuteRead(addr, parm_addr, sleep, sp, out datas);
        }

        #endregion

       
    }
}

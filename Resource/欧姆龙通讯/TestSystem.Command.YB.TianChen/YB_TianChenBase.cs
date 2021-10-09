using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TianChen
{
    internal abstract class YB_TianChenBase
    {
        protected TianChengYB tcyb;

        protected string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        protected string point;

        public string Point
        {
            get { return point; }
            set { point = value; }
        }
        protected int sleep;

        public int Sleep
        {
            get { return sleep; }
            set { sleep = value; }
        }
        protected bool isValdate;

        public bool IsValdate
        {
            get { return isValdate; }
            set { isValdate = value; }
        }

        protected string strdata;

        public string Strdata
        {
            get { return strdata; }
            set { strdata = value; }
        }



        public SerialPort sp;
        /// <summary>
        /// 读取设置值
        /// </summary>
        public string ExcuteRead()
        {
            try
            {
                if (isValdate)
                {
                    sp.Write(tcyb.SendStr("$" + address + point));
                    System.Threading.Thread.Sleep(sleep);
                    return tcyb.GetSetValue(sp.ReadExisting()).ToString();
                }
                else
                {
                    sp.Write("$" + address + point + Convert.ToChar(13));
                    System.Threading.Thread.Sleep(sleep);
                    string a = tcyb.GetSetValue(sp.ReadExisting()).ToString();
                    return a;
                }
            }
            catch
            {
                return "error!";
            }
        }

        /// <summary>
        /// 读取仪表测试值
        /// </summary>
        public virtual string ExcuteReadValue()
        {
            try
            {
                if (isValdate)
                {
                    sp.Write(tcyb.SendStr("#" + address + point));
                    System.Threading.Thread.Sleep(sleep);
                    //string tmp = sp.ReadExisting();
                    return tcyb.GetSend(sp.ReadExisting()).ToString();
                }
                else
                {
                    sp.Write("#" + address + point + Convert.ToChar(13));
                    System.Threading.Thread.Sleep(sleep);
                    string a = tcyb.GetSend(sp.ReadExisting()).ToString();
                    return a;
                }
            }
            catch
            {
                return "error!";
            }
        }
        ///// <summary>
        ///// 写入数据
        ///// </summary>
        public abstract bool ExcuteWrite();
        /// <summary>
        /// 写仪表参数值
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="command"></param>
        /// <param name="sp"></param>
        //public void ExcuteWrite(string addr, string command, System.IO.Ports.SerialPort sp)
        //public void ExcuteWrite()
        //{

        //    sp.Write(tcyb.SendStr("%" + Address + "10+1111"));
        //    System.Threading.Thread.Sleep(100);

        //    sp.Write(tcyb.SendStr("%" + Address + point + strdata));
        //    System.Threading.Thread.Sleep(100);


        //    sp.Write(tcyb.SendStr("%" + Address + "10+0000"));
        //    System.Threading.Thread.Sleep(100);
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.YB.TianChen
{
    class YBTianChenControl
    {
        private static YBTianChenControl uniqueInstance;
        private TianChengYB tcyb;
        private YBTianChenControl()
        {
            tcyb = new TianChengYB();
        }



        public static YBTianChenControl getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new YBTianChenControl();
            }


            return uniqueInstance;
        }

        /// <summary>
        /// 天辰仪表写操作
        /// </summary>
        /// <returns></returns>
        public bool ExcuteWrite(string point, string address, string strData, SerialPort sp)
        {
            try
            {
                sp.Write(tcyb.SendStr("%" + address + "10+1111"));
                System.Threading.Thread.Sleep(100);

                sp.Write(tcyb.SendStr("%" + address + point + "+" + strData));
                System.Threading.Thread.Sleep(100);


                sp.Write(tcyb.SendStr("%" + address + "10+0000"));
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 天辰仪表读
        /// </summary>
        /// <param name="address">仪表站位地址</param>
        /// <param name="point">读取的数据地址</param>
        /// <param name="sleep"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string ExcuteRead(string address,string point, int sleep, SerialPort sp)
        {
            try
            {
                sp.Write(tcyb.SendStr("$" + address+point));
                System.Threading.Thread.Sleep(sleep);
                return tcyb.GetSend(sp.ReadExisting()).ToString();
            }
            catch
            {
                return "error!";
            }
        }

        /// <summary>
        /// 读仪表PV数据
        /// </summary>
        /// <param name="addressStr"></param>
        /// <param name="sleep"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public string ExcuteReadValue(string addressStr, int sleep, SerialPort sp)
        {
            try
            {
                sp.Write(tcyb.SendStr(addressStr));
                System.Threading.Thread.Sleep(sleep);
                //string tmp = sp.ReadExisting();
                return tcyb.GetSend(sp.ReadExisting()).ToString();
            }
            catch
            {
                return "error!";
            }
        }



    }
}

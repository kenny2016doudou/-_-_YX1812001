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
       
        private YBTianChenControl()
        {
           
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
        public bool ExcuteWrite(string point, string address, string strData,MeterType mt, SerialPort sp)
        {
            try
            {

                YB_TianChenBase yb_tc=YBFactory.getInstance().CreateTianChenYB(mt);
                yb_tc.Address=address;
                yb_tc.Point=point;
                yb_tc.Strdata=strData;
                yb_tc.sp=sp;
                yb_tc.Sleep =100;
                yb_tc.IsValdate = true;
                return yb_tc.ExcuteWrite();
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
        public string ExcuteRead(string address,string point, int sleep,MeterType mt, SerialPort sp)
        {
            try
            {

                YB_TianChenBase yb_tc  = YBFactory.getInstance().CreateTianChenYB(mt);
                yb_tc.Address = address;
                yb_tc.Point = point;
                yb_tc.Sleep = sleep;
                yb_tc.sp = sp;
                return yb_tc.ExcuteRead();
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
        public string ExcuteReadValue(string address, string point, int sleep, MeterType mt, SerialPort sp)
        {
            try
            {

                YB_TianChenBase yb_tc = YBFactory.getInstance().CreateTianChenYB(mt);
                yb_tc.Address = address;
                yb_tc.Point = point;
                yb_tc.sp = sp;
                yb_tc.Sleep = sleep;
                return yb_tc.ExcuteReadValue();
            }
            catch
            {
                return "error!";
            }
        }



    }
}

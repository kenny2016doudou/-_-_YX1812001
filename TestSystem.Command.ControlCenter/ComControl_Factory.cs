using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.ControlCenter
{
    public class ComControl_Factory
    {
        private static ComControl_Factory uniqueInstance;

        private ComControl_Factory() { }



        public static ComControl_Factory getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new ComControl_Factory();
            }


            return uniqueInstance;
        }

        /// <summary>
        /// 创建Com控制对象
        /// </summary>
        /// <param name="sp">传入串口对象</param>
        /// <returns></returns>
        public IComControl CreateComControl(ref System.IO.Ports.SerialPort sp)
        {
            return new ComControl(ref sp);
        }
    }
}

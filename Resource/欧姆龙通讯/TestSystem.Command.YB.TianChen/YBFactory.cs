using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TianChen
{
    internal class YBFactory
    {
        private static YBFactory uniqueInstance;

        private YBFactory()
        {

        }

        public static YBFactory getInstance()
        {

            if (uniqueInstance == null)
            {
                uniqueInstance = new YBFactory();
            }


            return uniqueInstance;
        }

        /// <summary>
        /// 制造不同类型的仪表
        /// </summary>
        /// <param name="mt"></param>
        /// <returns></returns>
        public YB_TianChenBase CreateTianChenYB(MeterType mt)
        {
            switch (mt.ToString())
            {
                case "XST":
                    return new YB_XST();

                case "XSN":
                    return new YB_XSN();

                case "JS":
                    return new YB_JS();

                default:
                    return null;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TianChen
{
    internal class YB_XSN : YB_TianChenBase
    {
#pragma warning disable CS0169 // 从不使用字段“YB_XSN.uniqueInstance”
        private static YB_XSN uniqueInstance;
#pragma warning restore CS0169 // 从不使用字段“YB_XSN.uniqueInstance”

        public YB_XSN()
        {
            tcyb = new TianChengYB();
        }

        //public static YB_XSN getInstance()
        //{

        //    if (uniqueInstance == null)
        //    {
        //        uniqueInstance = new YB_XSN();
        //    }


        //    return uniqueInstance;
        //}

        public override bool ExcuteWrite()
        {
            try
            {
                sp.Write("%%" + Address + "10+01111" + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);

                sp.Write("%%" + Address + Point + "+" + Strdata + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);


                sp.Write("%%" + Address + "10+00000" + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

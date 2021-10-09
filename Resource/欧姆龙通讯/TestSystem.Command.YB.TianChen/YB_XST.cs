using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TianChen
{
    internal class YB_XST : YB_TianChenBase
    {
        //private static YB_XST uniqueInstance;


        public YB_XST()
        {
            tcyb = new TianChengYB();
        }

        //public static YB_XST getInstance()
        //{

        //    if (uniqueInstance == null)
        //    {
        //        uniqueInstance = new YB_XST();
        //    }


        //    return uniqueInstance;
        //}


        public override bool ExcuteWrite()
        {
            if (string.Empty != Strdata)
            {
                try
                {
                    if (IsValdate)
                    {
                        sp.Write(tcyb.SendStr("%" + Address + "10+1111"));
                        System.Threading.Thread.Sleep(100);

                        sp.Write(tcyb.SendStr("%" + Address + Point + "+" + Strdata));
                        System.Threading.Thread.Sleep(100);


                        sp.Write(tcyb.SendStr("%" + Address + "10+0000"));
                        System.Threading.Thread.Sleep(100);
                        return true;
                    }
                    else
                    {
                        sp.Write("%" + Address + "10+1111"+Convert.ToChar(13));
                        System.Threading.Thread.Sleep(100);

                        sp.Write("%" + Address + Point + "+" + Strdata+Convert.ToChar(13));
                        System.Threading.Thread.Sleep(100);


                        sp.Write("%" + Address + "10+0000"+Convert.ToChar(13));
                        System.Threading.Thread.Sleep(100);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}

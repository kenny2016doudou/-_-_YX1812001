using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TianChen
{
    internal class YB_JS : YB_TianChenBase
    {

        //private static YB_JS uniqueInstance;

        public YB_JS()
        {
            tcyb = new TianChengYB();
        }

        //public static YB_JS getInstance()
        //{

        //    if (uniqueInstance == null)
        //    {
        //        uniqueInstance = new YB_JS();
        //    }


        //    return uniqueInstance;
        //}

        public override bool ExcuteWrite()
        {
            try
            {
                sp.Write("%" + Address + "10+01111" + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);

                sp.Write("%" + Address + Point + "+" + Strdata + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);


                sp.Write("%" + Address + "10+00000" + Convert.ToChar(13));
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public override string ExcuteReadValue()
        {
            try
            {
                if (isValdate)
                {
                    sp.Write(tcyb.SendStr("#" + address + point));
                    System.Threading.Thread.Sleep(sleep);
                    //string tmp = sp.ReadExisting();
                    return GetSend(sp.ReadExisting()).ToString();
                }
                else
                {
                    sp.Write("#" + address + point + Convert.ToChar(13));
                    System.Threading.Thread.Sleep(sleep);
                    string a = GetSend(sp.ReadExisting()).ToString();
                    return a;
                }
            }
            catch
            {
                return "error!";
            }
        }


        private float GetSend(string command)
        {
            string getStr = command;
            try
            {
                if (getStr != "0")
                    getStr = getStr.Split('=')[1].Substring(0, 9);
            }
            catch { }
            float reValue = 0;
            try
            {
                reValue = float.Parse(getStr);
            }
            catch { }
            return reValue/100;
        }
    }
}

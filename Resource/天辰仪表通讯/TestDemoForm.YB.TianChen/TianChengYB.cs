using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDemoForm.YB.TianChen
{
    public class TianChengYB
    {
        public string SendStr(string command)
        {
            string strReturn = "0";
            int str = 0;

            //#0102
            if (command.Length > 0)
            {
                char[] args = command.ToCharArray();

                for (int i = 0; i < args.Length; i++)
                {
                    str += Convert.ToInt32(args[i]);
                }
                string hex = Convert.ToString(str, 16);
                while (hex.Length < 2)
                {
                    hex = "0" + hex;
                }
                string H1 = "4" + hex.Substring(hex.Length - 2, 1);
                string H2 = "4" + hex.Substring(hex.Length - 1, 1);
                //
                strReturn = Convert.ToChar(byte.Parse(H1, System.Globalization.NumberStyles.AllowHexSpecifier)).ToString() + Convert.ToChar(byte.Parse(H2, System.Globalization.NumberStyles.AllowHexSpecifier)).ToString();
                strReturn = command + strReturn + Convert.ToChar(13);
            }
            return strReturn;

        }
        public float GetSend(string command)
        {
            string getStr = SendStr(command);
            try
            {
                if (getStr != "0")
                    getStr = getStr.Substring(1, getStr.Length - 8);
            }
            catch { }
            float reValue = 0;
            try
            {
                reValue = float.Parse(getStr);
            }
            catch { }
            return reValue;
        }

        public float FormatData(string command)
        {
            if (command.Length > 0)
            {
                return float.Parse(command.Substring(2, command.Length - 5));
            }
            return 0;
        }


        /// <summary>
        /// 写仪表参数值
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="command"></param>
        /// <param name="sp"></param>
        public void WriteParameter(string addr, string command, System.IO.Ports.SerialPort sp)
        {
            sp.Write(SendStr("%" + addr + "10+1111")); 
            System.Threading.Thread.Sleep(100);

            sp.Write(SendStr("%" + addr + command));
            System.Threading.Thread.Sleep(100);


            sp.Write(SendStr("%" + addr + "10+0000"));
            System.Threading.Thread.Sleep(100);
        }

        /// <summary>
        /// 读仪表参数值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sp"></param>
        public string ReadParameter(string command, System.IO.Ports.SerialPort sp)
        {
            sp.Write(SendStr(command));
            System.Threading.Thread.Sleep(200);

            return FormatData(sp.ReadExisting()).ToString();

        }
    }
}

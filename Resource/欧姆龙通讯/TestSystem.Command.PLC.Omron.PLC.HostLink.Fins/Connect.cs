using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 欧姆龙通信
{
    public class Connect
    {
        /// <summary>
        /// 将数据进行FCS校验，并返回一个完整的命令字符串
        /// </summary>
        /// <param name="data">增加FCS校验的数据</param>
        /// <returns>带有FCS校验验并可发送到 OMRON PLC的数据</returns>
        public static string FCS(string data)
        {
            string hostlink = data + ComputeFCS(data) + "*" + (char)13;
            return hostlink; 
        }

        /// 上位机校验
        /// </summary>
        /// <param name="linkstring">进行校验的数据，以@开始的字符串</param>
        /// <returns></returns>
        private static string ComputeFCS(string linkstring)
        {
            char inFcs = (char)linkstring[0];
            int fcsResult = (int)inFcs;
            for (int i = 1; i < linkstring.Length; i++)
            {
                inFcs = (char)linkstring[i];
                fcsResult ^= (int)inFcs;
            }
            return fcsResult.ToString("X");
        }
        /// <summary>
        /// 对带有FCS校验的数据进行校验
        /// </summary>
        /// <param name="receives">带有FCS的数据</param>
        /// <returns>校验正确返回 true 否则返回 false</returns>
        public static bool CheckFCS(string receives)
        {
            int i = receives.IndexOf('*');
            string data = receives.Substring(0, i - 2);
            if (receives.Substring(i - 2, 2).Equals(ComputeFCS(data)))
                return true;
            else return false;
        }

        //选择HOSTLINK通信方式

        //通信命令格式：@00RR38000000

        //@前缀

        //00站号

        //R：为读PLC    W：为写PLC

        //R：为PLC地址区域CIO区

        //3800：为要操作的实际地址

        //0000：为需要读取或写入字的数量

        //@00RR38000000 进行校验后 + 结束符 =最终发送命令


       // http://wenku.baidu.com/view/7f22d2d0b9f3f90f76c61b22.html
    }
}

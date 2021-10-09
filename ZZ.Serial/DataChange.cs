using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZ.Serial
{
    public class DataChange
    {
        /// <summary>
        /// 16进制字符转整型
        /// </summary>
        public static int HexToInt(string Hex)
        {
            try
            {
                int s = int.Parse(Hex, System.Globalization.NumberStyles.HexNumber);
                
                //int s = Convert.ToInt32(Hex,16);// int.Parse(Hex.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                return s;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// ASC转16进制字符
        /// </summary>
        public static string AscToHex(int Asc)
        {
            try
            {
                string s = Convert.ToString(Asc, 16);
                if (s.Length == 1)
                    s = "0" + s;
                return s;
            }
            catch
            {
                return "00";
            }
        }


   

        /// <summary>   
        /// 判断是否是数字   
        /// </summary>   
        /// <param name="str">字符串</param>   
        /// <returns>bool</returns>   
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 整型转16进制字符
        /// </summary>
        public static string IntToHex(string str)
        {
            string Lowstr = "";
            int i = Convert.ToInt32(str);
            string Hex_str = Convert.ToString(i, 16);
            int len = Hex_str.Length;
            switch (len)
            {
                case 1: Lowstr = "000" + Hex_str; break;
                case 2: Lowstr = "00" + Hex_str; break;
                case 3: Lowstr = "0" + Hex_str; break;
                case 4: Lowstr = Hex_str; break;
                default: Lowstr = "0000"; break;
            }
            string Upperstr = Lowstr.ToUpper();//将小写转大写。如eb变为EB
            return Upperstr;
        }


        public static string IntToHexStr(int num)
        {
            string Hex_str = Convert.ToString(num, 16);
            for (int i = Hex_str.Length; i < 4; i++)
            {
                Hex_str = "0" + Hex_str;
            }
            return Hex_str.ToUpper();
        }


        //16转2
        /// <summary>
        /// 16进制转2进制
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        public static string HexToBit(string dataStr)
        {
            string ReStr = "";
            ReStr = Convert.ToString(Convert.ToInt32(dataStr, 16), 2);
            while (ReStr.Length < 8)
            {
                ReStr = "0" + ReStr;
            }
            return ReStr;
        }
        //public static string Hex1ToBit(string ch)
        //{
        //    switch (ch)
        //    {
        //        case "0":
        //            return "0000";
        //        case "1":
        //            return "1000"; //0001
        //        case "2":
        //            return "0100"; //0010
        //        case "3":
        //            return "1100"; //0011
        //        case "4":
        //            return "0010"; //0100
        //        case "5":
        //            return "1010"; //0101
        //        case "6":
        //            return "0110"; //0110
        //        case "7":
        //            return "1110"; //0111
        //        case "8":
        //            return "0001"; //1000
        //        case "9":
        //            return "1001"; //1001
        //        case "A":
        //            return "0101"; //1010
        //        case "B":
        //            return "1101"; //1011
        //        case "C":
        //            return "0011"; //1100
        //        case "D":
        //            return "1011"; //1101
        //        case "E":
        //            return "0111"; //1110
        //        case "F":
        //            return "1111"; //1111
        //    }
        //    return "0000";
        //}


        public static string[] IntToHex(int num)
        {
            string[] str = new string[2];
            string Hex_str = Convert.ToString(num, 16);
            int len = Hex_str.Length;
            switch (len)
            {
                case 1:
                    str[0] = "00";
                    str[1] = "0" + Hex_str.ToUpper();
                    break;
                case 2:
                    str[0] = "00";
                    str[1] = Hex_str.ToUpper();
                    break;
                case 3:
                    str[0] = "0" + Hex_str.Substring(0, 1).ToUpper();
                    str[1] = Hex_str.Substring(1, 2).ToUpper();
                    break;
                case 4:
                    str[0] = Hex_str.Substring(0, 2).ToUpper();
                    str[1] = Hex_str.Substring(2, 2).ToUpper();
                    break;
                default:
                    str[0] = "00";
                    str[1] = "00";
                    break;
            }
            return str;
        }

        public static string Right(string var1, int var2)
        {

            string result;
            result = var1.Substring(var1.Length - var2, var2);

            return result;
        }


        public static string Left(string var1, int var2)
        {
            return var1.Substring(0, var2);
        }


        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Reverse(string data)
        {
            string sReturn = "";
            for (int i = data.Length - 1; i >= 0; i--)
            {
                sReturn += data[i];
            }
            return sReturn;
        }

    }
}

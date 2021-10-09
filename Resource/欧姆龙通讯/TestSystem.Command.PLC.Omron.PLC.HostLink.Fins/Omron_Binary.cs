using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    /// <summary>
    /// 对于二进制转换代码
    /// </summary>
    public class Omron_Binary
    {
        //给个0.01
        public string Binary(string number, bool onOroff, int AreaValue)
        {
            string NewBinary = "";
            try
            {
                //将内存区域当前的值读取出来转换成二进制
                string areaBinary=  Convert.ToString(AreaValue, 2);
               
                string bit ="0."+ number.Split('.')[1];

                float length = float.Parse(bit) * 100;
                int value = (int)(length)+1;

                byte[]  bitBinary=new byte[16];
                //11101
                int index = 0;
                for (int i = bitBinary.Length-1; i >=0;i--)
                {
                    if (areaBinary.Length > index)
                    {
                        bitBinary[i] = Convert.ToByte(areaBinary.Substring(areaBinary.Length - (index + 1), 1));
                        index++;
                    }
                    else
                    {
                        bitBinary[i] = Convert.ToByte("0");
                    }
                }

                if (onOroff)
                {
                    bitBinary[bitBinary.Length -value] = Convert.ToByte("1");
                }
                else
                {
                    bitBinary[bitBinary.Length - value] = Convert.ToByte("0");
                }

               
                if (areaBinary.Length > value)
                {
                    for (int i = bitBinary.Length - areaBinary.Length; i <bitBinary.Length ; i++)
                    {
                        NewBinary += Convert.ToString(bitBinary[i]);
                    }
                   
                }
                else if (areaBinary.Length < value)
                {
                    for (int i = bitBinary.Length - value; i < bitBinary.Length; i++)
                    {
                        NewBinary += Convert.ToString(bitBinary[i]);
                    }
                }
                else
                {
                    for (int i = bitBinary.Length - value; i < bitBinary.Length; i++)
                    {
                        NewBinary += Convert.ToString(bitBinary[i]);
                    }

                }

            }
            catch
            {
 
            }

            return NewBinary;
        }
        /// <summary>
        /// 获得点位中对应的值 例如D100.00  先获取D100 对应的值 在获得00 这个位置的状态(0/1)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="AreaValue"></param>
        /// <returns></returns>
        public string BinaryConvert(string number, int AreaValue)
        {
            string str ="";
            try
            {
                
                string bit = "0." + number;
                float length = float.Parse(bit) * 100;

                string areaBinary = Convert.ToString(AreaValue, 2);
                //if(areaBinary.Length
                for(int i=areaBinary.Length;i<16;i++)
                {
                    areaBinary = "0" + areaBinary;
                }
                
                 str = areaBinary.Substring(areaBinary.Length - ((int)length + 1), 1);
               

            }
            catch
            {
                str = "0";
            }
            return str;

        }
        /// <summary>
        /// 将二进制数据转换成字符数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string[] BinaryConvertToArr(string value)
        {
            string[] strarr = new string[16];
            for (int i = 0; i < strarr.Length; i++)
            {
                if (i < value.Length)
                {
                    strarr[i] = value.Substring(value.Length - (i + 1), 1);
                }
                else
                {
                    strarr[i] = "0";
                }
            }

            return strarr;
        }
        /// <summary>
        /// 十进制转换成二进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Int32ToBinary(int value)
        {
            string binary = "";
            try
            {

                binary = Convert.ToString(value, 2);

            }
            catch
            { }
            return binary;
        }
        /// <summary>
        /// 二进制转十进制
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public int BinaryConvertToInt32(string binary)
        {
            int value = 0;
            try
            {
                
               value=Convert.ToInt32(binary,2);
                
            }
            catch
            { }
            return value;
        }
        /// <summary>
        /// 二进制转十六进制
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public string BinaryConvertToHex(string binary)
        {
            string value = "";
            try
            {
                value = Convert.ToString(Convert.ToInt32(binary), 16);
            }
            catch
            { }
            return value;
        }


        ///<summary>
        ///BCD转换成十进制
        ///</summary>
        ///<param name="num"></param>
        ///<returns></returns>
        public uint BcdToDec(uint num)
        {
            return HornerScheme(num, 0x10, 10);
        }
        ///<summary>
        ///十进制转换成BCD码
        ///</summary>
        ///<param name="num"></param>
        ///<returns></returns>
        public  uint DecToBcd(uint num)
        {
            return HornerScheme(num, 10, 0x10);
        }

        /// <summary>
        /// BCD转换功能
        /// </summary>
        /// <param name="num"></param>
        /// <param name="divider"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static uint HornerScheme(uint num, uint divider, uint factor)
        {
            uint remainder = 0, quotient = 0, result = 0;
            remainder = num % divider;
            quotient = num / divider;
            if (!(quotient == 0 && remainder == 0))
                result += HornerScheme(quotient, divider, factor) * factor + remainder;
            return result;
        }
    }
}

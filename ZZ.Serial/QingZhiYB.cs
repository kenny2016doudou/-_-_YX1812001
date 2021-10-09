using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZZ.Serial
{
    /// <summary>
    /// 青智仪表
    /// </summary>
    public class QingZhiYB
    {
        private static int[] datas_2B = new int[32];


        public static decimal GetData(string dataStr, int len)
        {
            string[] str = dataStr.Split('-');
            string str2B = DataChange.HexToBit(str[0]) + DataChange.HexToBit(str[1]) + DataChange.HexToBit(str[2]) + DataChange.HexToBit(str[3]);
            //将2存入数组
            for (int i = 0; i < datas_2B.Length; i++)
            {
                string tempStr = str2B;
                datas_2B[i] = Convert.ToInt32(tempStr.Substring(i, 1));
            }
            //计算
            //
            return DataFunction(len);
        }

        /// <summary>
        /// 计算校验核
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private static decimal DataFunction(int len)
        {
            //符号位
            double S = 0;
            //指数
            double E = 0;
            double Ex = 127;
            //小数
            decimal F = 0;

            S = datas_2B[0];
            string eStr = datas_2B[1].ToString() + datas_2B[2].ToString() + datas_2B[3].ToString() + datas_2B[4].ToString() + datas_2B[5].ToString() + datas_2B[6].ToString() + datas_2B[7].ToString() + datas_2B[8].ToString();
            E = Convert.ToInt32(eStr, 2);

            decimal baseNum = 2;
            decimal[] allNum = new decimal[23];
            for (int i = 9; i < datas_2B.Length; i++)
            {
                decimal num = Convert.ToDecimal(datas_2B[i]);
                if (num == 0)
                {
                    allNum[i - 9] = 0;
                    continue;
                }
                else
                {
                    decimal x = i - 8;
                    decimal y = Convert.ToDecimal(Math.Pow(Convert.ToDouble(baseNum), Convert.ToDouble(x)));
                    allNum[i - 9] = 1 / y;
                }
            }

            foreach (decimal tNum in allNum)
            {
                F = F + tNum;
            }
            decimal Fdata = 0;
            try
            {
                decimal A = Convert.ToDecimal(Math.Pow(-1, S));
                decimal B = Convert.ToDecimal(1 + F);
                decimal C = Convert.ToDecimal(Math.Pow(2, E - Ex));

                Fdata = A * B * C;
            }
            catch
            {
                Fdata = 0;
            }
            return Decimal.Round(Fdata, len);
        }
    }
}

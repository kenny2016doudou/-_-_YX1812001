using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSpecificTools.PlcConnectivity
{
    class PlcTags
    {
        #region V 区

        public const string BitVariable = "DB1.DBX0.0";
        public const string IntVariable = "DB1.DBW2";
        public const string DoubleVariable = "DB1.DBD4";
        public const string DIntVariable = "DB1.DBD8";
        public const string DwordVariable = "DB1.DBW12";

        #endregion

        #region M 区

        public const string BitVariable_M0 = "M0.0";
        public const string ByteVariable_M1 = "MB1";
        public const string BitVariable_M2 = "M0.2";

        #endregion
    }
}
/*
 * bit = 1位
 * byte = 8位;
 * word = 16位;
 * double word = 32位
 * 
 * 
 * V 区bit写法:DB1.DBX0.0
 * V 区IntVariable写法:DB1.DBW2
 * V 区RealVariable写法:DB1.DBD4
 * V 区DIntVariable写法:DB1.DBD8
 * V 区DwordVariable写法:DB1.DBW12 (ASCII)
 * DBX = bit 
 * DBB = byte
 * DBW = word
 * DBD = Double word
 * 
 * 
 * I O M区bit写法如:M00.0  M00.7 
 * 
 * 
 *  M A E区类似
 *  Memory byte  MB1
 *  Memory word  MW1
 *  Memory double-word   MD1
 */

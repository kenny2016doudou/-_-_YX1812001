using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public class OmronCommandData
    {
        /// <summary>
        /// 对应内存区域地址
        /// </summary>
        public AreaCode AC { get; set; }
        /// <summary>
        /// 对应的数据类型
        /// </summary>
        public DataType DType { get; set; }
        /// <summary>
        /// 对应的控制点例如W0.00 W0.01
        /// </summary>
        public string StrAddress { get; set; }
        /// <summary>
        /// 读取或者写入的控制点 例如 H0 D130
        /// </summary>
        public int IntAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public enum CommandType
    {
        /// <summary>
        /// 读取的命令类型 对应0101
        /// </summary>
        Read,
        /// <summary>
        /// 写入的命令类型 对应0102
        /// </summary>
        Write,

        /// <summary>
        /// 读取的命令类型 对应0104
        /// </summary>
        MultipleRead
    }
}

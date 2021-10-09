using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace TestSystem.Command.Interface
{
    public interface IRead
    {
        /// <summary>
        /// 单个数据对象读取
        /// </summary>
        object Data { get; }
        /// <summary>
        /// 多个数据对象读取
        /// </summary>
        object[] DataMuster { get; }

        /// <summary>
        /// 执行读取
        /// </summary>
        void Read(ref SerialPort sp);
    }
}

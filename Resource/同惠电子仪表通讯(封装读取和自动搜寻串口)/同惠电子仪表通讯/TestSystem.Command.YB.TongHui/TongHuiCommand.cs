using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.YB.TongHui
{
    /// <summary>
    /// 命令操作对象 注意获取仪器的结果 V  应该是?
    /// </summary>
    public enum TongHuiCommand
    {
        /// <summary>
        /// 量程自动(初始值)
        /// </summary>
        R0,//量程自动(初始值)
        /// <summary>
        /// 量程保持
        /// </summary>
        RF,//量程保持
        /// <summary>
        /// 慢速(初始值)读取
        /// </summary>
        S0,//慢速(初始值)读取
        /// <summary>
        /// 快速读取
        /// </summary>
        S1,//快速读取
        /// <summary>
        /// 分选ON
        /// </summary>
        S2,//分选ON
        /// <summary>
        /// 分选OFF
        /// </summary>
        S3,//分选OFF
        /// <summary>
        /// 显示电阻值(初始值) 对应仪表R
        /// </summary>
        S4,//显示电阻值(初始值) 对应仪表R
        /// <summary>
        /// 显示百分比  对应仪表%
        /// </summary>
        S5,//显示百分比  对应仪表%
        /// <summary>
        /// 连续触发(初始值)
        /// </summary>
        S6,//连续触发(初始值)
        /// <summary>
        /// 单次触发或者外触发
        /// </summary>
        S7,//单次触发或者外触发
        /// <summary>
        /// 清零ON
        /// </summary>
        S8,//清零ON
        /// <summary>
        /// 清零OFF
        /// </summary>
        S9,//清零OFF
        /// <summary>
        /// RS232 Print ON
        /// </summary>
        SP,//RS232 Print ON
        /// <summary>
        /// 启动
        /// </summary>
        G,//启动
        /// <summary>
        /// 请求发送仪器测试结果 最终发送结果应该是?
        /// </summary>
        V,//请求发送仪器测试结果 最终发送结果应该是?
        /// <summary>
        /// 标称值设定 例如:N01900 如果在20m量程则表示标准值设定为1.900m
        /// </summary>
        Nnnnnn,//标称值设定 例如:N01900 如果在20m量程则表示标准值设定为1.900m
        /// <summary>
        /// 下限值设定,例如L090, 表示设定下限值为-9.0%
        /// </summary>
        Lnnn,//下限值设定,例如L090, 表示设定下限值为-9.0%
        /// <summary>
        /// 上限值设定,例如L090,表示设定上限值为+9.0%
        /// </summary>
        Hnnn,//上限值设定,例如L090,表示设定上限值为+9.0%

    }
}

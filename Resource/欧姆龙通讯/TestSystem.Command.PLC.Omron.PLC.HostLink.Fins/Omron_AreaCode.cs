using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem.Command.PLC.Omron.HostLink.Fins
{
    public enum AreaCode
    {
        /// <summary>
        /// x区域 bit数据操作
        /// </summary>
        CIO_Bit,
        /// <summary>
        /// CIO区域 word 数据操作
        /// </summary>
        CIO_Word,
        /// <summary>
        /// 内部辅助继电器bit位操作
        /// </summary>
        WR_Bit,
        /// <summary>
        /// 内部辅助继电器Word操作
        /// </summary>
        WR_Word,
        /// <summary>
        /// 保持继电器Bit操作
        /// </summary>
        HR_Bit,
        /// <summary>
        /// 保持继电器Word操作
        /// </summary>
        HR_Word,
        /// <summary>
        /// 特殊辅助继电器bit操作
        /// </summary>
        AR_Bit,
        /// <summary>
        /// 特殊辅助继电器word操作
        /// </summary>
        AR_Word,
        
        TM_Flag,

        CNT_Flag, 
        TIM_PV,

        CNT_PV,
        /// <summary>
        /// 数据存储器bit操作
        /// </summary>
        DM_Bit,
        /// <summary>
        /// 数据存储器word操作
        /// </summary>
        DM_Word,
    }
}

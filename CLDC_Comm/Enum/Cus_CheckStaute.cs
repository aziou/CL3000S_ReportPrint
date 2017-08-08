using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 检定状态,0,2,32,16独立状态，（1、8）与（4）要组合
    /// fjk
    /// </summary>
    [Flags]
    public enum Cus_CheckStaute
    {
        /// <summary>
        /// 
        /// </summary>
        未赋值的=0,
        /// <summary>
        /// 
        /// </summary>
        检定=1,
        /// <summary>
        /// 
        /// </summary>
        停止检定=2,
        /// <summary>
        /// 
        /// </summary>
        调表=4,
        /// <summary>
        /// 
        /// </summary>
        单步检定=8,
        /// <summary>
        /// 
        /// </summary>
        录入完成=16,
        /// <summary>
        /// 
        /// </summary>
        错误=32
    }
}

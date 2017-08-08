using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    
    
    /// <summary>
    /// 试验类型
    /// </summary>
    public enum Cus_TaskType
    {
        /// <summary>
        /// 
        /// </summary>
        电能误差 = 0,
        /// <summary>
        /// 
        /// </summary>
        需量周期 = 1,
        /// <summary>
        /// 
        /// </summary>
        时钟日误差 = 2,
        /// <summary>
        /// 
        /// </summary>
        脉冲计数 = 3,
        /// <summary>
        /// 
        /// </summary>
        对标 = 4,
        /// <summary>
        /// 
        /// </summary>
        走字 = 5,
        /// <summary>
        /// 
        /// </summary>
        设置预付费试验 = 6,
        /// <summary>
        /// 
        /// </summary>
        设置底度对齐 = 7
    }
    /// <summary>
    /// 标准脉冲类型
    /// </summary>
    public enum enmStdPulseType
    {
        /// <summary>
        /// 
        /// </summary>
        标准时钟脉冲 = 0,
        /// <summary>
        /// 
        /// </summary>
        标准电能脉冲 = 1
    }
}

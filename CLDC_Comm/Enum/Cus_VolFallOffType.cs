using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 电压跌落试验类型
    /// </summary>
    public enum Cus_VolFallOffType
    {
       
        /// <summary>
        /// 
        /// </summary>
        电压跌落和短时中断 = 0,
        /// <summary>
        /// 
        /// </summary>
        电压逐渐变化 = 1,
        /// <summary>
        /// 
        /// </summary>
        逐渐关机和启动 = 2
    }
}

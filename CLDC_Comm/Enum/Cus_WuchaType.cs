using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 误差板当前状态
    /// </summary>
    public enum Cus_WuchaType
    {
        /// <summary>
        /// 电能误差
        /// </summary>
        电能误差 = 0,
        /// <summary>
        /// 需量周期误差
        /// </summary>
        需量周期误差 = 1,
        /// <summary>
        /// 日计时误差
        /// </summary>
        日计时误差 = 2,
        /// <summary>
        /// 脉冲个数
        /// </summary>
        脉冲个数 = 3,
        /// <summary>
        /// 对标状态
        /// </summary>
        对标状态=4
    }
}

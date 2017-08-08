using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 电源工作流程
    /// </summary>
    public enum Cus_PowerWorkFlow
    {
        /// <summary>
        /// 未进行过电源操作
        /// </summary>
        None,

        /// <summary>
        /// 升源
        /// </summary>
        PowerOn,

        /// <summary>
        /// 关源
        /// </summary>
        PowerOff
    }
}

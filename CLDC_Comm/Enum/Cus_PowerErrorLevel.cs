using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 电源错误等级
    /// </summary>
    [Serializable]
    public enum Cus_PowerErrorLevel
    {
        /// <summary>
        /// 提示信息，
        /// </summary>
        提示,

        /// <summary>
        /// 警告信息
        /// </summary>
        警告,

        /// <summary>
        /// 错误，服务端应该停止检定工作
        /// </summary>
        错误
    }
}

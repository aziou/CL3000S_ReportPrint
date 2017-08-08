using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 误差一致性项目枚举
    /// </summary>
    public enum Cus_ErrorAccordItem
    {
        /// <summary>
        /// 无效的项目
        /// </summary>
        无效的项目 = 0,
        /// <summary>
        /// 1
        /// </summary>
        误差一致性 = 1,
        /// <summary>
        /// 2
        /// </summary>
        误差变差 = 2,
        /// <summary>
        /// 3
        /// </summary>
        负载电流升降变差 = 3,
        /// <summary>
        /// 4
        /// </summary>
        电流过载 = 4,
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
    {
    /// <summary>
    /// 检定类型
    /// </summary>
    public enum Cus_TestType
        {
        /// <summary>
        /// 错误类型
        /// </summary>
        CHECK_INVALID = 0,
        /// <summary>
        /// 首检
        /// </summary>
        CHECK_FIRST = 1,

        /// <summary>
        /// 周期检定
        /// </summary>
        CHECK_CYCLE = 2,
        /// <summary>
        /// 验收检定
        /// </summary>
        CHECK_ACCEPT = 3,

        /// <summary>
        /// 自定义
        /// </summary>
        CHECK_OTHER =4
        }
    }

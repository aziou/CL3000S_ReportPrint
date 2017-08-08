using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 误差试验类型
    /// </summary>
    public enum  Cus_WcType
    {
        /// <summary>
        /// 错误
        /// </summary>
        error=0,
        /// <summary>
        /// 基本误差
        /// </summary>
        基本误差=1,
        /// <summary>
        /// 标准偏差
        /// </summary>
        标准偏差=2,
        /// <summary>
        /// 特殊检定
        /// </summary>
        特殊检定=3,
        /// <summary>
        /// 误差一致性
        /// </summary>
        误差一致性=4,
        /// <summary>
        /// 误差变差试验
        /// </summary>
        误差变差试验=5,
        /// <summary>
        /// 电流升降试验
        /// </summary>
        电流升降试验=6,
        /// <summary>
        /// 电流过载试验
        /// </summary>
        电流过载试验=7
    }
}

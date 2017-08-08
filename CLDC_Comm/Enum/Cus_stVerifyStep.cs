using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 检定状态维护
    /// </summary>
    public enum Cus_stVerifyStep
    {
        /// <summary>
        /// 空闲状态
        /// </summary>
        空闲状态=0,
        /// <summary>
        /// 参数录入
        /// </summary>
        参数录入=1,
        /// <summary>
        /// 误差检定
        /// </summary>
        误差检定=2,
        /// <summary>
        /// 走字试验录起码
        /// </summary>
        走字试验录起码=3,
        /// <summary>
        /// 走字试验录起码完毕
        /// </summary>
        走字试验录起码完毕=4,
        /// <summary>
        /// 走字试验录止码
        /// </summary>
        走字试验录止码=5,
        /// <summary>
        /// 走字试验录止码完毕
        /// </summary>
        走字试验录止码完毕=6,
        /// <summary>
        /// 计算误差完毕
        /// </summary>
        计算误差完毕=7,
        /// <summary>
        /// 多功能检定
        /// </summary>
        多功能检定=8,
        /// <summary>
        /// 特殊检定
        /// </summary>
        特殊检定=9
    }
}

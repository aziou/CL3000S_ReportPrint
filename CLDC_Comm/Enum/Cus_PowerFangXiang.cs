using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    #region 功率方向
    /// <summary>
    /// 功率方向
    /// </summary>
    public enum Cus_PowerFangXiang
    {
        /// <summary>
        /// 组合有功
        /// </summary>
        组合有功 = 0,

        /// <summary>
        /// 正向有功
        /// </summary>
        正向有功 = 1,

        /// <summary>
        /// 反向有功
        /// </summary>
        反向有功 = 2,

        /// <summary>
        /// 正向无功
        /// </summary>
        正向无功 = 3,

        /// <summary>
        /// 反向无功
        /// </summary>
        反向无功 = 4,

        /// <summary>
        /// 第一象限无功
        /// </summary>
        第一象限无功 = 5,

        /// <summary>
        /// 第二象限无功
        /// </summary>
        第二象限无功 = 6,

        /// <summary>
        /// 第三象限无功
        /// </summary>
        第三象限无功 = 7,

        /// <summary>
        /// 第四象限无功
        /// </summary>
        第四象限无功 = 8,

        /// <summary>
        /// 错误的、未赋值的
        /// </summary>
         Error= 9,

    }
    #endregion

    /// <summary>
    /// 需量方向
    /// </summary>
    public enum XuliangFangxiang
    {
        正向有功最大需量 =0,
        反向有功最大需量 =1,
        组合无功1最大需量 =2,
        组合无功2最大需量 =3,
        第一象限无功最大需量 =4,
        第二象限无功最大需量 =5,
        第三象限无功最大需量 =6,
        第四象限无功最大需量 =7,
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    #region 相序
    /// <summary>
    /// 相序
    /// </summary>
    public enum Cus_PowerPhase
    {
        /// <summary>
        /// 错误的、未赋值的
        /// </summary>
        Error = -1,

        /// <summary>
        /// 正相序
        /// </summary>
        正相序 = 0,

        /// <summary>
        /// 电压逆相序
        /// </summary>
        电压逆相序 = 1,

        /// <summary>
        /// 电流逆相序
        /// </summary>
        电流逆相序 = 2,

        
        
    }
    #endregion    
}

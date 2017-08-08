using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 通讯选择
    /// </summary>
    public enum Cus_LightSelect
    {
        /// <summary>
        /// 
        /// </summary>
        一对一模式485通讯 = 0,
        /// <summary>
        /// 
        /// </summary>
        奇数表位485通讯 = 1,
        /// <summary>
        /// 
        /// </summary>
        偶数表位485通讯 = 2,
        /// <summary>
        /// 
        /// </summary>
        一对一模式红外通讯 = 3,
        /// <summary>
        /// 
        /// </summary>
        奇数表位红外通讯 = 4,
        /// <summary>
        /// 
        /// </summary>
        偶数表位红外通讯 = 5,
        /// <summary>
        /// 
        /// </summary>
        切换到485总线 = 6       //电科院协议用
    }
}

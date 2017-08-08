using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    ///<summary>
    ///FileName:Cus_WorkStatus.cs
    ///CLRVersion:2.0.50727.5472
    ///Author:kaury
    ///Corporation:
    ///Description:
    ///DateTime:1/8/2014 10:53:15 AM
    ///</summary>
    /// <summary>
    /// 设备工作状态
    /// </summary>
    public enum Cus_WorkStatus : byte
    {
        /// <summary>
        /// 
        /// </summary>
        正常 = 0,
        /// <summary>
        /// 
        /// </summary>
        工作中 = 1,
        /// <summary>
        /// 
        /// </summary>
        未联机 = 2,
        /// <summary>
        /// 
        /// </summary>
        联机失败 = 3,
        /// <summary>
        /// 
        /// </summary>
        过载 = 4,
        /// <summary>
        /// 
        /// </summary>
        升源失败 = 5
    }
}

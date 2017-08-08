using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{

    /// <summary>
    /// 多功能协议参数对照
    /// </summary>
    public enum Cus_DgnProcotolPara
    {
        /// <summary>
        /// 
        /// </summary>
        错误参数 = 0,
        /// <summary>
        /// 
        /// </summary>
        通信测试 = 001,
        /// <summary>
        /// 
        /// </summary>
        写表时间 = 002,
        /// <summary>
        /// 
        /// </summary>
        读表时间 = 003,
        /// <summary>
        /// 
        /// </summary>
        清空需量 = 004,
        /// <summary>
        /// 
        /// </summary>
        读取需量 = 005,
        /// <summary>
        /// 
        /// </summary>
        读取电量 = 006,
        /// <summary>
        /// 
        /// </summary>
        读取时段 = 007,
        /// <summary>
        /// 
        /// </summary>
        清空电量 = 008,
        /// <summary>
        /// 
        /// </summary>
        事件记录 = 100,
        /// <summary>
        /// 
        /// </summary>
        瞬时寄存器 = 101,
        /// <summary>
        /// 
        /// </summary>
        状态寄存器 = 102,
        /// <summary>
        /// 
        /// </summary>
        失压寄存器 = 103,
        /// <summary>
        /// 
        /// </summary>
        运行状态 = 104,
        /// <summary>
        /// 
        /// </summary>
        预付费检查=105
    }
}

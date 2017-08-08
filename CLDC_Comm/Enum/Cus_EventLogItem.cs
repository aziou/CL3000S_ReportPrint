using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 事件记录检定项目枚举
    /// </summary>
    public enum Cus_EventLogItem
    {
        /// <summary>
        /// 无效的项目
        /// </summary>
        无效的项目 = 000,
        /// <summary>
        /// 失压记录
        /// </summary>
        失压记录 = 001,
        /// <summary>
        /// 过压记录
        /// </summary>
        过压记录 = 002,
        /// <summary>
        /// 欠压记录
        /// </summary>
        欠压记录 = 003,
        /// <summary>
        /// 失流记录
        /// </summary>
        失流记录 = 004,
        /// <summary>
        /// 断流记录
        /// </summary>
        断流记录 = 005,
        /// <summary>
        /// 过流记录
        /// </summary>
        过流记录 = 006,        
        /// <summary>
        /// 过载记录
        /// </summary>
        过载记录 = 007,
        /// <summary>
        /// 断相记录
        /// </summary>
        断相记录 = 008,
        /// <summary>
        /// 掉电记录
        /// </summary>
        掉电记录 = 009,  
        /// <summary>
        /// 全失压记录
        /// </summary>
        全失压记录 = 010, 
        /// <summary>
        ///  电压不平衡记录
        /// </summary>
        电压不平衡记录 = 011,
        /// <summary>
        ///  电流不平衡记录
        /// </summary>
        电流不平衡记录 = 012,
        /// <summary>
        /// 逆相序记录
        /// </summary>
        电压逆相序记录 = 013,
        /// <summary>
        /// 逆相序记录
        /// </summary>
        电流逆相序记录 = 014,   
        /// <summary>
        /// 开表盖记录
        /// </summary>
        开表盖记录 = 015,
        /// <summary>
        /// 开端钮盒记录
        /// </summary>
        开端钮盒记录 = 016,
        /// <summary>
        /// 编程记录
        /// </summary>
        编程记录 = 017,
        /// <summary>
        /// 校时记录
        /// </summary>
        校时记录 = 018,        
        /// <summary>
        /// 需量清零记录
        /// </summary>
        需量清零记录 = 019, 
        /// <summary>
        /// 事件清零记录
        /// </summary>
        事件清零记录 = 020,
        /// <summary>
        /// 电量清零记录
        /// </summary>               
        电表清零记录 = 021,        
        /// <summary>
        /// 潮流反向记录
        /// </summary>
        潮流反向记录 = 022,
        /// <summary>
        /// 功率反向记录
        /// </summary>
        功率反向记录 = 023,
        /// <summary>
        /// 需量超限记录
        /// </summary>
        需量超限记录 = 024,
        /// <summary>
        /// 功率因数超下限记录
        /// </summary>
        功率因数超下限记录 = 025,
        /// <summary>
        /// 过流载波记录
        /// </summary>
        过流载波记录 = 026,

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
    {
    /// <summary>
    /// 集控工作状态枚举
    /// </summary>
    public enum Cus_SetStatus
        {
        /// <summary>
        /// 错误状态
        /// </summary>
            CUSINVALID=0,

        /// <summary>
        /// 空闲状态
        /// </summary>
            CUSFREE=1,

        /// <summary>
        /// 已经连机
        /// </summary>
        CUSCONNECT=2,

        /// <summary>
        /// 工作中
        /// </summary>
        CUSWORKING=3,

        /// <summary>
        /// 项目检定完毕
        /// </summary>
        CUSCHECKOVER=4,

        /// <summary>
        /// 脱机控制
        /// </summary>
        CUSCONTROL=5,

        /// <summary>
        /// 断开
        /// </summary>
        CUSCLOSED=6
        }
    }

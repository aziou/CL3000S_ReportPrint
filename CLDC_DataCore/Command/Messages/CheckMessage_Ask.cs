using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Command.Messages
{
    /// <summary>
    /// 状态报告
    /// </summary>
    [Serializable()]
    public class CheckMessage_Ask : Command_Ask
    {
        /// <summary>
        /// 下面是数据字段
        /// </summary>
        public CLDC_Comm.MessageArgs.EventMessageArgs MessageArgs = null;
        /// <summary>
        /// 报告当前检定状态
        /// </summary>
        public Cus_CheckStaute checkState = Cus_CheckStaute.未赋值的;
        /// <summary>
        /// 当前进度时间
        /// </summary>
        public float NowMinute = 1;
        /// <summary>
        /// 
        /// </summary>
        public CheckMessage_Ask()
        {
            AskMessage = "检定状态报告";
        }
    }

}

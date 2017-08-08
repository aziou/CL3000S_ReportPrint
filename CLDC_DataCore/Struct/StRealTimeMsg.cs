using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 检定器消息结构
    /// </summary>
    public struct StRealTimeMsg
    {
        /// <summary>
        /// 消息发送者
        /// </summary>
        public object objSender;
        /// <summary>
        /// 消息参数
        /// </summary>
        public EventArgs objEventArgs;

        /// <summary>
        /// 数据参数
        /// </summary>
        public CLDC_CTNProtocol.CTNPCommand cmdData; 
    }
}

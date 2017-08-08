using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Messages
{
    /// <summary>
    /// 状态报告回复
    /// </summary>
    [Serializable]
    public class CheckMessage_Answer : CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public CheckMessage_Answer()
        {
        }
    }
}

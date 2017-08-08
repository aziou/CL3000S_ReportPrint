using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Command.Error
{
    /// <summary>
    /// 系统源 发生错误
    /// </summary>
    [Serializable]
    public class SendPowerError_Ask : Command_Ask
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// 错误等级
        /// </summary>
        public Cus_PowerErrorLevel ErrorLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public SendPowerError_Ask()
        {
            AskMessage = "电源发生错误";
        }
    }
}

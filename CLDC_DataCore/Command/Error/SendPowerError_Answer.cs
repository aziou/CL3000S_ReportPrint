using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;

namespace CLDC_DataCore.Command.Error
{
    /// <summary>
    /// 电源错误，应答信息
    /// </summary>
    [Serializable]
    public class SendPowerError_Answer : Command_Answer
    {

        /// <summary>
        /// 服务端处理结果
        /// </summary>
        public int ProcessResult
        {
            get;
            set;
        }
    }
}

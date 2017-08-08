using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// 发送总模型
    /// </summary>
    [Serializable]
    public class SendMeterGroup_Answer : Command_Answer 
    {
        /// <summary>
        /// 接收结果
        /// </summary>
        public bool bAgree;


    }
}

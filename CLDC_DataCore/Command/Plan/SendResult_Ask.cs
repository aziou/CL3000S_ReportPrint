using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// 报告接收方案文件的结果
    /// </summary>
    [Serializable]
    public class SendResult_Ask : Command_Ask 
    {
        /// <summary>
        /// 报告是否接收成功
        /// </summary>
        public bool IsRecvSccuess = false;

        public SendResult_Ask()
        {
            AskMessage = "接收方案文件结果";
        }

    }



}

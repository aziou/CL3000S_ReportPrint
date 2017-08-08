using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 请求停止检定的回答
    /// </summary>
    [Serializable]
    public class CheckStop_Answer : Command_Answer
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool bAgree;


    }
}
